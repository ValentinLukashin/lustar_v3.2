using nlApplication;
using nlDataMaster;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    public class crlAreaTree : crlArea
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

            Panel2.Controls.Add(_cTree);
            Panel2.Controls.SetChildIndex(_cTree, 0);

            __cToolBar.Items.Insert(0, _cButtonSelect);
            __cToolBar.Items.Insert(1, _cButtonRefresh);
            __cToolBar.Items.Add(_cButtonOperations);
            __cToolBar.Items.Add(_cButtonReports);
            __cToolBar.Items.Add(_cButtonEdit);

            _cButtonEdit.DropDownItems.Add(_cButtonEditCreate);
            _cButtonEdit.DropDownItems.Add(_cButtonEditCopy);
            _cButtonEdit.DropDownItems.Add(_cButtonEditEdit);
            _cButtonEdit.DropDownItems.Add(_cButtonEditRemove);
            _cButtonEdit.DropDownItems.Add(_cButtonEditRestore);

            _cButtonOperations.DropDownItems.Add(_cButtonOperationsAccess);

            #endregion Размешение компонентов

            #region /// Настройка компонентов

            // __cToolBar
            {
                // __cButtonRefresh
                {
                    _cButtonRefresh.__eMouseClickLeft += mButtonRefresh_eMouseClickLeft;
                    _cButtonRefresh.__eMouseClickRight += mButtonRefresh_eMouseClickRight;
                    _cButtonRefresh.Image = global::nlResourcesImages.Properties.Resources._SignRefresh_b32C;
                    _cButtonRefresh.ToolTipText = "[ F5 ] " + crlApplication.__oTunes.__mTranslate("Обновить");
                }
                // __cButtonEdit
                {
                    _cButtonEdit.Alignment = ToolStripItemAlignment.Right;
                    _cButtonEdit.DropDownOpened += mButton_DropDownOpened;
                    _cButtonEdit.Image = global::nlResourcesImages.Properties.Resources._PageEdit_y32;
                    _cButtonEdit.ToolTipText = "[ Ctrl + E ] " + crlApplication.__oTunes.__mTranslate("Правка");
                    {
                        _cButtonEditCopy.Click += mButtonEditCopy_Click;
                        _cButtonEditCopy.Image = global::nlResourcesImages.Properties.Resources._PageCopy_w16;
                        _cButtonEditCopy.__fCaption_ = "Копировать";

                        _cButtonEditEdit.Click += mButtonEditEdit_Click;
                        _cButtonEditEdit.Image = global::nlResourcesImages.Properties.Resources._PageEdit_w16;
                        _cButtonEditEdit.__fCaption_ = "Изменить";

                        //_cButtonEditNew.Click += _mButtonEditNew_Click;
                        _cButtonEditCreate.Image = global::nlResourcesImages.Properties.Resources._Page_w16C;
                        _cButtonEditCreate.__fCaption_ = "Создать";

                        //_cButtonEditRemove.Click += _mButtonEditRemove_Click;
                        _cButtonEditRemove.Image = global::nlResourcesImages.Properties.Resources._PageRemove_w16;
                        _cButtonEditRemove.__fCaption_ = "Удалить";

                        //_cButtonEditRestore.Click += _mButtonEditRestore_Click;
                        _cButtonEditRestore.Image = global::nlResourcesImages.Properties.Resources._PageRestore_w16;
                        _cButtonEditRestore.__fCaption_ = "Восстановить";
                    }

                }
                // __cButtonOperations
                {
                    _cButtonOperations.Alignment = ToolStripItemAlignment.Right;
                    _cButtonOperations.Image = global::nlResourcesImages.Properties.Resources._PageGear_y32;
                    _cButtonOperations.ToolTipText = "[ Ctrl + O ] " + crlApplication.__oTunes.__mTranslate("Операции");
                    {
                        //t _cButtonOperationsAccess.Click += mButtonOperationsAccess_Click;
                        _cButtonOperationsAccess.Image = global::nlResourcesImages.Properties.Resources._UserEdit_b16;
                        _cButtonOperationsAccess.__fCaption_ = "Определение прав пользователей";
                    }
                }
                // __cButtonReports
                {
                    _cButtonReports.Alignment = ToolStripItemAlignment.Right;
                    _cButtonReports.Image = global::nlResourcesImages.Properties.Resources._PageInBox_y32C;
                    _cButtonReports.ToolTipText = "[ Ctrl + R ] " + crlApplication.__oTunes.__mTranslate("Отчеты");
                }
                // __cButtonSelect
                {
                    _cButtonSelect.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                    _cButtonSelect.ToolTipText = "[ Ctrl + A ] " + crlApplication.__oTunes.__mTranslate("Выбрать");
                }
            }
            // __cConsoleInputs
            {
                _cTree.Dock = DockStyle.Fill;
            }
            // __cTree
            {
                _cTree.Dock = DockStyle.Fill;
                _cTree.DrawNode += __cTree_DrawNode;
                _cTree.__eMouseClickLeft += mTree_eMouseClickLeft;
                _cTree.__eMouseClickRight += mTree_eMouseClickRight;
                _cTree.DoubleClick += _cTree_DoubleClick;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        /// <summary>
        /// Выполняется при двойном клике мыши по узлу дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cTree_DoubleClick(object sender, EventArgs e)
        {
            if (__eNodeMouseDoubleClick != null)
                __eNodeMouseDoubleClick(_cTree.SelectedNode, e);
        }
        /// <summary>
        /// Выполняется при смене вкладки  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cPageBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            __mDataLoad();
        }

        #region - Кнопки управления

        /// <summary>
        /// Выполняется при выборе кнопки 'Правка'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButton_DropDownOpened(object sender, EventArgs e)
        {
            _fDropDownOpened = true;
        }

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
                    //(vFormDocument as crlFormDocument).__cAreaDocument.__fRecordClueForCopy = _cTree.__fRecordClue_;
                    //(vFormDocument as crlFormDocument).ShowDialog();
                }
                /// Вызов формы редактирования записи
                if (__fFormOpenedType == FORMOPENEDTYPES.FormRecord)
                {
                    crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                    //(vFormRecord as crlFormRecord).__cAreaRecord.__fRecordClueForCopy = _cGrid.__fRecordClue_;
                    //(vFormRecord as crlFormRecord).ShowDialog();
                }
                __mDataLoad(); /// Перегрузка данных
            }
        }
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
                        vFormDocument.__cAreaDocument.__fRecordClue = _cTree.__fRecordClue_;
                        (vFormDocument as crlFormDocument).ShowDialog();
                    }
                    /// Вызов формы редактирования записи
                    if (__fFormOpenedType == FORMOPENEDTYPES.FormRecord)
                    {
                        crlFormRecord vFormRecord = (crlFormRecord)Activator.CreateInstance(__oFormOpened);
                        vFormRecord.__cAreaRecord.__fRecordClue = _cTree.__fRecordClue_;
                        (vFormRecord as crlFormRecord).ShowDialog();
                    }
                    __mDataLoad(); /// Перегрузка данных
                }
            }
            else
                __fEditLock = false;
        }

        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь' левой клавишей мыши
        /// </summary>
        private void mButtonRefresh_eMouseClickLeft(object sender, EventArgs e)
        {
            if (__eButtonRefresh_ClickLeft != null)
                __eButtonRefresh_ClickLeft(_cButtonRefresh, new EventArgs());

            __mDataLoad();
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь' правой клавишей мыши
        /// </summary>
        private void mButtonRefresh_eMouseClickRight(object sender, EventArgs e)
        {
            if (__eButtonRefresh_ClickRight != null)
                __eButtonRefresh_ClickRight(_cButtonRefresh, new EventArgs());
        }

        #endregion Кнопки управления

        #region Дерево

        /// <summary>
        /// Выполняется при выборе узла дерева левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTree_eMouseClickLeft(object sender, EventArgs e)
        {
            if (__eNodeMouseClickLeft != null)
                __eNodeMouseClickLeft(this._cTree, new EventArgs());
        }
        /// <summary>
        /// Выполняется при выборе узла дерева правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTree_eMouseClickRight(object sender, EventArgs e)
        {
            if (__eNodeMouseClickRight != null)
                __eNodeMouseClickRight(this._cTree, new EventArgs());
        }
        /// <summary>
        /// Выполняется для перерисовки ячеек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //https://coderoad.ru/35155193/%D0%A0%D0%B0%D1%81%D0%BA%D1%80%D0%B0%D1%81%D1%8C%D1%82%D0%B5-%D1%83%D0%B7%D0%B5%D0%BB-treeview-%D0%B4%D1%80%D1%83%D0%B3%D0%B8%D0%BC-%D1%86%D0%B2%D0%B5%D1%82%D0%BE%D0%BC
        }

        #endregion Дерево

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка данных из источника данных
        /// </summary>
        public void __mDataLoad()
        {
            if(__eDataLoadBefore != null)
                __eDataLoadBefore(_cTree, new EventArgs());

            _cTree.__mDataLoad();

            if (__eDataLoadAfter != null)
                __eDataLoadAfter(_cTree, new EventArgs());
        }
        /// <summary>
        /// Загрузка дерева из [DataTable]
        /// </summary>
        /// <example>
        ///        DataRow vDataRowTable = vDataTable.NewRow();
        ///        vDataRowTable["clu"] = vIdentifier;
        ///        vDataRowTable["des"] = vModelTable.__fName + "   " + vModelTable.__fDescription;
        ///        vDataRowTable["tag"] = "0, " + vModelTable.__fEssenceName;
        ///        vDataTable.Rows.Add(vDataRowTable);
        /// </example>
        /// <param name="pDataTable"></param>
        public void __mDataLoad(DataTable pDataTable)
        {
            if (__eDataLoadBefore != null)
                __eDataLoadBefore(_cTree, new EventArgs());

            crlTreeNode vNode = new crlTreeNode();
            _cTree.Font = new Font("Microsoft Sans Serif", 9F);
            foreach (DataRow vDataRow in pDataTable.Rows)
            {
                if (appTypeString.__mWordNumberComma(Convert.ToString(vDataRow["tag"]), 0) == "0")
                    vNode = _cTree.__mNodeNew(Convert.ToString(vDataRow["des"]) + new String(' ', 20), Convert.ToString(vDataRow["tag"]), -1, -1, new Font("Microsoft Sans Serif", 9F, FontStyle.Bold), _cTree.ForeColor);
                else
                    _cTree.__mNodeSupply(vNode, Convert.ToString(vDataRow["des"]) + new String(' ', 20), Convert.ToString(vDataRow["tag"]), -1, -1, new Font("Microsoft Sans Serif", 9F), _cTree.ForeColor);
            }

            if (__eDataLoadAfter != null)
                __eDataLoadAfter(_cTree, new EventArgs());
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

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Блокировка изменения документа
        /// </summary>
        public bool __fEditLock = false;

        #endregion Атрибуты

        #region - Компоненты

        /// <summary>
        /// Кнопка 'Обновить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonRefresh = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Правка'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonEdit = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Правка'

        /// <summary>
        /// Пункт меню 'Копировать' кнопки 'Правка'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditCopy = new crlComponentMenuItem();
        /// <summary>
        /// Пункт меню 'Изменить' кнопки 'Правка'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditEdit = new crlComponentMenuItem();
        /// <summary>
        /// Пункт меню 'Создать' кнопки 'Правка'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditCreate = new crlComponentMenuItem();
        /// <summary>
        /// Пункт меню 'Удалить' кнопки 'Правка'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditRemove = new crlComponentMenuItem();
        /// <summary>
        /// Пункт меню 'Восстановить' кнопки 'Правка'
        /// </summary>
        protected crlComponentMenuItem _cButtonEditRestore = new crlComponentMenuItem();

        #endregion Меню кнопки 'Правка'

        /// <summary>
        /// Кнопка 'Операции'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonOperations = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Операции'

        /// <summary>Кнопка 'Операции / Доступ'
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

        #endregion Менб кнопки 'Отчеты'

        /// <summary>
        /// Кнопка 'Выбрать'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSelect = new crlComponentToolBarButton();

        /// <summary>
        /// Дерево
        /// </summary>
        protected crlComponentTree _cTree = new crlComponentTree();

        #endregion - Компоненты

        #region = Объекты

        /// <summary>
        /// Форма для формирования фильтра
        /// </summary>
        public Type __oFormFilter;
        /// <summary>
        /// Форма для редактирования записи
        /// </summary>
        public Type __oFormOpened;
        /// <summary>
        /// Тип формы для изменения данных
        /// </summary>
        public FORMOPENEDTYPES __fFormOpenedType = FORMOPENEDTYPES.FormRecord;

        /// <summary>
        /// Форма для определения прав пользователей
        /// </summary>
        public Type __oFormUsersAccess;

        #endregion - Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Выбранный узел дерева
        /// </summary>
        public TreeNode __fSelectedTreeNode_
        {
            get { return _cTree.SelectedNode; }
        }
        /// <summary>
        /// Сущность данных
        /// </summary>
        public datUnitEssence __oEssence_
        {
            get { return _cTree.__oEssence; }
            set { _cTree.__oEssence = value; }
        }

        #region ! Доступность кнопок управления

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
            get { return __fButtonReportsCurrentListEnabled_; }
            set { __fButtonReportsCurrentListEnabled_ = value; }
        }
        /// <summary>
        /// Доступность пункта меню 'История корректировок' кнопки 'Отчеты'
        /// </summary>
        public bool __fButtonReportsHistoryEnabled_
        {
            get { return __fButtonReportsHistoryEnabled_; }
            set { __fButtonReportsHistoryEnabled_ = value; }
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

        #region ! Видимость кнопок управления

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

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при выборе кнопки 'Обновить' левой клавишей мыши 
        /// </summary>
        public event EventHandler __eButtonRefresh_ClickLeft;
        /// <summary>
        /// Возникает при выборе кнопки 'Обновить' правой клавишей мыши 
        /// </summary>
        public event EventHandler __eButtonRefresh_ClickRight;
        /// <summary>
        /// Возникает при выборе кнопки 'Правка / Изменить' правой клавишей мыши 
        /// </summary>
        public event EventHandler __eButtonEditEditClick;
        /// <summary>
        /// Возникает при выборе узла дерева левой кнопкой мыши
        /// </summary>
        public event EventHandler __eNodeMouseClickLeft;
        /// <summary>
        /// Возникает при выборе узла дерева правой кнопкой мыши
        /// </summary>
        public event EventHandler __eNodeMouseClickRight;
        /// <summary>
        /// Возникает при двойном клике мыши по узлу дерева
        /// </summary>
        public event EventHandler __eNodeMouseDoubleClick;
        /// <summary>
        /// Возникает после загрузки данных
        /// </summary>
        public event EventHandler __eDataLoadAfter;
        /// <summary>
        /// Возникает перед загрузкой данных
        /// </summary>
        public event EventHandler __eDataLoadBefore;

        #endregion СОБЫТИЯ
    }
}
