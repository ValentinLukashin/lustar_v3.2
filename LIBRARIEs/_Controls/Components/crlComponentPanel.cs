using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentPanel'
    /// </summary>
    /// <remarks>Компонент - панель</remarks>
    public class crlComponentPanel : UserControl
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструкторы
        /// </summary>
        public crlComponentPanel()
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

            #region /// Настройка компонента

            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            TabStop = false;

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";
        }

        #endregion Объект

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #endregion ПОЛЯ
    }
}
