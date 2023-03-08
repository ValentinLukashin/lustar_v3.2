using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormRecord'
    /// </summary>
    /// <remarks>Абстактный класс формы для изменения записи данных</remarks>
    public abstract class crlFormRecord : crlForm
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

            Controls.Add(__cAreaRecord);
            Controls.SetChildIndex(__cAreaRecord, 0);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            Text = "Базовая форма для изменения записи данных";

            // _cAreaRecord
            {
                __cAreaRecord.Dock = DockStyle.Fill;
                __cAreaRecord.__fBlockInputsCheckShow_ = false;
                __cAreaRecord.__eOnDataLoaded += __cAreaRecord___eOnDataLoaded;
                __cAreaRecord.__eOnDataSaving += __cAreaRecord___eOnDataSaving;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        /// <summary>Выполняется после загрузки данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cAreaRecord___eOnDataLoaded(object sender, EventArgs e)
        {
            if (__eOnDataLoaded != null)
                __eOnDataLoaded(__cAreaRecord, new EventArgs());
        }
        /// <summary>Выполняется перед сохранением данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cAreaRecord___eOnDataSaving(object sender, EventArgs e)
        {
            if (__eOnDataSaving != null)
                __eOnDataSaving(__cAreaRecord, new EventArgs());
        }

        /// <summary>Выполняется при нажатии на клавиши
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                __cAreaRecord.__mPressButtonHelp();
            if (e.Control == true & e.KeyCode == Keys.A)
                __cAreaRecord.__mPressButtonSave();

            base.OnKeyDown(e);
        }

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Область для правки табличных данных
        /// </summary>
        public crlAreaRecord __cAreaRecord = new crlAreaRecord();

        #endregion Компоненты

        #endregion ПОЛЯ

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает после загрузки данных
        /// </summary>
        public event EventHandler __eOnDataLoaded;
        /// <summary>
        /// Возникает перед сохранением данных
        /// </summary>
        public event EventHandler __eOnDataSaving;

        #endregion СОБЫТИЯ
    }
}
