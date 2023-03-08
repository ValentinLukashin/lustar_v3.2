using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentMenu'
    /// </summary>
    /// <remarks>Компонент - меню</remarks>
    public class crlComponentMenu : MenuStrip
    {
        #region = ДИЗАЙНЕРЫ

        public crlComponentMenu()
        {
            _mLoad();
        }

        #endregion = ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Размещение клмплнента
        /// </summary>
        protected virtual void _mLoad()
        {
            SuspendLayout();

            #region Настройка компонента

            BackColor = Color.Transparent;
            TabStop = false;

            #endregion Настройка компонента

            ResumeLayout();

            Type vType = GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Полное название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion - Внутренние

        #endregion ПОЛЯ
    }
}
