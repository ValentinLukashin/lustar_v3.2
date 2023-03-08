using nlApplication;
using nlDataMaster;
using System;
using System.Data;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlAreaRecord'
    /// </summary>
    /// <remarks>Область для изменения записи данных</remarks>
    public class crlAreaRecord : crlArea 
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Panel2.Controls.Add(_cBlockInputs);
            Panel2.Controls.SetChildIndex(_cBlockInputs, 0);
            __cToolBar.Items.Insert(0, _cButtonSave);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            // _cButtonSave
            {
                _cButtonSave.Click += _cButtonSave_Click;
                _cButtonSave.Image = global::nlResourcesImages.Properties.Resources._Diskette_b32C;
                _cButtonSave.ToolTipText = "[ Ctrl + A ]\n" + crlApplication.__oTunes.__mTranslate("Применить");
            }
            // _cBlockInputs
            {
                _cBlockInputs.Dock = DockStyle.Fill;
                _cBlockInputs.AutoScroll = true;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется после создания объекта
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if(__fRecordMustBeSaved == true)
                _mDataLoad();
        }

        #endregion Объект

        #region Кнопки управления

        /// <summary>Выполняется при выборе кнопки 'Сохранить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonSave_Click(object sender, EventArgs e)
        {
            if (_mDataSave() == true) /// Закрытие формы при удачном сохранении
                (FindForm() as Form).Close();
        }

        #endregion Кнопки управления

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Получение данных из источника данных
        /// </summary>
        /// <returns>[true] - данные получены, иначе - [false]</returns>
        public bool _mDataLoad()
        {
            bool vReturn = true; // Возвращаемое значение

            DataTable vDataTable = __oEssence._mRecord(__fRecordClue); /// Запрос данных
            
            if (__fRecordClue <= 0)
            {
                DataRow vDataRow = __oEssence.__mRecordNew(vDataTable);
                
                if (__fRecordClueForCopy > 0)
                {
                    DataTable vDataTableCopy = __oEssence._mRecord(__fRecordClueForCopy);
                    foreach (DataRow vDataRowCopy in vDataTableCopy.Rows)
                    {
                        foreach (DataColumn vDataColumnCopy in vDataTableCopy.Columns)
                        {
                            if (!(vDataColumnCopy.ColumnName.StartsWith("CLU")
                                | vDataColumnCopy.ColumnName.StartsWith("cod")
                                | vDataColumnCopy.ColumnName.StartsWith("GUI")))
                            {
                                vDataRow[vDataColumnCopy.ColumnName] = vDataRowCopy[vDataColumnCopy.ColumnName];
                            }
                        }
                    }
                } /// Если идентификатор записи для копирования указан, читаются данные из этой записи и переписываются в новую
 
                vDataTable.Rows.Add(vDataRow);
            } /// Если идентификатор записи не указан, создается новая запись

            if (vDataTable.Rows.Count == 1)
            {
                _cBlockInputs.__oDataTable = vDataTable;
                _cBlockInputs.__mDataLoad();
                vReturn = true;
            }
            else
                vReturn = false;

            if (__eOnDataLoaded != null)
                __eOnDataLoaded(this, new EventArgs()); /// Формируется событие 'Возникает после загрузки данных'

            return vReturn;
        }
        /// <summary>
        /// Сохранение данных в источнике данных
        /// </summary>
        /// <returns></returns>
        public bool _mDataSave()
        {
            if (__fRecordMustBeSaved == false)
            { /// Данные не сохраняются, сохранение имитируется
                __fRecordSaved = true;
                return true;
            }
            //if (crlApplication.__oMessages.__mShow(MESSAGESTYPES.Question, crlApplication.__oTunes.__mTranslate("Сохранить текущие данные"), "", "") == DialogResult.No)
            //{
            //    return false;
            //}

            _cBlockInputs.__mDataSave();
            bool vReturn = __oEssence.__mUpdate(_cBlockInputs.__oDataTable); /// Сохранение документа
            if (crlApplication.__oData.__mDataSourceGet(__oEssence.__fDataSourceAlias).__fDateTimeStore == DATETIMESTORE.DateTime)
            {
                if (vReturn == true & Convert.ToDateTime(_cBlockInputs.__oDataTable.Rows[0][0]) != appTypeDateTime.__mMsSqlDateEmpty())
                    __fRecordClue = datApplication.__oData.__mDataSourceGet(__oEssence.__fDataSourceAlias).__mClueLastInserted(__oEssence.__fTableName); /// Получение идентификатора вставленной записи
            }
            else
            {
                if (vReturn == true & Convert.ToInt64(_cBlockInputs.__oDataTable.Rows[0][0]) != 0)
                    __fRecordClue = datApplication.__oData.__mDataSourceGet(__oEssence.__fDataSourceAlias).__mClueLastInserted(__oEssence.__fTableName); /// Получение идентификатора вставленной записи
            }
            /// Исправление в таблице блокировок идентификатора записи заблокированной записи равного 0 на фактический идентификатор 
            datApplication.__oData.__mDataSourceGet(__oEssence.__fDataSourceAlias).__mLockLnkRidChange(__oEssence.__fLockClue, __fRecordClue);
            //crlApplication.__oData._mTransactionOn(__oEssence.__fDataSourceAlias);

            //if (oDataTable != null)
            //{
            //    bool vRecordNew = false; // Новая запись
            //    if (__fRecordClue <= 0)
            //        vRecordNew = true;

            //    foreach (Control vInput in __cBlockInputs.Controls)
            //    {
            //        if ((vInput is crlInput) == true)
            //        {
            //            if ((vInput is crlInputComboBool) == true)
            //                oDataTable.Rows[0][(vInput as crlInputComboBool).__fFieldName] = (vInput as crlInputComboBool).__fValue_;
            //            if ((vInput is crlInputComboList) == true)
            //                oDataTable.Rows[0][(vInput as crlInputComboList).__fFieldName] = (vInput as crlInputComboList).__fValue_;
            //            if ((vInput is crlInputForm) == true)
            //                oDataTable.Rows[0][(vInput as crlInputForm).__fFieldName] = (vInput as crlInputForm).__fValue_;
            //            if ((vInput is crlInputFormSearch) == true)
            //                oDataTable.Rows[0][(vInput as crlInputFormSearch).__fFieldName] = (vInput as crlInputFormSearch).__fValue_;
            //            if ((vInput is crlInputNumberDecimal) == true)
            //                oDataTable.Rows[0][(vInput as crlInputNumberDecimal).__fFieldName] = (vInput as crlInputNumberDecimal).__fValue_;
            //            if ((vInput is crlInputNumberInt) == true)
            //                oDataTable.Rows[0][(vInput as crlInputNumberInt).__fFieldName] = (vInput as crlInputNumberInt).__fValue_;
            //            if ((vInput is crlInputChar) == true)
            //                oDataTable.Rows[0][(vInput as crlInputChar).__fFieldName] = (vInput as crlInputChar).__fValue_;
            //        }
            //    }/// Запись значений в строку значений

            //    vReturn = __oEssence.__mUpdate(oDataTable); /// Сохранение документа

            //} /// Если данные загружены из источника данных, то и сохраняются в источник данных

            if (__eOnDataSaving != null)
                __eOnDataSaving(this, new EventArgs()); /// Формируется событие 'Возникает перед сохранением данных'

            //crlApplication.__oData._mTransactionOff(vReturn & __fTransactionExternal, __oEssence.__fDataSourceAlias);
            //if (vReturn == true) /// Регистрация сохранения
            //    __fRecordSaved = true;

            return vReturn;
        }
        /// <summary>
        /// Добавление поля ввода на панель полей ввода
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        public bool __mInputAdd(crlInput pInput)
        {
            return _cBlockInputs.__mInputAdd(pInput);
        }
        /// <summary>
        /// Добавление блока вкладок на панель поля ввода
        /// </summary>
        /// <param name="pPageBlock"></param>
        public void __mPageBlockAdd(crlComponentPageBlock pPageBlock, AnchorStyles pAnchorStyles = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
        {
            _cBlockInputs.__mPageBlockAdd(pPageBlock, pAnchorStyles);
        }
        /// <summary>
        /// Добавление поля ввода в блок для отображения полей ввода
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        public bool __mBlockInputsAdd(crlInput pInput)
        {
            return _cBlockInputs.__mInputAdd(pInput);
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Сохранить'
        /// </summary>
        public void __mPressButtonSave()
        {
            _cButtonSave.PerformClick();
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Идентификатор редактируемой записи
        /// </summary>
        public int __fRecordClue = -1;
        /// <summary>Идентификатор записи используемой для копирования
        /// </summary>
        public int __fRecordClueForCopy = 0;
        /// <summary>Запись должна быть сохранена при нажатии на кнопку сохранена
        /// </summary>
        public bool __fRecordMustBeSaved = true;
        /// <summary>Признак, что данные были сохранены
        /// </summary>
        public bool __fRecordSaved = false;
        /// <summary>Рещзультат выполнения внешней транзакции
        /// </summary>
        public bool __fTransactionExternal = true;

        #endregion - Атрибуты

        #region - Компоненты

        /// <summary>* Кнопка 'Сохранить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSave = new crlComponentToolBarButton();
        /// <summary>* Блок для отображения полей ввода
        /// </summary>
        protected crlBlockInputs _cBlockInputs = new crlBlockInputs();

        #endregion - Компоненты

        #region - Объекты

        /// <summary>Сущность редактируемых данных
        /// </summary>
        public datUnitEssence __oEssence;

        #endregion - Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Разрешение отображения галочки во всех добавляемых полях ввода
        /// </summary>
        public bool __fBlockInputsCheckShow_
        {
            get { return _cBlockInputs.__fCheckShow; }
            set { _cBlockInputs.__fCheckShow = value; }
        }
        /// <summary>
        /// Получение списка полей ввода в блоке для отображения полей ввода
        /// </summary>
        public ControlCollection __fBlockInputsControls_
        {
            get { return _cBlockInputs.Controls; }

        }
        /// <summary>
        /// Отклонение от верхнего края последнего компонента
        /// </summary>
        public int __fBlockInputsTopCoordinate_
        {
            get { return _cBlockInputs.__fTopCoordinate; }
            set { _cBlockInputs.__fTopCoordinate = value; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>Возникает после загрузки данных
        /// </summary>
        public event EventHandler __eOnDataLoaded;
        /// <summary>Возникает после сохранения данных, но до закрытия транзакции
        /// </summary>
        public event EventHandler __eOnDataSaving;

        #endregion = СОБЫТИЯ
    }
}
