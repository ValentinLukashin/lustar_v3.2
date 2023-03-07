using nlApplication;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlAreaMessage'
    /// </summary>
    /// <remarks>Область для отображения сообщений пользователю</remarks>
    public class crlAreaMessage : crlArea
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

            __cToolBar.Items.Insert(0, _cButtonApply);
            __cToolBar.Items.Insert(1, _cButtonCancel);
            __cToolBar.Items.Add(_cButtonDetails);

            Panel2.Controls.Add(_cSplitter);
            Panel2.Controls.SetChildIndex(_cSplitter, 0);

            _cSplitter.Panel1.Controls.Add(_cPicture);
            _cSplitter.Panel1.Controls.Add(_cCheck);

            _cSplitter.Panel2.Controls.Add(_cLabel);

            #endregion Размещение компонентов

            #region Настройка компонентов

            BorderStyle = BorderStyle.FixedSingle;

            // _cSplitter
            {
                _cSplitter.BorderStyle = BorderStyle.Fixed3D;
                _cSplitter.BackColor = Color.Transparent;
                _cSplitter.Dock = DockStyle.Fill;
                _cSplitter.IsSplitterFixed = true;
                _cSplitter.Orientation = Orientation.Horizontal;
                _cSplitter.Panel1Collapsed = false;
                _cSplitter.FixedPanel = FixedPanel.Panel1;
                _cSplitter.TabStop = false;
                _cSplitter.Panel2.AutoScroll = true;
            }
            { /// Кнопки управления

                _cButtonApply.Click += _cButtonApply_Click;
                _cButtonApply.ImageScaling = ToolStripItemImageScaling.None;

                _cButtonCancel.Click += _cButtonCancel_Click;
                _cButtonCancel.ImageScaling = ToolStripItemImageScaling.None;

                _cButtonDetails.Alignment = ToolStripItemAlignment.Right;
                _cButtonDetails.Image = global::nlResourcesImages.Properties.Resources._BookOpen_b32C;
                _cButtonDetails.ImageScaling = ToolStripItemImageScaling.None;
                _cButtonDetails.ToolTipText = "[ Ctrl + L ]\n" + crlApplication.__oTunes.__mTranslate("Подробно");

                { /// Кнопки решения "Дополнительно"
                    _cButtonDetailHide = _cButtonDetails.DropDownItems.Add(crlApplication.__oTunes.__mTranslate("Сообщение"));
                    _cButtonDetailHide.Click += _cButtonDetailHide_Click;
                    _cButtonDetailHide.Image = global::nlResourcesImages.Properties.Resources._Book_b16C;
                    _cButtonDetailHide.ImageScaling = ToolStripItemImageScaling.None;

                    _cButtonDetailShow = _cButtonDetails.DropDownItems.Add(crlApplication.__oTunes.__mTranslate("Подробности"));
                    _cButtonDetailShow.Click += _cButtonDetailShow_Click;
                    _cButtonDetailShow.Image = global::nlResourcesImages.Properties.Resources._BookOpen_b16C;
                    _cButtonDetailShow.ImageScaling = ToolStripItemImageScaling.None;
                }
            } // Кнопки управления
            // _cPicture
            {
                _cPicture.SizeMode = PictureBoxSizeMode.CenterImage;
                _cPicture.Size = new Size(56, 56); /// !!!
                _cPicture.Location = new Point((_cSplitter.Panel1.Width - _cPicture.Width) / 2, crlInterface.__fIntervalVertical);
                _cPicture.Anchor = AnchorStyles.Top; /// !!!
                _cPicture.BackColor = Color.Transparent;
            }
            // _cCheck
            {
                _cCheck.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                _cCheck.Location = new Point(crlInterface.__fIntervalHorizontal * 2, 60);
            }
            // _cLabel
            {
                _cLabel.AutoSize = true;
                _cLabel.Location = new Point(crlInterface.__fIntervalHorizontal * 2, crlInterface.__fIntervalVertical * 2);
                _cLabel.Size = new Size(_cSplitter.Panel2.Width - crlInterface.__fIntervalHorizontal * 4, _cSplitter.Panel2.Height - crlInterface.__fIntervalVertical * 4);
                _cLabel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            }

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            _cSplitter.SplitterDistance = 120;

            _cButtonDetailHide.PerformClick();

            return;
        }
        /// <summary>
        /// Презентация объекта
        /// </summary>
        protected override void _mObjectPresetation()
        {
            base._mObjectPresetation();
            _cSplitter.SplitterDistance = 100;
            if (FindForm() != null)
                FindForm().WindowState = FormWindowState.Normal;
        }
        #endregion Объект

        #region Кнопки управления

        /// <summary>
        /// Выполняется при выборе кнопки 'Ok, Да'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonApply_Click(object sender, EventArgs e)
        {
            _cButtonDetailHide.PerformClick();
            switch (fMessageType)
            {
                case MESSAGESTYPES.None:
                    _fResult = DialogResult.OK;
                    break;
                case MESSAGESTYPES.Error:
                    _fResult = DialogResult.OK;
                    break;
                case MESSAGESTYPES.ErrorRetry:
                    _fResult = DialogResult.Retry;
                    break;
                case MESSAGESTYPES.Info:
                    _fResult = DialogResult.OK;
                    break;
                case MESSAGESTYPES.Question:
                    _fResult = DialogResult.Yes;
                    break;
                case MESSAGESTYPES.Warning:
                    _fResult = DialogResult.OK;
                    break;
            }
            Form vForm = FindForm();
            vForm.Close();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Отмена, Нет'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonCancel_Click(object sender, EventArgs e)
        {
            _cButtonDetailHide.PerformClick();
            switch (fMessageType)
            {
                case MESSAGESTYPES.ErrorRetry:
                    _fResult = DialogResult.Cancel;
                    break;
                case MESSAGESTYPES.Question:
                    _fResult = DialogResult.No;
                    break;
            }
            Form vForm = FindForm();
            vForm.Close();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonHelp_Click(object sender, EventArgs e)
        {
            crlForm vForm = FindForm() as crlForm;
            vForm.__mHelp();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Подробно / Сообщение'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonDetailHide_Click(object sender, EventArgs e)
        {
            _cLabel.Text = fMessage;
            if (FindForm() != null)
                FindForm().WindowState = FormWindowState.Normal;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Подробно / Детали'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonDetailShow_Click(object sender, EventArgs e)
        {
            _cLabel.Text = fMessage + "\n" + _fMessageDetail;
            FindForm().WindowState = FormWindowState.Maximized;
        }

        #endregion Кнопки управления

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Выполняется при выборе кнопки 'Применить'
        /// </summary>
        public void __mPressButtonApply()
        {
            _cButtonApply.PerformClick();

            return;
        }

        #endregion Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Значение возвращаемый формой
        /// </summary>
        public DialogResult _fResult = DialogResult.None;

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>
        /// Сообщение
        /// </summary>
        public string fMessage = "";
        /// <summary>
        /// Детали
        /// </summary>
        public string _fMessageDetail = "";
        /// <summary>
        /// Вид сообщения
        /// </summary>
        private MESSAGESTYPES fMessageType = MESSAGESTYPES.None;

        #endregion Внутренние

        #region - Компоненты

        /// <summary>
        /// Разделитель панелей
        /// </summary>
        protected SplitContainer _cSplitter = new SplitContainer();

        /// <summary>
        /// Кнопка 'Ok, Да'
        /// </summary>
        protected crlComponentToolBarButton _cButtonApply = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Отмена, Нет'
        /// </summary>
        protected crlComponentToolBarButton _cButtonCancel = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Подробно'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonDetails = new crlComponentToolBarButtonMenu();

        /// <summary>
        /// Меню 'Создать'
        /// </summary>
        protected ToolStripItem _cButtonDetailHide;
        /// <summary>
        /// Меню 'Изменить'
        /// </summary>
        protected ToolStripItem _cButtonDetailShow;

        /// <summary>
        /// Картинка вида сообщения 
        /// </summary>
        protected crlComponentPicture _cPicture = new crlComponentPicture();
        /// <summary>
        /// Включатель
        /// </summary>
        protected crlComponentCheck _cCheck = new crlComponentCheck();
        /// <summary>
        /// Текст
        /// </summary>
        protected crlComponentLabel _cLabel = new crlComponentLabel();

        #endregion Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string __fMessage_
        {
            set
            {
                fMessage = value;
                _cLabel.Text = fMessage;
            }
        }
        /// <summary>
        /// Вид сообщения
        /// </summary>
        public MESSAGESTYPES __fMessageType_
        {
            get { return fMessageType; }
            set
            {
                _cButtonApply.Visible = false;
                _cButtonCancel.Visible = false;
                Form vForm = FindForm(); // Форма на которой размещен контрол
                vForm.Text = appTypeString.__mWordPersonal(crlApplication.__fProcessName_);

                fMessageType = value;
                switch (fMessageType)
                {
                    case MESSAGESTYPES.None:
                        _cButtonApply.Visible = true;
                        _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                        _cButtonApply.ToolTipText = "[ Ctrl + 1 ]\n" + crlApplication.__oTunes.__mTranslate("Ok");
                        break;
                    case MESSAGESTYPES.Error:
                        vForm.Text = crlApplication.__oTunes.__mTranslate("Ошибка");
                        _cPicture.Image = global::nlResourcesImages.Properties.Resources._SmileError_y32C;

                        _cButtonApply.Visible = true;
                        _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                        _cButtonApply.ToolTipText = "[ Ctrl + 1 ]\n" + crlApplication.__oTunes.__mTranslate("Ok");
                        break;
                    case MESSAGESTYPES.ErrorRetry:
                        vForm.Text = crlApplication.__oTunes.__mTranslate("Ошибка");
                        _cPicture.Image = global::nlResourcesImages.Properties.Resources._SmileError_y32C;

                        _cButtonApply.Visible = true;
                        _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignRefresh_b32C;
                        _cButtonApply.ToolTipText = "[ Ctrl + 1 ]\n" + crlApplication.__oTunes.__mTranslate("Повторить");

                        _cButtonCancel.Visible = true;
                        _cButtonCancel.Image = global::nlResourcesImages.Properties.Resources._SignCross_r32C;
                        _cButtonCancel.ToolTipText = "[ Ctrl + 2 ]\n" + crlApplication.__oTunes.__mTranslate("Отмена");
                        break;
                    case MESSAGESTYPES.Info:
                        vForm.Text = crlApplication.__oTunes.__mTranslate("Информация");
                        _cPicture.Image = global::nlResourcesImages.Properties.Resources._SmileInfo_y32C;

                        _cButtonApply.Visible = true;
                        _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                        _cButtonApply.ToolTipText = "[ Ctrl + 1 ]\n" + crlApplication.__oTunes.__mTranslate("Ok");
                        break;
                    case MESSAGESTYPES.Question:
                        vForm.Text = crlApplication.__oTunes.__mTranslate("Вопрос");
                        _cPicture.Image = global::nlResourcesImages.Properties.Resources._SmileQuestion_y32C;

                        _cButtonApply.Visible = true;
                        _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                        _cButtonApply.ToolTipText = "[ Ctrl + 1 ]\n" + crlApplication.__oTunes.__mTranslate("Да");

                        _cButtonCancel.Visible = true;
                        _cButtonCancel.Image = global::nlResourcesImages.Properties.Resources._SignCross_r32C;
                        _cButtonCancel.ToolTipText = "[ Ctrl + 2 ]\n" + crlApplication.__oTunes.__mTranslate("Нет");
                        break;
                    case MESSAGESTYPES.Warning:
                        vForm.Text = crlApplication.__oTunes.__mTranslate("Предупреждение");
                        _cPicture.Image = global::nlResourcesImages.Properties.Resources._SmileWarning_y32C;

                        _cButtonApply.Visible = true;
                        _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                        _cButtonApply.ToolTipText = "[ Ctrl + 1 ]\n" + crlApplication.__oTunes.__mTranslate("Ok");
                        break;
                }
            }
        }
        /// <summary>
        /// Видимость включателя
        /// </summary>
        public bool __fCheckVisible_
        {
            get { return _cCheck.Visible; }
            set { _cCheck.Visible = value; }
        }
        /// <summary>
        /// Статус включателя
        /// </summary>
        public bool __fCheckChecked_
        {
            get { return _cCheck.Checked; }
            set { _cCheck.Checked = value; }
        }
        /// <summary>
        /// Текст включателя
        /// </summary>
        public string __fCheckCaption_
        {
            get { return _cCheck.__fCaption_; }
            set { _cCheck.__fCaption_ = value; }
        }

        #endregion СВОЙСТВА
    }
}
