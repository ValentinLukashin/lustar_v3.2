using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appFileIni'
    /// </summary>
    /// <remarks>Объект для работы с 'ini' файлами</remarks>
    public class appFileIni
    {
        #region = БИБЛИОТЕКИ

        [DllImport("kernel32", SetLastError = true)]
        static extern int WritePrivateProfileString(string Sec, string Key, string Val, string FilNam);
        [DllImport("kernel32", SetLastError = true)]
        static extern int WritePrivateProfileString(string section, string key, int value, string fileName);
        [DllImport("kernel32", SetLastError = true)]
        static extern int WritePrivateProfileString(string section, int key, string value, string fileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder result, int size, string fileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, int key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(int section, string key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        static extern int GetPrivateProfileSectionNames(String retVal, int size, string filePath);

        #endregion БИБЛИОТЕКИ

        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// * Конструктор без параметров
        /// </summary>
        public appFileIni()
        {
            _ObjectAssembly();
        }
        /// <summary>
        /// * Конструктор с параметром 'Название обрабатываемого файла'
        /// </summary>
        /// <param name="pFileName">Путь и имя обрабатываемого файла</param>
        public appFileIni(string pFileName)
        {
            _ObjectAssembly();
            __fFilePath = pFileName.Trim();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _ObjectAssembly()
        {
            Type vType = this.GetType();
            _ClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Чтение значения параметра
        /// </summary>
        /// <param name="pSectionName">Имя секции</param>
        /// <param name="pKeyName">Имя параметра</param>
        /// <returns>Значение параметра.</returns>
        public string __mValueRead(string pSectionName, string pKeyName)
        {
            string vReturn = ""; // Возвращаемое значение
            const int cBuffLeng = 0x400;
            StringBuilder lStrgBuil = new StringBuilder(cBuffLeng);

            try
            {
                GetPrivateProfileString(pSectionName, pKeyName, null, lStrgBuil, cBuffLeng, __fFilePath);
                vReturn = lStrgBuil.ToString();
            }
            catch (Exception vException)
            {
                appUnitError vError = new appUnitError();
                vError.__mMessageBuild("Failed to read file");
                vError.__mPropertyAdd("File: {0}", __fFilePath);
                vError.__fException = vException;
                vError.__fProcedure = _ClassNameFull + "_ValueRead(string, string";
                vError.__fErrorsType = ERRORSTYPES.Exception;
                appApplication.__oErrorsHandler.__mShow(vError);
                vReturn = "";
            }

            return vReturn;
        }
        /// <summary>
        /// Чтение значения параметра и создание его в случае отсутствия
        /// </summary>
        /// <param name="pValue">Значение записываемое в случае отстутсвия</param>
        /// <param name="pSectionName">Имя секции</param>
        /// <param name="pKeyName">Имя параметра</param>
        /// <returns>Значение параметра</returns>
        public string __mValueReadWrite(string pValue, string pSectionName, string pKeyName)
        {
            string vReturn = pValue; // Возвращаемое начение
            bool vFind = __mParameterExist(pSectionName, pKeyName); // Параметр найден в файле

            if (vFind == true)
            {
                vReturn = __mValueRead(pSectionName, pKeyName);
            }
            else
            {
                WritePrivateProfileString(pSectionName, pKeyName, pValue, __fFilePath);
            }

            return vReturn;
        }
        /// <summary>
        /// Запись нового значения параметру или создание параметра в случае его отсутствия
        /// </summary>
        /// <param name="pValue">Записываемое значение</param>
        /// <param name="pSectionName">Имя секции</param>
        /// <param name="pKeyName">Имя параметра</param>
        /// <returns>[true] - параметр записан, иначе - [false]</returns>
        public bool __mValueWrite(string pValue, string pSectionName, string pKeyName)
        {
            try
            {
                WritePrivateProfileString(pSectionName, pKeyName, pValue, __fFilePath);
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Удаление параметра в секции
        /// </summary>
        /// <param name="pSectionName">Название секции</param>
        /// <param name="pKeyName">Название параметра</param>
        /// <returns></returns>
        public bool __mParameterDelete(string pSectionName, string pKeyName)
        {
            bool vReturn = false;
            if (WritePrivateProfileString(pSectionName, "", 0, __fFilePath) > 0)
            {
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Получение списка параметров в секции
        /// </summary>
        /// <param name="pSectionName">Название секции</param>
        /// <returns>Список параметров в секции</returns>
        public ArrayList __mParametersList(string pSectionName)
        {
            ArrayList vReturn = new ArrayList(); // Возвращаемое значение

            for (int vMaxSize = 500; true; vMaxSize *= 2)
            {
                byte[] vByte = new byte[vMaxSize];
                int vSize = GetPrivateProfileString(pSectionName, 0, "", vByte, vMaxSize, __fFilePath);
                if (vSize < vMaxSize - 2)
                {
                    string vEnter = Encoding.ASCII.GetString(vByte, 0, vSize - (vSize > 0 ? 1 : 0));
                    if (vEnter != "")
                    {
                        int vWordCoun = appTypeString.__mWordCount(vEnter, '\0');
                        for (int vAmount = 0; vAmount < vWordCoun; vAmount++)
                        {
                            string lWord = appTypeString.__mWordNumber(vEnter, vAmount, '\0');
                            vReturn.Add(lWord);
                        }
                    }
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Получение списка параметров по вхождению маски
        /// </summary>
        /// <param name="pSectionName">Название секции</param>
        /// <param name="pMask">Маска</param>
        /// <returns>Список параметров в секции соответствующих маске</returns>
        public ArrayList __mParametersListByMaskInput(string pSectionName, string pMask)
        {
            ArrayList vReturn = new ArrayList(); // Возвращаемое значение

            for (int vMaxSize = 500; true; vMaxSize *= 2)
            {
                byte[] vByte = new byte[vMaxSize];
                int vSize = GetPrivateProfileString(pSectionName, 0, "", vByte, vMaxSize, __fFilePath);
                if (vSize < vMaxSize - 2)
                {
                    string vEnter = Encoding.ASCII.GetString(vByte, 0, vSize - (vSize > 0 ? 1 : 0));
                    if (vEnter != "")
                    {
                        int vWordCount = appTypeString.__mWordCount(vEnter, '\0');
                        for (int vAmount = 0; vAmount < vWordCount; vAmount++)
                        {
                            string vParameter = appTypeString.__mWordNumber(vEnter, vAmount, '\0'); // Название параметра
                            if (appTypeString.__mMaskFits(vParameter, pMask) == true) /// Провера на соответствие маске
                                vReturn.Add(vParameter);
                        }
                    }
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Получение списка параметров начинающихся с маски
        /// </summary>
        /// <param name="pSectionName">Название секции</param>
        /// <param name="pMask">Маска</param>
        /// <returns>Список параметров в секции соответствующих маске</returns>
        public ArrayList __mParametersListByMaskStart(string pSectionName, string pMask)
        {
            ArrayList vReturn = new ArrayList(); // Возвращаемое значение

            for (int vMaxSize = 500; true; vMaxSize *= 2)
            {
                byte[] vByte = new byte[vMaxSize];
                int vSize = GetPrivateProfileString(pSectionName, 0, "", vByte, vMaxSize, __fFilePath);
                if (vSize < vMaxSize - 2)
                {
                    string vEnter = Encoding.ASCII.GetString(vByte, 0, vSize - (vSize > 0 ? 1 : 0));
                    if (vEnter != "")
                    {
                        int vWordCount = appTypeString.__mWordCount(vEnter, '\0');
                        for (int vAmount = 0; vAmount < vWordCount; vAmount++)
                        {
                            string vParameter = appTypeString.__mWordNumber(vEnter, vAmount, '\0'); // Название параметра
                            if (vParameter.Length >= pMask.Length)
                            {
                                if (vParameter.Substring(0, pMask.Length) == pMask) /// Провера на соответствие маске
                                    vReturn.Add(vParameter);
                            }
                        }
                    }
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Проверка существования парметра в секции
        /// </summary>
        /// <param name="pSectionName">Имя секции</param>
        /// <param name="pKeyName">Имя искомого параметра</param>
        /// <returns>[true] - параметр существует, иначе - [false]</returns>
        public bool __mParameterExist(string pSectionName, string pKeyName)
        {
            bool vReturn = false; // Возвращаемое значение
            ArrayList vKeyList = __mParametersList(pSectionName); // Список параметров в секции

            foreach (string vKey in vKeyList)
            {
                if (vKey.Trim().ToLower() == pKeyName.Trim().ToLower())
                {
                    vReturn = true;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Очистка парметра от значения
        /// </summary>
        /// <param name="pSectionName">Имя секции</param>
        /// <param name="pKeyName">Имя ключа</param>
        /// <returns>[true] - параметр удален, иначе - [false]</returns>
        public bool __mParameterClear(string pSectionName, string pKeyName)
        {
            WritePrivateProfileString(pSectionName, pKeyName, "", __fFilePath);

            return true;
        }
        /// <summary>
        /// Получение списка секций в файле
        /// </summary>
        /// <returns>Список секций в файле</returns>
        public ArrayList __mSectionList()
        {
            ArrayList vReturn = new ArrayList(); // Возвращаемое значение
            string vString = new String('\0', 5000); // Пустая строка
            int vSectionnCount = GetPrivateProfileSectionNames(vString, vString.Length, __fFilePath); // Количество секций в файле
            int vIndexNull = 0;

            vString = vString.Substring(0, vSectionnCount);

            while (vString.Length > 0)
            {
                vIndexNull = vString.IndexOf('\0');
                if (vIndexNull > 0)
                {
                    vReturn.Add(vString.Substring(0, vIndexNull));
                    vString = vString.Substring(vIndexNull + 1);
                }
                else
                {
                    vReturn.Add(vString);
                    vString = "";
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Проверка существования секции в файле
        /// </summary>
        /// <param name="pSectionName">Имя секции</param>
        /// <returns>[true] - секция существует, иначе - [false]</returns>
        public bool __mSectionExists(string pSectionName)
        {
            bool vReturn = false; // Возвращаемое значение
            ArrayList vSectionList = __mSectionList(); // Список секций в файле

            foreach (string vSection in vSectionList)
            {
                if (vSection.Trim().ToUpper() == pSectionName.Trim().ToUpper())
                {
                    vReturn = true;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Удаление секции
        /// </summary>
        /// <param name="pSectionName">Имя секции</param>
        public void __mSectionDelete(string pSectionName)
        {
            WritePrivateProfileString(pSectionName, 0, "", __fFilePath);
        }
        /// <summary>
        /// Чтение списка открытых ранее файлов из INI файла
        /// </summary>
        /// <param name="pSectionName">Название секции</param>
        /// <returns>Список открытых ранее файлов</returns>
        public ArrayList __mOpenedFilesList(string pSectionName)
        {
            ArrayList vFileList = new ArrayList();
            for (int vIntAmount = 1; vIntAmount <= __fOpenedFilesCount; vIntAmount++)
            {
                if (__mParameterExist(pSectionName, "File" + vIntAmount) == true)
                {
                    vFileList.Add(__mValueRead(pSectionName, "File" + vIntAmount));
                }
                else
                {
                    break;
                }
            }

            return vFileList;
        }
        /// <summary>
        /// Добавление последнего открытого файла в список открытых ранее файлов
        /// </summary>
        /// <param name="pSectionName">Название секции в файле</param>
        /// <param name="pFilePath">Добавляемый файл</param>
        public void __mOpenedFileAttach(string pSectionName, string pFilePath)
        {
            ArrayList vFileList = __mOpenedFilesList(pSectionName); // Текущий список открытых ранее файлов (Содержание секции - указанной формы)
            int vFileListCount = vFileList.Count; // Количество файлов в исходном списке
            int vAmount = 1; // Счетчик записанных файлов

            foreach (string vFile in vFileList)
            {
                __mParameterClear(pSectionName, "File" + vAmount.ToString());
                vAmount++;
            } /// Очистка от названий файлов открытых ранее

            vAmount = 1;
            __mValueWrite(pFilePath, pSectionName, "File1"); /// Запись последнего файла

            foreach (string vFile in vFileList)
            {
                if (vFile.Length == 0)
                {
                    //                   vAmount++;
                    continue;
                }
                if (vFile.Trim().ToUpper() == pFilePath.Trim().ToUpper()) /// Файл был открыть чуть ранее
                {
                    vAmount++;
                    continue;
                }
                else
                {
                    vAmount++;
                    __mValueWrite(vFile, pSectionName, "File" + (vAmount).ToString());
                }
                if (vAmount >= __fOpenedFilesCount)
                    break;
            } /// Добавление файлов открытых ранее
        }
        /// <summary>
        /// Отключение последнего открытого файла из списка открытых ранее файлов
        /// </summary>
        /// <param name="pSectionName">Название секции в файле</param>
        /// <param name="pFilePath">Добавляемый файл</param>
        public void __mOpenedFileDetach(string pSectionName, string pFilePath)
        {
            ArrayList vFileList = __mOpenedFilesList(pSectionName); // Текущий список открытых ранее файлов
            int vAmount = 1; // Счетчик записанных файлов

            foreach (string vFile in vFileList) /// Очистка от названий файлов открытых ранее
            {
                __mParameterClear(pSectionName, "File" + vAmount.ToString());
                vAmount++;
            }
            //ArrayList vFileList_New = new ArrayList(); // Новый список открытых ранее файлов
            vAmount = 1;

            //_mValueWrite(pFilePath, pSectionName, "File1"); /// Запись последнего файла
            foreach (string vFile in vFileList) /// Добавление файлов открытых ранее
            {
                if (vFile.Trim().ToUpper() == pFilePath.Trim().ToUpper()) /// Файл был открыть чуть ранее
                {
                    vAmount++;
                    continue;
                }
                else
                {
                    __mValueWrite(vFileList[vAmount - 1].ToString(), pSectionName, "File" + (vAmount + 1).ToString());
                    vAmount++;
                }
                if (vAmount >= __fOpenedFilesCount)
                    break;
            }
        }
        /// <summary>
        /// Выбор первого не пустого значения файла
        /// </summary>
        /// <param name="pSectionName"></param>
        /// <returns></returns>
        public string __mOpenedFileGetFirst(string pSectionName)
        {
            string vReturn = "";
            ArrayList vFileList = __mOpenedFilesList(pSectionName); // Текущий список открытых ранее файлов
            int vAmount = 1; // Счетчик записанных файлов

            foreach (string vFile in vFileList) /// Очистка от названий файлов открытых ранее
            {
                if (vFile.Length > 0)
                {
                    vReturn = vFile.Trim();
                    break;
                }
                vAmount++;
            }
            return vReturn;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Путь и имя инициализационного файла
        /// </summary>
        public string __fFilePath = "";
        /// <summary>
        /// Ограниченное количество открытых ранее файлов из INI файла
        /// </summary>
        public int __fOpenedFilesCount = 10;
        /// <summary>
        /// Полное название класса
        /// </summary>
        protected string _ClassNameFull = "";

        #endregion Атрибуты

        #endregion ПОЛЯ
    }
}
