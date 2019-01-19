namespace GoogleMapsFormApp
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
               this.inputButton = new System.Windows.Forms.Button();
               this.textBox1 = new System.Windows.Forms.TextBox();
               this.cancelButton = new System.Windows.Forms.Button();
               this.mapButton = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // inputButton
               // 
               this.inputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
               this.inputButton.Location = new System.Drawing.Point(37, 30);
               this.inputButton.Name = "inputButton";
               this.inputButton.Size = new System.Drawing.Size(145, 23);
               this.inputButton.TabIndex = 0;
               this.inputButton.Text = "Select CSV File";
               this.inputButton.UseVisualStyleBackColor = false;
               // 
               // textBox1
               // 
               this.textBox1.Location = new System.Drawing.Point(37, 70);
               this.textBox1.Name = "textBox1";
               this.textBox1.Size = new System.Drawing.Size(414, 20);
               this.textBox1.TabIndex = 1;
               // 
               // cancelButton
               // 
               this.cancelButton.Location = new System.Drawing.Point(223, 138);
               this.cancelButton.Name = "cancelButton";
               this.cancelButton.Size = new System.Drawing.Size(95, 23);
               this.cancelButton.TabIndex = 2;
               this.cancelButton.Text = "Cancel";
               this.cancelButton.UseMnemonic = false;
               this.cancelButton.UseVisualStyleBackColor = true;
               // 
               // mapButton
               // 
               this.mapButton.Location = new System.Drawing.Point(354, 138);
               this.mapButton.Name = "mapButton";
               this.mapButton.Size = new System.Drawing.Size(97, 23);
               this.mapButton.TabIndex = 3;
               this.mapButton.Text = "Go";
               this.mapButton.UseVisualStyleBackColor = true;
               this.mapButton.Click += new System.EventHandler(this.mapButton_Click);
               // 
               // Form1
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.AutoSize = true;
               this.ClientSize = new System.Drawing.Size(486, 173);
               this.Controls.Add(this.mapButton);
               this.Controls.Add(this.cancelButton);
               this.Controls.Add(this.textBox1);
               this.Controls.Add(this.inputButton);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
               this.Name = "Form1";
               this.Text = "Form1";
               this.Load += new System.EventHandler(this.Form1_Load);
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Button inputButton;
          private System.Windows.Forms.TextBox textBox1;
          private System.Windows.Forms.Button cancelButton;
          private System.Windows.Forms.Button mapButton;
     }
}

