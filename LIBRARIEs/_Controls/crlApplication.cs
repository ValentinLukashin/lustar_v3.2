using nlDataMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nlControls
{
    #region ПЕРЕЧИСЛЕНИЯ

    /// <summary>
    /// Категории цветов используемых приложением
    /// </summary>
    public enum COLORS
    {
        /// <summary>
        /// Цвет текста данных
        /// </summary>
        Data,
        /// <summary>
        /// Цвет фона текста данных
        /// </summary>
        DataBack,
        /// <summary>
        /// Фон не доступных данных
        /// </summary>
        DataBackDisabled,
        /// <summary>
        /// Фон обязательно заполняемых данных
        /// </summary>
        DataBackNecessarily,
        /// <summary>
        /// Цвет формы
        /// </summary>
        FormActive,
        /// <summary>
        /// Цвет не активной формы
        /// </summary>
        FormDeactive,
        /// <summary>
        /// Подсветка записи помеченной для обновления менее X суток назад
        /// </summary>
        GridChange,
        /// <summary>
        /// Цвет обычного текста
        /// </summary>
        Text,
        /// <summary>
        /// Цвет текста - ссылки
        /// </summary>
        TextButton,
        /// <summary>
        /// Цвет текста - заголовка
        /// </summary>
        TextTitle
    }
    /// <summary>
    /// Вид данных отображаемых в ComboBox
    /// </summary>
    public enum COMBOTYPES
    {
        /// <summary>
        /// Логические значения
        /// </summary>
        Bool,
        /// <summary>
        /// Значения загружаются из источника данных и вводяться в ручную
        /// </summary>
        Items
    }
    /// <summary>
    /// Вид отображения датывремени
    /// </summary>
    public enum DATETIMETYPES
    {
        /// <summary>
        /// Дата
        /// </summary>
        Date,
        /// <summary>
        /// Дата и время
        /// </summary>
        DateTime
    }
    /// <summary>
    /// Категории шрифтов используемых приложеннием
    /// </summary>
    public enum FONTS
    {
        /// <summary>
        /// Шрифт текста данных
        /// </summary>
        Data,
        /// <summary>
        /// Не редактируемый узел дерева
        /// </summary>
        NodeNotEdit,
        /// <summary>
        /// Шрифт текста
        /// </summary>
        Text,
        /// <summary>
        /// Шрифт текста - ссылки
        /// </summary>
        TextButton,
        /// <summary>
        /// Шрифт текста - заголовка
        /// </summary>
        TextTitle
    }
    /// <summary>
    /// Обязательность заполнения поля ввода
    /// </summary>
    public enum FILLTYPES
    {
        /// <summary>
        /// Можно заполнять или не заполнять
        /// </summary>
        None,
        /// <summary>
        /// Данные должны быть введены обязательно
        /// </summary>
        Necessarily
    }
    /// <summary>
    /// Вид формы для правки данных
    /// </summary>
    public enum FORMOPENEDTYPES
    {
        /// <summary>
        /// Форма для правки документа
        /// </summary>
        FormDocument,
        /// <summary>
        /// Форма для правки записи
        /// </summary>
        FormRecord
    }
    /// <summary>
    /// Вид формы для выбора значений для crlInputForm 
    /// </summary>
    public enum FORMSELECTEDTYPES
    {
        /// <summary>
        /// Форма для работы с табличными данными
        /// </summary>
        FormGrid,
        /// <summary>
        /// Форма для работы с папочными данными
        /// </summary>
        FormGridFolder,
        /// <summary>
        /// Форма для работы с древовидными данными
        /// </summary>
        FormTree
    }
    /// <summary>
    /// Виды текста на форме
    /// </summary>
    public enum LABELTYPES
    {
        /// <summary>
        /// Надпись - ссылка
        /// </summary>
        Button,
        /// <summary>
        /// Обычный текст
        /// </summary>
        Normal,
        /// <summary>
        /// Заголовок
        /// </summary>
        Title
    }
    /// <summary>
    /// Виды путей
    /// </summary>
    public enum PATHTYPES
    {
        /// <summary>
        /// Файл
        /// </summary>
        File,
        /// <summary>
        /// Папка
        /// </summary>
        Folder
    }

    #endregion ПЕРЕЧИСЛЕНИЯ

    /// <summary>
    /// Класс 'crlApplication'
    /// </summary>
    /// <remarks>Элемент основа приложений работающих с интерфейсом</remarks>
    public class crlApplication : datApplication
    {
        #region = ПОЛЯ 

        /// <summary>
        /// Объект для работы с настройками интерфейса
        /// </summary>
        public static crlInterface __oInterface = new crlInterface();
        /// <summary>
        /// Объект для обработки ошибок приложения
        /// </summary>
        public new static crlErrorsHandler __oErrorsHandler = new crlErrorsHandler();
        /// <summary>
        /// Объект для отображения сообщений пользователю
        /// </summary>
        public new static crlMessages __oMessages = new crlMessages();
        /// <summary>
        /// Объект для работы с путями приложения
        /// </summary>
        public new static crlPathes __oPathes = new crlPathes();

        #endregion ПОЛЯ
    }
}
