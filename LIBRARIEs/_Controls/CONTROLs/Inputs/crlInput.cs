using System;
using System.Windows.Forms;
using System.Drawing;

namespace nlControls
{
    /// <summary>Класс 'crlInput'
    /// </summary>
    /// <remarks>Базовое поле ввода</remarks>
    public abstract class crlInput : crlComponentSplitter
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region - Объект

        /// <summary>
        /// Сборка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Panel1.Controls.Add(_cCheck);
            Panel1.Controls.Add(_cLabelCaption);
            Panel2.Controls.Add(_cLabelValue);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            BorderStyle = BorderStyle.None;
            IsSplitterFixed = true;
            FixedPanel = FixedPanel.Panel1;
            Orientation = Orientation.Vertical;
            Size = new Size(300, 25);
            SplitterDistance = 200;
            TabStop = false;

            // _cCheck
            {
                _cCheck.Location = new Point(crlInterface.__fIntervalHorizontal, crlInterface.__fIntervalVertical);
                _cCheck.TabStop = false;
            }
            // _cLabelCaption
            {
                _cLabelCaption.Location = new Point(_cCheck.Left
                    + _cCheck.Width
                    + crlInterface.__fIntervalHorizontal
                    , crlInterface.__fIntervalVertical);
                _cLabelCaption.__fCaption_ = "НАДПИСЬ";
            }
            // _cLabelValue
            {
                _cLabelValue.Location = new Point(0, crlInterface.__fIntervalVertical);
                _cLabelValue.Font = crlApplication.__oInterface.__mFont(FONTS.Data);
                _cLabelValue.ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
                _cLabelValue.__fCaption_ = "нет данных";
                _cLabelValue.Visible = false;
            }

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при первом отображении компонента
        /// </summary>
        protected override void _mObjectPresetation()
        {
            base._mObjectPresetation();

            if (Parent != null)
                Width = Parent.ClientSize.Width - crlInterface.__fIntervalHorizontal * 2;
        }

        #endregion Объект

        #endregion Поведение

        #region - Процедуры

        /// <summary>Перевод фокуса на поле ввода
        /// </summary>
        public virtual void __mInputFocus()
        {
            Focus();
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Название поля таблицы
        /// </summary>
        public string __fFieldName = "";
        /// <summary>Псевдоним таблицы в запросе данных
        /// </summary>
        public string __fTableAlias = "";
        /// <summary>Значение изменено программой
        /// </summary>
        public bool __fValueChangedByProgramm_ = false;
        /// <summary>Значение изменено пользователем
        /// </summary>
        public bool __fValueChangedByUser = false;

        #endregion - Атрибуты

        #region = Служебные

        /// <summary>
        /// Доступность контрола
        /// </summary>
        private bool fEnabled = true;
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        private FILLTYPES fFillType = FILLTYPES.None;
        /// <summary>
        /// Заголовок формы для поиска без перевода
        /// </summary>
        private string fCaptionNotTranslate = "";

        #endregion Служебные

        #region - Компоненты

        /// <summary>
        /// Компонент - включатель построения фильтра
        /// </summary>
        protected crlComponentCheck _cCheck = new crlComponentCheck();
        /// <summary>
        /// Надпись - заголовок
        /// </summary>
        protected crlComponentLabel _cLabelCaption = new crlComponentLabel();
        /// <summary>
        /// Надпись - значение, отображаемое только для чтения
        /// </summary>
        protected crlComponentLabel _cLabelValue = new crlComponentLabel();

        #endregion Компоененты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст заголовка
        /// </summary>
        /// <remarks>Отображаемый текст переводиться на язык интерфейса. Возвращается не переведенный текст</remarks>
        public string __fCaption_
        {
            get { return fCaptionNotTranslate; }
            set
            {
                fCaptionNotTranslate = value;
                _cLabelCaption.__fCaption_ = crlApplication.__oTunes.__mTranslate(value);
            }
        }
        /// <summary>
        /// Доступность включателя для построения фильтра
        /// </summary>
        public bool __fCheckEnabled
        {
            get { return _cCheck.Enabled; }
            set { _cCheck.Enabled = value; }
        }
        /// <summary>
        /// Статус включатель для построения фильтра
        /// </summary>
        public bool __fCheckStatus_
        {
            get { return _cCheck.Checked; }
            set { _cCheck.Checked = value; }
        }
        /// <summary>
        /// Видимость включателя для построения фильтра
        /// </summary>
        public bool __fCheckVisible_
        {
            get { return _cCheck.Visible; }
            set
            {
                _cCheck.Visible = value;
                if (value == true)
                    _cLabelCaption.Location = new Point(_cCheck.Left + _cCheck.Width, crlInterface.__fIntervalHorizontal);
                else
                    _cLabelCaption.Location = new Point(0, crlInterface.__fIntervalHorizontal);
            }
        }
        /// <summary>
        /// Доступность контрола
        /// </summary>
        public virtual bool __fEnabled_
        {
            get { return fEnabled; }
            set
            {
                fEnabled = value;
                _cLabelValue.Visible = !fEnabled;
            }
        }
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public virtual FILLTYPES __fFillType_
        {
            get { return fFillType; }
            set { fFillType = value; }
        }
        /// <summary>
        /// Условие фильтра для указанного поля
        /// </summary>
        public virtual string __fFilterExpression_ { get; }
        /// <summary>
        /// Выражение фильтра для указанного поля для отображения пользователю
        /// </summary>
        public virtual string __fFilterMessage_ { get; }
        /// <summary>
        /// Значение контрола
        /// </summary>
        public virtual object __fValue_
        {
            get { return null; }
            set { __fValueChangedByProgramm_ = true; }
        }
        /// <summary>
        /// Название значения контрола
        /// </summary>
        public virtual string __fValueName_
        {
            get { return _cLabelValue.Text; }
        }

        #endregion = СВОЙСТВА
    }
}
