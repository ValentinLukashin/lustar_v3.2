using nlApplication;
using nlDataMaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlInputComboList'
    /// </summary>
    /// <remarks>Поле выбора данных из выпадающего списка</remarks>
    public class crlInputFormSearch : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cLabelCaption
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Button; /// Назвначение вида надписи-заголовка - 'Кнопка' 
                _cLabelCaption.__eMouseClickLeft += cLabelCaption__eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += cLabelCaption__eMouseClickRight;
            }
            fLabelCaptionStatus = _cLabelCaption.__fLabelType_; /// Сохранение установленного статуса надписи-заголовка
            // _cLabelValue
            {
                _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("Значение не определено");
                _cLabelValue.Visible = false;
            }
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.TextChanged += cSearch_TextChanged;
                _cInput.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                _cInput.__eInteractiveChanged += _cInput___eInteractiveChanged;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }

        private void _cInput___eInteractiveChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра
        }

        /// <summary>Выполняется при выборе надписи левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cLabelCaption__eMouseClickLeft(object sender, EventArgs e)
        {
            crlForm vForm = FindForm() as crlForm;
            if (vForm != null & __oFormSelect != null)
            {
                switch (__fFormSelecltedType)
                {
                    case FORMSELECTEDTYPES.FormGrid:
                        crlFormGrid vFormGrid = (crlFormGrid)Activator.CreateInstance(__oFormSelect);
                        /// Восстановить vFormFilter._cAreaFilter._fFormNameParent = vForm.Name;
                        (vFormGrid as crlFormGrid).ShowDialog();
                        if((vFormGrid as crlForm).__fClosedByXButtonOrAltF4_ == false)
                        {
                            fValue = vFormGrid.__cAreaGrid.__fRecordClue_;
                            DataTable vDataTableGrid = __oEssence._mRecord(fValue);
                            if (vDataTableGrid.Rows.Count > 0)
                            {
                                _cInput.Text = Convert.ToString(vDataTableGrid.Rows[0]["des" + __oEssence.__fTableName]).Trim();
                                _cLabelValue.Text = _cInput.Text;
                                if (__eOnInteractivatChange != null)
                                    __eOnInteractivatChange(this, new EventArgs());
                            }
                        }
                        break;
                    case FORMSELECTEDTYPES.FormGridFolder:
                        crlFormGridFolder vFormGridFolder = (crlFormGridFolder)Activator.CreateInstance(__oFormSelect);
                        /// Восстановить vFormFilter._cAreaFilter._fFormNameParent = vForm.Name;
                        (vFormGridFolder as crlFormGridFolder).ShowDialog();
                        if ((vFormGridFolder as crlForm).__fClosedByXButtonOrAltF4_ == false)
                        {
                            fValue = vFormGridFolder.__cAreaGridFolder.__fRecordClue;
                            DataTable vDataTableGridFolder = __oEssence._mRecord(fValue);
                            if (vDataTableGridFolder.Rows.Count > 0)
                        {
                            _cInput.Text = Convert.ToString(vDataTableGridFolder.Rows[0]["des" + __oEssence.__fTableName]).Trim();
                            _cLabelValue.Text = _cInput.Text;
                            if (__eOnInteractivatChange != null)
                                __eOnInteractivatChange(this, new EventArgs());
                        }
                        }
                        break;
                }
            }
            else
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Форма для построения выбора значений из справочника не определена");
                vError.__fProcedure = _fClassNameFull + "__cLabelCaption__eMouseClickLeft(object, EventsArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
            }

            _cInput.Focus();
        }
        /// <summary>Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cLabelCaption__eMouseClickRight(object sender, EventArgs e)
        {
            //_cLabelValue.Text = crlApplication.__oTunes.__mTranslate("Значение не определено");
            fValue = 0; /// Значение приравниватся [0]
            _cInput.Text = ""; /// Очищается название искомого справочника
            __fCheckStatus_ = false; /// !!! Выключается использование фильтра
            _cInput.Focus(); /// Перемещается курсор в поле ввода

        }
        /// <summary>Выполняется при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSearch_TextChanged(object sender, EventArgs e)
        {
            if (_cInput.Text.EndsWith(" ") == true)
            {
                if (_cInput.Text.StartsWith("0") == true)
                {
                    try
                    {
                        fValue = crlApplication.__oData.__mClueByCode(__oEssence.__fTableName, Convert.ToInt32(_cInput.Text.Trim().Substring(1)));
                        _cInput.Text = crlApplication.__oData.__mNameByClue(__oEssence.__fTableName, fValue);
                    }
                    catch
                    {
                        fValue = 0;
                    }
                } /// Поиск справочника по учетному коду
                else
                {
                    crlFormSearch vFormSearch = new crlFormSearch();
                    vFormSearch.Text = fFormSearchCaption;
                    vFormSearch._cAreaSearch.__fColumnsList_ = fColumnsList;
                    vFormSearch._cAreaSearch.__fFieldsCharList = __fFieldsCharList;
                    vFormSearch._cAreaSearch.__fFieldCode = __fFieldCode;
                    vFormSearch._cAreaSearch.__fEssence_ = __oEssence;
                    vFormSearch._cAreaSearch.__mColumnsBuild();
                    vFormSearch._cAreaSearch.__fStringSearchText_ = _cInput.Text;
                    vFormSearch._cAreaSearch.__fStringSearchSelectionStart_ = vFormSearch._cAreaSearch.__fStringSearchText_.Trim().Length;
                    vFormSearch._cAreaSearch.__fStringSearchSelectionLength_ = 0;
                    vFormSearch._cAreaSearch.__mStringSearchFocus();

                    (vFormSearch as crlFormSearch).ShowDialog();
                    if (vFormSearch._cAreaSearch.__fValueClue > 0)
                    {
                        _cInput.Text = vFormSearch._cAreaSearch.__fValueString.Trim();
                        _cLabelValue.Text = _cInput.Text;
                        fValue = vFormSearch._cAreaSearch.__fValueClue;
                        if (__eOnInteractivatChange != null)
                            __eOnInteractivatChange(this, new EventArgs());
                    }
                }
            }
            else
            {
                if (__eOnInteractivatChange != null)
                    __eOnInteractivatChange(this, new EventArgs());
            }

        }
        
        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Добавление колонки
        /// </summary>
        /// <param name="pCaption">Заголовок колонки</param>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pReadOnly">Атрибут "Только чтение"</param>
        /// <param name="pVisible">Видимость колонки</param>
        /// <param name="pType">Вид колонки</param>
        /// <returns>[true] - Колонка добавлена, иначе - [false]</returns>
        public void __mColumnAdd(string pCaption, string pFieldName, bool pReadOnly, bool pVisible, string pType)
        {
            crlDataGridColumn vColumn = new crlDataGridColumn();
            vColumn.__fCaption = pCaption;
            vColumn.__fField = pFieldName;
            vColumn.__fReadOnly = pReadOnly;
            vColumn.__fVisible = pVisible;
            vColumn.__fType = pType;
            fColumnsList.Add(vColumn);
        }
        /// <summary>Сброс значения
        /// </summary>
        public void __mValueClear()
        {
            fValue = 0;
            _cInput.Text = "";
            _cLabelValue.Text = "";
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ    

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Список колей по которым будет вестись поиск
        /// </summary>
        public ArrayList __fFieldsCharList = new ArrayList();
        /// <summary>
        /// Название поля учетного кодо
        /// </summary>
        public string __fFieldCode = "";
        public FORMSELECTEDTYPES __fFormSelecltedType = FORMSELECTEDTYPES.FormGrid;

        #endregion Атрибуты

        #region - Компоненты

        /// <summary>
        /// Поле ввода символьных данных
        /// </summary>
        protected crlComponentChar _cInput = new crlComponentChar();

        #endregion Компоненты

        #region = Объекты

        /// <summary>Форма для выбора записи
        /// </summary>
        public Type __oFormSelect;
        /// <summary>Сущность данных
        /// </summary>
        public datUnitEssence __oEssence;

        #endregion - Объекты

        #region = Специальные

        /// <summary>
        /// Значение контрола
        /// </summary>
        private int fValue = 0;
        /// <summary>
        /// Список отображаемых колонок
        /// </summary>
        private List<crlDataGridColumn> fColumnsList = new List<crlDataGridColumn>();
        /// <summary>
        /// Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;
        /// <summary>
        /// Заголовок формы для поиска без перевода
        /// </summary>
        private string fFormSearchCaptionNotTranslate = "";
        /// <summary>
        /// Заголовок формы для поиска
        /// </summary>
        private string fFormSearchCaption = "";

        #endregion Специальные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Дотупность контрола
        /// </summary>
        public override bool __fEnabled_ 
        { 
            get => base.__fEnabled_;
            set
            {
                base.__fEnabled_ = value;
                _cInput.Visible = value;
                if(value == true)
                {
                    _cLabelCaption.__fLabelType_ = fLabelCaptionStatus;
                }
                else
                {
                    _cLabelCaption.__fLabelType_ = LABELTYPES.Normal;
                    if(_cInput.Text.Length > 0)
                        _cLabelCaption.Text = _cInput.Text;
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>
        /// Условие фильтра
        /// </summary>
        public override string __fFilterExpression_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                string vFieldNameWithPrefix = ""; // Название поля с префиксом таблицы
                if (__fTableAlias.Trim().Length > 0)
                { /// Добавление префикса таблицы если он указан
                    vFieldNameWithPrefix = __fTableAlias + "." + __fFieldName;
                }
                else
                { /// Префикс таблицы не указан
                    vFieldNameWithPrefix = __fFieldName;
                }

                vReturn = vFieldNameWithPrefix + "=" + fValue.ToString();

                return vReturn;
            }
        }
        /// <summary>
        /// Получение условия фильтра для отображения пользователю
        /// </summary>
        public override string __fFilterMessage_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                if (_cCheck.Checked == true)
                {
                    vReturn = __fCaption_ + ": " + _cInput.__fValue_;
                }

                return vReturn;
            }
        }
        /// <summary>
        /// Значение контрола
        /// </summary>
        public override object __fValue_
        {
            get { return fValue; }
            set
            {
                fValue = Convert.ToInt32(value);
                if (fValue > 0)
                {
                    DataTable vDataTable = __oEssence._mRecord(fValue);
                    _cInput.Text = Convert.ToString(vDataTable.Rows[0]["des" + __oEssence.__fTableName]).Trim();
                    _cLabelValue.Text = _cInput.Text;
                }
            }
        }
        /// <summary>
        /// Заголовок формы для поиска
        /// </summary>
        /// <remarks>Отображаемый текст переводиться на язык интерфейса. Возвращается не переведенный текст</remarks>
        public string __fFormSearchCaption_
        {
            get { return fFormSearchCaptionNotTranslate; }
            set 
            {
                fFormSearchCaptionNotTranslate = value;
                fFormSearchCaption = crlApplication.__oTunes.__mTranslate(value);
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        public event EventHandler __eOnInteractivatChange;

        #endregion = СОБЫТИЯ
    }
}
