using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlAreaEmail'
    /// </summary>
    /// <remarks>Область для формирования отправляемого почтового сообщения</remarks>
    public class crlAreaEmailSend : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов 

            __cToolBar.Items.Insert(0, __cButtonSend);
            
            Controls.Add(_cSplitter);
            Controls.SetChildIndex(_cSplitter, 0);

            _cSplitter.Panel1.Controls.Add(__cRecipientS);
            _cSplitter.Panel1.Controls.Add(__cInputSubject);
            _cSplitter.Panel1.Controls.Add(__cAttacmentS);

            _cSplitter.Panel2.Controls.Add(__cMessage);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // __cToolBar
            {
                // _cButtonSend
                {
                    __cButtonSend.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                    __cButtonSend.ToolTipText = "[ Ctrl + A ] " + crlApplication.__oTunes.__mTranslate("Отправить сообщение");
                }
            }
            // _cSplitter
            {
                _cSplitter.Dock = DockStyle.Fill;
                // Panel1
                {
                    // _cRecipientS
                    {
                        __cRecipientS.Location = new Point(5, 5);
                        __cRecipientS.Size = new Size(_cSplitter.Panel1.Width - crlInterface.__fIntervalHorizontal * 2, 100);
                        __cRecipientS.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        __cRecipientS.__fFieldName = "";
                        //_cRecipientS.__oFormSelect = crlFormCalled;
                        __cRecipientS.__fCaption_ = "Получатели";
                        __cRecipientS.__fCheckVisible_ = false;
                        __cRecipientS.__eLabelCaption_MouseClickLeft += _cRecipientS___eLabelCaption_MouseClickLeft;
                        __cRecipientS.__eLabelCaption_MouseClickRight += _cRecipientS___eLabelCaption_MouseClickRight;
                    }
                    // _cInputSubject
                    {
                        __cInputSubject.Location = new Point(5, __cRecipientS.Top + __cRecipientS.Height + crlInterface.__fIntervalVertical);
                        __cInputSubject.__fCaption_ = "Тема";
                        __cInputSubject.__fCheckVisible_ = false;
                        __cInputSubject.__fSymbolsCount_ = -1;
                    }
                    // _cAttacmentS
                    {
                        __cAttacmentS.Location = new Point(5, __cInputSubject.Top + __cInputSubject.Height + crlInterface.__fIntervalVertical);
                        __cAttacmentS.Size = new Size(_cSplitter.Panel1.Width - crlInterface.__fIntervalHorizontal * 2, 100);
                        __cAttacmentS.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        __cAttacmentS.__fFieldName = "";
                        //_cAttacmentS.__oFormSelect = crlFormCalled;
                        __cAttacmentS.__fCaption_ = "Вложения";
                        __cAttacmentS.__fCheckVisible_ = false;
                        __cAttacmentS.__eLabelCaption_MouseClickLeft += _cAttacmentS___eLabelCaption_MouseClickLeft;
                        __cAttacmentS.__eLabelCaption_MouseClickRight += _cAttacmentS___eLabelCaption_MouseClickRight;
                    }
                }
                _cSplitter.FixedPanel = FixedPanel.Panel1;
                _cSplitter.IsSplitterFixed = true;
                // Panel2
                {
                    // _cMessage
                    {
                        __cMessage.Dock = DockStyle.Fill;
                    }
                }
            }

            #endregion Настройка компонентов

            ResumeLayout();
        }

        #endregion Объект

        #region Надписи-кнопки

        /// <summary>Выполняется при клике левой кнопки мыши по заголовку компонента '_cRecipientS'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cRecipientS___eLabelCaption_MouseClickLeft(object sender, EventArgs e)
        {
            if (__eRecipientS_MouseClickLeft != null)
                __eRecipientS_MouseClickLeft(this, new EventArgs());
        }
        /// <summary>Выполняется при клике правой кнопки мыши по заголовку компонента '_cRecipientS'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cRecipientS___eLabelCaption_MouseClickRight(object sender, EventArgs e)
        {
            if (__eRecipientS_MouseClickRight != null)
                __eRecipientS_MouseClickRight(this, new EventArgs());
        }
        /// <summary>Выполняется при клике левой кнопки мыши по заголовку компонента '_cAttacmentS'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cAttacmentS___eLabelCaption_MouseClickLeft(object sender, EventArgs e)
        {
            //string vFilePath = ""; // Путь и имя выбранного файла
            //OpenFileDialog openFileDialog = new OpenFileDialog(); // Объект для выбора файла 

            //openFileDialog.InitialDirectory = "c:\\";
            //openFileDialog.Filter = "All files (*.*)|*.*";
            //openFileDialog.FilterIndex = 1;
            //openFileDialog.RestoreDirectory = true;

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    vFilePath = openFileDialog.FileName;
            //_cAttacmentS.__mItemAdd(vFilePath);
            //_cAttacmentS.__mDataRefresh();
            if (__eAttacmentS_MouseClickLeft != null)
                __eAttacmentS_MouseClickLeft(this, new EventArgs());
        }
        /// <summary>Выполняется при клике правой кнопки мыши по заголовку компонента '_cAttacmentS'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cAttacmentS___eLabelCaption_MouseClickRight(object sender, EventArgs e)
        {
            //_cAttacmentS.__mItemRemove();
            if (__eAttacmentS_MouseClickRight != null)
                __eAttacmentS_MouseClickRight(this, new EventArgs());
        }

        #endregion Надписи-кнопки

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Расчет позиции разделителя
        /// </summary>
        public void __mSplitterPositioning()
        {
            _cSplitter.SplitterDistance = __cAttacmentS.Top + __cAttacmentS.Height + crlInterface.__fIntervalVertical;
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Кнопка 'Отправить'
        /// </summary>
        public crlComponentToolBarButton __cButtonSend = new crlComponentToolBarButton();
        /// <summary>Разделитель
        /// </summary>
        protected crlComponentSplitter _cSplitter = new crlComponentSplitter();
        /// <summary>Поле ввода 'Получатели'
        /// </summary>
        public crlInputListBox __cRecipientS = new crlInputListBox();
        /// <summary>Поле ввода 'Тема'
        /// </summary>
        public crlInputChar __cInputSubject = new crlInputChar();
        /// <summary>Поле ввода 'Вложения'
        /// </summary>
        public crlInputListBox __cAttacmentS = new crlInputListBox();
        /// <summary>Поле ввода сообщения
        /// </summary>
        public crlAreaText __cMessage = new crlAreaText();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Видимость кнопки 'Открыть'
        /// </summary>
        public bool __fAreaTextButtonOpenVisible_
        {
            get { return __cMessage.__fButtonOpenVisible_; }
            set { __cMessage.__fButtonOpenVisible_ = value; }
        }
        /// <summary>Видимость кнопки 'Сохранить'
        /// </summary>
        public bool __fAreaTextButtonSaveVisible_
        {
            get { return __cMessage.__fButtonSaveVisible_; }
            set { __cMessage.__fButtonSaveVisible_ = value; }
        }
        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>Возникает при клике левой кнопки мыши по заголовку компонента '_cRecipientS'
        /// </summary>
        public event EventHandler __eRecipientS_MouseClickLeft;
        /// <summary>Возникает при клике правой кнопки мыши по заголовку компонента '_cRecipientS'
        /// </summary>
        public event EventHandler __eRecipientS_MouseClickRight;
        /// <summary>Возникает при клике левой кнопки мыши по заголовку компонента '_cAttacmentS'
        /// </summary>
        public event EventHandler __eAttacmentS_MouseClickLeft;
        /// <summary>Возникает при клике правой кнопки мыши по заголовку компонента '_cAttacmentS'
        /// </summary>
        public event EventHandler __eAttacmentS_MouseClickRight;

        #endregion = СОБЫТИЯ
    }
}
