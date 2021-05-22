using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Imaging;

namespace KingdomManager
{
    public partial class Main : Form
    {
        #region Interop Functions
        // 프로그램의 최상위 핸들을 찾아주는 함수
        [DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // 프로그램의 자식 핸들을 찾아주는 함수
        [DllImport("user32")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd1, int hWnd2, string lp1, string lp2);

        // 핸들로부터 이미지를 출력하는 함수
        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcblt, int nFlags);

        // 핸들에 메시지를 전달하는 함수
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        #endregion


        #region const & readonly Variables
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_LBUTTONMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MOUSEWHEEL = 0x20A;

        const int testerWidth = 1280;
        const int testerHeight = 720;

        readonly string[,] programList = { //{"ComboBox에 보여줄 이름", "프로세스 이름", "윈도우핸들이름", "윈도우핸들클래스" }, 
            {"LD플레이어", "dnplayer", "RenderWindow", "TheRender" }
        };
        #endregion

        #region Local Variables
        public bool isLinked;
        public IntPtr linkedHandle;
        public int linkedWidth;
        public int linkedHeight;

        public bool isRunMacro;

        public bool threadClose;

        class BitmapData
        {
            Bitmap originalBitmap;
            int originalWidth;
            int originalHeight;
            int screenWidth;
            int screenHeight;
            public Bitmap resultBitmap;
            public int currentScreenWidth;
            public int currentScreenHeight;

            public BitmapData(Bitmap bitmap, int width, int height)
            {
                originalBitmap = bitmap;
                originalWidth = bitmap.Width;
                originalHeight = bitmap.Height;
                screenWidth = width;
                screenHeight = height;
                Resize(width, height);
            }

            public void Resize(int currentWidth, int currentHeight)
            {
                if (resultBitmap != null)
                    resultBitmap.Dispose();

                float resizeWidth = (float)currentWidth / screenWidth;
                float resizeHeight = (float)currentHeight / screenHeight;

                resultBitmap = ResizeImage(originalBitmap, (int)Math.Round(originalWidth * resizeWidth), (int)Math.Round(originalHeight * resizeHeight));
                currentScreenWidth = currentWidth;
                currentScreenHeight = currentHeight;
            }
        }

        Dictionary<string, BitmapData> bitmapList;
        Bitmap currentScreen;
        #endregion

        public Main()
        {
            InitializeComponent();

            #region Initialize
            #if DEBUG == false
            tabControl.TabPages.Remove(developTab);
            #endif
            Unlink();
            isRunMacro = false;
            threadClose = false;
            bitmapList = new Dictionary<string, BitmapData>();

            Thread thread = new Thread(() => Run());
            thread.Start();
#endregion

            #region 앱플레이어 리스트 추가
            for (int i = 0; i < programList.GetLength(0); i++)
            {
                appPlayerList.Items.Add(programList[i, 0]);
            }

            appPlayerList.SelectedIndex = 0;
            #endregion

            #region 이미지 리스트 추가
            string[] files = System.IO.Directory.GetFiles("img", "*.png");
            foreach (string fileName in files)
            {
                Bitmap bmp = new Bitmap(fileName);
                BitmapData data = new BitmapData(bmp, testerWidth, testerHeight);
                bitmapList.Add(fileName, data);
            }
            #endregion
        }
        public void Run()
        {
            while (threadClose == false)
            {
                if (CoroutineManager.Runnings() > 0)
                {
                    CoroutineManager.Update();
                }
            }
        }

        #region 타이머(코루틴)
        IEnumerator Timer_Start()
        {
            while(isRunMacro)
            {
                if (currentScreen != null)
                    currentScreen.Dispose();

                currentScreen = PrintScreen();
                RefreshSize(currentScreen.Width, currentScreen.Height);

                Point? point = Find(currentScreen, "KingdomPass");
                if(point == null)
                {
                    PressKey(Keys.Escape);
                    yield return new WaitForSeconds(1);
                    continue;
                }
                TouchScreen(point.Value.X, point.Value.Y);
                yield return new WaitForSeconds(1);


                // 현재 위치 파악
                // 고기젤리 확인
                // 열기구 남은 시간 확인
                // 열기구 보내기
                // 열차 확인
                // 열차 보내기

                // 생산 시작

                // 납품 시작

                // 월드탐험 시작
            }
        }

