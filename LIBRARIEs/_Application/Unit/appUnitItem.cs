using System;

namespace nlApplication
{
    public class appUnitItem
    {
        #region = ПОЛЯ

        /// <summary>
        /// Включатель
        /// </summary>
        public bool __fCheck_ { get; set; }
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int __fClue_ { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string __fDesignation_ { get; set; }
        /// <summary>
        /// Тип данных значения
        /// </summary>
        public Type __fType_ { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public object __fValue_ { get; set; }

        #endregion ПОЛЯ   
    }
}
