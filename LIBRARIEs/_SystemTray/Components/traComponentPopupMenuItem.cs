using System.Drawing;
using System;
using System.Windows.Forms;

namespace nlFormTray
{
    /// <summary>
    /// Класс 'crlComponentPopupMenuItem'
    /// </summary>
    /// <remarks>Пункт всплывающего меню</remarks>
    public class traComponentPopupMenuItem : ToolStripMenuItem
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public traComponentPopupMenuItem()
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
            #region /// Описание компонента

            #endregion Описание компонента

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }
        /// <summary>
        /// Презентация объекта
        /// </summary>
        protected virtual void _mObjectPresentation()
        {
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

        #endregion ПОЛЯ
    }
}
