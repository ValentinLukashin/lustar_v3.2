using nlApplication;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace emlEmail
{
    /// <summary>
    /// 
    /// </summary>
    /// <see cref=">https://metanit.com/sharp/net/8.1.php"/>
    /// <seealso cref=">https://metanit.com/sharp/net/8.1.php"/>
    /// <seealso cref="Исключения=> https://learn.microsoft.com/ru-ru/dotnet/api/system.net.mail.smtpclient.sendmailasync?view=net-7.0"/>
    public class emlSmtp
    {
        #region = МЕТОДЫ

        #region - Процедуры

        public bool __mSendMessage(emlMessageSmtp pMessageSmtp, bool pAsynchronous)
        {
            return false;
        }
        private bool mSendMessage(emlMessageSmtp pMessageSmtp)
        {
            SmtpClient vSmtpClient = new SmtpClient(fServer, fPort);
            vSmtpClient.Credentials = new NetworkCredential(fLogin, fPassword);
            vSmtpClient.EnableSsl = true;
            vSmtpClient.Send(pMessageSmtp.__fMailMessage_);

            return false;
        }
        private async Task mSendMessageAsync(emlMessageSmtp pMessageSmtp)
        {
            SmtpClient vSmtpClient = new SmtpClient(fServer, fPort);
            vSmtpClient.Credentials = new NetworkCredential(fLogin, fPassword);
            vSmtpClient.EnableSsl = true;
            await vSmtpClient.SendMailAsync(pMessageSmtp.__fMailMessage_);
        }
        /// <summary>
        /// Формирование сообщений из файла или папки
        /// </summary>
        /// <param name="pPath">Путь к файлу или папке</param>
        /// <returns></returns>
        public emlMessageSmtp __mFromPath(string pPath)
        {
            appFileSystem vFileSystem = new appFileSystem(); // Объект для работы с файловой системой
            FILESYSTEMTYPEs vResult = vFileSystem.__mFileSystemType(pPath); // Вид элемента файловой системы
            /// Если получен файл
            if (vResult == FILESYSTEMTYPEs.File)
            {
                return __mFromFile(pPath);
            }
            /// Если получена папка
            if (vResult == FILESYSTEMTYPEs.Folder)
            {
                List<appUnitFile> vFiles = vFileSystem.__mFilesInFolder(pPath);
                foreach (appUnitFile vFile in vFiles)
                {
                    return __mFromFile(pPath);
                }
            }

            return null;
        }
        /// <summary>
        /// Загрузка сообщения из файла
        /// </summary>
        /// <param name="pFilePath"></param>
        private emlMessageSmtp __mFromFile(string pFilePath)
        {
            appFileIni vFileIni = new appFileIni(); // Объект для работы с инициализационными файлами
            emlMessageSmtp vMessageSmtp = new emlMessageSmtp();

            vFileIni.__fFilePath = pFilePath;
            vMessageSmtp.__fBody_ = vFileIni.__mValueRead("Message", "Body");
            vMessageSmtp.__fSubject_ = vFileIni.__mValueRead("Message", "Subject");

            ArrayList vAttachmentS = vFileIni.__mParametersList("ATTACHMENTs");
            foreach (string vAttachment in vAttachmentS)
            {
                vMessageSmtp.__mAttachmentAdd(vAttachment);
            }
            ArrayList vRecipientS = vFileIni.__mParametersList("RECIPIENTs");
            foreach (string vRecipient in vRecipientS)
            {
                vMessageSmtp.__mRecipientAdd(vRecipient);
            }
            ArrayList vRecipientBccS = vFileIni.__mParametersList("RECIPIENTsBCC");
            foreach (string vRecipientBcc in vRecipientBccS)
            {
                vMessageSmtp.__mRecipientBlindCarbonCopiesAdd(vRecipientBcc);
            }
            ArrayList vRecipientCcS = vFileIni.__mParametersList("RECIPIENTsCC");
            foreach (string vRecipientCc in vRecipientCcS)
            {
                vMessageSmtp.__mRecipientCarbonCopiesAdd(vRecipientCc);
            }
            ArrayList vHeaderS = vFileIni.__mParametersList("HEADERs");
            foreach (string vHeader in vHeaderS)
            {
                vMessageSmtp.__mHeaderAdd(vHeader);
            }

            return vMessageSmtp;
        }
        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Служебные

        /// <summary>
        /// Адрес Smtp сервера
        /// </summary>
        private string fServer = "";
        /// <summary>
        /// Порт Smtp сервера
        /// </summary>
        private int fPort = 25;
        /// <summary>
        /// Адрес почты отправителя
        /// </summary>
        private string fLogin = "";
        /// <summary>
        /// Пароль почты отправителя сообщения
        /// </summary>
        private string fPassword = "";

        #endregion Служебные

        #endregion ПОЛЯ
    }
}
