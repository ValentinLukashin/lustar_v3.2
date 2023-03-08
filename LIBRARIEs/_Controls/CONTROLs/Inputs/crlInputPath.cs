using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlInputPath'
    /// </summary>
    /// <remarks>Поле выбора пути</remarks>
    public class crlInputPath : crlInput
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// * Загрузка контрола
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
                _cLabelCaption.__fCaption_ = "Путь";
                _cLabelCaption.__fLabelType_ = LABELTYPES.Button;
                _cLabelCaption.__eMouseClickLeft += _cLabelCaption___eMouseClickLeft;
                _cLabelCaption.__eMouseClickRight += _cLabelCaption___eMouseClickRight;
            }
            // _cInput
            {
                _cInput.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                _cInput.Location = new Point(0, 0);
                _cInput.TextChanged += _cInput_TextChanged;
                _cInput.Width = Panel2.ClientSize.Width;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// * Возникает при клике левой клавиши мыши по надписи-заголовку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cLabelCaption___eMouseClickLeft(object sender, EventArgs e)
        {
            if (__fPathType == PATHTYPES.File)
            {
                OpenFileDialog vOpenFileDialog = new OpenFileDialog();
                vOpenFileDialog.InitialDirectory = _cInput.__fValue_.ToString();
                if (vOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _cInput.Text = vOpenFileDialog.FileName;
                }
            }
            if (__fPathType == PATHTYPES.Folder)
            {
                FolderBrowserDialog vFolderBrowserDialog = new FolderBrowserDialog();
                vFolderBrowserDialog.ShowNewFolderButton = true;
                if (vFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    _cInput.Text = vFolderBrowserDialog.SelectedPath;
                }
            }
        }
        /// <summary>
        /// * Возникает при клике правой клавиши мыши по надписи-заголовку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cLabelCaption___eMouseClickRight(object sender, EventArgs e)
        {
            _cInput.__fValue_ = "";
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// (m) Выполняется при изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput_TextChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра

            return;
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ    

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Индекс настройки
        /// </summary>
        /// <remarks>Используется в форме Изменение настроек приложения</remarks>
        public int __fTuneIndex = -1;
        /// <summary>
        /// * Вид пути обрабатываемый компонентом
        /// </summary>
        public PATHTYPES __fPathType = PATHTYPES.File;
        /// <summary>
        /// - Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion Атрибуты 

        #region - Компоненты

        /// <summary>
        /// * Поле ввода символьных данных
        /// </summary>
        protected crlComponentChar _cInput = new crlComponentChar();

        #endregion Компоненты

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
                    if (_cInput.Text.Length > 0)
                        _cLabelValue.Text = _cInput.Text;
                    else
                        _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>Обязательность заполнения
        /// </summary>
        public override FILLTYPES __fFillType_
        {
            get { return _cInput.__fFillType_; }
            set { _cInput.__fFillType_ = value; }
        }
        /// <summary>Многострочное использование
        /// </summary>
        public virtual bool __fMultiline_
        {
            get { return _cInput.Multiline; }
            set
            {
                _cInput.Multiline = value;
                if (_cInput.Multiline == true)
                {
                    _cInput.Dock = DockStyle.Fill;
                    _cInput.ScrollBars = ScrollBars.Both;
                    _cInput.WordWrap = false;
                }
                else
                {
                    //Height = fHeightNormal;
                    _cInput.Dock = DockStyle.None;
                    _cInput.ScrollBars = ScrollBars.None;
                    _cInput.WordWrap = true;
                }
            }
        }
        /// <summary>Количество отображаемых символов данных
        /// </summary>
        public virtual int __fSymbolsCount_
        {
            get { return _cInput.__fSymbolsCount_; }
            set { _cInput.__fSymbolsCount_ = value; }
        }
        /// <summary>Значение поля ввода
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
        /// <summary>Вид надписи
        /// </summary>
        public LABELTYPES __fLabelType_
        {
            get { return _cLabelCaption.__fLabelType_; }
            set { _cLabelCaption.__fLabelType_ = value; }
        }

        #endregion СВОЙСТВА    
    }
}
