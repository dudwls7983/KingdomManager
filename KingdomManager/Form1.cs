using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KingdomManager
{
    public partial class Form1 : Form
    {
        // 프로그램의 최상위 핸들을 찾아주는 함수
        [DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // 프로그램의 자식 핸들을 찾아주는 함수
        [DllImport("user32")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd1, int hWnd2, string lp1, string lp2);

        // 핸들로부터 이미지를 출력하는 함수
        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcblt, int nFlags);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string windowName = textBox1.Text;

            IntPtr parentHandle = FindWindow(null, windowName);
            if (parentHandle == null)
                return;

            IntPtr childHandle = FindWindowEx(parentHandle, 0, "RenderWindow", "TheRender");
            if (childHandle == null)
                return;

            Graphics graphics = Graphics.FromHwnd(childHandle);
            if (graphics == null)
                return;

            Rectangle rect = Rectangle.Round(graphics.VisibleClipBounds);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdc = g.GetHdc();
                PrintWindow(childHandle, hdc, 0x2);
                g.ReleaseHdc(hdc);
            }

            pictureBox1.Image = bmp;
        }
    }
}
