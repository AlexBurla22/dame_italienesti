namespace dame_italienesti
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
            this.label1 = new System.Windows.Forms.Label();
            this.randPictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPieseNegre = new System.Windows.Forms.Label();
            this.labelPieseAlbe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.randPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(702, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rand joc:";
            // 
            // randPictureBox
            // 
            this.randPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.randPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.randPictureBox.Location = new System.Drawing.Point(724, 294);
            this.randPictureBox.Name = "randPictureBox";
            this.randPictureBox.Size = new System.Drawing.Size(60, 60);
            this.randPictureBox.TabIndex = 1;
            this.randPictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(688, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numar piese negru";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(703, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Numar piese alb";
            // 
            // labelPieseNegre
            // 
            this.labelPieseNegre.AutoSize = true;
            this.labelPieseNegre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPieseNegre.Location = new System.Drawing.Point(739, 98);
            this.labelPieseNegre.Name = "labelPieseNegre";
            this.labelPieseNegre.Size = new System.Drawing.Size(63, 24);
            this.labelPieseNegre.TabIndex = 4;
            this.labelPieseNegre.Text = "Negre";
            // 
            // labelPieseAlbe
            // 
            this.labelPieseAlbe.AutoSize = true;
            this.labelPieseAlbe.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPieseAlbe.Location = new System.Drawing.Point(739, 485);
            this.labelPieseAlbe.Name = "labelPieseAlbe";
            this.labelPieseAlbe.Size = new System.Drawing.Size(49, 24);
            this.labelPieseAlbe.TabIndex = 5;
            this.labelPieseAlbe.Text = "Albe";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(843, 670);
            this.Controls.Add(this.labelPieseAlbe);
            this.Controls.Add(this.labelPieseNegre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.randPictureBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Joc Dame Italienesti";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.randPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox randPictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPieseNegre;
        private System.Windows.Forms.Label labelPieseAlbe;
    }
}

