using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Windows.Forms;
using System;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appProtocols'
    /// </summary>
    /// <remarks>Элемент для протоколирования событий приложения. Наследуется в приложениях с организацией протоколирования в источник отличный от текстового файла</remarks>
    public class appProtocols
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Создание нового протокола
        /// </summary>
        /// <param name="pProtocolType">Вид записи в протоколе</param>
        /// <param name="pProcedure">Название процедуры в котором возникло событие</param>
        public void __mCreate(PROTOCOLSTYPES pProtocolType, string pProcedure)
        {
            __mCreate(pProtocolType, pProcedure, DateTime.Now);
            return;
        }
        /// <summary>
        /// Создание нового протокола
        /// </summary>
        /// <param name="pProtocolType">Вид записи в протоколе</param>
        /// <param name="pProcedure">Название процедуры в котором возникло событие</param>
        /// <param name="pDateTime">Дата и время возникновения события</param>
        public void __mCreate(PROTOCOLSTYPES pProtocolType, string pProcedure, DateTime pDateTime)
        {
            fFilePath = appApplication.__oPathes.__fFileProtocol_; ///* Формируется имя файла, чтобы при переходе времени за полночь, выполнялась запись в новый файл
            fFilePathPrintScreen = ""; ///* Очистка пути к файлу изображения экрана
            DateTime vDateTime = DateTime.Now;
            int fProtocolType = 0; // Вид протокола

            /// Определение идентификатора вида протокола
            switch (pProtocolType)
            {
                case PROTOCOLSTYPES.ApplicationError:
                    fProtocolType = 1;
                    mPrintScreen();
                    break;
                case PROTOCOLSTYPES.ApplicationErrorProgramatic:
                    fProtocolType = 2;
                    mPrintScreen();
                    break;
                case PROTOCOLSTYPES.ApplicationException:
                    fProtocolType = 3;
                    mPrintScreen();
                    break;
                case PROTOCOLSTYPES.ApplicationEvent:
                    fProtocolType = 4;
                    break;
                case PROTOCOLSTYPES.DataError:
                    fProtocolType = 5;
                    mPrintScreen();
                    break;
                case PROTOCOLSTYPES.DataEvent:
                    fProtocolType = 6;
                    break;
                case PROTOCOLSTYPES.DeviceError:
                    fProtocolType = 7;
                    mPrintScreen();
                    break;
                case PROTOCOLSTYPES.DeviceEvent:
                    fProtocolType = 8;
                    break;
                case PROTOCOLSTYPES.UserError:
                    fProtocolType = 9;
                    mPrintScreen();
                    break;
                case PROTOCOLSTYPES.UserEvent:
                    fProtocolType = 10;
                    break;
                case PROTOCOLSTYPES.UserMessage:
                    fProtocolType = 11;
                    break;
                case PROTOCOLSTYPES.Other:
                    fProtocolType = 12;
                    break;
            }
            /// Задержка времени для создания изображения экрана без формы сообщения, возможно добавить звуковой сигнал
            appApplication.__oEventsHandler.__mPause(2);
            appFileText vFileText = new appFileText(); // Класс для работы с файлом
            vFileText.__mWriteToEnd(fFilePath
                , "[" + vDateTime.Ticks.ToString() + "]" // Время в тиках
                + "[" + vDateTime.ToString() + "]" // Время
                + "[" + appApplication.__fProcessName_ + "]" // Название приложения 
                + "[" + appApplication.__fPrefix + "]" // Префикс приложения
                + "[" + appApplication.__fDescription_ + "]" // Описание приложения
                + "[" + Environment.MachineName + "]" // Хост
                + "[" + Environment.UserName + "]" // Пользователь
                + "[" + fProtocolType.ToString() + "]" // Вид протокола
                + "[" + Path.GetFileName(fFilePathPrintScreen) + "]" // Название файла PrintScreen
                + "[" + pProcedure + "]"); // Название процедуры  

            return;
        }
        /// <summary>
        /// Создание записи в протоколе
        /// </summary>
        /// <param name="pRecordType">Вид записи в протоколе</param>
        /// <param name="pRecordText">Текст записи</param>
        /// <param name="pTick">Количество тиков затраченных на выполнение операции</param>
        public void __mRecord(PROTOCOLRECORDSTYPES pRecordType, string pRecordText, long pTick = -1)
        {
            int fProtocolRecordTypeKey = -1;
            /// Определение идентификаторв вида записи в протоколе
            switch (pRecordType)
            {
                case PROTOCOLRECORDSTYPES.Answer:
                    fProtocolRecordTypeKey = 1;
                    break;
                case PROTOCOLRECORDSTYPES.Detail:
                    fProtocolRecordTypeKey = 2;
                    break;
                case PROTOCOLRECORDSTYPES.Exception:
                    fProtocolRecordTypeKey = 3;
                    break;
                case PROTOCOLRECORDSTYPES.Image:
                    fProtocolRecordTypeKey = 4;
                    break;
                case PROTOCOLRECORDSTYPES.Message:
                    fProtocolRecordTypeKey = 5;
                    break;
                case PROTOCOLRECORDSTYPES.ObjectProperty:
                    fProtocolRecordTypeKey = 6;
                    break;
                default:
                    fProtocolRecordTypeKey = 5;
                    break;
            }

            appFileText vFileText = new appFileText();
            vFileText.__mWriteToEnd(fFilePath
                , "[" + fProtocolRecordTypeKey.ToString() + "]" // Уникальный идентификатор
                + "[" + pTick.ToString().Trim() + "]"
                + " - " + pRecordText.Trim());

            return;
        }
        /// <summary>
        /// Создание файла изображения экрана
        /// </summary>
        private void mPrintScreen()
        {
            Bitmap vBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics vGraphics = Graphics.FromImage(vBitmap as Image);
            vGraphics.CopyFromScreen(0, 0, 0, 0, vBitmap.Size);
            fFilePathPrintScreen = appApplication.__oPathes.__fFolderProtocolsImages_ + appTypeDateTime.__mDateTimeToFileNameTillSecond(DateTime.Now) + ".jpg";
            vBitmap.Save(fFilePathPrintScreen, ImageFormat.Jpeg);

            return;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Служебные

        private string fFilePath = "";
        private string fFilePathPrintScreen = "";

        #endregion Служебные

        #endregion ПОЛЯ
    }
}
