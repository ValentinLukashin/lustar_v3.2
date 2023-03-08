using nlApplication;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentNumberDecimal'
    /// </summary>
    /// <remarks>Компонент для ввода дробных десятичных значений</remarks>
    public class crlComponentNumberDecimal : TextBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentNumberDecimal()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            SuspendLayout();

            #region Настройка компонента

            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
            TextAlign = HorizontalAlignment.Right;

            #endregion Настройка компонента

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
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

            base.OnKeyDown(e);

            fKeyPressNow = true;
        }
        /// <summary>
        /// Выполняется при отпускании клавиши на клавиатуре
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            fKeyPressNow = false;
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
        /// Выполняется при изменении данных в компоненте
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
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
        }

        #endregion Объект

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
            string vText = Text.Length == 0 ? "0" : Text;
            int vCaretePositionNew = pCaretePosition;
            int vCaretePositionInScale = 0;
            /// Указание разделителя. Перевод курсора за разделитель
            if (pCharNew == "." | pCharNew == ",")
            {
                /// Если разделителя в поле ввода нет - выход
                if (Text.Contains(fDecimalSeperator) == false)
                {
                    return;
                }
                Select(Text.IndexOf(',', 0) + 1, 0); // Перевод курсора на позицию сразу за разделителем
            }
            /// Перевод в отрицательное значение и обратно
            if (fNegative == true)
            {
                if (pCharNew == "-")
                {
                    if (vText.StartsWith("-") == true)
                        Text = Text.Substring(1);
                    else
                        Text = "-" + Text.Trim();
                }
                if (pCharNew == "+")
                {
                    if (vText.StartsWith("-") == true)
                        Text = Text.Substring(1);
                }
            }
            /// Обработка клавиши 'BackSpace'
            if (pCharNew == "\b")
            {
                if (pCaretePosition == Text.IndexOf(fDecimalSeperator) + 1)
                {
                    return;
                }
                /// Редактирование целой части
                if (pCaretePosition < Text.IndexOf(fDecimalSeperator))
                {
                    Text = Text.Substring(0, pCaretePosition - 1) + Text.Substring(pCaretePosition);
                    if (pCaretePosition != 0)
                        Select(pCaretePosition - 1, 0);
                }
                else
                {
                    if (Text.Substring(0, pCaretePosition - 1) != "")
                        Text = Text.Substring(0, pCaretePosition - 1) + Text.Substring(pCaretePosition);
                    else
                        Text = "0" + Text.Substring(pCaretePosition);
                    Select(pCaretePosition - 1, 0);
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
                if (pCaretePosition == Text.IndexOf(fDecimalSeperator))
                {
                    return;
                }
                /// Редактирование целой части 123.46
                if (pCaretePosition < Text.IndexOf(fDecimalSeperator))
                {
                    Text = Text.Substring(0, pCaretePosition) + Text.Substring(pCaretePosition + 1);
                }
                /// Редактирование дробной части
                else
                {
                    Text = Text.Substring(0, pCaretePosition - 1) + fDecimalSeperator + Text.Substring(pCaretePosition + 1) + "0";
                }
                Select(pCaretePosition, 0);
                return;
            }
            /// Проверка ввода чисел
            if ("0123456789".Contains(pCharNew.ToString()) == false)
                return;
            /// Курсор в начале текста и текст начинается с '0'
            if (pCaretePosition == 0 & Text.Trim().StartsWith("0") == true)
            {
                vText = pCharNew.ToString() + fDecimalSeperator + appTypeString.__mWordNumberComma(vText, 1);
                vCaretePositionNew = 1;
            }
            else
            {
                /// Редактирование целой части
                if (pCaretePosition <= Text.IndexOf(fDecimalSeperator))
                {
                    // pCaretePosition = Text.IndexOf(_fDecimalSeperator);
                    vText = vText.Substring(0, pCaretePosition) + pCharNew.ToString() + Text.Substring(pCaretePosition);
                    // if(pCaretePosition < Text.IndexOf(_fDecimalSeperator))
                    vCaretePositionNew++;
                }
                /// Редактирование дробной части 
                else
                {
                    if (fScale > 0)
                    {
                        vCaretePositionInScale = pCaretePosition - Text.IndexOf(fDecimalSeperator);
                        if (vCaretePositionInScale > fScale)
                            vCaretePositionInScale = 1;
                        vText = vText.Substring(0, pCaretePosition) + pCharNew.ToString() + vText.Substring(pCaretePosition).Substring(1, fScale - vCaretePositionInScale);
                        if (pCaretePosition < Text.Length - 1)
                            vCaretePositionNew++;
                    }
                }
            }

            /// Проверка максимального значения
            if (Convert.ToDecimal(vText) > fValueMaximum)
            {
                (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные меньше {0}", fValueMaximum));
                return;
            }
            /// Проверка минимального значения
            if (Convert.ToDecimal(vText) < fValueMinimum)
            {
                (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные больше {0}", fValueMinimum));
                return;
            }

            Text = vText;
            Select(vCaretePositionNew, 0);
        }
        /// <summary>
        /// Проверка на вхождение в ограничения
        /// </summary>
        /// <param name="pText">Проверяемый текст</param>
        /// <returns></returns>
        //private bool _mValidMaxMinValues(string pText)
        //{
        //    bool vReturn = true; // Возвращаемое значение

        //    /// Если значение равно [0] и 0 - разрешенное значение 
        //    if (Convert.ToInt32(pText) == 0 & fZeroIsValid == true)
        //    {
        //    }
        //    else
        //    {
        //        /// Проверка максимального значения
        //        if (Convert.ToInt32(pText.Trim()) > fValueMaximum)
        //        {
        //            (FindForm() as crlForm)._mBaloonMessage(this, crlApplication._oTunes._mTranslate("Введите данные меньше {0}", fValueMaximum));
        //            vReturn = vReturn & false;
        //        }
        //        /// Проверка минимального значения
        //        if (Convert.ToInt32(pText.Trim()) < fValueMinimum)
        //        {
        //            (FindForm() as crlForm)._mBaloonMessage(this, crlApplication._oTunes._mTranslate("Введите данные больше {0}", fValueMinimum));
        //            vReturn = vReturn & false;
        //        }
        //    }

        //    return vReturn;
        //}

        #endregion Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region = Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Десятичный разделитель
        /// </summary>
        private string fDecimalSeperator = ","; // System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        /// <summary>
        /// Вид ввода данных
        /// </summary>
        private FILLTYPES fFillType = FILLTYPES.None;
        /// <summary>
        /// Состояние - нажата клавиша клавиатуры 
        /// </summary>
        private bool fKeyPressNow = false;
        /// <summary>
        /// Маска ввода данных
        /// </summary>
        private string fMask = "0";

        private int fPrecision = 0;
        private int fScale = 0;
        private bool fNegative = false;
        /// <summary>
        /// Максимальное значение
        /// </summary>
        private decimal fValueMaximum = 10000000;
        /// <summary>
        /// Минимальное значение
        /// </summary>
        private decimal fValueMinimum = -10000000;
        /// <summary>
        /// Разрешение нулевых значений
        /// </summary>
        private bool fZeroIsValid = true;

        #endregion Служебные

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
                if (fMask.Contains(",") == true)
                {
                    int vLeftPartLength = appTypeString.__mWordNumberComma(fMask, 0).Length;
                    Text = new string(' ', fMask.Length - vLeftPartLength - 2) + "0," + new string('0', vLeftPartLength);
                }
                else
                {
                    Text = new string(' ', fMask.Length - 1) + "0";
                }
                fPrecision = appTypeString.__mWordNumberComma(fMask, 0).Length;
                fScale = appTypeString.__mWordNumberComma(fMask, 1).Length;
                fNegative = fMask.StartsWith("-");
                Width = Convert.ToInt32(crlTypeFont.__mMeasureText(fMask.Length, crlApplication.__oInterface.__mFont(FONTS.Data)).Width);

                if (fValueMaximum > Convert.ToInt32(new string('9', fPrecision)))
                    fValueMaximum = Convert.ToInt32(new string('9', fPrecision));
                if (fScale == 0 & fValueMinimum < 0)
                    fValueMinimum = 0;
            }
        }
        /// <summary>
        /// Значение
        /// </summary>
        public decimal __fValue_
        {
            get 
            {
                string vValue = (Text.Trim() == "" ? "0" : Text.Trim());
                return Convert.ToDecimal(vValue);
            }
            set { Text = Convert.ToString(Math.Round(value, fScale)); }
        }
        /// <summary>
        /// Максимальное значение
        /// </summary>
        public decimal __fValueMaximum_
        {
            get { return fValueMaximum; }
            set { fValueMaximum = value; }
        }
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public decimal __fValueMinimum_
        {
            get { return fValueMinimum; }
            set { fValueMinimum = value; }
        }
        /// <summary>
        /// Разрешение нулевых значений
        /// </summary>
        public bool __fZeroIsValid_
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

        #endregion СОБЫТИЯ
    }
}
