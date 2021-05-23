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

        #region Class
        #endregion

        Dictionary<string, BitmapListData> bitmapList;
        Bitmap currentScreen;
        #endregion

        public Main()
        {
            InitializeComponent();
            InitializeItem();
            InitializeProduct();

            #region Initialize
            #if DEBUG == false
            tabControl.TabPages.Remove(developTab);
            #endif
            Unlink();
            isRunMacro = false;
            threadClose = false;
            bitmapList = new Dictionary<string, BitmapListData>();

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
                BitmapListData data = new BitmapListData(bmp, testerWidth, testerHeight);
                bitmapList.Add(fileName, data);
            }
            #endregion

            #region 빌딩 리스트 추가
            Settings_Building.Load();
            new Settings_Building(buildingList, "나무꾼의 집#1", Building.list[1], 3);
            new Settings_Building(buildingList, "나무꾼의 집#2", Building.list[1], 3);
            new Settings_Building(buildingList, "젤리빈 농장#1", Building.list[2], 3);
            new Settings_Building(buildingList, "젤리빈 농장#2", Building.list[2], 3);
            new Settings_Building(buildingList, "각설탕 채석장#1", Building.list[3], 3);
            new Settings_Building(buildingList, "각설탕 채석장#2", Building.list[3], 3);
            new Settings_Building(buildingList, "비스킷 풍차#1", Building.list[4], 3);
            new Settings_Building(buildingList, "비스킷 풍차#2", Building.list[4], 3);
            new Settings_Building(buildingList, "젤리베리 과수원#1", Building.list[5], 3);
            new Settings_Building(buildingList, "젤리베리 과수원#2", Building.list[5], 3);
            new Settings_Building(buildingList, "우유 우물#1", Building.list[6], 3);
            new Settings_Building(buildingList, "우유 우물#2", Building.list[6], 3);
            new Settings_Building(buildingList, "솜사탕양 목장#1", Building.list[7], 3);
            new Settings_Building(buildingList, "솜사탕양 목장#2", Building.list[7], 3);
            new Settings_Building(buildingList, "뚝딱 대장간#1", Building.list[11], 7);
            new Settings_Building(buildingList, "뚝딱 대장간#2", Building.list[11], 7);
            new Settings_Building(buildingList, "설탕몽땅 잼가게#1", Building.list[12], 5);
            new Settings_Building(buildingList, "설탕몽땅 잼가게#2", Building.list[12], 5);
            new Settings_Building(buildingList, "롤케이크 공작소#1", Building.list[13], 4);
            new Settings_Building(buildingList, "롤케이크 공작소#2", Building.list[13], 4);
            new Settings_Building(buildingList, "갓 구운 빵집#1", Building.list[14], 6);
            new Settings_Building(buildingList, "갓 구운 빵집#2", Building.list[14], 6);
            new Settings_Building(buildingList, "잼파이 레스토랑#1", Building.list[15], 6);
            new Settings_Building(buildingList, "잼파이 레스토랑#2", Building.list[15], 6);
            new Settings_Building(buildingList, "토닥토닥 도예공방#1", Building.list[16], 4);
            new Settings_Building(buildingList, "토닥토닥 도예공방#2", Building.list[16], 4);
            new Settings_Building(buildingList, "행복한 꽃가게#1", Building.list[17], 6);
            new Settings_Building(buildingList, "행복한 꽃가게#2", Building.list[17], 6);
            new Settings_Building(buildingList, "밀키 우유 가공소", Building.list[18], 3);
            new Settings_Building(buildingList, "카페 라떼", Building.list[19], 3);
            new Settings_Building(buildingList, "러블리 인형공방", Building.list[20], 3);
            new Settings_Building(buildingList, "오크통 쉼터", Building.list[21], 3);
            new Settings_Building(buildingList, "퐁 트 파티세리", Building.list[22], 3);
            new Settings_Building(buildingList, "살롱 드 쥬얼리", Building.list[23], 3);
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

        #region Initialize
        private void InitializeItem()
        {
            new Item("wood", "롤케이크 나무조각");
            new Item("jellybean", "젤리빈");
            new Item("sugar", "각설탕 조각");
            new Item("biscuit", "비스킷 가루");
            new Item("jellyberry", "젤리베리");
            new Item("milk", "밀키우유");
            new Item("cotton", "솜사탕 양털");

            new Item("tool1", "단단 도끼");
            new Item("tool2", "튼튼 곡괭이");
            new Item("tool3", "슥삭슥삭 톱");
            new Item("tool4", "푹푹 삽");
            new Item("tool5", "신비한 프레첼 말뚝");
            new Item("tool6", "영롱한 푸른사탕 집게");
            new Item("tool7", "불변의 슈가 코팅 망치");

            new Item("jam1", "젤리빈 잼");
            new Item("jam2", "스윗젤리 잼");
            new Item("jam3", "달고나 잼");
            new Item("jam4", "석류 잼");
            new Item("jam5", "톡톡베리 잼");

            new Item("rollcake1", "솔방울새 인형");
            new Item("rollcake2", "도토리 램프");
            new Item("rollcake3", "뻐꾹뻐꾹 시계");
            new Item("rollcake4", "백조깃털 드림캐쳐");

            new Item("bread1", "든든한 호밀빵");
            new Item("bread2", "달콤쫀득 잼파이");
            new Item("bread3", "은행 포카치아");
            new Item("bread4", "슈가코팅 도넛");
            new Item("bread5", "폭신 카스테라");
            new Item("bread6", "골드리치 크로와상");

            new Item("restaurant1", "따끈따끈 젤리스튜");
            new Item("restaurant2", "곰젤리 버거");
            new Item("restaurant3", "캔디크림 파스타");
            new Item("restaurant4", "폭신폭신 오므라이스");
            new Item("restaurant5", "콤비네이션 피자젤리");
            new Item("restaurant6", "고급스러운 젤리빈 정식");

            new Item("pot1", "비스킷 화분");
            new Item("pot2", "반짝반짝 유리판");
            new Item("pot3", "반짝이는 색동구슬");
            new Item("pot4", "무지갯빛 디저트 보울");

            new Item("flower1", "캔디꽃");
            new Item("flower2", "행복한 꽃화분");
            new Item("flower3", "캔디꽃다발");
            new Item("flower4", "롤리팝 꽃바구니");
            new Item("flower5", "유리꽃 부케");
            new Item("flower6", "찬란한 요거트 화환");

            new Item("milk1", "크림");
            new Item("milk2", "버터");
            new Item("milk3", "수제 치즈");

            new Item("latte1", "젤리빈 라떼");
            new Item("latte2", "몽글몽글 버블티");
            new Item("latte3", "스윗베리 에이드");

            new Item("cotton1", "구름사탕 쿠션");
            new Item("cotton2", "곰젤리 솜인형");
            new Item("cotton3", "용과 드래곤 솜인형");

            new Item("beer1", "크림 루트비어");
            new Item("beer2", "레드베리 주스");
            new Item("beer3", "빈티지 와일드 보틀");

            new Item("cake1", "으스스 머핀");
            new Item("cake2", "생딸기 케이크");
            new Item("cake3", "파티파티 쉬폰케이크");

            new Item("jewelry1", "글레이즈드 링");
            new Item("jewelry2", "루비베리 브로치");
            new Item("jewelry3", "로얄 곰젤리 크라운");
        }
        private void InitializeProduct()
        {
            #region 나무꾼의 집
            Building building = new Building(1, "나무꾼의 집");

            Product product = new Product(1, "롤케이크 나무조각", 30, 30, 3);
            building.Add(product);
            product = new Product(2, "롤케이크 나무 묶음", 360, 90, 9);
            building.Add(product);
            product = new Product(3, "롤케이크 나무 더미", 5400, 200, 20);
            building.Add(product);
            #endregion

            #region 젤리빈 농장
            building = new Building(2, "젤리빈 농장");

            product = new Product(1, "젤리빈", 60, 50, 3);
            building.Add(product);
            product = new Product(2, "젤리빈 한 바구니", 720, 150, 9);
            building.Add(product);
            product = new Product(3, "젤리빈 한 상자", 6000, 340, 20);
            building.Add(product);
            #endregion

            #region 각설탕 채석장
            building = new Building(3, "각설탕 채석장");

            product = new Product(1, "각설탕 조각", 90, 80, 3);
            building.Add(product);
            product = new Product(2, "각설탕 조각 뭉치", 1020, 240, 9);
            building.Add(product);
            product = new Product(3, "각설탕 조각 한 수레", 9000, 540, 20);
            building.Add(product);
            #endregion

            #region 비스킷 풍차
            building = new Building(4, "비스킷 풍차");

            product = new Product(1, "비스킷 가루", 600, 200, 3);
            building.Add(product);
            product = new Product(2, "비스킷 가루 포대", 1500, 600, 9);
            building.Add(product);
            product = new Product(3, "비스킷 가루 수레", 7200, 1340, 20);
            building.Add(product);
            #endregion

            #region 젤리베리 과수원
            building = new Building(5, "젤리베리 과수원");

            product = new Product(1, "젤리베리", 1080, 500, 3);
            building.Add(product);
            product = new Product(2, "젤리베리 바구니", 2400, 1500, 9);
            building.Add(product);
            product = new Product(3, "젤리베리 한 상자", 9000, 3000, 20);
            building.Add(product);
            #endregion

            #region 우유 우물
            building = new Building(6, "우유 우물");

            product = new Product(1, "밀키 우유", 1680, 700, 2);
            building.Add(product);
            product = new Product(2, "밀키 우유 통", 7200, 1750, 5);
            building.Add(product);
            product = new Product(3, "밀키 우유 한 수레", 12600, 2800, 8);
            building.Add(product);
            #endregion

            #region 솜사탕양 목장
            building = new Building(7, "솜사탕양 목장");

            product = new Product(1, "솜사탕 양털", 5400, 700, 1);
            building.Add(product);
            product = new Product(2, "솜사탕 양털 뭉치", 11100, 1750, 2);
            building.Add(product);
            product = new Product(3, "솜사탕 양털 상자", 17100, 2800, 3);
            building.Add(product);
            #endregion

            #region 뚝딱 대장간
            building = new Building(11, "뚝딱 대장간");

            product = new Product(1, Item.list["tool1"].name, 30);
            product.Add(Item.list["wood"], 2);
            building.Add(product);

            product = new Product(2, Item.list["tool2"].name, 180);
            product.Add(Item.list["wood"], 3);
            product.Add(Item.list["sugar"], 3);
            building.Add(product);

            product = new Product(3, Item.list["tool3"].name, 420);
            product.Add(Item.list["wood"], 6);
            product.Add(Item.list["sugar"], 5);
            building.Add(product);

            product = new Product(4, Item.list["tool4"].name, 900);
            product.Add(Item.list["wood"], 10);
            product.Add(Item.list["sugar"], 10);
            building.Add(product);

            product = new Product(5, Item.list["tool5"].name, 3600);
            product.Add(Item.list["wood"], 15);
            product.Add(Item.list["sugar"], 15);
            building.Add(product);

            product = new Product(6, Item.list["tool6"].name, 10800);
            product.Add(Item.list["wood"], 22);
            product.Add(Item.list["sugar"], 18);
            building.Add(product);

            product = new Product(7, Item.list["tool7"].name, 21600);
            product.Add(Item.list["wood"], 30);
            product.Add(Item.list["sugar"], 35);
            building.Add(product);
            #endregion

            #region 설탕몽땅 잼가게
            building = new Building(12, "설탕몽땅 잼가게");

            product = new Product(1, Item.list["jam1"].name, 90);
            product.Add(Item.list["jellybean"], 3);
            building.Add(product);

            product = new Product(2, Item.list["jam2"].name, 480);
            product.Add(Item.list["jellybean"], 6);
            building.Add(product);

            product = new Product(3, Item.list["jam3"].name, 1200);
            product.Add(Item.list["jellybean"], 6);
            product.Add(Item.list["jellyberry"], 5);
            building.Add(product);

            product = new Product(4, Item.list["jam4"].name, 7200);
            product.Add(Item.list["jellyberry"], 8);
            product.Add(Item.list["latte1"], 1);
            building.Add(product);

            product = new Product(5, Item.list["jam5"].name, 21600);
            product.Add(Item.list["jellybean"], 20);
            product.Add(Item.list["cotton"], 3);
            building.Add(product);
            #endregion

            #region 롤케이크 공작소
            building = new Building(13, "롤케이크 공작소");

            product = new Product(1, Item.list["rollcake1"].name, 300);
            product.Add(Item.list["wood"], 6);
            building.Add(product);

            product = new Product(2, Item.list["rollcake2"].name, 1320);
            product.Add(Item.list["wood"], 12);
            product.Add(Item.list["jellyberry"], 3);
            building.Add(product);

            product = new Product(3, Item.list["rollcake3"].name, 7200);
            product.Add(Item.list["biscuit"], 8);
            product.Add(Item.list["jam3"], 3);
            building.Add(product);

            product = new Product(4, Item.list["rollcake4"].name, 12600);
            product.Add(Item.list["cotton"], 1);
            product.Add(Item.list["restaurant4"], 1);
            building.Add(product);
            #endregion

            #region 갓 구운 빵집
            building = new Building(14, "갓 구운 빵집");

            product = new Product(1, Item.list["bread1"].name, 720);
            product.Add(Item.list["jam1"], 1);
            product.Add(Item.list["biscuit"], 2);
            building.Add(product);

            product = new Product(2, Item.list["bread2"].name, 1200);
            product.Add(Item.list["jellybean"], 6);
            product.Add(Item.list["biscuit"], 3);
            building.Add(product);

            product = new Product(3, Item.list["bread3"].name, 1800);
            product.Add(Item.list["biscuit"], 6);
            product.Add(Item.list["rollcake2"], 1);
            building.Add(product);

            product = new Product(4, Item.list["bread4"].name, 3600);
            product.Add(Item.list["sugar"], 15);
            product.Add(Item.list["pot2"], 1);
            building.Add(product);

            product = new Product(4, Item.list["bread5"].name, 10800);
            product.Add(Item.list["sugar"], 25);
            product.Add(Item.list["milk"], 8);
            building.Add(product);

            product = new Product(4, Item.list["bread6"].name, 21600);
            product.Add(Item.list["milk"], 15);
            product.Add(Item.list["milk2"], 1);
            building.Add(product);
            #endregion

            #region 잼파이 레스토랑
            building = new Building(15, "잼파이 레스토랑");

            product = new Product(1, Item.list["restaurant1"].name, 1080);
            product.Add(Item.list["jellybean"], 4);
            product.Add(Item.list["jellyberry"], 1);
            building.Add(product);

            product = new Product(2, Item.list["restaurant2"].name, 1320);
            product.Add(Item.list["jellybean"], 10);
            product.Add(Item.list["bread2"], 1);
            building.Add(product);

            product = new Product(3, Item.list["restaurant3"].name, 3000);
            product.Add(Item.list["biscuit"], 7);
            product.Add(Item.list["milk1"], 1);
            building.Add(product);

            product = new Product(4, Item.list["restaurant4"].name, 5400);
            product.Add(Item.list["jellyberry"], 6);
            product.Add(Item.list["bread3"], 1);
            building.Add(product);

            product = new Product(4, Item.list["restaurant5"].name, 12600);
            product.Add(Item.list["jellyberry"], 10);
            product.Add(Item.list["flower1"], 4);
            building.Add(product);

            product = new Product(4, Item.list["restaurant6"].name, 25200);
            product.Add(Item.list["jellybean"], 25);
            product.Add(Item.list["flower4"], 4);
            building.Add(product);
            #endregion

            #region 토닥토닥 도예공방
            building = new Building(16, "토닥토닥 도예공방");

            product = new Product(1, Item.list["pot1"].name, 900);
            product.Add(Item.list["rollcake1"], 2);
            product.Add(Item.list["biscuit"], 4);
            building.Add(product);

            product = new Product(2, Item.list["pot2"].name, 1620);
            product.Add(Item.list["sugar"], 12);
            product.Add(Item.list["restaurant1"], 1);
            building.Add(product);

            product = new Product(3, Item.list["pot3"].name, 7200);
            product.Add(Item.list["restaurant2"], 1);
            product.Add(Item.list["cotton"], 1);
            building.Add(product);

            product = new Product(4, Item.list["pot4"].name, 18000);
            product.Add(Item.list["sugar"], 30);
            product.Add(Item.list["bread3"], 1);
            building.Add(product);
            #endregion

            #region 행복한 꽃가게
            building = new Building(17, "행복한 꽃가게");

            product = new Product(1, Item.list["flower1"].name, 1200);
            product.Add(Item.list["jellyberry"], 2);
            product.Add(Item.list["pot1"], 1);
            building.Add(product);

            product = new Product(2, Item.list["flower2"].name, 1800);
            product.Add(Item.list["jellyberry"], 4);
            product.Add(Item.list["sugar"], 10);
            building.Add(product);

            product = new Product(3, Item.list["flower3"].name, 3600);
            product.Add(Item.list["jellyberry"], 5);
            product.Add(Item.list["restaurant3"], 1);
            building.Add(product);

            product = new Product(4, Item.list["flower4"].name, 9000);
            product.Add(Item.list["jellybean"], 18);
            product.Add(Item.list["bread1"], 3);
            building.Add(product);

            product = new Product(4, Item.list["flower5"].name, 16200);
            product.Add(Item.list["jellybean"], 20);
            product.Add(Item.list["cotton1"], 2);
            building.Add(product);

            product = new Product(4, Item.list["flower6"].name, 27000);
            product.Add(Item.list["jellybean"], 30);
            product.Add(Item.list["restaurant5"], 4);
            building.Add(product);
            #endregion

            #region 밀키 우유 가공소
            building = new Building(18, "밀키 우유 가공소");

            product = new Product(1, Item.list["milk1"].name, 1800);
            product.Add(Item.list["jam2"], 1);
            product.Add(Item.list["milk"], 2);
            building.Add(product);

            product = new Product(2, Item.list["milk2"].name, 9000);
            product.Add(Item.list["sugar"], 15);
            product.Add(Item.list["milk"], 5);
            building.Add(product);

            product = new Product(3, Item.list["milk3"].name, 16200);
            product.Add(Item.list["milk"], 10);
            product.Add(Item.list["rollcake3"], 1);
            building.Add(product);
            #endregion

            #region 카페 라떼
            building = new Building(19, "카페 라떼");

            product = new Product(1, Item.list["latte1"].name, 3600);
            product.Add(Item.list["jellybean"], 12);
            product.Add(Item.list["milk"], 2);
            building.Add(product);

            product = new Product(2, Item.list["latte2"].name, 10800);
            product.Add(Item.list["jellyberry"], 8);
            product.Add(Item.list["bread4"], 1);
            building.Add(product);

            product = new Product(3, Item.list["latte3"].name, 23400);
            product.Add(Item.list["jellyberry"], 12);
            product.Add(Item.list["pot3"], 2);
            building.Add(product);
            #endregion

            #region 러블리 인형공방
            building = new Building(20, "러블리 인형공방");

            product = new Product(1, Item.list["cotton1"].name, 5400);
            product.Add(Item.list["biscuit"], 8);
            product.Add(Item.list["cotton"], 1);
            building.Add(product);

            product = new Product(2, Item.list["cotton2"].name, 14400);
            product.Add(Item.list["flower3"], 2);
            product.Add(Item.list["cotton"], 1);
            building.Add(product);

            product = new Product(3, Item.list["cotton3"].name, 23400);
            product.Add(Item.list["cotton"], 4);
            product.Add(Item.list["beer1"], 2);
            building.Add(product);
            #endregion

            #region 오크통 쉼터
            building = new Building(21, "오크통 쉼터");

            product = new Product(1, Item.list["beer1"].name, 9000);
            product.Add(Item.list["biscuit"], 10);
            product.Add(Item.list["flower2"], 1);
            building.Add(product);

            product = new Product(2, Item.list["beer2"].name, 23400);
            product.Add(Item.list["jellyberry"], 14);
            product.Add(Item.list["cotton2"], 2);
            building.Add(product);

            product = new Product(3, Item.list["beer3"].name, 28800);
            product.Add(Item.list["wood"], 50);
            product.Add(Item.list["cake1"], 4);
            building.Add(product);
            #endregion

            #region 퐁 드 파티세리
            building = new Building(22, "퐁 드 파티세리");

            product = new Product(1, Item.list["cake1"].name, 12600);
            product.Add(Item.list["biscuit"], 12);
            product.Add(Item.list["milk"], 7);
            building.Add(product);

            product = new Product(2, Item.list["cake2"].name, 21600);
            product.Add(Item.list["biscuit"], 14);
            product.Add(Item.list["latte2"], 2);
            building.Add(product);

            product = new Product(3, Item.list["cake3"].name, 28800);
            product.Add(Item.list["biscuit"], 18);
            product.Add(Item.list["milk3"], 3);
            building.Add(product);
            #endregion

            #region 살롱 드 쥬얼리
            building = new Building(23, "살롱 드 쥬얼리");

            product = new Product(1, Item.list["jewelry1"].name, 18000);
            product.Add(Item.list["biscuit"], 12);
            product.Add(Item.list["bread6"], 1);
            building.Add(product);

            product = new Product(2, Item.list["jewelry2"].name, 27000);
            product.Add(Item.list["jellyberry"], 20);
            product.Add(Item.list["rollcake4"], 3);
            building.Add(product);

            product = new Product(3, Item.list["jewelry3"].name, 28800);
            product.Add(Item.list["cotton"], 4);
            product.Add(Item.list["flower5"], 2);
            building.Add(product);
            #endregion
        }
        #endregion

        #region 타이머(코루틴)
        IEnumerator Timer_Start()
        {
            while(isRunMacro)
            {
                GetCurrentScreen();

                // 메인화면인지 체크
                Point? point = Find(currentScreen, "Icon_KingdomPass", 0.1);
                if (point == null)
                {
                    // 메인화면 갈 때까지 ESC 반복
                    PressKey(Keys.Escape);
                    yield return new WaitForSeconds(1);
                    continue;
                }
                // 메인화면 도착
                point = Find(currentScreen, "Icon_Store", 0.3);
                if (point == null)
                {
                    // 버그 발생 "Can't Find Store"
                    yield return new WaitForSeconds(1f);
                    continue;
                }
                TouchScreen(point.Value.X, point.Value.Y);
                yield return new WaitForSeconds(1f);

                Random random = new Random();
                float adjustWidth = (float)linkedWidth / testerWidth;
                float adjustHeight = (float)linkedHeight / testerHeight;


                // 도끼 찾을 때까지 스크롤 내리기
                GetCurrentScreen();
                point = Find(currentScreen, "Store_Axe", 0.5);
                while (point == null)
                {
                    int randomHeight = random.Next(100, 550);
                    int randomWidth = random.Next(200, 1000);
                    Point from = new Point((int)Math.Round(adjustWidth * randomWidth), (int)Math.Round(adjustHeight * randomHeight));
                    Point to = new Point((int)Math.Round(adjustWidth * randomWidth), (int)Math.Round(adjustHeight * (randomHeight - 300)));

                    yield return Timer_Scroll(from.X, from.Y, to.X, to.Y, 0.5f);
                    yield return new WaitForSeconds(1f);
                    GetCurrentScreen();
                    point = Find(currentScreen, "Store_Axe", 0.4);
                }

                // 도끼를 찾았으면 클릭
                TouchScreen(point.Value.X, point.Value.Y);
                yield return new WaitForSeconds(1f);


                // 나무가 있으면 올바른 클릭
                GetCurrentScreen();
                point = Find(currentScreen, "Info_Wood", 0.3);
                while (point == null)
                {
                    // 없으면 뒤로가기
                    PressKey(Keys.Escape);
                    yield return new WaitForSeconds(1);
                    continue;
                }
                TouchScreen(point.Value.X, point.Value.Y);
                yield return new WaitForSeconds(1f);

                // 나무가 있으면 생산으로 이동
                GetCurrentScreen();
                point = Find(currentScreen, "Icon_Goto", 0.3);
                while (point == null)
                {
                    PressKey(Keys.Escape);
                    yield return new WaitForSeconds(1);
                    continue;
                }
                TouchScreen(point.Value.X, point.Value.Y);
                yield return new WaitForSeconds(2f);
                
                GetCurrentScreen();
                point = Find(currentScreen, "Building_Wood", 0.3);
                if (point == null) // 생산 건물에 못 들어간 경우
                {
                    TouchScreen(currentScreen.Width / 2, currentScreen.Height / 2);
                }
                yield return new WaitForSeconds(1f);
                yield return Timer_Previous();
                yield return new WaitForSeconds(1f);

                // 순환하며 재고 파악
                var buildings = Settings_Building.buildings;
                for (int i = 0; i < buildingList.Items.Count; i++)
                {
                    var building = buildings[buildingList.Items[i].ToString()];
                    if (building.State != 0)
                        continue;

                    // 재고 파악
                }

                for (int i = 0; i < buildingList.Items.Count; i++)
                {
                    var building = buildings[buildingList.Items[i].ToString()];
                    if (building.State != 0)
                        continue;

                    if (building.products.Count == 0)
                    {
                        yield return Timer_Next();
                        yield return new WaitForSeconds(1f);
                        continue;
                    }

                    yield return Timer_Produce(building.products[0].ID);
                    yield return new WaitForSeconds(1f);
                    yield return Timer_Next();
                    yield return new WaitForSeconds(1f);
                }

                PressKey(Keys.Escape);
                yield return new WaitForSeconds(1);

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
                CoroutineManager.StopAllCoroutines();
            }
        }

        private void OnBuildingListSelectionChanged(object sender, EventArgs e)
        {
            string item = buildingList.SelectedItem?.ToString();
            if (item == null)
                return;

            var building = Settings_Building.buildings[item];
            if (building == null)
                return;

            buildingState.SelectedIndex = building.State;
            buildingLevel.Items.Clear();
            for (int i = 1; i <= building.MaxLevel; i++)
            {
                buildingLevel.Items.Add(i.ToString());
            }
            #region 생산품 로드
            buildingProducts.Rows.Clear();
            for (int i = 0; i < building.products.Count; i++)
            {
                var product = building.products[i];
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                for (int j = 0; j < building.building.products.Count; j++)
                {
                    cell.Items.Add(building.building.products[j].name);
                }
                cell.Value = cell.Items[product.ID];

                int row = buildingProducts.Rows.Add(null, product.Min, product.Max);
                buildingProducts.Rows[row].Cells[0] = cell;
            }
            #endregion

            buildingLevel.SelectedIndex = building.Level;
        }

        private void OnAddProductButtonClicked(object sender, EventArgs e)
        {
            string item = buildingList.SelectedItem?.ToString();
            if (item == null)
                return;

            var building = Settings_Building.buildings[item];
            if (building == null)
                return;

            DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
            cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

            for (int i = 0; i < building.building.products.Count; i++)
            {
                cell.Items.Add(building.building.products[i].name);
            }
            cell.Value = cell.Items[0];

            int row = buildingProducts.Rows.Add(null, 20, 50);
            buildingProducts.Rows[row].Cells[0] = cell;

            var product = new Settings_Product();
            product.ID = 0;
            product.Min = 20;
            product.Max = 50;
            building.products.Add(product);
            Settings_Building.Save();
        }

        private void OnRemoveProductButtonClicked(object sender, EventArgs e)
        {
            string item = buildingList.SelectedItem?.ToString();
            if (item == null)
                return;

            var building = Settings_Building.buildings[item];
            if (building == null)
                return;

            var cell = buildingProducts.CurrentCell;
            if (cell == null)
                return;
            
            for (int i = 0; i < building.products.Count; i++)
            {
                if((int)cell.OwningRow.Cells[0].Value == building.products[i].ID)
                {
                    building.products.Remove(building.products[i]);
                }
            }
            buildingProducts.Rows.Remove(cell.OwningRow);
            Settings_Building.Save();
        }

        private void OnTestButtonClicked(object sender, EventArgs e)
        {
            CKP_Produce(5);
            //CKP_Scroll();
        }

        private void OnBuildingProductsCellEditEnded(object sender, DataGridViewCellEventArgs e)
        {
            string item = buildingList.SelectedItem?.ToString();
            if (item == null)
                return;

            var building = Settings_Building.buildings[item];
            if (building == null)
                return;

            var cell = buildingProducts.CurrentCell;
            if (cell == null)
                return;

            switch (e.ColumnIndex)
            {
                case 0:
                    var combo = (DataGridViewComboBoxCell)(buildingProducts.Rows[e.RowIndex].Cells[0]);
                    building.products[e.RowIndex].ID = combo.Items.IndexOf(combo.Value);
                    break;
                case 1:
                    building.products[e.RowIndex].Min = int.Parse(buildingProducts.Rows[e.RowIndex].Cells[1].Value.ToString());
                    break;
                case 2:
                    building.products[e.RowIndex].Max = int.Parse(buildingProducts.Rows[e.RowIndex].Cells[2].Value.ToString());
                    break;
                default:
                    break;
            }
            
            Settings_Building.Save();
        }

        private void OnBuildingProductsDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void OnBuildingStateSelectionChanged(object sender, EventArgs e)
        {
            string item = buildingList.SelectedItem?.ToString();
            if (item == null)
                return;

            var building = Settings_Building.buildings[item];
            if (building == null)
                return;

            building.State = buildingState.SelectedIndex;
            Settings_Building.Save();
        }

        private void OnBuildingLevelSelectionChanged(object sender, EventArgs e)
        {
            string item = buildingList.SelectedItem?.ToString();
            if (item == null)
                return;

            var building = Settings_Building.buildings[item];
            if (building == null)
                return;

            building.Level = buildingLevel.SelectedIndex;
            Settings_Building.Save();
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

        public void GetCurrentScreen()
        {
            if (currentScreen != null)
                currentScreen.Dispose();

            currentScreen = PrintScreen();
            RefreshSize(currentScreen.Width, currentScreen.Height);
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

        private Point? Find(Bitmap sourceBitmap, string keyword, double tolerance = 0.5)
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

            BitmapData smallData = searchingBitmap.LockBits(new Rectangle(0, 0, searchingBitmap.Width, searchingBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bigData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = sourceBitmap.Width;
            int bigHeight = sourceBitmap.Height - searchingBitmap.Height + 1;
            int smallWidth = searchingBitmap.Width * 3;
            int smallHeight = searchingBitmap.Height;
            
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - searchingBitmap.Width * 3;
                int bigOffset = bigStride - sourceBitmap.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            sourceBitmap.UnlockBits(bigData);
                            searchingBitmap.UnlockBits(smallData);
                            return new Point(x, y);
                        }
                        //If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }

                    if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            sourceBitmap.UnlockBits(bigData);
            searchingBitmap.UnlockBits(smallData);

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
