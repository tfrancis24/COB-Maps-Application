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
               System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
               this.inputButton = new System.Windows.Forms.Button();
               this.fileNameTextBox = new System.Windows.Forms.TextBox();
               this.cancelButton = new System.Windows.Forms.Button();
               this.mapButton = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // inputButton
               // 
               resources.ApplyResources(this.inputButton, "inputButton");
               this.inputButton.Name = "inputButton";
               this.inputButton.UseVisualStyleBackColor = false;
               this.inputButton.Click += new System.EventHandler(this.inputButton_Click);
               // 
               // fileNameTextBox
               // 
               resources.ApplyResources(this.fileNameTextBox, "fileNameTextBox");
               this.fileNameTextBox.Name = "fileNameTextBox";
               // 
               // cancelButton
               // 
               resources.ApplyResources(this.cancelButton, "cancelButton");
               this.cancelButton.Name = "cancelButton";
               this.cancelButton.UseMnemonic = false;
               this.cancelButton.UseVisualStyleBackColor = true;
               this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
               // 
               // mapButton
               // 
               resources.ApplyResources(this.mapButton, "mapButton");
               this.mapButton.Name = "mapButton";
               this.mapButton.UseVisualStyleBackColor = true;
               this.mapButton.Click += new System.EventHandler(this.mapButton_Click);
               // 
               // Form1
               // 
               resources.ApplyResources(this, "$this");
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.mapButton);
               this.Controls.Add(this.cancelButton);
               this.Controls.Add(this.fileNameTextBox);
               this.Controls.Add(this.inputButton);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
               this.Name = "Form1";
               this.Load += new System.EventHandler(this.Form1_Load);
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Button inputButton;
          private System.Windows.Forms.TextBox fileNameTextBox;
          private System.Windows.Forms.Button cancelButton;
          private System.Windows.Forms.Button mapButton;
     }
}

