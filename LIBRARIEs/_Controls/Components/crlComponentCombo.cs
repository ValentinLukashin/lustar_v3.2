using nlApplication;
using nlDataMaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentCombo'
    /// </summary>
    /// <remarks>Компонент для выбора данных из выпадающего списка</remarks>
    public class crlComponentCombo : ComboBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentCombo()
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

            if (fComboType == COMBOTYPES.Bool)
                Size = new System.Drawing.Size(50, 21);
            DropDownStyle = ComboBoxStyle.DropDownList;
            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при закрытии выпадающего списка
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            if (__eValueInteractiveChanged != null)
                __eValueInteractiveChanged(this, new EventArgs());

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Обновление отображаемых данных
        /// </summary>
        public void __mDataRefresh()
        {
            DataSource = null;
            Items.Clear(); // Очистка отображаемых данных
            DataSource = fItemS; // Назначение списка, как источника данных
            DisplayMember = "__fValue_"; // Cтолбец для отображения
            ValueMember = "__fClue_"; // Cтолбец с идентификатором записи
            Refresh();
            mWidthCalculate(); // Расчет размера компонента

            if (__eValueProgrammaticChanged != null)
                __eValueProgrammaticChanged(this, new EventArgs());

            return;
        }
        /// <summary>
        /// Добавление значения в конец списка значений компонента
        /// </summary>
        /// <param name="pValue">Добавляемое значение</param>
        /// <returns>Идентификатор добавляемой записи, положительный - из таблицы, отрицательный - назначаемый компонентом</returns>
        public int __mItemAdd(string pValue)
        {
            int vReturn = --fAmountClue;

            appUnitItem vItem = new appUnitItem();
            vItem.__fClue_ = vReturn;
            vItem.__fValue_ = pValue;
            fItemS.Add(vItem);

            return vReturn;
        }
        /// <summary>
        /// Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public bool __mItemsAdd(ArrayList pValueS)
        {
            bool vReturn = false; // Возвращаемое значение

            foreach (string vValue in pValueS)
            {
                __mItemAdd(vValue);
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public bool __mItemsAdd(params string[] pValueS)
        {
            bool vReturn = false; // Возвращаемое значение

            foreach (string vValue in pValueS)
            {
                __mItemAdd(vValue);
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Очистка всех данных и подготовка к вводу новых данных
        /// </summary>
        public void __mItemsClear()
        {
            DataSource = null;
            Items.Clear();
            fItemS.Clear();
            fAmountClue = 0;
            this.Sorted = false;

            return;
        }
        /// <summary>
        /// Загрузка данных из сущности данных
        /// </summary>
        /// <param name="pWhereExpression">Выражение выбора получаемых данных</param>
        /// <param name="pOrderExpression">Выражение сортировки получаемых данных</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mItemsEssenceLoad(string pWhereExpression, string pOrderExpression)
        {
            bool vReturn = false; // Возвращаемое значение

            if (__oEssence != null)
            {
                __mItemsLoad(__oEssence._mCombo(pWhereExpression, pOrderExpression));
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Загрузка данных из {DataTable}, со столбцами clu(идентификатор) и des(название)
        /// </summary>
        /// <param name="pDataTable">таблица</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mItemsLoad(DataTable pDataTable)
        {
            bool vReturn = false; // Возвращаемое значение

            if (pDataTable != null)
            {
                foreach (DataRow vDataRow in pDataTable.Rows)
                {
                    appUnitItem vItem = new appUnitItem();
                    vItem.__fClue_ = (int)vDataRow["clu"];
                    vItem.__fValue_ = (string)vDataRow["des"];
                    fItemS.Add(vItem);
                }

                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Получение индекса значения по идентификатору значения
        /// </summary>
        /// <param name="pClue"></param>
        /// <returns></returns>
        public int __mGetIndexByClue(int pClue)
        {
            int vReturn = 0;

            foreach (var vItem in fItemS)
            {
                if (vItem.__fClue_ == pClue)
                    break;
                vReturn++;
            }

            return vReturn;
        }
        /// <summary>
        /// Вычисление и установка размера объекта по существующим данным
        /// </summary>
        private void mWidthCalculate()
        {
            int vWidth = 20; // Устанавливаемая ширина объекта
            int vWidthFont = 0; // Ширина установленного шрифта

            for (int vAmount = 0; vAmount < fItemS.Count; vAmount++)
            {
                int vSymbolsCount = fItemS[vAmount].__fValue_.ToString().Length;
                if (vSymbolsCount <= 3)
                    vSymbolsCount = vSymbolsCount + 1;
                vWidthFont = Convert.ToInt32(crlTypeFont.__mMeasureText(vSymbolsCount, this.Font).Width);
                if (vWidthFont > vWidth)
                    vWidth = vWidthFont + SystemInformation.VerticalScrollBarWidth + crlInterface.__fIntervalHorizontal;
            }
            Width = vWidth + 10;

            return;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ    

        #region = ПОЛЯ

        #region = Атрибуты


        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Атрибуты

        #region = Внутренние

        /// <summary>
        /// Сущность данных
        /// </summary>
        public datUnitEssence __oEssence;

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Счетчик внутренних идентификаторов
        /// </summary>
        private int fAmountClue = 0;
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        private FILLTYPES fFillType = FILLTYPES.None;
        /// <summary>
        /// Вид заполнения выпадающего списка данными
        /// </summary>
        private COMBOTYPES fComboType = COMBOTYPES.Items;
        /// <summary>
        /// Список отображаемых данных
        /// </summary>
        private List<appUnitItem> fItemS = new List<appUnitItem>();

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Вид заполнения выпадающего списка данными
        /// </summary>
        public COMBOTYPES __fComboType_
        {
            get { return fComboType; }
            set
            {
                __mItemsClear(); /// Очистка и подготовка компонента к вводу данных

                fComboType = value;
                if (fComboType == COMBOTYPES.Bool)
                {
                    appUnitItem vItem = new appUnitItem();
                    vItem.__fClue_ = 0;
                    vItem.__fValue_ = crlApplication.__oTunes.__mTranslate("Нет");
                    fItemS.Add(vItem);
                    vItem = new appUnitItem();
                    vItem.__fClue_ = 1;
                    vItem.__fValue_ = crlApplication.__oTunes.__mTranslate("Да");
                    fItemS.Add(vItem);
                    __mDataRefresh();
                }
            }
        }
        /// <summary>
        /// Список отображаемых данных (только чтение)
        /// </summary>
        public List<appUnitItem> __fItemList_
        {
            get { return fItemS; }
        }
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public FILLTYPES __fFillType_
        {
            get { return fFillType; }
            set
            {
                fFillType = value;
                if (fFillType == FILLTYPES.None)
                    BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
                else
                    BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBackNecessarily);
            }
        }
        /// <summary>
        /// Значение компонента
        /// </summary>
        public object __fValue_
        {
            get
            {
                object vReturn = null; // Возвращаемое значение

                switch (__fComboType_)
                {
                    case COMBOTYPES.Bool: /// Логическое значение
                        vReturn = SelectedIndex;
                        break;
                    default:
                        if (SelectedIndex - 1 < fItemS.Count & fItemS.Count > 0)
                            vReturn = fItemS[SelectedIndex].__fClue_;
                        break;
                }

                return vReturn;
            }
            set
            {
                switch (__fComboType_)
                {
                    case COMBOTYPES.Bool: /// Логическое значение
                        SelectedIndex = Convert.ToInt32(value);
                        break;
                    default:
                        if (Items.Count > __mGetIndexByClue(Convert.ToInt32(value)))
                        {
                            SelectedIndex = __mGetIndexByClue(Convert.ToInt32(value));
                            Refresh();
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Отображаемое значение компонента
        /// </summary>
        public string __fValueText_
        {
            get
            {
                if (SelectedIndex >= 0)
                    return fItemS[SelectedIndex].__fValue_.ToString();
                else
                    return "";
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>Возникает при ручном изменении данных
        /// </summary>
        public event EventHandler __eValueInteractiveChanged;
        /// <summary>Возникает при программном изменении данных
        /// </summary>
        public event EventHandler __eValueProgrammaticChanged;

        #endregion СОБЫТИЯ
    }
}
