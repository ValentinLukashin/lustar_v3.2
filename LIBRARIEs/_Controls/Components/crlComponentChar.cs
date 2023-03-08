using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentChar'
    /// </summary>
    /// <remarks>Компонент для ввода символьных данных</remarks>
    public class crlComponentChar : TextBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentChar()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕР

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
            Height = 23;

            #endregion Настройка компонента

            PerformLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при нажатии клавиши на клавиатуре
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
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
        /// Выполняется при проверке ввода данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (__fFillType_ == FILLTYPES.Necessarily)
            {
                if (Text.Length == 0)
                {
                    (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Поле должно быть обязательно заполненным"));
                    e.Cancel = true;
                }
            }

            base.OnValidating(e);

            return;
        }

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Вид ввода данных
        /// </summary>
        private FILLTYPES fFillType = FILLTYPES.None;
        /// <summary>
        /// Состояние - нажата клавиша клавиатуры 
        /// </summary>
        private bool fKeyPressNow = false;
        /// <summary>
        /// Количество отображаемых символов данных
        /// </summary>
        private int fSymbolCount = 10;

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        ///  Обязательность заполнения
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
        /// Количество отображаемых символов
        /// </summary>
        public virtual int __fSymbolsCount_
        {
            get { return fSymbolCount; }
            set
            {
                fSymbolCount = value;

                if (fSymbolCount > 0)
                {
                    Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    MaxLength = fSymbolCount;
                    if (fSymbolCount > 3)
                        Width = Convert.ToInt32(crlTypeFont.__mMeasureText(fSymbolCount, crlApplication.__oInterface.__mFont(FONTS.Data)).Width);
                    else
                        Width = 10 + Convert.ToInt32(crlTypeFont.__mMeasureText(fSymbolCount, crlApplication.__oInterface.__mFont(FONTS.Data)).Width);
                } /// > Указано количество символов
                else
                {
                    MaxLength = 32767;
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    if (Parent != null)
                    {
                        Width = Parent.Width
                            - Left
                            - crlInterface.__fFormBorderWidth * 2;
                    }
                }/// > Количество символов не указано

                fSymbolCount = value;
            }
        }
        /// <summary> 
        /// Значение компонента
        /// </summary>
        public string __fValue_
        {
            get { return Text.Trim(); }
            set { Text = value.Trim(); }
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
