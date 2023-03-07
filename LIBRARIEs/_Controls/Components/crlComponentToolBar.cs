using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentToolBar'
    /// </summary>
    /// <remarks>Компонент - полоса инструментов</remarks>
    public class crlComponentToolBar : ToolStrip
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentToolBar()
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
            ImageScalingSize = new Size(32, 32);
            TabStop = false;

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }

        #endregion Объект

        #endregion Поведение

        #endregion = МЕТОДЫ

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
