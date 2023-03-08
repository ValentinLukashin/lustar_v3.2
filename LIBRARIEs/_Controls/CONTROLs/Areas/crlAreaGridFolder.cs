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
    /// <summary>Класс 'crlAreaGridFolder'
    /// </summary>
    /// <remarks>Область для правки древовидных данных</remarks>
    public class crlAreaGridFolder : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка компонента
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            __cToolBar.Items.Insert(0, _cButtonSelect);
            __cToolBar.Items.Insert(1, _cButtonRefresh);
            __cToolBar.Items.Add(_cButtonColumns);
            __cToolBar.Items.Add(_cButtonReports);
            __cToolBar.Items.Add(_cButtonOperations);
            __cToolBar.Items.Add(_cButtonEdit);

            _cButtonEdit.DropDownItems.Add(_cButtonEditNew);
            _cButtonEdit.DropDownItems.Add(_cButtonEditCopy);
            _cButtonEdit.DropDownItems.Add(_cButtonEditEdit);
            _cButtonEdit.DropDownItems.Add(_cButtonEditRemove);
            _cButtonEdit.DropDownItems.Add(_cButtonEditRestore);

            _cButtonOperations.DropDownItems.Add(__cButtonOperationsAccess);

            _cButtonReports.DropDownItems.Add(_cButtonReportsCurrentList);
            _cButtonReports.DropDownItems.Add(_cButtonReportsHistory);

            Controls.Add(_cSplitterFilterGrid);
            Controls.SetChildIndex(_cSplitterFilterGrid, 0);

            _cSplitterFilterGrid.Panel1.Controls.Add(_cLabelFilterCaption);
            _cSplitterFilterGrid.Panel1.Controls.Add(_cLabelFilterExpression);

            _cSplitterFilterGrid.Panel2.Controls.Add(_cGrid);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // __cButtonColumns
            {
                _cButtonColumns.Alignment = ToolStripItemAlignment.Right;
                _cButtonColumns.DropDownOpened += cButton_DropDownOpened;
                _cButtonColumns.Image = global::nlResourcesImages.Properties.Resources._TableColumn_b32C;
                _cButtonColumns.ToolTipText = "[ F12 ] " + crlApplication.__oTunes.__mTranslate("Видимость колонок");
                _cButtonColumns.__eMouseClickRight += mButtonColumns_eMouseClickRight;

            }
            // __cButtonEdit
            {
                _cButtonEdit.Alignment = ToolStripItemAlignment.Right;
                _cButtonEdit.DropDownOpened += cButton_DropDownOpened;
                _cButtonEdit.Image = global::nlResourcesImages.Properties.Resources._PageEdit_y32;
                _cButtonEdit.ToolTipText = "[ Ctrl + E ] " + crlApplication.__oTunes.__mTranslate("Правка");
                {
                    _cButtonEditNew.Click += mButtonEditNew_Click;
                    _cButtonEditNew.Image = global::nlResourcesImages.Properties.Resources._Page_w16C;
                    _cButtonEditNew.__fCaption_ = "Создать";

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
                    __cButtonOperationsAccess.Click += mButtonOperationsAccess_Click;
                    __cButtonOperationsAccess.Image = global::nlResourcesImages.Properties.Resources._UserEdit_b16;
                    __cButtonOperationsAccess.__fCaption_ = "Определение прав пользователей";
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
            // _cSplitter
            {
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
                _cGrid.DoubleClick += __cGrid_DoubleClick;
                _cGrid.RowLeave += __cGrid_RowLeave;
                _cGrid.RowEnter += __cGrid_RowEnter;
                _cGrid.RowValidated += __cGrid_RowValidated;
                _cGrid.SelectionChanged += __cGrid_SelectionChanged;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }

        #endregion Объект

        private void __cGrid_DoubleClick(object sender, EventArgs e)
        {
            if (_cGrid.Rows.Count > 0)
            {
                if (_cGrid.CurrentRow.Index >= 0 && _cGrid.Rows[_cGrid.CurrentRow.Index].Selected == true)
                { 
                    if (_cGrid.SelectedCells[2].Value.ToString() != "..")
                    {
                        if(Convert.ToInt32((_cGrid.DataSource as DataTable).Rows[_cGrid.CurrentCell.RowIndex][3]) > 0)
                            __fForderCluePreview = Convert.ToInt32(crlApplication.__oData.__mSqlValue(__oEssence.__fTableName
                                , "lnk" + __oEssence.__fTableName
                                , "clu" + __oEssence.__fTableName + "=" + _cGrid.__fRecordClue_.ToString())); // Идентификатор для возвращения на один уровень вверх 
                        __fFolderClue = _cGrid.__fRecordClue_;
                        __mDataLoad();
                    }
                    else
                    {
                        __fForderCluePreview = Convert.ToInt32(crlApplication.__oData.__mSqlValue(__oEssence.__fTableName
                            , "lnk" + __oEssence.__fTableName
                            , "clu" + __oEssence.__fTableName + "=" + __fForderCluePreview.ToString())); // Идентификатор для возвращения на один уровень вверх 
                        __fFolderClue = __fForderCluePreview;
                        __mDataLoad();
                    }
                }
            }
        }
        private void __cGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //__fForderCluePreview = __fFolderClue;
            if (_cGrid.Rows.Count > 0)
            {
                if (_cGrid.CurrentRow != null && _cGrid.CurrentRow.Index >= 0 && _cGrid.Rows[_cGrid.CurrentRow.Index].Selected == true)
                {
                    if (_cGrid.SelectedCells[2].Value.ToString() != "..")
                    {
                        __fForderCluePreview = Convert.ToInt32((_cGrid.DataSource as DataTable).Rows[_cGrid.CurrentCell.RowIndex][3]);
                    }
                    else
                    {
                        __fRecordClue = Convert.ToInt32((_cGrid.DataSource as DataTable).Rows[_cGrid.CurrentCell.RowIndex][3]);
                    }
                 }
            }
        }
        private void __cGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void __cGrid_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void __cGrid_SelectionChanged(object sender, EventArgs e)
        {
            __fFolderClue = _cGrid.__fRecordClue_;
            _cLabelFilterExpression.Text = __oEssence.__mTreeFullName(__fFolderClue);
        }

        #region Кнопки управления

        /// <summary>Выполняется при открытии меню кнопки 'Правка'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cButton_DropDownOpened(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
        }
        /// <summary>Изменение статуса видимости любой колонки
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
        /// <summary>Выполняется при выборе кнопки 'Правка / Создать' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditNew_Click(object sender, EventArgs e)
        {
            crlFormEmailRecipientsTypes vForm = FindForm() as crlFormEmailRecipientsTypes;
            if (vForm != null & __oFormOpened != null)
            {
                /// Вызов формы редактирования документа
                if (__pFormOpenedType == FORMOPENEDTYPES.FormDocument)
                {
                    crlFormDocument vFormDocument = (crlFormDocument)Activator.CreateInstance(__oFormOpened);
                    (vFormDocument as crlFormDocument).ShowDialog();
                }
                /// Вызов формы редактирования записи
                if (__pFormOpenedType == FORMOPENEDTYPES.FormRecord)
                {
                    crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                    (vFormRecord as crlFormRecord).ShowDialog();
                }
                __mDataLoad(); /// Перегрузка данных
            }
        }
        /// <summary>Выполняется при выборе кнопки 'Правка / Создать' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditCopy_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            crlFormEmailRecipientsTypes vForm = FindForm() as crlFormEmailRecipientsTypes;
            if (vForm != null & __oFormOpened != null)
            {
                /// Вызов формы редактирования документа
                if (__pFormOpenedType == FORMOPENEDTYPES.FormDocument)
                {
                    crlFormDocument vFormDocument = (crlFormDocument)Activator.CreateInstance(__oFormOpened);
                    (vFormDocument as crlFormDocument).ShowDialog();
                }
                /// Вызов формы редактирования записи
                if (__pFormOpenedType == FORMOPENEDTYPES.FormRecord)
                {
                    crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                    (vFormRecord as crlFormRecord).__cAreaRecord.__fRecordClueForCopy = _cGrid.__fRecordClue_;
                    (vFormRecord as crlFormRecord).ShowDialog();
                }
                __mDataLoad(); /// Перегрузка данных
            }
        }
        /// <summary>Выполняется при выборе кнопки 'Правка / Изменить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditEdit_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            crlFormEmailRecipientsTypes vForm = FindForm() as crlFormEmailRecipientsTypes;

            if (vForm != null & __oFormOpened != null)
            {
                /// Вызов формы редактирования документа
                if (__pFormOpenedType == FORMOPENEDTYPES.FormDocument)
                {
                    crlFormDocument vFormDocument = (crlFormDocument)Activator.CreateInstance(__oFormOpened);
                    //vFormDocument.__cAreaDocument._fRecordClue = __cGrid.__pRecordClue;
                    (vFormDocument as crlFormDocument).ShowDialog();
                }
                /// Вызов формы редактирования записи
                if (__pFormOpenedType == FORMOPENEDTYPES.FormRecord)
                {
                    crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                    vFormRecord.__cAreaRecord.__fRecordClue = _cGrid.__fRecordClue_;
                    (vFormRecord as crlFormRecord).ShowDialog();
                }
                __mDataLoad(); /// Перегрузка данных
            }
        }
        /// <summary>Выполняется при выборе кнопки 'Правка / Удалить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditRemove_Click(object sender, EventArgs e)
        {
            crlApplication.__oData.__mDataSourceGet().__mSqlCommand("Update " + __oEssence.__fTableName + " Set LCK = 1 Where clu" + __oEssence.__fTableName + " = " + __pRecordClue.ToString());
            __mDataLoad();
        }
        /// <summary>Выполняется при выборе кнопки 'Правка / Восстановить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditRestore_Click(object sender, EventArgs e)
        {
            crlApplication.__oData.__mDataSourceGet().__mSqlCommand("Update " + __oEssence.__fTableName + " Set LCK = 0 Where clu" + __oEssence.__fTableName + " = " + __pRecordClue.ToString());
            __mDataLoad();
        }
        /// <summary>Выполняется при выборе кнопки 'Операции / Определение прав пользователей'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonOperationsAccess_Click(object sender, EventArgs e)
        {
            if (__eButtonUsersAccessClick != null)
                __eButtonUsersAccessClick(this, new EventArgs());
        }
        /// <summary>Выполняется при выборе кнопки 'Обновить' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonRefresh_Click(object sender, EventArgs e)
        {
            crlFormEmailRecipientsTypes vForm = FindForm() as crlFormEmailRecipientsTypes;
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
        /// <summary>Выполняется при выборе кнопки 'Обновить' правой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonRefresh_eMouseClickRight(object sender, EventArgs e)
        {
            string vFormParentName = (FindForm() as crlFormEmailRecipientsTypes).Name; // Название формы на которой расположен компонент
            appFileIni vFileIni = (FindForm() as crlFormEmailRecipientsTypes).__oFileIni; // Объект для работы с инициализационными файлами
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
            (FindForm() as crlFormEmailRecipientsTypes).__cStatus.__fCaption_ = "Фильтр сброшен";
        }
        /// <summary>Выполняется при выборе меню "Отчеты/Текущий список"
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
                                    vReport.__mCell(vViewRow.Cells[vColumnNumber].Value.ToString(), "CL=DataCell");
                                else
                        if (_cGrid.Columns[vColumnNumber].Visible == true)
                                    if (vViewRow.Cells[vColumnNumber].Value != null)
                                        vReport.__mCell(vViewRow.Cells[vColumnNumber].Value.ToString(), "CL=DataCell-Last");
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
        /// <summary>Выполняется при выборе меню "Отчеты/История"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonReportsHistory_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            DataTable vDataTable = _cGrid.__oEssence._mRecordChanges(_cGrid.__fRecordClue_);
            DataView vDataView = vDataTable.DefaultView;
            vDataView.Sort = "dtmDatLck desc"; // Обратная сортировка по времени корректировки
            vDataTable = vDataView.ToTable();
            string vTableName = _cGrid.__oEssence.__fTableName; // Название текущей таблицы
            rrtReport vReport = new rrtReport();
            vReport.__mCreate();
            vReport.__fTitle = crlApplication.__oTunes.__mTranslate("Таблица '{0}'", __oEssence.__fTableName);

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
                            string vCaption = crlApplication.__oData._mModelFieldDescription(__oEssence.__fTableName, vDataTable.Columns[vColumnNumber].ColumnName);
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
                    //if (vDataColumn.ColumnName != "dtmDatLck")
                    if (Convert.ToString(vDataRow["dtmDatLck"]).Length > 0)
                        vReport.__mCell(vDataRow[vDataColumn.ColumnName], "CL=DataCell");
                }
            }

            vReport.__mFile();
            crlFormReportPreview vFormReportPreview = new crlFormReportPreview();
            vFormReportPreview._cAreaReportPreview._fUrl = vReport.__fFilePath;
            vFormReportPreview.ShowDialog();
        }
        /// <summary>Выполняется при выборе кнопки 'Выбрать' левой кнопкой мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonSelect_Click(object sender, EventArgs e)
        {
            (FindForm() as crlForm).Close();
        }
        private void mButtonColumns_eMouseClickRight(object sender, EventArgs e)
        {
            #region Отображение всех колонок

            foreach (crlComponentMenuItem vMenu in _cButtonColumns.DropDownItems)
            {
                vMenu.Checked = true;
            }

            #endregion Отображение всех колонок

            #region Восстановление порядка колонок

            ArrayList vColumnsIndexes = (FindForm() as crlFormEmailRecipientsTypes).__oFileIni.__mParametersListByMaskInput(Name, "Column_");
            int vColumnIndexDefault = 0;
            foreach (string vColumn in vColumnsIndexes)
            {
                (FindForm() as crlFormEmailRecipientsTypes).__oFileIni.__mParameterClear(Name, vColumn);
                _cGrid.Columns[vColumn.Substring(7)].DisplayIndex = vColumnIndexDefault;
                vColumnIndexDefault++;
            }

            #endregion Восстановление порядка колонок

            __mDataLoad();
        }

        #endregion Кнопки управления

        #endregion - Поведение

        #region - Процедуры

        /// <summary>* Добавление колонки
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
        /// <summary>* Добавление колонок в сетку
        /// </summary>
        /// <returns>[true] - колонки добавлены, иначе - [false]</returns>
        public bool __mColumnsBuild()
        {
            return _cGrid.__mColumnsBuild();
        }
        /// <summary>Заполнение меню кнопки "Колонки" данными
        /// </summary>
        public void __mMenuFieldFill()
        {
            if (_cGrid.__fColumnsList.Count > 0)
            {
                foreach (crlDataGridColumn vColumn in _cGrid.__fColumnsList)
                {
                    crlComponentMenuItem _cToolStripMenuItemColumn = new crlComponentMenuItem();

                    #region Меню - видимость колонок

                    _cToolStripMenuItemColumn.Checked = Convert.ToBoolean((FindForm() as crlForm).__oFileIni.__mValueReadWrite(vColumn.__fVisible.ToString(), (FindForm() as crlForm).Name, "Field_" + vColumn.__fField)); /// Загрузка состояния видимости поля
                    _cToolStripMenuItemColumn.CheckedChanged += _tbtFieldsVisible_CheckedChanged;
                    _cToolStripMenuItemColumn.CheckOnClick = true;
                    _cToolStripMenuItemColumn.Font = crlApplication.__oInterface.__mFont(FONTS.Text);
                    _cToolStripMenuItemColumn.ImageScaling = ToolStripItemImageScaling.None;
                    _cToolStripMenuItemColumn.Name = vColumn.__fField;
                    _cToolStripMenuItemColumn.Text = vColumn.__fCaption;

                    if (_cToolStripMenuItemColumn.Name.ToUpper() == "DES" + _cGrid.__oEssence.__fTableName.ToUpper())
                    {
                        _cGrid.Columns[vColumn.__fField].Visible = true; /// Определение видимости соответствующего поля в сетке
                        _cToolStripMenuItemColumn.Enabled = false;
                    }
                    else
                        _cGrid.Columns[vColumn.__fField].Visible = _cToolStripMenuItemColumn.Checked; /// Определение видимости соответствующего поля в сетке
                    _cButtonColumns.DropDownItems.Add(_cToolStripMenuItemColumn);

                    #endregion Меню - видимость колонок
                }
            }
            _cButtonColumns.PerformClick();
            //_mSortingLoad(); // Загрузка сортировки
        }
        /// <summary>* Загрузка данных
        /// </summary>
        /// <returns>[true] - данные загружены без ошибок, иначе - [false]</returns>
        public void __mDataLoad()
        {
            string vQuery = ""; // Тело запроса
            string vTableName = __oEssence.__fTableName; // Название таблицы
            if (__fCodeUsedInQuery == true)
            {
                if (__fFolderClue == 0)
                {
                    vQuery = " Select t.clu" + vTableName +
                             ", t.des" + vTableName +
                             ", t.cod" + vTableName +
                             ", t.lnk" + vTableName +
                             ", Count(ct.lnk" + vTableName + ") as Cou" +
                             " From " + vTableName + " as t" +
                             " Left Join " + vTableName + " as ct On ct.clu" + vTableName + " = t.clu" + vTableName +
                             " Where t.lnk" + vTableName + " = " + __fFolderClue.ToString() + " and t.clu" + vTableName + " != 0 " +
                             " Group by t.clu" + vTableName + ", t.des" + vTableName + ", t.cod" + vTableName + ", t.lnk" + vTableName;
                    _cGrid.DataSource = datApplication.__oData.__mDataSourceGet().__mSqlQuery(vQuery);
                } /// Выбор данных для верхнего уровня
                else
                {
                    vQuery = "Select " + __fFolderClue.ToString() + " as clu" + vTableName +
                             ", '..' as des" + vTableName +
                             ", 0 as cod" + vTableName +
                             ", 0 as lnk" + vTableName +
                             ", 0 as Cou" +
                             " From " + vTableName + " as t" +
                             " Union" +
                             " Select * " +
                             " From (" +
                             " Select t.clu" + vTableName +
                             ", t.des" + vTableName +
                             ", t.cod" + vTableName +
                             ", t.lnk" + vTableName +
                             ", Count(ct.lnk" + vTableName + ") as Cou" +
                             " From " + vTableName + " as t" +
                             " Left Join " + vTableName + " as ct On ct.clu" + vTableName + " = t.clu" + vTableName +
                             " Where t.lnk" + vTableName + " = " + __fFolderClue.ToString() + " and t.clu" + vTableName + " != 0 " +
                             " Group by t.clu" + vTableName + ", t.des" + vTableName + ", t.cod" + vTableName + ", t.lnk" + vTableName +
                             " ) as A";
                    DataTable vDataTable = datApplication.__oData.__mDataSourceGet().__mSqlQuery(vQuery);
                    if (vDataTable.Rows.Count > 1)
                        _cGrid.DataSource = vDataTable;
                } /// Выбор данных для вложенного уровня
            }
            else 
            {
                if (__fFolderClue == 0)
                {
                    vQuery = " Select t.clu" + vTableName +
                             ", t.des" + vTableName +
                             ", t.lnk" + vTableName +
                             ", Count(ct.lnk" + vTableName + ") as Cou" +
                             " From " + vTableName + " as t" +
                             " Left Join " + vTableName + " as ct On ct.clu" + vTableName + " = t.clu" + vTableName +
                             " Where t.lnk" + vTableName + " = " + __fFolderClue.ToString() + " and t.clu" + vTableName + " != 0 " +
                             " Group by t.clu" + vTableName + ", t.des" + vTableName + ", t.lnk" + vTableName;
                    _cGrid.DataSource = datApplication.__oData.__mDataSourceGet().__mSqlQuery(vQuery);
                } /// Выбор данных для верхнего уровня
                else
                {
                    vQuery = "Select " + __fFolderClue.ToString() + " as clu" + vTableName +
                             ", '..' as des" + vTableName +
                             ", 0 as lnk" + vTableName +
                             ", 0 as Cou" +
                             " From " + vTableName + " as t" +
                             " Union" +
                             " Select * " +
                             " From (" +
                             " Select t.clu" + vTableName +
                             ", t.des" + vTableName +
                             ", t.lnk" + vTableName +
                             ", Count(ct.lnk" + vTableName + ") as Cou" +
                             " From " + vTableName + " as t" +
                             " Left Join " + vTableName + " as ct On ct.clu" + vTableName + " = t.clu" + vTableName +
                             " Where t.lnk" + vTableName + " = " + __fFolderClue.ToString() + " and t.clu" + vTableName + " != 0 " +
                             " Group by t.clu" + vTableName + ", t.des" + vTableName + ", t.lnk" + vTableName +
                             " ) as A";
                    DataTable vDataTable = datApplication.__oData.__mDataSourceGet().__mSqlQuery(vQuery);
                    if (vDataTable.Rows.Count > 1)
                        _cGrid.DataSource = vDataTable;
                } /// Выбор данных для вложенного уровня
            }
        }

        /// <summary>* Сохранение сортировки в сетке
        /// </summary>
        public void __mSortingSave()
        {
            _cGrid.__mSortingSave();

            return;
        }

        #region - Внешние нажатия на кнопки управления

        /// <summary>* Выполняется при выборе кнопки 'Выбрать'
        /// </summary>
        public void __mPressButtonSelect()
        {
            _cButtonSelect.PerformClick();

            return;
        }
        /// <summary>* Выполняется при выборе кнопки 'Обновить'
        /// </summary>
        public void __mPressButtonRefresh()
        {
            _cButtonRefresh.PerformClick();

            return;
        }
        /// <summary>* Выполняется при выборе кнопки 'Правка'
        /// </summary>
        public void __mPressButtonEdit()
        {
            _cButtonEdit.ShowDropDown();

            return;
        }
        /// <summary>* Выполняется при выборе кнопки 'Операции'
        /// </summary>
        public void __mPressButtonOperations()
        {
            _cButtonOperations.PerformClick();

            return;
        }
        /// <summary>* Выполняется при выборе кнопки 'Отчеты'
        /// </summary>
        public void __mPressButtonReports()
        {
            _cButtonReports.PerformClick();

            return;
        }
        /// <summary>* Выполняется при выборе кнопки 'Колонки'
        /// </summary>
        public void __mPressButtonColumns()
        {
            _cButtonColumns.PerformClick();

            return;
        }

        #endregion Внешние нажатия на кнопки управления

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>В коде запроса запрашивается учетный код 
        /// </summary>
        public bool __fCodeUsedInQuery = true;
        /// <summary>Идентификатор папки
        /// </summary>
        public int __fFolderClue = -1;
        /// <summary>Идентификатор предыдущей папки 
        /// </summary>
        public int __fForderCluePreview = -1;
        /// <summary>Указывает форме, что есть открытые DropDown у кнопок
        /// </summary>
