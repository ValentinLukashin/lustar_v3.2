using nlApplication;
using nlDataMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentListView'
    /// </summary>
    /// <remarks>Компонент для просмотра списка данных с несколькими колонками</remarks>
    public class crlComponentListView : ListView
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentListView()
        {
            _mLoad();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mLoad()
        {
            SuspendLayout();

            #region /// Настройки компонента

            AllowColumnReorder = false;
            BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
            FullRowSelect = true;
            GridLines = true;
            View = View.Details;
            Sorting = SortOrder.None;

            SelectedIndexChanged += CrlComponentListView_SelectedIndexChanged;

            #endregion Настройка компонента

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при смене выбранной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrlComponentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndices.Count > 0)
            { /// Определение индекса выбранной записи
                __fRowIndex = SelectedIndices[0];
            }
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Удаление выбранной записи
        /// </summary>
        public void __mDataDelete()
        {
            for (int vAmount = 0; vAmount < __oDataTable.Rows.Count; vAmount++)
            {
                /// Помечение выбранной записи как удаленной
                if (vAmount == __fRowIndex)
                { 
                    __oDataTable.Rows[vAmount]["LCK"] = 1;
                    DataRow vDataRowMoved = __oDataTable.NewRow();
                    vDataRowMoved.ItemArray = __oDataTable.Rows[vAmount].ItemArray;
                    __oDataTable.Rows[vAmount].Delete();
                    /// Перенос записи помеченной на удаление в конец таблицы
                    __oDataTable.Rows.Add(vDataRowMoved); 
                    __mDataRefresh(false);
                }
            }
        }
        /// <summary>
        /// Загрузка и отображение данных
        /// </summary>
        /// <param name="pLoad">[true] - загрузка из источника данных, иначе обновление</param>
        public void __mDataRefresh(bool pLoad)  
        {
            appUnitError vError = new appUnitError();
            vError.__fProcedure = _fClassNameFull + "__mDataLoad()";
            Clear();
            if (pLoad == true)
            { /// Загрузка из источника данных
                __oDataTable = __oEssence.__mGrid(__fRecordNameParent + " = " + __fRecordClueParent.ToString() 
                    + " and " + __fTableMainAlias + ".LCK = 0", __fRecordNameParent);
            }
            if (__oDataTable.Columns.Count > 0)
            { /// Добавление колонок
                Columns.Clear();
                foreach (crlListViewColumns vListViewColumn in fColumnsList)
                {
                    Columns.Add(vListViewColumn.__fCaption, vListViewColumn.__fWidth, vListViewColumn.__fAlignment);
                }
            } // Добавление колонок
            if (__oDataTable.Rows.Count > 0)
            { /// Заполнение данными
                {
                    foreach (DataRow vDataRow in __oDataTable.Rows)
                    {
                        if(Convert.ToInt32(vDataRow["LCK"]) == 0)
                        { 
                            int vNewIndex = 0;

                            for(int vAmount = 0; vAmount < fColumnsList.Count; vAmount++)
                            {
                                if(vAmount == 0)
                                    vNewIndex = Items.Add(vDataRow[fColumnsList[vAmount].__fFieldName].ToString()).Index;
                                else
                                    Items[vNewIndex].SubItems.Add(vDataRow[fColumnsList[vAmount].__fFieldName].ToString());
                            }
                        }
                    }
                }
            } // Заполнение данными
            if (vError.__fReasonS_.Count > 0)
            { /// Отображение сообщения об ошибке
                crlApplication.__oErrorsHandler.__mShow(vError);
            }
        }
        /// <summary>
        /// Сохранение данных
        /// </summary>
        public void __mDataSave()
        {
            for (int vAmount = 0; vAmount < __oDataTable.Rows.Count; vAmount++)
            {
                if (Convert.ToInt32(__oDataTable.Rows[vAmount]["LCK"]) == 1 & Convert.ToInt32(__oDataTable.Rows[vAmount]["clu" + __oEssence.__fTableName]) == 0)
                { /// Запись была добавлена и удалена
                    __oDataTable.Rows[vAmount].Delete();
                }
            }
            __oEssence.__mUpdate(__oDataTable);
        }
        /// <summary>
        /// Содание колонки компонента
        /// </summary>
        /// <param name="pFieldName">Название поля колонки</param>
        /// <param name="pCaptionText">Заголовок колонки</param>
        /// <param name="pWidth">Ширина колонки</param>
        /// <param name="pHorizontalAlignment">Выравнивание текста данных</param>
        public void __mColumnAdd(string pFieldName, string pCaptionText, int pWidth, HorizontalAlignment pHorizontalAlignment)
        {
            crlListViewColumns vListViewColumns = new crlListViewColumns();
            vListViewColumns.__fAlignment = pHorizontalAlignment;
            vListViewColumns.__fCaption = pCaptionText;
            vListViewColumns.__fFieldName = pFieldName;
            vListViewColumns.__fWidth = pWidth;
            fColumnsList.Add(vListViewColumns);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>Идентификатор записи в родительской таблице
        /// </summary>
        public int __fRecordClueParent = -1;
        /// <summary>Название поля идентификатора записи в родительской таблице с указанием псевдонима
        /// </summary>
        public string __fRecordNameParent = "";
        /// <summary>Индекс выбранной записи
        /// </summary>
        public int __fRowIndex = -1;
        /// <summary>Псевдоним главной таблицы в запросе
        /// </summary>
        public string __fTableMainAlias = "";

        #endregion = Атрибуты

        #region = Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Список отображаемых колонок
        /// </summary>
        private List<crlListViewColumns> fColumnsList = new List<crlListViewColumns>();

        #endregion Служебные

        #region = Объекты

        /// <summary>
        /// Сущность данных
        /// </summary>
        public datUnitEssence __oEssence;
        /// <summary>
        /// Курсор с данными
        /// </summary>
        public DataTable __oDataTable;

        #endregion Объекты

        #endregion ПОЛЯ

        #region = СОБЫТИЯ

        ///// <summary>Возникает при ручном изменении данных
        ///// </summary>
        //public event EventHandler __eValueInteractiveChanged;
        ///// <summary>Возникает при программном изменении данных
        ///// </summary>
        //public event EventHandler __eValueProgrammaticChanged;

        #endregion СОБЫТИЯ
    }

    /// <summary>
    /// Класс 'crlColumn'
    /// </summary>
    public class crlListViewColumns
    {
        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Название отображаемого поля
        /// </summary>
        public string __fFieldName = "";
        /// <summary>
        /// Заголовок поля
        /// </summary>
        public string __fCaption = "";
        /// <summary>
        /// Ширина колонки
        /// </summary>
        public int __fWidth = 100;
        /// <summary>
        /// Выравнивание текста данных
        /// </summary>
        public HorizontalAlignment __fAlignment = HorizontalAlignment.Left;

        #endregion Атрибуты

        #endregion ПОЛЯ
    }
}
