using nlApplication;
using nlDataMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentGrid'
    /// </summary>
    /// <remarks>Компонент для правки табличных данных</remarks>
    public class crlComponentGrid : DataGridView
    {
        #region = БИБЛИОТЕКИ

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, Int32 lParam);

        #endregion БИБЛИОТЕКИ

        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentGrid()
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

            #region /// Настройка компонента

            AutoGenerateColumns = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToResizeRows = true;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DoubleBuffered = true; // Для ускорения перерисовки при подсветке строк
            MultiSelect = false;
            RowHeadersWidth = 25;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            VirtualMode = true; // Для выполнения сортировки связанных полей

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Презентация объекта
        /// </summary>
        protected virtual void _mObjectPresentation()
        {
            __mSortingLoad();

            return;
        }
        /// <summary>
        /// Выполняется после создания обхъекта
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }
        /// <summary>
        /// Выполняется при клике мыши по заголовку колонки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            _fRecordClueBeforeSortChanged = __fRecordClue_;
            base.OnColumnHeaderMouseClick(e);
        }
        /// <summary>
        /// Выполняется после изменения сортировки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSorted(EventArgs e)
        {
            if (CurrentRow != null)
            {
                base.OnSorted(e);
                try
                {
                    __fRecordClue_ = _fRecordClueBeforeSortChanged;
                }
                catch { }
            }
        }
        /// <summary>
        /// Выполняется при изменении данных в ячейке сетки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
            if (__eCellValueChanged != null)
                __eCellValueChanged(this, new EventArgs());
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            __mSortingSave();
            base.OnHandleDestroyed(e);
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Применить все изменения сделанные в сетке
        /// </summary>
        /// <param name="pColumnEditIndex">Номер редактируемой ячейки</param>
        public void __mAcceptChanges(int pColumnEditIndex)
        {
            int vRowIndexMax = Rows.Count - 1; // Индекс максимальной строки в сетке
            /// Перевод курсора вначале не первую ячейку последней записи, а затем на первую ячейку первой записи
            if (vRowIndexMax > 0)
            {
                DataGridViewCell vDataGridViewCell = Rows[vRowIndexMax].Cells[pColumnEditIndex];
                CurrentCell = vDataGridViewCell;
                CurrentCell.Selected = true;

                vDataGridViewCell = Rows[vRowIndexMax].Cells[pColumnEditIndex];
                CurrentCell = vDataGridViewCell;
                CurrentCell.Selected = true;
            }

            return;
        }
        /// <summary>
        /// Добавление колонки
        /// </summary>
        /// <param name="pCaption">Заголовок колонки</param>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pReadOnly">Атрибут "Только чтение"</param>
        /// <param name="pVisible">Видимость колонки</param>
        /// <param name="pType">Вид колонки</param>
        /// <returns>[true] - Колонка добавлена, иначе - [false]</returns>
        public bool __mColumnAdd(string pCaption, string pFieldName, bool pReadOnly, bool pVisible, string pType)
        {
            bool vReturn = true; // Возвращаемое значение

            crlDataGridColumn vDataGridColumn = new crlDataGridColumn();
            vDataGridColumn.__fCaption = pCaption;
            vDataGridColumn.__fField = pFieldName;
            vDataGridColumn.__fReadOnly = pReadOnly;
            vDataGridColumn.__fType = pType;
            vDataGridColumn.__fVisible = pVisible;

            __fColumnsList.Add(vDataGridColumn);

            return vReturn;
        }
        /// <summary>
        /// Изменение видимости колонок сетки
        /// </summary>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pFieldVisible">Видимость поля</param>
        /// <returns></returns>
        public void __mColumnChangeVisible(string pFieldName, bool pFieldVisible)
        {
            foreach (crlDataGridColumn vColumn in __fColumnsList)
            {
                if (vColumn.__fField == pFieldName)
                    vColumn.__fVisible = pFieldVisible;
            }

            return;
        }
        /// <summary>
        /// Добавление колонок в сетку
        /// </summary>
        /// <returns>[true] - колонки добавлены, иначе - [false]</returns>
        public bool __mColumnsBuild()
        {
            bool vReturn = true; // Возвращаемое значение
            crlForm vForm = this.FindForm() as crlForm; // Форма на которой расположен компонент

            foreach (crlDataGridColumn vColumn in __fColumnsList)
            {
                if (vColumn.__fType == "DataGridViewTextBoxColumn")
                {
                    DataGridViewTextBoxColumn vDataColumn = new DataGridViewTextBoxColumn();
                    vDataColumn.Name = vColumn.__fField;
                    if (vColumn.__fField == "clu" | vColumn.__fField == "cod" | vColumn.__fField == "des")
                        vDataColumn.Frozen = true;
                    vDataColumn.HeaderText = vColumn.__fCaption;
                    vDataColumn.DataPropertyName = vColumn.__fField;
                    vDataColumn.Visible = vColumn.__fVisible;
                    vDataColumn.ReadOnly = vColumn.__fReadOnly;
                    Columns.Add(vDataColumn);
                }
                if (vColumn.__fType == "DataGridViewCheckBoxColumn")
                {
                    DataGridViewCheckBoxColumn vDataColumn = new DataGridViewCheckBoxColumn();
                    vDataColumn.Name = vColumn.__fField;
                    vDataColumn.HeaderText = vColumn.__fCaption;
                    vDataColumn.DataPropertyName = vColumn.__fField;
                    vDataColumn.Visible = vColumn.__fVisible;
                    vDataColumn.ReadOnly = vColumn.__fReadOnly;
                    Columns.Add(vDataColumn);
                }
                if (vColumn.__fType == "DataGridViewButtonColumn")
                {
                    DataGridViewButtonColumn vDataColumn = new DataGridViewButtonColumn();
                    vDataColumn.Name = vColumn.__fField;
                    vDataColumn.HeaderText = vColumn.__fCaption;
                    vDataColumn.DataPropertyName = vColumn.__fField;
                    vDataColumn.Visible = vColumn.__fVisible;
                    vDataColumn.ReadOnly = vColumn.__fReadOnly;
                    Columns.Add(vDataColumn);
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="pRecordClue">Идентификатор записи который должен быть отображен</param>
        public virtual bool __mDataLoad(int pRecordClue)
        {
            return false;
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="pExpressionWhere">Условия выбора данных</param>
        /// <param name="pExpressionOrder">Поле сортировки данных </param>
        /// <param name="pRecordCount">Количество возвращаемых данных. -1 - все данные</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mDataLoad(string pExpressionWhere, string pExpressionOrder, int pRecordCount)
        {
            bool vReturn = true; // Возвращаемое значение
            int vCurrentRowIndex = -1; // Индекс текущей записи
            /// > Чтение фильтра из файла

            //https://overcoder.net/q/585303/c-%D0%BE%D1%87%D0%B5%D0%BD%D1%8C-%D0%BC%D0%B5%D0%B4%D0%BB%D0%B5%D0%BD%D0%BD%D0%BE-%D0%B7%D0%B0%D0%BF%D0%BE%D0%BB%D0%BD%D1%8F%D0%B5%D1%82-datagridview

            string vFilter = __fFilterConstant; // Фильтр для выбора данных из источника данных

            /// > Если указан загруженный фильтр
            if (_fFilterExpression.Length > 0)
            {
                /// > Если фильтр уже частично заполнен
                if (vFilter.Length > 0)
                { 
                    vFilter = vFilter + " and " + _fFilterExpression;
                }
                else
                { /// > Иначе
                    vFilter = _fFilterExpression;
                }
            }

            if (pExpressionWhere.Length > 0)
            {
                if (vFilter.Length > 0)
                {
                    vFilter = vFilter + " and " + pExpressionWhere;
                } /// Фильтр уже частично заполнен
                else
                {
                    vFilter = pExpressionWhere;
                } /// Фильтр не заполнен
            } /// Указан фильтр в параметрах метода

            /// Если есть выделенная строка, Сохраняем ее индекс
            if (CurrentRow != null)
            {
                vCurrentRowIndex = CurrentRow.Index; // Индекс текущей записи
            }

            if (__oEssence != null)
            {
                //AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                //SendMessage(Handle, WM_SETREDRAW, false, 0); // before

                __oDataTable = __oEssence.__mGrid(vFilter, pExpressionOrder);
                DataSource = __oDataTable;

                //SendMessage(Handle, WM_SETREDRAW, true, 0); // after
                //AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                //AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                appUnitError vError = new appUnitError();
                vError.__fMessage_ = "Источник данных не определен";
                vError.__fProcedure = _fClassNameFull + "_DataLoad(string, string, int)";
                vError.__fErrorsType = ERRORSTYPES.Programming;
                appApplication.__oErrorsHandler.__mShow(vError);

                return false;
            }
            /// Если была выбрана запись, возвращаем курсор на нее
            if (vCurrentRowIndex > 0)
            {
                /// Если индекс предыдущей записи больше или равно количеству записей в сетке
                if (Rows.Count > vCurrentRowIndex)
                {
                    CurrentCell = Rows[vCurrentRowIndex].Cells.OfType<DataGridViewCell>().First(c => c.Visible);
                }
            }

            Focus();

            return vReturn;
        }
        /// <summary>
        /// Обновление сетки с условиями фильтра
        /// </summary>
        /// <param name="pFilter"></param>
        public void __mRefresh(string pFilter)
        {
            __oDataTable.DefaultView.RowFilter = pFilter;

            return;
        }
        /// <summary>
        /// Удаление данных
        /// </summary>
        /// <returns>[true] - данные удалены, иначе - [false]</returns>
        public bool __mRecordDelete()
        {
            bool vReturn = false; // Возвращаемое значение

            if (CurrentRow != null)
            {
                DataTable vDataTable = DataSource as DataTable;
                int vRowIndex = SelectedRows[0].Index;
                vDataTable.Rows[vRowIndex]["LCK"] = 1;
                vReturn = __oEssence.__mUpdate(vDataTable);
            }

            return vReturn;
        }
        /// <summary>
        /// Восстановление данных
        /// </summary>
        /// <returns></returns>
        public bool __mRecordRestore()
        {
            bool vReturn = false; // Возвращаемое значение

            if (CurrentRow != null)
            {
                DataTable vDataTable = DataSource as DataTable;
                int vRowIndex = SelectedRows[0].Index;
                vDataTable.Rows[vRowIndex]["LCK"] = 0;
                vReturn = __oEssence.__mUpdate(vDataTable);
            }

            return vReturn;
        }
        /// <summary>
        /// Выполнение сортировки
        /// </summary>
        /// <param name="pColumnIndex">Индекс колонки по которой должна быть выполнена сортировка</param>
        /// <param name="pDirection">Направление сортироки ASCE, DESC</param>
        public void __mSorting(int pColumnIndex, string pDirection)
        {
            Columns[pColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic; // Установка программного режима сортировки

            if (pDirection.Substring(0, 4).ToUpper() == "ASCE")
                Sort(Columns[pColumnIndex], ListSortDirection.Ascending);
            if (pDirection.Substring(0, 4).ToUpper() == "DESC")
                Sort(Columns[pColumnIndex], ListSortDirection.Descending);

            Columns[pColumnIndex].SortMode = DataGridViewColumnSortMode.Automatic; // Установка пользовательского режима сортировки

            return;
        }
        /// <summary>
        /// Загрузка сортировки в сетку
        /// </summary>
        public void __mSortingLoad()
        {
            appFileIni vFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с инициализационными файлами
            vFileIni.__fFilePath = crlApplication.__oPathes.__mFileFormTunes(); // Указание настроечного файла
            string vFormName = (FindForm() as crlForm).Name; // Название формы на которой расположен компонент

            try
            {
                string vSortColumnNumber = vFileIni.__mValueRead(vFormName, "SortColumnIndex"); // Номер колонки по которой будет выполнена сортировка
                string vSortDirection = vFileIni.__mValueRead(vFormName, "SortDirection"); // Направление сортировки в колонке
                if (Convert.ToInt32(vSortColumnNumber) > 0)
                    __mSorting(Convert.ToInt32(vSortColumnNumber), vSortDirection);
            }
            catch { }
            try
            {
                __fRecordClue_ = Convert.ToInt32(vFileIni.__mValueRead(vFormName, "LastClue")); // Идентификатор последней выбранной записи
            }
            catch { }

            return;
        }
        /// <summary>
        /// Сохранение сортировки в сетке
        /// </summary>
        public void __mSortingSave()
        {
            appFileIni vFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с инициализационными файлами
            vFileIni.__fFilePath = crlApplication.__oPathes.__mFileFormTunes(); // Указание настроечного файла
            string vFormName = (FindForm() as crlForm).Name; // Название формы на которой расположен компонент

            vFileIni.__mValueWrite(__fSortColumnIndex_.ToString(), vFormName, "SortColumnIndex");
            vFileIni.__mValueWrite(__fSortColumnDirection_, vFormName, "SortDirection");
            vFileIni.__mValueWrite(__fRecordClue_.ToString(), vFormName, "LastClue"); // Идентификатор последней выбранной записи

            return;
        }
        /// <summary>
        /// Получение значения поля курсора в текущей ячейке
        /// </summary>
        /// <param name="pFieldName">Название поля курсора</param>
        /// <returns></returns>
        public object __mCurrentRowFieldValue(string pFieldName)
        {
            return ((DataRowView)this.SelectedRows[0].DataBoundItem).Row[pFieldName];
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Список отображаемых колонок
        /// </summary>
        public List<crlDataGridColumn> __fColumnsList = new List<crlDataGridColumn>();
        /// <summary>
        /// Вид формы вызываемой для правки записи
        /// </summary>
        public FORMOPENEDTYPES __fFormOpenedType = FORMOPENEDTYPES.FormRecord;
        /// <summary>
        /// Постоянно подключенный фильтр. Например тема файлов
        /// </summary>
        public string __fFilterConstant = "";
        /// <summary>
        /// Курсор с данными
        /// </summary>
        public DataTable __oDataTable = null;

        #endregion Атрибуты

        #region = Внутренние 

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";
        /// <summary>
        /// Выражения фильтра данных
        /// </summary>
        protected string _fFilterExpression = "";
        /// <summary>
        /// Идентификатор записи перед изменением сортировки
        /// </summary>
        protected int _fRecordClueBeforeSortChanged = 0;

        #endregion Внутренние

        #region = Константы

        private const int WM_SETREDRAW = 11;

        #endregion Константы

        #region = Объекты

        /// <summary>
        /// Сушность данных
        /// </summary>
        public datUnitEssence __oEssence;
        /// <summary>
        /// Вид формы для построения фильтра
        /// </summary>
        public Type __oFormFilter;
        /// <summary>
        /// Вид формы для изменения записи
        /// </summary>
        public Type __oFormOpened;

        #endregion Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Идентификатор текущей записи
        /// </summary>
        public int __fRecordClue_
        {
            get
            {
                Select();

                if (Rows.Count > 0 & CurrentRow != null)
                    return Convert.ToInt32(this[1, CurrentRow.Index].Value.ToString());
                else
                    return -1;
            }
            set
            {
                Select();

                if (value > 0)
                {
                    if (Columns.Count > 0)
                    {
                        try
                        {
                            foreach (DataGridViewRow vGridRow in Rows)
                            {
                                foreach (DataGridViewColumn vGridColumn in Columns)
                                {
                                    if (vGridColumn.Visible == true & (Int32)vGridRow.Cells[0].Value == value)
                                    {
                                        CurrentCell = vGridRow.Cells[1];
                                        break;
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        /// <summary>
        /// Индекс колонки по которой выполнена сортировка
        /// </summary>
        public int __fSortColumnIndex_
        {
            get
            {
                if (SortedColumn != null)
                    return SortedColumn.Index;
                else
                    return 0;
            }
        }
        /// <summary>
        /// Направление сортировки в колонке по которой выполнена сортировка
        /// </summary>
        public string __fSortColumnDirection_
        {
            get
            {
                if (SortedColumn != null)
                    return SortedColumn.HeaderCell.SortGlyphDirection.ToString();
                else
                    return "Ascending";
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при изменении данных в ячейке сетки
        /// </summary>
        public event EventHandler __eCellValueChanged;

        #endregion СОБЫТИЯ
    }

    /// <summary>
    /// Класс 'DataGridView'
    /// </summary>
    public class crlDataGridColumn
    {
        #region = ПОЛЯ 

        #region = Атрибуты

        /// <summary>
        /// Заголовок колонки
        /// </summary>
        public string __fCaption = "";
        /// <summary>
        /// Доступность колонки для редактирования
        /// </summary>
        public bool __fReadOnly = true;
        /// <summary>
        /// Название поля
        /// </summary>
        public string __fField = "";
        /// <summary>
        /// Видимость колонки
        /// </summary>
        public bool __fVisible = true;
        /// <summary>
        /// Объект для отображения данных
        /// </summary>
        public string __fType = "DataGridViewTextBoxColumn";

        #endregion Атрибуты

        #endregion ПОЛЯ
    }
}