#pragma warning disable CS0108 // "crlAreaGridFolder._fDropDownOpened" скрывает наследуемый член "crlArea._fDropDownOpened". Если скрытие было намеренным, используйте ключевое слово new.
        protected bool _fDropDownOpened = false;
#pragma warning restore CS0108 // "crlAreaGridFolder._fDropDownOpened" скрывает наследуемый член "crlArea._fDropDownOpened". Если скрытие было намеренным, используйте ключевое слово new.
        /// <summary>Использование кода по умолчанию для меню 'Отчеты / Список'
        /// </summary>
        public bool _fButtonReportsListDefaultCode = true;
        /// <summary>Идентификатор выбранной записи
        /// </summary>
        public int __fRecordClue = -1;

        #endregion - Атрибуты

        #region - Компоненты

        /// <summary>* Кнопка 'Видимость колонок'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonColumns = new crlComponentToolBarButtonMenu();
        /// <summary>* Кнопка 'Правка'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonEdit = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Правка'

        /// <summary>* Кнопка 'Правка / Копировать'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditCopy = new crlComponentMenuItem();
        /// <summary>* Кнопка 'Правка / Изменить'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditEdit = new crlComponentMenuItem();
        /// <summary>* Кнопка 'Правка / Создать'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditNew = new crlComponentMenuItem();
        /// <summary>* Кнопка 'Правка / Удалить'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditRemove = new crlComponentMenuItem();
        /// <summary>* Кнопка 'Правка / Восстановить'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditRestore = new crlComponentMenuItem();

        #endregion Меню кнопки 'Правка'

        /// <summary>* Кнопка 'Операции'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonOperations = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Операции'

        /// <summary>* Кнопка 'Операции / Доступ'
        /// </summary>
        protected crlComponentMenuItem __cButtonOperationsAccess = new crlComponentMenuItem();

        #endregion Меню кнопки 'Операции'

        /// <summary>Кнопка 'Отчеты'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonReports = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Отчеты'

        /// <summary>Кнопка 'Отчеты / Текущий список'
        /// </summary>
        protected crlComponentMenuItem _cButtonReportsCurrentList = new crlComponentMenuItem();
        /// <summary>Кнопка 'Отчеты / История записи'
        /// </summary>
        protected crlComponentMenuItem _cButtonReportsHistory = new crlComponentMenuItem();

        #endregion Меню кнопки 'Отчеты'

        /// <summary>Кнопка 'Обновить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonRefresh = new crlComponentToolBarButton();
        /// <summary>Кнопка 'Выбрать'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSelect = new crlComponentToolBarButton();

        /// <summary>Разделитель
        /// </summary>
        protected crlComponentSplitter _cSplitterFilterGrid = new crlComponentSplitter();

        /// <summary>Заголовок условия фильтра
        /// </summary>
        protected crlComponentLabel _cLabelFilterCaption = new crlComponentLabel();
        /// <summary>Содержание условия фильтра
        /// </summary>
        protected crlComponentLabel _cLabelFilterExpression = new crlComponentLabel();

        /// <summary>Сетка
        /// </summary>
        protected crlComponentGrid _cGrid = new crlComponentGrid();

        #endregion Компоненты

        #region - Объекты

        public Type __oFormFilter;
        public Type __oFormOpened;
        public FORMOPENEDTYPES __pFormOpenedType = FORMOPENEDTYPES.FormRecord;

        #endregion - Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        #region ! Видимость кнопок управления

        /// <summary>* Видимость кнопки 'Выбрать'
        /// </summary>
        public bool __fButtonSelectVisible
        {
            get { return __fButtonSelectVisible; }
            set { __fButtonSelectVisible = value; }
        }

        #endregion ! Видимость кнопок управления

        /// <summary>* Сущность данных
        /// </summary>
        public datUnitEssence __oEssence
        {
            get { return _cGrid.__oEssence; }
            set { _cGrid.__oEssence = value; }
        }
        /// <summary>* Идентификатор записи
        /// </summary>
        public int __pRecordClue
        {
            get { return _cGrid.__fRecordClue_; }
            set { _cGrid.__fRecordClue_ = value; }
        }
        /// <summary>* Колектция колонок добавленных в сетку
        /// </summary>
        public DataGridViewColumnCollection __fColumns_
        {
            get { return _cGrid.Columns; }
        }

        #endregion = СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>Возникает при выборе пункта меню 'Отчеты / Список'
        /// </summary>
        public event EventHandler __eButtonReportsListClick;
        /// <summary>Возникает при выборе пункта меню 'Операции / Права пользователей'
        /// </summary>
        public event EventHandler __eButtonUsersAccessClick;

        #endregion = СОБЫТИЯ
    }
}
