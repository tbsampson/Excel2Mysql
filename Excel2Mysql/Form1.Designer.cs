namespace Excel2Mysql
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.listBoxExcel = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textFolder = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textServer = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.textUser = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textCurrentFile = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.comboBoxQuery = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTickerBegin = new System.Windows.Forms.DateTimePicker();
            this.dateTickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(544, 85);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(124, 28);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Browse";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(544, 130);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(124, 30);
            this.buttonConvert.TabIndex = 1;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // listBoxExcel
            // 
            this.listBoxExcel.FormattingEnabled = true;
            this.listBoxExcel.Location = new System.Drawing.Point(15, 130);
            this.listBoxExcel.MultiColumn = true;
            this.listBoxExcel.Name = "listBoxExcel";
            this.listBoxExcel.Size = new System.Drawing.Size(524, 108);
            this.listBoxExcel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Folder";
            // 
            // textFolder
            // 
            this.textFolder.Location = new System.Drawing.Point(54, 89);
            this.textFolder.Name = "textFolder";
            this.textFolder.Size = new System.Drawing.Size(484, 20);
            this.textFolder.TabIndex = 4;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(544, 41);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(124, 28);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect Mysql";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textServer
            // 
            this.textServer.Location = new System.Drawing.Point(54, 43);
            this.textServer.Name = "textServer";
            this.textServer.Size = new System.Drawing.Size(100, 20);
            this.textServer.TabIndex = 7;
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(194, 45);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(44, 20);
            this.textPort.TabIndex = 8;
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(300, 45);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(82, 20);
            this.textUser.TabIndex = 9;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(451, 45);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(87, 20);
            this.textPassword.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(394, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Password";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 248);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(523, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // textCurrentFile
            // 
            this.textCurrentFile.Location = new System.Drawing.Point(544, 249);
            this.textCurrentFile.Name = "textCurrentFile";
            this.textCurrentFile.ReadOnly = true;
            this.textCurrentFile.Size = new System.Drawing.Size(124, 20);
            this.textCurrentFile.TabIndex = 12;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(5, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(630, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Please confirm whether \"dbals\" database exists in MySQL Server before you connect" +
    " to MySQL, Create it.";
            // 
            // buttonQuery
            // 
            this.buttonQuery.Location = new System.Drawing.Point(544, 286);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(124, 30);
            this.buttonQuery.TabIndex = 14;
            this.buttonQuery.Text = "Execute";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // comboBoxQuery
            // 
            this.comboBoxQuery.FormattingEnabled = true;
            this.comboBoxQuery.Items.AddRange(new object[] {
            "Show Avl for each day for each product",
            "Show number of times an item Avl was negative"});
            this.comboBoxQuery.Location = new System.Drawing.Point(72, 291);
            this.comboBoxQuery.Name = "comboBoxQuery";
            this.comboBoxQuery.Size = new System.Drawing.Size(248, 21);
            this.comboBoxQuery.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "QueryType";
            // 
            // dateTickerBegin
            // 
            this.dateTickerBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTickerBegin.Location = new System.Drawing.Point(326, 292);
            this.dateTickerBegin.Name = "dateTickerBegin";
            this.dateTickerBegin.Size = new System.Drawing.Size(96, 20);
            this.dateTickerBegin.TabIndex = 17;
            // 
            // dateTickerEnd
            // 
            this.dateTickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTickerEnd.Location = new System.Drawing.Point(442, 292);
            this.dateTickerEnd.Name = "dateTickerEnd";
            this.dateTickerEnd.Size = new System.Drawing.Size(96, 20);
            this.dateTickerEnd.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(426, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "~";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 325);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dateTickerEnd);
            this.Controls.Add(this.dateTickerBegin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxQuery);
            this.Controls.Add(this.buttonQuery);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textCurrentFile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textServer);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxExcel);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.buttonOpen);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Converter xls to mysql";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.ListBox listBoxExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFolder;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textServer;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.TextBox textUser;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textCurrentFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.ComboBox comboBoxQuery;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTickerBegin;
        private System.Windows.Forms.DateTimePicker dateTickerEnd;
        private System.Windows.Forms.Label label8;
    }
}

