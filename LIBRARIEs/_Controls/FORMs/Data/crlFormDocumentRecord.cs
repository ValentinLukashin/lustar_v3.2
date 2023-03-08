using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlFormDocumentRecord'
    /// </summary>
    /// <remarks>Базовый класс формы для правки записи документа</remarks>
    public abstract class crlFormDocumentRecord : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// (m) Загрузка формы
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(__cAreaDocumentRecord);
            Controls.SetChildIndex(__cAreaDocumentRecord, 0);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Text = "Базовая форма для правки записи документа";

            // _cAreaDocument
            {
                __cAreaDocumentRecord.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// (m) Выполняется при нажатии на клавиши
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                __cAreaDocumentRecord.__mPressButtonHelp();
            if (e.Control == true & e.KeyCode == Keys.A)
                __cAreaDocumentRecord.__mPressButtonSave();

            base.OnKeyDown(e);

            return;
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// (f) Область для правки документов
        /// </summary>
        public crlAreaDocumentRecord __cAreaDocumentRecord = new crlAreaDocumentRecord();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
