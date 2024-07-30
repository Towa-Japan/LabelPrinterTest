namespace LabelPrinterTest;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.URILbl = new System.Windows.Forms.Label();
            this.UriTxtBox = new System.Windows.Forms.TextBox();
            this.終了Btn = new System.Windows.Forms.Button();
            this.送信Btn = new System.Windows.Forms.Button();
            this.ジョブIDLbl = new System.Windows.Forms.Label();
            this.JobIdTxtBox = new System.Windows.Forms.TextBox();
            this.イメージパスLbl = new System.Windows.Forms.Label();
            this.ImagePathTxtBox = new System.Windows.Forms.TextBox();
            this.参照Btn = new System.Windows.Forms.Button();
            this.ImageFolderPanel = new System.Windows.Forms.Panel();
            this.ImageFolderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // URILbl
            // 
            this.URILbl.AutoSize = true;
            this.URILbl.Location = new System.Drawing.Point(12, 9);
            this.URILbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.URILbl.Name = "URILbl";
            this.URILbl.Size = new System.Drawing.Size(106, 19);
            this.URILbl.TabIndex = 0;
            this.URILbl.Text = "プリンターURI";
            // 
            // UriTxtBox
            // 
            this.UriTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UriTxtBox.Location = new System.Drawing.Point(12, 31);
            this.UriTxtBox.Name = "UriTxtBox";
            this.UriTxtBox.Size = new System.Drawing.Size(360, 26);
            this.UriTxtBox.TabIndex = 1;
            this.UriTxtBox.Text = "ipps://192.168.0.50:443/ipp/print";
            // 
            // 終了Btn
            // 
            this.終了Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.終了Btn.Location = new System.Drawing.Point(297, 159);
            this.終了Btn.Name = "終了Btn";
            this.終了Btn.Size = new System.Drawing.Size(75, 40);
            this.終了Btn.TabIndex = 99;
            this.終了Btn.Text = "終了";
            this.終了Btn.UseVisualStyleBackColor = true;
            this.終了Btn.Click += new System.EventHandler(this.終了Btn_Click);
            // 
            // 送信Btn
            // 
            this.送信Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.送信Btn.Location = new System.Drawing.Point(216, 159);
            this.送信Btn.Name = "送信Btn";
            this.送信Btn.Size = new System.Drawing.Size(75, 40);
            this.送信Btn.TabIndex = 90;
            this.送信Btn.Text = "送信";
            this.送信Btn.UseVisualStyleBackColor = true;
            this.送信Btn.Click += new System.EventHandler(this.送信Btn_Click);
            // 
            // ジョブIDLbl
            // 
            this.ジョブIDLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ジョブIDLbl.AutoSize = true;
            this.ジョブIDLbl.Location = new System.Drawing.Point(200, 66);
            this.ジョブIDLbl.Name = "ジョブIDLbl";
            this.ジョブIDLbl.Size = new System.Drawing.Size(66, 19);
            this.ジョブIDLbl.TabIndex = 10;
            this.ジョブIDLbl.Text = "ジョブID";
            // 
            // JobIdTxtBox
            // 
            this.JobIdTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JobIdTxtBox.Location = new System.Drawing.Point(272, 63);
            this.JobIdTxtBox.Name = "JobIdTxtBox";
            this.JobIdTxtBox.Size = new System.Drawing.Size(100, 26);
            this.JobIdTxtBox.TabIndex = 11;
            this.JobIdTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // イメージパスLbl
            // 
            this.イメージパスLbl.AutoSize = true;
            this.イメージパスLbl.Location = new System.Drawing.Point(-4, 0);
            this.イメージパスLbl.Name = "イメージパスLbl";
            this.イメージパスLbl.Size = new System.Drawing.Size(99, 19);
            this.イメージパスLbl.TabIndex = 100;
            this.イメージパスLbl.Text = "イメージパス";
            // 
            // ImagePathTxtBox
            // 
            this.ImagePathTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePathTxtBox.Location = new System.Drawing.Point(3, 22);
            this.ImagePathTxtBox.Name = "ImagePathTxtBox";
            this.ImagePathTxtBox.Size = new System.Drawing.Size(276, 26);
            this.ImagePathTxtBox.TabIndex = 1;
            // 
            // 参照Btn
            // 
            this.参照Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.参照Btn.Location = new System.Drawing.Point(285, 14);
            this.参照Btn.Name = "参照Btn";
            this.参照Btn.Size = new System.Drawing.Size(75, 40);
            this.参照Btn.TabIndex = 90;
            this.参照Btn.Text = "参照...";
            this.参照Btn.UseVisualStyleBackColor = true;
            this.参照Btn.Click += new System.EventHandler(this.参照Btn_Click);
            // 
            // ImageFolderPanel
            // 
            this.ImageFolderPanel.AllowDrop = true;
            this.ImageFolderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageFolderPanel.Controls.Add(this.イメージパスLbl);
            this.ImageFolderPanel.Controls.Add(this.参照Btn);
            this.ImageFolderPanel.Controls.Add(this.ImagePathTxtBox);
            this.ImageFolderPanel.Location = new System.Drawing.Point(12, 95);
            this.ImageFolderPanel.Name = "ImageFolderPanel";
            this.ImageFolderPanel.Size = new System.Drawing.Size(360, 54);
            this.ImageFolderPanel.TabIndex = 101;
            this.ImageFolderPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageFolderPanel_DragDrop);
            this.ImageFolderPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.ImageFolderPanel_DragOver);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.ImageFolderPanel);
            this.Controls.Add(this.送信Btn);
            this.Controls.Add(this.終了Btn);
            this.Controls.Add(this.JobIdTxtBox);
            this.Controls.Add(this.ジョブIDLbl);
            this.Controls.Add(this.UriTxtBox);
            this.Controls.Add(this.URILbl);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.Text = "IPPラスター送信テスト";
            this.ImageFolderPanel.ResumeLayout(false);
            this.ImageFolderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label URILbl;
    private System.Windows.Forms.TextBox UriTxtBox;
    private System.Windows.Forms.Label ジョブIDLbl;
    private System.Windows.Forms.TextBox JobIdTxtBox;
    private System.Windows.Forms.Button 終了Btn;
    private System.Windows.Forms.Button 送信Btn;
    private System.Windows.Forms.Label イメージパスLbl;
    private System.Windows.Forms.TextBox ImagePathTxtBox;
    private System.Windows.Forms.Button 参照Btn;
    private System.Windows.Forms.Panel ImageFolderPanel;
}
