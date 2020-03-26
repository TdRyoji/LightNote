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
            _open.Filter = "テキストファイル | *.txt|リッチテキストドキュメント| *.rtf";
            _open.Multiselect = true;

            if(_open.ShowDialog() == DialogResult.OK)
            {
                foreach(var _fn in _open.FileNames)
                {
                    this.createPage();
                    var _page = this.SelectedPage();

                    switch (_open.FilterIndex)
                    {
                        case 1:
                            using (var _reader =
                                new StreamReader(_fn, Encoding.Default))
                            {
                                _page.Note.Text = _reader.ReadToEnd();
                            }
                            break;
                        case 2:
                            try
                            {
                                _page.Note.LoadFile(_fn, RichTextBoxStreamType.RichText);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message, "問題が発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        default: break;
                    }

                    _page.Text = Path.GetFileNameWithoutExtension(_fn);
                    _page.FullPath = Path.GetFullPath(_fn);
                }
            }
        }

        private void saveAs(TextPage _page)
        {
            var _save = new SaveFileDialog();

            _save.FileName = _page.Text;
            _save.Filter = "テキストファイル | *.txt|リッチテキストドキュメント| *.rtf";
            _save.Title = _page.Text + "を保存";

            if(_save.ShowDialog() == DialogResult.OK)
            {
                switch (_save.FilterIndex)
                {
                    case 1:
                        using (var _writer = new StreamWriter(_save.FileName))
                        {
                            _writer.WriteLine(_page.Note.Text);
                        }
                        break;
                    case 2:
                        _page.Note.SaveFile(_save.FileName, RichTextBoxStreamType.RichText);
                        break;
                    default:
                        return;
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

        private bool closeDialog(TextPage _page)
        {
            switch(MessageBox.Show("保存しますか？", _page.Text + "の保存",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    this.save(_page);
                    return true;
                case DialogResult.No:
                    return true;
                default:
                    return false;
            }
        }
        private void deletePage()
        {
            var _page = this.SelectedPage();
            int _index = this.tab.SelectedIndex;
            if(this.closeDialog(_page))
            {
                this.tab.Controls.RemoveAt(_index);
                this.pages.RemoveAt(_index);
                this.max_index--;
                _page.Dispose();
                this.tab.SelectedIndex = (_index == 0 ? _index : _index - 1);
            }
        }
        private void closeonly()
        {
            if (this.max_index == 0) this.Close();
            else this.deletePage();
        }
        private void closeNew()
        {
            this.deletePage();
            this.createPage();
        }
        private void closeOpen()
        {
            this.deletePage();
            this.open();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            switch(MessageBox.Show("編集中のファイルを保存せずに終了しますか？", "確認",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.No:
                    this.saveAll();
                    break;
                default:
                    break;
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