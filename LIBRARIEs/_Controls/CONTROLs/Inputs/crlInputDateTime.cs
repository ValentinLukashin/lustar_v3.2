// using nlApplication;
using nlApplication;
using System;
using System.Drawing;

namespace nlControls
{
    /// <summary>Класс 'crlInputDateTime'
    /// </summary>
    /// <remarks>Поле ввода данных даты и времени</remarks>
    public class crlInputDateTime : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>* Загрузка контрола
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
                _cInput.__eValueInteractiveChanged += _cInput___eValueInteractiveChanged;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cLabelCaption___eMouseClickRight(object sender, EventArgs e)
        {
            __mEmptyValue();
        }
        /// <summary>Выполняется при изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput___eValueInteractiveChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true;  /// Включение использования фильтра
            if (__eValueInteractiveChanged != null)
                __eValueInteractiveChanged(_cInput, new EventArgs());
        }
        
        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Установка пустого значения
        /// </summary>
        public void __mEmptyValue()
        {
            _cInput.Text = ""; /// Очищается название искомого справочника
            __fCheckStatus_ = false; /// !!! Выключается использование фильтра
            __fValue_ = appTypeDateTime.__mMsSqlDateEmpty(); /// Установка пустого значчения даты-времени
            _cInput.Focus(); /// Перемещается курсор в поле ввода
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ  

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion - Внутренние

        #region . Компоненты

        /// <summary>* Поле ввода даты-времени
        /// </summary>
        protected crlComponentDateTime _cInput = new crlComponentDateTime();

        #endregion Компоненты

        #region . Свойства

        /// <summary>* Доступность контрола
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
                    if (_cInput.Value != appTypeDateTime.__mMsSqlDateEmpty())
                        _cLabelValue.Text = _cInput.Value.ToString();
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>* Вид отображения даты - времени
        /// </summary>
        public DATETIMETYPES __pDateTimeType
        {
            get { return _cInput.__fDateTimeType_; }
            set { _cInput.__fDateTimeType_ = value; }
        }
        /// <summary>* Значение контрола
        /// </summary>
        public override object __fValue_
        {
            get { return _cInput.__fValue_; }
            set
            {
                _cInput.__fValue_ = Convert.ToDateTime(value);
                _cLabelValue.Text = _cInput.__fValue_.ToString();  // Запись значения по умолчанию
            }
        }

        #endregion Свойства

        #endregion ПОЛЯ

        #region = СОБЫТИЯ

        /// <summary>* Возникает при 
        /// </summary>
        public event EventHandler __eValueInteractiveChanged;

        #endregion СОБЫТИЯ
    }
}
