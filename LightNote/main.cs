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

        private MenuStrip menu = new MenuStrip();

        public LightNote()
        {
            this.SetDesktopBounds(100, 50, 450, 400);
            this.Text = "Light Note";

            this.setSize();
            this.createPage();

            panel.Parent = this;
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Controls.Add(this.menu);
            panel.Controls.Add(this.tab);

            #region Menu
            ToolStripMenuItem
                m_file = new ToolStripMenuItem("ファイル(F)"),
                m_edit = new ToolStripMenuItem("編集(E)"),
                m_view = new ToolStripMenuItem("表示(V)");

            m_file.ShortcutKeys = Keys.Alt | Keys.F;
            m_edit.ShortcutKeys = Keys.Alt | Keys.E;
            m_view.ShortcutKeys = Keys.Alt | Keys.V;

            this.menu.Items.Add(m_file);
            this.menu.Items.Add(m_edit);
            this.menu.Items.Add(m_view);
            #endregion

            #region Event
            this.SizeChanged += (object sender, EventArgs e) =>
            {
                this.setSize();
            };
            #endregion
        }
    }
}
