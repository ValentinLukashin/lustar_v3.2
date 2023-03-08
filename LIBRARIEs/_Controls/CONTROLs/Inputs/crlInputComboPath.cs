using nlApplication;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputComboPath'
    /// </summary>
    /// <remarks>Поле выбора пути из выпадающего списка</remarks>
    public class crlInputComboPath : crlInput
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

            Panel2.Controls.Add(_cInput);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            // _cLabelCaption
            {
                _cLabelCaption.__fCaption_ = "Путь";
                _cLabelCaption.__fLabelType_ = LABELTYPES.Button;
                _cLabelCaption.__eMouseClickLeft += mLabelCaption_eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += mLabelCaption_eMouseClickRight;
            }
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.TextChanged += mInput_TextChanged;
                _cInput.__fScaleType_ = SCALETYPEs.Anchor;
                _cInput.__eValueInteractiveChanged += mInput_eValueChangedByUser;
                _cInput.__eValueProgrammaticChanged += mInput_eValueChangedByProgram;
            }

            #endregion Настройка компонентов

            ResumeLayout();

            return;
        }

        /// <summary>
        /// Возникает при клике левой клавиши мыши по надписи-заголовку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelCaption_eMouseClickLeft(object sender, EventArgs e)
        {
            if (__fPathType == PATHTYPES.File)
            {
                OpenFileDialog vOpenFileDialog = new OpenFileDialog();
                vOpenFileDialog.InitialDirectory = _cInput.__fValue_.ToString();
                if (vOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _cInput.__mItemAdd(vOpenFileDialog.FileName.Trim());
                    _cInput.__mDataRefresh();
                }
            }
            if (__fPathType == PATHTYPES.Folder)
            {
                FolderBrowserDialog vFolderBrowserDialog = new FolderBrowserDialog();
                vFolderBrowserDialog.ShowNewFolderButton = true;
                if (vFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    _cInput.__mItemAdd(vFolderBrowserDialog.SelectedPath.Trim());
                    _cInput.__mDataRefresh();
                }
            }
        }
        /// <summary>
        /// Возникает при клике правой клавиши мыши по надписи-заголовку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelCaption_eMouseClickRight(object sender, EventArgs e)
        {
            _cInput.__fValue_ = "";
        }
        /// <summary>
        /// Выполняется при изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mInput_TextChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра

            return;
        }
        /// <summary>
        /// Выполняется при изменении введенных данных программой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mInput_eValueChangedByProgram(object sender, EventArgs e)
        {
            if (__eValueChangedByProgram != null)
                __eValueChangedByProgram(this, new EventArgs());
        }
        /// <summary>
        /// Выполняется при изменении введенных данных пользователем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mInput_eValueChangedByUser(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра
            if (__eValueChangedByUser != null)
                __eValueChangedByUser(this, new EventArgs());
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Обновление отображаемых данных
        /// </summary>
        public virtual void __mDataRefresh()
        {
            _cInput.__mDataRefresh();
            _cLabelValue.Text = _cInput.Text;

            return;
        }
        /// <summary>
        /// Перевод фокуса на поле ввода
        /// </summary>
        public override void __mInputFocus()
        {
            _cInput.Focus();

            return;
        }
        /// <summary>
        /// Добавление значения в конец списка значений компонента
        /// </summary>
        /// <param name="pValue">Добавляемое значение</param>
        /// <returns>Идентификатор добавляемой записи, положительный - из таблицы, отрицательный - назначаемый компонентом</returns>
        public virtual int __mItemAdd(string pValue)
        {
            return _cInput.__mItemAdd(pValue);
        }
        /// <summary>
        /// Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public virtual bool __mItemsAdd(ArrayList pValueS)
        {
            return _cInput.__mItemsAdd(pValueS);
        }
        /// <summary>
        /// Добавление списка новых значений
        /// </summary>
        /// <param name="pValueS">Список значений в порядке определения индексов</param>
        /// <returns>[true] - Значение добавлено, иначе - [false]</returns>
        public virtual bool __mItemsAdd(params string[] pValueS)
        {
            return _cInput.__mItemsAdd(pValueS);
        }
        /// <summary>
        /// Очистка всех данных и подготовка к вводу новых данных
        /// </summary>
        public virtual void __mItemsClear()
        {
            _cInput.__mItemsClear();
        }
        /// <summary>
        /// Загрузка данных из сущности данных
        /// </summary>
        /// <param name="pWhereExpression">Выражение выбора получаемых данных</param>
        /// <param name="pOrderExpression">Выражение сортировки получаемых данных</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public virtual bool __mItemsEssenceLoad(string pWhereExpression, string pOrderExpression)
        {
            return _cInput.__mItemsEssenceLoad(pWhereExpression, pOrderExpression);
        }
        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="pPrefix">Префикс для выбора данных из файла настроек форм</param>
        public virtual void __mItemsLoadFromFile(string pPrefix)
        {
            __mItemsClear();
            crlForm vForm = FindForm() as crlForm; // Объект формы
            appFileIni vFileIni = vForm.__oFileIni; // Настроенный объект формы для работы с 'ini' файлами
            ArrayList vParametersList = vFileIni.__mParametersList(vForm.Name.ToUpper());
            foreach (string vParameter in vParametersList)
            {
                if (vParameter.StartsWith(pPrefix) == true)
                {
                    __mItemsAdd(vFileIni.__mValueRead(vForm.Name.ToUpper(), vParameter));
                }
            }
            __mDataRefresh();
        }
        /// <summary>
        /// Загрузка данных из {DataTable}, со столбцами clu(идентификатор) и des(название)
        /// </summary>
        /// <param name="pDataTable">таблица</param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public virtual bool __mItemsLoadFromDataTable(DataTable pDataTable)
        {
            return _cInput.__mItemsLoad(pDataTable);
        }
        /// <summary>
        /// Сохранение данных в файла
        /// </summary>
        /// <param name="pPrefix">Префикс для выбора данных из файла настроек форм</param>
        public virtual void __mItemsSaveToFile(string pPrefix)
        {
            crlForm vForm = FindForm() as crlForm; // Объект формы
            appFileIni vFileIni = vForm.__oFileIni; // Настроенный объект формы для работы с 'ini' файлами
            ArrayList vParametersList = vFileIni.__mParametersList(vForm.Name);
            int vCounter = 0;
            /// * Удаление старых параметров
            foreach (string vParameter in vParametersList)
            {
                if (vParameter.StartsWith(pPrefix) == true)
                {
                    vFileIni.__mParameterDelete(vForm.Name, vParameter);
                }
            }
            /// * Запись текущих данных
            foreach (appUnitItem vItem in _cInput.__fItemList_)
            {
                vFileIni.__mValueWrite(vItem.__fValue_.ToString(), vForm.Name, pPrefix + vCounter.ToString());
                vCounter++;
            }
        }
        /// <summary>
        /// Получение индекса значения по идентификатору значения
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns></returns>
        public virtual int __mGetIndexByClue(int pClue)
        {
            return _cInput.__mGetIndexByClue(pClue);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Вид пути обрабатываемый компонентом
        /// </summary>
        public PATHTYPES __fPathType = PATHTYPES.File;

        #endregion Атрибуты

        #region - Компоненты

        /// <summary>
        /// * Поле ввода символьных данных
        /// </summary>
        protected crlComponentCombo _cInput = new crlComponentCombo();

        #endregion Компоненты

        #region - Служебные

        /// <summary>
        /// Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Доступность контрола
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
                    if (_cInput.Text.Length > 0)
                        _cLabelValue.Text = _cInput.Text;
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public override FILLTYPES __fFillType_
        {
            get { return _cInput.__fFillType_; }
            set { _cInput.__fFillType_ = value; }
        }
        /// <summary>
        /// Значение поля ввода
        /// </summary>
        public override object __fValue_
        {
            get { return _cInput.Text; }
            set
            {
                _cInput.Text = value.ToString().Trim();
                _cLabelValue.Text = _cInput.Text;  // Запись значения по умолчанию
            }
        }
        /// <summary>
        /// Вид надписи
        /// </summary>
        public LABELTYPES __fLabelType_
        {
            get { return _cLabelCaption.__fLabelType_; }
            set { _cLabelCaption.__fLabelType_ = value; }
        }

        #endregion СВОЙСТВА   

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при ручном изменении данных
        /// </summary>
        public event EventHandler __eValueChangedByUser;
        /// <summary>
        /// Возникает при изменении данных программой
        /// </summary>
        public event EventHandler __eValueChangedByProgram;

        #endregion СОБЫТИЯ
    }
}
