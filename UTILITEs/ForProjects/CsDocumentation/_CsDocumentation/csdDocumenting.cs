using nlApplication;
using nlReports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace nlCsDocumentation
{
    /// <summary>
    /// Класс 'csdDocumenting'
    /// </summary>
    /// <remarks>Документирование проекстов C#</remarks>
    public class csdDocumenting
    {
        private string fClassNameFull = "";

        /// <summary> 
        /// Опреление цвета ключевых слов
        /// </summary>
        /// <param name="pKeyWord">Ключевое слово</param>
        /// <returns>Ключевое слово окруженное HTML тегами</returns>
        private string mKeyWord(string pKeyWord)
        {
            string vReturn = "";

            switch (pKeyWord.ToLower())
            {
                /// Области видимости
                case "public":
                    vReturn = "<Font Color=\"#0066FF\"><B>public</B></Font>";
                    break;
                case "private":
                    vReturn = "<Font Color=\"#0066FF\"><B>private</B></Font>";
                    break;
                case "internal":
                    vReturn = "<Font Color=\"#0066FF\"><B>internal</B></Font>";
                    break;
                case "protected":
                    vReturn = "<Font Color=\"#0066FF\"><B>protected</B></Font>";
                    break;
                /// Порядок использования
                case "abstract":
                    vReturn = "<Font Color=\"#7766FF\"><B>static</B></Font>";
                    break;
                case "event":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>event</I></B></Font>";
                    break;
                case "static":
                    vReturn = "<Font Color=\"#7766FF\"><B>static</B></Font>";
                    break;
                /// Наследственность
                case "virtual":
                    vReturn = "<Font Color=\"#4455FF\"><B>virtual</B></Font>";
                    break;
                case "override":
                    vReturn = "<Font Color=\"#4455FF\"><B>override</B></Font>";
                    break;
                /// Типы данных        
                case "arraylist":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>ArrayList</I></B></Font>";
                    break;
                case "bool":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>bool</I></B></Font>";
                    break;
                case "class":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>class</I></B></Font>";
                    break;
                case "datetime":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>datetime</I></B></Font>";
                    break;
                case "dialogresult":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>DialogResult</I></B></Font>";
                    break;
                case "enum":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>enum</I></B></Font>";
                    break;
                case "eventhandler":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>EventHandler</I></B></Font>";
                    break;
                case "int":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>int</I></B></Font>";
                    break;
                case "object":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>object</I></B></Font>";
                    break;
                case "string":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>string</I></B></Font>";
                    break;
                case "void":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>void</I></B></Font>";
                    break;
                case "xmlnode":
                    vReturn = "<Font Color=\"#0066FF\"><B><I>XmlNode</I></B></Font>";
                    break;
                /// 
                case "params":
                    vReturn = "<Font Color=\"#0022FF\"><B><I>params</I></B></Font>";
                    break;

                default:

                    vReturn = pKeyWord;
                    break;
            }

            return vReturn;
        }
        /// <summary>
        /// Форматирование строки в HTML формате
        /// </summary>
        /// <param name="pLine">Содержание строки</param>
        /// <remarks>Строка окруженная HTML тэгами</remarks>
        private string mLineColoring(string pLine)
        {
            string vReturn = ""; // Возвращаемое значение
            /// Перебор слов в строке и обработка их методом ' mKeyWord(string)'

            foreach (string vWord in appTypeString.__mWordsList(pLine.Trim(), ' '))
            {
                vReturn = vReturn + mKeyWord(vWord) + " ";
            }

            return vReturn;
        }
        /// <summary>
        /// Получение названия атрибута из объявления
        /// </summary>
        /// <param name="pLine"></param>
        /// <returns></returns>
        private string mGetName(string pLine)
        {
            string vReturn = ""; // Возвращаемое значение
            /// Перебор слов в строке и обработка их методом ' mKeyWord(string)'
            foreach (string vWord in appTypeString.__mWordsList(pLine.Trim(), ' '))
            {
                int vWordLenght = vWord.Trim().Length;
                if (mKeyWord(vWord).Length == vWordLenght)
                {
                    vReturn = vWord;
                    break;
                }
            }

            return vReturn;
        }

        /// <summary>
        /// Документирование проекта
        /// </summary>
        /// <param name="pFolderProject">Путь и имя папки проекта</param>
        /// <returns></returns>
        public int __mDocumentingProject(string pFolderProject)
        {
            #region /// Объявление переменных

            int vReturn = 0; // Возвращаемое значение
            string vFolderPathDocumentation = Path.Combine(pFolderProject, "_Documentation"); // Путь и имя папки для размещения файла документации
            string vFilePathDocumentation = Path.Combine(vFolderPathDocumentation, Path.ChangeExtension(Path.GetFileNameWithoutExtension(pFolderProject), "html")); // Название файла документации
            string vFileNameSource = ""; // Название файла класса

            const string sTab = "&nbsp;&nbsp;";
            const string sGroupAttributesFontSize = "FS=4";
            const string sGroupAttributesTextColor = "CT=FF4444";
            const string sSubGroupAttributesFontSize = "FS=4";
            const string sSubGroupAttributesTextColor = "CT=4444FF";
            const string sSubGroupAttributesLineSize = "S=3";
            const string sSubGroupAttributesColor = "C=FF4444";

            List<docIndex> vIndexFileContent = new List<docIndex>(); // Содержание индексного файла

            CLASSATTRIBUTEsGROUPs vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Class; // Главная группа аттрибутов класса
            CLASSATTRIBUTEsPROPERTIEs vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body; // Свойство обрабатываемого аттрибута класса
            /// Создание списка файлов классов в папке проекта с учетом вложенности папок
            string[] vFilesPathSource = Directory.GetFiles(pFolderProject, "*.cs", SearchOption.AllDirectories);
            string vName = ""; // Название класса или атрибута
            rrtReport vReport = new rrtReport(); // Объект для формирования файлов отчетов 
            ArrayList vNameSpaces = new ArrayList(); // Список пространств имен

            string vClassAuthors = "";
            string vClassName = "";
            string vClassDescription = "";
            string vClassNameSpace = "";
            ArrayList vClassNameSpaces = new ArrayList();

            string vMethodAuthors = "";
            string vMethodComments = "";
            string vMethodExamples = "";
            string vMethodName = "";

            ArrayList vAuthorS = new ArrayList(); // Список авторов
            ArrayList vSummarieS = new ArrayList(); // Список резюме 
            ArrayList vRemarkS = new ArrayList(); // Список замечаний 
            ArrayList vReturnS = new ArrayList(); // Возвращаемое значение
            ArrayList vCommentS = new ArrayList(); // Список комментариев мметода
            ArrayList vExampleS = new ArrayList(); // Пример
            List<docParameter> vParameterS = new List<docParameter>(); // Параметры
            List<csdEnum> vEnumS = new List<csdEnum>(); // Перечисления

            #endregion Объявление переменных

            /// Проверка папки проекта
            if (Directory.Exists(pFolderProject) == false)
            {
                appUnitError vError = new appUnitError();
                vError.__fProcedure = fClassNameFull + "__mDocumentingProject";
                // Дописать обработку ошибки
                goto Exit;
            }

            /// Создание папки для размещения описаний классов, если она отсутствует
            if (Directory.Exists(vFolderPathDocumentation) == false) Directory.CreateDirectory(vFolderPathDocumentation);

            #region /// Удаление предыдущей документации

            appFileSystem oFile = new appFileSystem();
            List<appUnitFile> vFileOldS = oFile.__mFilesInFolder(vFolderPathDocumentation);
            foreach (appUnitFile vFileUnit in vFileOldS)
            {
                File.Delete(Path.Combine(vFileUnit.__fFolder, vFileUnit.__fName));
            }

            #endregion Удаление предыдущей документации

            #region /// Перебор файлов в папке проекта

            foreach (string vFilePathSource in vFilesPathSource)
            {
                vFilePathDocumentation = Path.Combine(vFolderPathDocumentation, Path.ChangeExtension(Path.GetFileNameWithoutExtension(vFilePathSource), "html")); // Название файла документации

                #region /// Создание отчета документации проекта

                vReport.__mCreate(); // Создание файла документации
                vReport.__fBackColor = "FFEEDD"; // Цвет фона отчета
                vReport.__fFilePath = vFilePathDocumentation; // Путь и имя файла отчета
                vReport.__fColumnsCountInReport = 2; // Количество колонок
                vReport.__fTitle = Path.GetFileNameWithoutExtension(pFolderProject); // Заголовок файла отчета

                vReport.__mRow();
                vReport.__mCell("Вернуться к списку", "FS=2", "L=index.html", "SC=max");

                #endregion Создание отчета документации проекта

                vFileNameSource = Path.GetFileName(vFilePathSource); // Название файла класса

                vClassNameSpaces = new ArrayList(); // Очистка списка подключенных пространств имен
                vClassAuthors = "";
                vMethodAuthors = "";

                #region /// Пропускаемые файлы

                /// Пропукаются следующие файлы:
                /// - файлы с расширением 'net'
                if (vFileNameSource.StartsWith(".NET"))
                    continue;
                /// - файлы с расширением 'ass'
                if (vFileNameSource.StartsWith("Ass"))
                    continue;

                #endregion Пропускаемые файлы

                #region /// Заполнение переменных

                vFileNameSource = Path.GetFileName(vFilePathSource); // Название файла класса
                vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Class; // Сброс групп атрибутов класса
                vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body; // Сброс свойств атрибутов класса
                string[] vFileLines = File.ReadAllLines(vFilePathSource); // Массив строк файла

                #endregion Заполнение переменных

                #region /// Перебор строк в файле проекта

                foreach (string vFileLine in vFileLines)
                {
                    string ver = vFileLine; /// Временно для отображения содержания переменной при тестировании класса

                    #region /// Пропускаемые строки

                    /// 1 - Завершающие HTML тэги
                    if (vFileLine.Trim().StartsWith("/// </") == true) { vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body; continue; } // Сброс текущего свойства аттрибута - строки окончания названия '/// </summary>'
                    /// 1 - Пустые строки
                    if (vFileLine.Trim().Length == 0) continue;
                    /// 1 - Символы '{' и '}'
                    if (vFileLine.Trim() == "{" | vFileLine.Trim() == "}") continue;
                    /// 1 - Закоментированные строки'
                    if (vFileLine.Trim().StartsWith("// ") == true) continue;
                    /// 1 - Пустые строки завершения региона
                    if (vFileLine.Trim().StartsWith("#end") == true) continue;
                    /// 1 - Cтроки блоков отладки
                    if (vFileLine.Trim().StartsWith("#if DEBUG") == true | vFileLine.Trim().StartsWith("#endif")) continue;

                    #endregion Пропускаемые строки

                    /// Запись подключенных пространств имен
                    if (vFileLine.Trim().StartsWith("using") == true)
                    {
                        vClassNameSpaces.Add(mLineColoring(vFileLine));
                        continue;
                    }
                    /// Запись пространства имен текущего класса
                    if (vFileLine.Trim().StartsWith("namespace") == true)
                    {
                        vClassNameSpace = vFileLine.Trim().Substring(9);
                        continue;
                    }

                    /// Начало обработки класса
                    {
                        if (vFileLine.Trim().StartsWith("class") == true)
                        {
                            vClassName = appTypeString.__mWordNumberSpace(vFileLine.Trim(), 1);
                            vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Class;
                            vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body;
                        }
                        if (vFileLine.Trim().StartsWith("abstract class") == true |
                            vFileLine.Trim().StartsWith("sealed class") == true)
                        {
                            vClassName = appTypeString.__mWordNumberSpace(vFileLine.Trim(), 2);
                            vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Class;
                            vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body;
                        }
                        if (vFileLine.Trim().StartsWith("public class") == true |
                            vFileLine.Trim().StartsWith("internal class") == true)
                        {
                            vClassName = appTypeString.__mWordNumberSpace(vFileLine.Trim(), 2);
                            vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Class;
                            vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body;
                        }
                        if (vFileLine.Trim().StartsWith("public abstract class") == true |
                            vFileLine.Trim().StartsWith("internal abstract class") == true |
                            vFileLine.Trim().StartsWith("public sealed class") == true |
                            vFileLine.Trim().StartsWith("internal sealed class") == true)
                        {
                            vClassName = appTypeString.__mWordNumberSpace(vFileLine.Trim(), 3);
                            vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Class;
                            vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body;
                        }
                    }

                    #region /// Определение текущей группы атрибутов класса

                    if (vFileLine.Trim().StartsWith("#region =") == true)
                    {
                        vReport.__mRow();
                        vReport.__mCell(vFileLine.Trim().Substring(9).Trim(), "A=c", "SC=max", sGroupAttributesTextColor, sGroupAttributesFontSize, "FB");
                        vReport.__mLine(sSubGroupAttributesLineSize, sSubGroupAttributesColor);
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = БИБЛИОТЕКИ") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Libraries;
                        continue;
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = ДИЗАЙНЕРЫ") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Designs;
                        continue;
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = МЕТОДЫ") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Methods;
                        continue;
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = ПЕРЕЧИСЛЕНИЯ") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Enums;
                        vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Summary;
                        continue;
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = ПОЛЯ") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Fields;
                        continue;
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = СОБЫТИЯ") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Events;
                        continue;
                    }
                    if (vFileLine.Trim().ToUpper().StartsWith("#REGION = СВОЙСТВА") == true)
                    {
                        vClassAttributesGroup = CLASSATTRIBUTEsGROUPs.Properties;
                        continue;
                    }

                    #endregion Определение текущей группы атрибутов класса

                    #region /// Опредление подгрупп атрибутов

                    if (vFileLine.Trim().StartsWith("#region -") == true)
                    {
                        vReport.__mRow();
                        vReport.__mCell(vFileLine.Trim().Substring(9).Trim(), "A=c", "SC=max", sSubGroupAttributesTextColor, sSubGroupAttributesFontSize, "FB");
                        vReport.__mLine(sSubGroupAttributesLineSize, sSubGroupAttributesColor);
                        continue;
                    }
                    if (vFileLine.Trim().StartsWith("#region ") == true & vFileLine.Trim().StartsWith("#region ///") == false)
                    {
                        vReport.__mRow();
                        vReport.__mCell(vFileLine.Trim().Substring(7).Trim(), "A=l", "SC=max", sSubGroupAttributesTextColor, sSubGroupAttributesFontSize, "FB");
                        vReport.__mLine(sSubGroupAttributesLineSize, "C=4444FF");
                        continue;
                    }

                    #endregion Опредление подгрупп атрибутов

                    #region /// Определение обрабатываемого свойства атрибута класса

                    /// Если начинается свойство атрибута 'Author'
                    if (vFileLine.Trim().StartsWith("/// <authors>") == true)
                    {
                        vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Author;
                    }
                    /// Если начинается свойство атрибута 'Example'
                    if (vFileLine.Trim().StartsWith("/// <example>") == true) vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Example;
                    /// Если начинается свойство атрибута 'Param'
                    if (vFileLine.Trim().StartsWith("/// <param") == true) vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Param;
                    /// Если начинается свойство атрибута 'Remarks'
                    if (vFileLine.Trim().StartsWith("/// <remarks>") == true) vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Remark;
                    /// Если начинается свойство атрибута 'Return'
                    if (vFileLine.Trim().StartsWith("/// <returns>") == true) vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Return;
                    /// Если начинается свойство атрибута 'summary'
                    if (vFileLine.Trim().StartsWith("/// <summary>") == true)
                    {
                        vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Summary; /// Запись атрибуту свойства 'summary'
                    }
                    if (vFileLine.Trim().StartsWith("public") == true |
                        vFileLine.Trim().StartsWith("private") == true |
                        vFileLine.Trim().StartsWith("protected") == true |
                        vFileLine.Trim().StartsWith("internal") == true)
                    {
                        vClassAttributesProperty = CLASSATTRIBUTEsPROPERTIEs.Body;
                    }

                    #endregion Определение обрабатываемого свойства атрибута класса

                    #region /// Обработка строк файла класса

                    /// Обработка класса
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Class)
                    {
                        /// 1. Заполнение списка авторов класса
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Author)
                        {
                        }
                        /// 2. Запись данных по классу в файл документации
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                            vReport.__mRowEmpty();
                            vReport.__mRow();
                            vReport.__mCell("Класс:  " + vClassName + "<BR>" + vClassDescription, "A=c", "SC=max", "FS=5", "CT=880000");
                            vReport.__mLine("S=5", "C=880000");
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Example)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                            vClassDescription = vFileLine.Trim().Substring(13).Trim();
                            if (String.IsNullOrEmpty(vClassDescription) == false)
                                vClassDescription = vClassDescription.Substring(0, vClassDescription.IndexOf("<"));
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Return)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                        }
                    }
                    /// Обработка дизайнеров
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Designs)
                    {
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Example)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                        }
                    }
                    /// Обработка событий
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Enums)
                    {
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                            if (vFileLine.Trim().Contains(" enum ") == true)
                            {
                                vEnumS[vEnumS.Count - 1].__fName = vFileLine.Trim().Substring(vFileLine.Trim().IndexOf("enum") + 4).Trim();
                                vEnumS[vEnumS.Count - 1].__fTitle = true;
                            }
                            else
                            {
                                vEnumS[vEnumS.Count - 1].__fName = appTypeString.__mSymbolsLastDelete(vFileLine, ",");
                                //   vEnumS[vEnumS.Count - 1].__fTitle = true;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                            csdEnum vEnum = new csdEnum();

                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <summary>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vEnum.__fDescription = vFileLine.Trim().Substring(13, vIndexCloseTag - 13);
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vEnum.__fDescription = vFileLine.Trim().Substring(13);

                                if (vEnum.__fDescription.Length > 0)
                                    vEnumS.Add(vEnum);

                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vEnum.__fDescription = vFileLine.Trim().Substring(3, vIndexCloseTag - 3);
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vEnum.__fDescription = vFileLine.Trim().Substring(3);

                                vEnumS.Add(vEnum);

                                continue;
                            }
                        }
                    }
                    /// Обработка событий
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Events)
                    {
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Example)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                        }
                    }
                    /// Обработка полей
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Fields)
                    {
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                            string vFieldDescription = ""; // Описание поля
                            int vRecordNumber = 1;
                            foreach (string vSummary in vSummarieS)
                            {
                                if (vRecordNumber == 1)
                                    vFieldDescription += vSummary;
                                else
                                    vFieldDescription += "<BR>" + vSummary;
                                vRecordNumber++;
                            }
                            vFieldDescription += "<Font color='#999999'>"; // Смена цвета для примечания
                            foreach (string vRemark in vRemarkS)
                            {
                                if (vRecordNumber == 1)
                                    vFieldDescription += vRemark;
                                else
                                    vFieldDescription += "<BR>" + vRemark;
                                vRecordNumber++;
                            }
                            vFieldDescription += "</Font>"; // Закрытие тэга смены цвета для примечания

                            vSummarieS = new ArrayList();
                            vRemarkS = new ArrayList();

                            /// Добавление поля в отчет
                            string vFieldName = "";
                            if (vFileLine.IndexOf("=") > 0)
                                vFieldName = vFileLine.Trim().Substring(0, vFileLine.Trim().IndexOf("=") - 1);
                            else
                                vFieldName = vFileLine.Trim();

                            vReport.__mRow();
                            vReport.__mCell(mLineColoring(vFieldName), "A=l", "VA=t");
                            vReport.__mCell(vFieldDescription);

                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <remarks>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <summary>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                    }
                    /// Обработка методов
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Methods)
                    {
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Author)
                        {
                            /// Автор указан в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <authors>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vAuthorS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vAuthorS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vAuthorS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vAuthorS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                            /// 1: Определение названия метода
                            if (vMethodName.Length == 0)
                            {
                                vMethodName = vFileLine.Trim();
                            }
                            /// ---
                            if (vFileLine.Trim().StartsWith("#region ///") == true)
                            {
                                vCommentS.Add(vFileLine.Trim().Substring(11).Trim());
                                continue;
                            }
                            /// ---
                            if (vFileLine.Trim().StartsWith("return") == false)
                            {
                                if (vFileLine.Trim().StartsWith("///") == true)
                                {
                                    string vWordZero = appTypeString.__mWordNumber(vFileLine.Trim(), 0, ":");
                                    if (vWordZero.Length < 7)
                                    {
                                        string vNest = appTypeString.__mWordNumberSpace(vWordZero, 1); // Вложенность комментария
                                        int vNumber = Convert.ToInt32(vNest);
                                        vCommentS.Add("<Font face = 'Courier New'>" + appTypeString.__mWordReplicate(vNumber, sTab) + "</Font>" + vFileLine.Trim().Substring(vFileLine.Trim().IndexOf(":") + 1).Trim());
                                    }
                                    /// Комментарий верхнего уровня 
                                    else
                                    {
                                        vCommentS.Add(vFileLine.Trim().Substring(3).Trim());
                                    }
                                    continue;
                                }
                            }
                            else
                            {
                                string vMethodDescription = ""; // Описание поля
                                int vRecordNumber = 1;
                                foreach (string vSummary in vSummarieS)
                                {
                                    if (vRecordNumber == 1)
                                        vMethodDescription += vSummary;
                                    else
                                        vMethodDescription += "<BR>" + vSummary;
                                    vRecordNumber++;
                                }
                                vMethodDescription += "<Font color='#999999'>"; // Смена цвета для примечания
                                foreach (string vRemark in vRemarkS)
                                {
                                    if (vRecordNumber == 1)
                                        vMethodDescription += vRemark;
                                    else
                                        vMethodDescription += "<BR>" + vRemark;
                                    vRecordNumber++;
                                }
                                vMethodDescription += "</Font>"; // Закрытие тэга смены цвета для примечания

                                vMethodComments += "<Font color='#770000'>"; // Смена цвета для примечания
                                foreach (string vComment in vCommentS)
                                {
                                    if (vRecordNumber == 1)
                                        vMethodComments += vComment;
                                    else
                                        vMethodComments += "<BR>" + vComment;
                                    vRecordNumber++;
                                }
                                vMethodComments += "</Font>"; // Закрытие тэга смены цвета для примечания

                                vMethodAuthors += "<Font color='#770000'>"; // Смена цвета для примечания
                                vRecordNumber = 1;
                                foreach (string vAuthor in vAuthorS)
                                {
                                    if (vRecordNumber == 1)
                                        vMethodAuthors += vAuthor;
                                    else
                                        vMethodAuthors += "<BR>" + vAuthor;
                                    vRecordNumber++;
                                }
                                vMethodComments += "</Font>"; // Закрытие тэга смены цвета для примечания


                                vMethodExamples += "<Font color='#770033'>"; // Смена цвета для примеров
                                vRecordNumber = 1;
                                foreach (string vExample in vExampleS)
                                {
                                    if (vRecordNumber == 1)
                                        vMethodExamples += vExample;
                                    else
                                        vMethodExamples += "<BR>" + vExample;
                                    vRecordNumber++;
                                }
                                vMethodExamples += "</Font>"; // Закрытие тэга смены цвета для примечания

                                vReport.__mRow();
                                vReport.__mCell(mLineColoring(vMethodName), "A=l", "VA=t");
                                vReport.__mCell(vMethodDescription);
                                /// 2: Вывод параметров
                                if (vParameterS.Count > 0)
                                {
                                    vReport.__mRow();
                                    vReport.__mCell("Параметры:", "SC=max", "CT=000077", "FB");
                                    foreach (docParameter vParameter in vParameterS)
                                    {
                                        vReport.__mRow();
                                        vReport.__mCell(vParameter.__fName, "CT=770033");
                                        vReport.__mCell(vParameter.__fDescription);
                                    }
                                }
                                /// 2: Вывод возвращаемого значения
                                if (vReturnS.Count > 0)
                                {
                                    foreach (string vReturnValue in vReturnS)
                                    {
                                        vReport.__mRow();
                                        vReport.__mCell("<Font color='#000077'><B>Возвращаемое значение: </B></Font>" + mLineColoring(vReturnValue), "A=l", "VA=t", "SC= max");
                                    }
                                }
                                /// 2: Вывод авторов
                                if (vAuthorS.Count > 0)
                                {
                                    vReport.__mRow();
                                    vReport.__mCell("<Font color='#000077'><B>Авторы: </B></Font>" + vMethodAuthors, "A=l", "VA=t", "SC= max");
                                }
                                /// 2: Вывод комментариев в коде
                                if (vCommentS.Count > 0)
                                {
                                    vReport.__mRow();
                                    vReport.__mCell("<Font color='#000077'><B>Комментарии кода:</B></Font>" + vMethodComments, "A=l", "VA=t", "SC= max");
                                }
                                /// 2: Вывод примеров использования
                                if (vExampleS.Count > 0)
                                {
                                    vReport.__mRow();
                                    vReport.__mCell("<Font color='#000077'><B>Примеры:</B></Font><BR>" + vMethodExamples, "A=l", "VA=t", "SC= max");
                                }

                                vReport.__mRow();
                                vReport.__mLine();

                                vMethodAuthors = "";
                                vMethodDescription = "";
                                vMethodExamples = "";
                                vMethodName = "";
                                vMethodComments = "";
                                vAuthorS = new ArrayList();
                                vCommentS = new ArrayList();
                                vExampleS = new ArrayList();
                                vParameterS = new List<docParameter>();
                                vRemarkS = new ArrayList();
                                vReturnS = new ArrayList();
                                vSummarieS = new ArrayList();
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Example)
                        {
                            /// Пример описан в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <example>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vExampleS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vExampleS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vExampleS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vExampleS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Param)
                        {
                            /// Пример описан в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <param name=") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия параметра
                                if (vIndexCloseTag > 0)
                                {
                                    docParameter vParameter = new docParameter();
                                    vParameter.__fName = appTypeString.__mWordNumber(vFileLine, 1, '"').Trim();
                                    vParameter.__fDescription = appTypeString.__mWordNumber(vFileLine, 1, '>').Trim();
                                    vParameterS.Add(vParameter);
                                }
                                else
                                {
                                    if (vIndexCloseTag > 0)
                                    {
                                        docParameter vParameter = new docParameter();
                                        vParameter.__fName = appTypeString.__mWordNumber(vFileLine, 1, '"').Trim();
                                        vParameter.__fDescription = appTypeString.__mWordNumber(vFileLine, 1, '>').Trim();
                                        vParameterS.Add(vParameter);
                                    }
                                }
                                //int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия параметра
                                //if (vIndexCloseTag > 0)
                                //    vParameterS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                //else
                                //    if (vFileLine.Trim().Substring(13).Length > 0)
                                //    vParameterS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                docParameter vParameter = new docParameter();
                                vParameter.__fName = appTypeString.__mWordNumber(vFileLine, 1, '"').Trim();
                                vParameter.__fDescription = appTypeString.__mWordNumber(vFileLine, 1, '>').Trim();
                                vParameterS.Add(vParameter);

                                //int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия параметра
                                //if (vIndexCloseTag > 0)
                                //    vParameterS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                //else
                                //    if (vFileLine.Trim().Substring(3).Length > 0)
                                //    vParameterS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <remarks>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Return)
                        {
                            /// Результат описан в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <returns>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия возвращаемого значения
                                if (vIndexCloseTag > 0)
                                    vReturnS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vReturnS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия возвращаемого значения
                                if (vIndexCloseTag > 0)
                                    vReturnS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vReturnS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <summary>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                    }
                    /// Обработка полей
                    if (vClassAttributesGroup == CLASSATTRIBUTEsGROUPs.Properties)
                    {
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Body)
                        {
                            if (vSummarieS.Count == 0 & vRemarkS.Count == 0)
                                continue;
                            string vPropertyDescription = ""; // Описание поля
                            int vRecordNumber = 1;
                            foreach (string vSummary in vSummarieS)
                            {
                                if (vRecordNumber == 1)
                                    vPropertyDescription += vSummary;
                                else
                                    vPropertyDescription += "<BR>" + vSummary;
                                vRecordNumber++;
                            }
                            vPropertyDescription += "<Font color='#999999'>"; // Смена цвета для примечания
                            foreach (string vRemark in vRemarkS)
                            {
                                if (vRecordNumber == 1)
                                    vPropertyDescription += vRemark;
                                else
                                    vPropertyDescription += "<BR>" + vRemark;
                                vRecordNumber++;
                            }
                            vPropertyDescription += "</Font>";  // Закрытие тэга смены цвета для примечания

                            vSummarieS = new ArrayList();
                            vRemarkS = new ArrayList();

                            /// Добавление поля в отчет
                            string vFieldName = "";
                            if (vFileLine.IndexOf("=") > 0)
                                vFieldName = vFileLine.Trim().Substring(0, vFileLine.Trim().IndexOf("=") - 1);
                            else
                                vFieldName = vFileLine.Trim();

                            vReport.__mRow();
                            vReport.__mCell(mLineColoring(vFieldName), "A=l", "VA=t");
                            vReport.__mCell(vPropertyDescription);

                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Remark)
                        {
                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <remarks>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vRemarkS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                        if (vClassAttributesProperty == CLASSATTRIBUTEsPROPERTIEs.Summary)
                        {
                            /// Резюме описано в первой строке описания свойства
                            if (vFileLine.Trim().StartsWith("/// <summary>") == true)
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(13, vIndexCloseTag - 13));
                                else
                                    if (vFileLine.Trim().Substring(13).Length > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(13));
                                continue;
                            }
                            /// Обработка следующей строки описания свойства
                            if (vFileLine.Trim().StartsWith("///"))
                            {
                                int vIndexCloseTag = vFileLine.Trim().IndexOf("</"); // Индекс закрытия свойства
                                if (vIndexCloseTag > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(3, vIndexCloseTag - 3));
                                else
                                    if (vFileLine.Trim().Substring(3).Length > 0)
                                    vSummarieS.Add(vFileLine.Trim().Substring(3));
                                continue;
                            }
                        }
                    }

                    #endregion Обработка строк файла класса
                }

                #endregion Перебор строк в файле проекта

                /// Если файл существует, то он удаляется
                if (File.Exists(vFilePathDocumentation) == true)
                {
                    File.Delete(vFilePathDocumentation);
                }
                /// Вывод в файл документации проекта 
                docIndex vIndex = new docIndex();
                vIndex.__fFileName = Path.ChangeExtension(Path.GetFileName(vFileNameSource), ".html");
                vIndex.__fClassDescription = vClassDescription;

                vIndexFileContent.Add(vIndex);
                vReport.__mFile();

            }

            #endregion Перебор файлов в папке проекта

            #region /// Создание индексного файла

            string vFilePathIndex = Path.Combine(Path.GetDirectoryName(vFilePathDocumentation), "index.html");

            if (File.Exists(vFilePathIndex) == true)
            {
                File.Delete(vFilePathIndex);
            }

            vReport.__mCreate(); // Создание файла документации
            vReport.__fBackColor = "FFEEDD"; // Цвет фона отчета
            vReport.__fFilePath = vFilePathIndex; // Путь и имя файла отчета
            vReport.__fColumnsCountInReport = 2; // Количество колонок
            vReport.__fTitle = csdApplication.__oTunes.__mTranslate("Ссылки на документирование классов"); // Заголовок файла отчета

            vReport.__mRow();
            vReport.__mCell(csdApplication.__oTunes.__mTranslate("Ссылки на документирование классов"), "A=C", "FB", "FS=4", "SC=max");
            vReport.__mRowEmpty();

            vReport.__mRow();
            vReport.__mCell(csdApplication.__oTunes.__mTranslate("Перечисления"), "SC=max");
            vReport.__mCell("enums.html", "L=enums.html", "SC=max");
            vReport.__mRowEmpty();

            foreach (docIndex vIndex in vIndexFileContent)
            {
                vReport.__mRow();
                vReport.__mCell(vIndex.__fClassDescription, "L=" + vIndex.__fFileName, "SC=max");
                vReport.__mCell(vIndex.__fFileName, "L=" + vIndex.__fFileName, "SC=max");
            }

            vReport.__mFile();

            #endregion Создание индексного файла

            #region /// Создание файла перечислений

            string vFilePathEnums = Path.Combine(Path.GetDirectoryName(vFilePathDocumentation), "enums.html");

            if (File.Exists(vFilePathEnums) == true)
            {
                File.Delete(vFilePathEnums);
            }

            vReport.__mCreate(); // Создание файла документации
            vReport.__fBackColor = "FFEEDD"; // Цвет фона отчета
            vReport.__fFilePath = vFilePathEnums; // Путь и имя файла отчета
            vReport.__fColumnsCountInReport = 2; // Количество колонок
            vReport.__fTitle = csdApplication.__oTunes.__mTranslate("Перечисления используемые в классах классов"); // Заголовок файла отчета

            vReport.__mRow();
            vReport.__mCell("Вернуться к списку", "FS=2", "L=index.html", "SC=max");
            vReport.__mRowEmpty();

            vReport.__mRow();
            vReport.__mCell(csdApplication.__oTunes.__mTranslate("Перечисления используемые в классах классов"), "A=C", "FB", "FS=4", "SC=max");

            foreach (csdEnum vEnum in vEnumS)
            {
                if (vEnum.__fTitle == true)
                {
                    vReport.__mRowEmpty();
                    vReport.__mRow();
                    vReport.__mCell(vEnum.__fName.Trim() + " - " + vEnum.__fDescription.Trim(), "FB", "SC=max", "CT=#888888");
                }
                else
                {
                    vReport.__mRow();
                    vReport.__mCell(vEnum.__fName.Trim(), "SC=max");
                    vReport.__mCell(vEnum.__fDescription.Trim(), "SC=max");
                }
            }

            vReport.__mFile();

        #endregion Создание файла перечислений

        Exit: return vReturn;
        }

        #region = ПОЛЯ

        /// <summary>
        /// Включатель размещения документации всех классов в одном проекте
        /// </summary>
        //public bool __fOneFile = true;
        /// <summary>
        /// Включатель обработки заголовков второго уровня
        /// </summary>
        public bool __fHeader2 = false;
        /// <summary>
        /// Включатель обработки заголовков четвертого уровня
        /// </summary>
        public bool __fHeader4 = false;

        /// <summary>
        /// Первод каретки
        /// </summary>
        private const string CRLF = "\r\n";

        #endregion ПОЛЯ
    }
    public class docParameter
    {
        public string __fName = "";
        public string __fDescription = "";
    }
    public class docIndex
    {
        // http://htmlbook.ru/content/ssylki-vnutri-stranitsy

        #region = ПОЛЯ

        public string __fFileName = "";
        public string __fClassDescription = "";
        //string __fClassLink = "";

        #endregion ПОЛЯ
    }

    public class csdEnum
    {
        public string __fName = "";
        public string __fDescription = "";
        public bool __fTitle = false;
    }
}
