using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentCharMask'
    /// </summary>
    /// <remarks>Компонент для ввода символьных данных по маске</remarks>
    public class crlComponentCharMask : MaskedTextBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentCharMask()
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

            BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
            Height = 23;

            #endregion Настройка компонента

            ResumeLayout(false);

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
        /// Выполняется при проверке ввода данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (fFillType == FILLTYPES.Necessarily)
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
        /// <summary>
        /// Выполняется при изменении данных в компоненте
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (fKeyPressNow == false)
            {
                if (__mProgrammaticChanged != null)
                    __mProgrammaticChanged(this, new EventArgs());
            }
            else
            {
                if (__mInteractiveChanged != null)
                    __mInteractiveChanged(this, new EventArgs());
            }

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

        #endregion Служеюные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public virtual FILLTYPES _fFillType
        {
            get { return fFillType; }
            set { fFillType = value; }
        }
        /// <summary>
        /// Значение компонента
        /// </summary>
        public string __pValue
        {
            get { return Text.Trim(); }
            set { Text = value.Trim(); }
        }

        #endregion СВОЙСТВА    

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при изменении данных пользователем
        /// </summary>
        public event EventHandler __mInteractiveChanged;
        /// <summary>
        /// Возникает при программном изменении данных
        /// </summary>
        public event EventHandler __mProgrammaticChanged;

        #endregion СОБЫТИЯ
    }
}
