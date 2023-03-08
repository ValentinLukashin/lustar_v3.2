using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentCheck'
    /// </summary>
    /// <remarks>Компонент - включатель</remarks>
    public class crlComponentCheck : CheckBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentCheck()
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

            #region Настройка компонента

            AutoSize = true;
            BackColor = Color.Transparent;
            Font = crlApplication.__oInterface.__mFont(FONTS.Text);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Text);
            Text = "";

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Внутренние

        /// <summary>
        /// Полное название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Строка заголовка без перевода
        /// </summary>
        private string fTextWithOutTranslate = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст заголовка
        /// </summary>
        /// <remarks>Отображаемый текст переводиться на язык интерфейса. Возвращается не переведенный текст</remarks>
        public string __fCaption_
        {
            get { return fTextWithOutTranslate; }
            set
            {
                fTextWithOutTranslate = value.Trim();
                Text = crlApplication.__oTunes.__mTranslate(fTextWithOutTranslate);
            }
        }

        #endregion СВОЙСТВА
    }
}
