using System.Diagnostics;
using System.IO;
using System;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appEventsHandler'
    /// </summary>
    /// <remarks>Элемент для обработки основных событий приложений. Наследуется в каждом приложении</remarks>
    public class appEventsHandler 
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public appEventsHandler()
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

        /// <summary>
        /// Начало выполнения приложения
        /// </summary>
        /// <returns>[true] - приложение готово к выполнению, иначе - [false]</returns>
        public virtual bool __mBegin()
        {
            bool vReturn = true; // Возвращаемое значение
            appUnitError vError = new appUnitError(); // Объект ошибки
            vError.__fMessage_ = "Приложение не готово к выполнению";

            appApplication.__oPathes.__mFilesTempDelete(); /// Удаление временных файлов предыдущего сеанса, если они не удалились перед закрытием приложения

            appApplication.__oTunes.__mLoad(); /// Загрузка настроек приложения

            #region Проверка готовности к выполнению 

            /// Проверка готовности приложения к выполнению 

            //            if (appApplication._fDescription.Length == 0) /// Проверка указания краткого описания приложения
            //                vError._mReasonAdd("Не указано краткое описание приложения");

            //            if (appApplication._fPacket.Length == 0) /// Проверка указания пакета приложений
            //                vError._mReasonAdd("Не указан пакет которому принадлежит приложений");

            //            if (appApplication._fOwner.Length == 0) /// Проверка указания владельца приложения
            //                vError._mReasonAdd("Не указан владелец приложения");

            //            if (appApplication._fTradeMark.Length == 0) /// Проверка указания торговой марки приложения
            //                vError._mReasonAdd("Не указан торговая марка приложения");

            //            if (appApplication._fPrefix.Length == 0) /// Проверкаауказания префикса приложения
            //                vError._mReasonAdd("Не указан префикс приложения");
            /// Проверка указания версии создаваемого исполняемого файла
#if DEBUG
            if (Convert.ToInt32(appTypeString.__mWordNumberDot(appApplication.__fVersion_, 0)) == DateTime.Now.Year)
            {
                if (Convert.ToInt32(appTypeString.__mWordNumberDot(appApplication.__fVersion_, 1)) == DateTime.Now.Month)
                {
                    if (Convert.ToInt32(appTypeString.__mWordNumberDot(appApplication.__fVersion_, 2)) != DateTime.Now.Day)
                        vError.__mReasonAdd("Не верно указана версия приложения");
                }
                else
                    vError.__mReasonAdd("Не верно указана версия приложения");
            }
            else
                vError.__mReasonAdd("Не верно указана версия приложения");
#endif
            if (vError.__fReasonS_.Count > 0) /// Отображение ошибки если одна из проверок не пройдена
            {
                appApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }

            #endregion Проверка готовности к выполнению

            /// Протоколирование события приложения - Начало выполнения
            appApplication.__oProtocols.__mCreate(nlApplication.PROTOCOLSTYPES.ApplicationEvent, _fClassNameFull + "_mBegin()"); // Протоколирование начала работы
            appApplication.__oProtocols.__mRecord(nlApplication.PROTOCOLRECORDSTYPES.Message, appApplication.__oTunes.__mTranslate("Начало выполнения приложения"), -1);

            return vReturn;
        }
        /// <summary>
        /// Завершение выполнения приложения
        /// </summary>
        public virtual void __mEnd()
        {
            appApplication.__oPathes.__mFilesTempDelete(); /// Удаление временных файлов текущего сеанса
            appApplication.__oTunes.__mSave(); /// Сохранение настроек приложения
                                               /// Протоколирование события приложения - Завершение выполнения
            appApplication.__oProtocols.__mCreate(nlApplication.PROTOCOLSTYPES.ApplicationEvent, _fClassNameFull + "_mEnd()"); /// Протоколирование завершения работы
            appApplication.__oProtocols.__mRecord(nlApplication.PROTOCOLRECORDSTYPES.Message, appApplication.__oTunes.__mTranslate("Завершение выполнения приложения"), -1);
        }
        /// <summary>
        /// Отображения топика помощи для текущего приложения
        /// </summary>
        /// <param name="pHelpTopicName">Название топика</param>
        public void __mHelp(string pHelpTopicName)
        {
            __mHelp(appApplication.__fFileNameHelp, pHelpTopicName);
        }
        /// <summary>
        /// Вызов топика помощи из указанного файла помощи
        /// </summary>
        /// <param name="pHelpFileName">Путь и имя файла</param>
        /// <param name="pHelpTopicName">Название топика</param>
        public void __mHelp(string pHelpFileName, string pHelpTopicName)
        {
            appUnitError vError = new appUnitError(); // Объект ошибки
            string vHelpFileName = Path.Combine(appApplication.__oPathes.__fFolderHelp_, pHelpFileName); // Полный путь и имя файла

            if (pHelpFileName.Length == 0)
                vError.__mReasonAdd("Файл помощи не определен");

            if (File.Exists(vHelpFileName) == false)
                vError.__mReasonAdd("Файл помощи '{0}' отсутствует", pHelpFileName);

            if (vError.__fReasonS_.Count > 0)
            {
                vError.__mMessageBuild("Не возможно отобразить помощь");
                vError.__mPropertyAdd("Имя файла помощи '{0}'", pHelpFileName);
                appApplication.__oErrorsHandler.__mShow(vError);
                return;
            }

            ProcessStartInfo vProcssInfo;
            if (pHelpTopicName.Length == 0)
                vProcssInfo = new ProcessStartInfo("hh.exe", "mk:@MSITStore:" + vHelpFileName); // Открытие файла помощи
            else
                vProcssInfo = new ProcessStartInfo("hh.exe", "mk:@MSITStore:" + vHelpFileName + "::/" + pHelpTopicName); // Открытие топика помощи
            Process vProcess = new Process();
            vProcssInfo.UseShellExecute = false;
            vProcess.StartInfo = vProcssInfo;
            vProcess.Start();
        }
        /// <summary>
        /// Приостановка работы программы
        /// </summary>
        /// <param name="pSeconds">Время в секундах на которое нужно приостановить работу программы</param>
        public void __mPause(int pSeconds)
        {
            int vUnit = 1000;
            vUnit = vUnit * pSeconds;
            System.Threading.Thread.Sleep(vUnit);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Внутренние

        /// <summary>
        /// Полное название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #endregion ПОЛЯ   
    }
}
