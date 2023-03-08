using nlApplication;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentNumberDecimal'
    /// </summary>
    /// <remarks>Компонент для ввода целых десятичных чисел</remarks>
    public class crlComponentNumberInt : crlComponentNumber
    {
        #region = ДИЗАЙНЕРЫ

        public crlComponentNumberInt()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            SuspendLayout();

            #region /// Настройка компонента

            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
            TextAlign = HorizontalAlignment.Right;

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при нажатии клавиши на клавиатуре
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            int vCaretPosition = SelectionStart; // Позиция картеки при вводе символа
            if (e.KeyCode == Keys.Delete)
            {
                mFormatNumber(vCaretPosition, e.KeyCode.ToString());
                e.Handled = true;
            }

            if (__eKeyDown != null)
                __eKeyDown(this, e);

            base.OnKeyDown(e);

            fKeyPressNow = true;

            return;
        }
        /// <summary>
        /// Выполняется при отпускании клавиши на клавиатуре
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            fKeyPressNow = false;

            return;
        }
        /// <summary>
        /// Выполняется при изменении данных в компоненте
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (fKeyPressNow == false)
            {
                if (__eProgrammaticChanged != null)
                    __eProgrammaticChanged(this, new EventArgs());
            }
            else
            {
                if (__eInteractiveChanged != null)
                    __eInteractiveChanged(this, new EventArgs());
            }

            base.OnTextChanged(e);

            return;
        }
        /// <summary>
        /// Выполняется при создании объекта
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //_mObjectProperties();
        }
        /// <summary>
        /// Выполняется при получении фокуса
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            Select(0, 0);
            base.OnGotFocus(e);
        }
        /// <summary>
        /// Выполняется после потере фокуса
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            string vTextNew = "";
            foreach (char vChar in Text.ToCharArray())
            {
                if (vChar != ' ')
                    vTextNew = vTextNew + vChar.ToString();
            }
            Text = vTextNew;
            base.OnLostFocus(e);
        }
        /// <summary>
        /// Выполняется при полном цикле нажатия на клавишу
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            int vCaretPosition = SelectionStart; // Позиция картеки при вводе символа
            mFormatNumber(vCaretPosition, e.KeyChar.ToString());
            e.Handled = true;


            base.OnKeyPress(e);
        }
        /// <summary>
        /// Выполняется при проверке введенных данных
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            /// Обработка пустого значения
            if (this.Text.Equals(string.Empty) == true)
            {
                (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Нет данных"));
                e.Cancel = true;
            }
            /// Если значение [0] разрешено и установлено [0]  
            if (Convert.ToDecimal(this.__fValue_).Equals(Convert.ToDecimal(0)) && fFillType == FILLTYPES.None)
                return;
            /// Обработка нулевого значения
            if (Convert.ToDecimal(this.__fValue_).Equals(Convert.ToDecimal(0)) && fFillType == FILLTYPES.None)
            {
                (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные отличные от нуля"));
                e.Cancel = true;
                //return;
            }

            if ((this.Text.Equals(string.Empty) || Convert.ToDecimal(this.__fValue_).Equals(Convert.ToDecimal(0))) && fFillType == FILLTYPES.Necessarily)
            {
                (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные отличные от нуля"));
                e.Cancel = true;
            }

            base.OnValidating(e);

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Удаление первых нулей
        /// </summary>
        /// <param name="pText">Текстовый эквивалент числового значения</param>
        /// <returns>Выражение без незначащих нулей</returns>
        private void mDeleteFirstZeros(ref string pText)
        {
            while (pText.Trim().StartsWith("0") == true & pText.Trim().Length > 1)
            {
                pText = pText.Substring(0, 1);
            }
        }
        /// <summary>
        /// Проверка отображения текста в качестве числа
        /// </summary>
        /// <param name="pCaretePosition">Размещение каретки</param>
        /// <param name="pCharNew">Вставляемый символ</param>
        private void mFormatNumber(int pCaretePosition, string pCharNew)
        {
            int vCaretePositionNew = pCaretePosition; // Новое размещение каретки
            string vText = Text; // Текст до ввода нового значения
            /// Перевод в отрицательное значение и обратно
            if (fNegative == true)
            {
                /// Введен символ '-'
                if (pCharNew == "-")
                {
                    if (vText.Trim() == "0") /// Текущее значение = [0]
                        return;

                    /// Удаление минуса
                    if (vText.StartsWith("-") == true)
                    {
                        Text = Text.Substring(1);
                        if (pCaretePosition >= 1)
                            vCaretePositionNew = pCaretePosition - 1;
                    }
                    /// Добавление минуса
                    else
                    {
                        Text = "-" + Text.Trim();
                        vCaretePositionNew = pCaretePosition + 1;
                    }
                    SelectionStart = vCaretePositionNew;
                    return;
                }
                /// Введен символ '+'
                if (pCharNew == "+")
                {
                    if (vText.StartsWith("-") == true)
                    {
                        Text = Text.Substring(1);
                        if (pCaretePosition >= 1)
                            vCaretePositionNew = pCaretePosition - 1;
                    }
                    SelectionStart = vCaretePositionNew;
                    return;
                }
            }

            /// Обработка клавиши 'BackSpace'
            if (pCharNew == "\b")
            {
                /// Выделен весь текст
                if (SelectedText == vText)
                {
                    vText = "0";
                    vCaretePositionNew = 0;
                }
                if (pCaretePosition > 0)
                {
                    if (vText.Trim().Substring(0, 1) != "-")
                    {
                        vText = vText.Substring(0, pCaretePosition - 1) + vText.Substring(pCaretePosition);
                        vCaretePositionNew = pCaretePosition - 1;
                    }
                }
                /// Удалены все символы
                if (vText.Trim().Length == 0)
                {
                    vText = "0";
                    vCaretePositionNew = 0;
                }
                mDeleteFirstZeros(ref vText);
                Text = vText;
                SelectionStart = vCaretePositionNew;

                return;
            }
            /// Обработка клавиши 'Delete'
            if (pCharNew == "Delete")
            {
                /// Выделен весь текст
                if (SelectedText == vText)
                {
                    vText = "0";
                }
                /// Удаление символа после курсора
                if (pCaretePosition != vText.Length)
                {
                    if (vText.Trim().Substring(pCaretePosition, 1) != "-")
                    {
                        vText = vText.Trim().Substring(0, pCaretePosition) + vText.Trim().Substring(pCaretePosition + 1);
                        vCaretePositionNew = pCaretePosition;
                    }
                }
                /// Удалены все символы
                if (vText.Trim().Length == 0)
                {
                    vText = "0";
                    vCaretePositionNew = 0;
                }
                mDeleteFirstZeros(ref vText);
                Text = vText;
                SelectionStart = vCaretePositionNew;
                return;
            }
            /// Проверка ввода чисел
            if ("0123456789".Contains(pCharNew.ToString()) == false)
            { return; }
            /// Добавление пробела в конец данных
            if (vText.Trim() == "0")
            {
                vText = pCharNew + " ";
            }
            else
            {
                /// Выделен весь текст
                if (SelectedText == vText)
                {
                    vText = pCharNew;
                    vCaretePositionNew = 0;
                }
                else
                    vText = vText.Trim().Substring(0, pCaretePosition) + pCharNew.ToString() + Text.Trim().Substring(pCaretePosition) + " ";
            }
            vCaretePositionNew = vText.Trim().Length;

            /// Проверка на вхождение в ограничения
            if (vText.Trim().Length > fPrecision + 1 & mValidMaxMinValues(vText) == false)
                return;

            Text = vText;
            SelectionStart = vCaretePositionNew;
        }
        /// <summary>
        /// Проверка на вхождение в ограничения
        /// </summary>
        /// <param name="pText">Проверяемый текст</param>
        /// <returns></returns>
        private bool mValidMaxMinValues(string pText)
        {
            bool vReturn = true; // Возвращаемое значение

            /// Если значение равно [0] и 0 - разрешенное значение 
            if (Convert.ToInt32(pText) == 0 & _fZeroIsValue == true)
            {
            }
            else
            {
                /// Проверка максимального значения
                if (Convert.ToInt32(pText.Trim()) > fValueMaximum)
                {
                    (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные меньше {0}", fValueMaximum));
                    vReturn = vReturn & false;
                }
                /// Проверка минимального значения
                if (Convert.ToInt32(pText.Trim()) < fValueMinimum)
                {
                    (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные больше {0}", fValueMinimum));
                    vReturn = vReturn & false;
                }
            }

            return vReturn;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        public bool _fZeroIsValue = true;

        #endregion - Атрибуты

        #region - Внутренние

        /// <summary>Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";
        /// <summary>Вид ввода данных
        /// </summary>
        private FILLTYPES fFillType = FILLTYPES.None;
        /// <summary>Состояние - нажата клавиша клавиатуры 
        /// </summary>
        private bool fKeyPressNow = false;
        /// <summary>Включение / выключение возможности принимать отрицательное значение
        /// </summary>
        private bool fNegative = false;
        /// <summary>Маска ввода данных
        /// </summary>
        private string fMask = "0";
        /// <summary>Точность
        /// </summary>
        private int fPrecision = 0;
        /// <summary>Максимальное значение
        /// </summary>
        private int fValueMaximum = 10000000;
        /// <summary>Минимальное значение
        /// </summary>
        private int fValueMinimum = -10000000;
        /// <summary>Разрешение нулевых значений
        /// </summary>
        private bool fZeroIsValid = true;

        #endregion - Внутренние

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public FILLTYPES __fFillType_
        {
            get { return fFillType; }
            set
            {
                fFillType = value;
                if (fFillType == FILLTYPES.None)
                    BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
                else
                    BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBackNecessarily);
            }
        }
        /// <summary>
        /// Маска вводимых данных
        /// </summary>
        public string __fMask_
        {
            get { return fMask; }
            set
            {
                fMask = value;
                fNegative = fMask.StartsWith("-"); /// Разрешение ввода отрицательных значений
                if (fNegative == false)
                    fPrecision = appTypeString.__mWordNumberComma(fMask, 0).Length;
                else
                    fPrecision = appTypeString.__mWordNumberComma(fMask, 0).Length - 1; // Не учитываем минус
                if (fMask.Contains(",") == true | fMask.Contains(".") == true)
                {
                    int vRightPartLength = appTypeString.__mWordNumberComma(fMask, 1).Length;
                    Text = new string(' ', fMask.Length - vRightPartLength - 2) + "0," + new string('0', vRightPartLength);
                }
                else
                {
                    Text = new string(' ', fMask.Length - 1) + "0";
                }
                Text = Text.Trim();
                Width = Convert.ToInt32(crlTypeFont.__mMeasureText(fMask.Length + 2, crlApplication.__oInterface.__mFont(FONTS.Data)).Width);

                if (fValueMaximum > Convert.ToInt32(new string('9', fPrecision)))
                    fValueMaximum = Convert.ToInt32(new string('9', fPrecision));
                if (fValueMinimum < 0)
                    fValueMinimum = 0;
            }
        }
        /// <summary>
        /// Значение
        /// </summary>
        public int __fValue_
        {
            get
            {
                string vValue = (Text.Trim() == "" ? "0" : Text.Trim());
                return Convert.ToInt32(vValue);
            }
            set
            {
                Text = Convert.ToString(value);
            }
        }
        /// <summary>
        /// Максимальное значение
        /// </summary>
        public int __fValueMaximum_
        {
            get { return fValueMaximum; }
            set { fValueMaximum = value; }
        }
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public int __fValueMinimum_
        {
            get { return fValueMinimum; }
            set { fValueMinimum = value; }
        }
        /// <summary>
        /// Разрешение нулевых значений
        /// </summary>
        public bool __pfZeroIsValid_
        {
            get { return fZeroIsValid; }
            set { fZeroIsValid = value; }
        }

        #endregion СВОЙСТВА  

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при изменении данных пользователем
        /// </summary>
        public event EventHandler __eInteractiveChanged;
        /// <summary>
        /// Возникает при программном изменении данных
        /// </summary>
        public event EventHandler __eProgrammaticChanged;
        /// <summary>
        /// Возникает при нажатии клавиши
        /// </summary>
        public event EventHandler __eKeyDown;

        #endregion СОБЫТИЯ
    }
}
