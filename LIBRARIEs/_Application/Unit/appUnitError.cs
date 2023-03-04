using System.Collections;
using System;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appUnitError'
    /// </summary>
    /// <remarks>Элемент - ошибка</remarks>
    public class appUnitError
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Перевод сообщения об ошибке на язык интерфейса приложения и подключение параметров
        /// </summary>
        /// <param name="pMessage">Сообщение об ошибке</param>
        /// <param name="pParameterS">Дополнительные параметры</param>
        public void __mMessageBuild(string pMessage, params object[] pParameterS)
        {
            fMessage = appApplication.__oTunes.__mTranslate(pMessage, pParameterS);

            return;
        }
        /// <summary>
        /// Перевод свойства объекта в котором возникла ошибка на язык интерфейса приложения и подключение параметров
        /// </summary>
        /// <param name="pProperty">Свойство объекта в котором произошла ошибка</param>
        /// <param name="pParametersList">Дополнительные параметры</param>
        public void __mPropertyAdd(string pProperty, params object[] pParametersList)
        {
            fPropertieS.Add(appApplication.__oTunes.__mTranslate(pProperty, pParametersList));

            return;
        }
        /// <summary>
        /// Перевод причины возникновения ошибки на язык интерфейса приложения и подключение параметров
        /// </summary>
        /// <param name="pErrorReason">Детали сообщения об ошибке</param>
        /// <param name="pParameterS">Дополнительные параметры</param>
        public void __mReasonAdd(string pErrorReason, params object[] pParameterS)
        {
            fReasonS.Add(appApplication.__oTunes.__mTranslate(pErrorReason, pParameterS));

            return;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Исключение
        /// </summary>
        public Exception __fException;
        /// <summary>
        /// Путь и имя файла помощи
        /// </summary>
        public string __fHelpFileName = "";
        /// <summary>
        /// Название топика помощи описывающего ошибку
        /// </summary>
        public string __fHelpTopic = "";
        /// <summary>
        /// Вид ошибки
        /// </summary>
        public ERRORSTYPES __fErrorsType = ERRORSTYPES.Application;
        /// <summary>
        /// Название процедуры в которой возникла ошибка
        /// </summary>
        public string __fProcedure = "";

        #endregion Атрибуты

        #region - Служебные

        /// <summary>
        /// Переведенное сообщение об ошибке
        /// </summary>
        private string fMessage = "";
        /// <summary>
        /// Сведения об объекте в котором возникла ошибка  
        /// </summary>
        private ArrayList fPropertieS = new ArrayList();
        /// <summary>
        /// Причины возникновения ошибке
        /// </summary>
        private ArrayList fReasonS = new ArrayList();

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст заголовка
        /// </summary>
        /// <remarks>Перевод сообщения об ошибке на язык интерфейса приложения</remarks>
        public string __fMessage_
        {
            set
            {
                fMessage = appApplication.__oTunes.__mTranslate(value);
            }
        }
        /// <summary>
        /// Имя файла проекта в котором возникла ошибка
        /// </summary>
        public string __fFile_
        {
            get
            {
                if (__fException is Exception)
                {
                    Exception vExceptiond = __fException as Exception;
                    string vLine = vExceptiond.StackTrace.Trim();
                    int vIndex = vLine.ToLower().IndexOf(" в ");
                    vLine = vExceptiond.StackTrace.Trim().Substring(vIndex + 2).Trim(); // vLine + "" + vIndex.ToString(); // 
                    vIndex = vLine.ToLower().IndexOf(":строка");
                    return vLine.Trim().Substring(0, vIndex).Trim();
                }
                else
                {
                    return __fProcedure;
                }
            }
        }
        /// <summary>
        /// Номер строки в которой возникла ошибка
        /// </summary>
        public string __fLineNumber_
        {
            get
            {
                if (__fException is Exception)
                {
                    Exception vExceptiond = __fException as Exception;
                    string vLine = vExceptiond.StackTrace.Trim();
                    int vIndex = vLine.ToLower().IndexOf(":строка");
                    return vExceptiond.StackTrace.Trim().Substring(vIndex + 7).Trim();  // vLine + "" + vIndex.ToString(); //
                }
                else
                {
                    return "-1";
                }
            }
        }
        /// <summary>
        /// Причины возникновения ошибке
        /// </summary>
        /// <remarks>Только для чтения</remarks>
        public ArrayList __fReasonS_
        {
            get { return fReasonS; }
        }
        /// <summary>
        /// Свойства объекта в котором возникла ошибки
        /// </summary>
        /// <remarks>Только для чтения</remarks>
        public ArrayList __fPropertieS_
        {
            get { return fPropertieS; }
        }

        #endregion СВОЙСТВА
    }
}
