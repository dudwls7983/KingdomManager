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
            this.startButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.produceTab = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeProductionButton = new System.Windows.Forms.Button();
            this.productUpButton = new System.Windows.Forms.Button();
            this.productDownButton = new System.Windows.Forms.Button();
            this.addProductButton = new System.Windows.Forms.Button();
            this.buildingProducts = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buildingLevel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buildingState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buildingList = new System.Windows.Forms.ListBox();
            this.developTab = new System.Windows.Forms.TabPage();
            this.testButton = new System.Windows.Forms.Button();
            this.devRefreshButton = new System.Windows.Forms.Button();
            this.devRichBox = new System.Windows.Forms.RichTextBox();
            this.devPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.produceTab.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingProducts)).BeginInit();
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
            this.mainTab.Controls.Add(this.startButton);
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
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(6, 87);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(217, 87);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "실행";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.OnStartButtonClicked);
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
            this.produceTab.BackColor = System.Drawing.SystemColors.Control;
            this.produceTab.Controls.Add(this.panel1);
            this.produceTab.Controls.Add(this.buildingList);
            this.produceTab.Location = new System.Drawing.Point(4, 22);
            this.produceTab.Name = "produceTab";
            this.produceTab.Padding = new System.Windows.Forms.Padding(3);
            this.produceTab.Size = new System.Drawing.Size(1552, 811);
            this.produceTab.TabIndex = 1;
            this.produceTab.Text = "생산";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.removeProductionButton);
            this.panel1.Controls.Add(this.productUpButton);
            this.panel1.Controls.Add(this.productDownButton);
            this.panel1.Controls.Add(this.addProductButton);
            this.panel1.Controls.Add(this.buildingProducts);
            this.panel1.Controls.Add(this.buildingLevel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buildingState);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(388, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 376);
            this.panel1.TabIndex = 1;
            // 
            // removeProductionButton
            // 
            this.removeProductionButton.Location = new System.Drawing.Point(112, 246);
            this.removeProductionButton.Name = "removeProductionButton";
            this.removeProductionButton.Size = new System.Drawing.Size(75, 30);
            this.removeProductionButton.TabIndex = 9;
            this.removeProductionButton.Text = "제거";
            this.removeProductionButton.UseVisualStyleBackColor = true;
            this.removeProductionButton.Click += new System.EventHandler(this.OnRemoveProductButtonClicked);
            // 
            // productUpButton
            // 
            this.productUpButton.Location = new System.Drawing.Point(112, 72);
            this.productUpButton.Name = "productUpButton";
            this.productUpButton.Size = new System.Drawing.Size(75, 30);
            this.productUpButton.TabIndex = 8;
            this.productUpButton.Text = "▲";
            this.productUpButton.UseVisualStyleBackColor = true;
            // 
            // productDownButton
            // 
            this.productDownButton.Location = new System.Drawing.Point(112, 144);
            this.productDownButton.Name = "productDownButton";
            this.productDownButton.Size = new System.Drawing.Size(75, 30);
            this.productDownButton.TabIndex = 7;
            this.productDownButton.Text = "▼";
            this.productDownButton.UseVisualStyleBackColor = true;
            // 
            // addProductButton
            // 
            this.addProductButton.Location = new System.Drawing.Point(112, 108);
            this.addProductButton.Name = "addProductButton";
            this.addProductButton.Size = new System.Drawing.Size(75, 30);
            this.addProductButton.TabIndex = 5;
            this.addProductButton.Text = "추가";
            this.addProductButton.UseVisualStyleBackColor = true;
            this.addProductButton.Click += new System.EventHandler(this.OnAddProductButtonClicked);
            // 
            // buildingProducts
            // 
            this.buildingProducts.AllowUserToAddRows = false;
            this.buildingProducts.AllowUserToDeleteRows = false;
            this.buildingProducts.AllowUserToResizeColumns = false;
            this.buildingProducts.AllowUserToResizeRows = false;
            this.buildingProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.buildingProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.min,
            this.max});
            this.buildingProducts.Location = new System.Drawing.Point(193, 9);
            this.buildingProducts.MultiSelect = false;
            this.buildingProducts.Name = "buildingProducts";
            this.buildingProducts.RowTemplate.Height = 23;
            this.buildingProducts.Size = new System.Drawing.Size(383, 364);
            this.buildingProducts.TabIndex = 4;
            this.buildingProducts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnBuildingProductsCellEditEnded);
            this.buildingProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.OnBuildingProductsDataError);
            // 
            // name
            // 
            this.name.FillWeight = 80F;
            this.name.HeaderText = "아이템";
            this.name.Name = "name";
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.name.Width = 80;
            // 
            // min
            // 
            this.min.FillWeight = 130F;
            this.min.HeaderText = "최소 유지 개수";
            this.min.Name = "min";
            this.min.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.min.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.min.Width = 130;
            // 
            // max
            // 
            this.max.FillWeight = 130F;
            this.max.HeaderText = "최대 유지 개수";
            this.max.Name = "max";
            this.max.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.max.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.max.Width = 130;
            // 
            // buildingLevel
            // 
            this.buildingLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.buildingLevel.FormattingEnabled = true;
            this.buildingLevel.Location = new System.Drawing.Point(66, 46);
            this.buildingLevel.Name = "buildingLevel";
            this.buildingLevel.Size = new System.Drawing.Size(121, 20);
            this.buildingLevel.TabIndex = 3;
            this.buildingLevel.SelectedIndexChanged += new System.EventHandler(this.OnBuildingLevelSelectionChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "레벨";
            // 
            // buildingState
            // 
            this.buildingState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.buildingState.FormattingEnabled = true;
            this.buildingState.Items.AddRange(new object[] {
            "Normal",
            "Upgrading",
            "Not Yet"});
            this.buildingState.Location = new System.Drawing.Point(66, 9);
            this.buildingState.Name = "buildingState";
            this.buildingState.Size = new System.Drawing.Size(121, 20);
            this.buildingState.TabIndex = 1;
            this.buildingState.SelectedIndexChanged += new System.EventHandler(this.OnBuildingStateSelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "상태";
            // 
            // buildingList
            // 
            this.buildingList.BackColor = System.Drawing.SystemColors.Window;
            this.buildingList.FormattingEnabled = true;
            this.buildingList.ItemHeight = 12;
            this.buildingList.Location = new System.Drawing.Point(6, 6);
            this.buildingList.Name = "buildingList";
            this.buildingList.Size = new System.Drawing.Size(376, 376);
            this.buildingList.TabIndex = 0;
            this.buildingList.SelectedIndexChanged += new System.EventHandler(this.OnBuildingListSelectionChanged);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.tabControl);
            this.Name = "Main";
            this.Text = "KingdomManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnApplicationClosing);
            this.tabControl.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            this.produceTab.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingProducts)).EndInit();
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
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ListBox buildingList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox buildingState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox buildingLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView buildingProducts;
        private System.Windows.Forms.Button productUpButton;
        private System.Windows.Forms.Button productDownButton;
        private System.Windows.Forms.Button addProductButton;
        private System.Windows.Forms.Button removeProductionButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn min;
        private System.Windows.Forms.DataGridViewTextBoxColumn max;
    }
}

