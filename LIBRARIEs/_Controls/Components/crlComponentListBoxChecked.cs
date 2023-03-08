﻿using nlApplication;
using nlDataMaster;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentListBoxChecked'
    /// </summary>
    /// <remarks>Компонент 'CheckedListBox'</remarks>
    public class crlComponentListBoxChecked : CheckedListBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentListBoxChecked()
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

            #region Настройки компонента

            BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);

            #endregion Настройка компонента

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

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
            DataSource = __fItemS; // Назначение списка, как источника данных
            DisplayMember = "__fValue_"; // столбец для отображения
            ValueMember = "__fClue_"; // столбец с идентификатором записи
            Refresh();

            if (__eValueProgrammaticChanged != null)
                __eValueProgrammaticChanged(this, new EventArgs());
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
            __fItemS.Add(vItem);

            return vReturn;
        }
        public int __mItemAdd(appUnitItem pItem)
        {
            int vReturn = --fAmountClue;

            __fItemS.Add(pItem);

            return vReturn;
        }
        /// <summary>
        /// Изменение имени в уже отображаемом списке
        /// </summary>
        /// <param name="pClue">Идентификатор записи в котором нужно исправить название</param>
        /// <param name="pNameNew">Новое название</param>
        public void __mItemChangeName(int pClue, string pNameNew)
        {
            for (int vAmount = 0; vAmount < __fItemS.Count; vAmount++)
            {
                if (__fItemS[vAmount].__fClue_ == pClue)
                {
                    __fItemS[vAmount].__fValue_ = pNameNew.Trim();
                    __mDataRefresh();
                    SelectedIndex = vAmount;
                    break;
                }
            }

        }
        /// <summary>
        /// Удаление выделенной записи
        /// </summary>
        public void __mItemRemove()
        {
            if (__fItemS.Count > 0)
                __fItemS.RemoveAt(SelectedIndex);
            __mDataRefresh();
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
            __fItemS.Clear();
            fAmountClue = 0;
            this.Sorted = false;
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
                    vItem.__fClue_ = (int)vDataRow["CLU"];
                    vItem.__fValue_ = (string)vDataRow["des" + __oEssence.__fTableName.Trim()];
                    __fItemS.Add(vItem);
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

            foreach (appUnitItem vItem in __fItemS)
            {
                if (vItem.__fClue_ == pClue)
                    break;
                vReturn++;
            }

            return vReturn;
        }
        /// <summary>
        /// Возвращает идентификатор выбранной записи
        /// </summary>
        /// <returns></returns>
        public virtual int __mGetSelectedItemClue()
        {
            int vReturn = -1; // Возвращаемое значение
            for (int vAmount = 0; vAmount < __fItemS.Count; vAmount++)
            {
                if (vAmount == SelectedIndex)
                {
                    vReturn = __fItemS[vAmount].__fClue_;
                    break;
                }
            }
            return vReturn;
        }
        /// <summary>
        /// Сборка выражения с параметрами и перевод выражения на язык интерфейса 
        /// </summary>
        /// <param name="pString">Текст</param>
        /// <param name="pParameters">Список дополнительных парамметров</param>
        public void __mCaptionBuilding(string pString, params object[] pParameters)
        {
            fTextWithOutTranslate = String.Format(pString, pParameters);
            Text = crlApplication.__oTunes.__mTranslate(pString, pParameters);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Список отображаемых данных
        /// </summary>
        public List<appUnitItem> __fItemS = new List<appUnitItem>();

        #endregion Атрибуты

        #region = Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Счетчик внутренних идентификаторов
        /// </summary>
        private int fAmountClue = 0;
        /// <summary>
        /// Строка заголовка без перевода
        /// </summary>
        private string fTextWithOutTranslate = "";

        #endregion Служебные

        #region = Объекты

        /// <summary>
        /// Сущность данных
        /// </summary>
        public datUnitEssence __oEssence;

        #endregion Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Сохранение результата выбора пунктов
        /// </summary>
        /// <returns>Строка вида "01010110"</returns>
        public string __fCheckedValueList_
        {
            get 
            {
                string vReturn = ""; // Возвращаемое значение

                for (int vItemIndex = 0; vItemIndex < CheckedItems.Count; vItemIndex++)
                {
                    vReturn = vReturn + (CheckedItems[vItemIndex] as nlApplication.appUnitItem).__fClue_.ToString().Trim() + ";";
                }

                return vReturn;
            }
            set
            {
                for (int vItemValue = 0; vItemValue < appTypeString.__mWordCount(value.Trim(), ";"); vItemValue++)
                {
                    int vClueChecked = Convert.ToInt32(appTypeString.__mWordNumber(value.Trim(), vItemValue, ";")); // Идентификатор помеченной записи
                    for (int vItemIndex = 0; vItemIndex < CheckedItems.Count; vItemIndex++)
                    {
                        /// Если идентификатор пары совпадает с идентификаторром в отображенных данных
                        if (vClueChecked == (CheckedItems[vItemIndex] as nlApplication.appUnitItem).__fClue_)
                        {
                            SetItemChecked(vItemIndex, true);
                        }
                    }
                }
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при ручном изменении данных
        /// </summary>
        public event EventHandler __eValueInteractiveChanged;
        /// <summary>
        /// Возникает при программном изменении данных
        /// </summary>
        public event EventHandler __eValueProgrammaticChanged;

        #endregion СОБЫТИЯ

    }
}