        IEnumerator Timer_Touch(int x, int y, float duration = 0f)
        {
            if (IsLinkValid() == false)
                yield break;

            RefreshSize();

            PostMessage(linkedHandle, WM_LBUTTONDOWN, 1, CreateLParam(x, y));
            yield return new WaitForSeconds(duration);
            PostMessage(linkedHandle, WM_LBUTTONUP, 0, CreateLParam(x, y));
        }

        IEnumerator Timer_Scroll(int x, int y, int toX, int toY, float duration = 0f)
        {
            if (IsLinkValid() == false)
                yield break;

            RefreshSize();

            Random random = new Random();

            PostMessage(linkedHandle, WM_LBUTTONDOWN, 1, CreateLParam(toX, y));

            for (int i = 1; i <= 4; i++)
            {
                float t = (float)i / 4;
                yield return new WaitForSeconds(0.05f);
                SendMessage(linkedHandle, WM_LBUTTONMOVE, 1, CreateLParam(toX, (int)Math.Round((1f - t) * y + t * toY)));
            }
            SendMessage(linkedHandle, WM_LBUTTONDOWN, 1, CreateLParam(toX, toY));
            yield return new WaitForSeconds(duration);

            PostMessage(linkedHandle, WM_LBUTTONUP, 0, CreateLParam(toX, toY));
        }

        IEnumerator Timer_Produce(int id)
        {
            if (IsLinkValid() == false)
                yield break;

            id--;
            int scrollCount = id / 3;

            Random random = new Random();
            float adjustWidth = (float)linkedWidth / testerWidth;
            float adjustHeight = (float)linkedHeight / testerHeight;

            for (int i = 0; i < scrollCount; i++)
            {
                int randomHeight = random.Next(80, 700);
                int randomWidth = random.Next(770, 1240);
                Point from = new Point((int)Math.Round(adjustWidth * randomWidth), (int)Math.Round(adjustHeight * randomHeight));
                Point to = new Point((int)Math.Round(adjustWidth * randomWidth), (int)Math.Round(adjustHeight * (randomHeight - 855)));

                yield return Timer_Scroll(from.X, from.Y, to.X, to.Y, 0.5f);
                yield return new WaitForSeconds(0.5f);
            }
            int moduloID = id % 3;

            Point[] min = {
                new Point(949, 221),
                new Point(946, 430),
                new Point(950, 648)
            };
            Point[] max = {
                new Point(1211, 253),
                new Point(1210, 467),
                new Point(1210, 681)
            };

            int x = (int)Math.Round(adjustWidth * random.Next(min[moduloID].X, max[moduloID].X));
            int y = (int)Math.Round(adjustHeight * random.Next(min[moduloID].Y, max[moduloID].Y));
            yield return Timer_Touch(x, y, 0.5f);
        }

        IEnumerator Timer_Previous()
        {
            if (IsLinkValid() == false)
                yield break;


            Random random = new Random();
            float adjustWidth = (float)linkedWidth / testerWidth;
            float adjustHeight = (float)linkedHeight / testerHeight;


            Point min = new Point(210, 324);
            Point max = new Point(242, 377);

            int x = (int)Math.Round(adjustWidth * random.Next(min.X, max.X));
            int y = (int)Math.Round(adjustHeight * random.Next(min.Y, max.Y));
            yield return Timer_Touch(x, y);
        }

        IEnumerator Timer_Next()
        {
            if (IsLinkValid() == false)
                yield break;


            Random random = new Random();
            float adjustWidth = (float)linkedWidth / testerWidth;
            float adjustHeight = (float)linkedHeight / testerHeight;


            Point min = new Point(657, 324);
            Point max = new Point(689, 377);

            int x = (int)Math.Round(adjustWidth * random.Next(min.X, max.X));
            int y = (int)Math.Round(adjustHeight * random.Next(min.Y, max.Y));
            yield return Timer_Touch(x, y);
        }
        #endregion

        #region 이벤트 Listner
        private void OnLinkButtonClicked(object sender, EventArgs e)
        {
            if (isLinked)
            {
                linkButton.Text = "링크";
                Unlink();
                startButton.Enabled = false;
                appPlayerList.Enabled = true;
                targetList.Enabled = true;
            }
            else
            {
                if (Link() == false)
                    return;

                linkButton.Text = "링크해제";
                startButton.Enabled = true;
                appPlayerList.Enabled = false;
                targetList.Enabled = false;
            }
        }

