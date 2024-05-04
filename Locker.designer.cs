namespace LockerLock
{
    partial class Locker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Locker));
            this.pnIconos = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnIconos
            // 
            this.pnIconos.AllowDrop = true;
            this.pnIconos.BackColor = System.Drawing.Color.Transparent;
            this.pnIconos.BackgroundImage = global::LockerLock.Properties.Resources.lockerlock200x200;
            this.pnIconos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnIconos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnIconos.Location = new System.Drawing.Point(0, 44);
            this.pnIconos.Name = "pnIconos";
            this.pnIconos.Size = new System.Drawing.Size(1017, 649);
            this.pnIconos.TabIndex = 0;
            this.pnIconos.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.pnIconos.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            // 
            // Locker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1017, 693);
            this.Controls.Add(this.pnIconos);
            this.Name = "Locker";
            this.Text = "Locker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Locker_FormClosing);
            this.Load += new System.EventHandler(this.Locker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnIconos;
    }
}