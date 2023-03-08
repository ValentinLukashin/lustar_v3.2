using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentPage'
    /// </summary>
    /// <remarks>Компонент - вкладка</remarks>
    public class crlComponentPage : TabPage
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentPage()
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

            #region Размешение компонентов

            Controls.Add(__cPanel);

            #endregion Размещение компонентов

            #region Настройка компонента

            // __cPanel
            {
                __cPanel.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонента

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при изменении видимости вкладки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if(Visible == true)
                __cPanel.BackColor = crlApplication.__oInterface.__mColor(COLORS.FormActive);
            else
                __cPanel.BackColor = crlApplication.__oInterface.__mColor(COLORS.FormDeactive);
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

        #region - Компоненты

        /// <summary>
        /// Цветовая панель
        /// </summary>
        public crlComponentPanel __cPanel = new crlComponentPanel();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
