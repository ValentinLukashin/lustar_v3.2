using nlApplication;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.IO;
using System;

namespace nlDataMaster
{
    /// <summary>
    /// Класс 'datUnitEssence'
    /// </summary>
    /// <remarks>Элемент - сущность данных. Всегда наследуется</remarks>
    public abstract class datUnitEssence
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        /// <param name="pDeleteType">Порядок удаления записей из таблиц базы данных</param>
        public datUnitEssence(string pDataSourceAlias, DELETETYPES pDeleteType)
        {
            __fDataSourceAlias = pDataSourceAlias;
            __fDeleteType = pDeleteType;

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
            Type vType = this.GetType();
            _fClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        #region Access

        /// <summary>Получение прав роли и пользователя для объекта
        /// </summary>
        /// <param name="pObjectGuid">Уникальный идентификатор объекта</param>
        /// <param name="pUserRoleClue">Идентификатор роли пользователя</param>
        /// <param name="pUserClue">Идентификатор пользователя</param>
        /// <param name="pUserRoleAccess">Право роли пользователя</param>
        /// <param name="pUserAccess">Право пользователя</param>
        /// <returns>[true] - метод выполнен без ошибок, иначе - [false]</returns>
        public virtual bool _mAccess(string pObjectGuid, int pUserRoleClue, int pUserClue, ref bool pUserRoleAccess, ref bool pUserAccess)
        {
            bool vReturn = true; // Возвращаемое значение

            try
            {
                if (Convert.ToBoolean(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Select" +
                                                                                        " Case" +
                                                                                        " When Exists(Select Top 1 1 From UsrRht where lnkUsrRol = " + pUserRoleClue.ToString() + " and Obj = '" + pObjectGuid + "')" +
                                                                                        " THEN 1" +
                                                                                        " ELSE 0" +
                                                                                        " END")) == false)
                {
                    datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand("Insert Into UsrRht (lnkUsrRol, Obj, Rht) Values (" + pUserRoleClue.ToString() + ", '" + pObjectGuid + "', 1)");
                } // Проверка существования записи для роли пользователя
                if (Convert.ToBoolean(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Select" +
                                                                                        " Case" +
                                                                                        " When Exists(Select Top 1 1 From UsrRht where lnkUsr = " + pUserClue.ToString() + " and Obj = '" + pObjectGuid + "')" +
                                                                                        " THEN 1" +
                                                                                        " ELSE 0" +
                                                                                        " END")) == false)
                {
                    datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand("Insert Into UsrRht (lnkUsr, Obj, Rht) Values (" + pUserClue.ToString() + ", '" + pObjectGuid + "', 1)");
                } // Проверка существования записи для пользователя
                pUserRoleAccess = Convert.ToBoolean(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Select Rht From UsrRht Where lnkUsrRol = " + pUserRoleClue.ToString() + " and Obj = '" + pObjectGuid + "'"));
                pUserAccess = Convert.ToBoolean(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Select Rht From UsrRht Where lnkUsr = " + pUserClue.ToString() + " and Obj = '" + pObjectGuid + "'"));
            }
            catch { vReturn = false; }

            return vReturn;
        }

        #endregion Access

        #region Combo

        /// <summary>
        /// Получение данных для заполнения 'ComboBox'
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pExpressionWhere">Условие выбора данных</param>
        /// <param name="pExpressionOrder">Условие сортировки даных</param>
        /// <returns>{DataTable} заполненный данными</returns>
        public virtual DataTable _mCombo(string pExpressionWhere, string pExpressionOrder)
        {
            string vQuery = "Select CLU, RTrim(LTrim(des" + __fTableName + ")) as des" + " From " + __fTableName;
            if (pExpressionWhere.Length > 0)
                vQuery = vQuery + " Where " + pExpressionWhere;
            if (pExpressionOrder.Length > 0)
                vQuery = vQuery + " Order By " + pExpressionOrder;

            return datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlQuery(vQuery);
        }

        #endregion Combo

        /// <summary>
        /// Форимровние ошибки при сохранении данных
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pProcedure"></param>
        public virtual void __mError(string pMessage, string pProcedure)
        {
            if (_fTriggerErrorsDescriptions.Count > 0)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Data;
                vError.__fProcedure = pProcedure;
                vError.__mMessageBuild(pMessage);
                foreach (string vReason in _fTriggerErrorsDescriptions)
                {
                    vError.__mReasonAdd(vReason);
                }
                datApplication.__oErrorsHandler.__mShow(vError);
            }
        }

        #region Grid

