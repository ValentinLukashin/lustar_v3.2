using System;
using System.ComponentModel;
using System.Drawing;

namespace nlControls
{
    /// <summary>Класс 'crlInputNumberDecimal'
    /// </summary>
    /// <remarks>Поле ввода десятичных дробных данных</remarks>
    public class crlInputNumberDecimal : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region - Объект

        /// <summary>
        /// Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cLabelCaption
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Normal; /// Назначение вида надписи-заголовка - 'Надпись' 
                _cLabelCaption.__eMouseClickRight += _cLabelCaption___eMouseClickRight;
            }
            fLabelCaptionStatus = _cLabelCaption.__fLabelType_; /// Сохранение установленного статуса надписи-заголовка
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.__eInteractiveChanged += _cInput___eInteractiveChanged;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }
        /// <summary>
        /// Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cLabelCaption___eMouseClickRight(object sender, EventArgs e)
        {
            _cInput.Text = ""; /// Очищается название искомого справочника
            __fCheckStatus_ = false; /// !!! Выключается использование фильтра
            _cInput.Focus(); /// Перемещается курсор в поле ввода
        }
        /// <summary>
        /// Выполняется при ручном изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput___eInteractiveChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра
        }

        #endregion Объект

        #endregion Поведение

        #endregion МЕТОДЫ   

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Поле ввода дробных десятичных данных
        /// </summary>
        protected crlComponentNumber _cInput = new crlComponentNumber();

        #endregion Компоненты

        #region = Служебные

        /// <summary>
        /// Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion Служебные

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
        /// <summary>Условие фильтра
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
        /// <summary>
        /// Выражение фильтра для отображения пользователю
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
        /// <summary>
        /// Доступность контрола
        /// </summary>
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
                    if(_cInput.Text.Length > 0)
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
                _cInput.__fValue_ = Convert.ToDecimal(value);
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
        /// Максимальное количество отображаемых символов после запятой при полученном фокусе
        /// </summary>
        [Category("Расширенные настройки")]
        [DefaultValue(0)]
        [Description("Максимальное количество отображаемых символов после запятой при полученном фокусе")]
        [RefreshProperties(RefreshProperties.All)]
        public int __fScaleOnFocus_
        {
            get { return _cInput.__fScaleOnFocus_; }
            set { _cInput.__fScaleOnFocus_ = value; }
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
        /// <summary>
        /// Максимальное количество отображаемых символов после запятой при потере фокуса
        /// </summary>
        [Category("Расширенные настройки")]
        [DefaultValue(0)]
        [Description("Максимальное количество отображаемых символов после запятой при потере фокуса")]
        [RefreshProperties(RefreshProperties.All)]
        public int __fScaleOnLostFocus_
        {
            get { return _cInput.__fScaleOnLostFocus_; }
            set { _cInput.__fScaleOnLostFocus_ = value; }
        }

        #endregion СВОЙСТВА
    }
}
