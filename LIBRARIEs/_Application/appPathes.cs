using System.IO;
using System;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appPathes'
    /// </summary>
    /// <remarks>Элемент для работы с путями приложения. Наследуется приложениями в которых нужны пути отличные от базовых</remarks>
    public class appPathes
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public appPathes()
        {
            Type vType = this.GetType();
            _fClassNameFull = vType.FullName + ".";
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Процедуры

        #region Файлы

        /// <summary>
        /// Возвращает путь и имя файла текущего протокола приложения
        /// </summary>
        /// <param name="pFileExtension">Расширение файла протокола</param>
        /// <returns>Путь и имя файла текущего протокола приложения</returns>
        public string __mFileProtocol(string pFileExtension = "")
        {
            DateTime vDateTime = DateTime.Now; // Текущие дата и время
            return Path.Combine(__fFolderProtocols_, appApplication.__fPrefix + "_" + appTypeDateTime.__mDateToFileName(vDateTime) + (pFileExtension.Length > 0 ? "." + pFileExtension : pFileExtension)); ; ;
        }
        /// <summary>
        /// Возврвщает путь и имя временного файла
        /// </summary>
        /// <param name="pFileExtension">Расширение файла</param>
        /// <returns></returns>
        public string __mFileTemp(string pFileExtension)
        {
            /// Получение уникального имени файла
            string vFileTempName = __mFileUnique(pFileExtension);
            /// Объединение папки для хранения временных файлов с уникальным именем файла 
            return Path.Combine(__fFolderTemp_, appApplication.__fPrefix + "_" + Path.GetFileName(vFileTempName));
        }
        /// <summary>
        /// Возвращает путь и имя файла для размещения настроек форм приложения 
        /// </summary>
        public virtual string __mFileFormTunes()
        {
            return Path.Combine(__fFolderTunes_, "forms.tun");
        }
        /// <summary>
        /// Выполняет удаление временных файлов текущего приложения
        /// </summary>
        public void __mFilesTempDelete()
        {
            /// Получение списка файлов в папке для временных файлов по маске 'префикс_*.*'
            string[] vFileList = Directory.GetFiles(__fFolderTemp_, appApplication.__fPrefix + "_*.*"); // Список файлов созданных приложением во временной папке
            int vFileDeleteCount = 0;
            /// Удаление файлов. Если удаление не получается, то оно пропускается 
            foreach (string vFile in vFileList)
            {
                try
                {
                    File.Delete(vFile);
                }
                catch { }
                vFileDeleteCount++;
            }
            /// Протоколирование количества удаленных файлов
            if (vFileDeleteCount > 0)
            {
                appApplication.__oProtocols.__mRecord(nlApplication.PROTOCOLRECORDSTYPES.Message, appApplication.__oTunes.__mTranslate("Удалено {0} временных файлов", vFileDeleteCount.ToString()), 0);
            }
            return;
        }
        /// <summary>
        /// Возвращает путь и имя файла для размещения настроек форм приложения 
        /// </summary>
        public string __mFileTunes()
        {
            return Path.Combine(__fFolderTunes_, appApplication.__fProcessName_ + ".tun");
        }
        /// <summary>
        /// Возвращает уникальное имя файла
        /// </summary>
        /// <param name="pFileExtension">Расширение временного файла</param>
        public string __mFileUnique(string pFileExtension = "")
        {
            string vReturn = Path.GetRandomFileName();

            if (pFileExtension.Length > 0)
                vReturn = Path.GetFileNameWithoutExtension(vReturn) + Path.GetExtension(vReturn).Substring(1) + "." + pFileExtension;
            else
                vReturn = Path.GetFileNameWithoutExtension(vReturn) + Path.GetExtension(vReturn).Substring(1);

            return vReturn;
        }

        #endregion Файлы

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Путь и имя папки из которой запущено приложение
        /// </summary>
        public string __fFolderStart = Environment.CurrentDirectory;

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>
        /// Полное название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region - Служебные

        /// <summary>
        /// Путь и имя папки для размещения файлов помощи
        /// </summary>
        private string fFolderHelp = "";
        /// <summary>
        /// Путь и имя папки для размещения файлов протоколов
        /// </summary>
        private string fFolderProtocols = "";
        /// <summary>
        /// Путь и имя папки для размещения файлов изображений протоколов
        /// </summary>
        private string fFolderProtocolsImages = "";
        /// <summary>
        /// Путь и имя папки для размещения файлов отчетов
        /// </summary>
        private string fFolderReports = "";
        /// <summary>
        /// Путь и имя папки для временных файлов
        /// </summary>
        private string fFolderTemp = "";
        /// <summary>
        /// Путь и имя папки для файлов настроек
        /// </summary>
        private string fFolderTunes = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        #region Папки

        /// <summary>
        /// Путь и имя папки для размещения файлов помощи
        /// </summary>
        public string __fFolderHelp_
        {
            get
            {
                if (fFolderHelp.Length == 0)
                {
                    fFolderHelp = Path.Combine(__fFolderStart, @"HELP\");
                }
                try
                {
                    if (Directory.Exists(fFolderHelp) == false)
                    {
                        Directory.CreateDirectory(fFolderHelp);
                    }
                }
                catch
                {
                    fFolderHelp = Path.Combine(__fFolderStart, @"HELP\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с файлами помощи");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return fFolderHelp;
            }
            set { fFolderHelp = value.Trim(); }
        }
        /// <summary>
        /// Путь и имя папки для размещения файлов протоколов
        /// </summary>
        public string __fFolderProtocols_
        {
            get
            {
                if (fFolderProtocols.Length == 0)
                {
                    fFolderProtocols = Path.Combine(__fFolderStart, @"PROTOCOLs\");
                }
                try
                {
                    if (Directory.Exists(fFolderProtocols) == false)
                    {
                        Directory.CreateDirectory(fFolderProtocols);
                    }
                }
                catch
                {
                    fFolderProtocols = Path.Combine(__fFolderStart, @"PROTOCOLs\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с файлами протоколов");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return fFolderProtocols;
            }
            set { fFolderProtocols = value.Trim(); }
        }
        /// <summary>
        /// Путь и имя папки для размещения файлов протоколов
        /// </summary>
        public string __fFolderProtocolsImages_
        {
            get
            {
                if (fFolderProtocolsImages.Length == 0)
                {
                    fFolderProtocolsImages = Path.Combine(__fFolderProtocols_, @"IMAGEs\");
                }
                try
                {
                    if (Directory.Exists(fFolderProtocolsImages) == false)
                    {
                        Directory.CreateDirectory(fFolderProtocolsImages);
                    }
                }
                catch
                {
                    fFolderProtocolsImages = Path.Combine(__fFolderProtocols_, @"IMAGEs\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с файлами изображения для протоколов");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return fFolderProtocolsImages;
            }
            set { fFolderProtocolsImages = value.Trim(); }
        }
        /// <summary>
        /// Путь и имя папки для размещения файлов протоколов
        /// </summary>
        public string __fFolderReports_
        {
            get
            {
                if (fFolderReports.Length == 0)
                {
                    fFolderReports = Path.Combine(__fFolderStart, @"REPORTs\");
                }
                try
                {
                    if (Directory.Exists(fFolderReports) == false)
                    {
                        Directory.CreateDirectory(fFolderReports);
                    }
                }
                catch
                {
                    fFolderReports = Path.Combine(__fFolderStart, @"REPORTs\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с файлами отчетов");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return fFolderReports;
            }
            set { fFolderReports = value.Trim(); }
        }
        /// <summary>
        /// Путь и имя папки для размещения временных файлов 
        /// </summary>
        public string __fFolderTemp_
        {
            get
            {
                if (fFolderTemp.Length == 0)
                {
                    fFolderTemp = Path.Combine(__fFolderStart, @"TEMP\");
                }
                try
                {
                    if (Directory.Exists(fFolderTemp) == false)
                    {
                        Directory.CreateDirectory(fFolderTemp);
                    }
                }
                catch
                {
                    fFolderHelp = Path.Combine(__fFolderStart, @"TEMP\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с временными файлами");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return fFolderTemp;
            }
            set { fFolderTemp = value.Trim(); }
        }
        /// <summary>
        /// Путь и имя папки для размещения файлов настроек 
        /// </summary>
        public string __fFolderTunes_
        {
            get
            {
                if (fFolderTunes.Length == 0)
                {
                    fFolderTunes = Path.Combine(__fFolderStart, @"TUNEs\");
                }
                try
                {
                    if (Directory.Exists(fFolderTunes) == false)
                    {
                        Directory.CreateDirectory(fFolderTunes);
                    }
                }
                catch
                {
                    fFolderTunes = Path.Combine(__fFolderStart, @"TUNEs\");
                    appUnitError vError = new appUnitError();
                    vError.__fErrorsType = ERRORSTYPES.User;
                    vError.__fProcedure = _fClassNameFull;
                    vError.__mReasonAdd("Не верный путь в файле настроек");
                    vError.__fMessage_ = appApplication.__oTunes.__mTranslate("Не верно указан путь к папке с файлами настроек");
                    appApplication.__oErrorsHandler.__mProtocol(vError);
                }

                return fFolderTunes;
            }
            set { fFolderTunes = value.Trim(); }
        }

        #endregion Папки

        #region Файлы

        /// <summary>
        /// Путь и имя файла текущего протокола приложения
        /// </summary>
        public string __fFileProtocol_
        {
            get
            {
                DateTime vDateTime = DateTime.Now; // Текущие дата и время
                return Path.Combine(__fFolderProtocols_, appApplication.__fPrefix + "_" + appTypeDateTime.__mDateToFileName(vDateTime) + ".pcl");
            }
        }

        #endregion Файлы

        #endregion СВОЙСТВА
    }
}
