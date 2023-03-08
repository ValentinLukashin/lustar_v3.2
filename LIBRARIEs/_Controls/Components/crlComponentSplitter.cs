using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentSplitter'
    /// </summary>
    /// <remarks>Компонент - разделитель</remarks>
    public class crlComponentSplitter : SplitContainer
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentSplitter()
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

            #region Настройка компонентов

            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.Fixed3D;
            Dock = DockStyle.Fill;
            Orientation = Orientation.Vertical;
            TabStop = false;

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при первом отображении компонента
        /// </summary>
        protected virtual void _mObjectPresetation()
        {
            return;
        }

        /// <summary>
        /// Выполняется после создания объекта
        /// </summary>
        protected override void OnCreateControl()
        {
            _mObjectPresetation();
            base.OnCreateControl();

            return;
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
