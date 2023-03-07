using System;
using System.IO;
using System.Xml;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appFileXML'
    /// </summary>
    /// <remarks>Класс для работы с XML файлами</remarks>
    public class appFileXML
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Запись протокола в файл
        /// </summary>
        public virtual void __mToFile()
        {
            /// Если приложение будет работать за полночь, чтобы писать в новый файл
            string vFilePath = appApplication.__oPathes.__fFileProtocol_;
            DateTime vDateTime = DateTime.Now;

            XmlDocument vXmlDocument = new XmlDocument(); // Объект для работы с XML документами
            if (File.Exists(vFilePath) == false)
            {
                XmlDeclaration vXmlDeclaration = vXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement vXmlElementRoot = vXmlDocument.DocumentElement;
                vXmlDocument.InsertBefore(vXmlDeclaration, vXmlElementRoot);
                XmlElement vXmlElementProtocol = vXmlDocument.CreateElement(string.Empty, "Protocol", string.Empty);
                vXmlDocument.AppendChild(vXmlElementProtocol);

                vXmlDocument.Save(vFilePath);
            }
            vXmlDocument.Load(vFilePath); /// Открытие файла
            long __ProtocolKey = DateTime.Now.Ticks; // Сохранение ключа протокола
            string vProtocolKey = __ProtocolKey.ToString(); // Строчный идентификатор записи в протоколе

            XmlNode vXmlNodeRecord = vXmlDocument.CreateElement("Protocol"); // Создание записи протокола
            vXmlDocument.DocumentElement.AppendChild(vXmlNodeRecord);

            // Создание аттрибутов
            XmlAttribute vXmlAttributeKey = vXmlDocument.CreateAttribute("cluPcl"); // Аттрибут "Key"
            vXmlAttributeKey.Value = vProtocolKey;
            vXmlNodeRecord.Attributes.Append(vXmlAttributeKey);

            XmlAttribute vXmlAttributeDateTime = vXmlDocument.CreateAttribute("dtmPcl");// Аттрибут "DateTime"
            vXmlAttributeDateTime.Value = DateTime.Now.ToString();
            vXmlNodeRecord.Attributes.Append(vXmlAttributeDateTime);

            XmlAttribute vXmlAttributeApplicationName = vXmlDocument.CreateAttribute("desApl");// Аттрибут "Приложение"
            vXmlAttributeApplicationName.Value = appApplication.__fProcessName_;
            vXmlNodeRecord.Attributes.Append(vXmlAttributeApplicationName);

            XmlAttribute vXmlAttributeApplicationDescription = vXmlDocument.CreateAttribute("dpnApl");// Аттрибут "Краткое описание приложения"
            vXmlAttributeApplicationDescription.Value = appApplication.__fDescription_;
            vXmlNodeRecord.Attributes.Append(vXmlAttributeApplicationDescription);

            XmlAttribute vXmlAttributeHostName = vXmlDocument.CreateAttribute("desHst");// Аттрибут "Хост"
            vXmlAttributeHostName.Value = Environment.MachineName;
            vXmlNodeRecord.Attributes.Append(vXmlAttributeHostName);

            XmlAttribute vXmlAttributeHostLogin = vXmlDocument.CreateAttribute("Lgn");// Аттрибут "Логин хоста"
            vXmlAttributeHostLogin.Value = Environment.UserName;
            vXmlNodeRecord.Attributes.Append(vXmlAttributeHostLogin);

            XmlAttribute vXmlAttributeType = vXmlDocument.CreateAttribute("lnkPclTyp");// Вид протокола
            //vXmlAttributeType.Value = fProtocolTypeClue.ToString();
            vXmlNodeRecord.Attributes.Append(vXmlAttributeType);

            XmlAttribute vXmlAttributePrintScreen = vXmlDocument.CreateAttribute("FilPrnScr");// Путь и имя файла PrintScreen
            //vXmlAttributePrintScreen.Value = __fFilePathPrintScreen;
            vXmlNodeRecord.Attributes.Append(vXmlAttributePrintScreen);

            XmlAttribute vXmlAttributeProcedure = vXmlDocument.CreateAttribute("Prc");// Процедура
            //vXmlAttributeProcedure.Value = __fProcedure;
            vXmlNodeRecord.Attributes.Append(vXmlAttributeProcedure);

            // Создание полей
            XmlNode vXmlNodeFieldLnkPclRrdTyp = vXmlDocument.CreateElement("lnkPclRrdTyp");
            //vXmlNodeFieldLnkPclRrdTyp.InnerText = fProtocolRecordTypeKey.ToString();
            vXmlNodeRecord.AppendChild(vXmlNodeFieldLnkPclRrdTyp);

            XmlNode vXmlNodeFieldMsg = vXmlDocument.CreateElement("Msg");
            //vXmlNodeFieldMsg.InnerText = fProtocolRecordMessage;
            vXmlNodeRecord.AppendChild(vXmlNodeFieldMsg);

            XmlNode vXmlNodeFieldDtmPcl = vXmlDocument.CreateElement("Sec");
            //vXmlNodeFieldDtmPcl.InnerText = fProtocolRecordTick.ToString();
            vXmlNodeRecord.AppendChild(vXmlNodeFieldDtmPcl);
            /// Сохранение документа
            vXmlDocument.Save(vFilePath);
        }

        #endregion Процедуры

        #endregion = МЕТОДЫ
    }
}
