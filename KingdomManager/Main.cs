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
        #endregion


        #region const & readonly Variables
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

        public bool threadClose;
        #endregion

        public Main()
        {
            InitializeComponent();

            #region Initialize
            #if DEBUG == false
            tabControl.TabPages.Remove(developTab);
            #endif
            Unlink();
            threadClose = false;

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
        }

        #region 타이머(코루틴)
        public void Run()
        {
            while(threadClose == false)
            {
                if(CoroutineManager.Runnings() > 0)
                {
                    CoroutineManager.Update();
                }
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
        #endregion

        #region 이벤트 Listner
        private void OnLinkButtonClicked(object sender, EventArgs e)
        {
            if (isLinked)
                Unlink();
            else
            {
                if (Link() == false)
                    return;
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

        public void RefreshSize()
        {
            Graphics graphics = Graphics.FromHwnd(linkedHandle);
            if (graphics == null)
                return;

            linkedWidth = (int)Math.Round(graphics.VisibleClipBounds.Width);
            linkedHeight = (int)Math.Round(graphics.VisibleClipBounds.Height);
        }

        private IntPtr CreateLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
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
        #endregion
    }
}
