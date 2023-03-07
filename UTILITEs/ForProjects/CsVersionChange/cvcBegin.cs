using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsVersionChange
{
    internal static class cvcBegin
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string vFolderStart = Environment.CurrentDirectory; // Путь и имя папки из которой запущено приложение
            int vFilesChangedCount = 0; // Количество исправленных проектов
            List<cvcFileUnit> vFolderSource = mFilesInFolder(vFolderStart); // Папка с обновлениями

            /// Перебор всех файлов в текущей папке
            foreach (cvcFileUnit vFileUnit in vFolderSource)
            {
                /// Если файл - 'AssemblyInfo.cs'
                if (vFileUnit.__fName == "AssemblyInfo.cs")
                {
                    string vFilePath = Path.Combine(vFileUnit.__fFolder, vFileUnit.__fName); // Путь и имя исправляемого файла
                    StreamReader vStreamReader = new StreamReader(vFilePath); // Поток чтения исправляемого файла
                    StreamWriter vStreamWriter = new StreamWriter(vFilePath + ".out"); // Поток записи создаваемого файла
                    String vLine; // Обрабатываемая строка

                    string vVersionNew = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + ".";


                    while ((vLine = vStreamReader.ReadLine()) != null)
                    {
                        if (vLine.Trim().StartsWith("[assembly: AssemblyVersion(\"") == true)
                        {
                            vLine = vLine.Trim().Substring(0, vLine.Trim().Length - 3);
                            string vVersionOld = (mWordNumber(vLine, 0, '.') + "." + mWordNumber(vLine, 1, '.') + "." + mWordNumber(vLine, 2, '.') + ".").Substring(28);
                            if (vVersionNew == vVersionOld)
                            {
                                int vVersionOldFourSection = Convert.ToInt32(mWordNumber(vLine, 3, '.')) + 1;
                                vVersionNew += vVersionOldFourSection.ToString();
                            }
                            else
                                vVersionNew += "1";

                            vStreamWriter.WriteLine("[assembly: AssemblyVersion(\"" + vVersionNew + "\")]");
                            vStreamWriter.WriteLine("[assembly: AssemblyFileVersion(\"" + vVersionNew + "\")]");
                            break;

                        }
                        else
                            vStreamWriter.WriteLine(vLine);
                    }
                    vStreamReader.Close();
                    vStreamWriter.Close();
                    File.Delete(vFilePath);
                    File.Move(vFilePath + ".out", vFilePath);

                    vFilesChangedCount++;
                }
            }

            MessageBox.Show(String.Format("Исправленные проекты: {0}", vFilesChangedCount), "C# проекты. Исправление версий");
        }
        /// <summary>
        /// Получение списка файлов в папке и во всех вложенных папках
        /// </summary>
        /// <param name="pFolderPath">Сканируемая папка</param>
        /// <returns></returns>
        private static List<cvcFileUnit> mFilesInFolder(string pFolderPath)
        {
            List<cvcFileUnit> vFileInFolderUnit = mGetRecursFiles(pFolderPath);

            return vFileInFolderUnit;
        }
        /// <summary>
        /// Рекусивное чтение файлов
        /// </summary>
        /// <param name="pFolderPath"></param>
        /// <returns></returns>
        private static List<cvcFileUnit> mGetRecursFiles(string pFolderPath)
        {
            List<cvcFileUnit> ls = new List<cvcFileUnit>();
            try
            {
                string[] folders = Directory.GetDirectories(pFolderPath);
                foreach (string folder in folders)
                {
                    cvcFileUnit vFileFolderUnit = new cvcFileUnit();
                    vFileFolderUnit.__fFolder = pFolderPath;

                    ls.AddRange(mGetRecursFiles(folder));
                }
                string[] files = Directory.GetFiles(pFolderPath);
                foreach (string filename in files)
                {
                    FileInfo vFileInfo = new FileInfo(filename);

                    cvcFileUnit vFileFolderUnit = new cvcFileUnit();
                    vFileFolderUnit.__fName = Path.GetFileName(filename);
                    vFileFolderUnit.__fFolder = pFolderPath;
                    vFileFolderUnit.__fDateTimeCreate = File.GetCreationTime(filename);
                    vFileFolderUnit.__fDateTimeUpdate = File.GetLastWriteTime(filename);
                    vFileFolderUnit.__fSize = vFileInfo.Length;
                    ls.Add(vFileFolderUnit);
                }
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }

            return ls;
        }
        /// <summary>
        /// Поиск слова по номеру в выражении, разделенных указанным символьным разделителем
        /// </summary>
        /// <param name="pExpression">Выражение в котром ведеться поиск</param>
        /// <param name="pWordPosition">Номер искомого слова</param>
        /// <param name="pSeparator">Символ разделителя</param>
        /// <returns>Слово</returns>
        private static string mWordNumber(string pExpression, int pWordPosition, char pSeparator)
        {
            string vReturn = ""; // Возвращаемое значение
            int vWordCount = mWordCount(pExpression, pSeparator); // Количество слов в выражении
            if (pWordPosition == -1)
                return "";
            if (vWordCount < pWordPosition + 1)
                return "";
            int vIndexPosition = 0; // Индекс последней буквы искомого слова в выражении

            if (vWordCount == 1 & vWordCount == pWordPosition)
            { /// Разделители не обнаружены
                return "";
            }
            for (int vAmount = 0; vAmount <= vWordCount; vAmount++)
            {
                vIndexPosition = pExpression.IndexOf(pSeparator);
                if (vIndexPosition > 0)
                {
                    if (vAmount == pWordPosition)
                    {
                        vReturn = pExpression.Substring(0, vIndexPosition);
                        break;
                    }
                }
                else
                {
                    vReturn = pExpression;
                }
                pExpression = pExpression.Substring(vIndexPosition + 1);
            }
            vReturn = vReturn.Trim();

            return vReturn;
        }
        /// <summary>
        /// Получение количества слов в выражении, разделенных указанным символом
        /// </summary>
        /// <param name="pExpression">Строчное выражение</param>
        /// <param name="pSeparator">Символ - разделитель</param>
        /// <returns>Количество слов</returns>
        private static int mWordCount(string pExpression, char pSeparator)
        {
            /// Объявляется счетчик слов в выражении со значением 0
            int vReturn = 0; // Возвращаемое значение
            int vPosition = 0; // Номер позиции символа разделителя слов

            if (pExpression.Length > 0)
            {
                /// 1: Перебор символов в выражении
                while (true)
                {
                    ///2: Если символ из выражения равен второму параметру увеличивается счетчик слов выражении
                    vPosition = pExpression.IndexOf(pSeparator, 0);
                    if (vPosition == 0)
                    {
                        pExpression = pExpression.Substring(1);
                        vReturn = vReturn + 1;
                    }
                    if (vPosition > 0)
                    {
                        pExpression = pExpression.Substring(vPosition + 1);
                        if (pExpression.Length != 0)
                        {
                            vReturn = vReturn + 1;
                        }
                    }
                    if (vPosition < 0)
                    {
                        vReturn = vReturn + 1;
                        break;
                    }
                }
            }
            /// Возвращается значение счетчика слов в выражении
            return vReturn;
        }
    }

    /// <summary>
    /// Класс 'vchFileUnit'
    /// </summary>
    internal class cvcFileUnit
    {
        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Название файла
        /// </summary>
        public string __fName = "";
        /// <summary>
        /// Папка размещения файла
        /// </summary>
        public string __fFolder = "";
        /// <summary>
        /// Время создания файла
        /// </summary>
        public DateTime __fDateTimeCreate = DateTime.Now;
        /// <summary>
        /// Время последней записи в файл
        /// </summary>
        public DateTime __fDateTimeUpdate = DateTime.Now;
        /// <summary>
        /// Размер файла
        /// </summary>
        public long __fSize = -1;

        #endregion Атрибуты

        #endregion ПОЛЯ
    }

}
