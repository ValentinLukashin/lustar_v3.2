using System.Windows.Forms;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appErrorsHandler'
    /// </summary>
    /// <remarks>Элемент для обработки ошибок приложения</remarks>
    public class appErrorsHandler
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Протоколирование ошибки
        /// </summary>
        /// <param name="pError">Объект ошибки</param>
        /// <authors>Lukashin Valentin</authors>
        /// <example>
        /// appError vError = new appError;
        /// ...
        /// appApplication.__oErrorsHandler.__mProtocols(vError);
        /// </example>
        public void __mProtocol(appUnitError pError)
        {
            nlApplication.PROTOCOLSTYPES vProtocolType = PROTOCOLSTYPES.ApplicationError;
            /// Обработка вида ошибки
            switch (pError.__fErrorsType)
            {
                case ERRORSTYPES.Application:
                    vProtocolType = PROTOCOLSTYPES.ApplicationError;
                    break;
                case ERRORSTYPES.Data:
                    vProtocolType = PROTOCOLSTYPES.DataError;
                    break;
                case ERRORSTYPES.Device:
                    vProtocolType = PROTOCOLSTYPES.DeviceError;
                    break;
                case ERRORSTYPES.Exception:
                    vProtocolType = PROTOCOLSTYPES.ApplicationException;
                    break;
                case ERRORSTYPES.Programming:
                    vProtocolType = PROTOCOLSTYPES.ApplicationErrorProgramatic;
                    break;
                case ERRORSTYPES.User:
                    vProtocolType = PROTOCOLSTYPES.UserError;
                    break;
            }
            /// Создание заголовка протокола
            appApplication.__oProtocols.__mCreate(vProtocolType, pError.__fProcedure);

            #region /// Создание записей в протоколе

            /// Протоколирование сообщения пользователя
            appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Message, pError.__fMessage_, -1);
            /// Протоколирование причин возникновения ошибки
            if (pError.__fReasonS_.Count > 0)
                foreach (string vReason in pError.__fReasonS_)
                    appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Detail, vReason, -1);
            /// Протоколирование сведений об объекте в котором возникла ошибка
            if (pError.__fPropertieS_.Count > 0)
                foreach (string vProperty in pError.__fPropertieS_)
                    appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.ObjectProperty, vProperty, -1);
            /// Протоколирование исключения
            if (pError.__fException != null)
            {
                string vStackTrace = pError.__fException.StackTrace.Trim(); // Свойство исключения 'StackTrace'
                int vIndex = vStackTrace.ToLower().IndexOf(":строка");
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "LineNumber:" + pError.__fException.StackTrace.Trim().Substring(vIndex + 7).Trim(), -1);
                vIndex = vStackTrace.Trim().ToLower().Substring(2).IndexOf("в ");
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "Procedure:" + pError.__fException.StackTrace.Trim().Substring(2, vIndex).Trim(), -1);
                //string vLine = StackTrace.Trim();
                vIndex = vStackTrace.ToLower().IndexOf(" в ");
                vStackTrace = vStackTrace.Trim().Substring(vIndex + 2).Trim(); // vLine + "" + vIndex.ToString(); // 
                vIndex = vStackTrace.ToLower().IndexOf(":строка");
                //return vStackTrace.Trim().Substring(0, vIndex).Trim();
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "File:" + vStackTrace.Trim().Substring(0, vIndex).Trim(), -1);

                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "Message:" + pError.__fMessage_, -1);
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "TargetSite:" + pError.__fException.TargetSite, -1);
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "Source:" + pError.__fException.Source, -1);
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "StackTrace:" + pError.__fException.StackTrace, -1);
                appApplication.__oProtocols.__mRecord(PROTOCOLRECORDSTYPES.Exception, "HelpLink:" + pError.__fException.HelpLink, -1);
            }

            #endregion = Создание записей в протоколе

            return;
        }
        /// <summary>
        /// Отображение сообщения об ошибке пользователю 
        /// </summary>
        /// <param name="pError">Объект ошибки</param>
        public DialogResult __mShow(appUnitError pError)
        {
            /// Протоколирование ошибки
            __mProtocol(pError);
            string vMessage = pError.__fMessage_ + "."; // Текст ошибки
            string vMessageDetails = ""; // Текст деталей ошибки
            /// Добавление причин возникновения ошибок
            if (pError.__fReasonS_.Count > 0)
            {
                vMessageDetails = "\n" + appApplication.__oTunes.__mTranslate("Причины:") + "\n";
                /// Перебор причин возникновения ошибок
                foreach (string pMessageParameter in pError.__fReasonS_)
                    vMessageDetails = vMessageDetails + "    - " + pMessageParameter + ".\n";
            }
            /// Добавление сведений об объекте в котором возникла ошибка
            if (pError.__fPropertieS_.Count > 0)
            {
                vMessageDetails = vMessageDetails + "\n" + appApplication.__oTunes.__mTranslate("Сведения:") + "\n"; // Текст с описанием свойств объекта
                /// Перебор сведений об объекте
                foreach (string pMessageParameter in pError.__fPropertieS_)
                    vMessageDetails = vMessageDetails + "    - " + pMessageParameter + ".\n";
            }
            /// Добавление названия процедуры
            if (pError.__fProcedure.Length > 0)
            {
                vMessageDetails = vMessageDetails + "\n" + appApplication.__oTunes.__mTranslate("Процедура{0} {1}", ":", pError.__fProcedure) + "\n"; // Текст с описанием процедуры в которой возникла ошибка
            }

            return __mShowMessage(MESSAGESTYPES.Error, vMessage, vMessageDetails, pError.__fProcedure); // Возвращаемое значение
        }
        /// <summary>
        /// Вывод на экран сообщения об ошибке. Для возможности замены стандартного окна VStudio
        /// </summary>
        /// <param name="pMessageType">Вид ошибки</param>
        /// <param name="pMessage">Сообщение</param>
        /// <param name="pMessageDetails">Детали сообщения</param>
        /// <param name="pProcedure">Процедура</param>
        /// <returns>Решение пользователя</returns>
        public virtual DialogResult __mShowMessage(MESSAGESTYPES pMessageType, string pMessage, string pMessageDetails, string pProcedure)
        {
            return appApplication.__oMessages.__mShow(MESSAGESTYPES.Error, pMessage, pMessageDetails, pProcedure); // Возвращаемое значение
        }

        #endregion Процедуры

        #endregion МЕТОДЫ
    }
}
