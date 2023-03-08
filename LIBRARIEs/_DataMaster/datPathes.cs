using nlApplication;
using System.IO;
using System;

namespace nlDataMaster
{
    /// <summary>
    /// Класс 'datPathes'
    /// </summary>
    /// <remarks>Элемент для работы с путями приложения работаюшего с данными</remarks>
    public class datPathes : appPathes
    {
        #region = МЕТОДЫ

        #region - Файлы

        /// <summary>Формирование имени файла страховой копии базы данных
        /// </summary>
        /// <param name="pDatabaseName">Название базы данных</param>
        /// <param name="pExtension">Расширение копии файла</param>
        /// <returns>Путь и имя файла копии базы данных</returns>
        public string __mFileDataBaseBackUp(string pDatabaseName, string pExtension)
        {
            string vFileName = datApplication.__fPrefix + "_" + pDatabaseName + "_" + appTypeDateTime.__mDateTimeToFileNameTillSecond(DateTime.Now) + "." + pExtension;
            return Path.Combine(__fFolderDataBackUp, vFileName);
        }

        #endregion Файлы

        #endregion = МЕТОДЫ

        #region = СВОЙСТВА

        #region Папки

        /// <summary>
        /// Путь и имя папки для размещения файлов данных
        /// </summary>
        public string __fFolderData_
        {
            get
            {
                if (fFolderData.Length == 0)
                {
                    fFolderData = Path.Combine(__fFolderStart, "DATA\\");
                }
                try
                {
                    if (Directory.Exists(fFolderData) == false)
                    {
                        Directory.CreateDirectory(fFolderData);
                    }
                }
                catch { }

                return fFolderData;

            }
            set
            {
                fFolderData = value.Trim();
            }
        }
        /// <summary>
        /// Путь и имя папки для размещения копий файлов данны
        /// </summary>
        public string __fFolderDataBackUp
        {
            get
            {
                if (fFolderDataBackUp.Length == 0)
                {
                    fFolderDataBackUp = Path.Combine(__fFolderStart, "DATABackUp\\");
                }
                try
                {
                    if (Directory.Exists(fFolderDataBackUp) == false)
                    {
                        Directory.CreateDirectory(fFolderDataBackUp);
                    }
                }
                catch { }

                return fFolderDataBackUp;

            }
            set
            {
                fFolderDataBackUp = value.Trim();
            }
        }
        /// <summary>
        /// Путь и имя папки для размещения файлов с данными и ответами для отправки 
        /// </summary>
        public string __fFolderDataForSending_
        {
            get
            {
                if (fFolderDataForSending.Length == 0)
                {
                    fFolderDataForSending = Path.Combine(__fFolderData_, "ForSending\\");
                }
                try
                {
                    if (Directory.Exists(fFolderDataForSending) == false)
                    {
                        Directory.CreateDirectory(fFolderDataForSending);
                    }
                }
                catch { }

                return fFolderDataForSending;

            }
            set
            {
                fFolderDataForSending = value.Trim();
            }
        }
        /// <summary>
        /// Путь и имя папки для размещения полученных файлов с данными и ответов
        /// </summary>
        public string __fFolderDataReceived_
        {
            get
            {
                if (fFolderDataReceived.Length == 0)
                {
                    fFolderDataReceived = Path.Combine(__fFolderData_, "Received\\");
                }
                try
                {
                    if (Directory.Exists(fFolderDataReceived) == false)
                    {
                        Directory.CreateDirectory(fFolderDataReceived);
                    }
                }
                catch { }

                return fFolderDataReceived;

            }
            set
            {
                fFolderDataReceived = value.Trim();
            }
        }
        /// <summary>
        /// Путь и имя папки для размещения файлов запросов
        /// </summary>
        public string __pFolderQueries
        {
            get
            {
                if (_fFolderQueries.Length == 0)
                {
                    _fFolderQueries = Path.Combine(__fFolderStart, @"QUERIEs\");
                }
                try
                {
                    if (Directory.Exists(_fFolderQueries) == false)
                    {
                        Directory.CreateDirectory(_fFolderQueries);
                    }
                }
                catch
                {
                    _fFolderQueries = Path.Combine(__fFolderStart, @"QUERIEs\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с файлами запросов");
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return _fFolderQueries;
            }
            set { _fFolderQueries = value.Trim(); }
        }

        #endregion Папки

        #endregion СВОЙСТВА

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Путь и имя папки для размещения файлов запросов
        /// </summary>
        protected string _fFolderQueries = "";

        #endregion Внутренние

        #region - Служебные

        /// <summary>
        /// Путь и имя папки для размещения файлов данных
        /// </summary>
        private string fFolderData = "";
        /// <summary>
        /// Путь и имя папки для размещения копий файлов данных
        /// </summary>
        private string fFolderDataBackUp = "";
        /// <summary>
        /// Путь и имя папки для размещения файлов с данными и ответами для отправки 
        /// </summary>
        private string fFolderDataForSending = "";
        /// <summary>
        /// Путь и имя папки для размещения файлов с данными и ответами для отправки 
        /// </summary>
        private string fFolderDataReceived = "";

        #endregion Служебные

        #endregion ПОЛЯ
    }
}
