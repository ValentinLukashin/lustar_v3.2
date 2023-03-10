using System;
using System.IO;
using System.Text;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appFileText'
    /// </summary>
    /// <remarks>Элемент для работы с текстовыми файлами</remarks>
    public sealed class appFileText
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public appFileText()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ 

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        private void _mObjectAssembly()
        {
            Type vType = this.GetType();
            fClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Создание файла из строчного массива
        /// </summary>
        /// <remarks>Если массив строк пустой файл будет удален</remarks>
        /// <param name="pFilePath">Путь и имя файла</param>
        /// <param name="pFileLines">Массив строк файла</param>
        /// <returns>[true] - Файл создан, иначе - [false]</returns>
        public bool __mCreateFromArray(string pFilePath, string[] pFileLines)
        {
            bool vReturn = true; // Возвращаемое значение

            File.Delete(pFilePath); /// Удаление файла

            foreach (string vLine in pFileLines)
            {
                if (vLine != null)
                    __mWriteToEnd(pFilePath, vLine);
            }

            return vReturn;
        }
        /// <summary>
        /// * Поиск выражения в файле
        /// </summary>
        /// <param name="pFilePath">Путь и имя проверяемого файла</param>
        /// <param name="pSearchedExpression">Искомое строчное выражение</param>
        /// <returns>[true] - выражение найдено, иначе - [false]</returns>
        public bool _mSearchExpression(string pFilePath, string pSearchedExpression)
        {
            bool vReturn = false;
            /// > Если файл не существует, возвращается [false]. 
            if (File.Exists(pFilePath) == false)
                return false;

            StreamReader vStreamReader = new StreamReader(pFilePath, Encoding.Default);
            while (!vStreamReader.EndOfStream)
            {
                string vLine = vStreamReader.ReadLine();
                if (appTypeString.__mExpressionInExpression(vLine, pSearchedExpression) >= 0)
                {
                    vReturn = true;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// * Запись строки в конец файла
        /// </summary>
        /// <remarks>Строка добавляется в файл с новой строки</remarks>
        /// <param name="pFilePath">Путь и имя файла в который идет запись</param>
        /// <param name="pString">Записываемая строка</param>
        /// <returns>[true] - запись добавлена, иначе - [false]</returns>
        public bool __mWriteToEnd(string pFilePath, string pString)
        {
            FileStream vFileStream;

            try
            {
                vFileStream = new FileStream(pFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            catch (Exception vException)
            {
                appUnitError vError = new appUnitError();
                vError.__mMessageBuild("Не возможно создать файл");
                vError.__mPropertyAdd("Файл: {0}", pFilePath);
                vError.__fException = vException;
                vError.__fProcedure = fClassNameFull + "_WriteToEnd(string, string)";
                vError.__fErrorsType = ERRORSTYPES.Exception;
                appApplication.__oErrorsHandler.__mShow(vError);

                return false;
            }
            if (vFileStream != null)
            {
                string vLineContent = pString + Environment.NewLine;
                int vLineLength = vLineContent.Length;
                byte[] vByteLine = new byte[vLineLength];
                vFileStream.Seek(0, SeekOrigin.End);
                vFileStream.Write(Encoding.Default.GetBytes(vLineContent), 0, vLineLength);
                vFileStream.Close();
            }

            return true;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// * Полное название класса.
        /// </summary>
        private string fClassNameFull = "";

        #endregion Внутренние

        #endregion ПОЛЯ
    }
}
