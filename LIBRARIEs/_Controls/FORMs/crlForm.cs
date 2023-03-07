using nlApplication;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlForm'
    /// </summary>
    /// <remarks>Абстактный класс для построения всех форм</remarks>
    public abstract class crlForm : Form
    {
        #region = ДИЗАЙНЕРЫ

        public crlForm()
        {
            _mObjectAssembly();
        }

        #endregion = ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            SuspendLayout();

            #region Размещение компонентов

            Controls.Add(__cStatus);

            #endregion Размещение компонентов

            #region Настройка компонентов

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 261);
            KeyPreview = true; /// Разрешение обработки горячих клавиш вложенных контролов 
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Базовая форма";
            __oFileIni.__fFilePath = crlApplication.__oPathes.__mFileFormTunes();

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>Презентация объекта
        /// </summary>
        protected virtual void _mObjectPresentation()
        {
        }
        /// <summary>Выполняется при активации формы
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            BackColor = crlApplication.__oInterface.__mColor(COLORS.FormActive);
        }
        /// <summary>Выполняется при дезактивации формы
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            BackColor = crlApplication.__oInterface.__mColor(COLORS.FormDeactive);
        }
        /// <summary>Выполняется перед закрытием формы
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            __mTunesSave();
            base.OnClosing(e);
        }
        /// <summary>Выполняется после создания формы 
        /// </summary>
        protected override void OnCreateControl()
        {
            _mObjectPresentation();
            if (__mFormRightsCheck() == true)
            {
                base.OnCreateControl();
                __mTunesLoad();
            }
            else
                Top = 1000;

        }
        /// <summary> Выполняется при нажатии на клавиши клавиатуры
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (__fKeyEscapeLock == false)
                    Close();
            }
            base.OnKeyDown(e);
        }
        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == WM_SYSCOMMAND && msg.WParam.ToInt32() == SC_CLOSE)
                __fClosedByXButtonOrAltF4_ = true;

            base.WndProc(ref msg);
        }

        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        #region Настройки

        /// <summary>
        /// Загрузка настроек текущей формы из файла
        /// </summary>
        protected virtual void __mTunesLoad()
        {
            __mTunesLoad(Name);
        }
        /// <summary>
        /// Загрузка настроек указанной формы
        /// </summary>
        /// <param name="pFormName">'Name' формы</param>
        protected virtual void __mTunesLoad(string pFormName)
        {
            bool vTuneExists = true; // Настройки формы взяты мз настроечного файла
            Rectangle vRectangle = Screen.PrimaryScreen.Bounds;
            string vString = __oFileIni.__mValueRead(pFormName.ToUpper(), "Top");
            if (StartPosition != FormStartPosition.CenterParent & StartPosition != FormStartPosition.CenterScreen)
            {
                try
                {
                    if (vString.Length == 0)
                    {
                        vTuneExists = false;
                    } // Данных в файле нет
                    Top = Convert.ToInt32(vString);
                    if (Top < 0)
                        Top = 0;
                    if (Top > vRectangle.Size.Height)
                        Top = 0;
                }
                catch
                {
                    Top = 0;
                }
                vString = __oFileIni.__mValueRead(pFormName.ToUpper(), "Left");
                try
                {
                    Left = Convert.ToInt32(vString);
                    if (Left < 0)
                        Left = 0;
                    if (Left > vRectangle.Size.Width)
                        Left = 0;
                }
                catch
                {
                    Left = 0;
                }
            }
            if (FormBorderStyle == FormBorderStyle.Sizable)
            {
                vString = __oFileIni.__mValueRead(pFormName.ToUpper(), "Height");
                try
                {
                    Height = Convert.ToInt32(vString);
                    if (Height < MinimumSize.Height)
                        Height = MinimumSize.Height;
                }
                catch
                {
                    if (Height < MinimumSize.Height)
                        Height = MinimumSize.Height;
                }
                vString = __oFileIni.__mValueRead(pFormName.ToUpper(), "Width");
                try
                {
                    Width = Convert.ToInt32(vString);
                    if (Width < MinimumSize.Width)
                        Width = MinimumSize.Width;
                }
                catch
                {
                    if (Width < MinimumSize.Width)
                        Width = MinimumSize.Width;
                }
            }
            vString = __oFileIni.__mValueRead(pFormName.ToUpper(), "WindowState");
            try
            {
                switch (vString)
                {
                    case "Maximized":
                        WindowState = FormWindowState.Maximized;
                        break;
                    case "Minimized":
                        WindowState = FormWindowState.Minimized;
                        break;
                    case "Normal":
                        WindowState = FormWindowState.Normal;
                        break;
                }
            }
            catch { }
            if (vTuneExists == false)
            {
                WindowState = FormWindowState.Maximized;
            }
            {
                string vStringMinimumHeight = __oFileIni.__mValueRead(pFormName.ToUpper(), "MinimumHeight");
                int vIntMinimumHeight = 0;
                if (!(vStringMinimumHeight.Length == 0 | vStringMinimumHeight == "0"))
                    vIntMinimumHeight = Convert.ToInt32(vStringMinimumHeight);
                string vStringMinimumWidth = __oFileIni.__mValueRead(pFormName.ToUpper(), "MinimumWidth");
                int vIntMinimumWidth = 0;
                if (!(vStringMinimumWidth.Length == 0 | vStringMinimumWidth == "0"))
                    vIntMinimumWidth = Convert.ToInt32(vStringMinimumWidth);

                MinimumSize = new Size(vIntMinimumWidth, vIntMinimumHeight);
            } /// Сохранение минимальных размеров формы
            if (vTuneExists == false)
            {
                WindowState = FormWindowState.Maximized;
            }

        }
        /// <summary>
        /// Сохранение настроек текущей форрмы
        /// </summary>
        protected virtual void __mTunesSave()
        {
            __mTunesSave(Name);
        }
        /// <summary>
        /// Сохранение настроек указанной формы
        /// </summary>
        /// <param name="pFormName">'Name' формы</param>
        protected virtual void __mTunesSave(string pFormName)
        {
            if (WindowState == FormWindowState.Normal) // Сохраняются размеры нормальной формы
            {
                __oFileIni.__mValueWrite(Top.ToString(), pFormName.ToUpper(), "Top");
                __oFileIni.__mValueWrite(Left.ToString(), pFormName.ToUpper(), "Left");
                __oFileIni.__mValueWrite(Height.ToString(), pFormName.ToUpper(), "Height");
                __oFileIni.__mValueWrite(Width.ToString(), pFormName.ToUpper(), "Width");
            }

            __oFileIni.__mValueWrite(WindowState.ToString(), pFormName.ToUpper(), "WindowState");

            __oFileIni.__mValueWrite(MinimumSize.Height.ToString(), pFormName.ToUpper(), "MinimumHeight");
            __oFileIni.__mValueWrite(MinimumSize.Width.ToString(), pFormName.ToUpper(), "MinimumWidth");
        }

        #endregion Настройки

        public virtual bool __mFormRightsCheck()
        {
            return true;
        }

        /// <summary>Отображение облака сообщения
        /// </summary>
        /// <param name="pTitle">Заголовок облака</param>
        /// <param name="pMessage">Сообщение</param>
        public void __mBaloonMessage(Control pObject, string pMessage)
        {
            ToolTip vToolTip = new ToolTip();
            vToolTip.IsBalloon = true;
            vToolTip.ToolTipTitle = appApplication.__oTunes.__mTranslate("Ошибка ввода");
            vToolTip.ToolTipIcon = ToolTipIcon.None;
            vToolTip.UseFading = true;
            vToolTip.Show(string.Empty, pObject, 1000); // Для правильного позиционирования облака сообщения
            vToolTip.Show(pMessage, pObject, pObject.Width, pObject.Height);
        }
        /// <summary>Сборка выражения с параметрами и перевод выражения на язык интерфейса 
        /// </summary>
        /// <param name="pString">Текст</param>
        /// <param name="pParameters">Список дополнительных парамметров</param>
        public void __mCaptionBuilding(string pString, params object[] pParameters)
        {
            fTextWithOutTranslate = String.Format(pString, pParameters);
            Text = crlApplication.__oTunes.__mTranslate(pString, pParameters);
        }
        /// <summary>Вызов топика помощи связанного с формой
        /// </summary>
        public void __mHelp()
        {
            if (_fHelpFile.Length == 0)
                crlApplication.__oEventsHandler.__mHelp(_fHelpTopic);
            else
                crlApplication.__oEventsHandler.__mHelp(_fHelpFile, _fHelpTopic);
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Блокировка клавиши 'Escape'
        /// </summary>
        public bool __fKeyEscapeLock = false;
        /// <summary>Название права формы
        /// </summary>
        protected string __fFormRightName = "";

        #endregion - Атрибуты

        #region - Внутренние

        /// <summary>Строка заголовка без перевода
        /// </summary>
        private string fTextWithOutTranslate = "";
        /// <summary>Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";
        /// <summary>Название топика помощи связанного с формой
        /// </summary>
        protected string _fHelpTopic = "";
        /// <summary>Имя файла помощи в котором находиться топик помощи
        /// </summary>
        protected string _fHelpFile = "";

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Компонент для отображения состояния формы
        /// </summary>
        public crlPanelStatus __cStatus = new crlPanelStatus();

        #endregion - Компоненты

        #region - Константы

        private const int SC_CLOSE = 0xF060;
        private const int WM_SYSCOMMAND = 0x0112;

        #endregion - Константы

        #region - Объекты

        /// <summary>Объект для работы с инициализационными файлами
        /// </summary>
        public appFileIni __oFileIni = new appFileIni();

        #endregion - Объекты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        public bool __fClosedByXButtonOrAltF4_ { get; private set; }

        #endregion = СВОЙСТВА
    }
}
