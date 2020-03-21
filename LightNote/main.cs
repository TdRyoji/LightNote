using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightNote
{
    class TextPage : TabPage
    {
        public RichTextBox Note { get; set; }

        public TextPage()
        {
            this.Note = new RichTextBox();
            this.Note.Multiline = true;
            this.Note.AcceptsTab = true;
            this.Note.WordWrap = false;
            this.Note.Dock = DockStyle.Fill;
            this.Note.Parent = this;
        }
    }

    partial class LightNote : Form
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new LightNote());
        }

        private FlowLayoutPanel panel = new FlowLayoutPanel();
        private TabControl tab = new TabControl();
        private List<TextPage> pages = new List<TextPage>();
        private int max_index = -1;

        public LightNote()
        {
            this.SetDesktopBounds(100, 50, 450, 400);
            this.Text = "Light Note";

            this.setSize();
            this.createPage();

            panel.Parent = this;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Controls.Add(this.tab);

            this.SizeChanged += (object sender, EventArgs e) =>
            {
                this.setSize();
            };
        }
    }
}
