namespace MovieApp
{
    partial class AddMovie
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
            this.txtMovieName = new System.Windows.Forms.TextBox();
            this.txtMovieCategory = new System.Windows.Forms.TextBox();
            this.txtMovieIMDB = new System.Windows.Forms.TextBox();
            this.txtMovieURL = new System.Windows.Forms.TextBox();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.pictureBoxSelectedImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(917, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Film Ekle";
            // 
            // txtMovieName
            // 
            this.txtMovieName.Location = new System.Drawing.Point(757, 190);
            this.txtMovieName.Name = "txtMovieName";
            this.txtMovieName.Size = new System.Drawing.Size(272, 26);
            this.txtMovieName.TabIndex = 1;
            // 
            // txtMovieCategory
            // 
            this.txtMovieCategory.Location = new System.Drawing.Point(757, 303);
            this.txtMovieCategory.Name = "txtMovieCategory";
            this.txtMovieCategory.Size = new System.Drawing.Size(272, 26);
            this.txtMovieCategory.TabIndex = 2;
            // 
            // txtMovieIMDB
            // 
            this.txtMovieIMDB.Location = new System.Drawing.Point(757, 245);
            this.txtMovieIMDB.Name = "txtMovieIMDB";
            this.txtMovieIMDB.Size = new System.Drawing.Size(272, 26);
            this.txtMovieIMDB.TabIndex = 3;
            // 
            // txtMovieURL
            // 
            this.txtMovieURL.Location = new System.Drawing.Point(757, 343);
            this.txtMovieURL.Name = "txtMovieURL";
            this.txtMovieURL.Size = new System.Drawing.Size(272, 26);
            this.txtMovieURL.TabIndex = 4;
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddMovie.Location = new System.Drawing.Point(624, 458);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(405, 91);
            this.btnAddMovie.TabIndex = 7;
            this.btnAddMovie.Text = "Film Ekle";
            this.btnAddMovie.UseVisualStyleBackColor = true;
            this.btnAddMovie.Click += new System.EventHandler(this.btnAddMovie_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(620, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Film Adı:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(620, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Film IMDB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(620, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Film Kategori";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(620, 349);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Film URL:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(620, 393);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Film Görseli";
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(757, 393);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(272, 33);
            this.btnSelectImage.TabIndex = 14;
            this.btnSelectImage.Text = "Görsel Seç";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // pictureBoxSelectedImage
            // 
            this.pictureBoxSelectedImage.Location = new System.Drawing.Point(1072, 190);
            this.pictureBoxSelectedImage.Name = "pictureBoxSelectedImage";
            this.pictureBoxSelectedImage.Size = new System.Drawing.Size(300, 236);
            this.pictureBoxSelectedImage.TabIndex = 15;
            this.pictureBoxSelectedImage.TabStop = false;
            // 
            // AddMovie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1972, 1048);
            this.Controls.Add(this.pictureBoxSelectedImage);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddMovie);
            this.Controls.Add(this.txtMovieURL);
            this.Controls.Add(this.txtMovieIMDB);
            this.Controls.Add(this.txtMovieCategory);
            this.Controls.Add(this.txtMovieName);
            this.Controls.Add(this.label1);
            this.Name = "AddMovie";
            this.Text = "AddMovie";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMovieName;
        private System.Windows.Forms.TextBox txtMovieCategory;
        private System.Windows.Forms.TextBox txtMovieIMDB;
        private System.Windows.Forms.TextBox txtMovieURL;
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.PictureBox pictureBoxSelectedImage;
    }
}