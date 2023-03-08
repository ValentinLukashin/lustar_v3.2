using nlApplication;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlAreaFilter'
    /// </summary>
    /// <remarks>Область для построения фильтра данных</remarks>
    public class crlAreaFilter : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Panel2.Controls.Add(_cBlockInputs);
            Panel2.Controls.SetChildIndex(_cBlockInputs, 0);
            Panel2.Controls.SetChildIndex(__cToolBar, 1);
            __cToolBar.Items.Insert(0, _cButtonApply);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            // _cButtonApply
            {
                _cButtonApply.Click += _cButtonApply_Click;
                _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                _cButtonApply.ToolTipText = "[ Ctrl + A ]\n" + crlApplication.__oTunes.__mTranslate("Применить");
            }

            // _cBlockInputs
            {
                _cBlockInputs.Dock = DockStyle.Fill;
                _cBlockInputs.AutoScroll = true;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при первом отображении объекта
        /// </summary>
        protected override void _mObjectPresetation()
        {
            base._mObjectPresetation();
            __mFilterLoad();
        }

        #region Кнопки управления

        /// <summary>
        /// Выполняется при выборе кнопки 'Применить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonApply_Click(object sender, EventArgs e)
        {
            __mFilterSave();
            crlForm vForm = FindForm() as crlForm;
            vForm.Close();
        }

        #endregion Кнопки управления

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка настроек фильтра из файла
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mFilterLoad()
        {
            bool vReturn = true; // Возвращаемое значение
            crlForm vForm = FindForm() as crlForm;
            appFileIni oFileIni = vForm.__oFileIni; // Объект для работы с инициализационным файлом
            oFileIni.__fFilePath = crlApplication.__oPathes.__mFileFormTunes();

            if (_fFormNameParent.Length == 0)
            {
                _fFormNameParent = FindForm().Name;
            } /// Не указано название формы для которой строиться фильтр

            /// Перебор установленных компонентов фильтра
            foreach (Control vInput in _cBlockInputs.Controls)
            { /// Перебор установленных компонентов фильтра
                string vFieldName = (vInput as crlInput).__fFieldName; // Название поля для которого строиться фильтр

                if ((vInput is crlInput) == true)
                { /// Компонент - поле ввода
                    try
                    {
                        (vInput as crlInput).__fCheckStatus_ = Convert.ToBoolean(oFileIni.__mValueRead(_fFormNameParent, "FilterStatus_" + vFieldName)); /// Загрузка статуса
                    }
                    catch
                    {
                        (vInput as crlInput).__fCheckStatus_ = false;
                    }
                    try
                    {
                        if (vInput is crlInputComboBool)
                            (vInput as crlInputComboBool).__fValue_ = Convert.ToBoolean(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if(vInput is crlInputComboList)
                            (vInput as crlInputComboList).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if(vInput is crlInputDateTime)
                            (vInput as crlInputDateTime).__fValue_ = Convert.ToDateTime(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputDateTimePeriod)
                        {
                            if ((vInput as crlInputDateTimePeriod).__pValueInTicks == false)
                            {
                                (vInput as crlInputDateTimePeriod).__fValue_ = DateTime.Parse(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName), new CultureInfo("ru-RU", false));
                                (vInput as crlInputDateTimePeriod).__fValueTo_ = DateTime.Parse(oFileIni.__mValueRead(_fFormNameParent, "FilterValueTo_" + vFieldName), new CultureInfo("ru-RU", false));
                            } /// Данные храняться как дата-время
                            else
                            {
                                (vInput as crlInputDateTimePeriod).__fValue_ = new DateTime(Convert.ToInt64(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName)));
                                (vInput as crlInputDateTimePeriod).__fValueTo_ = new DateTime(Convert.ToInt64(oFileIni.__mValueRead(_fFormNameParent, "FilterValueTo_" + vFieldName)));
                            } /// Данные храняться как тики
                        }
                        if(vInput is crlInputForm)
                            (vInput as crlInputForm).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName)); 
                        if (vInput is crlInputFormSearch)
                            (vInput as crlInputFormSearch).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if(vInput is crlInputNumberDecimal)
                            (vInput as crlInputNumberDecimal).__fValue_ = Convert.ToDecimal(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if(vInput is crlInputNumberInt)
                            (vInput as crlInputNumberInt).__fValue_ = Convert.ToInt32(oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName));
                        if (vInput is crlInputPhone)
                            (vInput as crlInputPhone).__fValue_ = oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName); 
                        if(vInput is crlInputChar)
                            (vInput as crlInputChar).__fValue_ = oFileIni.__mValueRead(_fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    catch
                    {
                        (vInput as crlInput).__fCheckStatus_ = false; /// Первая загрузка статуса
                    }
                } /// Компонент - поле ввода

            }

            return vReturn;
        }
        /// <summary>
        /// Сохранение настроек фильтра в файл
        /// </summary>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public bool __mFilterSave()
        {
            _fFormFilterExpression = ""; // Сформированное условие фильтра
            _fFormFilterMessage = ""; // Условие фильтра отображаемое пользователю

            bool vReturn = true; // Возвращаемое значение
            crlForm vForm = FindForm() as crlForm;
            appFileIni oFileIni = vForm.__oFileIni; // Объект для работы с инициализационным файлом

            if (_fFormNameParent.Length == 0)
            {
                _fFormNameParent = FindForm().Name;
            } /// Не указано название формы для которой строиться фильтр

            foreach (Control vInput in _cBlockInputs.Controls)
            { /// Перебор установленных компонентов фильтра
                string vFieldName = (vInput as crlInput).__fFieldName; // Название поля для которого строиться фильтр

                if ((vInput is crlInput) == true)
                { /// Компонент - поле ввода
                    oFileIni.__mValueWrite((vInput as crlInput).__fCheckStatus_.ToString(), _fFormNameParent, "FilterStatus_" + vFieldName); /// Сохранение статуса использования текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fCaption_, _fFormNameParent, "FilterCaption_" + vFieldName); /// Сохранение заголовка текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fFilterExpression_, _fFormNameParent, "FilterExpression_" + vFieldName); /// Сохранение условия фильтра текущего компонента
                    oFileIni.__mValueWrite((vInput as crlInput).__fFilterMessage_, _fFormNameParent, "FilterMessage_" + vFieldName); /// Сохранение выражение фильтра текущего компонента

                    if(vInput is crlInputComboBool)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputComboBool).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputComboList)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputComboList).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputDateTime)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputDateTime).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputDateTimePeriod)
                    {
                        oFileIni.__mValueWrite(appTypeDateTime.__mDateTimeToString(Convert.ToDateTime((vInput as crlInputDateTimePeriod).__fValue_)).ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                        oFileIni.__mValueWrite(appTypeDateTime.__mDateTimeToString(Convert.ToDateTime((vInput as crlInputDateTimePeriod).__fValueTo_)).ToString(), _fFormNameParent, "FilterValueTo_" + vFieldName);
                    }
                    if (vInput is crlInputForm)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputForm).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputFormSearch)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputFormSearch).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputNumberDecimal)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputNumberDecimal).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputNumberInt)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputNumberInt).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputPhone)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputPhone).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }
                    if (vInput is crlInputChar)
                    {
                        oFileIni.__mValueWrite((vInput as crlInputChar).__fValue_.ToString(), _fFormNameParent, "FilterValue_" + vFieldName);
                    }

                    string vFilterExpression = (vInput as crlInput).__fFilterExpression_.Trim();
                    string vFilterMessage = (vInput as crlInput).__fFilterMessage_.Trim();
                    if (vFilterExpression.Length > 0)
                    { /// Собрание всех условий в единый фильтр
                        if (_fFormFilterExpression.Length == 0)
                        {
                            _fFormFilterExpression = vFilterExpression;
                            _fFormFilterMessage = vFilterMessage;
                        }
                        else
                        {
                            if (vFilterExpression.Length > 0)
                            {
                                _fFormFilterExpression = _fFormFilterExpression + " AND " + vFilterExpression;
                                _fFormFilterMessage = _fFormFilterMessage + "\n" + vFilterMessage;
                            }
                        }
                    } /// Собрание всех условий в единый фильтр
                } /// Компонент - поле ввода
            } /// Перебор установленных компонентов фильтра

            return vReturn;
        }
        /// <summary>
        /// Добавление поля ввода на панель полей ввода
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        public bool __mInputAdd(crlInput pInput)
        {
            return _cBlockInputs.__mInputAdd(pInput);
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Применить' 
        /// </summary>
        public void __mPressButtonApply()
        {
            _cButtonApply.PerformClick();
            return;
        }

        #endregion - Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary> 
        /// Сформированное условие фильтра
        /// </summary>
        public string _fFormFilterExpression = "";
        /// <summary>
        /// Условие фильтра отображаемое пользователю
        /// </summary>
        public string _fFormFilterMessage = "";
        /// <summary>
        /// Имя родительской формы для которой строиться фильтр
        /// </summary>
        public string _fFormNameParent = "";

        #endregion Атрибуты

        #region - Компоненты

        /// <summary>
        /// Кнопка 'Применить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonApply = new crlComponentToolBarButton();
        /// <summary>
        /// Панель для отображения полей ввода
        /// </summary>
        protected crlBlockInputs _cBlockInputs = new crlBlockInputs();

        #endregion Компоненты

        #endregion ПОЛЯ
    }
}
