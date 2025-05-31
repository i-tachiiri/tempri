namespace PrintSiteBuilder
{
    partial class Form1
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
            dataGridView = new DataGridView();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            IsUpdateSlide = new CheckBox();
            IsExportSvgFromSlide = new CheckBox();
            IsUpdateAllHtml = new CheckBox();
            IsUpdateAllPdf = new CheckBox();
            IsUpdateAllImage = new CheckBox();
            FactoryClasses = new ComboBox();
            IsExportSvg = new CheckBox();
            button2 = new Button();
            IsUpdateConfig = new CheckBox();
            ProgressLabel = new Label();
            IsFtpUpload = new CheckBox();
            IsCreateHtml = new CheckBox();
            IsCreatePdf = new CheckBox();
            IsCreateImage = new CheckBox();
            IsOnlyTest = new CheckBox();
            IsUpdateAllConfig = new CheckBox();
            ButtonAutoCreate2 = new Button();
            ButtonTest = new Button();
            LabelStatus = new Label();
            tabPage2 = new TabPage();
            RakutenItemName = new ComboBox();
            RakutenRegister = new Button();
            label2 = new Label();
            label1 = new Label();
            RakutenHtml = new TextBox();
            tabPage3 = new TabPage();
            IsJsonUpdate = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Font = new Font("Yu Gothic UI", 9F);
            dataGridView.Location = new Point(19, 168);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(984, 370);
            dataGridView.TabIndex = 2;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1026, 606);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.DimGray;
            tabPage1.Controls.Add(IsJsonUpdate);
            tabPage1.Controls.Add(IsUpdateSlide);
            tabPage1.Controls.Add(IsExportSvgFromSlide);
            tabPage1.Controls.Add(IsUpdateAllHtml);
            tabPage1.Controls.Add(IsUpdateAllPdf);
            tabPage1.Controls.Add(IsUpdateAllImage);
            tabPage1.Controls.Add(FactoryClasses);
            tabPage1.Controls.Add(IsExportSvg);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(IsUpdateConfig);
            tabPage1.Controls.Add(ProgressLabel);
            tabPage1.Controls.Add(IsFtpUpload);
            tabPage1.Controls.Add(IsCreateHtml);
            tabPage1.Controls.Add(IsCreatePdf);
            tabPage1.Controls.Add(IsCreateImage);
            tabPage1.Controls.Add(IsOnlyTest);
            tabPage1.Controls.Add(IsUpdateAllConfig);
            tabPage1.Controls.Add(ButtonAutoCreate2);
            tabPage1.Controls.Add(ButtonTest);
            tabPage1.Controls.Add(LabelStatus);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1018, 578);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "item";
            // 
            // IsUpdateSlide
            // 
            IsUpdateSlide.AutoSize = true;
            IsUpdateSlide.Location = new Point(774, 27);
            IsUpdateSlide.Name = "IsUpdateSlide";
            IsUpdateSlide.Size = new Size(84, 19);
            IsUpdateSlide.TabIndex = 40;
            IsUpdateSlide.Text = "スライド更新";
            IsUpdateSlide.UseVisualStyleBackColor = true;
            // 
            // IsExportSvgFromSlide
            // 
            IsExportSvgFromSlide.AutoSize = true;
            IsExportSvgFromSlide.Location = new Point(774, 109);
            IsExportSvgFromSlide.Name = "IsExportSvgFromSlide";
            IsExportSvgFromSlide.Size = new Size(100, 19);
            IsExportSvgFromSlide.TabIndex = 39;
            IsExportSvgFromSlide.Text = "SVGエクスポート";
            IsExportSvgFromSlide.UseVisualStyleBackColor = true;
            // 
            // IsUpdateAllHtml
            // 
            IsUpdateAllHtml.AutoSize = true;
            IsUpdateAllHtml.Location = new Point(365, 96);
            IsUpdateAllHtml.Name = "IsUpdateAllHtml";
            IsUpdateAllHtml.Size = new Size(74, 19);
            IsUpdateAllHtml.TabIndex = 38;
            IsUpdateAllHtml.Text = "一括更新";
            IsUpdateAllHtml.UseVisualStyleBackColor = true;
            // 
            // IsUpdateAllPdf
            // 
            IsUpdateAllPdf.AutoSize = true;
            IsUpdateAllPdf.Location = new Point(283, 96);
            IsUpdateAllPdf.Name = "IsUpdateAllPdf";
            IsUpdateAllPdf.Size = new Size(74, 19);
            IsUpdateAllPdf.TabIndex = 37;
            IsUpdateAllPdf.Text = "一括更新";
            IsUpdateAllPdf.UseVisualStyleBackColor = true;
            // 
            // IsUpdateAllImage
            // 
            IsUpdateAllImage.AutoSize = true;
            IsUpdateAllImage.Location = new Point(193, 96);
            IsUpdateAllImage.Name = "IsUpdateAllImage";
            IsUpdateAllImage.Size = new Size(74, 19);
            IsUpdateAllImage.TabIndex = 36;
            IsUpdateAllImage.Text = "一括更新";
            IsUpdateAllImage.UseVisualStyleBackColor = true;
            // 
            // FactoryClasses
            // 
            FactoryClasses.FormattingEnabled = true;
            FactoryClasses.Location = new Point(29, 27);
            FactoryClasses.Name = "FactoryClasses";
            FactoryClasses.Size = new Size(609, 23);
            FactoryClasses.TabIndex = 35;
            // 
            // IsExportSvg
            // 
            IsExportSvg.AutoSize = true;
            IsExportSvg.Location = new Point(29, 71);
            IsExportSvg.Name = "IsExportSvg";
            IsExportSvg.Size = new Size(47, 19);
            IsExportSvg.TabIndex = 34;
            IsExportSvg.Text = "SVG";
            IsExportSvg.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(774, 65);
            button2.Name = "button2";
            button2.Size = new Size(115, 28);
            button2.TabIndex = 29;
            button2.Text = "MD一括処理";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CreateHtmlFromMarkdown;
            // 
            // IsUpdateConfig
            // 
            IsUpdateConfig.AutoSize = true;
            IsUpdateConfig.Location = new Point(92, 71);
            IsUpdateConfig.Name = "IsUpdateConfig";
            IsUpdateConfig.Size = new Size(84, 19);
            IsUpdateConfig.TabIndex = 28;
            IsUpdateConfig.Text = "設定ファイル";
            IsUpdateConfig.UseVisualStyleBackColor = true;
            // 
            // ProgressLabel
            // 
            ProgressLabel.AutoSize = true;
            ProgressLabel.BackColor = Color.Transparent;
            ProgressLabel.Font = new Font("Yu Gothic UI", 9F);
            ProgressLabel.Location = new Point(43, 514);
            ProgressLabel.Name = "ProgressLabel";
            ProgressLabel.Size = new Size(52, 15);
            ProgressLabel.TabIndex = 27;
            ProgressLabel.Text = "Progress";
            // 
            // IsFtpUpload
            // 
            IsFtpUpload.AutoSize = true;
            IsFtpUpload.Location = new Point(469, 71);
            IsFtpUpload.Name = "IsFtpUpload";
            IsFtpUpload.Size = new Size(96, 19);
            IsFtpUpload.TabIndex = 21;
            IsFtpUpload.Text = "FTPアップロード";
            IsFtpUpload.UseVisualStyleBackColor = true;
            // 
            // IsCreateHtml
            // 
            IsCreateHtml.AutoSize = true;
            IsCreateHtml.Location = new Point(365, 71);
            IsCreateHtml.Name = "IsCreateHtml";
            IsCreateHtml.Size = new Size(58, 19);
            IsCreateHtml.TabIndex = 20;
            IsCreateHtml.Text = "HTML";
            IsCreateHtml.UseVisualStyleBackColor = true;
            // 
            // IsCreatePdf
            // 
            IsCreatePdf.AutoSize = true;
            IsCreatePdf.Location = new Point(283, 71);
            IsCreatePdf.Name = "IsCreatePdf";
            IsCreatePdf.Size = new Size(47, 19);
            IsCreatePdf.TabIndex = 19;
            IsCreatePdf.Text = "PDF";
            IsCreatePdf.UseVisualStyleBackColor = true;
            // 
            // IsCreateImage
            // 
            IsCreateImage.AutoSize = true;
            IsCreateImage.Location = new Point(193, 71);
            IsCreateImage.Name = "IsCreateImage";
            IsCreateImage.Size = new Size(50, 19);
            IsCreateImage.TabIndex = 18;
            IsCreateImage.Text = "画像";
            IsCreateImage.UseVisualStyleBackColor = true;
            // 
            // IsOnlyTest
            // 
            IsOnlyTest.AutoSize = true;
            IsOnlyTest.Location = new Point(365, 125);
            IsOnlyTest.Name = "IsOnlyTest";
            IsOnlyTest.Size = new Size(101, 19);
            IsOnlyTest.TabIndex = 16;
            IsOnlyTest.Text = "テストページのみ";
            IsOnlyTest.UseVisualStyleBackColor = true;
            // 
            // IsUpdateAllConfig
            // 
            IsUpdateAllConfig.AutoSize = true;
            IsUpdateAllConfig.Location = new Point(92, 96);
            IsUpdateAllConfig.Name = "IsUpdateAllConfig";
            IsUpdateAllConfig.Size = new Size(74, 19);
            IsUpdateAllConfig.TabIndex = 15;
            IsUpdateAllConfig.Text = "一括更新";
            IsUpdateAllConfig.UseVisualStyleBackColor = true;
            // 
            // ButtonAutoCreate2
            // 
            ButtonAutoCreate2.Location = new Point(644, 65);
            ButtonAutoCreate2.Name = "ButtonAutoCreate2";
            ButtonAutoCreate2.Size = new Size(115, 28);
            ButtonAutoCreate2.TabIndex = 14;
            ButtonAutoCreate2.Text = "SVG一括処理";
            ButtonAutoCreate2.UseVisualStyleBackColor = true;
            ButtonAutoCreate2.Click += CreateItemsFromSvg;
            // 
            // ButtonTest
            // 
            ButtonTest.Location = new Point(644, 23);
            ButtonTest.Name = "ButtonTest";
            ButtonTest.Size = new Size(115, 28);
            ButtonTest.TabIndex = 8;
            ButtonTest.Text = "プリント更新";
            ButtonTest.UseVisualStyleBackColor = true;
            ButtonTest.Click += UpdateSlide;
            // 
            // LabelStatus
            // 
            LabelStatus.AutoSize = true;
            LabelStatus.Location = new Point(29, 11);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(0, 15);
            LabelStatus.TabIndex = 5;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView);
            tabPage2.Controls.Add(RakutenItemName);
            tabPage2.Controls.Add(RakutenRegister);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(RakutenHtml);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1018, 578);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "R";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // RakutenItemName
            // 
            RakutenItemName.Font = new Font("Yu Gothic UI", 11F);
            RakutenItemName.FormattingEnabled = true;
            RakutenItemName.Location = new Point(19, 30);
            RakutenItemName.Name = "RakutenItemName";
            RakutenItemName.Size = new Size(984, 28);
            RakutenItemName.TabIndex = 24;
            // 
            // RakutenRegister
            // 
            RakutenRegister.Font = new Font("Yu Gothic UI", 11F);
            RakutenRegister.Location = new Point(19, 544);
            RakutenRegister.Name = "RakutenRegister";
            RakutenRegister.Size = new Size(984, 28);
            RakutenRegister.TabIndex = 23;
            RakutenRegister.Text = "登録";
            RakutenRegister.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 9F);
            label2.Location = new Point(19, 60);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 22;
            label2.Text = "HTML";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 9F);
            label1.Location = new Point(19, 14);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 21;
            label1.Text = "ItemName";
            // 
            // RakutenHtml
            // 
            RakutenHtml.Location = new Point(19, 78);
            RakutenHtml.Multiline = true;
            RakutenHtml.Name = "RakutenHtml";
            RakutenHtml.Size = new Size(984, 84);
            RakutenHtml.TabIndex = 19;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1018, 578);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "A";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // IsJsonUpdate
            // 
            IsJsonUpdate.AutoSize = true;
            IsJsonUpdate.Location = new Point(864, 27);
            IsJsonUpdate.Name = "IsJsonUpdate";
            IsJsonUpdate.Size = new Size(59, 19);
            IsJsonUpdate.TabIndex = 41;
            IsJsonUpdate.Text = "上書き";
            IsJsonUpdate.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1050, 630);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button ButtonSyncSlide;
        private Label LabelStatus;
        private Button ButtonSvgSync;
        private Button ButtonCreateHtml;
        private Button ButtonTest;
        private Button ButtonAll;
        private Button ButtonFtpSyncPage;
        private Button ButtonAutoCreateItems;
        private TabPage tabPage3;
        private ComboBox RakutenItemName;
        private Button RakutenRegister;
        private Label label2;
        private Label label1;
        private TextBox RakutenHtml;
        private Button ButtonCreateIndex;
        private Button ButtonAutoCreate2;
        private CheckBox IsUpdateAll;
        private CheckBox IsOnlyTest;
        private ProgressBar progressBar1;
        private CheckBox IsCreateImage;
        private CheckBox IsCreatePdf;
        private CheckBox IsCreateHtml;
        private CheckBox IsFtpUpload;
        private Label ProgressLabel;
        private CheckBox IsUpdateConfig;
        private Button button2;
        private CheckBox IsExportSvg;
        private ComboBox FactoryClasses;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox IsUpdateAllConfig;
        private CheckBox IsUpdateAllHtml;
        private CheckBox IsUpdateAllPdf;
        private CheckBox IsUpdateAllImage;
        private CheckBox IsExportSvgFromSlide;
        private CheckBox IsUpdateSlide;
        private CheckBox IsJsonUpdate;
    }
}
