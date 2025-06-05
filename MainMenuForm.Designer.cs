using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MusicalTiles_
{
    partial class MainMenuForm
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 400);
            Name = "MainMenuForm";
            Text = "Musical Tiles - Меню";
            ResumeLayout(false);
        }
    }
}
