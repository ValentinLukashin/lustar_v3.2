using nlApplication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace emlEmail
{
    /// <summary>
    /// 
    /// </summary>
    /// <see cref="MailMessage Class=> https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.mailmessage?view=net-7.0"/>
    /// <seealso cref="Поля To, CC и BCC в e-mail=>  https://www.yaklass.ru/p/informatika/5-klass/peredacha-informatcii-13630/peredacha-informatcii-elektronnaia-pochta-12392/re-17768d3e-a095-4984-b591-a5289772f172"/>
    public class emlMessageSmtp
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Добавление вложения
        /// </summary>
        /// <param name="pPath">Путь к вложению</param>
        /// <returns>[true] - вложение принято, иначе - [false]</returns>
        public bool __mAttachmentAdd(string pPath)
        {
            /// Добавить определение папка или файл, выделить  относительный путь'
            fAttamentS.Add(pPath);

            return true;
        }
        /// <summary>
        /// Очистка класса от данных
        /// </summary>
        public void __mClear()
        {
            fAttamentS = new ArrayList();
            fBody = "";
            fBodyEncoding = Encoding.Default;
            fBcc = new MailAddressCollection();
            fCc = new MailAddressCollection();
            fFromAddress = "";
            fFromName = "";
            fHeaderS = new SortedDictionary<string, string>();
            fHeadersEncoding = Encoding.Default;
            fTo = new MailAddressCollection();
            fReplyToList = new MailAddressCollection();
            fSubject = "";
            fPriority = MailPriority.Normal;
            fIsBodyHtml = false;
        }
        /// <summary>
        /// Добавление заголовка 
        /// </summary>
        /// <param name="pHeader">Заголовок, пара название, значение через знак '='</param>
        /// <returns>[true] - заголовок принят, иначе - [false]</returns>
        public bool __mHeaderAdd(string pHeader)
        {
            bool vReturn = false; // Возвращаемое значение
            /// Проверка заполненности заголовока
            if (String.IsNullOrEmpty(pHeader) == false)
            {
                /// Проверка формата заголовка
                int vSeparatorPosition = pHeader.IndexOf('='); /// Позиция разделителя выражений [ = ]
                if (vSeparatorPosition > 0) /// Разделитель обнаружен
                {
                    try
                    {
                        fHeaderS.Add(pHeader.Substring(0, vSeparatorPosition).Trim(), pHeader.Substring(vSeparatorPosition + 1).Trim());
                        vReturn = true;
                    }
                    /// Ошибки при записи заголовка пишутся в протол
                    catch (Exception vException)
                    {
                        appUnitError vError = new appUnitError();
                        vError.__fException = vException;
                    }
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Добавление адреса скрытого получателя копии сообщения
        /// </summary>
        /// <returns></returns>
        public bool __mRecipientBlindCarbonCopiesAdd(string pAddress)
        {
            return false;
        }
        /// <summary>
        /// Добавление адреса получателя копии сообщения
        /// </summary>
        /// <returns></returns>
        public bool __mRecipientCarbonCopiesAdd(string pAddress)
        {
            return false;
        }
        public bool __mRecipientAdd(string pAddress)
        {
            return false;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Служебные 

        /// <summary>
        /// Список вложений
        /// </summary>
        private ArrayList fAttamentS = new ArrayList();
        /// <summary>
        /// Тело сообщения
        /// </summary>
        private string fBody = "";
        /// <summary>
        /// Кодировка тела сообщения 
        /// </summary>
        private Encoding fBodyEncoding = Encoding.Default;
        /// <summary>
        /// Получатели скрытой копии сообщения
        /// </summary>
        private MailAddressCollection fBcc = new MailAddressCollection();
        /// <summary>
        /// Получатели копии сообщения
        /// </summary>
        private MailAddressCollection fCc = new MailAddressCollection();
        /// <summary>
        /// Адрес отправителя
        /// </summary>
        private string fFromAddress = "";
        /// <summary>
        /// Имя отправителя
        /// </summary>
        private string fFromName = "";
        /// <summary>
        /// Адрес главного получателя
        /// </summary>
        private string fToAddress = "";
        /// <summary>
        /// Имя главного получателя
        /// </summary>
        private string fToName = "";
        /// <summary>
        /// Список заголовков
        /// </summary>
        private SortedDictionary<string, string> fHeaderS = new SortedDictionary<string, string>();
        /// <summary>
        /// Кодировка заголовоков сообщения
        /// </summary>
        private Encoding fHeadersEncoding = Encoding.Default;
        /// <summary>
        /// Список адресов получателя
        /// </summary>
        private MailAddressCollection fTo = new MailAddressCollection();
        /// <summary>
        /// Список адресов для получения ответа
        /// </summary>
        private MailAddressCollection fReplyToList = new MailAddressCollection();
        /// <summary>
        /// Тема сообщения
        /// </summary>
        private string fSubject = "";
        /// <summary>
        /// Приоритет сообщения
        /// </summary>
        private MailPriority fPriority = MailPriority.Normal;
        /// <summary>
        /// Тело сообщения в формате 'HTML'
        /// </summary>
        private bool fIsBodyHtml = false;

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string __fBody_
        {
            get { return fBody; }
            set { fBody = value; }
        }
        /// <summary>
        /// Почтовое сообщение
        /// </summary>
        public MailMessage __fMailMessage_
        {
            get
            {
                MailAddress vFrom = new MailAddress(fFromAddress, fFromName);
                /// Создание сообщения
                MailMessage vMailMesage = new MailMessage(fToAddress, fToName);
                /// Добавление получателей сообщения
                foreach (MailAddress vMailAddress in fTo)
                {
                    vMailMesage.To.Add(vMailAddress);
                }
                /// Добавление получателей копии сообщения
                foreach (MailAddress vMailAddress in fCc)
                {
                    vMailMesage.CC.Add(vMailAddress);
                }
                /// Добавление получателей скрытой копии сообщения
                foreach (MailAddress vMailAddress in fBcc)
                {
                    vMailMesage.Bcc.Add(vMailAddress);
                }
                /// Добавление получателей ответа сообщения
                foreach (MailAddress vMailAddress in fReplyToList)
                {
                    vMailMesage.ReplyToList.Add(vMailAddress);
                }
                /// Добавление вложений
                foreach (string vAttachmentPath in fAttamentS)
                {
                    Attachment vAttachments = new Attachment(vAttachmentPath, MediaTypeNames.Application.Octet);
                    vMailMesage.Attachments.Add(vAttachments);
                }
                /// Добавление заголовоков в сообщение
                foreach (string vKey in fHeaderS.Keys)
                {
                    string vValue = "";
                    fHeaderS.TryGetValue(vKey, out vValue);
                    vMailMesage.Headers.Add(vKey.Trim(), vValue.Trim());
                }

                vMailMesage.Body = fBody;
                vMailMesage.BodyEncoding = fBodyEncoding;
                vMailMesage.HeadersEncoding = fHeadersEncoding;
                vMailMesage.IsBodyHtml = fIsBodyHtml;
                vMailMesage.Priority = fPriority;
                vMailMesage.Subject = fSubject;

                return vMailMesage;
            }
            set { MailMessage vMailMassage = value; }
        }
        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string __fSubject_
        {
            get { return fSubject; }
            set { fSubject = value; }
        }

        #endregion СВОЙСТВА
    }
}
