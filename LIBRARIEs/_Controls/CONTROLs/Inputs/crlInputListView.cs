using nlDataMaster;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlInputListView'
    /// </summary>
    public class crlInputListView : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Height = 50;

            // _cLabel
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Button;
                _cLabelCaption.__eMouseClickLeft += _cLabelCaption___eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += _cLabelCaption___eMouseClickRight;
            }
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                //_cInput.__eValueInteractiveChanged +
                _cInput.Dock = DockStyle.Fill;
                _cInput.DoubleClick += _cInput_DoubleClick;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }

        /// <summary>Выполняется при двойном клике по записи в поле ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput_DoubleClick(object sender, EventArgs e)
        {
            if (__eInput_DoubleClick != null)
                __eInput_DoubleClick(this, new EventArgs());
        }

        private void _cLabelCaption___eMouseClickLeft(object sender, EventArgs e)
        {
            //crlForm vForm = FindForm() as crlForm;
            //if (vForm != null & __oFormSelect != null)
            //{
            //    crlFormRecord vFormSelect = (crlFormRecord)Activator.CreateInstance(__oFormSelect);
            //    /// Восстановить vFormFilter._cAreaFilter._fFormNameParent = vForm.Name;
            //    (vFormSelect as crlFormRecord).ShowDialog();
            //    //fValue = vFormSelect.__cAreaGrid.__pRecordClue;
            //    //DataTable vDataTable = __oEssence._mRecord(fValue);
            //    //_cInput.Text = Convert.ToString(vDataTable.Rows[0]["des" + __oEssence.__fTableName]).Trim();
            //    //_cLabelValue.Text = _cInput.Text;
            //    //if (__eOnInteractivatChange != null)
            //    //    __eOnInteractivatChange(this, new EventArgs());
            //}
            //else
            //{
            //    appError vError = new appError();
            //    vError.__fErrorsType = ERRORSTYPES.Programming;
            //    vError.__mMessageBuild("Форма для построения выбора значений из справочника не определена");
            //    vError.__fProcedure = _fClassNameFull + "__cLabelCaption__eMouseClickLeft(object, EventsArgs)";
            //    crlApplication.__oErrorsHandler.__mShow(vError);
            //}
            if (__eLabelCaption_MouseClickLeft != null)
                __eLabelCaption_MouseClickLeft(this, new EventArgs());

        }

        private void _cLabelCaption___eMouseClickRight(object sender, EventArgs e)
        {
            //if (crlApplication.__oMessages.__mShow(nlApplication.MESSAGESTYPES.Question, "Удалить {0}", __fEssenceObjectName, false, "", "") == DialogResult.Yes)
            //{ 
            //}
            if (__eLabelCaption_MouseClickRight != null)
                __eLabelCaption_MouseClickRight(this, new EventArgs());
        }

        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Добавление колонки
        /// </summary>
        /// <param name="pFieldName">Название поля</param>
        /// <param name="pCaptionText">Название колонки</param>
        /// <param name="pColumnWidth">Ширина колонок</param>
        /// <param name="vHorizontalAlignment">Выравнивание текста данных</param>
        public virtual void __mColumnAdd(string pFieldName, string pCaptionText, int pColumnWidth, HorizontalAlignment vHorizontalAlignment)
        {
            _cInput.__mColumnAdd(pFieldName, pCaptionText, pColumnWidth, vHorizontalAlignment);
        }
        /// <summary>Удаление выбранной записи
        /// </summary>
        public void __mDataDelete()
        {
            _cInput.__mDataDelete();
        }
        /// <summary>Загрузка и отображение данных
        /// </summary>
        /// <param name="pLoad">[true] - загрузка из источника данных, иначе обновление</param>
        public virtual void __mDataRefresh(bool pLoad)
        {
            _cInput.__mDataRefresh(pLoad);
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region Атрибуты

        /// <summary>Название объекта мкщности
        /// </summary>
        public string __fEssenceObjectName = "";
        /// <summary>Заголовок надписи
        /// </summary>
        public string __fFormSearchCaption = "";

        #endregion Атрибуты

        #region - Внутренние
        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Поле ввода множественных значений
        /// </summary>
        protected crlComponentListView _cInput = new crlComponentListView();

        #endregion - Компоненты

        #region - Объект

        /// <summary>Форма для выбора записи
        /// </summary>
        public Type __oFormSelect;

        #endregion - Объект

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Курсор с данными
        /// </summary>
        public DataTable __fDataTable
        {
            get { return _cInput.__oDataTable; }
            set { _cInput.__oDataTable = value; }
        }
        /// <summary>Сущность данных
        /// </summary>
        public datUnitEssence __fEssence
        {
            get { return _cInput.__oEssence; }
            set { _cInput.__oEssence = value; }
        }
        /// <summary>Идентификатор записи в родительской таблице
        /// </summary>
        public int __fRecordClueParent
        {
            get { return _cInput.__fRecordClueParent; }
            set { _cInput.__fRecordClueParent = value; }
        }
        /// <summary>Название поля идентификатора записи в родительской таблице
        /// </summary>
        public string __fRecordNameParent
        {
            get { return _cInput.__fRecordNameParent; }
            set { _cInput.__fRecordNameParent = value; }
        }
        
        public int __fRowIndex
        {
            get { return _cInput.__fRowIndex; }
            set { _cInput.__fRowIndex = value; }
        }

        /// <summary>Псевдоним главной таблицы в запросе
        /// </summary>
        public string __fTableMainAlias
        {
            get { return _cInput.__fTableMainAlias; }
            set { _cInput.__fTableMainAlias = value; }
        }

        #endregion = СВОЙСТВА

        #region = СОБЫТИЯ

        public event EventHandler __eLabelCaption_MouseClickLeft;
        public event EventHandler __eLabelCaption_MouseClickRight;
        /// <summary>Возникает при двойном клике по поллю ввода
        /// </summary>
        public event EventHandler __eInput_DoubleClick;

        #endregion = СОБЫТИЯ
    }
}
