using System.Diagnostics;

namespace nlApplication
{
    #region = ПЕРЕЧИСЛЕНИЯ

    /// <summary>
    /// Вид ошибки
    /// </summary>
    public enum ERRORSTYPES
    {
        /// <summary>
        /// Ошибка приложения
        /// </summary>
        Application,
        /// <summary>
        /// Ошибка источника данных
        /// </summary>
        Data,
        /// <summary>
        /// Ошибка устройства
        /// </summary>
        Device,
        /// <summary>
        /// Критическая ошибка
        /// </summary>
        Exception,
        /// <summary>
        /// Ошибка программирования
        /// </summary>
        Programming,
        /// <summary>
        /// Ошибка пользователя
        /// </summary>
        User
    }
    /// <summary>
    /// Виды сообщений
    /// </summary>
    public enum MESSAGESTYPES
    {
        /// <summary>
        /// Ошибка
        /// </summary>
        Error,
        /// <summary>
        /// Ошибка с повтором
        /// </summary>
        ErrorRetry,
        /// <summary>
        /// Информация
        /// </summary>
        Info,
        /// <summary>
        /// Вид не определен
        /// </summary>
        None,
        /// <summary>
        /// Вопрос к пользователю
        /// </summary>
        Question,
        /// <summary>
        /// Предупреждение
        /// </summary>
        Warning
    }
    /// <summary>
    /// Виды протоколов
    /// </summary>
    public enum PROTOCOLSTYPES
    {
        /// <summary>
        /// Ошибка приложения
        /// </summary>
        ApplicationError,
        /// <summary>
        /// Критическая ошибка приложения
        /// </summary>
        ApplicationException,
        /// <summary>
        /// Ошибка программирования
        /// </summary>
        ApplicationErrorProgramatic,
        /// <summary>
        /// Событие приложения
        /// </summary>
        ApplicationEvent,
        /// <summary>
        /// Ошибка источника данных
        /// </summary>
        DataError,
        /// <summary>
        /// Событие источника данных - изменение полей
        /// </summary>
        DataEvent,
        /// <summary>
        /// Ошибка устройства
        /// </summary>
        DeviceError,
        /// <summary>
        /// Событие устройства
        /// </summary>
        DeviceEvent,
        /// <summary>
        /// Ошибка пользователя
        /// </summary>
        UserError,
        /// <summary>
        /// Действия пользователя
        /// </summary>
        UserEvent,
        /// <summary>
        /// Сообщения отображенные пользователю
        /// </summary>
        UserMessage,
        /// <summary>
        /// Прочие события
        /// </summary>
        Other
    }
    /// <summary>
    /// Виды записей в протоколе
    /// </summary>
    public enum PROTOCOLRECORDSTYPES
    {
        /// <summary>
        /// Решение пользователя
        /// </summary>
        Answer,
        /// <summary>
        /// Детали события
        /// </summary>
        Detail,
        /// <summary>
        /// Исключения
        /// </summary>
        Exception,
        /// <summary>
        /// Изображение
        /// </summary>
        Image,
        /// <summary>
        /// Сообщение
        /// </summary>
        Message,
        /// <summary>
        /// Свойство объекта
        /// </summary>
        ObjectProperty
    }

    #endregion ПЕРЕЧИСЛЕНИЯ

    /// <summary>
    /// Класс 'appApplication'
    /// </summary>
    /// <remarks>Элемент - основа приложений, не работающих с данными и не имеющими интерфейса</remarks>
    public class appApplication
    {
        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Заголовок главного окна приложения
        /// </summary>
        /// <remarks>Не переводиться на язык интерфейса</remarks>
        public static string __fCaption = "";
        /// <summary>
        /// Название файла помощи
        /// </summary>
        public static string __fFileNameHelp = "";
        /// <summary>
        /// Название пакета приложений которому принадлежит приложение
        /// </summary>
        public static string __fPacket = "";
        /// <summary>
        /// Префикс приложения
        /// </summary>
        public static string __fPrefix = "";

        #endregion Атрибуты

        #region - Объекты

        /// <summary>
        /// Объект для обработки ошибок приложения
        /// </summary>
        public static appErrorsHandler __oErrorsHandler = new appErrorsHandler();
        /// <summary>
        /// Объект для обработки основных событий приложения
        /// </summary>
        public static appEventsHandler __oEventsHandler = new appEventsHandler();
        /// <summary>
        /// Объект для отображения сообщений пользователю
        /// </summary>
        public static appMessages __oMessages = new appMessages();
        /// <summary>
        /// Объект для работы с путями приложения
        /// </summary>
        public static appPathes __oPathes = new appPathes();
        /// <summary>
        /// Объект протоколирования событий приложения 
        /// </summary>
        public static appProtocols __oProtocols = new appProtocols();
        /// <summary>
        /// Объект для работы с настройками приложения
        /// </summary>
        public static appTunes __oTunes = new appTunes();

        #endregion Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Чтение комментария из 'AssemblyInfo-Comments' 
        /// </summary>
        public static string __fDescription_
        {
            get
            {
                FileVersionInfo vFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
                return vFileVersionInfo.Comments;
            }
        }
        /// <summary>
        /// Чтение владельца из 'AssemblyInfo-Company' 
        /// </summary>
        public static string __fOwner_
        {
            get
            {
                FileVersionInfo vFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
                return vFileVersionInfo.CompanyName;
            }
        }
        /// <summary>
        /// Определение идентификатора текущего процесса
        /// </summary>
        public static int __fProcessClue_
        {
            get { return (int)Process.GetCurrentProcess().Id; }
        }
        /// <summary>
        /// Определение названия процесса выполняемой программы
        /// </summary>
        public static string __fProcessName_
        {
            get
            {
                FileVersionInfo vFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
                return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            }
        }
        /// <summary>
        /// Определение торговой марки из 'AssemblyTrademark'
        /// </summary>
        public static string __fTradeMark_
        {
            get
            {
                FileVersionInfo vFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
                return vFileVersionInfo.LegalTrademarks;
            }
        }
        /// <summary>
        /// Определение версии приложения из 'AssemblyVersion'
        /// </summary>
        public static string __fVersion_
        {
            get
            {
                FileVersionInfo vFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
                return vFileVersionInfo.FileVersion;
            }
        }

        #endregion СВОЙСТВА
    }
}
