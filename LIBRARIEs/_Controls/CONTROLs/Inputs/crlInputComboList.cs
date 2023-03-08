using nlDataMaster;
using System;
using System.Collections;
using System.Data;
using System.Drawing;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputComboList'
    /// </summary>
    /// <remarks>Поле выбора данных из выпадающего списка</remarks>
    public class crlInputComboList : crlInput
    {
        #region = МЕТОДЫ

        #region . Поведение

        /// <summary>
        /// * Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cLabelCaption
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Normal; /// Назначение вида надписи-заголовка - 'Надпись' 
                _cLabelCaption.__eMouseClickRight += _cLabelCaption___eMouseClickRight;
            }
            fLabelCaptionStatus = _cLabelCaption.__fLabelType_; /// Сохранение установленного статуса надписи-заголовка
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.__eValueInteractiveChanged += _cInput___eValueInteractiveChanged;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// * Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cLabelCaption___eMouseClickRight(object sender, EventArgs e)
        {
            _cInput.Text = ""; /// * Очищается название искомого справочника
            __fCheckStatus_ = false; /// !!! Выключается использование фильтра
            __fValue_ = 0;
            _cInput.Focus(); /// Перемещается курсор в поле ввода
        }
        /// <summary>Выполняется при изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput___eValueInteractiveChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра
            if (__eValueInteractiveChanged != null)
                __eValueInteractiveChanged(this, new EventArgs());
        }

        #endregion Поведение

        #region . Процедуры

        /// <summary>
        /// * Обновление отображаемых данных
        /// </summary>
        public virtual void __mDataRefresh()
        {
            _cInput.__mDataRefresh();
            _cLabelValue.Text = _cInput.Text;

            return;
        }
        /// <summary>
        ///  * Перевод фокуса на поле ввода
        /// </summary>
        public override void __mInputFocus()
        {
            _cInput.Focus();

            return; 
        }
        /// <summary>
        /// * Добавление значения в конец списка значений компонента
        /// </summary>
        /// <param name="pValue">Добавляемое значение</param>
        /// <returns>Идентификатор добавляемой записи, положительный - из таблицы, отрицательный - назначаемый компонентом</returns>
        public virtual int __mItemAdd(string pValue)
        {
            return _cInput.__mItemAdd(pValue);
        }
        /// <summary>
        /// * Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public virtual bool __mItemsAdd(ArrayList pValueS)
        {
            return _cInput.__mItemsAdd(pValueS);
        }
        /// <summary>
        /// * Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public virtual bool __mItemsAdd(params string[] pValueS)
        {
            return _cInput.__mItemsAdd(pValueS);
        }
        /// <summary>
        /// * Очистка всех данных и подготовка к вводу новых данных
        /// </summary>
        public virtual void __mItemsClear()
        {
            _cInput.__mItemsClear();
        }
        /// <summary>
        /// * Загрузка данных из сущности данных
        /// </summary>
        /// <param name="pWhereExpression">Выражение выбора получаемых данных</param>
        /// <param name="pOrderExpression">Выражение сортировки получаемых данных</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public virtual bool __mItemsEssenceLoad(string pWhereExpression, string pOrderExpression)
        {
            return _cInput.__mItemsEssenceLoad(pWhereExpression, pOrderExpression);
        }
        /// <summary>
        /// * Загрузка данных из {DataTable}, со столбцами clu(идентификатор) и des(название)
        /// </summary>
        /// <param name="pDataTable">таблица</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public virtual bool __mItemsLoad(DataTable pDataTable)
        {
            return _cInput.__mItemsLoad(pDataTable);
        }
        /// <summary>
        /// * Получение индекса значения по идентификатору значения
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns></returns>
        public virtual int __mGetIndexByClue(int pClue)
        {
            return _cInput.__mGetIndexByClue(pClue);
        }

        #endregion - Процедуры

        #endregion МЕТОДЫ    

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Поле выбора данных из выпадающего списка
        /// </summary>
        protected crlComponentCombo _cInput = new crlComponentCombo();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Построение условия фильтра
        /// </summary>
        public override string __fFilterExpression_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                if (__fTableAlias.Length > 0)
                    vReturn = __fTableAlias + ".";
                vReturn = vReturn + __fFieldName + " = " + Convert.ToInt32(__fValue_).ToString();

                return vReturn;
            }
        }
        /// <summary>Поучение условия фильтра для отображения пользователю
        /// </summary>
        public override string __fFilterMessage_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                vReturn = vReturn + __fCaption_.Trim() + " = " + _cInput.__fValueText_;

                return vReturn;
            }
        }
        /// <summary>Доступность контрола
        /// </summary>
        public override bool __fEnabled_ 
        { 
            get => base.__fEnabled_;
            set
            {
                base.__fEnabled_ = value;
                _cInput.Visible = value;
                if (value == true)
                {
                    _cLabelCaption.__fLabelType_ = fLabelCaptionStatus;
                }
                else
                {
                    _cLabelCaption.__fLabelType_ = LABELTYPES.Normal;
                    if (_cInput.__fValueText_.Length > 0)
                        _cLabelValue.Text = _cInput.__fValueText_;
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>Сущность данных
        /// </summary>
        public datUnitEssence __oEssence_
        {
            get { return _cInput.__oEssence; }
            set { _cInput.__oEssence = value; }
        }
        /// <summary>Значение
        /// </summary>
        public override object __fValue_
        {
            get 
            { 
                return _cInput.__fValue_; 
            }
            set
            {
                _cInput.__fValue_ = Convert.ToInt32(value);
                //base.__fValue_ = value;
            }
        }
        /// <summary>Название значения контрола
        /// </summary>
        public override string __fValueName_
        {
            get { return _cInput.__fValueText_; }
        }
        
        #endregion = СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// * Возникает при ручном изменении данных
        /// </summary>
        public event EventHandler __eValueInteractiveChanged;

        #endregion СОБЫТИЯ
    }
}
