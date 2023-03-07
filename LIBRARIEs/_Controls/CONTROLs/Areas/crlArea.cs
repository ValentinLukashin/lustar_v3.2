using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlArea'
    /// </summary>
    /// <remarks>Элемент области для работы с данными</remarks>
    public class crlArea : crlComponentSplitter
    {
        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            #region /// Размещение компонентов

            Panel1.Controls.Add(_cHeaderPicture);
            Panel1.Controls.Add(_cHeaderLabel);
            Panel2.Controls.Add(__cToolBar);
            __cToolBar.Items.Add(_cButtonHelp);
#if DEBUG
            __cToolBar.Items.Add(_cButtonDebug);
#endif

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            BorderStyle = BorderStyle.Fixed3D;
            Dock = DockStyle.Fill;
            IsSplitterFixed = true;
            FixedPanel = FixedPanel.Panel1;
            Orientation = Orientation.Horizontal;
            TabStop = false;
            Panel1Collapsed = true;

            // _cButtonHelp
            {
                _cButtonHelp.Image = global::nlResourcesImages.Properties.Resources._SignQuestion_b32C;
                _cButtonHelp.ToolTipText = "[ F1 ] " + crlApplication.__oTunes.__mTranslate("Помощь");
                _cButtonHelp.__eMouseClickLeft += mButtonHelp_eMouseClickLeft;
                _cButtonHelp.__eMouseClickRight += mButtonHelp_eMouseClickRight;
            }
            // _cButtonDebug
            {
                _cButtonDebug.Image = global::nlResourcesImages.Properties.Resources._Gears_b32C;
                _cButtonDebug.ToolTipText = "Операции тестирования";
                _cButtonDebug.__eMouseClickLeft += mButtonDebug_eMouseClickLeft;
                _cButtonDebug.__eMouseClickRight += mButtonDebug_eMouseClickRight;
            }
            // _cHeaderPicture
            {
                _cHeaderPicture.BorderStyle = BorderStyle.Fixed3D;
                _cHeaderPicture.Location = new Point(crlInterface.__fIntervalHorizontal, crlInterface.__fIntervalVertical);
                _cHeaderPicture.Size = new Size(36, 36);
            }
            // _cHeaderLabel
            {
                _cHeaderLabel.Location = new Point(_cHeaderPicture.Left + _cHeaderPicture.Width + crlInterface.__fIntervalHorizontal, _cHeaderPicture.Height / 2);
                _cHeaderLabel.__fCaption_ = "Название области";
                _cHeaderLabel.__fLabelType_ = LABELTYPES.Title;
            }

            SplitterDistance = _cHeaderPicture.Top + _cHeaderPicture.Height + crlInterface.__fIntervalVertical * 2;

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #region - Кнопки управления

        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь'
        /// </summary>
        public void __mPressButtonHelp()
        {
            _cButtonHelp.PerformClick();

            return;
        }

        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonHelp_eMouseClickLeft(object sender, EventArgs e)
        {
            if (__eButtonHelp_ClickLeft != null)
                __eButtonHelp_ClickLeft(_cButtonHelp, e);
            else
                (FindForm() as crlForm).__mHelp();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь' правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonHelp_eMouseClickRight(object sender, EventArgs e)
        {
            if (__eButtonHelp_ClickRight != null)
                __eButtonHelp_ClickRight(_cButtonHelp, e);
            else
            {
                Form vForm = FindForm() as Form;
                if (vForm.MinimumSize.Width > 0)
                {
                    vForm.MinimumSize = new Size(0, 0);
                    (FindForm() as crlForm).__cStatus.__fCaption_ = "Минимальные размеры формы сброшены";
                }
                else
                {
                    vForm.MinimumSize = new Size(vForm.Width, vForm.Height);
                    (FindForm() as crlForm).__cStatus.__fCaption_ = "Текущие размеры установлены как минимальные";
                }
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Тест' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonDebug_eMouseClickLeft(object sender, EventArgs e)
        {
            if (__eButtonDebug_ClickLeft != null)
                __eButtonDebug_ClickLeft(_cButtonDebug, e);
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Тест' правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonDebug_eMouseClickRight(object sender, EventArgs e)
        {
            if (__eButtonDebug_ClickRight != null)
                __eButtonDebug_ClickRight(_cButtonDebug, e);
        }

        #endregion Кнопки управления

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Признак указывающий, что в данный момент открыто выпадающее меню кнопки
        /// </summary>
        protected bool _fDropDownOpened = false;

        #endregion Внутренние

        #region - Компоненты

        /// <summary>
        /// Полоса инструментов
        /// </summary>
        public crlComponentToolBar __cToolBar = new crlComponentToolBar();
        /// <summary>
        /// Кнопка 'Помощь'
        /// </summary>
        protected crlComponentToolBarButton _cButtonHelp = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Отладка'
        /// </summary>
        protected crlComponentToolBarButton _cButtonDebug = new crlComponentToolBarButton();
        /// <summary>
        /// Изображение в заголовке области
        /// </summary>
        protected crlComponentPicture _cHeaderPicture = new crlComponentPicture();
        /// <summary>
        /// Заголовок названия области
        /// </summary>
        protected crlComponentLabel _cHeaderLabel = new crlComponentLabel();

        #endregion Компоненты

        #region - Служебные

        /// <summary>
        /// Текст заголовка области
        /// </summary>
        private string fHeaderText = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Доступность кнопки 'Помощь'
        /// </summary>
        public bool __fButtonHelpEnabled_
        {
            get { return _cButtonHelp.Enabled; }
            set { _cButtonHelp.Enabled = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Помощь'
        /// </summary>
        public bool __fButtonHelpVisible_
        {
            get { return _cButtonHelp.Visible; }
            set { _cButtonHelp.Visible = value; }
        }
        /// <summary>
        /// Подсказка к кнопке 'Помощь' переведенная на язык пользователя
        /// </summary>
        public string __fButtonHelpToolTipText
        {
            set { _cButtonHelp.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Изображение на кнопке 'Помощь'
        /// </summary>
        public Image __fButtonHelpImage
        {
            set { _cButtonHelp.Image = value; }
        }

        /// <summary>
        /// Изображение-логотип области 
        /// </summary>
        public Image __fHeaderImage_
        {
            //d get { return _cHeaderPicture.Image; }
            set { _cHeaderPicture.Image = value; }
        }
        /// <summary>
        /// Текст заголовка области
        /// </summary>
        /// <remarks>Выполняется перевод на язык интерфейса. При чтении возвращается не переведенный текст</remarks>
        public string __fHeaderText_
        {
            get { return fHeaderText; }
            set
            {
                fHeaderText = value;
                _cHeaderLabel.__fCaption_ = crlApplication.__oTunes.__mTranslate(value);
            }
        }
        /// <summary>
        /// Видимость заголовка
        /// </summary>
        public bool __fHeaderVisible_
        {
            get { return !Panel1Collapsed; }
            set
            {
                Panel1Collapsed = !value;
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при клике левой клавиши мыши по кнопке 'Помощь'
        /// </summary>
        public event EventHandler __eButtonHelp_ClickLeft;
        /// <summary>
        /// Возникает при клике правой клавиши мыши по кнопке 'Помощь'
        /// </summary>
        public event EventHandler __eButtonHelp_ClickRight;
        /// <summary>
        /// Возникает при выборе кнопки 'Отладка' левой клавишей мыши
        /// </summary>
        public event EventHandler __eButtonDebug_ClickLeft;
        /// <summary>
        /// Возникает при выборе кнопки 'Отладка' левой клавишей мыши
        /// </summary>
        public event EventHandler __eButtonDebug_ClickRight;

        #endregion СОБЫТИЯ
    }
}
