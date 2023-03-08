using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormReport'
    /// </summary>
    /// <remarks>Абстактный класс формы для формирования отчетов</remarks>
    public abstract class crlFormReport : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка формы
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            Text = "Базовая форма для формирования отчетов";

            #region Размещение компонентов

            Controls.Add(__cAreaReport);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // __cAreaReport
            {
                __cAreaReport.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при нажатии на клавиши
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                __cAreaReport.__mPressButtonHelp();
            if (e.Control == true & e.KeyCode == Keys.A)
                __cAreaReport.__mPressButtonRun();
            if (e.Control == true & e.KeyCode == Keys.O)
                __cAreaReport.__mPressButtonOperations();

            base.OnKeyDown(e);
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для правки табличных данных
        /// </summary>
        public crlAreaReport __cAreaReport = new crlAreaReport();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
