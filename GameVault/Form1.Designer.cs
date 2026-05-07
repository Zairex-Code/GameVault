namespace GameVault
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
            dgvGames = new DataGridView();
            txtTitle = new TextBox();
            txtGenre = new TextBox();
            txtPrice = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGames).BeginInit();
            SuspendLayout();
            // 
            // dgvGames
            // 
            dgvGames.AllowUserToAddRows = false;
            dgvGames.AllowUserToDeleteRows = false;
            dgvGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGames.Location = new Point(562, 27);
            dgvGames.Name = "dgvGames";
            dgvGames.Size = new Size(748, 586);
            dgvGames.TabIndex = 0;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(53, 179);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(446, 23);
            txtTitle.TabIndex = 1;
            // 
            // txtGenre
            // 
            txtGenre.Location = new Point(53, 233);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(446, 23);
            txtGenre.TabIndex = 1;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(53, 290);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(446, 23);
            txtPrice.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 154);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 2;
            label1.Text = "Title";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 215);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 2;
            label2.Text = "Genre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(53, 272);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 2;
            label3.Text = "Price";
            // 
            // button1
            // 
            button1.Location = new Point(53, 329);
            button1.Name = "button1";
            button1.Size = new Size(446, 29);
            button1.TabIndex = 3;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(53, 364);
            button2.Name = "button2";
            button2.Size = new Size(446, 29);
            button2.TabIndex = 3;
            button2.Text = "Delete";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1334, 636);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPrice);
            Controls.Add(txtGenre);
            Controls.Add(txtTitle);
            Controls.Add(dgvGames);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvGames).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvGames;
        private TextBox txtTitle;
        private TextBox txtGenre;
        private TextBox txtPrice;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
    }
}
