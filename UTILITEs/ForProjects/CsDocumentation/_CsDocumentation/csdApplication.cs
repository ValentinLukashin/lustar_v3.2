using nlApplication;
using nlControls;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;

namespace nlCsDocumentation
{
    #region ПЕРЕЧИСЛЕНИЯ

    /// <summary>
    /// Главные группы аттрибутов класса
    /// </summary>
    public enum CLASSATTRIBUTEsGROUPs
    {
        /// <summary>
        /// Класс
        /// </summary>
        Class,
        /// <summary>
        /// Дизайнеры
        /// </summary>
        Designs,
        /// <summary>
        /// Перечисления
        /// </summary>
        Enums,
        /// <summary>
        /// События
        /// </summary>
        Events,
        /// <summary>
        /// Поля
        /// </summary>
        Fields,
        /// <summary>
        /// Библиотеки
        /// </summary>
        Libraries,
        /// <summary>
        /// Методы
        /// </summary>
        Methods,
        /// <summary>
        /// Методы
        /// </summary>
        Properties
    }
    /// <summary>
    /// Свойства аттрибута класса
    /// </summary>
    public enum CLASSATTRIBUTEsPROPERTIEs
    {
        /// <summary>
        /// Авторы
        /// </summary>
        Author,
        /// <summary>
        /// Код класса
        /// </summary>
        Body,
        /// <summary>
        /// Примеры
        /// </summary>
        Example,
        /// <summary>
        /// Не документируется
        /// </summary>
        Lock,
        /// <summary>
        /// Параметр
        /// </summary>
        Param,
        /// <summary>
        /// Приечания
        /// </summary>
        Remark,
        /// <summary>
        /// Возвращаемые значения
        /// </summary>
        Return,
        /// <summary>
        /// Резюме
        /// </summary>
        Summary
    }
    /// <summary>
    /// Свойства аттрибута класса
    /// </summary>
    public enum ELEMENTATTRIBUTE
    {
        /// <summary>
        /// Авторы
        /// </summary>
        Author,
        /// <summary>
        /// Код класса
        /// </summary>
        Body,
        /// <summary>
        /// Примеры
        /// </summary>
        Example,
        /// <summary>
        /// Параметр
        /// </summary>
        Param,
        /// <summary>
        /// Не определен
        /// </summary>
        None,
        /// <summary>
        /// Примечания
        /// </summary>
        Remark,
        /// <summary>
        /// Возвращаемые значения
        /// </summary>
        Return,
        /// <summary>
        /// Резюме
        /// </summary>
        Summary
    }
    public enum ELEMENTACCESSMODIFIER
    {
        /// <summary>
        /// 
        /// </summary>
        Internal,
        /// <summary>
        /// 
        /// </summary>
        File,
        /// <summary>
        /// 
        /// </summary>
        Private,
        /// <summary>
        /// 
        /// </summary>
        PrivateProtected,
        /// <summary>
        /// 
        /// </summary>
        Protected,
        /// <summary>
        /// 
        /// </summary>
        ProtectedInternal,
        /// <summary>
        /// 
        /// </summary>
        Public
    }
    /// <summary>
    /// Виды элементов класса
    /// </summary>
    public enum ELEMENTTYPE
    {
        /// <summary>
        /// Оббъявление API библиотек
        /// </summary>
        Api,
        /// <summary>
        /// Конструктор
        /// </summary>
        Constructor,
        /// <summary>
        /// Класс
        /// </summary>
        Class,
        /// <summary>
        /// Событие
        /// </summary>
        Event,
        /// <summary>
        /// Поле
        /// </summary>
        Field,
        /// <summary>
        /// Метод
        /// </summary>
        Method,
        /// <summary>
        /// Не используется
        /// </summary>
        None,
        /// <summary>
        /// Свойства
        /// </summary>
        Property
    }

    #endregion ПЕРЕЧИСЛЕНИЯ

    /// <summary>
    /// Класс 'csdApplication'
    /// </summary>
    /// <remarks>Элемент - основа приложений для документирования проектов</remarks>
    public class csdApplication : crlApplication
    {
    }
}
