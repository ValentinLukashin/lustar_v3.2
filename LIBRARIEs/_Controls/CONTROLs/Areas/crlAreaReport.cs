using nlApplication;
using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlAreaReport'
    /// </summary>
    /// <remarks>Область для формирования условия отчета</remarks>
    public class crlAreaReport : crlArea
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

            #region Размещение компонентов

            Panel2.Controls.Add(_cBlockInputs);
            Panel2.Controls.SetChildIndex(_cBlockInputs, 0);
            Panel2.Controls.SetChildIndex(__cToolBar, 1);
            __cToolBar.Items.Insert(0, _cButtonRun);
            __cToolBar.Items.Add(_cButtonOperations);

            _cButtonOperations.DropDownItems.Add(_cButtonOperationsAccess);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cButtonRun
            {
                _cButtonRun.Click += mButtonRun_Click;
                _cButtonRun.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                _cButtonRun.ToolTipText = "[ Ctrl + A ]\n" + crlApplication.__oTunes.__mTranslate("Выполнить");
            }
            // __cButtonOperations
            {
                _cButtonOperations.Alignment = ToolStripItemAlignment.Right;
                _cButtonOperations.DropDownOpened += cButton_DropDownOpened;
                _cButtonOperations.Image = global::nlResourcesImages.Properties.Resources._PageGear_y32;
                _cButtonOperations.ToolTipText = "[ Ctrl + O] " + crlApplication.__oTunes.__mTranslate("Операции");
                {
                    _cButtonOperationsAccess.Click += __cButtonOperationsAccess_Click;
                    _cButtonOperationsAccess.Image = global::nlResourcesImages.Properties.Resources._UserEdit_b16;
                    _cButtonOperationsAccess.__fCaption_ = "Определение прав пользователей";
                }
            }
            // _cBlockInputs
            {
                _cBlockInputs.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }

        protected override void OnCreateControl()
        {
            __mFilterLoad();
            base.OnCreateControl();
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            __mFilterSave();
            base.OnHandleDestroyed(e);
        }

        #endregion Объект

        #region Кнопки управления

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
        /// Выполняется при выборе кнопки 'Выполнить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonRun_Click(object sender, EventArgs e)
        {
            if (__eButtonRunClick != null)
                __eButtonRunClick(_cButtonRun, e);
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Операции / Определение прав пользователей'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cButtonOperationsAccess_Click(object sender, EventArgs e)
        {
            if (__eButtonUsersAccessClick != null)
                __eButtonUsersAccessClick(this, new EventArgs());
        }

        #endregion Кнопки управления

        #endregion - Поведение

        #region - Процедуры
        public void __mFilterBuild(ref string pFilter, ref string pFilterMessage)
        {
            foreach (Control vInput in _cBlockInputs.Controls)
            { /// Перебор установленных компонентов фильтра
                if ((vInput as crlInput).__fCheckStatus_ == true)
                {
                    if((vInput as crlInput).__fFieldName.Length != 0)
                    {
                        pFilter = pFilter + (pFilter.Length > 0 ? " and " : "");
                        pFilterMessage += (vInput as crlInput).__fFilterMessage_ + "<BR>";
                        if((vInput is crlInputChar) == true)
                            pFilter = pFilter + (vInput as crlInputChar).__fFilterExpression_;
                        if ((vInput is crlInputComboBool) == true)
                            pFilter = pFilter + (vInput as crlInputComboBool).__fFilterExpression_;
                        if ((vInput is crlInputDateTimePeriod) == true)
                                pFilter = pFilter + (vInput as crlInputDateTimePeriod).__fFilterExpression_;
                        if ((vInput is crlInputFormSearch) == true)
                                pFilter = pFilter + (vInput as crlInputFormSearch).__fFilterExpression_;
                            //default:
                            //    pFilter = pFilter + ((vInput as crlInput).__fTableAlias.Length != 0 ? "" : (vInput as crlInput).__fTableAlias + ".") + (vInput as crlInput).__fFieldName + "=" + (vInput as crlInput).__fValue_.ToString();
                            //    break;
                    }
                }
            }
        }
        /// <summary>
        /// Загрузка настроек фильтра из файла
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mFilterLoad()
        {
            bool vReturn = true; // Возвращаемое значение
            crlForm vForm = FindForm() as crlForm;
            string _fFormNameParent = vForm.Name;
            appFileIni oFileIni = vForm.__oFileIni; // Объект для работы с инициализационным файлом
            oFileIni.__fFilePath = crlApplication.__oPathes.__mFileFormTunes();

            /// Перебор установленных компонентов фильтра
            foreach (Control vInput in _cBlockInputs.Controls)
            { /// Перебор установленных компонентов фильтра
                string vFieldName = (vInput as crlInput).__fFieldName; // Название поля для которого строиться фильтр

                if ((vInput is crlInput) == true)
                { /// Компонент - поле ввода
                    try
                    {
                        (vInput as crlInput).__fCheckStatus_ = Convert.ToBoolean(oFileIni.__mValueRead(_fFormNameParent, "FilterStatus_" + vFieldName)); /// Загрузка статуса
                    }
                    catch
                    {
                        (vInput as crlInput).__fCheckStatus_ = false;
                    }

                    try
                    {
                        if (vInput is crlInputComboBool)
                            (vInput as crlInputComboBool).__fValue_ = Convert.ToBoolean(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputComboList)
                            (vInput as crlInputComboList).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputDateTime)
                            (vInput as crlInputDateTime).__fValue_ = Convert.ToDateTime(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputDateTimePeriod)
                        {
                            if ((vInput as crlInputDateTimePeriod).__pValueInTicks == false)
                            {
                                (vInput as crlInputDateTimePeriod).__fValue_ = Convert.ToDateTime(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                                (vInput as crlInputDateTimePeriod).__fValueTo_ = Convert.ToDateTime(oFileIni.__mValueRead(_fFormNameParent, "FilterValueTo_" + vFieldName));
                            } /// Данные храняться как дата-время
                            else
                            {
                                (vInput as crlInputDateTimePeriod).__fValue_ = new DateTime(Convert.ToInt64(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName)));
                                (vInput as crlInputDateTimePeriod).__fValueTo_ = new DateTime(Convert.ToInt64(oFileIni.__mValueRead(_fFormNameParent, "FilterValueTo_" + vFieldName)));
                            } /// Данные храняться как тики
                        }
                        if (vInput is crlInputForm)
                            (vInput as crlInputForm).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputFormSearch)
                            (vInput as crlInputFormSearch).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputNumberDecimal)
                            (vInput as crlInputNumberDecimal).__fValue_ = Convert.ToDecimal(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputNumberInt)
                            (vInput as crlInputNumberInt).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputPhone)
                            (vInput as crlInputPhone).__fValue_ = oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName);
                        if (vInput is crlInputChar)
                            (vInput as crlInputChar).__fValue_ = oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName);
                        if (vInput is crlInputListBoxChecked)
                        {
                            (vInput as crlInputListBoxChecked).__mCheckedSet(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        }
                    }
                    catch
                    {
                        (vInput as crlInput).__fCheckStatus_ = false; /// Первая загрузка статуса
                    }
                } /// Компонент - поле ввода

            }

            return vReturn;
        }
        /// <summary>
        /// Сохранение настроек фильтра в файл
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mFilterSave()
        {
            bool vReturn = true; // Возвращаемое значение
            crlForm vForm = FindForm() as crlForm;
            string _fFormNameParent = vForm.Name;
            appFileIni oFileIni = vForm.__oFileIni; // Объект для работы с инициализационным файлом
            /// Перебор установленных компонентов фильтра
            foreach (Control vInput in _cBlockInputs.Controls)
            { 
                string vFieldName = (vInput as crlInput).__fFieldName; // Название поля для которого строиться фильтр
                /// Компонент - поле ввода
                if ((vInput is crlInput) == true)
                { 
                    oFileIni.__mValueWrite((vInput as crlInput).__fCheckStatus_.ToString(), _fFormNameParent, "FilterStatus_" + vFieldName); /// Сохранение статуса использования текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fCaption_, _fFormNameParent, "FilterCaption_" + vFieldName); /// Сохранение заголовка текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fFilterExpression_, _fFormNameParent, "FilterExpression_" + vFieldName); /// Сохранение условия фильтра текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fFilterMessage_, _fFormNameParent, "FilterMessage_" + vFieldName); /// Сохранение выражение фильтра текущего компонента

                    if (vInput is crlInputComboList)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputComboList).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputDateTime)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputDateTime).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputDateTimePeriod)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputDateTimePeriod).__fValue_.ToString(), _fFormNameParent, "FilterValueFrom_" + vFieldName);
                        oFileIni.__mValueWrite((vInput as crlInputDateTimePeriod).__fValueTo_.ToString(), _fFormNameParent, "FilterValueTo_" + vFieldName);
                    }
                    if (vInput is crlInputForm)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputForm).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputFormSearch)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputFormSearch).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputNumberDecimal)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputNumberDecimal).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputNumberInt)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputNumberInt).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputPhone)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputPhone).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputChar)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputChar).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputListBoxChecked)
                    {
                        string vCheckedLine = (vInput as crlInputListBoxChecked).__mCheckedGet();
                        oFileIni.__mValueWrite(vCheckedLine, _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                } /// Компонент - поле ввода
            } /// Перебор установленных компонентов фильтра

            return vReturn;
        }
        /// <summary>
        /// Добавление поля ввода на панель поля ввода
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        public bool __mInputAdd(crlInput pInput)
        {
            return _cBlockInputs.__mInputAdd(pInput);
        }

        #region ! Внешние нажатия на кнопки управления

        /// <summary>* Выполняется при выборе кнопки 'Выполнить'
        /// </summary>
        public void __mPressButtonRun()
        {
            _cButtonRun.PerformClick();

            return;
        }
        /// <summary>* Выполняется при выборе кнопки 'Операции'
        /// </summary>
        public void __mPressButtonOperations()
        {
            _cButtonOperations.ShowDropDown();

            return;
        }

        #endregion Внешние нажатия на кнопки управления

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>* Кнопка 'Применить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonRun = new crlComponentToolBarButton();
        /// <summary>* Кнопка 'Операции'
        /// </summary>
        protected crlComponentToolBarButtonMenu _cButtonOperations = new crlComponentToolBarButtonMenu();

        #region Меню кнопки 'Операции'

        /// <summary>* Кнопка 'Операции / Доступ'
        /// </summary>
        protected crlComponentMenuItem _cButtonOperationsAccess = new crlComponentMenuItem();

        #endregion Меню кнопки 'Операции'
        
        /// <summary>Панель для отображения полей ввода
        /// </summary>
        protected crlBlockInputs _cBlockInputs = new crlBlockInputs();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>* Отклонение от верхнего края последнего компонента
        /// </summary>
        public int __fBlockInputsTopCoordinate_
        {
            get { return _cBlockInputs.__fTopCoordinate; }
            set { _cBlockInputs.__fTopCoordinate = value; }
        }
        /// <summary>* Доступность кнопки 'Выполнить'
        /// </summary>
        public bool __fButtonRunEnabled_
        {
            get { return _cButtonRun.Enabled; }
            set { _cButtonRun.Enabled = value; }
        }
        /// <summary>* Доступность кнопки 'Операции'
        /// </summary>
        public bool __fButtonOperationsEnabled_
        {
            get { return _cButtonOperations.Enabled; }
            set { _cButtonOperations.Enabled = value; }
        }
        /// <summary>* Доступность пункта меню 'Доступ' кнопки управления 'Операции'
        /// </summary>
        public bool __fButtonOperationsAccessEnabled_
        {
            get { return _cButtonOperationsAccess.Enabled; }
            set { _cButtonOperationsAccess.Enabled = value; }
        }
        
        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при выборе кнопки 'Выполнить'
        public event EventHandler __eButtonRunClick;
        /// <summary>
        /// Возникает при выборе пункта меню 'Операции / Права пользователей'
        /// </summary>
        public event EventHandler __eButtonUsersAccessClick;

        #endregion = СОБЫТИЯ
    }
}