        private void OnAppPlayerListSelectionChanged(object sender, EventArgs e)
        {
            targetList.Items.Clear();
            Process[] processList = Process.GetProcesses();

            foreach (Process process in processList)
            {
                String title = process.MainWindowTitle;
                if (String.IsNullOrEmpty(title) == true || process.ProcessName.Equals(programList[appPlayerList.SelectedIndex,1]) == false)
                    continue;

                targetList.Items.Add(title);
            }

            if (targetList.Items.Count > 0)
                targetList.SelectedIndex = 0;
        }
        
        private void OnDevRefreshButtonClicked(object sender, EventArgs e)
        {
            if(IsLinkValid())
            {
                Bitmap bmp = PrintScreen();
                if (bmp == null)
                    return;

                // 기존 이미지의 메모리 할당을 해제한다.
                if (devPictureBox.Image != null)
                    devPictureBox.Image.Dispose();

                devPictureBox.Width = bmp.Width;
                devPictureBox.Height = bmp.Height;
                devPictureBox.Image = bmp;
            }
        }
        
        private void OnDevPictureBoxClicked(object sender, MouseEventArgs e)
        {
            if (IsLinkValid() == false)
                return;

            Point clickPos = e.Location;
            devRichBox.Text = "X : " + clickPos.X + Environment.NewLine + 
            "Y : " + clickPos.Y;
        }

        private void OnApplicationClosing(object sender, FormClosingEventArgs e)
        {
            threadClose = true;
        }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            if(isRunMacro == false)
            {
                startButton.Text = "중지";
                isRunMacro = true;
                linkButton.Enabled = false;
                CoroutineManager.StartCoroutine(Timer_Start());
            }
            else
            {
                startButton.Text = "실행";
                isRunMacro = false;
                linkButton.Enabled = true;
                CoroutineManager.StopCoroutine(Timer_Start());
            }
        }

        private void OnTestButtonClicked(object sender, EventArgs e)
        {
            CKP_Produce(5);
            //CKP_Scroll();
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// 사용 후 반드시 Dispose로 메모리할당 해제해줘야 한다.
        /// </summary>
        /// <returns></returns>
        public Bitmap PrintScreen()
        {
            if (IsLinkValid() == false)
                return null;

            Graphics graphics = Graphics.FromHwnd(linkedHandle);
            if (graphics == null)
                return null;

            Rectangle rect = Rectangle.Round(graphics.VisibleClipBounds);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            linkedWidth = rect.Width;
            linkedHeight = rect.Height;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdc = g.GetHdc();
                PrintWindow(linkedHandle, hdc, 0x2);
                g.ReleaseHdc(hdc);
            }

            return bmp;
        }

        public void TouchScreen(int x, int y, float duration = 0f)
        {
            if (IsLinkValid() == false)
                return;

            CoroutineManager.StartCoroutine(Timer_Touch(x, y, duration));
        }

        public void PressKey(Keys key)
        {
            if (IsLinkValid() == false)
                return;

            SendMessage(linkedHandle, WM_KEYDOWN, Convert.ToInt32(key), IntPtr.Zero);
            SendMessage(linkedHandle, WM_KEYUP, Convert.ToInt32(key), IntPtr.Zero);
        }
        #endregion

        #region Private Functions
        private bool IsLinkValid()
        {
            return isLinked && linkedHandle != IntPtr.Zero;
        }
        private bool Link()
        {
            var item = targetList.SelectedItem;
            if (item == null)
                return false;

            IntPtr parentHandle = FindWindow(null, item.ToString());
            if (parentHandle == null)
                return false;

            IntPtr childHandle = FindWindowEx(parentHandle, 0, "RenderWindow", "TheRender");
            if (childHandle == null)
                return false;

            isLinked = true;
            linkedHandle = childHandle;
            return true;
        }

        private void Unlink()
        {
            isLinked = false;
            linkedHandle = IntPtr.Zero;
        }

        private void RefreshSize()
        {
            Graphics graphics = Graphics.FromHwnd(linkedHandle);
            if (graphics == null)
                return;

            linkedWidth = (int)Math.Round(graphics.VisibleClipBounds.Width);
            linkedHeight = (int)Math.Round(graphics.VisibleClipBounds.Height);
        }

        private void RefreshSize(int width, int height)
        {
            linkedWidth = width;
            linkedHeight = height;
        }

        private IntPtr CreateLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
        
