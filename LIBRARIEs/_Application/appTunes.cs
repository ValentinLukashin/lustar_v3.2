﻿using System;
using System.Collections.Generic;
using System.IO;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appTunes'
    /// </summary>
    /// <remarks>Элемент для работы с настройками приложения. Не наследуется</remarks>
    public sealed class appTunes
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Загрузка настроек из файла
        /// </summary>
        /// <returns>[true] - настройки загружены, иначе - [false]</returns>
        public bool __mLoad()
        {
            bool vReturn = true; // Возвращаемое значение
            appFileIni vFile_Ini = new appFileIni(appApplication.__oPathes.__mFileTunes()); // Объект для работы с инициализационным файлом
            /// Перебор настроек приложения
            foreach (appUnitTune vTune in fTuneList)
            {
                try
                {
                    /// Разрешена загрузка из файла
                    if (vTune.__fLoadFromFile == true)
                    {
                        if (vFile_Ini.__mSectionExists(vTune.__fName) == true)
                        {
                            vTune.__fSection = vFile_Ini.__mValueRead(vTune.__fName, "Section");
                            vTune.__fDescription = vFile_Ini.__mValueRead(vTune.__fName, "Description");
                            vTune.__fListDescriptions = vFile_Ini.__mValueRead(vTune.__fName, "DescriptionList");
                            vTune.__fValueList = vFile_Ini.__mValueRead(vTune.__fName, "ValueList");
                            vTune.__fValue = vFile_Ini.__mValueRead(vTune.__fName, "Value");
                            if (vTune.__fValue.Length > 0)
                                vTune.__fSaveInFile = true;
                        }
                    }
                }
                catch
                {
                    vReturn = false;
                    break;
                }
            }

            __fLanguage = __mTuneRead("Language");

            #region Загрузка словаря

            string vFilePath = appApplication.__oPathes.__fFolderTunes_ + __fLanguage + ".lng"; // Путь и имя файла выражений
            if (File.Exists(vFilePath) == true)
            {
                appFileDictionary vFileDictionary = new appFileDictionary(vFilePath);
                fExpressionsList.Clear(); /// Очистка списка выражений приложения
                fExpressionsList = vFileDictionary.__mLoad();
            }

            string vFileConfig = appApplication.__oTunes.__mTuneRead("ColorFileConfig"); // Имя конфигурационного файла цветов
            vFilePath = appApplication.__oPathes.__fFolderTunes_ + "\\" + vFileConfig; // Путь и имя файла хранения цветов
            if (File.Exists(vFilePath) == true)
            {
                appFileDictionary vFileDctn = new appFileDictionary(vFilePath);
            }

            vFileConfig = appApplication.__oTunes.__mTuneRead("FontFileConfig");
            vFilePath = appApplication.__oPathes.__fFolderTunes_ + "\\" + vFileConfig; // Путь и имя файла хранения шрифтов
            if (File.Exists(vFilePath) == true)
            {
                appFileDictionary vFileDictionary = new appFileDictionary(vFilePath);
            }

            #endregion Загрузка словаря

            return vReturn;
        }
        /// <summary>
        /// Добавление новой настройки в список настроек приложения
        /// </summary>
        /// <param name="pSectionName">Название секции</param>
        /// <param name="pTuneName">Название настройки</param>
        /// <param name="pTuneValue">Значение настройки</param>
        /// <param name="pTuneDescription">Описание настройки</param>
        /// <param name="pValueList">Список доступных значений настройки</param>
        /// <param name="pDspnList">Описание доступных значений настройки</param>
        /// <param name="pObjectType">Объект для редактирования настроек</param>
        /// <param name="pFormEdit">Разрешение редактирования настройки</param>
        /// <param name="pSaveFile">Разрешение сохранения значения настройки в файле</param>
        /// <param name="pLoadFile">Разрешение загрузки значения настройки из файла (не сохраняется)</param>
        /// <returns>[true] - настройка создана, иначе - [false]</returns>
        public bool __mNew(string pSectionName, string pTuneName, string pTuneValue, string pTuneDescription, string pValueList, string pDescriptionList, string pObjectType, bool pFormEdit, bool pSaveFile, bool pLoadFile)
        {
            bool vReturn = false; // Возвращаемое значение
            bool vFind = false; // Обнаружение настройки в списке настроек
            /// Поиск настройки в списке настроек
            foreach (appUnitTune vTune in fTuneList)
            {
                if (vTune.__fName.ToUpper() == pTuneName.ToUpper())
                    vFind = true;
            }
            /// Если настройка не найдена в списке настроек, то она создается
            if (vFind == false)
            {
                appUnitTune vTune = new appUnitTune();
                vTune.__fDescription = pTuneDescription;
                vTune.__fEdited = pFormEdit;
                vTune.__fListDescriptions = pDescriptionList;
                vTune.__fValueList = pValueList;
                vTune.__fLoadFromFile = pLoadFile;
                vTune.__fName = pTuneName;
                vTune.__fObjectForEdit = pObjectType;
                vTune.__fSaveInFile = pSaveFile;
                vTune.__fSection = pSectionName;
                vTune.__fValue = pTuneValue;
                fTuneList.Add(vTune);
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Сохранение настроек в файле
        /// </summary>
        /// <returns>[true] - настойки сохранены, иначе - [false]</returns>
        public bool __mSave()
        {
            bool vReturn = true; // Возвращаемое значение
            string vFilePath = appApplication.__oPathes.__mFileTunes(); // Путь конфигурационного файла
            appFileIni vFile_Ini = new appFileIni(vFilePath); // Объект для работы с инициализационным файлом

            if (File.Exists(vFilePath) == true) /// Удаление файла, если он существует, для удаления настроек, которым изменен статус сохранения
                File.Delete(vFilePath);
            /// Перебор настроек приложения
            foreach (appUnitTune vTune in fTuneList)
            {
                try
                {
                    if (vTune.__fSaveInFile == true)
                    { /// Если разрешено сохранение настройки в файле, то она сохраняется
                        vFile_Ini.__mValueWrite(vTune.__fSection, vTune.__fName, "Section");
                        vFile_Ini.__mValueWrite(vTune.__fDescription, vTune.__fName, "Description");
                        vFile_Ini.__mValueWrite(vTune.__fListDescriptions, vTune.__fName, "DescriptionList");
                        vFile_Ini.__mValueWrite(vTune.__fValueList, vTune.__fName, "ValueList");
                        vFile_Ini.__mValueWrite(vTune.__fValue, vTune.__fName, "Value");
                    }
                }
                catch
                {
                    vReturn = false;
                    break;
                }
            }

            #region Сохранение словаря

            /// Сохранение словаря перевода выражений на язык пользователя
            // Путь и имя файла выражений
            vFilePath = Path.Combine(appApplication.__oPathes.__fFolderTunes_, __fLanguage + ".lng");
            // Объект для записи словаря
            appFileDictionary vFileDictionary = new appFileDictionary(vFilePath);
            vFileDictionary.__mSave(fExpressionsList);

            // Имя конфигурационного файла цветов
            string vFileConfig = appApplication.__oTunes.__mTuneRead("ColorFileConfig");
            if (vFileConfig.Length > 0)
            {
                // Путь и имя файла хранения цветов
                vFilePath = Path.Combine(appApplication.__oPathes.__fFolderTunes_, vFileConfig);
            }

            // Имя конфигурационного файла шрифтов
            vFileConfig = appApplication.__oTunes.__mTuneRead("FontFileConfig");
            if (vFileConfig.Length > 0)
            {
                // Путь и имя файла хранения шрифтов
                vFilePath = Path.Combine(appApplication.__oPathes.__fFolderTunes_, vFileConfig);
            }

            #endregion Сохранение словаря

            return vReturn;
        }
        /// <summary>
        /// Перевод выражений на язык интерфейса
        /// </summary>
        /// <param name="pString">Переводимое выражение</param>
        /// <param name="pParameterS">Дополнительные параметры</param>
        /// <returns>Выражение на языке интерфейса</returns>
        public string __mTranslate(string pString, params object[] pParameterS)
        {
            string vReturn = pString; // Возвращаемое значение
            bool vExpressionSearched = false; // Перевод выражения найден

            /// Перебор пар выражений в словаре переводов
            foreach (string vLanguageKey in fExpressionsList.Keys)
            {
                /// Если ключ найден, возвращается его значение
                if (vLanguageKey == pString)
                {
                    fExpressionsList.TryGetValue(vLanguageKey, out vReturn);
                    vExpressionSearched = true;
                    break;
                }
            }
            /// Если ключ не найден, добавляется новая запись указанная в параметрах
            if (vExpressionSearched == false)
                fExpressionsList.Add(pString, pString);

            return String.Format(vReturn, pParameterS);
        }
        /// <summary>
        /// Чтение значения настройки
        /// </summary>
        /// <param name="pTuneName">Название настройки</param>
        /// <returns>Строчный эквивалент значения, указанной настройки</returns>
        public string __mTuneRead(string pTuneName)
        {
            string vReturn = ""; // Возвращаемое значение

            foreach (appUnitTune vTune in fTuneList)
            {
                /// Перебор настроек приложения
                if (vTune.__fName.ToUpper() == pTuneName.ToUpper())
                {
                    vReturn = vTune.__fValue;  /// Если настройка найдена - возвращается строчный эквивалент её значения
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Запись нового строчного значения настройки
        /// </summary>
        /// <param name="pTuneName">Название настройки</param>
        /// <param name="pTuneValue">Новое значение настойки</param>
        /// <returns>[true] - значение настройки изменено, иначе - [false]</returns>
        public bool __mTuneWrite(string pTuneName, object pTuneValue)
        {
            bool vReturn = false; // Возвращаемое значение

            foreach (appUnitTune vTune in fTuneList)
            {
                /// Перебор настроек приложения
                if (vTune.__fName.ToUpper() == pTuneName.ToUpper())
                {
                    vTune.__fValue = pTuneValue.ToString(); /// Если настройка найдена - записывается строчный эквивалент нового значения
                    vReturn = true;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Запись нового целочисленного значения настройки
        /// </summary>
        /// <param name="pTuneName">Название настройки</param>
        /// <param name="pTuneValue">Новое значение настойки</param>
        /// <returns>[true] - значение настройки изменено, иначе - [false]</returns>
        public bool __mTuneWrite(int pTuneIndex, object pTuneValue)
        {
            bool vReturn = false; // Возвращаемое значение

            for (int vAmount = 0; vAmount < appApplication.__oTunes.__fTunesCount; vAmount++)
            {
                /// Выполняется перебор настроек
                if (vAmount == pTuneIndex)
                {
                    fTuneList[vAmount].__fValue = pTuneValue.ToString(); /// Если настройка найдена - записывается строчный эквивалент нового значения
                    vReturn = true;
                    break;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Получение объекта настройки указанной индексом
        /// </summary>
        /// <param name="pIndex">Индекс настройки</param>
        public appUnitTune __mTuneByIndex(int pIndex)
        {
            return fTuneList[pIndex];
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Язык приложения
        /// </summary>
        public string __fLanguage = "russian";

        #endregion Атрибуты

        #region = Внутренние

        /// <summary>
        /// Список выражений используемых приложением
        /// </summary>
        private SortedDictionary<string, string> fExpressionsList = new SortedDictionary<string, string>();
        /// <summary>
        /// Список настроек
        /// </summary>
        private List<appUnitTune> fTuneList = new List<appUnitTune>();

        #endregion Внутренние

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Количество настроек приложения. Только чтение
        /// </summary>
        public int __fTunesCount
        {
            get { return fTuneList.Count; }
        }

        #endregion СВОЙСТВА
    }
}
