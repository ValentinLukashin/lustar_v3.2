using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nlApplication
{
    /// <summary>
    /// Класс настройки приложения
    /// </summary>
    /// <remarks>Элемент - настройка приложения</remarks>
    public class appUnitTune
    {
        #region = ПОЛЯ

        /// <summary>
        /// Описание настройки
        /// </summary>
        public string __fDescription = "";
        /// <summary>
        /// Доступность на форме настроек приложения
        /// </summary>
        public bool __fEdited = false;
        /// <summary>
        /// Список описаний допустимых значений
        /// </summary>
        public string __fListDescriptions = "";
        /// <summary>
        /// Разрешение загрузки настройки из файла (если она там определена, сохранение запрещено)
        /// </summary>
        public bool __fLoadFromFile = false;
        /// <summary>
        /// Название настройки
        /// </summary>
        public string __fName = "";
        /// <summary>
        /// Объект для отображения настройки
        /// </summary>
        public string __fObjectForEdit = null;
        /// <summary>
        /// Разрешение хранения настройки в файле
        /// </summary>
        public bool __fSaveInFile = false;
        /// <summary>
        /// Название секции настройки
        /// </summary>
        public string __fSection = "";
        /// <summary>
        /// Значение настройки
        /// </summary>
        public string __fValue = "";
        /// <summary>
        /// Тип данных значения настройки
        /// </summary>
        public Type __fValueDataType = null;
        /// <summary>
        /// Список допустимых значений
        /// </summary>
        public string __fValueList = "";

        #endregion ПОЛЯ
    }
}
