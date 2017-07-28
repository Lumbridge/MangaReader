namespace MangaReader.Forms
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_Right = new System.Windows.Forms.Button();
            this.button_Left = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(494, 553);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_Right
            // 
            this.button_Right.Location = new System.Drawing.Point(407, 518);
            this.button_Right.Name = "button_Right";
            this.button_Right.Size = new System.Drawing.Size(75, 23);
            this.button_Right.TabIndex = 1;
            this.button_Right.Text = "Backward";
            this.button_Right.UseVisualStyleBackColor = true;
            this.button_Right.Click += new System.EventHandler(this.button_Right_Click);
            // 
            // button_Left
            // 
            this.button_Left.Location = new System.Drawing.Point(12, 518);
            this.button_Left.Name = "button_Left";
            this.button_Left.Size = new System.Drawing.Size(75, 23);
            this.button_Left.TabIndex = 2;
            this.button_Left.Text = "Forward";
            this.button_Left.UseVisualStyleBackColor = true;
            this.button_Left.Click += new System.EventHandler(this.button_Left_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 553);
            this.Controls.Add(this.button_Left);
            this.Controls.Add(this.button_Right);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Manga Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Right;
        private System.Windows.Forms.Button button_Left;
    }
}

