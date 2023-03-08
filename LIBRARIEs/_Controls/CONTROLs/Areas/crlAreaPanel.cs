using nlApplication;
using nlDataMaster;
using nlReports;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlAreaGrid'
    /// </summary>
    /// <remarks>Область для правки данных отображенных на панели</remarks>
    public class crlAreaPanel : crlArea
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

            __cToolBar.Items.Insert(0, _cButtonSelect);
            __cToolBar.Items.Insert(1, _cButtonRefresh);
            __cToolBar.Items.Add(_cButtonReports);
            __cToolBar.Items.Add(_cButtonOperations);
            __cToolBar.Items.Add(_cButtonEdit);

            _cButtonEdit.DropDownItems.Add(_cButtonEditCreate);
            _cButtonEdit.DropDownItems.Add(_cButtonEditCopy);
            _cButtonEdit.DropDownItems.Add(_cButtonEditEdit);
            _cButtonEdit.DropDownItems.Add(_cButtonEditRemove);
            _cButtonEdit.DropDownItems.Add(_cButtonEditRestore);

            _cButtonOperations.DropDownItems.Add(_cButtonOperationsAccess);

            _cButtonReports.DropDownItems.Add(_cButtonReportsCurrentList);
            _cButtonReports.DropDownItems.Add(_cButtonReportsHistory);

            Panel2.Controls.Add(_cSplitterFilterGrid);
            Panel2.Controls.SetChildIndex(_cSplitterFilterGrid, 0);

            _cSplitterFilterGrid.Panel1.Controls.Add(_cLabelFilterCaption);
            _cSplitterFilterGrid.Panel1.Controls.Add(_cLabelFilterExpression);

            _cSplitterFilterGrid.Panel2.Controls.Add(_cGrid);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            // __cButtonEdit
            {
                _cButtonEdit.Alignment = ToolStripItemAlignment.Right;
                _cButtonEdit.DropDownOpened += cButton_DropDownOpened;
                _cButtonEdit.Image = global::nlResourcesImages.Properties.Resources._PageEdit_y32;
                _cButtonEdit.ToolTipText = "[ Ctrl + E ] " + crlApplication.__oTunes.__mTranslate("Правка");
                {
                    _cButtonEditCreate.Click += mButtonEditNew_Click;
                    _cButtonEditCreate.Image = global::nlResourcesImages.Properties.Resources._Page_w16C;
                    _cButtonEditCreate.__fCaption_ = "Создать";

                    _cButtonEditCopy.Click += mButtonEditCopy_Click;
                    _cButtonEditCopy.Image = global::nlResourcesImages.Properties.Resources._PageCopy_w16;
                    _cButtonEditCopy.__fCaption_ = "Копировать";

                    _cButtonEditEdit.Click += mButtonEditEdit_Click;
                    _cButtonEditEdit.Image = global::nlResourcesImages.Properties.Resources._PageEdit_w16;
                    _cButtonEditEdit.__fCaption_ = "Изменить";

                    _cButtonEditRemove.Click += mButtonEditRemove_Click;
                    _cButtonEditRemove.Image = global::nlResourcesImages.Properties.Resources._PageRemove_w16;
                    _cButtonEditRemove.__fCaption_ = "Удалить";

                    _cButtonEditRestore.Click += mButtonEditRestore_Click;
                    _cButtonEditRestore.Image = global::nlResourcesImages.Properties.Resources._PageRestore_w16;
                    _cButtonEditRestore.__fCaption_ = "Восстановить";
                }
            }
            // __cButtonOperations
            {
                _cButtonOperations.Alignment = ToolStripItemAlignment.Right;
                _cButtonOperations.DropDownOpened += cButton_DropDownOpened;
                _cButtonOperations.Image = global::nlResourcesImages.Properties.Resources._PageGear_y32;
                _cButtonOperations.ToolTipText = "[ Ctrl + O] " + crlApplication.__oTunes.__mTranslate("Операции");
                {
                    _cButtonOperationsAccess.Click += mButtonOperationsAccess_Click;
                    _cButtonOperationsAccess.Image = global::nlResourcesImages.Properties.Resources._UserEdit_b16;
                    _cButtonOperationsAccess.__fCaption_ = "Определение прав пользователей";
                }
            }
            // __cButtonRefresh
            {
                _cButtonRefresh.Click += mButtonRefresh_Click;
                _cButtonRefresh.__eMouseClickRight += mButtonRefresh_eMouseClickRight;
                _cButtonRefresh.Image = global::nlResourcesImages.Properties.Resources._SignRefresh_b32C;
                _cButtonRefresh.ToolTipText = "[ F5 ] " + crlApplication.__oTunes.__mTranslate("Обновить");
            }
            // __cButtonReports
            {
                _cButtonReports.Alignment = ToolStripItemAlignment.Right;
                _cButtonReports.DropDownOpened += cButton_DropDownOpened;
                _cButtonReports.Image = global::nlResourcesImages.Properties.Resources._PageInBox_y32C;
                _cButtonReports.ToolTipText = "[ Ctrl + R ] " + crlApplication.__oTunes.__mTranslate("Отчеты");
                {
                    // _cButtonReportsCurrentList
                    {
                        _cButtonReportsCurrentList.Click += mButtonReportsCurrentList_Click;
                        _cButtonReportsCurrentList.Image = global::nlResourcesImages.Properties.Resources._FolderTree_y16;
                        _cButtonReportsCurrentList.__fCaption_ = "Текущий список";
                    }
                    // _cButtonReportsHistory
                    {
                        _cButtonReportsHistory.Click += mButtonReportsHistory_Click;
                        _cButtonReportsHistory.Image = global::nlResourcesImages.Properties.Resources._FolderTree_y16;
                        _cButtonReportsHistory.__fCaption_ = "История корректировок";
                    }
                }
            }
            // __cButtonSelect
            {
                _cButtonSelect.Click += mButtonSelect_Click;
                _cButtonSelect.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                _cButtonSelect.ToolTipText = "[ Ctrl + S] " + crlApplication.__oTunes.__mTranslate("Выбрать");
            }
            // _cSplitterFilterGrid
            {
                _cSplitterFilterGrid.SplitterDistance = 20;
                _cSplitterFilterGrid.Dock = DockStyle.Fill;
                _cSplitterFilterGrid.Orientation = Orientation.Horizontal;
                _cSplitterFilterGrid.IsSplitterFixed = true;
                _cSplitterFilterGrid.FixedPanel = FixedPanel.Panel1;
            }
            // __cLabelFilterCaption
            {
                _cLabelFilterCaption.Location = new Point(crlInterface.__fIntervalHorizontal, crlInterface.__fIntervalVertical);
                _cLabelFilterCaption.__fCaption_ = "Показаны данные для:";
            }
            // __cLabelFilterExpression
            {
                _cLabelFilterExpression.Location = new Point(crlInterface.__fIntervalHorizontal * 2
                    , _cLabelFilterCaption.Top
                    + _cLabelFilterCaption.Height
                    + crlInterface.__fIntervalVertical);
            }
            // __cGrid
            {
                _cGrid.Dock = DockStyle.Fill;
                _cGrid.CellDoubleClick += mGrid_CellDoubleClick;
                _cGrid.KeyDown += mGrid_KeyDown;
            }

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при нажатии клавиши при фокусе находящемся в _cGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    if (__fButtonSelectVisible_ == true)
                        mGrid_CellDoubleClick(null, null);
                    break;
            }

            return;
        }
        /// <summary>
        /// Выполняется при двойном клике по ячейке сетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            (FindForm() as crlFormGrid).Close();

            return;
        }

        #region = Кнопки управления

        #region Внешние нажатия на кнопки управления

        /// <summary>
        /// Выполняется при выборе кнопки 'Выбрать'
        /// </summary>
        public void __mPressButtonSelect()
        {
            _cButtonSelect.PerformClick();

            return;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Обновить'
        /// </summary>
        public void __mPressButtonRefresh()
        {
            _cButtonRefresh.PerformClick();

            return;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Правка'
        /// </summary>
        public void __mPressButtonEdit()
        {
            _cButtonEdit.ShowDropDown();

            return;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Операции'
        /// </summary>
        public void __mPressButtonOperations()
        {
            _cButtonOperations.ShowDropDown();

            return;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Отчеты'
        /// </summary>
        public void __mPressButtonReports()
        {
            _cButtonReports.ShowDropDown();

            return;
        }

        #endregion Внешние нажатия на кнопки управления

        /// <summary>
        /// Выполняется при открытии меню кнопки 'Правка'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cButton_DropDownOpened(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
        }
        /// <summary>
        /// Изменение статуса видимости любой колонки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tbtFieldsVisible_CheckedChanged(object sender, System.EventArgs e)
        {
            if (_cGrid.Columns[(sender as ToolStripMenuItem).Name] != null)
            {
                _cGrid.Columns[(sender as ToolStripMenuItem).Name].Visible = (sender as ToolStripMenuItem).Checked; /// Исправление видимости колонки в сетке
                _cGrid.__mColumnChangeVisible((sender as ToolStripMenuItem).Name, (sender as ToolStripMenuItem).Checked); /// Исправление видимости колонки в настройках сетки
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Правка / Создать' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditNew_Click(object sender, EventArgs e)
        {
            crlForm vForm = FindForm() as crlForm;
            if (vForm != null & __oFormOpened != null)
            {
                /// Вызов формы редактирования документа
                if (__fFormOpenedType == FORMOPENEDTYPES.FormDocument)
                {
                    crlFormDocument vFormDocument = (crlFormDocument)Activator.CreateInstance(__oFormOpened);
                    (vFormDocument as crlFormDocument).ShowDialog();
                }
                /// Вызов формы редактирования записи
                if (__fFormOpenedType == FORMOPENEDTYPES.FormRecord)
                {
                    crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                    (vFormRecord as crlFormRecord).ShowDialog();
                }
                __mDataLoad(); /// Перегрузка данных
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Правка / Создать' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditCopy_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            crlForm vForm = FindForm() as crlForm;
            if (vForm != null & __oFormOpened != null)
            {
                /// Вызов формы редактирования документа
                if (__fFormOpenedType == FORMOPENEDTYPES.FormDocument)
                {
                    crlFormDocument vFormDocument = (crlFormDocument)Activator.CreateInstance(__oFormOpened);
                    (vFormDocument as crlFormDocument).__cAreaDocument.__fRecordClueForCopy = _cGrid.__fRecordClue_;
                    (vFormDocument as crlFormDocument).ShowDialog();
                }
                /// Вызов формы редактирования записи
                if (__fFormOpenedType == FORMOPENEDTYPES.FormRecord)
                {
                    crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                    (vFormRecord as crlFormRecord).__cAreaRecord.__fRecordClueForCopy = _cGrid.__fRecordClue_;
                    (vFormRecord as crlFormRecord).ShowDialog();
                }
                __mDataLoad(); /// Перегрузка данных
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Правка / Изменить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditEdit_Click(object sender, EventArgs e)
        {
            if (__eButtonEditEditClick != null)
                __eButtonEditEditClick(sender, e);

            _fDropDownOpened = true;

            if (__fEditLock == false)
            {
                crlForm vForm = FindForm() as crlForm;

                if (vForm != null & __oFormOpened != null)
                {
                    /// Вызов формы редактирования документа
                    if (__fFormOpenedType == FORMOPENEDTYPES.FormDocument)
                    {
                        crlFormDocument vFormDocument = (crlFormDocument)Activator.CreateInstance(__oFormOpened);
                        vFormDocument.__cAreaDocument.__fRecordClue = _cGrid.__fRecordClue_;
                        (vFormDocument as crlFormDocument).ShowDialog();
                    }
                    /// Вызов формы редактирования записи
                    if (__fFormOpenedType == FORMOPENEDTYPES.FormRecord)
                    {
                        crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                        vFormRecord.__cAreaRecord.__fRecordClue = _cGrid.__fRecordClue_;
                        (vFormRecord as crlFormRecord).ShowDialog();
                    }
                    __mDataLoad(); /// Перегрузка данных
                }
            }
            else
                __fEditLock = false;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Правка / Удалить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditRemove_Click(object sender, EventArgs e)
        {
            if (crlApplication.__oMessages.__mShow(MESSAGESTYPES.Question, "Удалить запись №" + _cGrid.__mCurrentRowFieldValue("cod" + _cGrid.__oEssence.__fTableName.Trim())) == DialogResult.Yes)
            {
                _cGrid.__mRecordDelete();
                __mDataLoad();
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Правка / Восстановить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditRestore_Click(object sender, EventArgs e)
        {
            _cGrid.__mRecordRestore();
            __mDataLoad();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Операции / Определение прав пользователей'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonOperationsAccess_Click(object sender, EventArgs e)
        {
            if (__eButtonUsersAccessClick != null)
                __eButtonUsersAccessClick(this, new EventArgs());
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Обновить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonRefresh_Click(object sender, EventArgs e)
        {
            crlForm vForm = FindForm() as crlForm;
            if (vForm != null & __oFormFilter != null)
            {
                __mSortingSave();
                crlFormFilter vFormFilter = (crlFormFilter)Activator.CreateInstance(__oFormFilter);
                vFormFilter.__cAreaFilter._fFormNameParent = FindForm().Name;

                (vFormFilter as crlFormFilter).ShowDialog();
                __mDataLoad(); // Перегрузка данных
            }
            else
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Форма для построения фильтра не определена");
                vError.__fProcedure = _fClassNameFull + "_cButtonRefresh_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Обновить' правой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonRefresh_eMouseClickRight(object sender, EventArgs e)
        {
            string vFormParentName = (FindForm() as crlForm).Name; // Название формы на которой расположен компонент
            appFileIni vFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с инициализационными файлами
            vFileIni.__fFilePath = crlApplication.__oPathes.__mFileFormTunes(); // Указание настроечного файла
            ArrayList vParametersList = new ArrayList(); // Список параметров в конфигурационном файле для текущей формы
            vParametersList = vFileIni.__mParametersList(vFormParentName);

            /// Перебор параметров в секции формы
            foreach (string vParameter in vParametersList)
            {
                /// Чтение статуса условия фильтра
                if (vParameter.StartsWith("FilterStatus") == true)
                {
                    vFileIni.__mValueWrite("False", vFormParentName, vParameter); /// Сброс статуса использования 
                }

            }
            __mDataLoad();
            (FindForm() as crlForm).__cStatus.__fCaption_ = "Фильтр сброшен";
        }
        /// <summary>
        /// Выполняется при выборе меню "Отчеты/Текущий список"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonReportsCurrentList_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            if (_fButtonReportsListDefaultCode == true)
            {
                rrtReport vReport = new rrtReport();
                vReport.__mCreate();
                vReport.__fTitle = crlApplication.__oTunes.__mTranslate("Список '{0}'", (FindForm() as crlFormGrid).Text);
                vReport.__fColumnsCountInReport = 0;

                /// Подсчет отображаемых колонок
                for (int vColumnNumber = 0; vColumnNumber < _cGrid.Columns.Count; vColumnNumber++)
                {
                    if (vColumnNumber != _cGrid.Columns.Count)
                        if (_cGrid.Columns[vColumnNumber].Visible == true)
                            vReport.__fColumnsCountInReport++;
                }
                /// Отображение заголовка
                vReport.__mRow();
                vReport.__mCell(vReport.__fTitle, "CL=Caption", "SC=" + vReport.__fColumnsCountInReport.ToString());
                vReport.__mRow();
                vReport.__mCell(_cLabelFilterCaption.Text, "SC=" + vReport.__fColumnsCountInReport.ToString(), "CL=TimeUser");
                vReport.__mRow();
                vReport.__mCell(_cLabelFilterExpression.Text.Replace("\n", "<BR>"), "SC=" + vReport.__fColumnsCountInReport.ToString(), "CL=TimeUser");
                vReport.__mRowEmpty();
                vReport.__mTime("CL=TimeUser");
                vReport.__mUser(crlApplication.__oData.__mUserAlias(), "CL=TimeUser");
                vReport.__mRowEmpty();

                /// Построение заголовка таблицы
                vReport.__mRow();
                for (int vColumnNumber = 0; vColumnNumber < _cGrid.Columns.Count; vColumnNumber++)
                {
                    if (vColumnNumber != _cGrid.Columns.Count)
                        if (_cGrid.Columns[vColumnNumber].Visible == true)
                            vReport.__mCell(_cGrid.Columns[vColumnNumber].HeaderCell.Value, "CL=HeaderCell");
                        else
                            if (_cGrid.Columns[vColumnNumber].Visible == true)
                            vReport.__mCell(_cGrid.Columns[vColumnNumber].HeaderCell.Value, "CL=HeaderCell-Last");
                }
                /// Отображение данных
                /// Перебор строк в курсоре
                foreach (DataGridViewRow vViewRow in _cGrid.Rows)
                {
                    vReport.__mRow();
                    /// Перебор полей
                    for (int vColumnNumber = 0; vColumnNumber < _cGrid.Columns.Count; vColumnNumber++)
                    {
                        if (vColumnNumber != _cGrid.Columns.Count)
                            if (_cGrid.Columns[vColumnNumber].Visible == true)
                                if (vViewRow.Cells[vColumnNumber].Value != null)
                                {
                                    if (vViewRow.Cells[vColumnNumber].Value.GetType() == typeof(bool))
                                        vReport.__mCell(Convert.ToBoolean(vViewRow.Cells[vColumnNumber].Value) == true ? crlApplication.__oTunes.__mTranslate("Да") : crlApplication.__oTunes.__mTranslate("Нет"), "CL=DataCell");
                                    else
                                        vReport.__mCell(vViewRow.Cells[vColumnNumber].Value.ToString(), "CL=DataCell");
                                }
                                else
                                    if (_cGrid.Columns[vColumnNumber].Visible == true)
                                    if (vViewRow.Cells[vColumnNumber].Value != null)
                                    {
                                        if (vViewRow.Cells[vColumnNumber].Value.GetType() == typeof(bool))
                                            vReport.__mCell(Convert.ToBoolean(vViewRow.Cells[vColumnNumber].Value) == true ? crlApplication.__oTunes.__mTranslate("Да") : crlApplication.__oTunes.__mTranslate("Нет"), "CL=DataCell-Last");
                                        else
                                            vReport.__mCell(vViewRow.Cells[vColumnNumber].Value.ToString(), "CL=DataCell-Last");
                                    }
                    }
                }

                vReport.__mFile();
                crlFormReportPreview vFormReportPreview = new crlFormReportPreview();
                vFormReportPreview._cAreaReportPreview._fUrl = vReport.__fFilePath;
                vFormReportPreview.ShowDialog();
            }
            else
            {
                if (__eButtonReportsListClick != null)
                    __eButtonReportsListClick(_cGrid, new EventArgs());
            }
        }
        /// <summary>
        /// Выполняется при выборе меню "Отчеты/История"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonReportsHistory_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            DataTable vDataTable = _cGrid.__oEssence._mRecordChanges(_cGrid.__fRecordClue_);
            /// Выполнение обратной сортировки истории
            DataView vDataView = vDataTable.DefaultView;
            vDataView.Sort = "dtmDatLck desc"; // Обратная сортировка по времени корректировки
            vDataTable = vDataView.ToTable();
            string vTableName = _cGrid.__oEssence.__fTableName; // Название текущей таблицы
            rrtReport vReport = new rrtReport();
            vReport.__mCreate();
            vReport.__fTitle = crlApplication.__oTunes.__mTranslate("Таблица '{0}'", __oEssence_.__fTableName);

            /// Отображение заголовка
            vReport.__mRow();
            vReport.__mCell(vReport.__fTitle, "CL=Caption", "SC=" + vReport.__fColumnsCountInReport.ToString(), "A=center", "SC=Max");
            vReport.__mRow();
            vReport.__mCell(crlApplication.__oTunes.__mTranslate("История записи '{0}' id={1}", (FindForm() as crlFormGrid).Text, _cGrid.__fRecordClue_), "SC=Max");
            vReport.__fColumnsCountInReport = vDataTable.Columns.Count;
            vReport.__mRowEmpty();

            /// Построение заголовка таблицы
            vReport.__mRow();
            for (int vColumnNumber = 0; vColumnNumber < vDataTable.Columns.Count; vColumnNumber++)
            {
                if (vColumnNumber != vDataTable.Columns.Count)
                {
                    switch (vDataTable.Columns[vColumnNumber].ColumnName)
                    {
                        case "CHG":
                            break;
                        case "desUsrChg":
                            vReport.__mCell(crlApplication.__oTunes.__mTranslate("Пользователь"), "CL=HeaderCell");
                            break;
                        case "dtmDatLck":
                            vReport.__mCell(crlApplication.__oTunes.__mTranslate("Время"), "CL=HeaderCell");
                            break;
                        case "lnkDatLck":
                            vReport.__mCell(crlApplication.__oTunes.__mTranslate("Блокировка"), "CL=HeaderCell");
                            break;
                        default:
                            string vCaption = crlApplication.__oData._mModelFieldDescription(__oEssence_.__fTableName, vDataTable.Columns[vColumnNumber].ColumnName);
                            if (vCaption.Trim().Length > 0)
                                vReport.__mCell(vCaption, "CL=HeaderCell");
                            else
                                vReport.__mCell(vDataTable.Columns[vColumnNumber].ColumnName, "CL=HeaderCell");
                            break;
                    }
                }
            }

            /// Отображение данных
            /// Перебор строк в курсоре
            foreach (DataRow vDataRow in vDataTable.Rows)
            {
                vReport.__mRow();
                foreach (DataColumn vDataColumn in vDataTable.Columns)
                {
                    if (vDataColumn.ColumnName == "CHG")
                        continue;
                    if (Convert.ToString(vDataRow["dtmDatLck"]).Length > 0)
                    {
                        if (vDataRow[vDataColumn.ColumnName].GetType() == typeof(bool))
                            vReport.__mCell(Convert.ToBoolean(vDataRow[vDataColumn.ColumnName]) == true ? crlApplication.__oTunes.__mTranslate("Да") : crlApplication.__oTunes.__mTranslate("Нет"), "CL=DataCell");
                        else
                            vReport.__mCell(vDataRow[vDataColumn.ColumnName], "CL=DataCell");
                    }
                }
            }

            vReport.__mFile();
            crlFormReportPreview vFormReportPreview = new crlFormReportPreview();
            vFormReportPreview._cAreaReportPreview._fUrl = vReport.__fFilePath;
            vFormReportPreview.ShowDialog();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Выбрать' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonSelect_Click(object sender, EventArgs e)
        {
            (FindForm() as crlForm).Close();
        }

        #endregion Кнопки управления

        #endregion Поведение

        #region - Процедуры

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
            return _cGrid.__mColumnAdd(pCaption, pFieldName, pReadOnly, pVisible, pType);
        }
        /// <summary>
        /// Добавление колонок в сетку
        /// </summary>
        /// <returns>[true] - колонки добавлены, иначе - [false]</returns>
        public bool __mGridBuild()
        {
            bool vReturn = _cGrid.__mColumnsBuild();
            mMenuFieldFill();
            return vReturn;
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <returns>[true] - данные загружены без ошибок, иначе - [false]</returns>
        public bool __mDataLoad()
        {
            if (__eDataLoadBefore != null)
                __eDataLoadBefore(_cGrid, new EventArgs());

            bool vReturn = false; // Возвращаемое значение
            string vFilterExpression = ""; // Условия фильтра формы
            string vFormParentName = (FindForm() as crlForm).Name; // Название формы на которой расположен компонент
            appFileIni vFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с инициализационными файлами
            ArrayList vParametersList = new ArrayList(); // Список параметров в конфигурационном файле для текущей формы
            string vFilterMessage = ""; // Выражение отображения фильтра
            int vCurrentRowIndex = -1; // Индекс строки выбранной в текущий момент

            if (_cGrid.CurrentRow != null) // Определение индекса текущей строки
                vCurrentRowIndex = _cGrid.CurrentRow.Index;

            /// Получение списка параметров в секции формы 
            vParametersList = vFileIni.__mParametersList(vFormParentName);

            bool vUsed = true;

            /// Перебор параметров в секции формы
            foreach (string vParameter in vParametersList)
            {
                if (vParameter.StartsWith("FilterStatus") == true)
                {
                    if (Convert.ToBoolean(vFileIni.__mValueRead(vFormParentName, vParameter)) == false)
                        vUsed = false;
                    else
                        vUsed = true;
                } // Чтение статуса условия фильтра
                if (vUsed == true)
                {
                    if (vParameter.StartsWith("FilterExpression") == true)
                    {
                        if (vFilterExpression.Length > 0)
                            vFilterExpression = vFilterExpression + " and ";
                        vFilterExpression = vFilterExpression + vFileIni.__mValueRead(vFormParentName, vParameter);
                    }
                    if (vParameter.StartsWith("FilterMessage") == true)
                    {
                        if (vFilterMessage.Length > 0)
                            vFilterMessage = vFilterMessage + "\n";
                        vFilterMessage = vFilterMessage + vFileIni.__mValueRead(vFormParentName, vParameter);
                    }
                }
            }

            /// Отображение условия фильтра
            if (vFilterMessage.Length > 0)
            {
                _cSplitterFilterGrid.Panel1Collapsed = false;
                _cLabelFilterExpression.Text = vFilterMessage;
            }
            else
            {
                _cSplitterFilterGrid.Panel1Collapsed = true;
                _cLabelFilterExpression.Text = "";
            }
            if (_cLabelFilterExpression.Top + _cLabelFilterExpression.Height + crlInterface.__fIntervalVertical * 2 > _cSplitterFilterGrid.Panel1MinSize)
            {
                try
                {
                    _cSplitterFilterGrid.SplitterDistance = _cLabelFilterExpression.Top + _cLabelFilterExpression.Height + crlInterface.__fIntervalVertical * 2;
                }
                catch { }
            }

            /// Загрузка данных из источника данных
            vReturn = _cGrid.__mDataLoad(vFilterExpression, "", -1);

            _cGrid.__mSortingLoad();

            #region /// Перевод курсора на строку выбранную до загрузки

            if (vCurrentRowIndex >= 0 & _cGrid.Rows.Count > 0)
            {
                if (_cGrid.Rows.Count < vCurrentRowIndex + 1)
                    vCurrentRowIndex = _cGrid.Rows.Count - 1;
                _cGrid.CurrentCell = _cGrid.Rows[vCurrentRowIndex].Cells["des" + __oEssence_.__fTableName];
            }

            /// Исправление логических значений
            vFilterExpression = vFilterExpression.Replace("False", "0");
            vFilterExpression = vFilterExpression.Replace("True", "1");

            #endregion Перевод курсора на строку выбранную до загрузки

            if (__eDataLoadAfter != null)
                __eDataLoadAfter(_cGrid, new EventArgs());

            _cGrid.Refresh();

            return vReturn;
        }
        /// <summary>
        /// Очистка выпадающего меню кнопки управления
        /// </summary>
        public void __mButtonDropDownItemsClear(string pButtonName)
        {
            crlComponentToolBarButtonMenu vButton = new crlComponentToolBarButtonMenu();

            switch (pButtonName)
            {
                case "_cButtonEdit":
                    vButton = _cButtonEdit;
                    break;
                case "_cButtonOperations":
                    vButton = _cButtonOperations;
                    break;
                case "_cButtonReports":
                    vButton = _cButtonReports;
                    break;
            }

            vButton.DropDownItems.Clear();

            return;
        }
        /// <summary>
        /// Добавление меню в кнопку управления
        /// </summary>
        /// <param name="pMenuItem"></param>
        public void __mButtonDropDownItemsAdd(string pButtonName, crlComponentMenuItem pMenuItem)
        {
            crlComponentToolBarButtonMenu vButton = new crlComponentToolBarButtonMenu();
            switch (pButtonName)
            {
                case "_cButtonEdit":
                    vButton = _cButtonEdit;
                    break;
                case "_cButtonOperations":
                    vButton = _cButtonOperations;
                    break;
                case "_cButtonReports":
                    vButton = _cButtonReports;
                    break;
            }
            vButton.DropDownItems.Add(pMenuItem);

            return;
        }
        /// <summary>
        /// Добавление меню в кнопку управления
        /// </summary>
        /// <param name="pMenuItem"></param>
        public void __mButtonDropDownItemsAdd(string pButtonName, string pMenuItem)
        {
            crlComponentToolBarButtonMenu vButton = new crlComponentToolBarButtonMenu();
            switch (pButtonName)
            {
                case "_cButtonEdit":
                    vButton = _cButtonEdit;
                    break;
                case "_cButtonOperations":
                    vButton = _cButtonOperations;
                    break;
                case "_cButtonReports":
                    vButton = _cButtonReports;
                    break;
            }
            vButton.DropDownItems.Add(pMenuItem);

            return;
        }
        /// <summary>
        /// Заполнение меню кнопки "Колонки" данными
        /// </summary>
        private void mMenuFieldFill()
        {
            if (_cGrid.__fColumnsList.Count > 0)
            {
                foreach (crlDataGridColumn vColumn in _cGrid.__fColumnsList)
                {
                    crlComponentMenuItem _cToolStripMenuItemColumn = new crlComponentMenuItem();

                    #region Меню - видимость колонок

                    _cToolStripMenuItemColumn.Checked = Convert.ToBoolean((FindForm() as crlForm).__oFileIni.__mValueReadWrite(vColumn.__fVisible.ToString(), (FindForm() as crlForm).Name, "Field_" + vColumn.__fField)); // Загрузка состояния видимости поля
                    _cToolStripMenuItemColumn.CheckedChanged += _tbtFieldsVisible_CheckedChanged;
                    _cToolStripMenuItemColumn.CheckOnClick = true;
                    _cToolStripMenuItemColumn.Font = crlApplication.__oInterface.__mFont(FONTS.Text);
                    _cToolStripMenuItemColumn.ImageScaling = ToolStripItemImageScaling.None;
                    _cToolStripMenuItemColumn.Name = vColumn.__fField;
                    _cToolStripMenuItemColumn.Text = vColumn.__fCaption;

                    //if (_cToolStripMenuItemColumn.Name.ToUpper().StartsWith("DES") _ == true)
                    /// Определение видимости соответствующего поля в сетке
                    if (_cToolStripMenuItemColumn.Name.ToUpper() == "DES" + _cGrid.__oEssence.__fTableName.ToUpper())
                    {
                        _cGrid.Columns[vColumn.__fField].Visible = true;
                        _cToolStripMenuItemColumn.Enabled = false;
                    }
                    /// Определение видимости соответствующего поля в сетке
                    else
                        _cGrid.Columns[vColumn.__fField].Visible = _cToolStripMenuItemColumn.Checked;

                    #endregion Меню - видимость колонок
                }
            }
            //_mSortingLoad(); // Загрузка сортировки
        }
        /// <summary>
        /// Загрузка сортировки в сетку
        /// </summary>
        public void __mSortingLoad()
        {
            _cGrid.__mSortingLoad();

            return;
        }
        /// <summary>
        /// Сохранение сортировки в сетке
        /// </summary>
        public void __mSortingSave()
        {
            _cGrid.__mSortingSave();

            return;
        }
        /// <summary>
        /// Получение значения поля курсора в текущей ячейке
        /// </summary>
        /// <param name="pFieldName">Название поля курсора</param>
        /// <returns></returns>
        public object __mCurrentRowFieldValue(string pFieldName)
        {
            return _cGrid.__mCurrentRowFieldValue(pFieldName);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Указывает форме, что есть открытые DropDown у кнопок
        /// </summary>
        //      public bool __fDropDownOpened = false;
        /// <summary>
        /// Использование кода по умолчанию для меню 'Отчеты / Список'
        /// </summary>
        public bool _fButtonReportsListDefaultCode = true;
        /// <summary>
        /// Блокировка изменения документа
        /// </summary>
        public bool __fEditLock = false;

        #endregion Атрибуты

        #region - Компоненты

        /// <summary>
        /// Кнопка 'Правка'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonEdit = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Правка'

        /// <summary>
        /// Кнопка 'Правка / Копировать'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditCopy = new crlComponentMenuItem();
        /// <summary>
        /// Кнопка 'Правка / Создать'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditCreate = new crlComponentMenuItem();
        /// <summary>
        /// Кнопка 'Правка / Изменить'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditEdit = new crlComponentMenuItem();
        /// <summary>
        /// Кнопка 'Правка / Удалить'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditRemove = new crlComponentMenuItem();
        /// <summary>
        /// Кнопка 'Правка / Восстановить'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditRestore = new crlComponentMenuItem();

        #endregion Меню кнопки 'Правка'

        /// <summary>
        /// Кнопка 'Операции'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonOperations = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Операции'

        /// <summary>
        /// Кнопка 'Операции / Доступ'
        /// </summary>
        protected crlComponentMenuItem _cButtonOperationsAccess = new crlComponentMenuItem();

        #endregion Меню кнопки 'Операции'

        /// <summary>
        /// Кнопка 'Отчеты'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonReports = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Отчеты'

        /// <summary>
        /// Пункт меню 'Текущий список' кнопки 'Отчеты'
        /// </summary>
        protected crlComponentMenuItem _cButtonReportsCurrentList = new crlComponentMenuItem();
        /// <summary>
        /// Пункт меню 'История записи' кнопки 'Отчеты'
        /// </summary>
        protected crlComponentMenuItem _cButtonReportsHistory = new crlComponentMenuItem();

        #endregion Меню кнопки 'Отчеты'

        /// <summary>
        /// Кнопка 'Обновить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonRefresh = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Выбрать'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSelect = new crlComponentToolBarButton();
        /// <summary>
        /// Разделитель
        /// </summary>
        protected crlComponentSplitter _cSplitterFilterGrid = new crlComponentSplitter();
        /// <summary>
        /// Заголовок условия фильтра
        /// </summary>
        protected crlComponentLabel _cLabelFilterCaption = new crlComponentLabel();
        /// <summary>
        /// Содержание условия фильтра
        /// </summary>
        protected crlComponentLabel _cLabelFilterExpression = new crlComponentLabel();
        /// <summary>
        /// Сетка
        /// </summary>
        protected crlComponentGrid _cGrid = new crlComponentGrid();

        #endregion Компоненты

        #region - Объекты

        /// <summary>
        /// Тип формы для построения фильтра
        /// </summary>
        public Type __oFormFilter;
        /// <summary>
        /// Тип формы для изменения данных
        /// </summary>
        public Type __oFormOpened;
        /// <summary>
        /// Тип формы для изменения данных
        /// </summary>
        public FORMOPENEDTYPES __fFormOpenedType = FORMOPENEDTYPES.FormRecord;

        #endregion Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        #region /// Доступность кнопок управления

        /// <summary>
        /// Доступность кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditEnabled_
        {
            get { return _cButtonEdit.Enabled; }
            set { _cButtonEdit.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Копировать' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditCopyEnabled_
        {
            get { return _cButtonEditCopy.Enabled; }
            set { _cButtonEditCopy.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Создать' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditCreateEnabled_
        {
            get { return _cButtonEditCreate.Enabled; }
            set { _cButtonEditCreate.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Изменить' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditEditEnabled_
        {
            get { return _cButtonEditEdit.Enabled; }
            set { _cButtonEditEdit.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Удалить' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditRemoveEnabled_
        {
            get { return _cButtonEditRemove.Enabled; }
            set { _cButtonEditRemove.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Восстановить' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditRestoreEnabled_
        {
            get { return _cButtonEditRestore.Enabled; }
            set { _cButtonEditRestore.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Операции'
        /// </summary>
        public bool __fButtonOperationsEnabled_
        {
            get { return _cButtonOperations.Enabled; }
            set { _cButtonRefresh.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Права пользователей' кнопки 'Операции'
        /// </summary>
        public bool __fButtonOperationsAccessEnabled_
        {
            get { return _cButtonOperationsAccess.Enabled; }
            set { _cButtonOperationsAccess.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Отчеты'
        /// </summary>
        public bool __fButtonReportsEnabled_
        {
            get { return _cButtonOperations.Enabled; }
            set { _cButtonRefresh.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'Текущий список' кнопки 'Отчеты'
        /// </summary>
        public bool __fButtonReportsCurrentListEnabled_
        {
            get { return _cButtonReportsCurrentList.Enabled; }
            set { _cButtonReportsCurrentList.Enabled = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'История корректировок' кнопки 'Отчеты'
        /// </summary>
        public bool __fButtonReportsHistoryEnabled_
        {
            get { return _cButtonReportsHistory.Enabled; }
            set { _cButtonReportsHistory.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Обновить'
        /// </summary>
        public bool __fButtonRefreshEnabled_
        {
            get { return _cButtonRefresh.Enabled; }
            set { _cButtonRefresh.Enabled = value; }
        }
        /// <summary>
        /// Доступность кнопки 'Выбрать'
        /// </summary>
        public bool __fButtonSelectEnabled_
        {
            get { return _cButtonSelect.Enabled; }
            set { _cButtonSelect.Enabled = value; }
        }

        #endregion Доступность кнопок управления

        #region /// Видимость кнопок управления

        /// <summary>
        /// Видимость кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditVisible_
        {
            get { return _cButtonEdit.Visible; }
            set { _cButtonEdit.Visible = value; }
        }
        /// <summary>
        /// Видимость пункта меню 'Копировать' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditCopyVisible_
        {
            get { return _cButtonEditCopy.Visible; }
            set { _cButtonEditCopy.Visible = value; }
        }
        /// <summary>
        /// Видимость пункта меню 'Создать' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditCreateVisible_
        {
            get { return _cButtonEditCreate.Visible; }
            set { _cButtonEditCreate.Visible = value; }
        }
        /// <summary>
        /// Видимость пункта меню 'Изменить' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditEditVisible_
        {
            get { return _cButtonEditEdit.Visible; }
            set { _cButtonEditEdit.Visible = value; }
        }
        /// <summary>
        /// Видимость пункта меню 'Удалить' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditRemoveVisible_
        {
            get { return _cButtonEditRemove.Visible; }
            set { _cButtonEditRemove.Visible = value; }
        }
        /// <summary>
        /// Видимость пункта меню 'Восстановить' кнопки 'Правка'
        /// </summary>
        public bool __fButtonEditRestoreVisible_
        {
            get { return _cButtonEditRestore.Visible; }
            set { _cButtonEditRestore.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Операции'
        /// </summary>
        public bool __fButtonOperationsVisible_
        {
            get { return _cButtonOperations.Visible; }
            set { _cButtonOperations.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Отчеты'
        /// </summary>
        public bool __fButtonReportsVisible_
        {
            get { return _cButtonReports.Visible; }
            set { _cButtonReports.Visible = value; }
        }
        /// <summary>
        /// Видимость пункта меню 'История изменений' кнопки 'Отчеты'
        /// </summary>
        public bool __fButtonReportsHistoryVisible_
        {
            get { return _cButtonReportsHistory.Visible; }
            set { _cButtonReportsHistory.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Обновить'
        /// </summary>
        public bool __fButtonRefreshVisible_
        {
            get { return _cButtonRefresh.Visible; }
            set { _cButtonRefresh.Visible = value; }
        }
        /// <summary>
        /// Видимость кнопки 'Выбрать'
        /// </summary>
        public bool __fButtonSelectVisible_
        {
            get { return _cButtonSelect.Visible; }
            set { _cButtonSelect.Visible = value; }
        }

        #endregion Видимость кнопок управления

        #region /// Подсказки к кнопкам

        /// <summary>
        /// Подсказка к кнопке 'Выбрать' переведенная на язык пользователя
        /// </summary>
        public string __fButtonSelectToolTipText
        {
            set { _cButtonSelect.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Обновить' переведенная на язык пользователя
        /// </summary>
        public string __fButtonRefreshToolTipText
        {
            set { _cButtonRefresh.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Правка' переведенная на язык пользователя
        /// </summary>
        public string __fButtonEditToolTipText
        {
            set { _cButtonEdit.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Операции' переведенная на язык пользователя
        /// </summary>
        public string __fButtonOperationsToolTipText
        {
            set { _cButtonOperations.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }
        /// <summary>
        /// Подсказка к кнопке 'Отчеты' переведенная на язык пользователя
        /// </summary>
        public string __fButtonReportsToolTipText
        {
            set { _cButtonReports.ToolTipText = crlApplication.__oTunes.__mTranslate(value); }
        }

        #endregion Подсказки к кнопкам

        #region /// Изображения на кнопках

        /// <summary>
        /// Изображение на кнопке 'Помощь'
        /// </summary>
        public Image __fButtonSelectImage
        {
            set { _cButtonSelect.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Обновить'
        /// </summary>
        public Image __fButtonRefreshImage
        {
            set { _cButtonRefresh.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Правка'
        /// </summary>
        public Image __fButtonEditImage
        {
            set { _cButtonEdit.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Операции'
        /// </summary>
        public Image __fButtonOperationsImage
        {
            set { _cButtonOperations.Image = value; }
        }
        /// <summary>
        /// Изображение на кнопке 'Отчеты'
        /// </summary>
        public Image __fButtonReportsImage
        {
            set { _cButtonReports.Image = value; }
        }

        #endregion Изображения на кнопках

        /// <summary>
        /// Сущность данных
        /// </summary>
        public datUnitEssence __oEssence_
        {
            get { return _cGrid.__oEssence; }
            set { _cGrid.__oEssence = value; }
        }
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int __fRecordClue_
        {
            get { return _cGrid.__fRecordClue_; }
            set { _cGrid.__fRecordClue_ = value; }
        }
        /// <summary>
        /// Выбранная строка в сетке
        /// </summary>
        public DataGridViewRow __fSelectedRow_
        {
            get { return _cGrid.SelectedRows[0]; }
        }
        /// <summary>
        /// Коллекция колонок добавленных в сетку
        /// </summary>
        public DataGridViewColumnCollection __fColumns_
        {
            get { return _cGrid.Columns; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при выборе пункта меню 'Копировать' кнопки 'Правка'
        /// </summary>
        //public event EventHandler __eButtonEditCopyClick;
        /// <summary>
        /// Возникает при выборе пункта меню 'Изменить' кнопки 'Правка'
        /// </summary>
        public event EventHandler __eButtonEditEditClick;
        /// <summary>
        /// Возникает при выборе пункта меню 'Отчеты / Список'
        /// </summary>
        public event EventHandler __eButtonReportsListClick;
        /// <summary>
        /// Возникает при выборе пункта меню 'Операции / Права пользователей'
        /// </summary>
        public event EventHandler __eButtonUsersAccessClick;
        /// <summary>
        /// Возникает после загрузки данных
        /// </summary>
        public event EventHandler __eDataLoadAfter;
        /// <summary>
        /// Возникает перед загрузкой данных
        /// </summary>
        public event EventHandler __eDataLoadBefore;

        #endregion = СОБЫТИЯ
    }
}
