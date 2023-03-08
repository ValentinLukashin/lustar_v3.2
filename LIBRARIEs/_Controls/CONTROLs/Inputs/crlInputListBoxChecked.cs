using nlApplication;
using nlDataMaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputListBoxChecked'
    /// </summary>
    /// <remarks>Поле ввода множественных значений c выбором</remarks>
    public class crlInputListBoxChecked : crlInput
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

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Height = 50;

            // _cLabel
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Button;
                _cLabelCaption.__eMouseClickLeft += _cLabelCaption___eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += _cLabelCaption___eMouseClickRight;
            }
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                //_cInput.__eValueInteractiveChanged +
                _cInput.Dock = DockStyle.Fill;
                _cInput.DoubleClick += _cInput_DoubleClick;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        /// <summary>
        /// Выполняется при двойном клике по записи в поле ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput_DoubleClick(object sender, EventArgs e)
        {
            if (__eInput_DoubleClick != null)
                __eInput_DoubleClick(this, new EventArgs());
        }

        private void _cLabelCaption___eMouseClickLeft(object sender, EventArgs e)
        {
            //crlForm vForm = FindForm() as crlForm;
            //if (vForm != null & __oFormSelect != null)
            //{
            //    crlFormRecord vFormSelect = (crlFormRecord)Activator.CreateInstance(__oFormSelect);
            //    /// Восстановить vFormFilter._cAreaFilter._fFormNameParent = vForm.Name;
            //    (vFormSelect as crlFormRecord).ShowDialog();
            //    //fValue = vFormSelect.__cAreaGrid.__pRecordClue;
            //    //DataTable vDataTable = __oEssence._mRecord(fValue);
            //    //_cInput.Text = Convert.ToString(vDataTable.Rows[0]["des" + __oEssence.__fTableName]).Trim();
            //    //_cLabelValue.Text = _cInput.Text;
            //    //if (__eOnInteractivatChange != null)
            //    //    __eOnInteractivatChange(this, new EventArgs());
            //}
            //else
            //{
            //    appError vError = new appError();
            //    vError.__fErrorsType = ERRORSTYPES.Programming;
            //    vError.__mMessageBuild("Форма для построения выбора значений из справочника не определена");
            //    vError.__fProcedure = _fClassNameFull + "__cLabelCaption__eMouseClickLeft(object, EventsArgs)";
            //    crlApplication.__oErrorsHandler.__mShow(vError);
            //}
            if (__eLabelCaption_MouseClickLeft != null)
                __eLabelCaption_MouseClickLeft(this, new EventArgs());

        }

        private void _cLabelCaption___eMouseClickRight(object sender, EventArgs e)
        {
            //if (crlApplication.__oMessages.__mShow(nlApplication.MESSAGESTYPES.Question, "Удалить {0}", __fEssenceObjectName, false, "", "") == DialogResult.Yes)
            //{ 
            //}
            if (__eLabelCaption_MouseClickRight != null)
                __eLabelCaption_MouseClickRight(this, new EventArgs());
        }

        #endregion Объект

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Обновление отображаемых данных
        /// </summary>
        public virtual void __mDataRefresh()
        {
            _cInput.__mDataRefresh();
        }
        /// <summary>
        /// Перевод фокуса на поле ввода
        /// </summary>
        public override void __mInputFocus()
        {
            _cInput.Focus();
        }
        /// <summary>
        /// Добавление значения в конец списка значений компонента
        /// </summary>
        /// <param name="pValue">Добавляемое значение</param>
        /// <returns>Идентификатор добавляемой записи, положительный - из таблицы, отрицательный - назначаемый компонентом</returns>
        public virtual int __mItemAdd(string pValue)
        {
            return _cInput.__mItemAdd(pValue);
        }
        /// <summary>
        /// Добавление значения в конец списка значений компонента
        /// </summary>
        /// <param name="pValue">Добавляемое значение</param>
        /// <returns>Идентификатор добавляемой записи, положительный - из таблицы, отрицательный - назначаемый компонентом</returns>
        public virtual int __mItemAdd(appUnitItem pItem)
        {
            return _cInput.__mItemAdd(pItem);
        }
        /// <summary>
        /// Изменение имени в уже отображаемом списке
        /// </summary>
        /// <param name="pClue">Идентификатор записи в котором нужно исправить название</param>
        /// <param name="pNameNew">Новое название</param>
        public virtual void __mItemChangeName(int pClue, string pNameNew)
        {
            _cInput.__mItemChangeName(pClue, pNameNew);
        }

        public virtual void __mItemRemove()
        {
            _cInput.__mItemRemove();
        }
        /// <summary>
        /// Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public virtual bool __mItemsAdd(ArrayList pValueS)
        {
            return _cInput.__mItemsAdd(pValueS);
        }
        /// <summary>
        /// Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public virtual bool __mItemsAdd(params string[] pValueS)
        {
            return _cInput.__mItemsAdd(pValueS);
        }
        /// <summary>
        /// Очистка всех данных и подготовка к вводу новых данных
        /// </summary>
        public virtual void __mItemsClear()
        {
            _cInput.__mItemsClear();
        }
        /// <summary>
        /// Загрузка данных из сущности данных
        /// </summary>
        /// <param name="pWhereExpression">Выражение выбора получаемых данных</param>
        /// <param name="pOrderExpression">Выражение сортировки получаемых данных</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public virtual bool __mItemsEssenceLoad(string pWhereExpression, string pOrderExpression)
        {
            return _cInput.__mItemsEssenceLoad(pWhereExpression, pOrderExpression);
        }
        /// <summary>
        /// Загрузка данных из {DataTable}, со столбцами clu(идентификатор) и des(название)
        /// </summary>
        /// <param name="pDataTable">таблица</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public virtual bool __mItemsLoad(DataTable pDataTable)
        {
            return _cInput.__mItemsLoad(pDataTable);
        }
        /// <summary>
        /// Получение индекса значения по идентификатору значения
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns></returns>
        public virtual int __mGetIndexByClue(int pClue)
        {
            return _cInput.__mGetIndexByClue(pClue);
        }
        /// <summary>
        /// Возвращает идентификатор выбраной записи
        /// </summary>
        /// <returns>[int] - идентификатор выбраной записи</returns>
        public virtual int __mGetSelectedItemClue()
        {
            return _cInput.__mGetSelectedItemClue();
        }

        public string __mCheckedGet()
        { 
            return _cInput.__fCheckedValueList_;
        }
        public void __mCheckedSet(string vCheckedList)
        {
            _cInput.__fCheckedValueList_ = vCheckedList;
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region Атрибуты

        /// <summary>
        /// Название объекта сущности
        /// </summary>
        //public string __fEssenceObjectName = "";
        /// <summary>
        /// Заголовок надписи
        /// </summary>
        public string __fFormSearchCaption = "";

        #endregion Атрибуты

        #region - Внутренние
        #endregion - Внутренние

        #region - Компоненты

        /// <summary>
        /// Поле ввода множественных значений
        /// </summary>
        protected crlComponentListBoxChecked _cInput = new crlComponentListBoxChecked();

        #endregion Компоненты

        #region - Объект

        /// <summary>
        /// Форма для выбора записи
        /// </summary>
        public Type __oFormSelect;

        #endregion - Объект

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        public new List<appUnitItem> __pValueS
        {
            get
            {
                return _cInput.__fItemS;
            }
            set
            {
                _cInput.__fItemS = value;
            }
        }

        public datUnitEssence __oEssence_
        {
            get { return _cInput.__oEssence; }
            set { _cInput.__oEssence = value; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        public event EventHandler __eLabelCaption_MouseClickLeft;
        public event EventHandler __eLabelCaption_MouseClickRight;
        /// <summary>
        /// Возникает при двойном клике по поллю ввода
        /// </summary>
        public event EventHandler __eInput_DoubleClick;

        #endregion = СОБЫТИЯ
    }
}
