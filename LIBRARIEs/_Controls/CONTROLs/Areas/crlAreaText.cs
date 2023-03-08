using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlAreaText'
    /// </summary>
    /// <remarks>Область для формирования почтового сообщения</remarks>
    public class crlAreaText : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            __cToolBar.Items.Insert(0, _cButtonSave);
            __cToolBar.Items.Insert(0, _cButtonOpen);
            __cToolBar.Items.Add(_cSeparator1);
            __cToolBar.Items.Add(_cButtonAlignLeft);
            __cToolBar.Items.Add(_cButtonAlignCenter);
            __cToolBar.Items.Add(_cButtonAlignRight);
            __cToolBar.Items.Add(_cSeparator2);
            __cToolBar.Items.Add(_cButtonFontBold);
            __cToolBar.Items.Add(_cButtonFontItalic);
            __cToolBar.Items.Add(_cButtonFontUnderline);
            __cToolBar.Items.Add(_cButtonFontStrikethrough);
            __cToolBar.Items.Add(_cSeparator3);
            __cToolBar.Items.Add(_cButtonScriptSuper);
            __cToolBar.Items.Add(_cButtonScriptSub);
            __cToolBar.Items.Add(_cSeparator4);
            __cToolBar.Items.Add(_cButtonFont);
            __cToolBar.Items.Add(_cButtonColor);

            Panel2.Controls.Add(_cText);
            Panel2.Controls.SetChildIndex(_cText, 0);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            // __cToolBar
            {
                // _cButtonOpen
                {
                    _cButtonOpen.Image = global::nlResourcesImages.Properties.Resources._Folder_y32C;
                    _cButtonOpen.Click += _cButtonOpen_Click;
                }
                // _cButtonSave
                {
                    _cButtonSave.Image = global::nlResourcesImages.Properties.Resources._Diskette_b32C;
                    _cButtonSave.Click += _cButtonSave_Click;
                }
                // _cButtonAlignLeft
                {
                    _cButtonAlignLeft.Image = global::nlResourcesImages.Properties.Resources._TextAlignLeft_a32C;
                    _cButtonAlignLeft.Click += _cButtonAlignLeft_Click;
                }
                // _cButtonAlignCenter
                {
                    _cButtonAlignCenter.Image = global::nlResourcesImages.Properties.Resources._TextAlignCenter_a32C;
                    _cButtonAlignCenter.Click += _cButtonAlignCenter_Click;
                }
                // _cButtonAlignRight
                {
                    _cButtonAlignRight.Image = global::nlResourcesImages.Properties.Resources._TextAlignRight_a32C;
                    _cButtonAlignRight.Click += _cButtonAlignRight_Click;
                }
                // _cButtonFontBold
                {
                    _cButtonFontBold.Image = global::nlResourcesImages.Properties.Resources._FontBold_a32C;
                    _cButtonFontBold.Click += _cButtonFontBold_Click;
                }
                // _cButtonFontItalic
                {
                    _cButtonFontItalic.Image = global::nlResourcesImages.Properties.Resources._FontItalic_a32C;
                    _cButtonFontItalic.Click += _cButtonFontItalic_Click;
                }
                // _cButtonFontUnderlined
                {
                    _cButtonFontUnderline.Image = global::nlResourcesImages.Properties.Resources._FontUnderline_a32C;
                    _cButtonFontUnderline.Click += _cButtonFontUnderlined_Click;
                }
                // _cButtonFontStrikethrough
                {
                    _cButtonFontStrikethrough.Image = global::nlResourcesImages.Properties.Resources._FontStrikethroungh_a32C;
                    _cButtonFontStrikethrough.Click += _cButtonFontStrikethrough_Click;
                }
                // _cButtonScriptSuper
                {
                    _cButtonScriptSuper.Image = global::nlResourcesImages.Properties.Resources._FontScriptSuper_a32C;
                    _cButtonScriptSuper.Click += _cButtonScriptSuper_Click;
                }
                // _cButtonScriptSub
                {
                    _cButtonScriptSub.Image = global::nlResourcesImages.Properties.Resources._FontScriptSub_a32C;
                    _cButtonScriptSub.Click += _cButtonScriptSub_Click;
                }
                // _cButtonFont
                {
                    _cButtonFont.Image = global::nlResourcesImages.Properties.Resources._Font_b32C;
                    _cButtonFont.Click += _cButtonFont_Click;
                }
                // _cButtonColor
                {
                    _cButtonColor.Image = global::nlResourcesImages.Properties.Resources._FontColor_32C;
                    _cButtonColor.Click += _cButtonColor_Click;
                }
            }
            // _cText
            {
                _cText.Dock = DockStyle.Fill;
            }

            #endregion Настойка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Объект

        #region Панель инструментов

        private void _cButtonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if ((vOpenFileDialog.ShowDialog() == DialogResult.OK) && (vOpenFileDialog.FileName.Length > 0))
                {
                    string vExtension = System.IO.Path.GetExtension(vOpenFileDialog.FileName).ToLower();

                    if (vExtension == ".rtf")
                    {
                        _cText.LoadFile(vOpenFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else if (vExtension == ".txt")
                    {
                        _cText.LoadFile(vOpenFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    }
                    else if ((vExtension == ".htm") || (vExtension == ".html"))
                    {
                        // Read the HTML format
                        StreamReader vStreamReader = File.OpenText(vOpenFileDialog.FileName);
                        string strHTML = vStreamReader.ReadToEnd();
                        vStreamReader.Close();

                        _cText.AddHTML(strHTML);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There was an error loading the file: " + vOpenFileDialog.FileName);
            }
        }

        private void _cButtonSave_Click(object sender, EventArgs e)
        {
            if ((vSaveFileDialog.ShowDialog() == DialogResult.OK) && (vSaveFileDialog.FileName.Length > 0))
            {
                string strExt = System.IO.Path.GetExtension(vSaveFileDialog.FileName).ToLower();

                if (strExt == ".rtf")
                {
                    _cText.SaveFile(vSaveFileDialog.FileName);
                }
                else if (strExt == ".txt")
                {
                    _cText.SaveFile(vSaveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                else if ((strExt == ".htm") || (strExt == ".html"))
                {
                    try
                    {
                        // save as HTML format
                        string strText = _cText.GetHTML(true, true);

                        if (File.Exists(vSaveFileDialog.FileName))
                            File.Delete(vSaveFileDialog.FileName);

                        StreamWriter sr = File.CreateText(vSaveFileDialog.FileName);
                        sr.Write(strText);
                        sr.Close();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error saving the file: " + vSaveFileDialog.FileName);
                    }
                }
            }
        }

        private void _cButtonAlignLeft_Click(object sender, EventArgs e)
        {
            _cText.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void _cButtonAlignCenter_Click(object sender, EventArgs e)
        {
            _cText.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void _cButtonAlignRight_Click(object sender, EventArgs e)
        {
            _cText.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void _cButtonFontBold_Click(object sender, EventArgs e)
        {
            if (_cText.SelectionFont != null)
            {
                _cText.SelectionFont = new Font(_cText.SelectionFont, _cText.SelectionFont.Style ^ FontStyle.Bold);
            }
        }

        private void _cButtonFontItalic_Click(object sender, EventArgs e)
        {
            if (_cText.SelectionFont != null)
            {
                _cText.SelectionFont = new Font(_cText.SelectionFont, _cText.SelectionFont.Style ^ FontStyle.Italic);
            }
        }

        private void _cButtonFontUnderlined_Click(object sender, EventArgs e)
        {
            if (_cText.SelectionFont != null)
            {
                _cText.SelectionFont = new Font(_cText.SelectionFont, _cText.SelectionFont.Style ^ FontStyle.Underline);
            }
        }

        private void _cButtonFontStrikethrough_Click(object sender, EventArgs e)
        {
            if (_cText.SelectionFont != null)
            {
                _cText.SelectionFont = new Font(_cText.SelectionFont, _cText.SelectionFont.Style ^ FontStyle.Strikeout);
            }
        }

        private void _cButtonScriptSuper_Click(object sender, EventArgs e)
        {
            if (_cButtonScriptSuper.Checked)
            {
                _cText.SetSuperScript(true);
                _cText.SetSubScript(false);
            }
            else
            {
                _cText.SetSuperScript(false);
            }
        }

        private void _cButtonScriptSub_Click(object sender, EventArgs e)
        {
            if (_cButtonScriptSub.Checked)
            {
                _cText.SetSubScript(true);
                _cText.SetSuperScript(false);
            }
            else
            {
                _cText.SetSubScript(false);
            }
        }

        private void _cButtonFont_Click(object sender, EventArgs e)
        {
            if (_cText.SelectionFont != null)
                vFontDialog.Font = _cText.SelectionFont;
            else
                vFontDialog.Font = _cText.Font;

            if (vFontDialog.ShowDialog() == DialogResult.OK)
            {
                if (_cText.SelectionFont != null)
                    _cText.SelectionFont = vFontDialog.Font;
                else
                    _cText.Font = vFontDialog.Font;
            }
        }

        private void _cButtonColor_Click(object sender, EventArgs e)
        {
            vColorDialog.Color = _cText.SelectionColor;

            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                _cText.SelectionColor = vColorDialog.Color;
            }
        }

        #endregion Панель инструментов

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="pFilePath">Путь к файлу</param>
        /// <returns></returns>
        public bool __mFileLoad(string pFilePath)
        {
            bool vReturn = true; // Возвращаемое значение
            _cText.LoadFile(pFilePath);
            return vReturn;
        }
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="pFilePath">Путь к файлу</param>
        /// <returns></returns>
        public bool __mFileSave(string pFilePath)
        {
            bool vReturn = true; // Возвращаемое значение
            _cText.SaveFile(pFilePath);
            return vReturn;
        }

        #endregion Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Путь к редактируемому файлу
        /// </summary>
        public string __fFilePath = "";

        #endregion Атрибуты

        #region - Компоненты

        /// <summary>
        /// Кнопка 'Открыть'
        /// </summary>
        protected crlComponentToolBarButton _cButtonOpen = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Сохранить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSave = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Привязка текста к левому краю'
        /// </summary>
        protected crlComponentToolBarButton _cButtonAlignLeft = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Привязка текста по центру'
        /// </summary>
        protected crlComponentToolBarButton _cButtonAlignCenter = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Привязка текста к правому краю'
        /// </summary>
        protected crlComponentToolBarButton _cButtonAlignRight = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Толстый шрифт'
        /// </summary>
        protected crlComponentToolBarButton _cButtonFontBold = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Наклонный шрифт'
        /// </summary>
        protected crlComponentToolBarButton _cButtonFontItalic = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Подчеркнутый шрифт'
        /// </summary>
        protected crlComponentToolBarButton _cButtonFontUnderline = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Зачеркнутый шрифт'
        /// </summary>
        protected crlComponentToolBarButton _cButtonFontStrikethrough = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Верхний регистр'
        /// </summary>
        protected crlComponentToolBarButton _cButtonScriptSuper = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Нижний регистр'
        /// </summary>
        protected crlComponentToolBarButton _cButtonScriptSub = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Шрифт'
        /// </summary>
        protected crlComponentToolBarButton _cButtonFont = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Цвет'
        /// </summary>
        protected crlComponentToolBarButton _cButtonColor = new crlComponentToolBarButton();

        protected ToolStripSeparator _cSeparator1 = new ToolStripSeparator();
        protected ToolStripSeparator _cSeparator2 = new ToolStripSeparator();
        protected ToolStripSeparator _cSeparator3 = new ToolStripSeparator();
        protected ToolStripSeparator _cSeparator4 = new ToolStripSeparator();

        /// <summary>
        /// Копонент 'Ввод текста'
        /// </summary>
        protected crlComponentText _cText = new crlComponentText();

        #endregion Компоненты

        #region - Объекты

        OpenFileDialog vOpenFileDialog = new OpenFileDialog();
        SaveFileDialog vSaveFileDialog = new SaveFileDialog();
        FontDialog vFontDialog = new FontDialog();
        ColorDialog vColorDialog = new ColorDialog();

        #endregion Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        #region Доступность кнопок управления

        /// <summary>
        /// Доступность кнопки 'Сохранить'
        /// </summary>
        public bool __fButtonSaveEnabled_
        {
            get { return _cButtonSave.Enabled; }
            set { _cButtonSave.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Открыть'
        /// </summary>
        public bool __fButtonOpenEnabled_
        {
            get { return _cButtonOpen.Enabled; }
            set { _cButtonOpen.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Привязка текста к левому краю'
        /// </summary>
        public bool __fButtonAlignLeftEnabled_
        {
            get { return _cButtonAlignLeft.Enabled; }
            set { _cButtonAlignLeft.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Привязка текста по центру'
        /// </summary>
        public bool __fButtonAlignCenterEnabled_
        {
            get { return _cButtonAlignCenter.Enabled; }
            set { _cButtonAlignCenter.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Привязка текста к правому краю'
        /// </summary>
        public bool __fButtonAlignRightEnabled_
        {
            get { return _cButtonAlignRight.Enabled; }
            set { _cButtonAlignRight.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Толстый шрифт'
        /// </summary>
        public bool __fButtonFontBoldEnabled_
        {
            get { return _cButtonFontBold.Enabled; }
            set { _cButtonFontBold.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Наклонный шрифт'
        /// </summary>
        public bool __fButtonFontItalicEnabled_
        {
            get { return _cButtonFontItalic.Enabled; }
            set { _cButtonFontItalic.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Подчеркнутый шрифт'
        /// </summary>
        public bool __fButtonFontUnderlineEnabled_
        {
            get { return _cButtonFontUnderline.Enabled; }
            set { _cButtonFontUnderline.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Верхний регистр'
        /// </summary>
        public bool __fButtonScriptSuperEnabled_
        {
            get { return _cButtonScriptSuper.Enabled; }
            set { _cButtonScriptSuper.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Нижний регистр'
        /// </summary>
        public bool __fButtonScriptSubEnabled_
        {
            get { return _cButtonScriptSub.Enabled; }
            set { _cButtonScriptSub.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Шрифт'
        /// </summary>
        public bool __fButtonFontEnabled_
        {
            get { return _cButtonFont.Enabled; }
            set { _cButtonFont.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Цвет'
        /// </summary>
        public bool __fButtonColorEnabled_
        {
            get { return _cButtonColor.Enabled; }
            set { _cButtonColor.Enabled = value; }
        }

        #endregion Доступность кнопок управления

        #region Видимость кнопок управления

        /// <summary>
        /// Видимость кнопки 'Сохранить'
        /// </summary>
        public bool __fButtonSaveVisible_
        {
            get { return _cButtonSave.Visible; }
            set { _cButtonSave.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Открыть'
        /// </summary>
        public bool __fButtonOpenVisible_
        {
            get { return _cButtonOpen.Visible; }
            set { _cButtonOpen.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Привязка текста к левому краю'
        /// </summary>
        public bool __fButtonAlignLeftVisible_
        {
            get { return _cButtonAlignLeft.Visible; }
            set { _cButtonAlignLeft.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Привязка текста по центру'
        /// </summary>
        public bool __fButtonAlignCenterVisible_
        {
            get { return _cButtonAlignCenter.Visible; }
            set { _cButtonAlignCenter.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Привязка текста к правому краю'
        /// </summary>
        public bool __fButtonAlignRightVisible_
        {
            get { return _cButtonAlignRight.Visible; }
            set { _cButtonAlignRight.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Толстый шрифт'
        /// </summary>
        public bool __fButtonFontBoldVisible_
        {
            get { return _cButtonFontBold.Visible; }
            set { _cButtonFontBold.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Наклонный шрифт'
        /// </summary>
        public bool __fButtonFontItalicVisible_
        {
            get { return _cButtonFontItalic.Visible; }
            set { _cButtonFontItalic.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Подчеркнутый шрифт'
        /// </summary>
        public bool __fButtonFontUnderlineVisible_
        {
            get { return _cButtonFontUnderline.Visible; }
            set { _cButtonFontUnderline.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Верхний регистр'
        /// </summary>
        public bool __fButtonScriptSuperVisible_
        {
            get { return _cButtonScriptSuper.Visible; }
            set { _cButtonScriptSuper.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Нижний регистр'
        /// </summary>
        public bool __fButtonScriptSubVisible_
        {
            get { return _cButtonScriptSub.Visible; }
            set { _cButtonScriptSub.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Шрифт'
        /// </summary>
        public bool __fButtonFontVisible_
        {
            get { return _cButtonFont.Visible; }
            set { _cButtonFont.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Цвет'
        /// </summary>
        public bool __fButtonColorVisible_
        {
            get { return _cButtonColor.Visible; }
            set { _cButtonColor.Visible = value; }
        }

        #endregion Видимость кнопок управления

        #region Подсказки к кнопкам

        /// <summary>
        /// Подсказка к кнопке 'Открыть'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonOpenToolTipText
        {
            set { _cButtonOpen.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Сохранить'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonSaveToolTipText
        {
            set { _cButtonSave.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Привязка текста к левому краю'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonAlignLeftToolTipText
        {
            set { _cButtonAlignLeft.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Привязка текста по центру'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonAlignCenterToolTipText
        {
            set { _cButtonAlignCenter.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Привязка текста к правому краю'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonAlignRightToolTipText
        {
            set { _cButtonAlignRight.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Толстый шрифт'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonFontBoldToolTipText
        {
            set { _cButtonFontBold.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Наклонный шрифт'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonFontItalicToolTipText
        {
            set { _cButtonFontItalic.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Зачеркнутый шрифт'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonFontStrikethroughToolTipText
        {
            set { _cButtonFontStrikethrough.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Верхний регистр'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonScriptSuperToolTipText
        {
            set { _cButtonScriptSuper.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Нижний регистр'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonScriptSubToolTipText
        {
            set { _cButtonScriptSub.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Шрифт'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonFontToolTipText
        {
            set { _cButtonFont.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Цвет'. Переведиться на язык пользователя
        /// </summary>
        public string __fButtonColorToolTipText
        {
            set { _cButtonColor.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }

        #endregion Подсказки к кнопкам

        #region Изображения на кнопках

        /// <summary>
        /// Изображение на кнопке 'Открыть'
        /// </summary>
        public Image __fButtonOpenImage
        {
            set { _cButtonOpen.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Сохранить'
        /// </summary>
        public Image __fButtonSaveImage
        {
            set { _cButtonOpen.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Привязка текста к левому краю'
        /// </summary>
        public Image __fButtonAlignLeftImage
        {
            set { _cButtonAlignLeft.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Привязка текста по центру'
        /// </summary>
        public Image __fButtonAlignCenterImage
        {
            set { _cButtonAlignCenter.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Привязка текста к правому краю'
        /// </summary>
        public Image __fButtonAlignRightImage
        {
            set { _cButtonAlignRight.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Толстый шрифт'
        /// </summary>
        public Image __fButtonFontBoldImage
        {
            set { _cButtonAlignRight.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Наклонный шрифт'
        /// </summary>
        public Image __fButtonFontItalicImage
        {
            set { _cButtonFontItalic.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Подчеркнутый шрифт'
        /// </summary>
        public Image __fButtonFontStrikethroughImage
        {
            set { _cButtonFontUnderline.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Зачеркнутый шрифт'
        /// </summary>
        public Image __fButtonFontUnderlineImage
        {
            set { _cButtonFontUnderline.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Верхний регистр'
        /// </summary>
        public Image __fButtonScriptSuperImage
        {
            set { _cButtonScriptSuper.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Нижний регистр'
        /// </summary>
        public Image __fButtonScriptSubImage
        {
            set { _cButtonScriptSub.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Шрифт'
        /// </summary>
        public Image __fButtonFont
        {
            set { _cButtonFont.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Цвет'
        /// </summary>
        public Image __fButtonColor
        {
            set { _cButtonColor.Image = value; }
        }

        #endregion Изображения на кнопках

        #endregion СВОЙСТВА
    }
}
