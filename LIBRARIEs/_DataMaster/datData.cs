using nlApplication;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System;

namespace nlDataMaster
{
    /// <summary>
    /// Класс 'datData'
    /// </summary>
    /// <remarks>Элемент для работы с набором источников данных</remarks>
    public class datData
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public datData()
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
            Type vType = this.GetType();
            _fClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        #region Sql операции

        /// <summary>
        /// Отправка команды источнику данных
        /// </summary>
        /// <param name="pCommand">Команда отправляемая источнику данных</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Количество обработанных командой записей</returns>
        public int __mSqlCommand(string pCommand, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlCommand(string, string = \"\")";
            vError.__mPropertyAdd("Команда{0} {1}", ":", pCommand);
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания команды
            if (String.IsNullOrEmpty(pCommand) == true)
                vError.__mReasonAdd("Не указана команда");

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mSqlCommand(pCommand);
        }
        /// <summary>
        /// Отправка команды источнику данных с асинхронным получением результатов
        /// </summary>
        /// <param name="pCommand">Команда отправляемая источнику данных</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Количество обработанных командой записей</returns>
        public Task<int> __mSqlCommandAsync(string pCommand, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlCommandAsync(string, string = \"\")";
            vError.__mPropertyAdd("Команда{0} {1}", ":", pCommand);
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания команды
            if (String.IsNullOrEmpty(pCommand) == true)
                vError.__mReasonAdd("Отсутствует команда");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mSqlCommandAsync(pCommand);
        }
        /// <summary>
        /// Отправка запроса источнику данных
        /// </summary>
        /// <param name="pQuery">Команда отправляемая источнику данных</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Количество обработанных командой записей</returns>
        public DataTable __mSqlQuery(string pQuery, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlQuery(string, string = \"\")";
            vError.__mPropertyAdd("Команда{0} {1}", ":", pQuery);
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания запроса
            if (String.IsNullOrEmpty(pQuery) == true)
                vError.__mReasonAdd("Отсутствует содержание запроса");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mSqlQuery(pQuery);
        }
        /// <summary>
        /// Отправка запроса источнику данных с асинхронным получением результата
        /// </summary>
        /// <param name="pQuery">Команда отправляемая источнику данных</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Количество обработанных командой записей</returns>
        public Task<DataTable> __mSqlQueryAsync(string pQuery, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlQueryAsync(string, string = \"\")";
            vError.__mPropertyAdd("Команда{0} {1}", ":", pQuery);
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания запроса
            if (String.IsNullOrEmpty(pQuery) == true)
                vError.__mReasonAdd("Отсутствует содержание запроса");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mSqlQueryAsync(pQuery);
        }
        /// <summary>
        /// Выполнение хранимой процедуры источника данных
        /// </summary>
        /// <param name="pStoredProcedure">Название хранимой процедуры</param>
        /// <param name="pParameters">Список параметров</param>
        /// <returns></returns>
        public virtual DataTable __mSqlStoredProcedures(string pStoredProcedure, string pDataSourceAlias = "", params object[] pParameters)
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlCommandStoredProcedures(string, params object[], \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);
            vError.__mPropertyAdd("Хранимая процедура{0} {1}", ":", pStoredProcedure);
            foreach (object vObject in pParameters)
            {
                vError.__mPropertyAdd("Параметр{0} {1}", ":", vObject.ToString());
            }
            /// Проверка указания команды
            if (String.IsNullOrEmpty(pStoredProcedure) == true)
                vError.__mReasonAdd("Не указана хранимая процедура");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mSqlStoredProcedures(pStoredProcedure, pParameters);
        }
        /// <summary>
        /// Получение значения поля удовлетворяющего команде. Заполняется в предметном источнике данных 
        /// </summary>
        /// <param name="pCommand">Команда для получения значения поля</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - если значение не найдено, иначе - {object} - значение поля</returns>
        public object __mSqlValue(string pCommand, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlValue(string, string = \"\")";
            vError.__mPropertyAdd("Команда{0} {1}", ":", pCommand);
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания команды
            if (String.IsNullOrEmpty(pCommand) == true)
                vError.__mReasonAdd("Отсутствует команда");

            /// Создание источника данных и проверка его достоверности
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return (vDataSource.__mSqlQuery(pCommand) as DataTable).Rows[0][0];
        }
        /// <summary>
        /// Получение значения поля удовлетворяющего команде. Заполняется в предметном источнике данных 
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pExpressionWhere">Условие поиска записи</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - если значение не найдено, иначе - {object} - значение поля</returns>
        public object __mSqlValue(string pTableName, string pFieldName, string pExpressionWhere, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mSqlValue(string, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания таблицы
            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Отсутствует таблица");
            /// Проверка указания поля
            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Отсутствует поле");
            /// Проверка указания условия поиска
            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Отсутствует условие поиска");

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mSqlValue(pTableName, pFieldName, pExpressionWhere);
        }

        #endregion Sql операции

        #region Базы данных

        /// <summary>
        /// Создание резервной копии базы данных
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Путь созданного файла копии базы данных</returns>
        public string __mDatabaseBackUp(string pDataSourceAlias = "")
        {
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            return vDataSource.__mDatabaseBackUp();
        }
        /// <summary>
        /// Сравнение структуры таблиц в базе данных с моделью базы данных
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будет выполняться сравнение</param>
        /// <returns>[true] - структуры одинаковы, иначе - [false]</returns>
        public bool __mDatabaseCompareWithModel(string pDataSourceAlias = "")
        {
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            return vDataSource.__mDatabaseCompareWithModel();
        }
        /// <summary>
        /// Создание базы данных
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - база данных создана или уже существует, иначе - [false]</returns>
        public bool __mDatabaseCreate(string pDataSourceAlias = "")
        {
            return __mDataSourceGet(pDataSourceAlias).__mDatabaseCreate();
        }
        /// <summary>
        /// Восстановление базы данных из копии
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - Файл копии базы данных создан, иначе - [false]</returns>
        public bool __mDatabaseRestore(string pFilePath, string pDataSourceAlias = "")
        {
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            return vDataSource.__mDatabaseRestore(pFilePath);
        }
        /// <summary>
        /// Печать структуры базы данных в источнике
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Путь и имя файла отчета</returns>
        public string __mDatabaseStructurePrint(string pDataSourceAlias = "")
        {
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            return vDataSource.__mDatabaseStructurePrint();
        }
        /// <summary>
        /// Удаление базы данных
        /// </summary>
        /// <param name="pDataBase"></param>
        /// <param name="pDataSourceAlias"></param>
        /// <returns>[true] </returns>
        public bool __mDatabaseDrop(string pDataBase, string pDataSourceAlias)
        {
            return __mDataSourceGet(pDataSourceAlias).__mDatabaseDrop(pDataBase);
        }

        #endregion Базы данных

        #region Блокировки

        /// <summary>
        /// Закрытие блокировок пользователя
        /// </summary>
        /// <param name="pUserClue">Идентификатор пользователя</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        public void __mLockClear(int pUserClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mLockClear(int, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания идентификатора пользователя
            if (pUserClue <= 0)
                vError.__mReasonAdd("Идентификатор пользователя указан не верно");

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return;
            }
            else
            {
                /// Получение списка процессов удерживающих записи
                /// Проверка процессов в списке запущенных процессов по идентификатору и названию
                /// Снятие блокировки с остановленных процессов
                vDataSource.__mLockClear(pUserClue);
            }
        }
        /// <summary>
        /// Снятие блокировок пользователя во всех областях
        /// </summary>
        /// <param name="pUserClue"></param>
        public void __mLockClearAll(int pUserClue)
        {
            foreach (datUnitDataSource vDataSource in fDataSourceS)
            {
                __mLockClear(pUserClue, vDataSource.__fAlias);
            }
        }
        /// <summary>
        /// Снятие блокировки
        /// </summary>
        /// <param name="pLockClue">Идентификатор блокировки</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - блокировка снята, иначе - [false]</returns>
        public bool _mLockOff(int pLockClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mLockOff(int, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания идентификатора блокировки
            if (pLockClue <= 0)
                vError.__mReasonAdd("Идентификатор блокировки указан не верно");

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mLockOff(pLockClue);
        }
        /// <summary>
        /// Блокировка таблицы или записи в таблице 
        /// </summary>
        /// <param name="pLockName">Название объекта блокировки</param>
        /// <param name="pClue">Идентификатор записи</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <remarks>Если 'pRecord' = 0, то блокируется вся таблица</remarks>
        /// <returns>Идентификатор заблокированной записи, [0] - запись не удалось заблокировать, [-1] - Блокировки отключены</returns>
        public int _mLockOn(string pLockName, int pClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mLockOn(string, int, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания объекта блокировки
            if (String.IsNullOrEmpty(pLockName) == true)
                vError.__mReasonAdd("Объект блокировки указан не верно");
            /// Проверка указания идентификатора блокировки
            if (pClue < 0)
                vError.__mReasonAdd("Идентификатор записи указан не верно");

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mLockOn(pLockName, pClue);
        }

        #endregion Блокировки

        #region Выражения

        /// <summary>
        /// Создание выражения 'Like' с использованием транслита
        /// </summary>
        /// <param name="pFieldName">Название поля для которого строиться выражение</param>
        /// <param name="pText">Текст условия на одной из раскладок клавиатуры</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        public string __mExpressionLikeEntryTranslit(string pFieldName, string pText, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mExpressionLikeTranslit(string, string)";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return "";
            }
            else
                return vDataSource.__mExpressionLikeEntryTranslit(pFieldName, pText);

        }
        /// <summary>
        /// Создание выражения 'Like' с использованием транслита
        /// </summary>
        /// <param name="pFieldName">Название поля для которого строиться выражение</param>
        /// <param name="pText">Текст условия на одной из раскладок клавиатуры</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        public string __mExpressionLikeStartTranslit(string pFieldName, string pText, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mExpressionLikeTranslit(string, string)";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return "";
            }
            else
                return vDataSource.__mExpressionLikeStartTranslit(pFieldName, pText);

        }
  
        #endregion Выражения

        #region Источники данных

        /// <summary>
        /// Подключение источника данных
        /// </summary>
        /// <param name="pDataSource">Источник данных</param>
        /// <returns>[true] - источник данных подключен, иначе - [false]</returns>
        public bool __mDataSourceAdd(datUnitDataSource pDataSource)
        {
            bool vReturn = false; // Возвращаемое значение
            long vTicksStart = DateTime.Now.Ticks;
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fProcedure = _fClassNameFull + "__mDataSourceAdd(datDataSource)";
            vError.__fMessage_ = "Не удалось подключить к приложению источник данных";

            /// Проверка указания источника данных
            if (pDataSource == null)
            {
                vError.__mPropertyAdd("Источник данных не определен");
                vError.__mReasonAdd("Параметр - источник данных{0} {1}", " =", "[null]");
            }
            else
            {
                /// Проверка указания псевдонима у источника данных
                if (pDataSource.__fAlias.Length == 0)
                {
                    vError.__mReasonAdd("Псевдоним источник данных не определен");
                }
                /// Поиск подключенного источика данных с псевдонимом одинаковым с псевдонимом полученного источника данных
                foreach (datUnitDataSource vDataSourceConnected in fDataSourceS)
                {
                    if (vDataSourceConnected.__fAlias.Trim().ToUpper() == pDataSource.__fAlias.Trim().ToUpper())
                    {
                        vError.__mPropertyAdd("Псевдоним источника данных{0} '{1}'", ":", pDataSource.__fAlias);
                        vError.__mReasonAdd("Источник данных с указанным псевдонимом уже подключен");
                        break;
                    }
                }
            }
            /// Если обнаружены ошибки, выдается сообщение об ошибке
            if (vError.__fReasonS_.Count > 0)
            {
                vError.__mPropertyAdd("Псевдоним источника данных: {0}", pDataSource.__fAlias);
                vError.__mPropertyAdd("База данных: {0}", Path.Combine(pDataSource.__fDatabasePath, pDataSource.__fDatabaseName));
                vError.__mPropertyAdd("Строка подключения: {0}", pDataSource.__fConnectionLine);
                vError.__mMessageBuild("Не удалось подключить источник данных");
                vError.__fErrorsType = ERRORSTYPES.Data;
                datApplication.__oErrorsHandler.__mShow(vError);
            }
            /// Подключение источника данных
            else
            {
                fDataSourceS.Add(pDataSource);
                datApplication.__oProtocols.__mCreate(PROTOCOLSTYPES.DataEvent, _fClassNameFull + "__mDataSourceAdd(datDataSource)");
                datApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Message, datApplication.__oTunes.__mTranslate("Подключен источник данных '{0}'", pDataSource.__fAlias), DateTime.Now.Ticks - vTicksStart);
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Получение подключенного к приложению источника данных по псевдониму
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        /// <returns>{datDataSource}</returns>
        public datUnitDataSource __mDataSourceGet(string pDataSourceAlias = "")
        {
            datUnitDataSource vReturn = null; // Возвращаемое значение

            /// Если псевдоним источника данных не указан, то береться назначенный по умолчанию.
            if (String.IsNullOrEmpty(pDataSourceAlias) == true)
            {
                if (String.IsNullOrEmpty(__fDataSourceCurrentAlias) == false)
                {
                    pDataSourceAlias = __fDataSourceCurrentAlias;
                }
                /// Если источник данных по умолчанию не указан, отображается окно с ошибкой и возвращается [null].
                else
                {
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.Data;
                    vError.__fProcedure = _fClassNameFull + "__mDataSourceGet(string)";
                    vError.__fMessage_ = "Не возможно получить источник данных";
                    vError.__mReasonAdd("Название источника данных не указано");
                    vError.__mPropertyAdd("Параметр{0} {1}", ":", pDataSourceAlias);
                    vError.__mPropertyAdd("Название источника данных по умолчанию{0} '{1}'", ":", __fDataSourceCurrentAlias);
                    datApplication.__oErrorsHandler.__mShow(vError);
                    return null;
                }
            }
            /// Поиск подключенного источика данных в списке подключенных источников данных.
            foreach (datUnitDataSource vDataSourceConnected in fDataSourceS)
            {
                if (vDataSourceConnected.__fAlias.Trim().ToUpper() == pDataSourceAlias.Trim().ToUpper())
                {
                    vReturn = vDataSourceConnected;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Отключение источника данных
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        public bool __mDataSourceRemove(string pDataSourceAlias)
        {
            if (String.IsNullOrEmpty(pDataSourceAlias) == true)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Data;
                vError.__fProcedure = _fClassNameFull + "__mDataSourceGet(string)";
                vError.__fMessage_ = "Не возможно получить источник данных";
                vError.__mReasonAdd("Название источника данных не указано");
                vError.__mPropertyAdd("Параметр{0} {1}", ":", pDataSourceAlias);
                vError.__mPropertyAdd("Название источника данных по умолчанию{0} '{1}'", ":", __fDataSourceCurrentAlias);
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias); // Удаляемый источник данных

            if (vDataSource != null)
            {
                fDataSourceS.Remove(vDataSource);
            }

            return true;
        }
        /// <summary>
        /// Отключение всех источников данных
        /// </summary>
        public bool __mDataSourceRemoveAll()
        {
            List<datUnitDataSource> vDataSourceS = fDataSourceS; // Список подключенных источников данных
            ArrayList vDataSourcesAliaseS = new ArrayList(); // Список псевдонимов подключенных источников данных
            /// Получение списка псевдонимов подключенных источников данных
            foreach (datUnitDataSource vDataSource in vDataSourceS)
            {
                vDataSourcesAliaseS.Add(vDataSource.__fAlias);
            }
            /// Отключение источников данных по списку подключенных
            foreach (string vAlias in vDataSourcesAliaseS)
            {
                __mDataSourceRemove(vAlias);
            }

            return true;
        }

        #endregion Источники данных

        #region Модель

        /// <summary>
        /// Печать модели структуры базы данных
        /// </summary>
        /// <returns>Путь и имя файла отчета</returns>
        public string _mModelStructurePrint(string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mModelStructurePrint(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return "";
            }
            else
                return vDataSource.__mModelStructurePrint();
        }
        /// <summary>
        /// Создание отчета сравнения модели с базой данных 
        /// </summary>
        public string _mModelCompareWithDatabase(string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mModelCompareWithDatabase(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return "";
            }
            else
                return vDataSource._mModelCompareWithDatabase();
        }
        /// <summary>
        /// Получение описания таблицы из модели 
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        /// <returns>Описание таблицы</returns>
        public string _mModelTableDescription(string pTableName, string pDataSourceAlias = "")
        {
            string vReturn = ""; // Возвращаемое значение

            foreach (datUnitModelTable vTable in __mDataSourceGet(pDataSourceAlias).__fModelTableS)
            {
                if (vTable.__fName == pTableName)
                {
                    vReturn = vTable.__fDescription;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Получение описания поля таблицы  из модели
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        /// <returns>Описание поля таблицы</returns>
        public string _mModelFieldDescription(string pTableName, string pFieldName, string pDataSourceAlias = "")
        {
            string vReturn = ""; // Возвращаемое значение

            foreach (datUnitModelTable vTable in datApplication.__oData.__mDataSourceGet(pDataSourceAlias).__fModelTableS)
            {
                if (vTable.__fName == pTableName)
                {
                    bool vFieldFound = false; // Поле таблицы найдено

                    foreach (datUnitModelField vField in vTable.__fFieldS)
                    {
                        if (vField.__fName == pFieldName)
                        {
                            vReturn = vField.__fDescription;
                            vFieldFound = true;
                            break;
                        }
                    }

                    if (vFieldFound == true)
                        break;
                }
            }

            return vReturn;
        }

        #endregion Модель

        #region Подключение

        /// <summary>
        /// Построение строки подключения к источнику данных
        /// </summary>
        /// <param name="pLogin">Использование логина с паролем</param>
        /// <returns>[true] - строка построена, иначе - [false]</returns>
        protected virtual bool _mConnectionLineBuild(bool pLogin)
        {
            return false;
        }
        /// <summary>
        /// Разрыв соединения с источником данных
        /// </summary>
        protected virtual bool _mConnectionOff()
        {
            return false;
        }
        /// <summary>
        /// Установка соединения с источником данных
        /// </summary>
        /// <returns>[true] - соединение установлено, иначе - [false]</returns>
        protected virtual bool _mConnectionOn()
        {
            return false;
        }

        #endregion Подключение

        #region Пользователи

        /// <summary>
        /// Получение доступа пользователя к объекту
        /// </summary>
        /// <param name="pRight">Право</param>
        /// <param name="pClueUserRole">Рoль пользователя</param>
        /// <param name="pClueUser">Пользователь</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        /// <returns>[true] - доступ разрешен, иначе - [false]</returns>
        public bool __mUserAccess(int pRight, int pClueUser, string pDataSourceAlias = "")
        {
            bool vReturn = false; // Возвращаемое значение
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mUserAccess(int, int, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            /// Проверка указания права
            if (pRight <= 0)
                vError.__mReasonAdd("Право указано не верно");
            /// Проверка указания заголовка объекта
            if (pClueUser <= 0)
                vError.__mReasonAdd("Пользователь указан не верно");

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                vReturn = false;
            }
            else
                vReturn = vDataSource.__mUserAccess(pRight, pClueUser);

            return vReturn;
        }
        /// <summary>
        /// Пользователь - администратор
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - текущий пользователь администратор, иначе - [false]</returns>
        public bool _mUserAdministrator(string pDataSourceAlias = "")
        {
            bool vReturn = false; // Возвращаемое значение
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mUserAdministrator(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                vReturn = false;
            }
            else
                vReturn = vDataSource.__fUserAdministrator;

            return vReturn;
        }
        /// <summary>
        /// Псевдоним пользователя
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[""] - пользователь не определен, иначе - псевдоним пользователя</returns>
        public string __mUserAlias(string pDataSourceAlias = "")
        {
            string vReturn = ""; // Возвращаемое значение

            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mUserAlias(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                vReturn = "";
            }
            else
                vReturn = vDataSource.__fUserAlias.Trim();

            return vReturn;
        }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[<=0] - пользователь не определен, иначе - [true]</returns>
        public int __mUserClue(string pDataSourceAlias = "")
        {
            int vReturn = 0; // Возвращаемое значение

            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mUserClue(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                vReturn = -1;
            }
            else
                vReturn = vDataSource.__fUserClue;

            return vReturn;
        }
        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[<=0] - роль пользователя не определена, иначе - [true]</returns>
        public int __mUserRoleClue(string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mUserRoleClue(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__fUserRoleClue;
        }
        /// <summary>
        /// Название роли пользователя
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[""] - роль пользователя не определена, иначе - название роли пользователя</returns>
        public string __mUserRoleName(string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mUserRoleName(string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return "";
            }
            else
                return vDataSource.__fUserRoleName.Trim();
        }

        #endregion Пользователи

        #region Таблицы

        /// <summary>
        /// Получение пустого курсора со структурой таблицы из базы данных (заполняется в базовом классе)
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - таблица не получена, иначе {DataTable}</returns>
        public DataTable __mTableEmpty(string pTableName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableEmpty(string, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mTableEmpty(pTableName);
        }
        /// <summary>
        /// Проверка существования таблицы в базе данных источника данных
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - таблица существует, иначе - [false]</returns>
        public bool __mTableExists(string pTableName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableExists(string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTableExists(pTableName);
        }
        /// <summary>
        /// Получение списка таблиц в базе данных
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - таблиц нет, иначе - список таблиц в базе данных</returns>
        public ArrayList _mTablesList(string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mTablesList(string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return new ArrayList();
            }
            else
                return vDataSource.__mTablesList();
        }
        /// <summary>
        /// Очистка таблицы со сбросом идентификатора в 0
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - таблица очищена, иначе - [false]</returns>
        public bool _mTableTruncate(string pTableName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mTableTruncate(string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTableTruncate(pTableName);
        }

        #endregion Таблицы

        #region Таблицы - Строки

        /// <summary>
        /// Получение записи из таблицы указанной идентификатором
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClue">Идентификатор записи</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - таблиц нет, иначе - {DataTable}</returns>
        public DataTable __mTableRow(string pTableName, int pClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableRow(string, int, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (pClue <= 0)
                vError.__mReasonAdd("Не указан идентификатор записи");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mTableRow(pTableName, pClue);
        }
        /// <summary>
        /// Получение записи из таблицы указанной уникальным идентификатором
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pGuid">Уникальный идентификатор записи</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - таблиц нет, иначе - {DataTable}</returns>
        public DataTable __mTableRow(string pTableName, Guid pGuid, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableRow(string, Guid, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (pGuid == Guid.Empty)
                vError.__mReasonAdd("Не указан уникальный идентификатор записи");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mTableRow(pTableName, pGuid);
        }
        /// <summary>
        /// Установка текущего времени в качестве последнего времени изменения записи
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClue">Идентификатор записи</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - Время исправлено, иначе - [false]</returns>
        public bool __mTableRowChangeTimeNow(string pTableName, int pClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mRowChangeTimeNow(string, int, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (pClue <= 0)
                vError.__mReasonAdd("Не указан уникальный идентификатор записи");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTableRowChangeTimeNow(pTableName, pClue);
        }
        /// <summary>
        /// Подсчет количества дублирующихся записей
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - если данные не найдены, иначе - {DataTable} - таблица с названием поля и количеством повторений</returns>
        public DataTable __mTableRowCountDouble(string pTableName, string pFieldName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mRowCountDouble(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (String.IsNullOrEmpty(pFieldName) == true)
                vError.__mReasonAdd("Не указан имя поля");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mTableRowsCountDouble(pTableName, pFieldName);
        }
        /// <summary>
        /// Подсчет количества записей удовлетворяющих условию
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pExpressionWhere">Условие для подсчета записей</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Количество подсчитанных записей</returns>
        public int __mTableRowsCountWhere(string pTableName, string pExpressionWhere, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mRowsCountWhere(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (String.IsNullOrEmpty(pExpressionWhere) == true)
                vError.__mReasonAdd("Не указано условие для подсчета записей");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mTableRowsCountWhere(pTableName, pExpressionWhere);
        }

        #endregion Таблицы - Строки

        #region Таблицы - Поля

        /// <summary>
        /// Проверка существования поля в таблице источника данных
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - Поле существует, иначе - [false]</returns>
        public bool __mTableColumnExists(string pTableName, string pFieldName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableFieldExists(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (String.IsNullOrEmpty(pFieldName) == true)
                vError.__mReasonAdd("Не указано имя поля");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTableColumnExists(pTableName, pFieldName);
        }
        /// <summary>
        /// Получение списка полей в таблице
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[null] - данные не обнаружены, иначе {DataTable} заполненная списком полей указанной таблицы</returns>
        public DataTable _mTableColumnS(string pTableName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mTableFieldS(string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mTableColumnS(pTableName);
        }
        /// <summary>
        /// Получение информации о поле таблицы 
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pField">Название поля</param>
        /// <param name="pFieldInfo">Вид операции</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>Запрашиваемое значение, иначе [null]</returns>
        public virtual object __mTableColumnInfo(string pTableName, string pFieldName, FIELDINFO pFieldInfo, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableFieldInfo(string, string, FIELDINFO, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (String.IsNullOrEmpty(pFieldName) == true)
                vError.__mReasonAdd("Не указано имя поля");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return null;
            }
            else
                return vDataSource.__mTableColumnInfo(pTableName, pFieldName, pFieldInfo);
        }
        /// <summary>
        /// Добавление колонки в таблицу
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pColumnName">Название колонки</param>
        /// <param name="pDataType">Тип колонки</param>
        /// <param name="IsNull">Допустимость 'Null' значений</param>
        /// <param name="pColumnScale">Размер колонки</param>
        /// <param name="pColumnPrecision">Точность колонки</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - Колонка добавлена, иначе - [false]</returns>
        public virtual bool __mTableColumnAdd(string pTableName, string pColumnName, COLUMNSTYPES pDataType, bool IsNull, int pColumnScale = 0, int pColumnPrecision = 0, string pDefaultValue = "0", string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableColumnAdd(string, string, COLUMNSTYPES, bool, int = 0, int = 0, string = \"\")";
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (String.IsNullOrEmpty(pColumnName) == true)
                vError.__mReasonAdd("Не указано имя поля");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTableColumnAdd(pTableName, pColumnName, pDataType, IsNull, pColumnScale, pColumnPrecision, pDefaultValue);
        }
        /// <summary>
        /// Удаление колонки из таблицы
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pColumnName">Название колонки</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - Колонка удалена, иначе - [false]</returns>
        public virtual bool __mTableColumnDrop(string pTableName, string pColumnName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mTableColumnDrop(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            if (String.IsNullOrEmpty(pTableName) == true)
                vError.__mReasonAdd("Не указано имя таблицы");
            if (String.IsNullOrEmpty(pColumnName) == true)
                vError.__mReasonAdd("Не указано имя поля");
            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");

            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTableColumnDrop(pTableName, pColumnName);
        }

        #endregion Таблицы - Поля

        #region Транзакции

        /// <summary>
        /// Закрытие транзакции
        /// </summary>
        /// <param name="pCommit">Условие закрытия транзакции. [true] - [Commit], [false] - [RollBack]</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - Транзакция закрыта, иначе - [false]</returns>
        public bool _mTransactionOff(bool pCommit, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mTransactionOff(bool, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTransactionOff(pCommit);
        }
        /// <summary>
        /// Открытие транзакции
        /// </summary>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[true] - транзация создана, иначе - [false]</returns>
        public bool _mTransactionOn(string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "_mTransactionOn(string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mTransactionOn();
        }

        #endregion Транзакции

        #region Функции - Идентификаторы

        /// <summary>
        /// Получение идентификатора по учетному коду
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pCode">Учетный код</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных в котором будут выполняться операции</param>
        /// <returns>[<=0] - Запись не найдена, иначе - идентификатор</returns>
        public virtual int __mClueByCode(string pTableName, int pCode, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mClueByCode(string, int, string = \"\")"; ;
            vError.__mPropertyAdd("Параметр 'Название таблицы' {0} {1}", ":", pDataSourceAlias);
            vError.__mPropertyAdd("Параметр 'Учетный код' {0} {1}", ":", pDataSourceAlias);
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mClueByCode(pTableName, pCode);

        }
        /// <summary>
        /// Получение идентификатора записи по значению поля названия
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pFieldName">Значение поля названия</param>
        /// <returns></returns>
        public virtual int __mClueByName(string pTableName, string pFieldName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mClueByName(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mClueByName(pTableName, pFieldName);
        }
        /// <summary>
        /// Получение идентификатора записи по названию поля опции
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pFieldOptionName">Имя поля опции</param>
        /// <returns>Значение поля идентификатор записи</returns>
        public virtual int __mClueByOption(string pTableName, string pFieldOptionName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mClueByOption(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mClueByOption(pTableName, pFieldOptionName);
        }
        /// <summary>
        /// Проверка существования идентификатора в таблице
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClue">Идентификатоор записи</param>
        /// <returns></returns>
        public virtual bool __mClueExists(string pTableName, int pClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mClueExists(string, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            else
                return vDataSource.__mClueExists(pTableName, pClue);
        }
        /// <summary>
        /// Получение идентификатора последеней вставленной записи в таблицу
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <returns>[<=0] - Запись не найдена, иначе - идентификатор</returns>
        public virtual int __mClueLastInserted(string pTableName)
        {
            return 0;
        }

        #endregion Функции - Идентификаторы

        #region Функции - Названия

        /// <summary>
        /// Получение названия по идентификатору записи
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClue">Идентификатор искомой строки</param>
        /// <returns>Значение поля 'Название'</returns>
        public string __mNameByClue(string pTableName, int pClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mNameByClue(string, int, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return datApplication.__oTunes.__mTranslate("не определен");
            }
            else
                return vDataSource.__mNameByClue(pTableName, pClue);
        }
        /// <summary>
        /// Получение названия по идентификатору записи
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClue">Идентификатор искомой строки</param>
        /// <returns>[""] - значение не найдено, иначе - значение поля 'Название'</returns>
        public string __mNameByCode(string pTableName, int pClue)
        {
            return "null";
        }
        /// <summary>
        /// Получение названия справочника по названию и значению опции
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pOptionName">Название опции</param>
        /// <returns>Значение поля 'Название' </returns>
        public string __mNameByOption(string pTableName, string pOptionName, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mNameByClue(string, int, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return datApplication.__oTunes.__mTranslate("не определен");
            }
            else
                return vDataSource.__mNameByOption(pTableName, pOptionName);
        }
        /// <summary>
        /// Проверка существования названия
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pValue">Значение поля названия</param>
        /// <param name="pClueSkip">Идентификатор исключаемой записи</param>
        /// <returns>[true] - Указанное название уже существует, иначе - [false]</returns>
        public bool __mNameExists(string pTableName, string pValue, int pClueSkip)
        {
            return false;
        }

        #endregion Функции - Названия

        #region Функции - Учетные коды

        /// <summary>
        /// Получение значения поля учетного кода по идентификатору записи
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClue">Идентификатор записи</param>
        /// <param name="pDataSourceAlias">Псевдоним источника данных</param>
        /// <returns>Значение поля учетного кода</returns>
        public virtual int __mCodeByClue(string pTableName, int pClue, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mCodeByClue(string, int, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mCodeByClue(pTableName, pClue);
        }
        /// <summary>
        /// Проверка существования учетного кода исключая идентификатор записи указанный в 'pClueSkip'
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pCodeCheck">Проверяемый учетный код</param>
        /// <param name="pClueSkip">Идентификатор записи который нужно исключить из поиска</param>
        /// <returns>[true] - Дублирующийся учетный код найден, иначе - [false]</returns>
        public virtual bool __mCodeExists(string pTableName, int pCodeCheck, int pClueSkip)
        {
            return false;
        }
        /// <summary>
        /// Вычисление нового учетного кода
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClueSkip">Идентификатор записи исключаемой из обработки</param>
        /// <param name="pCodeNewCalculateType">Порядок расчета нового учетного кода</param>
        /// <param name="pValueMaximal">Минимальное значение кода</param>
        /// <param name="pValueMinimal">Максимальное значение кода</param>
        /// <returns>Новый учетный код</returns>
        public virtual int __mCodeNew(string pTableName, int pClueSkip, CODESNEWTYPES pCodeNewCalculateType, int pValueMinimal = 1, int pValueMaximal = 999999)
        {
            return -1;
        }
        /// <summary>
        /// Вычисление нового учетного кода по нескольким полям
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClueSkip">Идентификатор записи исключаемой из обработки</param>
        /// <param name="pCodeNewCalculateType">Порядок расчета нового учетного кода</param>
        /// <param name="pValueMaximal">Минимальное значение кода</param>
        /// <param name="pValueMinimal">Максимальное значение кода</param>
        /// <param name="pFieldS">Список дополнительных полей</param>
        /// <returns>[0] - неудалось вычислить учетный код, иначе - новый учетный код</returns>
        public virtual int __mCodeNewGroup(string pTableName, int pClueSkip, CODESNEWTYPES pCodeNewCalculateType, int pValueMinimal, int pValueMaximal, ArrayList pFieldS)
        {
            return -1;
        }

        #endregion Функции - Учетные коды

        #region Функции - Позиция в документе

        /// <summary>
        /// Вычисление новой позиции в документе
        /// </summary>
        /// <param name="pTableName">Название таблицы</param>
        /// <param name="pClueSkip">Идентификатор записи исключаемой из обработки</param>
        /// <param name="pWhere">Условие отбирающее документ</param>
        /// <returns></returns>
        public int __mPositionNew(string pTableName, int pClueSkip, string pWhere, string pDataSourceAlias = "")
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mPositionNew(string, int, string, string = \"\")"; ;
            vError.__mPropertyAdd("Источник данных{0} {1}", ":", pDataSourceAlias);

            datUnitDataSource vDataSource = __mDataSourceGet(pDataSourceAlias);
            /// Проверка достоверности указания источника данных
            if (vDataSource == null)
                vError.__mReasonAdd("Источник данных указан не верно");
            if (vError.__fReasonS_.Count > 0)
            {
                datApplication.__oErrorsHandler.__mShow(vError);
                return -1;
            }
            else
                return vDataSource.__mPositionNew(pTableName, pClueSkip, pWhere);
        }

        #endregion Функция - Позиция в документе

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Псевдоним текущего источника данных
        /// </summary>
        public string __fDataSourceCurrentAlias = "";

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region - Служебные

        /// <summary>
        /// Подключенные источники данных
        /// </summary>
        private List<datUnitDataSource> fDataSourceS = new List<datUnitDataSource>();

        #endregion Служебные

        #endregion ПОЛЯ
    }
}
