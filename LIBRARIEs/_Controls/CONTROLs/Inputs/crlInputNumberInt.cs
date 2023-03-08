using System;
using System.ComponentModel;
using System.Drawing;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputNumberInt'
    /// </summary>
    /// <remarks>Поле ввода десятичных целых данных</remarks>
    public class crlInputNumberInt : crlInput
    {
        #region = МЕТОДЫ

        #region = Действия

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            TabStop = false;

            // _cLabelCaption
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Normal; /// Назначение вида надписи-заголовка - 'Надпись' 
                _cLabelCaption.__eMouseClickLeft += mLabelCaption_eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += mLabelCaption_eMouseClickRight;
            }
            fLabelCaptionStatus = _cLabelCaption.__fLabelType_; /// Сохранение установленного статуса надписи-заголовка
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.__fScaleOnFocus_ = 0;
                _cInput.__fScaleOnLostFocus_ = 0;
                _cInput.__eInteractiveChanged += mInput_eInteractiveChanged;
                _cInput.__eKeyDown += mInput_eKeyDown;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при выборе надписи левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelCaption_eMouseClickLeft(object sender, EventArgs e)
        {
            if (_cLabelCaption.__fLabelType_ == LABELTYPES.Button)
                if (__eOnLabelCaptionClickLeft != null)
                    __eOnLabelCaptionClickLeft(this, new EventArgs());
        }
        /// <summary>
        /// Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelCaption_eMouseClickRight(object sender, EventArgs e)
        {
            /// Очищается название искомого справочника
            _cInput.Text = "";
            /// Выключается использование фильтра
            __fCheckStatus_ = false;
            /// Перемещается курсор в поле ввода
            _cInput.Focus(); 
        }
        /// <summary>
        /// Выполняется при ручном изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mInput_eInteractiveChanged(object sender, EventArgs e)
        {
            if (__eInteractiveChanged != null)
                __eInteractiveChanged(this, e);
            /// Включение использования фильтра
            __fCheckStatus_ = true; 

            return;
        }
        /// <summary>
        /// Выполняется при нажатии на клавиши
        /// </summary>
        /// <param name="e"></param>
        private void mInput_eKeyDown(object sender, EventArgs e)
        {
            if (__eKeyDown != null)
                __eKeyDown(_cInput, e);
        }

        #endregion - Поведение

        #region - Процедуры

        /// <summary>
        /// Перевод фокуса на поле ввода
        /// </summary>
        public override void __mInputFocus()
        {
            base.__mInputFocus();
            _cInput.Focus();
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ  

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Поле ввода числовых целых данных
        /// </summary>
        protected crlComponentNumberInt _cInput = new crlComponentNumberInt();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public override FILLTYPES __fFillType_
        {
            get { return _cInput.__fFillType_; }
            set { _cInput.__fFillType_ = value; }
        }
        /// <summary>
        /// Условие фильтра
        /// </summary>
        public override string __fFilterExpression_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                if (__fTableAlias.Length > 0)
                    vReturn = __fTableAlias + ".";
                vReturn = vReturn + __fFieldName + " = " + __fValue_.ToString();

                return vReturn;
            }

        }
        /// <summary>Выражение фильтра для отображения пользователю
        /// </summary>
        public override string __fFilterMessage_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                vReturn = vReturn + __fCaption_.Trim() + " = " + __pValueText;

                return vReturn;
            }
        }
        public override bool __fEnabled_
        {
            get { return base.__fEnabled_; }
            set
            {
                base.__fEnabled_ = value;
                _cInput.Visible = value;
                if (value == true)
                {
                    _cLabelCaption.__fLabelType_ = fLabelCaptionStatus;
                }
                else
                {
                    _cLabelCaption.__fLabelType_ = LABELTYPES.Normal;
                    if (_cInput.Text.Length > 0)
                        _cLabelValue.Text = _cInput.Text;
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>
        /// Значение
        /// </summary>
        public override object __fValue_
        {
            get { return _cInput.__fValue_; }
            set
            {
                _cInput.__fValue_ = Convert.ToInt32(value);
                _cLabelValue.Text = _cInput.__fValue_.ToString();  // Запись значения по умолчанию
            }
        }
        /// <summary>
        /// Строчный эквивалент 
        /// </summary>
        public string __pValueText
        {
            get { return _cInput.Text; }
        }
        /// <summary>
        /// Разрешение использования отрицательных значений
        /// </summary>
        [Category("Расширенные настройки")]
        [DefaultValue(false)]
        [Description("Разрешение использования отрицательных значений")]
        [RefreshProperties(RefreshProperties.None)]
        public bool __fAllowNegative_
        {
            get { return _cInput.__fAllowNegative_; }
            set { _cInput.__fAllowNegative_ = value; }
        }
        /// <summary>
        /// Разрешение использования нулевого значения
        /// </summary>
        [Category("Расширенные настройки")]
        [DefaultValue(false)]
        [Description("Разрешение использования нулевого значения")]
        [RefreshProperties(RefreshProperties.None)]
        public bool __fAllowZero_
        {
            get { return _cInput.__fAllowZero_; }
            set { _cInput.__fAllowZero_ = value; }
        }
        /// <summary>
        /// Максимальное количество отображаемых числовых символов
        /// </summary>
        [Category("Расширенные настройки")]
        [DefaultValue(10)]
        [Description("Максимальное количество отображаемых числовых символов")]
        [RefreshProperties(RefreshProperties.None)]
        public int __fPrecision_
        {
            get { return _cInput.__fPrecision_; }
            set { _cInput.__fPrecision_ = value; }
        }

        #endregion = СВОЙСТВА

        #region = СОБЫТИЯ

        public event EventHandler __eOnLabelCaptionClickLeft;
        /// <summary>
        /// Возникает при ручном изменении данных 
        /// </summary>
        public event EventHandler __eInteractiveChanged;
        public event EventHandler __eKeyDown;

        #endregion = СОБЫТИЯ
    }
}
