namespace FlatUI.Examples
{
	partial class BasicExample
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
			this.FormSkin = new FlatUI.FormSkin();
			this.ProgressButton = new FlatUI.FlatButton();
			this.FlatMini = new FlatUI.FlatMini();
			this.FlatClose = new FlatUI.FlatClose();
			this.ProgressCheckBox = new FlatUI.FlatCheckBox();
			this.FlatProgressBar = new FlatUI.FlatProgressBar();
			this.FlatLabel = new FlatUI.FlatLabel();
			this.FormSkin.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormSkin
			// 
			this.FormSkin.BackColor = System.Drawing.Color.White;
			this.FormSkin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
			this.FormSkin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
			this.FormSkin.Controls.Add(this.FlatLabel);
			this.FormSkin.Controls.Add(this.ProgressButton);
			this.FormSkin.Controls.Add(this.FlatMini);
			this.FormSkin.Controls.Add(this.FlatClose);
			this.FormSkin.Controls.Add(this.ProgressCheckBox);
			this.FormSkin.Controls.Add(this.FlatProgressBar);
			this.FormSkin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FormSkin.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.FormSkin.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
			this.FormSkin.HeaderMaximize = false;
			this.FormSkin.Location = new System.Drawing.Point(0, 0);
			this.FormSkin.Name = "FormSkin";
			this.FormSkin.Size = new System.Drawing.Size(520, 262);
			this.FormSkin.TabIndex = 0;
			this.FormSkin.Text = "FlatUI Example";
			// 
			// ProgressButton
			// 
			this.ProgressButton.BackColor = System.Drawing.Color.Transparent;
			this.ProgressButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
			this.ProgressButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ProgressButton.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.ProgressButton.Location = new System.Drawing.Point(12, 185);
			this.ProgressButton.Name = "ProgressButton";
			this.ProgressButton.Rounded = false;
			this.ProgressButton.Size = new System.Drawing.Size(152, 29);
			this.ProgressButton.TabIndex = 0;
			this.ProgressButton.Text = "Progress";
			this.ProgressButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			this.ProgressButton.Click += new System.EventHandler(this.FlatButton_Click);
			// 
			// FlatMini
			// 
			this.FlatMini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FlatMini.BackColor = System.Drawing.Color.White;
			this.FlatMini.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
			this.FlatMini.Font = new System.Drawing.Font("Marlett", 12F);
			this.FlatMini.Location = new System.Drawing.Point(466, 12);
			this.FlatMini.Name = "FlatMini";
			this.FlatMini.Size = new System.Drawing.Size(18, 18);
			this.FlatMini.TabIndex = 5;
			this.FlatMini.Text = "Minimize";
			this.FlatMini.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			// 
			// FlatClose
			// 
			this.FlatClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FlatClose.BackColor = System.Drawing.Color.White;
			this.FlatClose.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.FlatClose.Font = new System.Drawing.Font("Marlett", 10F);
			this.FlatClose.Location = new System.Drawing.Point(490, 12);
			this.FlatClose.Name = "FlatClose";
			this.FlatClose.Size = new System.Drawing.Size(18, 18);
			this.FlatClose.TabIndex = 4;
			this.FlatClose.Text = "Exit";
			this.FlatClose.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			// 
			// ProgressCheckBox
			// 
			this.ProgressCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
			this.ProgressCheckBox.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
			this.ProgressCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
			this.ProgressCheckBox.Checked = false;
			this.ProgressCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ProgressCheckBox.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.ProgressCheckBox.Location = new System.Drawing.Point(170, 192);
			this.ProgressCheckBox.Name = "ProgressCheckBox";
			this.ProgressCheckBox.Options = FlatUI.FlatCheckBox._Options.Style1;
			this.ProgressCheckBox.Size = new System.Drawing.Size(125, 22);
			this.ProgressCheckBox.TabIndex = 2;
			this.ProgressCheckBox.Text = "Progress slowly";
			// 
			// FlatProgressBar
			// 
			this.FlatProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
			this.FlatProgressBar.DarkerProgress = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(148)))), ((int)(((byte)(92)))));
			this.FlatProgressBar.Location = new System.Drawing.Point(0, 220);
			this.FlatProgressBar.Maximum = 100;
			this.FlatProgressBar.Name = "FlatProgressBar";
			this.FlatProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
			this.FlatProgressBar.Size = new System.Drawing.Size(520, 42);
			this.FlatProgressBar.TabIndex = 1;
			this.FlatProgressBar.Text = "Progress";
			this.FlatProgressBar.Value = 0;
			// 
			// FlatLabel
			// 
			this.FlatLabel.AutoSize = true;
			this.FlatLabel.BackColor = System.Drawing.Color.Transparent;
			this.FlatLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
			this.FlatLabel.ForeColor = System.Drawing.Color.White;
			this.FlatLabel.Location = new System.Drawing.Point(12, 60);
			this.FlatLabel.Name = "FlatLabel";
			this.FlatLabel.Size = new System.Drawing.Size(214, 13);
			this.FlatLabel.TabIndex = 6;
			this.FlatLabel.Text = "This is an example program using FlatUI.";
			// 
			// BasicExample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(520, 262);
			this.Controls.Add(this.FormSkin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "BasicExample";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FlatUI Example";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.FormSkin.ResumeLayout(false);
			this.FormSkin.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private FormSkin FormSkin;
		private FlatButton ProgressButton;
		private FlatProgressBar FlatProgressBar;
		private FlatCheckBox ProgressCheckBox;
		private FlatMini FlatMini;
		private FlatClose FlatClose;
		private FlatLabel FlatLabel;
	}
}

