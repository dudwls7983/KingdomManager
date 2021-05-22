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
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        #endregion


        #region const & readonly Variables
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;

        readonly string[,] programList = { //{"ComboBox에 보여줄 이름", "프로세스 이름", "윈도우핸들이름", "윈도우핸들클래스" }, 
            {"LD플레이어", "dnplayer", "RenderWindow", "TheRender" }
        };
        #endregion

        #region Local Variables
        public bool isLinked;
        public IntPtr linkedHandle;
        public int linkedWidth;
        public int linkedHeight;
        #endregion

        public Main()
        {
            InitializeComponent();

            #region Initialize
            #if DEBUG == false
            tabControl.TabPages.Remove(developTab);
            #endif
            Unlink();

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
            while(true)
            {
                if(CoroutineManager.Runnings() > 0)
                {
                    CoroutineManager.Update();
                }
            }
        }

        IEnumerator Timer_TouchEnd(float seconds, IntPtr param)
        {
            yield return new WaitForSeconds(seconds);
            TouchEnd(param);
            yield return null;
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

        private void OnTestButtonClicked(object sender, EventArgs e)
        {
            CKP_Produce(2);
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

            IntPtr param = new IntPtr(x | (y << 16));
            SendMessage(linkedHandle, WM_LBUTTONDOWN, 1, param);
            if(duration == 0f)
            {
                SendMessage(linkedHandle, WM_LBUTTONUP, 0, param);
            }
            else
            {
                CoroutineManager.StartCoroutine(Timer_TouchEnd(duration, param));
            }
        }

        public void TouchEnd(IntPtr param)
        {
            if (IsLinkValid() == false)
                return;
            
            SendMessage(linkedHandle, WM_LBUTTONUP, 1, param);
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
        #endregion

        #region 쿠킹덤 생산 함수
        /// <summary>
        /// Cookierun Kingdom Produce - 생산
        /// </summary>
        /// <param name="id">1~3번째 품목 생산</param>
        private void CKP_Produce(int id)
        {
            const int testerWidth = 1280;
            const int testerHeight = 720;
            float adjustWidth = (float)linkedWidth / testerWidth;
            float adjustHeight = (float)linkedHeight / testerHeight;

            Point min1 = new Point(949, 221);
            Point max1 = new Point(1211, 253);
            Point min2 = new Point(946, 430);
            Point max2 = new Point(1210, 467);
            Point min3 = new Point(950, 648);
            Point max3 = new Point(1210, 681);

            Random random = new Random();

            switch (id)
            {
                case 1:
                    break;
                case 2:
                    int x = (int)Math.Round(adjustWidth * random.Next(min2.X, max2.X));
                    int y = (int)Math.Round(adjustHeight * random.Next(min2.Y, max2.Y));
                    TouchScreen(x, y, 5);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        private void CKP_Scroll()
        {

        }
        #endregion
    }
}
