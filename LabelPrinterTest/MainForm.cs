using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using LabelPrinterTest.Printing;

namespace LabelPrinterTest;

public partial class MainForm : Form {
    public MainForm() {
        InitializeComponent();
    }

    private async void 送信Btn_Click(object sender, EventArgs e) {
        try {
            DirectoryInfo imagePath = new(ImagePathTxtBox.Text);
            if(!imagePath.Exists) {
                MessageBox.Show("イメージパスが存在しません。");
                return;
            }

            if(!imagePath.EnumerateFiles().Any()) {
                MessageBox.Show("イメージファイルがありません。");
                return;
            }

            LabelPrinter printer = new(new NLog.Extensions.Logging.NLogLoggerFactory());
            await printer.Print(
                new PrintJobInfo() {
                    JobId = int.Parse(JobIdTxtBox.Text),
                    JobName = "StockLabel",
                    PrinterUri = new(UriTxtBox.Text)
                },
                imagePath.EnumerateFiles()
            );

            MessageBox.Show("印刷が終わりました。");
        } catch(Exception ex) {
            MessageBox.Show(ex.ToString());
        }
    }

    private bool CanAcceptFileNames(string[]? fileNames)
        => fileNames != null && fileNames.Length == 1 && Directory.Exists(fileNames[0]);


    private void ImageFolderPanel_DragDrop(object sender, DragEventArgs e) {
        if(e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false) {
            string[]? fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if(CanAcceptFileNames(fileNames)) {
                ImagePathTxtBox.Text = fileNames![0];
            }
        }
    }

    private void ImageFolderPanel_DragOver(object sender, DragEventArgs e) {
        DragDropEffects rslt = DragDropEffects.None;
        if(e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false) {
            string[]? fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if(CanAcceptFileNames(fileNames)) {
                rslt = DragDropEffects.Link;
            }
        }

        e.Effect = rslt;
    }

    private void 参照Btn_Click(object sender, EventArgs e) {
        using FolderBrowserDialog fd = new() {
            SelectedPath = ImagePathTxtBox.Text
        };

        if(fd.ShowDialog() == DialogResult.OK) {
            ImagePathTxtBox.Text = fd.SelectedPath;
        }
    }

    private void 終了Btn_Click(object sender, EventArgs e) {
        Close();
    }
}
