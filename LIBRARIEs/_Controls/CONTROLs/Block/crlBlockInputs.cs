using nlApplication;
using System;
using System.Data;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlBlockInputs'
    /// </summary>
    /// <remarks>Блок для размещения полей ввода</remarks>
    public class crlBlockInputs : crlComponentPanel
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Настройка контрола

            Dock = DockStyle.Fill;
            Width = 400; // Для нормального размещения полей ввода

            #endregion Настройка контрола

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            __fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }

        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка данных из источника данных
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mDataLoad()
        {
            bool vReturn = true; // Возвращаемое значение
            appUnitError vError = new appUnitError();

            vError.__fErrorsType = ERRORSTYPES.Programming;
            vError.__fProcedure = _fClassNameFull + "__mDataLoad()";
            vError.__fMessage_ = "Не удалось загрузить данные";

            if (__oDataTable == null)
            {
                vError.__mReasonAdd("не определены данные");
                crlApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            if (__oDataTable.Rows.Count != 1)
            {
                vError.__mReasonAdd("не верно указаны данные. Должна быть одна запись");
                crlApplication.__oErrorsHandler.__mShow(vError);
                return false;
            }
            /// Заполнение компонентов данными
            foreach (Control vInput in Controls)
            {
                /// Исключение блока вкладок
                if ((vInput is crlComponentPageBlock) == true)
                    continue;
                /// Исключение компонентов с неуказанным названием поля или объекта - {null}
                if ((vInput as crlInput).__fFieldName == "")
                    continue;
                if ((vInput is crlInputComboBool) == true)
                {
                    (vInput as crlInputComboBool).__fValue_ = Convert.ToBoolean(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputComboList) == true)
                {
                    (vInput as crlInputComboList).__fValue_ = Convert.ToInt32(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputDateTime) == true)
                {
                    (vInput as crlInputDateTime).__fValue_ = Convert.ToDateTime(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputForm) == true)
                {
                    (vInput as crlInputForm).__fValue_ = Convert.ToInt32(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputFormSearch) == true)
                {
                    (vInput as crlInputFormSearch).__fValue_ = Convert.ToInt32(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputNumberDecimal) == true)
                {
                    (vInput as crlInputNumberDecimal).__fValue_ = Convert.ToDecimal(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputNumberInt) == true)
                {
                    (vInput as crlInputNumberInt).__fValue_ = Convert.ToInt32(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
                if ((vInput is crlInputChar) == true)
                {
                    (vInput as crlInputChar).__fValue_ = Convert.ToString(__oDataTable.Rows[0][(vInput as crlInput).__fFieldName]);
                    continue;
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Сохранение данных в источнике данных
        /// </summary>
        /// <returns>[true] - данные сохранены, иначе - [false]</returns>
        public bool __mDataSave()
        {
            bool vReturn = true; // Возвращаемое значение
            /// Запись значений в таблицу
            if (__oDataTable != null)
            {
                foreach (Control vInput in Controls)
                {
                    if ((vInput is crlInput) == true)
                    {
                        if ((vInput as crlInput).__fFieldName.Length == 0)
                            continue;
                        if ((vInput is crlInputComboBool) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputComboBool).__fFieldName] = (vInput as crlInputComboBool).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputComboList) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputComboList).__fFieldName] = (vInput as crlInputComboList).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputDateTime) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputDateTime).__fFieldName] = (vInput as crlInputDateTime).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputForm) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputForm).__fFieldName] = (vInput as crlInputForm).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputFormSearch) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputFormSearch).__fFieldName] = (vInput as crlInputFormSearch).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputNumberDecimal) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputNumberDecimal).__fFieldName] = (vInput as crlInputNumberDecimal).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputNumberInt) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputNumberInt).__fFieldName] = (vInput as crlInputNumberInt).__fValue_;
                            continue;
                        }
                        if ((vInput is crlInputChar) == true)
                        {
                            __oDataTable.Rows[0][(vInput as crlInputChar).__fFieldName] = (vInput as crlInputChar).__fValue_;
                            continue;
                        }
                    }
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Получение единого выражения фильтра на основе состояния компонентов
        /// </summary>
        /// <returns></returns>
        public string _mFilterBuild()
        {
            string vReturn = ""; // Возвращаемое значение

            /// Перебор установленных компонентов фильтра
            foreach (crlInput vInput in Controls)
            {
                if (vInput.__fCheckStatus_ == true)
                {
                    if (vInput.__fFieldName.Length == 0)
                    {
                        appUnitError vError = new appUnitError();
                        vError.__fErrorsType = ERRORSTYPES.Programming;
                        vError.__mMessageBuild("Не указана имя поля для компоненты ввода");
                        vError.__fProcedure = __fClassNameFull + "_mFilterLoad()";
                        vError.__mReasonAdd("Компонент ввода: {0}", vInput.Name);
                        crlApplication.__oErrorsHandler.__mShow(vError);

                        return vReturn;
                    }
                    if (string.IsNullOrEmpty(vReturn) == false)
                    {
                        vReturn = vReturn + " and ";
                    }
                    vReturn = vReturn + vInput.__fFilterExpression_;
                }
            } /// Перебор установленных компонентов фильтра

            return vReturn;
        }
        /// <summary>
        /// Загрузка настроек фильтра из файла
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool _mFilterLoad()
        {
            bool vReturn = true; // Возвращаемое значение
            appFileIni oFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с инициализационным файлом

            if (__fFormParentName.Length == 0)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Не указана форма для которой строиться фильтр");
                vError.__fProcedure = __fClassNameFull + "_mFilterLoad()";
                crlApplication.__oErrorsHandler.__mShow(vError);

                return false;
            } /// Не указано название формы для которой строиться фильтр

            foreach (Control vInput in Controls)
            { /// Перебор установленных компонентов фильтра
                if ((vInput is crlInput) == true)
                { /// Компонент - поле ввода
                    if ((vInput as crlInput).__fFieldName.Length == 0)
                    {
                        appUnitError vError = new appUnitError();
                        vError.__fErrorsType = ERRORSTYPES.Programming;
                        vError.__mMessageBuild("Не указана имя поля для компоненты ввода");
                        vError.__fProcedure = __fClassNameFull + "_mFilterLoad()";
                        vError.__mReasonAdd("Компонент ввода: {0}", (vInput as crlInput).Name);
                        crlApplication.__oErrorsHandler.__mShow(vError);

                        return false;
                    }
                    string vFieldName = (vInput as crlInput).__fFieldName; // Название поля для которого строиться фильтр
                    try
                    {
                        switch ((vInput as crlInput).Name)
                        {
                            case "appInputDateTimePeriod":
                            //(vInput as appInputDateTimePeriod)._cCheckFilterUsed.Checked = Convert.ToBoolean(oFileIni._mValueRead(_fFormParentName, "FilterStatus_" + vFieldName)); // Загрузка статуса
                            //(vInput as appInputDateTimePeriod)._cCheckFilterUseTo.Checked = Convert.ToBoolean(oFileIni._mValueRead(_fFormParentName, "FilterStatusTo_" + vFieldName)); // Загрузка статуса

                            //if ((vInput as appInputDateTimePeriod)._cInput._fValueInTicks == false)
                            //{
                            //    (vInput as appInputDateTimePeriod)._cInput.Value = Convert.ToDateTime(oFileIni._mValueRead(_fFormParentName, "FilterValue_" + vFieldName)); // Загрузка значений
                            //    (vInput as appInputDateTimePeriod)._cInputTo.Value = Convert.ToDateTime(oFileIni._mValueRead(_fFormParentName, "FilterValueTo_" + vFieldName)); // Загрузка значений
                            //} /// Данные храняться как дата-время
                            //else
                            //{
                            //    (vInput as appInputDateTimePeriod)._cInput.Value = new DateTime(Convert.ToInt64(oFileIni._mValueRead(_fFormParentName, "FilterValue_" + vFieldName))); // Загрузка значений
                            //    (vInput as appInputDateTimePeriod)._cInputTo.Value = new DateTime(Convert.ToInt64(oFileIni._mValueRead(_fFormParentName, "FilterValueTo_" + vFieldName))); // Загрузка значений
                            //} /// Данные храняться как тики
                            //break;
                            case "crlInputText":
                                (vInput as crlInputChar).__fCheckStatus_ = Convert.ToBoolean(oFileIni.__mValueRead(__fFormParentName, "FilterStatus_" + vFieldName)); /// Загрузка статуса
                                // Воссстановить (vInput as crlInputText)._fValue = oFileIni._mValueRead(_fFormParentName, "FilterValue_" + vFieldName); /// Загрузка значения
                                break;
                        }
                    }
                    catch
                    {
                        (vInput as crlInput).__fCheckStatus_ = false; // Первая загрузка статуса
                    }
                } /// Компонент - поле ввода

            } /// Перебор установленных компонентов фильтра
            return vReturn;
        }
        /// <summary>
        /// Сохранение настроек фильтра в файл
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool _mFilterSave()
        {
            __fFormFilterExpression = ""; // Сформированное условие фильтра
            __fFormFilterMessage = ""; // Условие фильтра отображаемое пользователю

            bool vReturn = true; // Возвращаемое значение
            appFileIni oFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с инициализационным файлом

            if (__fFormParentName.Length == 0)
            {
                __fFormParentName = FindForm().Name;
            } /// Не указано название формы для которой строиться фильтр

            foreach (Control vInput in Controls)
            { /// Перебор установленных компонентов фильтра
                string vFieldName = (vInput as crlInput).__fFieldName; // Название поля для которого строиться фильтр

                if ((vInput is crlInput) == true)
                { /// Компонент - поле ввода

                    oFileIni.__mValueWrite((vInput as crlInput).__fCheckVisible_.ToString(), __fFormParentName, "FilterStatus_" + vFieldName); /// Сохранение статуса использования текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fCaption_, __fFormParentName, "FilterCaption_" + vFieldName); /// Сохранение заголовка текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fFilterExpression_, __fFormParentName, "FilterExpression_" + vFieldName); /// Сохранение условия фильтра текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fFilterMessage_, __fFormParentName, "FilterMessage_" + vFieldName); /// Сохранение выражение фильтра текущего компонента
                    // Восстановить oFileIni._mValueWrite((vInput as crlInputText)._fValue.ToString(), _fFormParentName, "FilterValue_" + vFieldName); /// Сохранение значения текущего компонента

                    string vFilterExpression = (vInput as crlInput).__fFilterExpression_.Trim(); // 
                    string vFilterMessage = (vInput as crlInput).__fFilterMessage_.Trim(); // 
                    if (vFilterExpression.Length > 0)
                    { /// Собрание всех условий в единый фильтр
                        if (__fFormFilterExpression.Length == 0)
                        {
                            __fFormFilterExpression = vFilterExpression;
                            __fFormFilterMessage = vFilterMessage;
                        }
                        else
                        {
                            if (vFilterExpression.Length > 0)
                            {
                                __fFormFilterExpression = __fFormFilterExpression + " AND " + vFilterExpression;
                                __fFormFilterMessage = __fFormFilterMessage + "\n";
                            }
                        }
                    } /// Собрание всех условий в единый фильтр

                } /// Компонент - поле ввода
            } /// Перебор установленных компонентов фильтра

            return vReturn;
        }
        /// <summary>
        /// Добавление поля ввода
        /// </summary>
        /// <param name="pInput">Поле ввода</param>
        /// <returns>[true] - поле ввода добавлено, иначе - [false]</returns>
        public virtual bool __mInputAdd(crlInput pInput, AnchorStyles pAnchorStyles = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
        {
            bool vReturn = true; // Возвращаемое значение
            bool vSeached = false; // Добавляемый элемент уже обнаружен в области

            if (pInput == null)
            {
                __fTopCoordinate = __fTopCoordinate + 25;
                return true;
            }

            if (vSeached == false)
            {
                Controls.Add(pInput);
                pInput.__fCheckVisible_ = __fCheckShow;
                pInput.Anchor = pAnchorStyles;
                pInput.Left = crlInterface.__fIntervalHorizontal;
                pInput.Top = __fTopCoordinate;
                pInput.Width = Width - crlInterface.__fIntervalHorizontal * 2;

                __fTopCoordinate = __fTopCoordinate + pInput.Height + crlInterface.__fIntervalVertical;
                //delete Width = fTop;
            } /// Добавление компонента

            return vReturn;
        }
        /// <summary>
        /// Добавление панели вкладок
        /// </summary>
        /// <param name="pPageBlock"></param>
        /// <param name="pAnchorStyles"></param>
        public virtual void __mPageBlockAdd(crlComponentPageBlock pPageBlock, AnchorStyles pAnchorStyles = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
        {
            pPageBlock.Name = "_cPageBlock";
            pPageBlock.Top = __fTopCoordinate;
            pPageBlock.Left = crlInterface.__fIntervalHorizontal;
            pPageBlock.Width = Width - crlInterface.__fIntervalHorizontal * 2;
            pPageBlock.Anchor = pAnchorStyles; // !!!
            Controls.Add(pPageBlock);
            pPageBlock.Height = 150;
            __fTopCoordinate = __fTopCoordinate + pPageBlock.Height + crlInterface.__fIntervalVertical;
            //delete Width = fTop - crlInterface.__fIntervalVertical;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ 

        #region - Атрибуты

        /// <summary>
        /// Разрешение отображения галочки во всех добавляемых контролах
        /// </summary>
        public bool __fCheckShow = true;
        /// <summary>
        /// Название родительской формы, для которой строиться фильтр
        /// </summary>
        public string __fFormParentName = "";
        /// <summary>
        /// Сформированное условие фильтра
        /// </summary>
        public string __fFormFilterExpression = "";
        /// <summary>
        /// Условие фильтра отображаемое пользователю
        /// </summary>
        public string __fFormFilterMessage = "";
        /// <summary>
        /// Отклонение компонента от верхнего края
        /// </summary>
        public int __fTopCoordinate = crlInterface.__fIntervalHorizontal;

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>
        /// Полное название класса 
        /// </summary>
        protected string __fClassNameFull = "";

        #endregion Внутренние

        #region - Объекты 

        /// <summary>
        /// Таблица с обрабатываемой записью
        /// </summary>
        public DataTable __oDataTable;

        #endregion Объекты

        #endregion ПОЛЯ
    }
}
