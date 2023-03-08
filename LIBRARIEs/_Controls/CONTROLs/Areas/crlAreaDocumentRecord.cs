using nlApplication;
using nlDataMaster;
using System;
using System.Data;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlAreaDocumentRecord'
    /// </summary>
    /// <remarks>Класс области для правки записи документа</remarks>
    public class crlAreaDocumentRecord : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(_cBlockInputs);
            Controls.SetChildIndex(_cBlockInputs, 0);
            __cToolBar.Items.Insert(0, _cButtonSave);

            #endregion Размещение компонентов

            #region Настройка компонентов

            _cBlockInputs.__fCheckShow = false; // Скрыть галочки

            // _cButtonSave
            {
                _cButtonSave.Click += _cButtonSave_Click;
                _cButtonSave.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
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
        /// <summary>Выполняется после создания объекта
        /// </summary>
        protected override void _mObjectPresetation()
        {
            if (__fRecordMustBeSaved == true)
                __mDataLoad();
        }

        #endregion Объект

        #region Кнопки управления

        /// <summary>Выполняется при выборе кнопки 'Сохранить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonSave_Click(object sender, EventArgs e)
        {
            if (__mDataSave() == true) /// Закрытие формы при удачном сохранении
                (FindForm() as Form).Close();
        }

        #endregion Кнопки управления

        #endregion Поведение

        #region - Процедуры

        /// <summary>* Получение данных из источника данных
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mDataLoad()
        {
            bool vReturn = true; // Возвращаемое значение
            DataTable vDataTable = __oDataRow.Table;

            foreach (DataColumn vDataColumnCopy in vDataTable.Columns)
            { ///* Перебор колонок в таблице полученной строке
                foreach (crlInput vInput in _cBlockInputs.Controls)
                {
                    bool vFind = false;

                    if (vInput is crlInputChar)
                    {
                        vInput.__fValue_ = Convert.ToString(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vInput is crlInputComboBool)
                    {
                        vInput.__fValue_ = Convert.ToBoolean(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vInput is crlInputComboList)
                    {
                        vInput.__fValue_ = Convert.ToInt32(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vInput is crlInputDateTime)
                    {
                        vInput.__fValue_ = Convert.ToDateTime(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vInput is crlInputFormSearch)
                    {
                        vInput.__fValue_ = Convert.ToInt32(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vInput is crlInputNumberDecimal)
                    {
                        vInput.__fValue_ = Convert.ToDecimal(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vInput is crlInputNumberInt)
                    {
                        vInput.__fValue_ = Convert.ToInt32(__oDataRow[vInput.__fFieldName]);
                        vFind = true;
                    }
                    if (vFind == false)
                    {
                        appUnitError vError = new appUnitError();
                        vError.__fErrorsType = ERRORSTYPES.Programming;
                        vError.__fProcedure = _fClassNameFull + "_mDataLoad()";                        
                        vError.__mMessageBuild("Не все поля привязаны к компонентам");
                        crlApplication.__oErrorsHandler.__mShow(vError);
                        vReturn = false;
                    } // Обработка ошибки
                }
            }

            if (__eOnDataLoaded != null)
                __eOnDataLoaded(this, new EventArgs()); /// Формируется событие 'Возникает после загрузки данных'

            return vReturn;
        }
        /// <summary>* Сохранение данных в источнике данных
        /// </summary>
        /// <returns></returns>
        public bool __mDataSave()
        {
            bool vReturn = true; // Возвращаемое значение

            foreach (Control vInput in _cBlockInputs.Controls)
            {
                if ((vInput is crlInput) == true)
                {
                    bool vFind = false;

                    if ((vInput is crlInputChar) == true)
                    {
                        __oDataRow[(vInput as crlInputChar).__fFieldName] = (vInput as crlInputChar).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputComboBool) == true)
                    {
                        __oDataRow[(vInput as crlInputComboBool).__fFieldName] = (vInput as crlInputComboBool).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputComboList) == true)
                    {
                        __oDataRow[(vInput as crlInputComboList).__fFieldName] = (vInput as crlInputComboList).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputDateTime) == true)
                    {
                        __oDataRow[(vInput as crlInputDateTime).__fFieldName] = (vInput as crlInputDateTime).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputForm) == true)
                    {
                        __oDataRow[(vInput as crlInputForm).__fFieldName] = (vInput as crlInputForm).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputFormSearch) == true)
                    {
                        __oDataRow[(vInput as crlInputFormSearch).__fFieldName] = (vInput as crlInputFormSearch).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputNumberDecimal) == true)
                    {
                        __oDataRow[(vInput as crlInputNumberDecimal).__fFieldName] = (vInput as crlInputNumberDecimal).__fValue_;
                        vFind = true;
                    }
                    if ((vInput is crlInputNumberInt) == true)
                    {
                        __oDataRow[(vInput as crlInputNumberInt).__fFieldName] = (vInput as crlInputNumberInt).__fValue_;
                        vFind = true;
                    }
                    if (vFind == false)
                    {
                        appUnitError vError = new appUnitError();
                        vError.__fErrorsType = ERRORSTYPES.Programming;
                        vError.__fProcedure = _fClassNameFull + "_mDataLoad()";
                        vError.__mMessageBuild("Не все поля привязаны к компонентам");
                        crlApplication.__oErrorsHandler.__mShow(vError);
                        vReturn = false;
                    } // Обработка ошибки
                }
            }/// Запись значений в строку значений

            __fRecordSaved = true;

            if (__eOnDataSaving != null)
                __eOnDataSaving(this, new EventArgs()); /// Формируется событие 'Возникает перед сохранением данных'

            return vReturn;
        }
        /// <summary>* Добавление поля ввода на панель полей ввода
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        public bool __mInputAdd(crlInput pInput)
        {
            return _cBlockInputs.__mInputAdd(pInput);
        }
        /// <summary>* Добавление блока вкладок на панель поля ввода
        /// </summary>
        /// <param name="pPageBlock"></param>
        public void __mPageBlockAdd(crlComponentPageBlock pPageBlock, AnchorStyles pAnchorStyles = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
        {
            _cBlockInputs.__mPageBlockAdd(pPageBlock, pAnchorStyles);
        }

        /// <summary>* Выполняется при выборе кнопки 'Сохранить' левой кнопкой мыши
        /// </summary>
        public void __mPressButtonSave()
        {
            _cButtonSave.PerformClick();

            return;
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

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
        /// <summary>* Панель для отображения полей ввода
        /// </summary>
        protected crlBlockInputs _cBlockInputs = new crlBlockInputs();

        #endregion - Компоненты

        #region - Объекты

        /// <summary>Строка из таблицы источника данных
        /// </summary>
        public DataRow __oDataRow;

        #endregion - Объекты

        #endregion ПОЛЯ

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
