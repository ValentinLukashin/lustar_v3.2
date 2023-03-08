using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlFormGrid'
    /// </summary>
    /// <remarks>Абстактный класс формы для построения форм для правки табличных данных</remarks>
    public abstract class crlFormGrid : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region - Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Controls.Add(__cAreaGrid);
            Controls.SetChildIndex(__cAreaGrid, 0);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            Text = "Базовая форма для правки табличных данных";

            // _cAreaGrid
            {
                __cAreaGrid.Dock = DockStyle.Fill;
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
                __cAreaGrid.__mPressButtonHelp();
            if (e.KeyCode == Keys.F5)
                __cAreaGrid.__mPressButtonRefresh();
            if (e.KeyCode == Keys.F12)
                __cAreaGrid.__mPressButtonColumns();
            if (e.Control == true & e.KeyCode == Keys.A)
                __cAreaGrid.__mPressButtonSelect();
            if (e.Control == true & e.KeyCode == Keys.E)
                __cAreaGrid.__mPressButtonEdit();
            if (e.Control == true & e.KeyCode == Keys.O)
                __cAreaGrid.__mPressButtonOperations();
            if (e.Control == true & e.KeyCode == Keys.R)
                __cAreaGrid.__mPressButtonReports(); 

            base.OnKeyDown(e);
        }

        #endregion Объект

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка настроек формы из файла
        /// </summary>
        /// <param name="pFormName">Название формы</param>
        protected override void __mTunesLoad(string pFormName)
        {
            base.__mTunesLoad(pFormName);

            #region Загрузка видимости полей

            foreach (DataGridViewColumn vGridColumn in __cAreaGrid.__fColumns_)
            {
                string vString = __oFileIni.__mValueRead(pFormName.ToUpper(), "Field_" + vGridColumn.Name);
                try
                {
                    vGridColumn.Visible = Convert.ToBoolean(vString);
                }
                catch
                {
                    vGridColumn.Visible = true;
                }
            }

            #endregion Загрузка видимости полей
        }
        /// <summary>
        /// Сохранение настроек формы в файл
        /// </summary>
        /// <param name="pFormName">Название формы</param>
        protected override void __mTunesSave(string pFormName)
        {
            base.__mTunesSave(pFormName);

            #region Сохранение видимости полей

            foreach (DataGridViewColumn vDataGridColumn in __cAreaGrid.__fColumns_)
            {
                __oFileIni.__mValueWrite(vDataGridColumn.Visible.ToString(), pFormName.ToUpper(), "Field_" + vDataGridColumn.Name);
            }

            #endregion Сохранение видимости полей

            /// Сохранение сортировки
            __cAreaGrid.__mSortingSave();
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Область для правки табличных данных
        /// </summary>
        public crlAreaGrid __cAreaGrid = new crlAreaGrid();

        #endregion Компоненты

        #endregion ПОЛЯ
    }
}
