using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LightNote
{
    partial class LightNote
    {
        private void setSize()
        {
            this.panel.Height = this.ClientSize.Height;
            this.panel.Width = this.ClientSize.Width;
            this.tab.Height = this.panel.Height - 60;
            this.tab.Width = this.panel.Width;
        }

        private void createPage()
        {
            this.max_index++;
            var _page = new TextPage();
            _page.Text = "No Title" + (this.max_index + 1).ToString();

            this.pages.Add(_page);
            this.tab.Controls.Add(_page);
            this.tab.SelectedIndex = this.max_index;
        }

        public TextPage SelectedPage()
        {
            return this.tab.SelectedTab as TextPage;
        }

        #region Edit
        private void undo()
        {
            this.SelectedPage().Note.Undo();
        }
        private void redo()
        {
            this.SelectedPage().Note.Redo();
        }
        private void cut()
        {
            this.SelectedPage().Note.Cut();
        }
        private void copy()
        {
            this.SelectedPage().Note.Copy();
        }
        private void paste()
        {
            this.SelectedPage().Note.Paste();
        }
        private void delete()
        {
            var _page = this.SelectedPage();
            _page.Note.SelectAll();
            SendKeys.Send("\b");
        }
        #endregion

        #region Option
        private void font()
        {
            var _page = this.SelectedPage();
            var _font = new FontDialog();
            _font.ShowDialog();

            _page.Note.SelectionFont = _font.Font;
        }
        private void fontcolor()
        {
            var _page = this.SelectedPage();
            var _col = new ColorDialog();
            _col.ShowDialog();

            _page.Note.SelectionColor = _col.Color;
        }
        private void just_centered()
        {
            this.SelectedPage().Note.SelectionAlignment
                = HorizontalAlignment.Center;
        }
        private void just_left()
        {
            this.SelectedPage().Note.SelectionAlignment
                = HorizontalAlignment.Left;
        }
        private void just_right()
        {
            this.SelectedPage().Note.SelectionAlignment
                = HorizontalAlignment.Right;
        }
        #endregion
    }
}