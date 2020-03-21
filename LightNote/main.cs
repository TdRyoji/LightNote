using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

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
        private ToolStrip tool = new ToolStrip();
        private ToolStripButton
            b_new, b_open, b_save, b_saveAll, b_print, b_close,
            b_undo, b_redo, b_cut, b_copy, b_paste, b_delete,
            b_font, b_color, b_justL, b_just, b_justR;
        private Image
            i_new, i_open, i_save, i_saveAll, i_print, i_close,
            i_undo, i_redo, i_cut, i_copy, i_paste, i_delete,
            i_font, i_color, i_justL, i_just, i_justR;

        public LightNote()
        {
            #region Setup
            this.SetDesktopBounds(100, 50, 450, 400);
            this.Text = "Light Note";

            this.setSize();
            this.createPage();

            panel.Parent = this;
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Controls.Add(this.menu);
            panel.Controls.Add(this.tool);
            panel.Controls.Add(this.tab);
            #endregion

            #region Image
            i_new = Properties.Resources.newfile;
            i_open = Properties.Resources.close;
            i_save = Properties.Resources.save;
            i_saveAll = Properties.Resources.saveall;
            i_print = Properties.Resources.print;
            i_close = Properties.Resources.close;
            i_undo = Properties.Resources.Undo;
            i_redo = Properties.Resources.Redo;
            i_cut = Properties.Resources.cut;
            i_copy = Properties.Resources.copy;
            i_paste = Properties.Resources.paste;
            i_delete = Properties.Resources.delete;
            i_font = Properties.Resources.font;
            i_color = Properties.Resources.fontcolor;
            i_justL = Properties.Resources.lines_left;
            i_just = Properties.Resources.lines_centered;
            i_justR = Properties.Resources.lines_right;
            #endregion

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

            #region Tool
            #region Definition
            b_new = new ToolStripButton(this.i_new);
            b_open = new ToolStripButton(this.i_open);
            b_save = new ToolStripButton(this.i_save);
            b_saveAll = new ToolStripButton(this.i_saveAll);
            b_print = new ToolStripButton(this.i_print);
            b_close = new ToolStripButton(this.i_close);
            b_undo = new ToolStripButton(this.i_undo);
            b_redo = new ToolStripButton(this.i_redo);
            b_cut = new ToolStripButton(this.i_cut);
            b_copy = new ToolStripButton(this.i_copy);
            b_paste = new ToolStripButton(this.i_paste);
            b_delete = new ToolStripButton(this.i_delete);
            b_font = new ToolStripButton(this.i_font);
            b_color = new ToolStripButton(this.i_color);
            b_justL = new ToolStripButton(this.i_just);
            b_just = new ToolStripButton(this.i_justL);
            b_justR = new ToolStripButton(this.i_justR);

            this.tool.Items.Add(b_new);
            this.tool.Items.Add(b_open);
            this.tool.Items.Add(b_save);
            this.tool.Items.Add(b_saveAll);
            this.tool.Items.Add(b_print);
            this.tool.Items.Add(b_close);
            this.tool.Items.Add(new ToolStripSeparator());
            this.tool.Items.Add(b_undo);
            this.tool.Items.Add(b_redo);
            this.tool.Items.Add(new ToolStripSeparator());
            this.tool.Items.Add(b_cut);
            this.tool.Items.Add(b_copy);
            this.tool.Items.Add(b_paste);
            this.tool.Items.Add(b_delete);
            this.tool.Items.Add(new ToolStripSeparator());
            this.tool.Items.Add(b_font);
            this.tool.Items.Add(b_color);
            this.tool.Items.Add(new ToolStripSeparator());
            this.tool.Items.Add(b_justL);
            this.tool.Items.Add(b_just);
            this.tool.Items.Add(b_justR);
            #endregion
            #region Text and Event
            b_new.ToolTipText = "新規作成 (Ctrl+N)";
            b_new.Click += (object sender, EventArgs e) =>
            {

            };
            b_open.ToolTipText = "開く (Ctrl+O)";
            b_open.Click += (object sender, EventArgs e) =>
            {

            };
            b_save.ToolTipText = "保存 (Ctrl+S)";
            b_save.Click += (object sender, EventArgs e) =>
            {

            };
            b_saveAll.ToolTipText = "全て保存 (Ctrl+L)";
            b_saveAll.Click += (object sender, EventArgs e) =>
            {

            };
            b_print.ToolTipText = "印刷 (Ctrl+P)";
            b_print.Click += (object sender, EventArgs e) =>
            {

            };
            b_close.ToolTipText = "閉じる(Alt+C)";
            b_close.Click += (object sender, EventArgs e) =>
            {

            };
            b_undo.ToolTipText = "元に戻す (Ctrl+Z)";
            b_undo.Click += (object sender, EventArgs e) =>
            {

            };
            b_redo.ToolTipText = "やり直し (Ctrl+Y)";
            b_redo.Click += (object sender, EventArgs e) =>
            {

            };
            b_cut.ToolTipText = "切り取り (Ctrl+X)";
            b_cut.Click += (object sender, EventArgs e) =>
            {

            };
            b_copy.ToolTipText = "コピー (Ctrl+C)";
            b_copy.Click += (object sender, EventArgs e) =>
            {

            };
            b_paste.ToolTipText = "貼り付け (Ctrl+V)";
            b_paste.Click += (object sender, EventArgs e) =>
            {

            };
            b_delete.ToolTipText = "削除 (Del)";
            b_delete.Click += (object sender, EventArgs e) =>
            {

            };
            b_font.ToolTipText = "フォント (Ctrl+F1)";
            b_font.Click += (object sender, EventArgs e) =>
            {

            };
            b_color.ToolTipText = "カラー (Ctrl+F2)";
            b_color.Click += (object sender, EventArgs e) =>
            {

            };
            b_justL.ToolTipText = "左揃え (Ctrl+Shift+L)";
            b_justL.Click += (object sender, EventArgs e) =>
            {

            };
            b_just.ToolTipText = "中央揃え (Ctrl+Shift+J)";
            b_just.Click += (object sender, EventArgs e) =>
            {

            };
            b_justR.ToolTipText = "右揃え (Ctrl+Shift+R)";
            b_justR.Click += (object sender, EventArgs e) =>
            {

            };
            #endregion
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
