using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using APP.Utility;
using APP.Utility.Extension;
using APP.Utility.HttpClientUtils;

namespace APP
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 启动参数
        /// </summary>
        private static string[] init_param = default(string[]);
        private const string url = "https://www.ithome.com/rss/";
        private const string _title = "更新时间：{0}";
        private static string str_response_xml = string.Empty;
        private static string pubDate = string.Empty;
        private Timer timmer = new Timer() { Interval = 1000 * 30, Enabled = true };
        private delegate void SetDataHandler(string str_Text, IEnumerator enum_dgv);
        private SetDataHandler handler = null;

        public MainForm(string[] arr_param)
        {
            init_param = arr_param ?? new string[0] { };
            InitializeComponent();
            handler = new SetDataHandler(SetData);
            timmer.Tick += Timmer_Tick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show(false);
            GetRSS();
        }

        private void Timmer_Tick(object sender, EventArgs e)
        {
            GetRSS();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Show(false);
            }
        }

        private void exiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnregisterHotKey(Handle, 201);
            Application.Exit();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            btn_refresh.Enabled = false;
            GetRSS();
        }

        private void GetRSS()
        {
            Task.Run(() => {
                var _clientContext = new ClientContext(new CommonClient());
                try
                {
                    var _str_response_xml = _clientContext.Post(url, null);
                    if (_str_response_xml.IsNullOrWhiteSpace())
                    {
                        MessageBox.Show("没有数据");
                        return;
                    }

                    if (str_response_xml.Length == _str_response_xml.Length
                        || str_response_xml.Equals(_str_response_xml, StringComparison.OrdinalIgnoreCase))
                    {
                        pubDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

                        if (this.InvokeRequired)
                        {
                            this.Invoke(handler, string.Format(_title, pubDate), null);
                        }
                        else
                        {
                            this.SetData(string.Format(_title, pubDate), null);
                        }
                        return;
                    }

                    str_response_xml = _str_response_xml;
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(str_response_xml);
                    pubDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Globals.ConvertStringToDateTime(xmlDoc.SelectNodes("rss/channel/pubDate")[0].InnerText, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    IEnumerator ienum = xmlDoc.SelectNodes("rss/channel/item").GetEnumerator();

                    if (this.InvokeRequired)
                    {
                        this.Invoke(handler, string.Format(_title, pubDate), ienum);
                    }
                    else
                    {
                        this.SetData(string.Format(_title, pubDate), ienum);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            });
        }

        private void SetData(string str_Text, IEnumerator enum_dgv)
        {
            this.Text = str_Text;
            this.notify.Text = this.Text;

            if (enum_dgv != null)
            {
                this.dgv_list.Rows.Clear();
                while (enum_dgv.MoveNext())
                {
                    var item = enum_dgv.Current as XmlNode;
                    var title = item.SelectSingleNode("title").InnerText;
                    var link = item.SelectSingleNode("link").InnerText;
                    var newsID = string.Empty;
                    if (!link.IsNullOrWhiteSpace())
                    {
                        var arr = link.GetArray("/");
                        if (arr.Length > 4)
                            newsID = "{0}{1}{2}".ToFormat(arr[arr.Length - 3], arr[arr.Length - 2], arr[arr.Length - 1]).TrimAny(".htm", ".html").TrimStart('0');

                        if (!newsID.IsNullOrWhiteSpace() && init_param.Contains("/m", StringComparison.OrdinalIgnoreCase))
                            link = "https://m.ithome.com/html/{0}.htm".ToFormat(newsID);
                    }

                    //var description = item.SelectSingleNode("description").InnerText;
                    var pubDate_sub = string.Format("{0:yy-MM-dd HH:mm:ss}", Globals.ConvertStringToDateTime(item.SelectSingleNode("pubDate").InnerText, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    this.dgv_list.Rows.Add(new object[] { title, pubDate_sub, /*description,*/ link, newsID.ValueOrEmpty() });
                }
            }

            btn_refresh.Enabled = true;
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var row = this.dgv_list[e.ColumnIndex, e.RowIndex];
                var link = (string)row.Value ?? "https://www.ithome.com/";
                System.Diagnostics.Process.Start(link);
                Clipboard.SetText(link);
            }
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var row = this.dgv_list["Link", e.RowIndex];
                //var link = "https://www.ithome.com/rss";
                var link = (string)row.Value ?? "https://www.ithome.com/";
                System.Diagnostics.Process.Start(link);
                Clipboard.SetText(link);
            }
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            Show(false);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show(true);
        }

        private void Show(bool show = false)
        {
            if (show)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }

            RegKey(Handle, 201, KeyModifiers.Ctrl, Keys.Tab);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                switch (m.WParam.ToInt32())
                {
                    case 201:
                        {
                            if (this.WindowState == FormWindowState.Minimized)
                                Show(true);
                            else
                                Show(false);
                        }
                        break;
                }
                break;
            }
            base.WndProc(ref m);
        }



        #region 全局热键

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）          
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        public static void RegKey(IntPtr hwnd, int hotKey_id, KeyModifiers keyModifiers, Keys key)
        {
            try
            {
                if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
                {
                    if (Marshal.GetLastWin32Error() == 1409)
                    { MessageBox.Show("热键被占用 ！"); }
                    else
                    { MessageBox.Show("注册热键失败！"); }
                }
            }
            catch (Exception) { }
        }


        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        public static void UnRegKey(IntPtr hwnd, int hotKey_id)
        {
            //注销Id号为hotKey_id的热键设定
            UnregisterHotKey(hwnd, hotKey_id);
        }

        #endregion 全局热键

    }
}
