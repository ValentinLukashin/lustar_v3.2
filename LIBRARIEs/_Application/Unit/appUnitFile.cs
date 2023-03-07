using System;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appFileUnit'
    /// </summary>
    /// <remarks>Элемент файловой системы</remarks>
    public class appUnitFile
    {
        #region = ПОЛЯ

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
        public DateTime __fDateTimeWrite = DateTime.Now;
        /// <summary>
        /// Размер файла
        /// </summary>
        public long __fSize = -1;
        /// <summary>
        /// Размер файла
        /// </summary>
        public string __fVersion = "1.0.0.0";

        #endregion ПОЛЯ
    }
}
