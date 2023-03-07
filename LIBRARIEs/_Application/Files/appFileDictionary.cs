using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appFileDictionary'
    /// </summary>
    /// <remarks>Класс для работы с файлами словарями</remarks>
    public class appFileDictionary
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор 
        /// </summary>
        public appFileDictionary()
        {
            _mLoad();
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pFilePath">Путь и имя файла</param>
        public appFileDictionary(string pFilePath)
        {
            _mLoad();
            _fFilePath = pFilePath;
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ 

        #region - Поведение

        /// <summary>
        /// Выполняется при создании объекта
        /// </summary>
        protected virtual void _mLoad()
        {
            Type vType = this.GetType();
            _fClassNameFull = vType.FullName + vType.Name + ".";
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Чтение словаря из файла
        /// </summary>
        /// <returns>SortedDictionary<string, string></returns>
        public SortedDictionary<string, string> __mLoad()
        {
            SortedDictionary<string, string> vReturn = new SortedDictionary<string, string>(); // Возвращаемое значение
            if (File.Exists(_fFilePath) == true)
            { /// Файл существует
                string[] vFileContent = File.ReadAllLines(_fFilePath, Encoding.Default); // Построчное содержание файла
                foreach (string vLine in vFileContent)
                {
                    if (vLine.Length > 0)
                    {
                        int vSeparatorPosition = vLine.IndexOf('='); /// Позиция разделителя выражений [ = ]
                        if (vSeparatorPosition > 0) /// Разделитель обнаружен
                            try
                            {
                                vReturn.Add(vLine.Substring(0, vSeparatorPosition).Trim(), vLine.Substring(vSeparatorPosition + 1).Trim());
                            }
                            catch
                            { }
                    }
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Запись словаря в файл
        /// </summary>
        /// <param name="pDictionary">Словарь</param>
        public void __mSave(SortedDictionary<string, string> pDictionary)
        {
            appFileText vFileText = new appFileText(); // Объект для работы с текстовыми файлами
            if (File.Exists(_fFilePath) == true)
                File.Delete(_fFilePath);
            foreach (string vKey in pDictionary.Keys)
            {
                string vValue = "";
                pDictionary.TryGetValue(vKey, out vValue);
                vFileText.__mWriteToEnd(_fFilePath, vKey.Trim() + " = " + vValue.Trim());
            }
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region № ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Путь и имя файла
        /// </summary>
        public string _fFilePath = "";

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>Название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #endregion ПОЛЯ
    }
}
