using System;
using System.Windows.Forms;

namespace nlControls
{
    public class crlFormGridFolder : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка формы
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(__cAreaGridFolder);
            Controls.SetChildIndex(__cAreaGridFolder, 0);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Text = "Базовая форма для правки древовидных данных";

            // _cAreaGrid
            {
                __cAreaGridFolder.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }
        /// <summary>Выполняется при нажатии на клавиши
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                __cAreaGridFolder.__mPressButtonHelp();
            if (e.KeyCode == Keys.F5)
                __cAreaGridFolder.__mPressButtonRefresh();
            if (e.KeyCode == Keys.F12)
                __cAreaGridFolder.__mPressButtonColumns();
            if (e.Control == true & e.KeyCode == Keys.A)
                __cAreaGridFolder.__mPressButtonSelect();
            if (e.Control == true & e.KeyCode == Keys.E)
                __cAreaGridFolder.__mPressButtonEdit();
            if (e.Control == true & e.KeyCode == Keys.O)
                __cAreaGridFolder.__mPressButtonOperations();
            if (e.Control == true & e.KeyCode == Keys.R)
                __cAreaGridFolder.__mPressButtonReports();

            base.OnKeyDown(e);
        }

        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Загрузка настроек формы из файла
        /// </summary>
        /// <param name="pFormName">Название формы</param>
        protected override void __mTunesLoad(string pFormName)
        {
            base.__mTunesLoad(pFormName);

            #region Загрузка видимости полей

            foreach (DataGridViewColumn vGridColumn in __cAreaGridFolder.__fColumns_)
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
        /// <summary>Сохранение настроек формы в файл
        /// </summary>
        /// <param name="pFormName">Название формы</param>
        protected override void __mTunesSave(string pFormName)
        {
            base.__mTunesSave(pFormName);

            #region Сохранение видимости полей

            foreach (DataGridViewColumn vDataGridColumn in __cAreaGridFolder.__fColumns_)
            {
                __oFileIni.__mValueWrite(vDataGridColumn.Visible.ToString(), pFormName.ToUpper(), "Field_" + vDataGridColumn.Name);
            }

            #endregion Сохранение видимости полей

            /// Сохранение сортировки
            __cAreaGridFolder.__mSortingSave();
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для правки табличных данных
        /// </summary>
        public crlAreaGridFolder __cAreaGridFolder = new crlAreaGridFolder();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
