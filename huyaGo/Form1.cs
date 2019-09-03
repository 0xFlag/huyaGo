using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace huyaGo
{
    public partial class Form1 : Form
    {
        private string len4;
        private string html;
        private string len7;
        public Form1()
        {
            InitializeComponent();
            combox();
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            this.textBox2.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == "")
            {
                MessageBox.Show("请输入直播地址");
            }
            else
            {
                this.textBox3.Text = null;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string url = this.textBox2.Text;
                WebRequest oRequest = WebRequest.Create(url);
                WebResponse oResponse = oRequest.GetResponse();
                StreamReader oReader = new StreamReader(oResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                html = oReader.ReadToEnd();
                //this.textBox3.AppendText(html);
                //var TT_ROOM_DATA = {}
                //@"var\s+TT_ROOM_DAT\s+=\s+{(?<text>[^<]*)}"
                foreach (Match m in Regex.Matches(html, @"<script\s+nonce=""(.*)""\s+data-fixed=""true"">(?<text>[^<]*)</script>"))
                {
                    var a = m.Groups["text"].ToString();
                    var len1 = a.IndexOf("gameLiveInfo");
                    var len2 = a.IndexOf("," + "\"" + "gameStreamInfoList");
                    var len3 = a.Substring(len1 + 1, len2 - len1 - 1);
                    len4 = len3.Replace("ameLiveInfo" + "\"" + ":", "");

                    JsonSerializer serializer = new JsonSerializer();
                    TextReader tr = new StringReader(len4);
                    JsonTextReader jtr = new JsonTextReader(tr);
                    object obj = serializer.Deserialize(jtr);
                    if (obj != null)
                    {
                        StringWriter textWriter = new StringWriter();
                        JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                        {
                            Formatting = Formatting.Indented,
                            Indentation = 4,
                            IndentChar = ' '
                        };
                        serializer.Serialize(jsonWriter, obj);
                        this.textBox3.AppendText(textWriter.ToString());
                    }
                    else
                    {
                        this.textBox3.AppendText(len4.ToString());
                    }
                    var len5 = a.IndexOf("sStreamName");
                    var len6 = a.IndexOf("sFlvUrl");
                    len7 = a.Substring(len5 + 14, len6 - len5 - 17);
                }
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("显示失败");
            }
            else
            {
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    this.pictureBox3.Visible = true;
                    JObject jo = JObject.Parse(len4);
                    string screenshot = jo["screenshot"].ToString();
                    this.pictureBox3.LoadAsync(screenshot);
                }
                else if (checkBox1.CheckState == CheckState.Unchecked)
                {
                    this.pictureBox3.Visible = false;
                }
                else
                {
                    MessageBox.Show("未知错误");
                }
            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("显示失败");
            }
            else
            {
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    this.pictureBox4.Visible = true;
                    JObject jo = JObject.Parse(len4);
                    string avatar180 = jo["avatar180"].ToString();
                    this.pictureBox4.LoadAsync(avatar180);
                    //this.textBox3.Text = avatar180;
                }
                else if (checkBox2.CheckState == CheckState.Unchecked)
                {
                    this.pictureBox4.Visible = false;
                }
                else
                {
                    MessageBox.Show("未知错误");
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.textBox3.Text == "")
            {
                MessageBox.Show("解析失败");
            }
            else
            {
                this.textBox4.Text = null;
                if (this.comboBox2.SelectedIndex == 0)
                {
                    JObject jo = JObject.Parse(len4);
                    string channel = jo["channel"].ToString();
                    string liveChannel = jo["liveChannel"].ToString();
                    string nick = jo["nick"].ToString();
                    string flash = "http://hyplayer.msstatic.com/v3.2_19082301/main.swf?topSid=" + channel + "&subSid=" + liveChannel + "&pnick=" + nick;
                    this.textBox4.Text = flash;
                }
                else if (this.comboBox2.SelectedIndex == 1)
                {
                    string aldirecthls = "http://aldirect.hls.huya.com/huyalive/" + len7 + ".m3u8";
                    string txflv = "http://tx.flv.huya.com/backsrc/" + len7 + ".m3u8";
                    string txhls = "http://tx.hls.huya.com//huyalive/" + len7 + ".m3u8";
                    this.textBox4.Text = aldirecthls + "\r\n" + txflv + "\r\n" + txhls;
                }
                else if (this.comboBox2.SelectedIndex == 2)
                {
                    string aldirecthls_1500 = "http://aldirect.hls.huya.com/huyalive/" + len7 + "_1500.m3u8";
                    string txflv_1500 = "http://tx.flv.huya.com/backsrc/" + len7 + "_1500.m3u8";
                    string txhls_1500 = "http://tx.hls.huya.com//huyalive/" + len7 + "_1500.m3u8";
                    this.textBox4.Text = aldirecthls_1500 + "\r\n" + txflv_1500 + "\r\n" + txhls_1500;
                }
                else if (this.comboBox2.SelectedIndex == 3)
                {
                    string aldirecthls_1200 = "http://aldirect.hls.huya.com/huyalive/" + len7 + "_1200.m3u8";
                    string txflv_1200 = "http://tx.flv.huya.com/backsrc/" + len7 + "_1200.m3u8";
                    string txhls_1200 = "http://tx.hls.huya.com//huyalive/" + len7 + "_1200.m3u8";
                    this.textBox4.Text = aldirecthls_1200 + "\r\n" + txflv_1200 + "\r\n" + txhls_1200;
                }
                else if (this.comboBox2.SelectedIndex == 4)
                {
                    string aldirecthls_800 = "http://aldirect.hls.huya.com/huyalive/" + len7 + "_800.m3u8";
                    string txflv_800 = "http://tx.flv.huya.com/backsrc/" + len7 + "_800.m3u8";
                    string txhls_800 = "http://tx.hls.huya.com//huyalive/" + len7 + "_800.m3u8";
                    this.textBox4.Text = aldirecthls_800 + "\r\n" + txflv_800 + "\r\n" + txhls_800;
                }
                else if (this.comboBox2.SelectedIndex == 5)
                {
                    string aldirecthls_500 = "http://aldirect.hls.huya.com/huyalive/" + len7 + "_500.m3u8";
                    string txflv_500 = "http://tx.flv.huya.com/backsrc/" + len7 + "_500.m3u8";
                    string txhls_500 = "http://tx.hls.huya.com//huyalive/" + len7 + "_500.m3u8";
                    this.textBox4.Text = aldirecthls_500 + "\r\n" + txflv_500 + "\r\n" + txhls_500;
                }
                else if (this.comboBox2.SelectedIndex == 6)
                {
                    string aldirecthls_100 = "http://aldirect.hls.huya.com/huyalive/" + len7 + "_100.m3u8";
                    string txflv_100 = "http://tx.flv.huya.com/backsrc/" + len7 + "_100.m3u8";
                    string txhls_100 = "http://tx.hls.huya.com//huyalive/" + len7 + "_100.m3u8";
                    this.textBox4.Text = aldirecthls_100 + "\r\n" + txflv_100 + "\r\n" + txhls_100;
                }
            }
        }

        private void request()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string g = "https://www.huya.com/g";
            WebRequest oRequest = WebRequest.Create(g);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader oReader = new StreamReader(oResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            html = oReader.ReadToEnd();
        }

        private void combox()
        {
            request();
            // <h3\s+class=""title"">(.*?)\</h3\>
            // <title>(?<Text>[^<]*)</title>
            // <h3 class="title">疯狂原始人</h3>
            // @"<h3\s+class=""title"">(?<text>[^<]*)</h3>"
            // @"<li[^>]*class=""game-list-item""[^>]*gid=([""'])?(?<gid>[^'""]+)[^>]*>\s+<a[^>]*target=""_blank""[^>]*href=([""'])?(?<href>[^'""]+)[^>]*class=""pic new-clickstat""[^>]*report=([""'])?(?<report>[^'""]+)[^>]*>\s+<img[^>]*class=""pic-img""[^>]*data-original=([""'])?(?<do>[^'""]+)[^>]*src=([""'])?(?<src>[^'""]+)[^>]*data-default-img=([""'])?(?<ddi>[^'""]+)[^>]*alt=([""'])?(?<alt>[^'""]+)[^>]*title=([""'])?(?<title>[^'""]+)[^>]*>\s+<h3\s+class=""title"">(?<text>[^<]*)</h3>\s+</a>\s+</li>"
            
            foreach (Match m in Regex.Matches(html, @"<h3\s+class=""title"">(?<text>[^<]*)</h3>"))
            {
                this.comboBox1.Items.Add(m.Groups["text"].ToString());
                this.label4.Text = "分类数量：" + this.comboBox1.Items.Count;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            view();
        }

        private void view()
        {
            this.label1.Text = "当前分类：" + this.comboBox1.SelectedItem.ToString();
           // string title = "\"" + this.comboBox1.SelectedItem.ToString() + "\"";

          //  var len8 = html.IndexOf("<li class=" + "\"" + "game-list-item" + "\"" + " gid=");
           // var len9 = html.IndexOf("<img title=" + title);
           // var len10 = html.Substring(len8+1, len9);
            //this.textBox5.Text = html.ToString();
           string title = "\"" + this.comboBox1.SelectedItem.ToString() + "\"";
            request();//<li[^>]*class=""game-list-item""[^>]*gid=([""'])?(?<gid>[^'""]+)[^>]*>\s+
            foreach (Match m in Regex.Matches(html, @"<a[^>]*target=""_blank""[^>]*href=([""'])?(?<href>[^'""]+)[^>]*class=""pic new-clickstat""[^>]*report=([""'])?(?<report>[^'""]+)[^>]*>\s+<img[^>]*class=""pic-img""[^>]*data-original=([""'])?(?<do>[^'""]+)[^>]*src=([""'])?(?<src>[^'""]+)[^>]*data-default-img=([""'])?(?<ddi>[^'""]+)[^>]*alt=" + title + "[^>]*title=" + title + "[^>]*>"))
            {
                //this.pictureBox1.LoadAsync(m.Groups["do"].ToString());
                this.textBox1.Text = m.Groups["href"].ToString();
               // gid = m.Groups["gid"].ToString();
                break;
            }

          //  json();*/
        }

       /* private void json()
        {
            string url = "https://www.huya.com/cache.php?m=LiveList&do=getLiveListByPage&gameId=" + gid + "&tagAll=0&page=" + 1;
            string z = "https://www.huya.com/cache.php?m=LiveList&do=getLiveListByPage&gameId=4&tagAll=0&page=1";
            ServicePointManager.DefaultConnectionLimit = 500;
            WebClient client = new WebClient();
            client.Proxy = null;
            client.Encoding = System.Text.Encoding.GetEncoding("UTF-8");
            string getJson = client.DownloadString(url);

            JObject jo = JObject.Parse(getJson);
            string all = jo.ToString();
            string status = jo["status"].ToString();
            /* string datas = jo["data"]["datas"].ToString();
            for (int i = 0; i < datas.Length; i++)
            {
                string roomName = jo["data"]["datas"][i]["roomName"].ToString();
                this.textBox2.AppendText(roomName + "\r\n");
            }*/

            /* JArray jlist = JArray.Parse(jo["data"]["datas"].ToString());
             for (int i = 0; i < jlist.Count; ++i)  //遍历JArray  
             {
                 roomName = jo["data"]["datas"][i]["roomName"].ToString() + "\r\n";
                 //this.textBox1.AppendText(roomName);

                /* ListViewItem lvi = new ListViewItem();
                 lvi.SubItems[0].Text = Convert.ToString(listView1.Items.Count + 1);
                 lvi.SubItems.Add(line);

               
             }

            //string line = roomName.Trim();
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems[0].Text = Convert.ToString(listView1.Items.Count + 1);
            lvi.SubItems.Add(status);

        }*/
    }
}
