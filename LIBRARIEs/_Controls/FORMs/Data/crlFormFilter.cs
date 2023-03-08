using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormFilter'
    /// </summary>
    /// <remarks>Абстактный класс формы для построения фильтров</remarks>
    public class crlFormFilter : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Controls.Add(__cAreaFilter);
            Controls.SetChildIndex(__cAreaFilter, 0);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            Text = "Базовая форма для построения фильтров";
            
            // _cAreaFilter
            {
                __cAreaFilter.Dock = DockStyle.Fill;
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
                __cAreaFilter.__mPressButtonHelp();
            if (e.Control == true & e.KeyCode == Keys.A)
                __cAreaFilter.__mPressButtonApply();

            base.OnKeyDown(e);

            return;
        }

        #endregion Поведение

        protected override void __mTunesSave()
        {
            base.__mTunesSave();
            /// Сохранение текущих настроек в файл
            __cAreaFilter.__mFilterSave();
        }

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Область для построения фильтра
        /// </summary>
        public crlAreaFilter __cAreaFilter = new crlAreaFilter();

        #endregion Компоненты

        #endregion ПОЛЯ
    }
}
