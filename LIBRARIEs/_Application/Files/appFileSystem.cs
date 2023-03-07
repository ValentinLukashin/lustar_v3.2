using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace nlApplication
{
    // https://docs.microsoft.com/ru-ru/dotnet/standard/security/walkthrough-creating-a-cryptographic-application Создание ассимитричного ключа
    // https://www.codeproject.com/Articles/1278566/Simple-AES-Encryption-using-Csharp

    /// <summary>
    /// Класс 'appFile'
    /// </summary>
    /// <remarks>Объект для работы с файлами</remarks>
    public class appFileSystem
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public appFileSystem()
        {
            /// Вызывается метод '_mObjectAssembly'
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ 

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected void _mObjectAssembly()
        {
            /// Фиксируется полный путь к создаваемому объекту
            Type vType = this.GetType();
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        #region Файлы в папках

        /// <summary>
        /// Получение списка файлов в папке и во всех вложенных папках
        /// </summary>
        /// <remarks>Метод выполняется с рекурсивной обработкой</remarks>
        /// <param name="pFolderPath">Сканируемая папка</param>
        /// <example>List&lt;appFileUnit&gt; vList = __mFilesInFolder(@'D:\Temp')</example>
        /// <returns>List<T></returns>
        public List<appUnitFile> __mFilesInFolder(string pFolderPath)
        {
            List<appUnitFile> vFileInFolderUnit = mGetRecursiveFiles(pFolderPath);

            return vFileInFolderUnit;
        }
        /// <summary>
        /// Рекурсивный поиск файлов в папке
        /// </summary>
        /// <param name="pFolderPath">Путь и имя папки</param>
        /// <returns>Список файлов в полученной папке</returns>
        private List<appUnitFile> mGetRecursiveFiles(string pFolderPath)
        {
            List<appUnitFile> vResult = new List<appUnitFile>(); // Возвращаемое значение
            try
            {
                string[] folders = Directory.GetDirectories(pFolderPath);
                foreach (string folder in folders)
                {
                    appUnitFile vFileFolderUnit = new appUnitFile();
                    vFileFolderUnit.__fFolder = pFolderPath;

                    vResult.AddRange(mGetRecursiveFiles(folder));
                }
                string[] files = Directory.GetFiles(pFolderPath);
                foreach (string filename in files)
                {
                    FileInfo vFileInfo = new FileInfo(filename);

                    appUnitFile vFileFolderUnit = new appUnitFile();
                    vFileFolderUnit.__fName = Path.GetFileName(filename);
                    vFileFolderUnit.__fFolder = pFolderPath;
                    vFileFolderUnit.__fDateTimeCreate = File.GetCreationTime(filename);
                    vFileFolderUnit.__fDateTimeWrite = File.GetLastWriteTime(filename);
                    vFileFolderUnit.__fSize = vFileInfo.Length;
                    if (Path.GetExtension(filename) == ".exe" | Path.GetExtension(filename) == ".dll")
                    {
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(filename);
                        vFileFolderUnit.__fVersion = myFileVersionInfo.FileVersion;
                    }
                    vResult.Add(vFileFolderUnit);
                }
            }
            catch (Exception vException)
            {
                //MessageBox.Show(e.Message);
            }

            return vResult;
        }

        #endregion Файлы в папках

        #region Размер

        /// <summary>
        /// Вычисление размера файла
        /// </summary>
        /// <param name="pFilePath">Путь и имя файла</param>
        /// <returns>Строчный эквивалент размера файла</returns>
        public string __mFileStringSize(string pFilePath)
        {
            string vReturn = "-1"; // Возвращаемое значение
            /// Если файл отсутствует, формируется сообщение об ошибке.
            if (File.Exists(pFilePath) == false)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__fProcedure = _fClassNameFull + "__mFileStringSize(string)";
                vError.__mPropertyAdd("Путь к файлу: {0}", pFilePath);
                vError.__mReasonAdd("Полученный файл отсутствует");
                vError.__fMessage_ = "Не возможно измерить размер файла";
                appApplication.__oErrorsHandler.__mShow(vError);
                /// 1 Выводиться ошибка пользователю и метод завершает работу.
                return vReturn;
            }
            else
            {
                FileInfo vFileInfo = new FileInfo(pFilePath);
                long vSize = vFileInfo.Length;
                string[] vList = { "bt", "KB", "MB", "GB", "TB", "PB", "EB" };
                if (vSize == 0)
                    vReturn = "0" + vList[0];
                else
                {
                    long vLong = Math.Abs(vSize);
                    int vPlace = Convert.ToInt32(Math.Floor(Math.Log(vLong, 1024)));
                    double vNumber = Math.Round(vLong / Math.Pow(1024, vPlace), 1);
                    vReturn = (Math.Sign(vSize) * vNumber).ToString() + vList[vPlace];
                }
            }

            return vReturn;
        }

        #endregion Размер

        #region Работа с описаниями

        /// <summary>
        /// Удаление описания файла
        /// </summary>
        /// <param name="pFilePath">Путь и имя файла</param>
        /// <returns></returns>
        public bool __mDescriptionDelete(string pFilePath)
        {
            bool vReturn = true; // Возвращаемое значение
            string vDescriptionFilePath = Path.Combine(Path.GetDirectoryName(pFilePath), "descript.ion"); // Путь и имя файла описаний
            appFileText vFileText = new appFileText(); // Объект для работы с текстовыми файлами

            /// Если файл отсутсвует, то возвращается пустое значение
            if (File.Exists(vDescriptionFilePath) == false)
            {
                return vReturn;
            }

            string[] vDescriptionFileLines = File.ReadAllLines(vDescriptionFilePath); // Список строк файла описаний
            int vArrayIndex = 0; // Индекс записи в массиве
            foreach (string vLine in vDescriptionFileLines)
            {
                string[] vWords = vLine.Split('"'); // Список слов с разделителем ["]
                /// Искомый файл найден
                if (vWords[1].ToLower() == Path.GetFileName(pFilePath).ToLower())
                {
                    vDescriptionFileLines[vArrayIndex] = null;
                }
                vArrayIndex++;
            }
            /// Перезапись массива строк
            vFileText.__mCreateFromArray(vDescriptionFilePath, vDescriptionFileLines);

            return vReturn;
        }
        /// <summary>
        /// Чтение описания файла
        /// </summary>
        /// <param name="pFilePath">Путь и имя файла</param>
        /// <returns></returns>
        public string __mDescriptionRead(string pFilePath)
        {
            string vReturn = ""; // Возвращаемое значение
            string vDescriptionFilePath = Path.Combine(Path.GetDirectoryName(pFilePath), "descript.ion"); // Путь и имя файла описаний
            /// Если файл отсутсвует, то возвращается пустое значение
            if (File.Exists(vDescriptionFilePath) == false)
            {
                return vReturn;
            }

            string[] vDescriptionFileLines = File.ReadAllLines(vDescriptionFilePath); // Список строк файла описаний
            int vArrayIndex = 0; // Индекс записи в массиве
            foreach (string vLine in vDescriptionFileLines)
            {
                string[] vWords = vLine.Split('"'); // Список слов с разделителем ["]
                if (vWords[1].ToLower() == Path.GetFileName(pFilePath).ToLower())
                { /// Искомый файл найден
                    vReturn = vLine.Substring(vLine.IndexOf('"', 1));
                }
                vArrayIndex++;
            }

            return vReturn;
        }
        /// <summary>
        /// Добавление описания файлу
        /// </summary>
        /// <param name="pFilePath">Путь и имя файла</param>
        /// <param name="pDescription">Описание файла</param>
        /// <returns>[true] - описание записано, иначе - [false] </returns>
        public bool __mDescriptionWrite(string pFilePath, string pDescription)
        {
            appFileText vFileText = new appFileText(); // Объект для работы с текстовыми файлами
            bool vReturn = true; // Возвращаемое значение
            string vDescriptionFilePath = Path.Combine(Path.GetDirectoryName(pFilePath), "descript.ion"); // Путь и имя файла описаний
            bool vFileFound = false; // Имя файла найдено в файле описаний

            /// Если файл отсутствует, то он создается
            if (File.Exists(vDescriptionFilePath) == false)
                File.Create(vDescriptionFilePath);
            /// Установка атрибута 'нормальный' для видимости файла
            File.SetAttributes(vDescriptionFilePath, FileAttributes.Normal);

            #region /// Поиск наличия файла которому пишется описание

            string[] vDescriptionFileLines = File.ReadAllLines(vDescriptionFilePath); // Список строк файла описаний
            int vArrayIndex = 0; // Индекс записи в массиве
            foreach (string vLine in vDescriptionFileLines)
            {
                string[] vWords = vLine.Split('"'); // Список слов с разделителем ["]
                /// Искомый файл найден
                if (vWords[1].ToLower() == Path.GetFileName(pFilePath).ToLower())
                {
                    /// Изменение записи в файле описаний
                    vDescriptionFileLines[vArrayIndex] = "\"" + Path.GetFileName(pFilePath) + "\" " + pDescription;
                    vFileFound = true;
                }
                vArrayIndex++;
            }

            #endregion Поиск наличия файла которому пишется описание

            /// Если файл не найден в файле описаний, добавление записи в файл описаний
            if (vFileFound == false)
            {
                vReturn = vFileText.__mWriteToEnd(vDescriptionFilePath, "\"" + Path.GetFileName(pFilePath) + "\" " + pDescription);
            }
            /// Если файл найден, выполняется перезапись массива строк
            else
            {
                vReturn = vFileText.__mCreateFromArray(vDescriptionFilePath, vDescriptionFileLines);
            }

            /// Установка атрибута 'Скрытый'
            File.SetAttributes(vDescriptionFilePath, FileAttributes.Hidden);

            return vReturn;
        }

        #endregion Работа с описаниями

        #region Шифрование

        /// <summary>
        /// Симметричное шифрование файла простым ключом
        /// </summary>
        /// <param name="pPassword">Пароль</param>
        /// <param name="pFilePathInput">Путь к шифруемому файлу</param>
        /// <param name="pFilePathOutput">Путь к зашифрованному файлу</param>
        public void __mEncrypt(string pPassword, string pFilePathInput, string pFilePathOutput)
        {
            try
            {
                UnicodeEncoding vUnicodeEncoding = new UnicodeEncoding(); // Объект кодирования Юникод символов в UTF-16
                FileStream vFileStreamInput = new FileStream(pFilePathInput, FileMode.Open); // Поток для работы с выходным файлом
                FileStream vFileStreamOutput = new FileStream(pFilePathOutput, FileMode.Create); // Поток для работы с выходным файлом
                RijndaelManaged vRijndaelManaged = new RijndaelManaged(); // Объект базовой реализации симметричного шифрования
                byte[] vKey = vUnicodeEncoding.GetBytes(pPassword); // Массив байт пароля

                CryptoStream oCryptoStream = new CryptoStream(vFileStreamOutput
                                                             , vRijndaelManaged.CreateEncryptor(vKey, vKey)
                                                             , CryptoStreamMode.Write); // Объект шифрования в потоке

                int vByteCount; // Количество байт прочитанных из входного файла
                while ((vByteCount = vFileStreamInput.ReadByte()) != -1)
                    oCryptoStream.WriteByte((byte)vByteCount); // Запись данных в выходной файл

                vFileStreamInput.Close();
                oCryptoStream.Close();
                vFileStreamInput.Close();
            }
            catch (Exception vException)
            {
                appUnitError vError = new appUnitError();
                vError.__fException = vException;
                vError.__fReasonS_.Add(appApplication.__oTunes.__mTranslate("Входной файл") + pFilePathInput);
                vError.__fReasonS_.Add(appApplication.__oTunes.__mTranslate("Выходной файл") + pFilePathOutput);
                vError.__fProcedure = _fClassNameFull + "_mEncrypt(string, string, string)";
                vError.__fErrorsType = ERRORSTYPES.Exception;
                appApplication.__oErrorsHandler.__mShow(vError);
            }

            return;
        }
        /// <summary>
        /// Encrypt a file. Симметричная криптография
        /// </summary>
        /// <param name="sourceFilename">The full path and name of the file to be encrypted</param>
        /// <param name="destinationFilename">The full path and name of the file to be output</param>
        /// <param name="password">The password for the encryption</param>
        /// <param name="salt">The salt to be applied to the password</param>
        /// <param name="iterations">The number of iterations Rfc2898DeriveBytes should use before generating the key and initialization vector for the decryption</param>
        public void __mEncryptGenerator(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
            }

            return;
        }
        /// <summary>
        /// Расшифровка файла зашифрованного симетрично с простым ключом
        /// </summary>
        /// <param name="pPassword">Пароль</param>
        /// <param name="pFilePathInput">Входной файл</param>
        /// <param name="pFilePathOutput">Выходной файл</param>
        public void __mDecrypt(string pPassword, string pFilePathInput, string pFilePathOutput)
        {
            UnicodeEncoding oUnicodeEncoding = new UnicodeEncoding(); // Объект кодирования Юникод символов в UTF-16
            FileStream oFileStreamInput = new FileStream(pFilePathInput, FileMode.Open); // Поток для работы с выходным файлом
            FileStream oFileStreamOutput = new FileStream(pFilePathOutput, FileMode.Create); // Поток для работы с выходным файлом
            RijndaelManaged oRijndaelManaged = new RijndaelManaged(); // Объект базовой реализации симметричного шифрования
            byte[] vKey = oUnicodeEncoding.GetBytes(pPassword); // Массив байт пароля

            CryptoStream oCryptoStream = new CryptoStream(oFileStreamInput
                                                         , oRijndaelManaged.CreateDecryptor(vKey, vKey)
                                                         , CryptoStreamMode.Read);  // Объект шифрования в потоке

            int vByteCount;  // Количество байт прочитанных из входного файла
            while ((vByteCount = oCryptoStream.ReadByte()) != -1)
                oFileStreamOutput.WriteByte((byte)vByteCount);  // Запись данных в выходной файл

            oFileStreamOutput.Close();
            oCryptoStream.Close();
            oFileStreamInput.Close();

            return;
        }

        public void __mDecryptGenerator(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    try
                    {
                        using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            source.CopyTo(cryptoStream);
                        }
                    }
                    catch (CryptographicException exception)
                    {
                        if (exception.Message == "Padding is invalid and cannot be removed.")
                            throw new ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                        else
                            throw;
                    }
                }
            }

            return;
        }

        #endregion Шифрование

        #endregion Процедуры

        #endregion МЕТОДЫ 

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Полное имя файла
        /// </summary>
        protected string _fClassNameFull = "";
        // Rfc2898DeriveBytes constants: Передавать как параметры
        public readonly byte[] salt = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; // Must be at least eight bytes.  MAKE THIS SALTIER!
        public const int iterations = 1042; // Recommendation is >= 1000.

        #endregion Внутренние

        #endregion ПОЛЯ
    }

}
