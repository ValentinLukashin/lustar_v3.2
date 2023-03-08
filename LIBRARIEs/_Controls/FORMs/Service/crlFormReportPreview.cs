using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormReportPreview'
    /// </summary>
    /// <remarks>Форма для предварительного просмотра отчетов</remarks>
    public class crlFormReportPreview : crlForm
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

            Controls.Add(_cAreaReportPreview);
            Controls.SetChildIndex(_cAreaReportPreview, 0);

            // _cAreaReportPreview
            {
                _cAreaReportPreview.Dock = DockStyle.Fill;
            }

            ResumeLayout(true);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для правки табличных данных
        /// </summary>
        public crlAreaReportPreview _cAreaReportPreview = new crlAreaReportPreview();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
