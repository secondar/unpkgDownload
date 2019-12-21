namespace unpkgDownload
{
    partial class main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_geturl = new System.Windows.Forms.Button();
            this.lb_url = new System.Windows.Forms.Label();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_savepath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_savepath = new System.Windows.Forms.Button();
            this.bt_download = new System.Windows.Forms.Button();
            this.cb_edition = new System.Windows.Forms.ComboBox();
            this.lv_msg = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // bt_geturl
            // 
            this.bt_geturl.Location = new System.Drawing.Point(574, 25);
            this.bt_geturl.Name = "bt_geturl";
            this.bt_geturl.Size = new System.Drawing.Size(75, 23);
            this.bt_geturl.TabIndex = 0;
            this.bt_geturl.Text = "拉取信息";
            this.bt_geturl.UseVisualStyleBackColor = true;
            this.bt_geturl.Click += new System.EventHandler(this.Bt_geturl_Click);
            // 
            // lb_url
            // 
            this.lb_url.AutoSize = true;
            this.lb_url.Location = new System.Drawing.Point(11, 28);
            this.lb_url.Name = "lb_url";
            this.lb_url.Size = new System.Drawing.Size(65, 12);
            this.lb_url.TabIndex = 1;
            this.lb_url.Text = "unpkg地址:";
            // 
            // tb_url
            // 
            this.tb_url.Location = new System.Drawing.Point(83, 25);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(484, 21);
            this.tb_url.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择版本:";
            // 
            // tb_savepath
            // 
            this.tb_savepath.Location = new System.Drawing.Point(83, 52);
            this.tb_savepath.Name = "tb_savepath";
            this.tb_savepath.ReadOnly = true;
            this.tb_savepath.Size = new System.Drawing.Size(168, 21);
            this.tb_savepath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "保存路径:";
            // 
            // bt_savepath
            // 
            this.bt_savepath.Location = new System.Drawing.Point(256, 50);
            this.bt_savepath.Name = "bt_savepath";
            this.bt_savepath.Size = new System.Drawing.Size(75, 23);
            this.bt_savepath.TabIndex = 12;
            this.bt_savepath.Text = "选择";
            this.bt_savepath.Click += new System.EventHandler(this.Bt_savepath_Click);
            // 
            // bt_download
            // 
            this.bt_download.Enabled = false;
            this.bt_download.Location = new System.Drawing.Point(574, 51);
            this.bt_download.Name = "bt_download";
            this.bt_download.Size = new System.Drawing.Size(75, 23);
            this.bt_download.TabIndex = 9;
            this.bt_download.Text = "下载";
            this.bt_download.UseVisualStyleBackColor = true;
            this.bt_download.Click += new System.EventHandler(this.Bt_download_Click);
            // 
            // cb_edition
            // 
            this.cb_edition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_edition.FormattingEnabled = true;
            this.cb_edition.Location = new System.Drawing.Point(402, 53);
            this.cb_edition.Name = "cb_edition";
            this.cb_edition.Size = new System.Drawing.Size(165, 20);
            this.cb_edition.TabIndex = 10;
            // 
            // lv_msg
            // 
            this.lv_msg.Location = new System.Drawing.Point(19, 79);
            this.lv_msg.Name = "lv_msg";
            this.lv_msg.Size = new System.Drawing.Size(624, 359);
            this.lv_msg.TabIndex = 11;
            this.lv_msg.UseCompatibleStateImageBehavior = false;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 450);
            this.Controls.Add(this.lv_msg);
            this.Controls.Add(this.cb_edition);
            this.Controls.Add(this.bt_download);
            this.Controls.Add(this.bt_savepath);
            this.Controls.Add(this.tb_savepath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.lb_url);
            this.Controls.Add(this.bt_geturl);
            this.Name = "main";
            this.Text = "unpkg下载工具 - 一颗大萝北";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_geturl;
        private System.Windows.Forms.Label lb_url;
        private System.Windows.Forms.TextBox tb_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_savepath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_savepath;
        private System.Windows.Forms.Button bt_download;
        private System.Windows.Forms.ComboBox cb_edition;
        private System.Windows.Forms.ListView lv_msg;
    }
}