        /// <summary>
        /// Получение табличных данных
        /// </summary>
        /// <param name="pExpressionWhere">Условие выбора данных</param>
        /// <param name="pExpressionOrder">Условие сортировки даных</param>
        /// <returns>{DataTable} заполненный данными</returns>
        public virtual DataTable __mGrid(string pExpressionWhere, string pExpressionOrder)
        {
            string vQuery = "Select * From " + __fTableName + " as " + __fTablePrefix;
            if (pExpressionWhere.Length > 0)
                vQuery = vQuery + " Where " + pExpressionWhere;
            if (pExpressionOrder.Length > 0)
                vQuery = vQuery + " Order By " + pExpressionOrder;

            return datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlQuery(vQuery);
        }

        #endregion Grid

        #region Record

        /// <summary>
        /// Получение курсора с записью указанной идентификатором
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns></returns>
        public virtual DataTable _mRecord(int pClue)
        {
            return datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTableRow(__fTableName, pClue);
        }
        /// <summary>
        /// Установка текущего времени в качестве последнего времени изменения записи
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        public void _mRecordChangesNow(int pClue)
        {
            string vCommand = "Update " + __fTableName + " Set CHG = GetDate() Where CLU = " + pClue.ToString();
            datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommand);
        }
        /// <summary>
        /// Получение истории изменений
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns>{DataTable}</returns>
        public DataTable _mRecordChanges(int pClue)
        {
            /// Команда получения текущего состояния записи
            string vQuery = "Select * From " + __fTableName + " Where CLU = " + pClue.ToString();
            DataTable vDataTableReturn = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlQuery(vQuery);
            vDataTableReturn.Columns.Add("lnkDatLck", typeof(Int32)); // Добавление поля 'Идентификатор блокировки'
            vDataTableReturn.Columns.Add("desUsrChg", typeof(String)); // Добавление поля сортировки
            vDataTableReturn.Columns.Add("dtmDatLck", typeof(DateTime)); // Добавление поля сортировки

            /// Команда получения зафиксированных изменений
            vQuery = "Select DC.lnkDatLck"
+ ", DC.Fld"
+ ", DC.Val"
+ ", U.desUsr as desUsrChg"
+ ", DL.dtmDatLck_On as dtmDatLck"
+ " From DatLck as DL"
+ " Left Join DatChg as DC On DC.lnkDatLck = DL.CLU "
+ " Left Join Usr as U On U.CLU = DL.lnkUsr"
+ " Where desDatLck = '" + __fTableName + "'"
+ "  and lnkRid = " + pClue.ToString()
+ "  and lnkDatLck > 0"
+ " Order By DL.CLU";

            /// Запрос зафиксированных изменений
            DataTable vDataTableChanges = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlQuery(vQuery);

            int vDataLockLast = -1; // Предыдущий идентификатор блокировки, для исправления нескольких полей в таблице
            DataRow vDataRowNew = null;

            /// Перебор записей с зафиксированными изменениями
            foreach (DataRow vDataRowChanges in vDataTableChanges.Rows)
            {
                int vDataLock = Convert.ToInt32(vDataRowChanges["lnkDatLck"]);
                string vUserAlias = Convert.ToString(vDataRowChanges["desUsrChg"]);
                DateTime vDataLockCHG = Convert.ToDateTime(vDataRowChanges["dtmDatLck"]);

                /// Если новый идентификатор блокировки - создаем новую запись в результатаной таблице
                if (vDataLockLast != vDataLock)
                {
                    if (vDataRowNew != null)
                        vDataTableReturn.Rows.Add(vDataRowNew);
                    vDataRowNew = vDataTableReturn.NewRow(); // Создание новой записи в результатной таблице 
                    vDataRowNew.ItemArray = vDataTableReturn.Rows[0].ItemArray.Clone() as object[]; // Копирование в новую запись текущих данных
                    vDataRowNew["lnkDatLck"] = vDataLock;
                    vDataRowNew["desUsrChg"] = vUserAlias;
                    vDataRowNew["dtmDatLck"] = vDataLockCHG;
                    //vDataRowNew["CHG"] = vDataLockCHG;
                }
                switch (vDataRowNew[vDataRowChanges["Fld"].ToString()].GetType().Name)
                {
                    case "Boolean":
                        //if (vDataRowChanges["Val"].ToString() == "0")
                        //    vDataRowNew[vDataRowChanges["Fld"].ToString()] = false;
                        //else
                        //    vDataRowNew[vDataRowChanges["Fld"].ToString()] = true;
                        //break;
                        if (vDataRowChanges["Val"].ToString() == "False")
                            vDataRowNew[vDataRowChanges["Fld"].ToString()] = false;
                        else
                            vDataRowNew[vDataRowChanges["Fld"].ToString()] = true;
                        break;
                    case "DateTime":
                        vDataRowNew[vDataRowChanges["Fld"].ToString()] = Convert.ToDateTime(vDataRowChanges["Val"]); // Исправление значений зафиксированными изменениями
                        break;
                    case "Decimal":
                        vDataRowNew[vDataRowChanges["Fld"].ToString()] = Convert.ToDecimal(vDataRowChanges["Val"].ToString().Replace('.', ',')); // Исправление значений зафиксированными изменениями
                        break;
                    default:
                        vDataRowNew[vDataRowChanges["Fld"].ToString()] = vDataRowChanges["Val"]; // Исправление значений зафиксированными изменениями
                        break;
                }
                vDataLockLast = vDataLock;
            }
            if (vDataRowNew != null)
                vDataTableReturn.Rows.Add(vDataRowNew);

            return vDataTableReturn;
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns>[true] - Запись удалена без ошибок, иначе - [false]</returns>
        public bool _mRecordDelete(int pClue)
        {
            bool vReturn = true; // Возвращамое значение
            string vCommand = "Delete From " + __fTableName + " Where CLU = " + pClue.ToString();
            int vRecordCount = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommand);
            if (vRecordCount <= 0)
                vReturn = false;

            return vReturn;
        }
        /// <summary>
        /// Получение пустой записи для указанной таблицы
        /// </summary>
        /// <param name="pDataTable">Таблица в которую добавляется пустая запись</param>
        /// <returns>{DataRow} заполненная значениями по умолчанию</returns>
        public virtual DataRow __mRecordNew(DataTable pDataTable)
        {
            return null;
        }

        #endregion Record

        #region Tree

        /// <summary>
        /// Получение древовидных данных
        /// </summary>
        /// <param name="pExpressionWhere">Условие выбора данных</param>
        /// <returns>{DataTable} заполненный данными</returns>
        public virtual DataTable _mTree(string pExpressionWhere)
        {
            string vQuery = "Select T.CLU" +
                                 ", T.des" + __fTableName +
                                 ", T.lnk" + __fTableName +
                                 ", T.TreSor" +
                            " From " + __fTableName + " As T";
            if (pExpressionWhere.Length > 0)
                vQuery = vQuery + " Where " + pExpressionWhere;
            vQuery = vQuery + " Order By T.Lnk" + __fTableName + ", T.TreSor, T.des" + __fTableName;

            DataTable vDataTable = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlQuery(vQuery);
            return vDataTable;
        }
        /// <summary>
        /// Получение идентификатора записи для древовидной структуры по уровню вложенности и индексу сортировки
        /// </summary>
        /// <param name="pLevel">Уровень вложенности</param>
        /// <param name="pIndex">Индекс сортровки</param>
        /// <returns></returns>
        public virtual int _mTreeClueByLevelIndex(int pLevel, int pIndex)
        {
            return -1;
        }
        /// <summary>
        /// Получение полного имени из дерева данных
        /// </summary>
        /// <param name="pClue">Идентификатор записи для которой нужно получить полное имя</param>
        public virtual string __mTreeFullName(int pClue)
        {
            fTreeFullName = "";
            mTreeFullName(pClue);
            return fTreeFullName;
        }
        /// <summary>
        /// Получение данных для заполнения 'appAreaPath'
        /// </summary>
        /// <param name="pClue">Идентификатор родительской записи</param>
        /// <returns></returns>
        public virtual DataTable __mTreeData(int pClueParent)
        {
            string vQuery = ""; // Тело запроса

            if (pClueParent == 0)
            {
                vQuery = " Select t.CLU" +
                         ", t.des" + __fTableName +
                         ", t.TreSor" +
                         ", Count(ct.lnk" + __fTableName + ") as Cou" +
                         " From " + __fTableName + " as t" +
                         " Left Join " + __fTableName + " as ct On ct.CLU = t.clu" + __fTableName +
                         " Where t.lnk" + __fTableName + " = " + pClueParent.ToString() +
                         " Group by t.CLU, t.des" + __fTableName + ", t.TreSor" +
                         " Order by TreSor Desc, des" + __fTableName;
            } /// Выбор данных для верхнего уровня
            else
            {
                vQuery = "Select " + pClueParent.ToString() + " as CLU" +
                         ", '..' as des" + __fTableName +
                         ", 0 as TreSor" +
                         ", 0 as Cou" +
                         " From " + __fTableName + " as t" +
                         " Union" +
                         " Select * " +
                         " From (" +
                         " Select t.CLU" +
                         ", t.des" + __fTableName +
                         ", t.TreSor" +
                         ", Count(ct.lnk" + __fTableName + ") as Cou" +
                         " From " + __fTableName + " as t" +
                         " Left Join " + __fTableName + " as ct On ct.CLU" + " = t.CLU" +
                         " Where t.lnk" + __fTableName + " = " + pClueParent.ToString() +
                         " Group by t.CLU" + ", t.des" + __fTableName + ", t.TreSor" +
                         " ) as A";
            } /// Выбор данных для вложенного уровня
            return datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlQuery(vQuery);
        }

        #endregion Tree

        #region Блокировки

        /// <summary>
        /// Выполнение блокировки записи 
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <remarks>Если [pRecord] = 0, то блокируется вся таблица</remarks>
        /// <returns>Идентификатор блокировки</returns>
        public int _mLockOn(int pClue)
        {
            return datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mLockOn(__fTableName, pClue);

            //__fLockClue = 0; // Идентификатор блокировки

            ///// Блокировка записей разрешена
            //if (__fLockUsed == true)
            //{
            //    int vLockCount = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTableRowsCountWhere("DatLck"
            //        , "desDatLck = '" + __fTableName + "'"
            //        + " and lnkRid = " + pClue.ToString()
            //        + " and dtmDatLckOff = CONVERT(datetime,'01.01.1900')"); // Количество блокировок указанной записи

            //    /// Запись не заблокирована
            //    if (vLockCount == 0)
            //    {
            //        /// Выполнение блокировки
            //        if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand("Insert Into DatLck(desDatLck, dtmDatLck_On, lnkRid, lnkUsr) " +
            //                            "Values('" + __fTableName + "'" +
            //                            ", Cast('" + appTypeDateTime.__mMsSqlDateTimeToString(DateTime.Now).ToString() + " ' as DateTime)" +
            //                            ", " + pClue.ToString() +
            //                            ", " + datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__fUserClue.ToString() + ")") > 0)
            //            /// Получение идентификатора выполненной блокировки
            //            __fLockClue = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mClueLastInserted("DatLck");
            //    }
            //    /// Запись заблокирована 
            //    else
            //    {
            //        int vUserClue = Convert.ToInt32(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("DatLck"
            //            , "lnkUsr"
            //            , "desDatLck = '" + __fTableName + "'"
            //            + " and lnkRid = " + pClue.ToString()
            //            + " and dtmDatLckOff = CONVERT(datetime,'01.01.1900')")); // Идентификатор пользователя забокировавшего запись
            //        string vUserName = Convert.ToString(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Usr"
            //            , "desUsr"
            //            , "CLU = " + vUserClue.ToString())).Trim(); // Псевдоним пользователя заблокировашего запись
            //        DateTime vLockTime = Convert.ToDateTime(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("DatLck"
            //            , "dtmDatLck_On"
            //            , "desDatLck = '" + __fTableName + "'"
            //            + " and lnkRid = " + pClue.ToString()
            //            + " and dtmDatLckOff = CONVERT(datetime,'01.01.1900')")); // Время создания блокировки

            //        appError vError = new appError();
            //        vError.__fErrorsType = ERRORSTYPES.Data;
            //        vError.__fProcedure = _fClassNameFull + "_LockOn(int)";
            //        vError.__fHelpFileName = "errors.chm";
            //        vError.__fHelpTopic = "mnk_2_2.htm";
            //        vError.__mMessageBuild("Запись заблокирована");
            //        vError.__mPropertyAdd("Пользователь заблокировавший запись: {0}", vUserName);
            //        vError.__mPropertyAdd("Время блокировки: {0}", vLockTime.ToString());

            //        datApplication.__oErrorsHandler.__mShow(vError);

            //        return -1;
            //    }
            //}

            //return __fLockClue;
        }
        /// <summary>
        /// Снятие блокировкию
        /// </summary>
        /// <param name="pLockClue">Идентификатор блокировки.</param>
        /// <returns>[true] - блокировка снята, иначе - [false].</returns>
        public bool _mLockOff(int pLockClue)
        {
            return datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mLockOff(pLockClue);


            //bool vReturn = false; // Возвращаемое значение

            ///// Блокировка записей разрешена
            //if (__fLockUsed == true)
            //{
            //    string vCommand = "Update DatLck Set";
            //    if (pInsertClue > -1)
            //        vCommand = vCommand + " lnkRid = " + pInsertClue.ToString() + ",";
            //    vCommand = vCommand + " dtmDatLckOff = GetDate() Where CLU = " + pLockClue.ToString();
            //    if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommand) > 0)
            //        vReturn = true;
            //}

            //return vReturn;
        }

        #endregion Блокировки

        #region Таблица

        /// <summary>
        /// Формирование отклонений в таблице начиная с указанного времени
        /// </summary>
        /// <param name="pDateTimeChanges">Время с которого нужно формировать отклонения</param>
        /// <param name="pFileName">Название файла для размещения отклонений</param>
        /// <returns></returns>
        public virtual bool __mDistributionsExport(DateTime pDateTimeChanges, string pFileName)
        {
            bool vReturn = false; // Возвращаемое значение
            string vFilePath = Path.Combine(datApplication.__oPathes.__fFolderDataForSending_, ""); // Путь к создаваемому файлу
            string vQuery = "Select * From " + __fTableName + " Where CHG > " + appTypeDateTime.__mMsSqlDateTimeToString(pDateTimeChanges); // Запрос для получения измененных данных
            /// Получение изменений после указанного времени
            DataTable vDataTable = datApplication.__oData.__mDataSourceGet().__mSqlQuery(vQuery);
            vDataTable.WriteXml(vFilePath);
            if (File.Exists(vFilePath) == true)
            {
                vReturn = true;
            }
            return vReturn;
        }
        ///// <summary>Построение модели таблицы
        ///// </summary>
        ///// <returns>[true] - модель создана, иначе - [false]</returns>
        //public virtual bool _mModelBuild()
        //{
        //    return false;
        //}
        /// <summary>
        /// Получение описания поля таблицы из модели
        /// </summary>
        /// <param name="pFieldName">Название таблицы</param>
        /// <returns>Описание таблицы</returns>
        public string _mModelFieldDescription(string pFieldName)
        {
            string vReturn = ""; // Возвращаемое значение
            foreach (datUnitModelField vField in __fFieldS)
            {
                if (vField.__fName.Trim().ToLower() == pFieldName.Trim().ToLower())
                {
                    vReturn = vField.__fDescription.Trim();
                    break;
                }
            }
            return vReturn;
        }
        /// <summary>
        /// Обновление данных в источнике данных
        /// </summary>
        /// <param name="pDataTable">{DataTable}</param>
        /// <returns>[true] - обновление выполнено, иначе - [false]</returns>
        public bool __mUpdate(DataTable pDataTable)
        {
            _fTableChanges = new DataTable("ChangesValue"); // Таблица с выполненными корректировками
            _fTableChanges.Columns.Add("lnkDatLck", typeof(int)); // Добавление идентификатора блокировки
            _fTableChanges.Columns.Add("Fld", typeof(string)); // Добавление названия поля
            _fTableChanges.Columns.Add("Val", typeof(string)); // Добавление нового значения

            bool vReturn = true; // Возвращаемый результат
            int vLockClue = 0; // Идентификатор блокировки
            DateTime vTime = DateTime.Now; // Время выполнения операции

            /// Открытие транзакции
            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTransactionOn() == true)
            {
                /// Перебор записей в курсоре
                for (int vRowNumber = 0; vRowNumber < pDataTable.Rows.Count; vRowNumber++)
                {
                    DataRow vDataRow = pDataTable.Rows[vRowNumber]; // Выбранная в курсоре запись
                    vLockClue = _mLockOn(Convert.ToInt32(vDataRow["CLU"])); // Идентификатор заблокированной записи
                    int vLastInsertedClue = -1; // Идентификатор созданной записи

                    __fLockClue = vLockClue;

                    /// Запись вставлена
                    if (vDataRow.RowState == DataRowState.Added)
                    {
                        /// Выполнение триггера вставки
                        if (__mTriggerInsert(ref vDataRow) == false)
                        {
                            _mLockOff(vLockClue);
                            __mError("Не удалось создать запись", "__mUpdate(DataTable)");
                            return false;
                        }
                        /// Определение времени изменения записи
                        if (pDataTable.Columns.Contains("CHG") == true)
                        {
                            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__fDateTimeStore == DATETIMESTORE.DateTime)
                                vDataRow["CHG"] = DateTime.Now;
                            else
                                vDataRow["CHG"] = DateTime.Now.Ticks;
                        }

                        string vCommandInsert = "Insert Into " + __fTableName + "("; // Команда вставки

                        /// Добавление названий полей в команду вставки
                        foreach (DataColumn vColumn in pDataTable.Columns)
                        {
                            /// Поле отсутствует в основной таблице запроса, пропускается
                            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTableColumnExists(__fTableName, vColumn.ColumnName) == false)
                            {
                                continue;
                            }
                            /// Поле не является идентификатором записи в таблице, добавляется в команду
                            if (vColumn.ColumnName.ToUpper() != "CLU")
                                vCommandInsert = vCommandInsert + vColumn.ColumnName + ",";
                        }

                        vCommandInsert = vCommandInsert.Substring(0, vCommandInsert.Length - 1) + ") Values(";

                        /// Добавление значений полей в команду вставки
                        foreach (DataColumn vColumn in pDataTable.Columns)
                        {
                            /// Поле отсутствует в основной таблице запроса, пропускается
                            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTableColumnExists(__fTableName, vColumn.ColumnName) == false)
                            {
                                continue;
                            }
                            /// Поле не является идентификатором записи в таблице, добавляется в команду
                            if (vColumn.ColumnName.ToUpper() != "CLU")
                            {
                                string vValueNew = "";

                                if (vColumn.DataType.Name.ToUpper() == "BOOLEAN")
                                {
                                    vValueNew = "0";
                                    if (Convert.ToBoolean(vDataRow[vColumn]) == true)
                                        vValueNew = "1";
                                    vCommandInsert = vCommandInsert + vValueNew + ",";
                                }
                                if (vColumn.DataType.Name.ToUpper() == "DATE")
                                {
                                    vValueNew = appTypeDateTime.__mMsSqlDateToString(Convert.ToDateTime(vDataRow[vColumn]));
                                    vCommandInsert = vCommandInsert + "Cast('" + vValueNew + "' as Date),";
                                }
                                if (vColumn.DataType.Name.ToUpper() == "DATETIME")
                                {
                                    vValueNew = appTypeDateTime.__mMsSqlDateTimeToString(Convert.ToDateTime(vDataRow[vColumn])).ToString().Trim();
                                    vCommandInsert = vCommandInsert + "Cast('" + vValueNew + "' as DateTime),";
                                    vValueNew = Convert.ToDateTime(vDataRow[vColumn]).ToString();
                                }
                                if (vColumn.DataType.Name.ToUpper() == "DECIMAL")
                                {
                                    vValueNew = vDataRow[vColumn].ToString().Trim().Replace(',', '.');
                                    vCommandInsert = vCommandInsert + vValueNew + ",";
                                }
                                if (vColumn.DataType.Name.ToUpper() == "GUID")
                                {
                                    vValueNew = vDataRow[vColumn].ToString().Trim().Replace(',', '.');
                                    vCommandInsert = vCommandInsert + "'" + vValueNew.ToString() + "',";
                                }
                                if (vColumn.DataType.Name.ToUpper() == "INT16" | vColumn.DataType.Name.ToUpper() == "INT32" | vColumn.DataType.Name.ToUpper() == "INT64")
                                {
                                    vValueNew = vDataRow[vColumn].ToString().Trim();
                                    vCommandInsert = vCommandInsert + vValueNew + ",";
                                }
                                if (vColumn.DataType.Name.ToUpper() == "STRING")
                                {
                                    vValueNew = vDataRow[vColumn].ToString().Trim();
                                    vCommandInsert = vCommandInsert + "'" + vValueNew + "',";
                                }
                                /// Заполнение таблицы с изменениями DatChg
                                _fTableChanges.Rows.Add(__fLockClue, vColumn.ColumnName, vValueNew);
                            }
                        }

                        vCommandInsert = vCommandInsert.Substring(0, vCommandInsert.Length - 1) + ")";

                        /// Выполнение вставки записи в источник данных
                        if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommandInsert) <= 0)
                            vReturn = false; //Ошибка при вставке записи
                        else
                        {
                            vLastInsertedClue = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mClueLastInserted(__fTableName);
                            __fLastInsertedKey = vLastInsertedClue;
                            vReturn = true & __mOtherTableChangeOnInsert(vLastInsertedClue);
                        }
                    }

                    /// Запись изменена
                    if (vDataRow.RowState == DataRowState.Modified)
                    {
                        /// Выполнение триггера обновления
                        if (__mTriggerUpdate(ref vDataRow) == false)
                        {
                            _mLockOff(vLockClue);
                            __mError("Не удалось изменить запись", "__mUpdate(DataTable)");
                            return false;
                        }
                        /// Определение времени изменения записи
                        if (pDataTable.Columns.Contains("CHG") == true)
                        {
                            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__fDateTimeStore == DATETIMESTORE.DateTime)
                                vDataRow["CHG"] = DateTime.Now;
                            else
                                vDataRow["CHG"] = DateTime.Now.Ticks;
                        }

                        string vCommandUpdate = "Update " + __fTableName + " Set "; // Команда обновления

                        int vRecordClue = Convert.ToInt32(vDataRow["CLU"].ToString()); // Идентификатор записи
                        string vClueString = Convert.ToString(vRecordClue); // Идентификатор записи

                        /// Добавление обновляемых полей
                        foreach (DataColumn vColumn in pDataTable.Columns)
                        {
                            string vValueOld = vDataRow[vColumn, DataRowVersion.Original].ToString(); // Значение до корректировки
                            string vValueNew = vDataRow[vColumn, DataRowVersion.Current].ToString(); // Значение после корректировки
                            /// Обнаружено изменение значения поля
                            if (vValueOld != vValueNew)
                            {
                                if (vColumn.ColumnName.Substring(0, 3) != "CHG")
                                    _fTableChanges.Rows.Add(__fLockClue, vColumn.ColumnName, vValueNew); /// Заполнение таблицы с изменениями
                            }
                            else
                                continue; // Изменений нет, поле исключается из команды

                            /// Поле отсутствует в основной таблице запроса, пропускается
                            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTableColumnExists(__fTableName, vColumn.ColumnName) == false)
                            {
                                continue;
                            }

                            /// Поле не является идентификатором записи в таблице, добавляется в команду
                            if (vColumn.ColumnName.ToUpper() != "CLU")
                            {
                                if (vColumn.DataType.Name.ToUpper() == "BOOLEAN")
                                {
                                    string vValue = "0";
                                    if (Convert.ToBoolean(vDataRow[vColumn]) == true)
                                        vValue = "1";
                                    vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = " + vValue + ",";
                                    continue;
                                }
                                if (vColumn.DataType.Name.ToUpper() == "DATETIME")
                                {
                                    vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = Cast('" + appTypeDateTime.__mMsSqlDateTimeToString(Convert.ToDateTime(vDataRow[vColumn])) + "' as DateTime),";
                                    continue;
                                }
                                if (vColumn.DataType.Name.ToUpper() == "DECIMAL")
                                {
                                    vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = " + vDataRow[vColumn].ToString().Trim().Replace(',', '.') + ",";
                                    continue;
                                }
                                if (vColumn.DataType.Name.ToUpper() == "GUID")
                                {
                                    if (vDataRow[vColumn].ToString().Trim().Length > 0)
                                        vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = '" + vDataRow[vColumn].ToString().Trim() + "',";
                                    else
                                        vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = null,";
                                    continue;
                                }
                                if (vColumn.DataType.Name.ToUpper() == "STRING")
                                {
                                    vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = '" + vDataRow[vColumn].ToString().Trim() + "',";
                                    continue;
                                }
                                if (vColumn.DataType.Name.ToUpper() == "SINGLE")
                                {
                                    vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = " + vDataRow[vColumn].ToString().Trim().Replace(',', '.') + ",";
                                    continue;
                                }
                                vCommandUpdate = vCommandUpdate + vColumn.ColumnName + " = " + vDataRow[vColumn].ToString().Trim() + ",";
                                /// Заполнение таблицы с изменениями DatChg
                                //del __fTableChanges.Rows.Add(_fLockClue, vColumn.ColumnName, vValueNew);
                            }
                        }
                        vCommandUpdate = vCommandUpdate.Substring(0, vCommandUpdate.Length - 1) + " Where CLU = " + vClueString;

                        if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommandUpdate) >= 0)
                        {
                            vReturn = true & __mOtherTableChangeOnUpdate(Convert.ToInt32(vDataRow["CLU"]));
                        }
                    }

                    /// Удаление записи
                    if (vDataRow.RowState == DataRowState.Deleted)
                    {
                        int vRecordClue = Convert.ToInt32(vDataRow["CLU", DataRowVersion.Original]); // Идентификатор записи
                        string vCommandDelete = "Delete From " + __fTableName + " Where CLU = " + vRecordClue.ToString();

                        if (__mTriggerDelete(vDataRow) == false)
                        {
                            __mError("Не удалось удалить запись", "__mUpdate(DataTable)");
                            _mLockOff(vLockClue);
                            return false;
                        }
                        else
                        {
                            if (datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommandDelete) > 0)
                            {
                                vReturn = true & __mOtherTableChangeOnDelete(Convert.ToInt32(vDataRow["CLU"]));
                            }

                            else
                                vReturn = false;
                        }
                    }

                    _mLockOff(vLockClue);
                }
                /// Протоколирование исправлений в полях
                if (_fTableChanges.Rows.Count > 0)
                {
                    foreach (DataRow vDataRow in _fTableChanges.Rows)
                    {
                        string vCommandString = "Insert Into DatChg(lnkDatLck, Fld, Val) Values(" + vDataRow["lnkDatLck"].ToString() +
                                                                                                    ", '" + vDataRow["Fld"].ToString() + "'" +
                                                                                                    ", '" + vDataRow["Val"].ToString() + "')";
                        int vResult = datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlCommand(vCommandString);
                    }
                }

                #region - Закрытие транзакции

                if (vReturn == true)
                {
                    datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTransactionOff(false);
                }
                else
                {
                    datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTransactionOff(true);
                }

                #endregion - Закрытие транзакции

                return vReturn;
            } // Открытие транзакции
            else
            {
                datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mTransactionOff(false);
                return false;
            }
        }

        #endregion Таблица

        #region Триггеры

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        /// <param name="pDataRow">Запись таблицы</param>
        /// <param name="pTriggerType">Вид триггера</param>
        /// <returns>[true] - все поля заполнены верно, иначе - [false]</returns>
        public virtual bool __mCheckRecordFieldsFill(ref DataRow pDataRow, TRIGGERTYPEFORCHANGERECORD pTriggerType)
        {
            return true;
        }
        /// <summary>
        /// Триггер вставки записи
        /// </summary>
        /// <param name="pDataRow">Проверяемая вставляемая строка</param>
        /// <returns>[true] - Запись может быть вставлена, иначе - [false]</returns>
        public virtual bool __mTriggerInsert(ref DataRow pDataRow)
        {
            bool vReturn = true; /// Возвращаемое значение

            if (__mCheckRecordFieldsFill(ref pDataRow, TRIGGERTYPEFORCHANGERECORD.Insert) == false) // Проверка заполнения полей
                vReturn = false;
            return vReturn;
        }
        /// <summary>
        /// Триггер обновления записи
        /// </summary>
        /// <param name="pDataRow">Проверяемая обновляемая строка</param>
        /// <returns>[true] - Запись может быть обновлена, иначе - [false]</returns>
        public virtual bool __mTriggerUpdate(ref DataRow pDataRow)
        {
            bool vReturn = true; /// Возвращаемое значение

            if (__mCheckRecordFieldsFill(ref pDataRow, TRIGGERTYPEFORCHANGERECORD.Update) == false) // Проверка заполнения полей
                vReturn = false;
            return vReturn;
        }
        /// <summary>
        /// Триггер удаления
        /// </summary>
        /// <param name="pRecordClue">Идентификатор удаляемой записи таблицы</param>
        /// <returns>[true] - удаление возможно, иначе - [false].</returns>
        public virtual bool __mTriggerDelete(DataRow pDataRow)
        {
            if (__fDeleteType == DELETETYPES.Mark)
                return false;
            else
                return true;
        }

        #endregion Триггеры

        #region Исправления в других таблицах

        /// <summary>
        /// Выполнение исправлений в других таблицах при вставке
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns>[true] - изменения в других таблицах выполнены, иначе - [false].</returns>
        public virtual bool __mOtherTableChangeOnInsert(int pClue)
        {
            return true;
        }
        /// <summary>
        /// Выполнение исправлений в других таблицах при обновлении
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns>[true] - изменения в других таблицах выполнены, иначе - [false].</returns>
        public virtual bool __mOtherTableChangeOnUpdate(int pClue)
        {
            return true;
        }
        /// <summary>
        /// Выполнение исправлений в других таблицах при удалении
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns>[true] - изменения в других таблицах выполнены, иначе - [false]</returns>
        public virtual bool __mOtherTableChangeOnDelete(int pClue)
        {
            return true;
        }

        #endregion Исправления в других таблицах

        #endregion Процедуры

        #region - Внутренние

        /// <summary>
        /// Получение полного имени из дерева данных
        /// </summary>
        /// <param name="pClue">Идентификатор записи для которой нужно получить полное имя</param>
        private void mTreeFullName(int pClue)
        {
            fTreeFullName = Convert.ToString(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Select des" + __fTableName + " From " + __fTableName + " Where CLU = " + pClue.ToString())).TrimEnd() + "\\" + fTreeFullName; // Полное имя узла дерева
            int vClueParent = Convert.ToInt32(datApplication.__oData.__mDataSourceGet(__fDataSourceAlias).__mSqlValue("Select lnk" + __fTableName + " From " + __fTableName + " Where CLU = " + pClue.ToString())); // Ключ родительской записи
            if (vClueParent > 0)
                mTreeFullName(vClueParent);
        }

        #endregion Внутренние 

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Порядок расчета нового учетного кода
        /// </summary>
        public CODESNEWTYPES __fCodeNewCalculateType = CODESNEWTYPES.Next;
        /// <summary>
        /// Имя используемого источника данных
        /// </summary>
        public string __fDataSourceAlias = "";
        /// <summary>
        /// Вид удаления записей
        /// </summary>
        public DELETETYPES __fDeleteType = DELETETYPES.Delete;
        /// <summary>
        /// Поля содержащиеся в таблице
        /// </summary>
        public List<datUnitModelField> __fFieldS = new List<datUnitModelField>();
        /// <summary>
        /// Идентификатор последней вставленной записи в методе __mUpdate()
        /// </summary>
        public int __fLastInsertedKey = -1;
        /// <summary>
        /// Идентификатор блокировки записи
        /// </summary>
        public int __fLockClue = 0;
        /// <summary>
        /// Указание использования блокировки записи
        /// </summary>
        public bool __fLockUsed = false;
        /// <summary>
        /// Краткое описание таблицы
        /// </summary>
        public string __fTableDescription = "";
        /// <summary>
        /// Название таблицы сущности
        /// </summary>
        public string __fTableName = "";
        /// <summary>
        /// Префикс таблицы в запросах
        /// </summary>
        public string __fTablePrefix = "";

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>
        /// Полное название класса 
        /// </summary>
        protected string _fClassNameFull = "";
        /// <summary>
        /// Таблица со списком исправленнных полей
        /// </summary>
        protected DataTable _fTableChanges = new DataTable("ChangesValue");
        /// <summary>
        /// Список ошибок при выполнении триггеров
        /// </summary>
        protected ArrayList _fTriggerErrorsDescriptions = new ArrayList();

        #endregion Внутренние

        #region - Служебные

        /// <summary>
        /// Полный путь к узлу дерева
        /// </summary>
        private string fTreeFullName = "";

        #endregion Служебные 

        #endregion ПОЛЯ    
    }
}
