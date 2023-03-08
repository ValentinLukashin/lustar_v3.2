using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputChar'
    /// </summary>
    /// <remarks>Поле ввода символьных данных</remarks>
    public class crlInputChar : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

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
            if(_cLabelCaption.__fLabelType_ == LABELTYPES.Button)
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
            if(__eInteractiveChanged != null)
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

        #endregion Поведение

        #endregion МЕТОДЫ    

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Индекс настройки
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

        /// <summary>Поле ввода символьных данных
        /// </summary>
        protected crlComponentChar _cInput = new crlComponentChar();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

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
                    if (_cInput.Text.Trim().Length > 0)
                        _cLabelValue.Text = _cInput.Text.Trim();
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>
        /// Построение условия фильтра
        /// </summary>
        public override string __fFilterExpression_
        {
            get
            {
                string vFilter = ""; // Условие фильтра
                string vString = _cInput.Text.Trim();
                string[] vWordsList = vString.Split(' '); // Количество слов в условии
                foreach (string vWord in vWordsList)
                {
                    if (vWord.Length == 0)
                        continue;
                    if (vFilter.Length != 0)
                        vFilter = vFilter + " and ";
                    if (__fTableAlias.Length > 0)
                        vFilter = vFilter + __fTableAlias + ".";
                    vFilter = vFilter + __fFieldName + " Like '%" + vWord + "%'";
                }

                return vFilter;
            }
        }
        /// <summary>
        /// Поучение условия фильтра для отображения пользователю
        /// </summary>
        public override string __fFilterMessage_
        {
            get
            {
                return _cLabelCaption.Text.Trim() + ": " + __fValue_.ToString();
            }
        }
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public override FILLTYPES __fFillType_
        {
            get { return _cInput.__fFillType_; }
            set { _cInput.__fFillType_ = value; }
        }
        /// <summary>
        /// Многострочное использование
        /// </summary>
        public virtual bool __fMultiline_
        {
            get { return _cInput.Multiline; }
            set
            {
                _cInput.Multiline = value;
                if (_cInput.Multiline == true)
                {
                    _cInput.Dock = DockStyle.Fill;
                    _cInput.ScrollBars = ScrollBars.Both;
                    _cInput.WordWrap = false;
                }
                else
                {
                    //Height = fHeightNormal;
                    _cInput.Dock = DockStyle.None;
                    _cInput.ScrollBars = ScrollBars.None;
                    _cInput.WordWrap = true;
                }
            }
        }
        /// <summary>
        /// Символ для маскировки введенного пароля
        /// </summary>
        public char __fPasswordChar_
        {
            get { return _cInput.PasswordChar; }
            set { _cInput.PasswordChar = value; }
        }
        /// <summary>
        /// Количество отображаемых символов данных
        /// </summary>
        public virtual int __fSymbolsCount_
        {
            get { return _cInput.__fSymbolsCount_; }
            set { _cInput.__fSymbolsCount_ = value; }
        }
        /// <summary>
        /// Назначение символа за которым будут скрываться символы при вводе пароля
        /// </summary>
        public char __fSymbolsHide_
        {
            set
            {
                _cInput.PasswordChar = value;
            }
        }
        /// <summary>
        /// Значение поля ввода
        /// </summary>
        public override object __fValue_
        {
            get { return _cInput.Text; }
            set
            {
                _cInput.Text = value.ToString().Trim();
                _cLabelValue.Text = _cInput.Text;  // Запись значения по умолчанию
            }
        }
        /// <summary>
        /// Вид надписи
        /// </summary>
        public LABELTYPES __fLabelType_
        {
            get { return _cLabelCaption.__fLabelType_; }
            set { _cLabelCaption.__fLabelType_ = value; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        public event EventHandler __eOnLabelCaptionClickLeft;
        /// <summary>
        /// Возникает при ручном изменении данных 
        /// </summary>
        public event EventHandler __eInteractiveChanged;
        public event EventHandler __eKeyDown;

        #endregion СОБЫТИЯ
    }
}