        /// <summary>
         /// Resize the image to the specified width and height.
         /// </summary>
         /// <param name="image">The image to resize.</param>
         /// <param name="width">The width to resize to.</param>
         /// <param name="height">The height to resize to.</param>
         /// <returns>The resized image.</returns>
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private Point? Find(Bitmap sourceBitmap, string keyword)
        {
            if (IsLinkValid() == false)
                return null;

            keyword = @"img\" + keyword + ".png";
            var data = bitmapList[keyword];
            if (data == null)
                return null;

            if (data.currentScreenWidth != linkedWidth || data.currentScreenHeight != linkedHeight)
            {
                data.Resize(linkedWidth, linkedHeight);
            }

            var searchingBitmap = data.resultBitmap;

            if (null == sourceBitmap || null == searchingBitmap)
            {
                return null;
            }
            if (sourceBitmap.Width < searchingBitmap.Width || sourceBitmap.Height < searchingBitmap.Height)
            {
                return null;
            }

            var pixelFormatSize = Image.GetPixelFormatSize(sourceBitmap.PixelFormat) / 8;


            // Copy sourceBitmap to byte array
            var sourceBitmapData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, sourceBitmap.PixelFormat);
            var sourceBitmapBytesLength = sourceBitmapData.Stride * sourceBitmap.Height;
            var sourceBytes = new byte[sourceBitmapBytesLength];
            Marshal.Copy(sourceBitmapData.Scan0, sourceBytes, 0, sourceBitmapBytesLength);
            sourceBitmap.UnlockBits(sourceBitmapData);

            // Copy serchingBitmap to byte array
            var serchingBitmapData =
                searchingBitmap.LockBits(new Rectangle(0, 0, searchingBitmap.Width, searchingBitmap.Height),
                    ImageLockMode.ReadOnly, searchingBitmap.PixelFormat);
            var serchingBitmapBytesLength = serchingBitmapData.Stride * searchingBitmap.Height;
            var serchingBytes = new byte[serchingBitmapBytesLength];
            Marshal.Copy(serchingBitmapData.Scan0, serchingBytes, 0, serchingBitmapBytesLength);
            searchingBitmap.UnlockBits(serchingBitmapData);

            var pointsList = new List<Point>();

            // Serching entries
            // minimazing serching zone
            // sourceBitmap.Height - serchingBitmap.Height + 1
            for (var mainY = 0; mainY < sourceBitmap.Height - searchingBitmap.Height + 1; mainY++)
            {
                var sourceY = mainY * sourceBitmapData.Stride;

                for (var mainX = 0; mainX < sourceBitmap.Width - searchingBitmap.Width + 1; mainX++)
                {// mainY & mainX - pixel coordinates of sourceBitmap
                 // sourceY + sourceX = pointer in array sourceBitmap bytes
                    var sourceX = mainX * pixelFormatSize;

                    var isEqual = true;
                    for (var c = 0; c < pixelFormatSize; c++)
                    {// through the bytes in pixel
                        if (sourceBytes[sourceX + sourceY + c] == serchingBytes[c])
                            continue;
                        isEqual = false;
                        break;
                    }

                    if (!isEqual) continue;

                    var isStop = false;

                    // find fist equalation and now we go deeper) 
                    for (var secY = 0; secY < searchingBitmap.Height; secY++)
                    {
                        var serchY = secY * serchingBitmapData.Stride;

                        var sourceSecY = (mainY + secY) * sourceBitmapData.Stride;

                        for (var secX = 0; secX < searchingBitmap.Width; secX++)
                        {// secX & secY - coordinates of serchingBitmap
                         // serchX + serchY = pointer in array serchingBitmap bytes

                            var serchX = secX * pixelFormatSize;

                            var sourceSecX = (mainX + secX) * pixelFormatSize;

                            for (var c = 0; c < pixelFormatSize; c++)
                            {// through the bytes in pixel
                                if (sourceBytes[sourceSecX + sourceSecY + c] == serchingBytes[serchX + serchY + c]) continue;

                                // not equal - abort iteration
                                isStop = true;
                                break;
                            }

                            if (isStop) break;
                        }

                        if (isStop) break;
                    }

                    if (!isStop)
                    {// serching bitmap is founded!!
                        return new Point(mainX, mainY);
                    }
                }
            }

            return null;
        }
        #endregion

        #region 쿠킹덤 생산 함수
        /// <summary>
        /// Cookierun Kingdom Produce - 생산
        /// </summary>
        /// <param name="id">1~3번째 품목 생산</param>
        private void CKP_Produce(int id)
        {
            CoroutineManager.StartCoroutine(Timer_Produce(id));
        }

        private void CKP_PreviousBuilding()
        {
            CoroutineManager.StartCoroutine(Timer_Previous());
        }

        private void CKP_NextBuilding()
        {
            CoroutineManager.StartCoroutine(Timer_Next());
        }
        #endregion
    }
}
