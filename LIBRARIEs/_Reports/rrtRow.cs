using System.Collections.Generic;

namespace nlReports
{
    /// <summary>
    /// Класс строки отчета
    /// </summary>
    public class rrtRow
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Добавление ячейки в список ячеек строки
        /// </summary>
        /// <param name="pCell">Ячейка</param>
        /// <returns>Номер ячейки в строке</returns>
        public int __mAdd(rrtCell pCell)
        {
            _fCellsList.Add(pCell);
            return _fCellsList.Count;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Горизонтальная привязка текста
        /// </summary>
        /// <remarks>Допустимые значения
        /// left, center, right, justify
        /// </remarks>
        public string __fAlign = "";
        /// <summary>
        /// Отображаемые линии рамки
        /// </summary>
        /// <remarks>
        /// l - левая линия рамки
        /// r - правая линия рамки
        /// t - верхняя линия рамки
        /// b - нижняя линия рамки
        /// </remarks>
        public string __fBorder = "";
        /// <summary>
        /// Цвет линий рамки
        /// </summary>
        public string __fBorderColor = "";
        /// <summary>
        /// Толщина линий рамки
        /// </summary>
        public string __fBorderSize = "";
        /// <summary>
        /// Используемые классы
        /// </summary>
        public string __fClasses = "";
        /// <summary>
        /// Цвет фона
        /// </summary>
        public string __fColorBack = "";
        /// <summary>
        /// Цвет текста
        /// </summary>
        public string __fColorText = "";
        /// <summary>
        /// Толстый шрифт
        /// </summary>
        public bool __fFontBold = false;
        /// <summary>
        /// Наклонный шрифт
        /// </summary>
        public bool __fFontItalic = false;
        /// <summary>
        /// Подчеркнутый текст
        /// </summary>
        public bool __fFontUnderline = false;
        /// <summary>
        /// Название шрифта
        /// </summary>
        public string __fFontName = "";
        /// <summary>
        /// Размер шрифта
        /// </summary>
        public string __fFontSize = "";
        /// <summary>
        /// Высота
        /// </summary>
        public string __fHeight = "";
        /// <summary>
        /// Скрыть строку при печати
        /// </summary>
        public bool __fHide = false;
        /// <summary>
        /// Количество объеденяемых ячеек по горизонтали
        /// </summary>
        public string __fSpanColumn = "";
        /// <summary>
        /// Количество объеденяемых ячеек по горизонтали
        /// </summary>
        public string __fSpanRow = "";
        /// <summary>
        /// Значение ячейки
        /// </summary>
        public object __fValue;
        /// <summary>
        /// Вертикальная привязка текста
        /// </summary>
        public string __fValign = "";
        /// <summary>
        /// Ширина
        /// </summary>
        public string __fWidth = "";
        /// <summary>
        /// Список ячеек в строке
        /// </summary>
        internal List<rrtCell> _fCellsList = new List<rrtCell>();

        #endregion Атрибуты

        #endregion ПОЛЯ
    }
}
