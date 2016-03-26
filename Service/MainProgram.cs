using System;
using Grapevine.Server;
using System.Threading;
using System.Net;
using WindowsInput;
using WindowsInput.Native;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace RESTInput
{
    //s.Keyboard.KeyPress(VirtualKeyCode.F6);

    public sealed class HandleInput : RESTResource
    {
        private InputSimulator mIS = new InputSimulator();
        private string[] mappingTable = new string[256];
        private bool mappingDone = false;

        public void generateMapping()
        {
            if (mappingDone)
                return;
            mappingTable[1] = "LBUTTON";
            mappingTable[2] = "RBUTTON";
            mappingTable[3] = "CANCEL";
            mappingTable[4] = "MBUTTON";
            mappingTable[5] = "XBUTTON1";
            mappingTable[6] = "XBUTTON2";
            mappingTable[8] = "BACK";
            mappingTable[9] = "TAB";
            mappingTable[12] = "CLEAR";
            mappingTable[13] = "RETURN";
            mappingTable[16] = "SHIFT";
            mappingTable[17] = "CONTROL";
            mappingTable[18] = "MENU";
            mappingTable[19] = "PAUSE";
            mappingTable[20] = "CAPITAL";
            mappingTable[21] = "KANA";
            mappingTable[21] = "HANGEUL";
            mappingTable[21] = "HANGUL";
            mappingTable[23] = "JUNJA";
            mappingTable[24] = "FINAL";
            mappingTable[25] = "HANJA";
            mappingTable[25] = "KANJI";
            mappingTable[27] = "ESCAPE";
            mappingTable[28] = "CONVERT";
            mappingTable[29] = "NONCONVERT";
            mappingTable[30] = "ACCEPT";
            mappingTable[31] = "MODECHANGE";
            mappingTable[32] = "SPACE";
            mappingTable[33] = "PRIOR";
            mappingTable[34] = "NEXT";
            mappingTable[35] = "END";
            mappingTable[36] = "HOME";
            mappingTable[37] = "LEFT";
            mappingTable[38] = "UP";
            mappingTable[39] = "RIGHT";
            mappingTable[40] = "DOWN";
            mappingTable[41] = "SELECT";
            mappingTable[42] = "PRINT";
            mappingTable[43] = "EXECUTE";
            mappingTable[44] = "SNAPSHOT";
            mappingTable[45] = "INSERT";
            mappingTable[46] = "DELETE";
            mappingTable[47] = "HELP";
            mappingTable[48] = "VK_0";
            mappingTable[49] = "VK_1";
            mappingTable[50] = "VK_2";
            mappingTable[51] = "VK_3";
            mappingTable[52] = "VK_4";
            mappingTable[53] = "VK_5";
            mappingTable[54] = "VK_6";
            mappingTable[55] = "VK_7";
            mappingTable[56] = "VK_8";
            mappingTable[57] = "VK_9";
            mappingTable[65] = "VK_A";
            mappingTable[66] = "VK_B";
            mappingTable[67] = "VK_C";
            mappingTable[68] = "VK_D";
            mappingTable[69] = "VK_E";
            mappingTable[70] = "VK_F";
            mappingTable[71] = "VK_G";
            mappingTable[72] = "VK_H";
            mappingTable[73] = "VK_I";
            mappingTable[74] = "VK_J";
            mappingTable[75] = "VK_K";
            mappingTable[76] = "VK_L";
            mappingTable[77] = "VK_M";
            mappingTable[78] = "VK_N";
            mappingTable[79] = "VK_O";
            mappingTable[80] = "VK_P";
            mappingTable[81] = "VK_Q";
            mappingTable[82] = "VK_R";
            mappingTable[83] = "VK_S";
            mappingTable[84] = "VK_T";
            mappingTable[85] = "VK_U";
            mappingTable[86] = "VK_V";
            mappingTable[87] = "VK_W";
            mappingTable[88] = "VK_X";
            mappingTable[89] = "VK_Y";
            mappingTable[90] = "VK_Z";
            mappingTable[91] = "LWIN";
            mappingTable[92] = "RWIN";
            mappingTable[93] = "APPS";
            mappingTable[95] = "SLEEP";
            mappingTable[96] = "NUMPAD0";
            mappingTable[97] = "NUMPAD1";
            mappingTable[98] = "NUMPAD2";
            mappingTable[99] = "NUMPAD3";
            mappingTable[100] = "NUMPAD4";
            mappingTable[101] = "NUMPAD5";
            mappingTable[102] = "NUMPAD6";
            mappingTable[103] = "NUMPAD7";
            mappingTable[104] = "NUMPAD8";
            mappingTable[105] = "NUMPAD9";
            mappingTable[106] = "MULTIPLY";
            mappingTable[107] = "ADD";
            mappingTable[108] = "SEPARATOR";
            mappingTable[109] = "SUBTRACT";
            mappingTable[110] = "DECIMAL";
            mappingTable[111] = "DIVIDE";
            mappingTable[112] = "F1";
            mappingTable[113] = "F2";
            mappingTable[114] = "F3";
            mappingTable[115] = "F4";
            mappingTable[116] = "F5";
            mappingTable[117] = "F6";
            mappingTable[118] = "F7";
            mappingTable[119] = "F8";
            mappingTable[120] = "F9";
            mappingTable[121] = "F10";
            mappingTable[122] = "F11";
            mappingTable[123] = "F12";
            mappingTable[124] = "F13";
            mappingTable[125] = "F14";
            mappingTable[126] = "F15";
            mappingTable[127] = "F16";
            mappingTable[128] = "F17";
            mappingTable[129] = "F18";
            mappingTable[130] = "F19";
            mappingTable[131] = "F20";
            mappingTable[132] = "F21";
            mappingTable[133] = "F22";
            mappingTable[134] = "F23";
            mappingTable[135] = "F24";
            mappingTable[144] = "NUMLOCK";
            mappingTable[145] = "SCROLL";
            mappingTable[160] = "LSHIFT";
            mappingTable[161] = "RSHIFT";
            mappingTable[162] = "LCONTROL";
            mappingTable[163] = "RCONTROL";
            mappingTable[164] = "LMENU";
            mappingTable[165] = "RMENU";
            mappingTable[166] = "BROWSER_BACK";
            mappingTable[167] = "BROWSER_FORWARD";
            mappingTable[168] = "BROWSER_REFRESH";
            mappingTable[169] = "BROWSER_STOP";
            mappingTable[170] = "BROWSER_SEARCH";
            mappingTable[171] = "BROWSER_FAVORITES";
            mappingTable[172] = "BROWSER_HOME";
            mappingTable[173] = "VOLUME_MUTE";
            mappingTable[174] = "VOLUME_DOWN";
            mappingTable[175] = "VOLUME_UP";
            mappingTable[176] = "MEDIA_NEXT_TRACK";
            mappingTable[177] = "MEDIA_PREV_TRACK";
            mappingTable[178] = "MEDIA_STOP";
            mappingTable[179] = "MEDIA_PLAY_PAUSE";
            mappingTable[180] = "LAUNCH_MAIL";
            mappingTable[181] = "LAUNCH_MEDIA_SELECT";
            mappingTable[182] = "LAUNCH_APP1";
            mappingTable[183] = "LAUNCH_APP2";
            mappingTable[186] = "OEM_1";
            mappingTable[187] = "OEM_PLUS";
            mappingTable[188] = "OEM_COMMA";
            mappingTable[189] = "OEM_MINUS";
            mappingTable[190] = "OEM_PERIOD";
            mappingTable[191] = "OEM_2";
            mappingTable[192] = "OEM_3";
            mappingTable[219] = "OEM_4";
            mappingTable[220] = "OEM_5";
            mappingTable[221] = "OEM_6";
            mappingTable[222] = "OEM_7";
            mappingTable[223] = "OEM_8";
            mappingTable[226] = "OEM_102";
            mappingTable[229] = "PROCESSKEY";
            mappingTable[231] = "PACKET";
            mappingTable[246] = "ATTN";
            mappingTable[247] = "CRSEL";
            mappingTable[248] = "EXSEL";
            mappingTable[249] = "EREOF";
            mappingTable[250] = "PLAY";
            mappingTable[251] = "ZOOM";
            mappingTable[252] = "NONAME";
            mappingTable[253] = "PA1";
            mappingTable[254] = "OEM_CLEAR";
            mappingDone = true;
        }

        [RESTRoute(Method = Grapevine.HttpMethod.GET, PathInfo = @"/actions")]
        public void getActions(HttpListenerContext ctx)
        {
            generateMapping();
            JArray items = new JArray();

            for (int i = 0; i != mappingTable.Length; ++i)
            {
                if (mappingTable[i] != null && mappingTable[i].Length > 0)
                    items.Add(mappingTable[i]);
            }
            ctx.Response.ContentType = "application/json";
            this.SendTextResponse(ctx, items.ToString());
        }

        [RESTRoute(Method = Grapevine.HttpMethod.POST, PathInfo = @"/interact")]
        public void performActions(HttpListenerContext ctx)
        {
            generateMapping();
            JObject json = null;
            try
            {
                string data = this.GetPayload(ctx.Request);
                if (!object.ReferenceEquals(data, null))
                {
                    json = JObject.Parse(data);
                }
            }
            catch (Exception e)
            {
                this.InternalServerError(ctx, e);
                return;
            }
            if ((JArray)json["action"] != null)
            {
                foreach (string item in json["action"].Children())
                {
                    if (item != null && item != "")
                    {
                        bool halfduplex = false;
                        bool release = false;
                        string key = item.ToUpper();
                        VirtualKeyCode keycode = 0;
                        if (key[0] == '-' || key[0] == '+')
                        {
                            release = key[0] == '-';
                            halfduplex = true;
                            key = key.Substring(1);
                        }

                        for (int i = 0; i != mappingTable.Length; ++i)
                        {
                            if (key.Equals(mappingTable[i]))
                            {
                                keycode = (VirtualKeyCode)i;
                                break;
                            }
                        }

                        if (keycode != 0) {
                            if (halfduplex)
                            {
                                if (release)
                                    mIS.Keyboard.KeyUp(keycode);
                                else
                                    mIS.Keyboard.KeyDown(keycode);
                            }
                            else
                                mIS.Keyboard.KeyPress(keycode);
                        }
                    }
                }

            }
            else if ((string)json["text"] != null && ((string)json["text"]).Length > 0)
            {
                mIS.Keyboard.TextEntry((string)json["text"]);
            }
            this.SendTextResponse(ctx, "OK");
        }
    }

    static class Program
    {

        static public void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TrayIconManager());
        }

    }

    class TrayIconManager : ApplicationContext
    {
        private static NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        private RESTServer server = new RESTServer();

        public TrayIconManager()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            TrayIcon.Visible = true;
            server.Port = "5000";
            server.Host = "+";
            server.Start();
        }

        public void InitializeComponent()
        {
            TrayIcon = new NotifyIcon();

            TrayIcon.Text = "REST Input Service";


            //The icon is added to the project resources.
            //Here I assume that the name of the file is 'TrayIcon.ico'
            TrayIcon.Icon = Resources.TrayIcon;

            //Optional - Add a context menu to the TrayIcon:
            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();

            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {
                this.CloseMenuItem});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new Size(153, 70);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new Size(152, 22);
            this.CloseMenuItem.Text = "Exit the REST Input server";
            this.CloseMenuItem.Click += new EventHandler(this.CloseMenuItem_Click);

            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            TrayIcon.Visible = false;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            //Here you can do stuff if the tray icon is doubleclicked
            TrayIcon.ShowBalloonTip(10000);
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit?",
                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                server.Stop();
                Application.Exit();
            }
        }

    }
}
