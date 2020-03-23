using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LightNote
{
    partial class LightNote
    {
        private void setSize()
        {
            this.panel.Height = this.ClientSize.Height;
            this.panel.Width = this.ClientSize.Width;
            this.tab.Height = this.panel.Height - 56;
            this.tab.Width = this.panel.Width;
        }

        public TextPage SelectedPage()
        {
            return this.tab.SelectedTab as TextPage;
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

        #region File
        private void open()
        {
            var _open = new OpenFileDialog();
            _open.Filter = "テキストファイル | *.txt";

            if(_open.ShowDialog() == DialogResult.OK)
            {
                this.createPage();
                var _page = this.SelectedPage();

                using (var _reader =
                    new StreamReader(_open.FileName, Encoding.Default))
                {
                    _page.Note.Text = _reader.ReadToEnd();
                }

                _page.Text = Path.GetFileNameWithoutExtension(_open.FileName);
                _page.FullPath = Path.GetFullPath(_open.FileName);
            }
        }

        private void saveAs(TextPage _page)
        {
            var _save = new SaveFileDialog();

            _save.FileName = _page.Text;
            _save.Filter = "テキストファイル | *.txt";
            _save.Title = _page.Text + "を保存";

            if(_save.ShowDialog() == DialogResult.OK)
            {
                using (var _writer = new StreamWriter(_save.FileName))
                {
                    _writer.WriteLine(_page.Note.Text);
                }
            }

            _page.Text = Path.GetFileNameWithoutExtension(_save.FileName);
            _page.FullPath = Path.GetFullPath(_save.FileName);
        }

        private void save(TextPage _page)
        {
            if(_page.FullPath == null)
            {
                this.saveAs(_page);
                return;
            }

            File.WriteAllText(_page.FullPath, _page.Note.Text);
        }

        private void saveAll()
        {
            foreach(var p in this.pages)
            {
                this.save(p);
            }
        }
        #endregion

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
    }
}