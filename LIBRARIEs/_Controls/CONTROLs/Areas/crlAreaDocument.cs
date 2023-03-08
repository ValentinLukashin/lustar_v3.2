using nlDataMaster;
using System;
using System.Data;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlAreaDocument'
    /// </summary>
    /// <remarks>Область для изменения документа</remarks>
    public class crlAreaDocument : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>* Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            __cToolBar.Items.Insert(0, _cButtonSave);
            __cToolBar.Items.Add(_cButtonReports);
            __cToolBar.Items.Add(_cButtonOperations);
            __cToolBar.Items.Add(_cButtonEdit);
            {
                _cButtonEdit.DropDownItems.Add(_cButtonEditNew);
                _cButtonEdit.DropDownItems.Add(_cButtonEditCopy);
                _cButtonEdit.DropDownItems.Add(_cButtonEditEdit);
                _cButtonEdit.DropDownItems.Add(_cButtonEditRemove);
                _cButtonEdit.DropDownItems.Add(_cButtonEditRestore);
            }
            Panel2.Controls.Add(CSplitterCaptionContent);
            Panel2.Controls.SetChildIndex(CSplitterCaptionContent, 0);
            CSplitterCaptionContent.Panel1.Controls.Add(_cSplitterCaptionLeftRight);
            {
                _cSplitterCaptionLeftRight.Panel1.Controls.Add(_cBlockInputsLeft);
                _cSplitterCaptionLeftRight.Panel2.Controls.Add(_cBlockInputsRight);

                _cBlockInputsLeft.__mInputAdd(_cInputCode);
                _cBlockInputsLeft.__mInputAdd(_cInputDateTimeCreate);
            }
            CSplitterCaptionContent.Panel2.Controls.Add(_cPageBlock);
            {
                _cPageBlock.TabPages.Add(_cPageDocument);

                _cPageDocument.__cPanel.Controls.Add(_cGrid);
            }

            #endregion Размещение компонентов

            #region Настройка компонентов

            Dock = DockStyle.Fill;

            // __cToolBar
            {
                // _cButtonEdit
                {
                    _cButtonEdit.Alignment = ToolStripItemAlignment.Right;
                    //_cButtonEdit.Click 
                    //_cButtonEdit._eMouseClickRight 
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
                        _cButtonEditRestore.Visible = false;
                    }
                }
                // _cButtonSave
                {
                    _cButtonSave.__eMouseClickLeft += mButtonSave_ClickLeft;
                    _cButtonSave.__eMouseClickRight += mButtonSave_ClickRight;
                    _cButtonSave.Image = global::nlResourcesImages.Properties.Resources._Diskette_b32C;
                    _cButtonSave.ToolTipText = "[ Ctrl + A ] " + crlApplication.__oTunes.__mTranslate("Сохранить");
                }
                // _cButtonOperations
                {
                    _cButtonOperations.Alignment = ToolStripItemAlignment.Right;
                    _cButtonOperations.Image = global::nlResourcesImages.Properties.Resources._PageGear_y32;
                    _cButtonOperations.ToolTipText = "[ Ctrl + O ] " + crlApplication.__oTunes.__mTranslate("Операции");
                }
                // _cButtonReports
                {
                    _cButtonReports.Alignment = ToolStripItemAlignment.Right;
                    _cButtonReports.Image = global::nlResourcesImages.Properties.Resources._PageInBox_y32C;
                    _cButtonReports.ToolTipText = "[ Ctrl + R ] " + crlApplication.__oTunes.__mTranslate("Отчет");
                }
            }
            // _cSplitterCaptionContent
            {
                CSplitterCaptionContent.Dock = DockStyle.Fill;
                // _cSplitterCaptionContent.Panel1
                {
                    // _cSplitterCaptionLeftRight
                    {
                        _cSplitterCaptionLeftRight.Orientation = Orientation.Vertical;
                        // _cSplitterCaptionLeftRight.Panel1
                        {
                            // _cBlockInputsLeft
                            {
                                _cBlockInputsLeft.Dock = DockStyle.Fill;
                                _cBlockInputsLeft.__fCheckShow = false;
                                {
                                    _cInputCode.__fCaption_ = "Номер документа";
                                    _cInputCode.__fCheckVisible_ = false;

                                    _cInputDateTimeCreate.__fCaption_ = "Время создания";
                                    _cInputDateTimeCreate.__fCheckVisible_ = false;
                                }
                            }
                            // _cBlockInputsRight
                            {
                                _cBlockInputsRight.Dock = DockStyle.Fill;
                                _cBlockInputsRight.__fCheckShow = false;
                            }
                        }
                        // _cSplitterCaptionLeftRight.Panel2
                        {
                        }
                    }
                }
                // _cSplitterCaptionContent.Panel2
                {
                    _cPageBlock.Dock = DockStyle.Fill;
                    _cPageBlock.SelectedIndexChanged += _cPageBlock_SelectedIndexChanged;
                    _cPageDocument.Text = crlApplication.__oTunes.__mTranslate("Записи");

                    _cGrid.Dock = DockStyle.Fill;
                }
            }

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>Выполняется при смене выбранной вкладки блока вкладок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cPageBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (__ePageBlockSelectedIndexChanged != null)
                __ePageBlockSelectedIndexChanged(_cPageBlock, e);
        }

        /// <summary>* Выполняется при первом отображении компонента
        /// </summary>
        protected override void _mObjectPresetation()
        {
            base._mObjectPresetation();
            _cSplitterCaptionLeftRight.Dock = DockStyle.Fill;

            __mDataLoad();

            return;
        }

        #region Кнопки управления

        /// <summary>Выполняется при клике левой клавиши мыши по кнопке 'Сохранить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonSave_ClickLeft(object sender, EventArgs e)
        {
            if (crlApplication.__oMessages.__mShow(nlApplication.MESSAGESTYPES.Question, "Сохранить документ") == DialogResult.Yes)
            {
                if (_mDataSave() == true)
                    FindForm().Close();
            }
            ///* Формирование события клика левой клавиши мыши по кнопке 'Сохранить'
            if (__eButtonSave_ClickLeft != null)
                __eButtonSave_ClickLeft(_cButtonSave, e);
        }
        /// <summary>Выполняется при клике правой клавиши мыши по кнопке 'Сохранить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonSave_ClickRight(object sender, EventArgs e)
        {
            /// Формирование события клика правой клавиши мыши по кнопке 'Сохранить'
            if (__eButtonSave_ClickRight != null)
                __eButtonSave_ClickRight(_cButtonSave, e);
        }
        private void mButtonEditCopy_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
            crlForm vForm = FindForm() as crlForm;

            if (vForm != null & __oFormDocumentRecord != null)
            {
                crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormDocumentRecord);
                vFormRecord.__cAreaRecord.__fRecordClue = -1;
                vFormRecord.ShowDialog();
            }
        }
        private void mButtonEditEdit_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
        }
        /// <summary>
        /// Выполняется при выборе меню 'Правка / Создать'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonEditNew_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;

            return;
        }
        private void mButtonEditRemove_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;

            return;
        }
        private void mButtonEditRestore_Click(object sender, EventArgs e)
        {
            _fDropDownOpened = true;

            return;
        }

        #endregion Кнопки управления

        #endregion Поведение

        #region . Процедуры

        /// <summary>* Загрузка данных
        /// </summary>
        /// <returns>[true] - данные получены, иначе - [false]</returns>
        public bool __mDataLoad()
        {
            bool vReturn = true; // Возвращаемое значение

            if (__eDataLoadBefore != null) ///* Формирование события перед загрузкой данных
                __eDataLoadBefore(this, new EventArgs());

            ///* Загрузка заголовка документа
            {
                __oDataTableHeader = __oEssence._mRecord(__fRecordClue);
                if (__fRecordClue <= 0)
                {
                    DataRow vDataRowHeader = __oEssence.__mRecordNew(__oDataTableHeader);
                    ///* Запись идентификатора местной валюты
                    vDataRowHeader["lnkCurLcl"] = crlApplication.__oData.__mClueByOption("Cur", "mrkLcl");
                    ///* Если выполняется копирование текущего документа в новый 
                    if (__fRecordClueForCopy > 0)
                    {
                        DataTable vDataTableForCopy = __oEssence._mRecord(__fRecordClueForCopy);
                        vDataRowHeader["lnkArrTyp"] = vDataTableForCopy.Rows[0]["lnkArrTyp"];
                        vDataRowHeader["lnkCli"] = vDataTableForCopy.Rows[0]["lnkCli"];
                        vDataRowHeader["lnkAccCdt"] = vDataTableForCopy.Rows[0]["lnkAccCdt"];
                        vDataRowHeader["lnkAccDbt"] = vDataTableForCopy.Rows[0]["lnkAccDbt"];
                        vDataRowHeader["lnkCurCli"] = vDataTableForCopy.Rows[0]["lnkCurCli"];
                    }
                    __oDataTableHeader.Rows.Add(vDataRowHeader);

                } ///* Если идентификатор записи не указан, создается новая запись

                _cBlockInputsLeft.__oDataTable = __oDataTableHeader;
                vReturn = vReturn & _cBlockInputsLeft.__mDataLoad();
                _cBlockInputsRight.__oDataTable = __oDataTableHeader;
                vReturn = vReturn & _cBlockInputsRight.__mDataLoad();
            }

            ///* Загрузка содержания документа
            vReturn = vReturn & _cGrid.__mDataLoad("lnk" + __oEssence.__fTableName + " = " + __fRecordClue.ToString() + " and " + __oEssence.__fTablePrefix + "C.LCK = 0", "Pos", -1);


            if (__eDataLoadAfter != null) ///* Формирование события после загрузки данных
                __eDataLoadAfter(this, new EventArgs());

            return vReturn;
        }
        /// <summary>
        /// (m) Сохранение данных
        /// </summary>
        protected bool _mDataSave()
        {
            __fTransactionStatus = true; // Статус завершения транзакции
            _cBlockInputsLeft.__mDataSave(); // Запись данных из левой части заголовка
            _cBlockInputsRight.__mDataSave(); // Запись данных из правой части заголовка

            ///* Открытие транзакции
            crlApplication.__oData._mTransactionOn();

            ///* Формирование события перед сохранением данных
            if (__eDataSaveBefore != null)
                __eDataSaveBefore(this, new EventArgs());

            __fTransactionStatus = __fTransactionStatus & __oEssence.__mUpdate(__oDataTableHeader); ///* Сохранение заголовка прихода
            int vClue = __oEssence.__fLastInsertedKey; ///* Идентификатор только, что созданного документа

            foreach (DataRow vDataRow in (_cGrid.DataSource as DataTable).Rows)
            {
                if (Convert.ToInt32(vDataRow["lnkArr"]) == 0)
                {
                    vDataRow["lnkArr"] = vClue;
                }
            } ///* Заполнение идентификатора прихода в содержании
            __fTransactionStatus = __fTransactionStatus & _cGrid.__oEssence.__mUpdate(_cGrid.DataSource as DataTable); ///* Сохранение содержания прихода

                                                                                                                       ///* Формирование события после сохранения данных
            if (__eDataSaveAfter != null)
                __eDataSaveAfter(this, new EventArgs());

            ///* Закрытие транзакции
            crlApplication.__oData._mTransactionOff(__fTransactionStatus);

            return __fTransactionStatus;
        }
        /// <summary>* Проверка заполненности заголовка документа
        /// </summary>
        /// <returns>[true] - заголовок документа заполнен, иначе - [false]</returns>
        public void __mHeaderValid()
        {
            if (__eHeaderValid != null)
                __eHeaderValid(this, new EventArgs());
        }
        /// <summary>Добавление поля ввода на левую панель заголовка
        /// </summary>
        /// <param name="pInput">Поле ввода</param>
        /// <returns>[true] - поле ввода добавлено, иначе - [false]</returns>
        public bool __mBlockInputsLeftInputAdd(crlInput pInput)
        {
            return _cBlockInputsLeft.__mInputAdd(pInput);
        }
        /// <summary>Добавление поля ввода на правую панель заголовка
        /// </summary>
        /// <param name="pInput">Поле ввода</param>
        /// <returns>[true] - поле ввода добавлено, иначе - [false]</returns>
        public bool __mBlockInputsRightInputAdd(crlInput pInput)
        {
            return _cBlockInputsRight.__mInputAdd(pInput);
        }
        /// <summary>* Установка пустого значения компоненту 'Дата-время создания'
        /// </summary>
        public void __mInputDateTimeCreateEmptyValue()
        {
            _cInputDateTimeCreate.__mEmptyValue();

            return;
        }
        /// <summary>* Добавление вкладки на блок вкладок
        /// </summary>
        /// <param name="pPage"></param>
        public void __mPageBlockAddPage(crlComponentPage pPage)
        {
            _cPageBlock.TabPages.Add(pPage);

            return;
        }
        /// <summary>* Добавление колонки в сетку с содержанием документа 
        /// </summary>
        /// <param name="pHeaderText"></param>
        /// <param name="pFieldName"></param>
        /// <param name="pReadOnly"></param>
        /// <param name="pVisible"></param>
        /// <param name="pType"></param>
        public void __mGridColumnAdd(string pHeaderText, string pFieldName, bool pReadOnly, bool pVisible, string pType)
        {
            _cGrid.__mColumnAdd(crlApplication.__oTunes.__mTranslate(pHeaderText), pFieldName, pReadOnly, pVisible, pType);
        }
        /// <summary>* Добавление колонок в сетку  с содержанием документа
        /// </summary>
        /// <returns>[true] - колонки добавлены, иначе - [false]</returns>
        public bool __mGridColumnsBuild()
        {
            return _cGrid.__mColumnsBuild();
        }
        /// <summary>* Получение значения из сетки по названию поля и индексу строки
        /// </summary>
        /// <param name="pFieldName"></param>
        /// <param name="pRowIndex"></param>
        /// <returns></returns>
        public object __mGridCellValue(string pFieldName, int pRowIndex)
        {
            return _cGrid[pFieldName, pRowIndex].Value;
        }
        // DataRow vDataRow = ((DataRowView)__cAreaDocument.__fGridContentRowIndex_.DataBoundItem).Row; // Запись данных соответствующая выбранной строке в сетке
        // DataRow vDataRow = ((DataRowView)__cAreaDocument._cGrid.SelectedRows[0].DataBoundItem).Row; 
        /// <summary>* Получение объекта строки сетка 
        /// </summary>
        /// <returns></returns>
        public DataRow __mGridContentDataRow()
        {
            return ((DataRowView)_cGrid.SelectedRows[__fGridCurrentRowIndex_].DataBoundItem).Row;
        }
        /// <summary>* Обновление сетки в таблице
        /// </summary>
        public void __mGridRefresh()
        {
            _cGrid.Refresh();

            return;
        }
        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Открытые

        /// <summary>
        /// Сущность данных заголовка
        /// </summary>
        public datUnitEssence __oEssence;
        /// <summary>
        /// Тип формы для изменения данных
        /// </summary>
        public Type __oFormDocumentRecord;
        /// <summary>
        /// Идентификатор записи документа
        /// </summary>
        public int __fRecordClue = -1;
        /// <summary>
        /// Идентификатор записи для копирования в новый документ 
        /// </summary>
        public int __fRecordClueForCopy = -1;
        /// <summary>
        /// Статус завершения транзакции
        /// </summary>
        public bool __fTransactionStatus = true;

        #endregion Открытые

        #region . Объекты

        /// <summary>Таблица с данными заголовка документа
        /// </summary>
        public DataTable __oDataTableHeader;

        #endregion Объекты

        #region . Компоненты

        /// <summary>* Кнопка 'Правка'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonEdit = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Правка'

        /// <summary>* Пункт меню 'Копировать' кнопки 'Правка'
        /// </summary>
        public crlComponentMenuItem _cButtonEditCopy = new crlComponentMenuItem();
        /// <summary>* Пункт меню 'Изменить' кнопки 'Правка'
        /// </summary>
        public crlComponentMenuItem _cButtonEditEdit = new crlComponentMenuItem();
        /// <summary>
        /// (f) Кнопка 'Правка / Создать'
        /// </summary>
        public crlComponentMenuItem _cButtonEditNew = new crlComponentMenuItem();
        /// <summary>
        /// (f) Кнопка 'Правка / Удалить'
        /// </summary>
        public crlComponentMenuItem _cButtonEditRemove = new crlComponentMenuItem();
        /// <summary>
        /// (f) Кнопка 'Правка / Восстановить'
        /// </summary>
        public crlComponentMenuItem _cButtonEditRestore = new crlComponentMenuItem();

        #endregion Меню кнопки 'Правка'

        /// <summary>* Кнопка 'Операции'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonOperations = new crlComponentToolBarButtonMenu();
        /// <summary>* Кнопка 'Отчеты'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonReports = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Отчеты'


        #endregion Меню кнопки 'Отчеты'

        /// <summary>* Кнопка 'Сохранить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSave = new crlComponentToolBarButton(); // Используется при сохранении приходов

        /// <summary>* Разделитель 'Заголовок / Содержание'
        /// </summary>
        protected crlComponentSplitter _cSplitterCaptionContent = new crlComponentSplitter();
        /// <summary>Разделитель заголовка 'Левое / Правое'
        /// </summary>
        protected crlComponentSplitter _cSplitterCaptionLeftRight = new crlComponentSplitter();
        /// <summary>Левая панель для размещения полей ввода
        /// </summary>
        protected crlBlockInputs _cBlockInputsLeft = new crlBlockInputs();
        /// <summary>Правая панель для размещения полей ввода
        /// </summary>
        protected crlBlockInputs _cBlockInputsRight = new crlBlockInputs();

        /// <summary>Поле ввода учетного номера
        /// </summary>
        protected crlInputNumberInt _cInputCode = new crlInputNumberInt();
        /// <summary>Поле ввода даты создания
        /// </summary>
        protected crlInputDateTime _cInputDateTimeCreate = new crlInputDateTime();

        /// <summary>Блок вкладок
        /// </summary>
        protected crlComponentPageBlock _cPageBlock = new crlComponentPageBlock();
        /// <summary>Вкладка для правки документа
        /// </summary>
        protected crlComponentPage _cPageDocument = new crlComponentPage();
        /// <summary>Область для изменения документа
        /// </summary>
        protected crlComponentGrid _cGrid = new crlComponentGrid();


        #endregion Компоненты

        #region . Свойства

        /// <summary>* Видимость кнопки 'Правка / Копировать'
        /// </summary>
        public bool __fButtonEditCopyVisible_
        {
            get { return _cButtonEditCopy.Visible; }
            set { _cButtonEditCopy.Visible = value; }
        }
        /// <summary>* Видимость кнопки 'Правка / Изменить'
        /// </summary>
        public bool __fButtonEditEditVisible_
        {
            get { return _cButtonEditEdit.Visible; }
            set { _cButtonEditEdit.Visible = value; }
        }
        /// <summary>* Видимость кнопки 'Правка / Создать'
        /// </summary>
        public bool __fButtonEditNewVisible_
        {
            get { return _cButtonEditNew.Visible; }
            set { _cButtonEditNew.Visible = value; }
        }
        /// <summary>* Видимость кнопки 'Правка / Удалить'
        /// </summary>
        public bool __fButtonEditRemoveVisible_
        {
            get { return _cButtonEditRemove.Visible; }
            set { _cButtonEditRemove.Visible = value; }
        }
        /// <summary>* Видимость кнопки 'Правка / Восстановить'
        /// </summary>
        public bool __fButtonEditRestoreVisible_
        {
            get { return _cButtonEditRestore.Visible; }
            set { _cButtonEditRestore.Visible = value; }
        }
        /// <summary>* Доступность кнопки 'Сохранить'
        /// </summary>
        public bool __fButtonSaveEnabled_
        {
            get { return _cButtonSave.Enabled; }
            set { _cButtonSave.Enabled = value; }
        }
        /// <summary>* Видимость кнопки 'Сохранить'
        /// </summary>
        public bool __fButtonSaveVisible_
        {
            get { return _cButtonSave.Visible; }
            set { _cButtonSave.Visible = value; }
        }
        /// <summary>* Видимость кнопки 'Операции'
        /// </summary>
        public bool __fButtonOperationsVisible_
        {
            get { return _cButtonOperations.Visible; }
            set { _cButtonOperations.Visible = value; }
        }

        protected crlComponentSplitter CSplitterCaptionContent { get => _cSplitterCaptionContent; set => _cSplitterCaptionContent = value; }

        /// <summary>Положение разделителя 'Заголовок | Содержание'
        /// </summary>
        public int __fSplitterCaptionContentSplitterDistance_
        {
            get { return _cSplitterCaptionContent.SplitterDistance; }
            set { _cSplitterCaptionContent.SplitterDistance = value; }
        }
        /// <summary>Положение разделителя 'Заголовок - Левая часть | Правая часть'
        /// </summary>
        public int __fSplitterCaptionLeftRightSplitterDistance_
        {
            get { return _cSplitterCaptionLeftRight.SplitterDistance; }
            set { _cSplitterCaptionLeftRight.SplitterDistance = value; }
        }


        /// <summary>* Название поля таблицы 
        /// </summary>
        public string __fInputCodeFieldName_
        {
            get { return _cInputCode.__fFieldName; }
            set { _cInputCode.__fFieldName = value; }
        }
        /// <summary>* Доступность поля ввода 'Код'
        /// </summary>
        public bool __fInputCodeEnabled_
        {
            get { return _cInputCode.__fEnabled_; }
            set { _cInputCode.__fEnabled_ = value; }
        }

        /// <summary>* Название поля таблицы 
        /// </summary>
        public string __fInputDateTimeCreateFieldName_
        {
            get { return _cInputDateTimeCreate.__fFieldName; }
            set { _cInputDateTimeCreate.__fFieldName = value; }
        }
        /// <summary>* Доступность поля ввода 'Дата-время создания'
        /// </summary>
        public bool __fInputDateTimeCreateEnabled_
        {
            get { return _cInputDateTimeCreate.__fEnabled_; }
            set { _cInputDateTimeCreate.__fEnabled_ = value; }
        }


        /// <summary>* Значение поля ввода 'Дата-время создания'
        /// </summary>
        public object __fInputDateTimeCreateValue_
        {
            get { return _cInputDateTimeCreate.__fValue_; }
            set { _cInputDateTimeCreate.__fValue_ = value; }
        }

        /// <summary>Индекс выбранной вкладки в блоке вкладок
        /// </summary>
        public int __fPageBlockSelectedPageIndex
        {
            get { return _cPageBlock.SelectedIndex; }
            set { _cPageBlock.SelectedIndex = value; }
        }

        /// <summary>* Сущность данных сетки с содержанием документа
        /// </summary>
        public datUnitEssence __oGridEssence_
        {
            get { return _cGrid.__oEssence; }
            set { _cGrid.__oEssence = value; }
        }
        /// <summary>* Курсор с данными сетки с содержанием документа
        /// </summary>
        public DataTable __oGridDataTable_
        {
            get{ return _cGrid.__oDataTable; }
            set { _cGrid.__oDataTable = value; }
        }
        /// <summary>* Идентификатор текущей записи в сетке
        /// </summary>
        public int __fGridRecordClue_
        {
            get { return _cGrid.__fRecordClue_; }
            set { _cGrid.__fRecordClue_ = value; }
        }
        /// <summary>* Индекс текущей строки сетки
        /// </summary>
        public int __fGridCurrentRowIndex_
        {
            get { return _cGrid.CurrentRow.Index; }

        }
        /// <summary>Выбранная строка в сетке
        /// </summary>
        public DataGridViewRow __oGridCurrentRow_
        {
            get { return _cGrid.SelectedRows[0]; }
        }
        /// <summary>* Доступность сетки
        /// </summary>
        public bool __fGridEnabled_
        {
            get { return _cGrid.Enabled; }
            set { _cGrid.Enabled = value; }
        }
        /// <summary>* Установка статуса видимости колонок сетки
        /// </summary>
        /// <param name="pColumnName">Название колонки</param>
        /// <param name="pVisibleStatus">Устанавливаемый статус видимости</param>
        public void __mGridColumnVisible(string pColumnName, bool pVisibleStatus)
        {
            _cGrid.Columns[pColumnName].Visible = pVisibleStatus;

            return;
        }

        #endregion . Свойства

        #endregion ПОЛЯ

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при клике левой клавишей мыши по кнопке 'Сохранить'
        /// </summary>
        public event EventHandler __eButtonSave_ClickLeft;
        /// <summary>
        /// Возникает при клике правой клавишей мыши по кнопке 'Сохранить'
        /// </summary>
        public event EventHandler __eButtonSave_ClickRight;
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
        /// <summary>
        /// Возникает после сохранения данных, до закрытия транзакции
        /// </summary>
        public event EventHandler __eDataSaveAfter;
        /// <summary>
        /// Возникает перед сохранением данных, после открытия транзакции
        /// </summary>
        public event EventHandler __eDataSaveBefore;
        /// <summary>
        /// Возникает при проверке заполненности заголовка документа
        /// </summary>
        public event EventHandler __eHeaderValid;
        /// <summary>
        /// Возникает при смене выбранной вкладки блока вкладок
        /// </summary>
        public event EventHandler __ePageBlockSelectedIndexChanged;

        #endregion СОБЫТИЯ
    }
}
