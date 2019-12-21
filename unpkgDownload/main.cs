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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unpkgDownload
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        static string downloadUrl = "";
        static string downloadPath = "";
        static string unpkgUrl = "";
        static List<string> fileList = new List<string>();
        private void Main_Load(object sender, EventArgs e)
        {
            lv_msg.View = View.Details;
            lv_msg.Columns.Add("消息日志列表", lv_msg.Width-5);
            cb_edition.Items.Add("等待拉取信息");
            cb_edition.SelectedIndex = 0;
            ////////
            tb_url.Text = "https://unpkg.com/browse/element-ui/";
        }

        private void Bt_geturl_Click(object sender, EventArgs e)
        {
            string strUrl =  tb_url.Text.Trim();
            if (strUrl == "")
            {
                MessageBox.Show("请输入unpkg地址！","提示");
                ListViewItem ls = new ListViewItem("错误:请输入unpkg地址！");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                return;
            }
            if (!strUrl.Contains("//unpkg.com/browse/"))
            {
                MessageBox.Show("请输入正确unpkg地址！\n如:\nhttps://unpkg.com/browse/element-ui/", "提示");
                ListViewItem ls = new ListViewItem("错误:请输入unpkg地址！");
                lv_msg.Items.Add(ls);
                ls = new ListViewItem("如:https://unpkg.com/browse/element-ui/");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                return;

            }
            //判断URL是否带版本,如果带版本就取消版本进行获取并设置
            if (strUrl.Contains("@"))
            {
                //带版本信息,处理掉
                string strTmpUrl = GetMiddleStr(strUrl, "@", "/");
                strTmpUrl = "@" + strTmpUrl + "/";
                strUrl = strUrl.Replace(strTmpUrl, "");
            }
            else
            {
                //不带版本信息,判断是否最后带一个/,如果带处理掉最后一个/
                if (strUrl[strUrl.Length - 1].ToString() == "/")
                {
                    strUrl = strUrl.Substring(0, strUrl.Length - 1);
                }
            }
            Handle(strUrl);
            /*ListViewItem ls = new ListViewItem("123");
            lv_msg.Items.Add(ls);*/
        }
        /// <summary>
        /// 取中间文本
        /// </summary>
        /// <param name="oldStr">原文</param>
        /// <param name="preStr">前文</param>
        /// <param name="nextStr">后文</param>
        /// <returns></returns>
        private static string GetMiddleStr(string oldStr, string preStr, string nextStr)
        {
            string tempStr = oldStr.Substring(oldStr.IndexOf(preStr) + preStr.Length);
            tempStr = tempStr.Substring(0, tempStr.IndexOf(nextStr));
            return tempStr;
        }
        public void Handle(string strUrl)
        {
            //访问获取网页源代码
            string strHtml = "";
            try
            {
                ListViewItem ls = new ListViewItem("请求中,请稍后...");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                strHtml = getHttp(strUrl);
                string strJson = GetMiddleStr(strHtml, "<script>window.__DATA__ = ", "</script>");
                strJson = "{\"ret\":" + strJson + "}";
                JObject Data = (JObject)JsonConvert.DeserializeObject(strJson);
                JArray jArray = new JArray();
                jArray = JArray.Parse(Data["ret"]["availableVersions"].ToString());
                cb_edition.Text = jArray[jArray.Count-1].ToString();
                cb_edition.Items.Clear();
                foreach (var i in jArray)
                {
                    cb_edition.Items.Add(i.ToString());
                }
                cb_edition.SelectedIndex = jArray.Count - 1;
                ls = new ListViewItem("版本信息获取成功...");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                bt_download.Enabled = true;
                unpkgUrl = strUrl;
            }
            catch
            {
                ListViewItem ls = new ListViewItem("请求中失败,请检查网络连接...");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }
                    
        }
        /// <summary>
         /// 
         /// </summary>
         /// <param name="strUrl"></param>
         /// <param name="Timeout"></param>
         /// <returns></returns>
        private string getHttp(string strUrl, int Timeout = 900000)
        {
            strUrl = strUrl + "/";
            ListViewItem ls;
            ls = new ListViewItem("正在请求:" + strUrl);
            this.lv_msg.Invoke(new Action(() =>
            {
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private void Bt_savepath_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
                tb_savepath.Text = path;
            }
            else
            {
                ListViewItem ls = new ListViewItem("用户取消选择保存路径...");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }
            
        }

        private void Bt_download_Click(object sender, EventArgs e)
        {
            ListViewItem ls;
            if (tb_savepath.Text.Trim()=="")
            {
                MessageBox.Show("请选择保存路径", "错误");
                ls = new ListViewItem("错误:请选择保存路径");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                return;
            }
            //判断用户选择的路径是否存在
            if (!Directory.Exists(tb_savepath.Text.Trim()))
            {
                MessageBox.Show("请选择正确文件路径","错误");
                ls = new ListViewItem("错误:请选择正确保存路径");
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                return;
            }
            ls = new ListViewItem("开始获取文件信息...");
            lv_msg.Items.Add(ls);
            unpkgUrl = unpkgUrl + "@" + cb_edition.Text;
            downloadPath = tb_savepath.Text.Trim() + "/";
            bt_download.Enabled = false;
            bt_geturl.Enabled = false;
            bt_savepath.Enabled = false;
            Thread thread = new Thread(Download);
            thread.IsBackground = true;
            thread.Start();

        }
        //获取文件及目录进行下载
        public void Download()
        {
            string strUrl = unpkgUrl;
            string strHtml = "";
            string strDownloadUrl = strUrl.Replace("browse/", "");
            ListViewItem ls;
            try
            {
                ls = new ListViewItem("开始获取...");
                this.lv_msg.Invoke(new Action(() =>
                {
                    lv_msg.Items.Add(ls);
                    lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                }));
                
                fileList = scanPath(strUrl, strDownloadUrl, strUrl);
            }
            catch
            {
                ls = new ListViewItem("访问unpkg错误,请重试");
                this.lv_msg.Invoke(new Action(() =>
                {
                    lv_msg.Items.Add(ls);
                    lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                }));
                return;
            }
            ls = new ListViewItem("文件获取完成,即将开始下载...");
            this.lv_msg.Invoke(new Action(() =>
            {
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }));
            foreach (var url in fileList)
            {
                //strDownloadUrl
                string strPath = url.Replace(strDownloadUrl + "/", "");
                string strDownloadPath = downloadPath;
                if (strPath.Contains("/"))
                {
                    string[] arrPath = strPath.Split('/');
                    //Console.WriteLine("文件:" + arrPath[arrPath.Length-1]);
                    //创建完整文件夹
                    for (int i = 0; i < arrPath.Length - 1; i++)
                    {
                        string mkdirPath = "";
                        if (i == 0)
                        {
                            mkdirPath = downloadPath + arrPath[i] + "/";
                        }
                        else
                        {
                            string tmpPath = "/";
                            for (int IS = 0; IS <= i; IS++)
                            {
                                tmpPath = tmpPath + arrPath[IS] + "/";
                            }
                            mkdirPath = downloadPath + tmpPath;
                        }
                        //判断目录是否存在,不存在创建
                        if (!Directory.Exists(mkdirPath))
                        {
                            System.IO.Directory.CreateDirectory(mkdirPath);
                            ls = new ListViewItem("创建文件夹:" + mkdirPath);
                            this.lv_msg.Invoke(new Action(() =>
                            {
                                lv_msg.Items.Add(ls);
                                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                            }));
                        }
                        strDownloadPath = mkdirPath;
                        strDownloadPath = strDownloadPath + arrPath[arrPath.Length - 1];
                    }
                }
                else
                {
                    strDownloadPath = strDownloadPath + strPath;
                }
                //开始下载文件
                bool DownloadBack = DownloadFile(url, strDownloadPath);
                if (!DownloadBack)
                {
                    ls = new ListViewItem("文件下载失败:" + url);
                    this.lv_msg.Invoke(new Action(() =>
                    {
                        lv_msg.Items.Add(ls);
                        lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                    }));

                }
            }
            ls = new ListViewItem("全部下载完成");
            this.lv_msg.Invoke(new Action(() =>
            {
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }));
            ls = new ListViewItem("文件目录:" + downloadPath);
            this.lv_msg.Invoke(new Action(() =>
            {
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }));
            //还原按钮状态
            this.bt_download.Invoke(new Action(() =>
            {
                bt_download.Enabled = true;
            }));
            this.bt_geturl.Invoke(new Action(() =>
            {
                bt_geturl.Enabled = true;
            }));
            this.bt_savepath.Invoke(new Action(() =>
            {
                bt_savepath.Enabled = true;
            }));
        }
        private List<string> scanPath(string strUrl, string strDownloadUrl, string homeUrl)
        {
            List<string> list = new List<string>();
            ListViewItem ls;
            try
            {
                string strHtml = getHttp(strUrl);

                string strJson = GetMiddleStr(strHtml, "<script>window.__DATA__ = ", "</script>");
                strJson = "{\"ret\":" + strJson + "}";
                JObject Data = (JObject)JsonConvert.DeserializeObject(strJson);
                foreach (var i in Data["ret"]["target"]["details"])
                {
                    foreach (var iS in i)
                    {
                        if (iS["type"].ToString() == "file")
                        {
                            list.Add(strDownloadUrl + iS["path"].ToString());
                            ls = new ListViewItem("加入文件:" + iS["path"].ToString());
                            this.lv_msg.Invoke(new Action(() =>
                            {
                                lv_msg.Items.Add(ls);
                                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                            }));
                        }
                        else if ("directory" == iS["type"].ToString())
                        {
                            List<string> listBak = new List<string>();
                            listBak = scanPath(homeUrl + iS["path"].ToString(), strDownloadUrl, homeUrl);
                            foreach (var backList in listBak)
                            {
                                list.Add(backList.ToString());
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ls = new ListViewItem("访问unpkg错误,请重试...");
                this.lv_msg.Invoke(new Action(() =>
                {
                    lv_msg.Items.Add(ls);
                }));

            }
            return list;
        }
        public bool DownloadFile(string URL, string filename)
        {
            ListViewItem ls;
            ls = new ListViewItem("正在下载文件:" + URL);
            this.lv_msg.Invoke(new Action(() =>
            {
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }));
            ls = new ListViewItem("已下载:0byts");
            this.lv_msg.Invoke(new Action(() =>
            {
                lv_msg.Items.Add(ls);
                lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
            }));
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);


                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;

                    so.Write(by, 0, osize);

                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    // "当前补丁下载进度" + percent.ToString() + "%";
                    //ls = new ListViewItem("下载进度:" + percent.ToString());
                    this.lv_msg.Invoke(new Action(() =>
                    {
                        lv_msg.Items[lv_msg.Items.Count - 1].Text = "已下载:" + (percent - percent * 2).ToString() + "byts";
                        //lv_msg.Items.Add(ls);
                        //lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                    }));
                }
                so.Close();
                st.Close();
                ls = new ListViewItem("文件下载完成...");
                this.lv_msg.Invoke(new Action(() =>
                {
                    lv_msg.Items.Add(ls);
                    lv_msg.Items[lv_msg.Items.Count - 1].EnsureVisible();
                }));
                return true;
            }
            catch (System.Exception ex)
            {
                //Debug.WriteLine(ex.ToString());
                //throw;
                return false;
            }
        }
    }
}
