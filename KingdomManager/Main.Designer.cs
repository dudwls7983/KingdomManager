namespace KingdomManager
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkButton = new System.Windows.Forms.Button();
            this.appPlayerList = new System.Windows.Forms.ComboBox();
            this.targetList = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.produceTab = new System.Windows.Forms.TabPage();
            this.developTab = new System.Windows.Forms.TabPage();
            this.devRefreshButton = new System.Windows.Forms.Button();
            this.devRichBox = new System.Windows.Forms.RichTextBox();
            this.devPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.testButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.developTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.devPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // linkButton
            // 
            this.linkButton.Location = new System.Drawing.Point(123, 58);
            this.linkButton.Name = "linkButton";
            this.linkButton.Size = new System.Drawing.Size(100, 23);
            this.linkButton.TabIndex = 2;
            this.linkButton.Text = "링크";
            this.linkButton.UseVisualStyleBackColor = true;
            this.linkButton.Click += new System.EventHandler(this.OnLinkButtonClicked);
            // 
            // appPlayerList
            // 
            this.appPlayerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appPlayerList.FormattingEnabled = true;
            this.appPlayerList.Location = new System.Drawing.Point(123, 6);
            this.appPlayerList.Name = "appPlayerList";
            this.appPlayerList.Size = new System.Drawing.Size(100, 20);
            this.appPlayerList.TabIndex = 4;
            this.appPlayerList.SelectedIndexChanged += new System.EventHandler(this.OnAppPlayerListSelectionChanged);
            // 
            // targetList
            // 
            this.targetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetList.FormattingEnabled = true;
            this.targetList.Location = new System.Drawing.Point(123, 32);
            this.targetList.Name = "targetList";
            this.targetList.Size = new System.Drawing.Size(100, 20);
            this.targetList.TabIndex = 5;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.mainTab);
            this.tabControl.Controls.Add(this.produceTab);
            this.tabControl.Controls.Add(this.developTab);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1560, 837);
            this.tabControl.TabIndex = 6;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.label2);
            this.mainTab.Controls.Add(this.label1);
            this.mainTab.Controls.Add(this.appPlayerList);
            this.mainTab.Controls.Add(this.linkButton);
            this.mainTab.Controls.Add(this.targetList);
            this.mainTab.Location = new System.Drawing.Point(4, 22);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(1552, 811);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "일반";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "타겟";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "앱플레이어";
            // 
            // produceTab
            // 
            this.produceTab.Location = new System.Drawing.Point(4, 22);
            this.produceTab.Name = "produceTab";
            this.produceTab.Padding = new System.Windows.Forms.Padding(3);
            this.produceTab.Size = new System.Drawing.Size(1552, 811);
            this.produceTab.TabIndex = 1;
            this.produceTab.Text = "생산";
            this.produceTab.UseVisualStyleBackColor = true;
            // 
            // developTab
            // 
            this.developTab.Controls.Add(this.testButton);
            this.developTab.Controls.Add(this.devRefreshButton);
            this.developTab.Controls.Add(this.devRichBox);
            this.developTab.Controls.Add(this.devPictureBox);
            this.developTab.Location = new System.Drawing.Point(4, 22);
            this.developTab.Name = "developTab";
            this.developTab.Padding = new System.Windows.Forms.Padding(3);
            this.developTab.Size = new System.Drawing.Size(1552, 811);
            this.developTab.TabIndex = 2;
            this.developTab.Text = "개발자메뉴";
            this.developTab.UseVisualStyleBackColor = true;
            // 
            // devRefreshButton
            // 
            this.devRefreshButton.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.devRefreshButton.Location = new System.Drawing.Point(6, 166);
            this.devRefreshButton.Name = "devRefreshButton";
            this.devRefreshButton.Size = new System.Drawing.Size(150, 69);
            this.devRefreshButton.TabIndex = 3;
            this.devRefreshButton.Text = "갱신";
            this.devRefreshButton.UseVisualStyleBackColor = true;
            this.devRefreshButton.Click += new System.EventHandler(this.OnDevRefreshButtonClicked);
            // 
            // devRichBox
            // 
            this.devRichBox.Location = new System.Drawing.Point(6, 6);
            this.devRichBox.Name = "devRichBox";
            this.devRichBox.ReadOnly = true;
            this.devRichBox.Size = new System.Drawing.Size(150, 154);
            this.devRichBox.TabIndex = 2;
            this.devRichBox.Text = "";
            // 
            // devPictureBox
            // 
            this.devPictureBox.Location = new System.Drawing.Point(162, 3);
            this.devPictureBox.Name = "devPictureBox";
            this.devPictureBox.Size = new System.Drawing.Size(800, 450);
            this.devPictureBox.TabIndex = 1;
            this.devPictureBox.TabStop = false;
            this.devPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnDevPictureBoxClicked);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1552, 811);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(6, 241);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(150, 39);
            this.testButton.TabIndex = 4;
            this.testButton.Text = "TestButton";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.OnTestButtonClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.tabControl);
            this.Name = "Main";
            this.Text = "KingdomManager";
            this.tabControl.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            this.developTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.devPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button linkButton;
        private System.Windows.Forms.ComboBox appPlayerList;
        private System.Windows.Forms.ComboBox targetList;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage produceTab;
        private System.Windows.Forms.TabPage developTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button devRefreshButton;
        private System.Windows.Forms.RichTextBox devRichBox;
        private System.Windows.Forms.PictureBox devPictureBox;
        private System.Windows.Forms.Button testButton;
    }
}

