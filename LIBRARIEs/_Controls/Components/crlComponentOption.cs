using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentOption'
    /// </summary>
    public class crlComponentOption : RadioButton
    {
        #region = ДИЗАЙНЕРЫ

        public crlComponentOption()
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
            BackColor = Color.Transparent;

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>Сборка выражения с параметрами и перевод выражения на язык интерфейса 
        /// </summary>
        /// <param name="pString">Текст</param>
        /// <param name="pParameters">Список дополнительных парамметров</param>
        public void __mCaptionBuilding(string pString, params object[] pParameters)
        {
            _fTextWithOutTranslate = String.Format(pString, pParameters);
            Text = crlApplication.__oTunes.__mTranslate(pString, pParameters);
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";
        /// <summary>Не переведенный текст заголовка
        /// </summary>
        protected string _fTextWithOutTranslate = "";

        #endregion - Внутренние

        #endregion ПОЛЯ
    }
}
