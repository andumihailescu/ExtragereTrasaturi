namespace ExtragereaTrasaturilor
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
            processDataBtn = new Button();
            chooseFolderCb = new ComboBox();
            statusLabel = new Label();
            SuspendLayout();
            // 
            // processDataBtn
            // 
            processDataBtn.Location = new Point(62, 115);
            processDataBtn.Name = "processDataBtn";
            processDataBtn.Size = new Size(150, 50);
            processDataBtn.TabIndex = 0;
            processDataBtn.Text = "Process Data";
            processDataBtn.UseVisualStyleBackColor = true;
            processDataBtn.Click += processDataBtn_Click;
            // 
            // chooseFolderCb
            // 
            chooseFolderCb.FormattingEnabled = true;
            chooseFolderCb.Items.AddRange(new object[] { "Testing + Training", "Large Data Set" });
            chooseFolderCb.Location = new Point(62, 64);
            chooseFolderCb.Name = "chooseFolderCb";
            chooseFolderCb.Size = new Size(150, 28);
            chooseFolderCb.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(79, 179);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 20);
            statusLabel.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 253);
            Controls.Add(statusLabel);
            Controls.Add(chooseFolderCb);
            Controls.Add(processDataBtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button processDataBtn;
        private ComboBox chooseFolderCb;
        private Label statusLabel;
    }
}