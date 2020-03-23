using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace LightNote
{
    class TextPage : TabPage
    {
        public RichTextBox Note { get; set; }
        public string FullPath { get; set; }

        public TextPage()
        {
            this.Note = new RichTextBox();
            this.Note.Multiline = true;
            this.Note.AcceptsTab = true;
            this.Note.WordWrap = false;
            this.Note.Dock = DockStyle.Fill;
            this.Note.Parent = this;

            this.FullPath = null;
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
        private ToolStripMenuItem
            m_new, m_open, m_close, m_closeNew, m_closeOpen,
            m_save, m_saveAs, m_saveAll, m_exit,
            m_undo, m_redo, m_cut, m_copy, m_paste, m_delete,
            m_font, m_color, m_justL, m_just, m_justR;
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
            #region Images
            i_new = Properties.Resources.newfile;
            i_open = Properties.Resources.open;
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
            #region Topitems
            ToolStripMenuItem
                m_file = new ToolStripMenuItem("ファイル(F)"),
                m_edit = new ToolStripMenuItem("編集(E)"),
                m_option = new ToolStripMenuItem("書式(O)");

            this.menu.Items.Add(m_file);
            this.menu.Items.Add(m_edit);
            this.menu.Items.Add(m_option);

            m_file.ShortcutKeys = Keys.Alt | Keys.F;
            m_edit.ShortcutKeys = Keys.Alt | Keys.E;
            m_option.ShortcutKeys = Keys.Alt | Keys.O;
            #endregion

            #region File
            #region Declaration
            m_new = new ToolStripMenuItem("新規作成", this.i_new);
            m_open = new ToolStripMenuItem("開く", this.i_open);
            m_close = new ToolStripMenuItem("閉じる", this.i_close);
            m_closeNew = new ToolStripMenuItem("閉じて新規作成");
            m_closeOpen = new ToolStripMenuItem("閉じて開く");
            m_save = new ToolStripMenuItem("現在のテキストファイルを上書き保存", this.i_save);
            m_saveAs = new ToolStripMenuItem("現在のテキストファイルに名前を付けて保存");
            m_saveAll = new ToolStripMenuItem("全て保存", this.i_saveAll);
            m_exit = new ToolStripMenuItem("終了");

            m_file.DropDownItems.Add(m_new);
            m_file.DropDownItems.Add(m_open);
            m_file.DropDownItems.Add(new ToolStripSeparator());
            m_file.DropDownItems.Add(m_close);
            m_file.DropDownItems.Add(m_closeNew);
            m_file.DropDownItems.Add(m_closeOpen);
            m_file.DropDownItems.Add(new ToolStripSeparator());
            m_file.DropDownItems.Add(m_save);
            m_file.DropDownItems.Add(m_saveAs);
            m_file.DropDownItems.Add(m_saveAll);
            m_file.DropDownItems.Add(new ToolStripSeparator());
            m_file.DropDownItems.Add(m_exit);
            #endregion
            #region ShortcutKeys and Events
            m_new.ShortcutKeys = Keys.Control | Keys.N;
            m_open.ShortcutKeys = Keys.Control | Keys.O;
            m_close.ShortcutKeys = Keys.Alt | Keys.C;
            m_closeNew.ShortcutKeys = Keys.Alt | Keys.R;
            m_closeOpen.ShortcutKeys = Keys.Alt | Keys.O;
            m_save.ShortcutKeys = Keys.Control | Keys.S;
            m_saveAs.ShortcutKeys = Keys.Control | Keys.A;
            m_saveAll.ShortcutKeys = Keys.Control | Keys.L;
            m_exit.ShortcutKeys = Keys.Alt | Keys.X;

            m_new.Click += (object sender, EventArgs e) => this.createPage();
            m_open.Click += (object sender, EventArgs e) => this.open();
            m_close.Click += (object sender, EventArgs e) => this.closeonly();
            m_closeNew.Click += (object sender, EventArgs e) => this.closeNew();
            m_closeOpen.Click += (object sender, EventArgs e) => this.closeOpen();
            m_save.Click += (object sender, EventArgs e) => this.save(this.SelectedPage());
            m_saveAs.Click += (object sender, EventArgs e) => this.saveAs(this.SelectedPage());
            m_saveAll.Click += (object sender, EventArgs e) => this.saveAll();
            m_exit.Click += (object sender, EventArgs e) => this.Close();
            #endregion
            #endregion

            #region Edit
            #region Declaration
            m_undo = new ToolStripMenuItem("元に戻す", this.i_undo);
            m_redo = new ToolStripMenuItem("やり直し", this.i_redo);
            m_cut = new ToolStripMenuItem("切り取り", this.i_cut);
            m_copy = new ToolStripMenuItem("コピー", this.i_copy);
            m_paste = new ToolStripMenuItem("貼り付け", this.i_paste);
            m_delete = new ToolStripMenuItem("削除", this.i_delete);

            m_edit.DropDownItems.Add(m_undo);
            m_edit.DropDownItems.Add(m_redo);
            m_edit.DropDownItems.Add(new ToolStripSeparator());
            m_edit.DropDownItems.Add(m_cut);
            m_edit.DropDownItems.Add(m_copy);
            m_edit.DropDownItems.Add(m_paste);
            m_edit.DropDownItems.Add(m_delete);
            #endregion
            #region ShortcutKeys and Events
            m_undo.ShortcutKeys = Keys.Control | Keys.Z;
            m_redo.ShortcutKeys = Keys.Control | Keys.Y;
            m_cut.ShortcutKeys = Keys.Control | Keys.X;
            m_copy.ShortcutKeys = Keys.Control | Keys.C;
            m_paste.ShortcutKeys = Keys.Control | Keys.V;
            m_delete.ShortcutKeys = Keys.Delete;

            m_undo.Click += (object sender, EventArgs e) => this.undo();
            m_redo.Click += (object sender, EventArgs e) => this.redo();
            m_cut.Click += (object sender, EventArgs e) => this.cut();
            m_copy.Click += (object sender, EventArgs e) => this.copy();
            m_paste.Click += (object sender, EventArgs e) => this.paste();
            m_delete.Click += (object sender, EventArgs e) => this.delete();
            #endregion
            #endregion

            #region Option
            #region Declaration
            m_font = new ToolStripMenuItem("フォント", this.i_font);
            m_color = new ToolStripMenuItem("カラー", this.i_color);
            m_justL = new ToolStripMenuItem("左揃え", this.i_justL);
            m_just = new ToolStripMenuItem("中央揃え", this.i_just);
            m_justR = new ToolStripMenuItem("右揃え", this.i_justR);

            m_option.DropDownItems.Add(m_font);
            m_option.DropDownItems.Add(m_color);
            m_option.DropDownItems.Add(new ToolStripSeparator());
            m_option.DropDownItems.Add(m_justL);
            m_option.DropDownItems.Add(m_just);
            m_option.DropDownItems.Add(m_justR);
            #endregion
            #region ShortcutKeys and Events
            m_font.ShortcutKeys = Keys.Control | Keys.F1;
            m_color.ShortcutKeys = Keys.Control | Keys.F2;
            m_justL.ShortcutKeys = Keys.Control | Keys.Shift | Keys.L;
            m_just.ShortcutKeys = Keys.Control | Keys.Shift | Keys.J;
            m_justR.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;

            m_font.Click += (object sender, EventArgs e) =>
            {

            };
            m_color.Click += (object sender, EventArgs e) =>
            {

            };
            m_justL.Click += (object sender, EventArgs e) =>
            {

            };
            m_just.Click += (object sender, EventArgs e) =>
            {

            };
            m_justR.Click += (object sender, EventArgs e) =>
            {

            };
            #endregion
            #endregion
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
            #region Text and Events
            b_new.ToolTipText = "新規作成 (Ctrl+N)";
            b_new.Click += (object sender, EventArgs e) => this.createPage();

            b_open.ToolTipText = "開く (Ctrl+O)";
            b_open.Click += (object sender, EventArgs e) => this.open();

            b_save.ToolTipText = "保存 (Ctrl+S)";
            b_save.Click += (object sender, EventArgs e) => this.save(this.SelectedPage());

            b_saveAll.ToolTipText = "全て保存 (Ctrl+L)";
            b_saveAll.Click += (object sender, EventArgs e) => this.saveAll();

            b_print.ToolTipText = "印刷 (Ctrl+P)";
            b_print.Click += (object sender, EventArgs e) => { };

            b_close.ToolTipText = "閉じる(Alt+C)";
            b_close.Click += (object sender, EventArgs e) => this.closeonly();

            b_undo.ToolTipText = "元に戻す (Ctrl+Z)";
            b_undo.Click += (object sender, EventArgs e) =>
            {
                this.undo();
            };
            b_redo.ToolTipText = "やり直し (Ctrl+Y)";
            b_redo.Click += (object sender, EventArgs e) =>
            {
                this.redo();
            };
            b_cut.ToolTipText = "切り取り (Ctrl+X)";
            b_cut.Click += (object sender, EventArgs e) =>
            {
                this.cut();
            };
            b_copy.ToolTipText = "コピー (Ctrl+C)";
            b_copy.Click += (object sender, EventArgs e) =>
            {
                this.copy();
            };
            b_paste.ToolTipText = "貼り付け (Ctrl+V)";
            b_paste.Click += (object sender, EventArgs e) =>
            {
                this.paste();
            };
            b_delete.ToolTipText = "削除 (Del)";
            b_delete.Click += (object sender, EventArgs e) =>
            {
                this.delete();
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
            this.SizeChanged += (object sender, EventArgs e) => this.setSize();
            #endregion
        }
    }
}
