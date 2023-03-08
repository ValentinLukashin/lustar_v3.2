using System;
using System.Drawing;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputComboBool'
    /// </summary>
    /// <remarks>Поле ввода логических данных</remarks>
    public class crlInputComboBool : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
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
                _cLabelCaption.__eMouseClickRight += mLabelCaption_eMouseClickRight;
            }
            fLabelCaptionStatus = _cLabelCaption.__fLabelType_; /// Сохранение установленного статуса надписи-заголовка
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.__fComboType_ = COMBOTYPES.Bool;
                _cInput.__eValueInteractiveChanged += mInput_eValueInteractiveChanged;
                _cInput.__eValueProgrammaticChanged += mInput_eValueProgrammaticChanged;
            }

            #endregion Настойка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }

        /// <summary>
        /// (m) Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelCaption_eMouseClickRight(object sender, EventArgs e)
        {
            _cInput.Text = ""; /// Очищается название искомого справочника
            __fCheckStatus_ = false; /// !!! Выключается использование фильтра
            __fValue_ = 0;
            _cInput.Focus(); /// Перемещается курсор в поле ввода

            return;
        }
        /// <summary>
        /// (m) Выполняется при изменении введенных данных пользователем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mInput_eValueInteractiveChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра
            if(__eValueInteractiveChanged != null)
                __eValueInteractiveChanged(_cInput, e);

            return;
        }
        /// <summary>
        /// (m) Выполняется при изменении введенных данных программно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mInput_eValueProgrammaticChanged(object sender, EventArgs e)
        {
            if(__eValueProgrammaticChanged != null)
                __eValueProgrammaticChanged(_cInput, e);

            return;
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ    

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>* Индекс настройки
        /// </summary>
        /// <remarks>Используется в форме Изменение настроек приложения</remarks>
        public int __fTuneIndex = -1;

        #endregion - Атрибуты 

        #region - Внутренние

        /// <summary>Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>* Поле ввода логических данных
        /// </summary>
        protected crlComponentCombo _cInput = new crlComponentCombo();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// * Доступность контрола
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
                    if (_cInput.__fValueText_.Length > 0)
                        _cLabelValue.Text = _cInput.__fValueText_;
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>
        /// * Обязательность заполнения
        /// </summary>
        public override FILLTYPES __fFillType_
        {
            get { return _cInput.__fFillType_; }
            set { _cInput.__fFillType_ = value; }
        }
        /// <summary>
        /// * Условие фильтра
        /// </summary>
        public override string __fFilterExpression_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                if (__fTableAlias.Length > 0)
                    vReturn = __fTableAlias + ".";
                vReturn = vReturn + __fFieldName + " = " + Convert.ToInt32(__fValue_);

                return vReturn;
            }

        }
        /// <summary>
        /// * Выражение фильтра для отображения пользователю
        /// </summary>
        public override string __fFilterMessage_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                vReturn = vReturn + __fCaption_.Trim() + " = " + (__pValueText == "True" ? crlApplication.__oTunes.__mTranslate("Нет") : crlApplication.__oTunes.__mTranslate("Да"));

                return vReturn;
            }

        }
        /// <summary>
        /// * Значение контрола
        /// </summary>
        public override object __fValue_
        {
            get { return Convert.ToBoolean(_cInput.SelectedIndex); }
            set
            {
                try
                {
                    if (Convert.ToBoolean(value) != true)
                        _cInput.SelectedIndex = 0;
                    else
                        _cInput.SelectedIndex = 1;
                }
                catch
                {
                    _cInput.SelectedIndex = 0;
                }

                _cLabelValue.Text = _cInput.Text; // Запись значения по умолчанию
            }
        }
        /// <summary>
        /// * Текст соответсвующий значению
        /// </summary>
        public string __pValueText
        {
            get { return _cInput.__fValueText_; }
        }

        #endregion = СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>* Возникает при ручном изменении данных
        /// </summary>
        public event EventHandler __eValueInteractiveChanged;
        /// <summary>* Возникает при программном изменении данных
        /// </summary>
        public event EventHandler __eValueProgrammaticChanged;

        #endregion = СОБЫТИЯ
    }
}
