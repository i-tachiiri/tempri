using System.Data;
using System.DirectoryServices;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System;
using PrintSiteBuilder.Models;
using PrintSiteBuilder.Utilities;
using PrintSiteBuilder.SiteItem;
using PrintSiteBuilder.Print;
using PrintSiteBuilder.Models.Print;


namespace PrintSiteBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
#if !DEBUG
            this.BackColor = SystemColors.ActiveCaption;
            tabPage1.BackColor = SystemColors.ActiveCaption;
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //FactoryClasses.DataSource = PrintFactory.ClassNameWithClass.Keys.Reverse().ToList();
            //FactoryClasses.Text = PrintFactory.ClassNameWithClass.Keys.Reverse().ToList().FirstOrDefault();
            FactoryClasses.DataSource = PrintFactory.ClassNameWithClass.Keys.ToList();
            FactoryClasses.Text = PrintFactory.ClassNameWithClass.Keys.ToList().FirstOrDefault();
        }

        private async void UpdateSlide(object sender, EventArgs e)
        {
            ProgressLabel.Text = "Googleスライド取得中...";
            ProgressLabel.Update();
            var json = new Json();
            var iPrint = PrintFactory.GetPrintClass(FactoryClasses.Text);
            var slidePages = new SlidePages(iPrint.PresentationId);
            var path = Path.Combine(@"C:\drive\work\www\item\print\100000", $"{iPrint.PresentationId}.json");

            if (!File.Exists(path) || IsJsonUpdate.Checked)
            {
                json.SerializePrintConfig(iPrint);
                
            }

            if (IsUpdateSlide.Checked)
            {
                ProgressLabel.Text = "Googleスライド更新中...";
                ProgressLabel.Update();
                slidePages.UpdateSlide(iPrint);
            }
            if (IsExportSvgFromSlide.Checked)
            {
                ProgressLabel.Text = "SVGエクスポート中...";
                ProgressLabel.Update(); var slide = new Slide();
                slide.GetSvgsFromSlide(FactoryClasses.Text);
            }
            ProgressLabel.Text = "完了";
            ProgressLabel.Update();
        }


        private async void CreateItemsFromSvg(object sender, EventArgs e)
        {
            if(IsExportSvg.Checked)
            {
                var slide = new Slide();
                await slide.GetSvgsFromAllSlides();
            }

            var category = new Category();
            category.CreateCategoryConfig();
            category.CreateDocConfig();
            /*if (category.IsGroupConfigCreated())
            {
                MessageBox.Show("新しい設定ファイルが作成されました。カテゴリを設定してください。");
                return;
            }*/
            var json = new Json();
            var item = new Item();
            ItemsConfig itemsConfig;
            if (IsUpdateConfig.Checked)
            {
                itemsConfig = item.GetItemsConfig(IsUpdateAllConfig.Checked);
                json.SerializeItemsConfig(itemsConfig);
            }
            itemsConfig = json.DeserializeItemsConfig();
            AutoGenerate(itemsConfig);
        }
        private void AutoGenerate(ItemsConfig itemsConfig)//, ItemsConfig docsConfig)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (itemsConfig.itemConfigList != null)
            {
                var item = new Item();
                item.RemoveEmptyItem();
                item.RemoveInvalidItem(itemsConfig);
            }

            var image = new PrintSiteBuilder.SiteItem.Image();
            if (IsCreateImage.Checked)
            {
                image.CreateQr(IsUpdateAllImage.Checked, itemsConfig);
                image.CreateAttachedLogoAndQr(IsUpdateAllImage.Checked, itemsConfig);
                image.CreateInstagramImage(IsUpdateAllImage.Checked, itemsConfig);
                image.CreateImages(IsUpdateAllImage.Checked, itemsConfig);
            }
            if (IsCreatePdf.Checked)
            {
                image.CreatePdfFromGroup(IsUpdateAllImage.Checked, itemsConfig);
            }
            if (IsCreateHtml.Checked)
            {
                var html = new Html();
                html.CreateIndexHtml();
                html.CreateHtmls(IsUpdateAllHtml.Checked, IsOnlyTest.Checked, itemsConfig);
            }

            ProgressLabel.Text = "Update Sitemap...";
            ProgressLabel.Update();
            var jsonClass = new Json();
            var docsConfig = jsonClass.DeserializeDocsConfig();
            var sitemap = new Sitemap();
            sitemap.UpdateSitemap(itemsConfig, docsConfig);

            ProgressLabel.Text = "Update Keys...";
            ProgressLabel.Update();
            var keys = new KeysJs();
            keys.UpdateKeys(itemsConfig, docsConfig);

            if (IsFtpUpload.Checked)
            {
                ProgressLabel.Text = "FTP uploading...";
                ProgressLabel.Update();
                var bat = new Bat();
                bat.RunBat("ftp-sync.bat", true);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            ProgressLabel.Text = $"実行時間: {ts.Hours} : {ts.Minutes} : {ts.Seconds}";
            ProgressLabel.Update();

            MessageBox.Show("終了");
        }

        private void CreateHtmlFromMarkdown(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var category = new Category();
            category.CreateDocConfig();

            var doc = new Doc();
            var json = new Json();
            var itemsConfig = json.DeserializeItemsConfig();
            var docsConfig = doc.GetDocsConfig(ProgressLabel);

            if (docsConfig.itemConfigList != null)
            {
                ProgressLabel.Text = "Removing Empty Items...";
                ProgressLabel.Update();
                var item = new Item();
                item.RemoveEmptyItem();
            }
            if (IsCreateImage.Checked)
            {
                var image = new PrintSiteBuilder.SiteItem.Image();
                image.CreateDocImages(IsUpdateAllImage.Checked, docsConfig, ProgressLabel);
            }
            if (IsCreateHtml.Checked)
            {
                doc.CreateDocs(IsUpdateAllHtml.Checked, IsOnlyTest.Checked, docsConfig, ProgressLabel);
            }

            ProgressLabel.Text = "Update Sitemap...";
            ProgressLabel.Update();
            var sitemap = new Sitemap();
            sitemap.UpdateSitemap(itemsConfig, docsConfig);

            ProgressLabel.Text = "Update Keys...";
            ProgressLabel.Update();
            var keys = new KeysJs();
            keys.UpdateKeys(itemsConfig, docsConfig);

            if (IsFtpUpload.Checked)
            {
                ProgressLabel.Text = "FTP uploading...";
                ProgressLabel.Update();
                var bat = new Bat();
                bat.RunBat("ftp-sync-page.bat", true);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            ProgressLabel.Text = $"実行時間: {Math.Round(ts.TotalSeconds)}秒";
            ProgressLabel.Update();

            MessageBox.Show("終了");

        }
    }
}
