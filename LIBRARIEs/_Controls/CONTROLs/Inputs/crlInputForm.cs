using nlApplication;
using nlDataMaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlInputForm'
    /// </summary>
    /// <remarks>Поле выбора данных из вызываемой формы</remarks>
    public class crlInputForm : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cLabelCaption
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Button;
                _cLabelCaption.__eMouseClickLeft += __cLabelCaption__eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += __cLabelCaption__eMouseClickRight;
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
                _cInput.TextChanged += _cSearch_TextChanged;
                _cInput.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }
        /// <summary>Выполняется при выборе надписи правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cLabelCaption__eMouseClickRight(object sender, EventArgs e)
        {
            _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("Значение не определено");
            //_cInput.Text = "0";
            _cInput.__fValue_ = 0;
        }
        /// <summary>Выполняется при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cSearch_TextChanged(object sender, EventArgs e)
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
                }
                else
                {
                    crlForm vForm = FindForm() as crlForm;
                    crlFormSearch vFormSearch = new crlFormSearch();
                    vFormSearch.Text = _fFormSearchCaption;
                    vFormSearch._cAreaSearch.__fColumnsList_ = fColumnsList;
                    vFormSearch._cAreaSearch.__fFieldsCharList = _fFieldsList;
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
                        __fValue_ = vFormSearch._cAreaSearch.__fValueClue;
                    }
                }
            }
        }
        /// <summary>Выполняется при проверке ввода данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (__fFillType_ == FILLTYPES.Necessarily)
            {
                if (Text.Length == 0)
                {
                    (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Поле должно быть обязательно заполненным"));
                    e.Cancel = true;
                }
            }
            base.OnValidating(e);
        }

        #endregion Объект

        /// <summary>Выполняется при выборе надписи левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cLabelCaption__eMouseClickLeft(object sender, EventArgs e)
        {
            crlForm vForm = FindForm() as crlForm;
            if (vForm != null & oFormSearch != null)
            {
                crlFormSearch vFormSearch = (crlFormSearch)Activator.CreateInstance(oFormSearch);
                /// Восстановить vFormFilter._cAreaFilter._fFormNameParent = vForm.Name;
                (vFormSearch as crlFormSearch).ShowDialog();
                fValue = vFormSearch._cAreaSearch.__fValueClue;
                _cLabelValue.Text = vFormSearch._cAreaSearch.__fValueString;
            }
            else
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Форма для построения выбора значений из справочника не определена");
                vError.__fProcedure = _fClassNameFull + "__cLabelCaption__eMouseClickLeft(object, EventsArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
            }
        }
        #endregion - Поведение

        #endregion = МЕТОДЫ    

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Список колей по которым будет вестись поиск
        /// </summary>
        public ArrayList _fFieldsList = new ArrayList();
        /// <summary>Заголовок формы для поиска
        /// </summary>
        public string _fFormSearchCaption = "";

        #endregion - Атрибуты

        #region - Внутренние

        /// <summary>Значение контрола
        /// </summary>
        private int fValue = 0;
        /// <summary>Список отображаемых колонок
        /// </summary>
        private List<crlDataGridColumn> fColumnsList = new List<crlDataGridColumn>();
        /// <summary>Обязательность заполнения
        /// </summary>
#pragma warning disable CS0414 // Полю "crlInputForm.fFillType" присвоено значение, но оно ни разу не использовано.
        private FILLTYPES fFillType = FILLTYPES.None;
#pragma warning restore CS0414 // Полю "crlInputForm.fFillType" присвоено значение, но оно ни разу не использовано.
        /// <summary>Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Поле ввода символьных данных
        /// </summary>
        protected crlComponentNumberInt _cInput = new crlComponentNumberInt();

        #endregion - Компоненты

        #region - Объекты

        /// <summary>Сущность данных
        /// </summary>
        public datUnitEssence __oEssence;
        /// <summary>Форма для выполнения поиска
        /// </summary>
        public Type _oFormSelect;
        /// <summary>Форма для выполнения поиска
        /// </summary>
        private Type oFormSearch = typeof(crlFormSearch);

        #endregion - Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Доступность контрола
        /// </summary>
        public override bool __fEnabled_
        {
            get { return base.__fEnabled_; }
            set
            {
                base.__fEnabled_ = value;
                _cInput.Visible = value;
                if (value == true)
                {
                    _cLabelCaption.__fLabelType_ = fLabelCaptionStatus;
                }
                else
                {
                    _cLabelCaption.__fLabelType_ = LABELTYPES.Normal;
                    _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        #endregion = СВОЙСТВА
    }
}
