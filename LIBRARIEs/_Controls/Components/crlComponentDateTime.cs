using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentDateTime'
    /// </summary>
    /// <remarks>Компонент для выбора даты-времени</remarks>
    public class crlComponentDateTime : DateTimePicker
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentDateTime()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            SuspendLayout();

            #region Описание компонента

            DropDownAlign = LeftRightAlignment.Left;
            Format = DateTimePickerFormat.Custom;
            MinDate = Convert.ToDateTime("01.01.1900");
            MaxDate = Convert.ToDateTime("01.01.3000");
            this.CalendarTitleBackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
            this.CalendarTitleForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
            ShowUpDown = false;
            //Size = new Size(110, 20);

            BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
            Font = crlApplication.__oInterface.__mFont(FONTS.Data);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
            Format = DateTimePickerFormat.Custom;
            __fDateTimeType_ = DATETIMETYPES.DateTime;

            #endregion Описание компонента

            Invalidate();

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при проверке ввода данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (fFillType == FILLTYPES.Necessarily)
            {
                if (Convert.ToDateTime(__fValue_) == new DateTime(1900, 1, 1))
                {
                    (FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Поле должно быть обязательно заполненным"));
                    e.Cancel = true;
                }
            }
            base.OnValidating(e);

            return;
        }
        /// <summary>
        /// Выполняется после изменения значения
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            __fValue_ = Value;
            if (fKeyPressed == true)
                if (__eValueInteractiveChanged != null)
                    __eValueInteractiveChanged(this, new EventArgs());
            else
                if (__eValueProgrammativChanged != null)
                    __eValueProgrammativChanged(this, new EventArgs());

            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            fKeyPressed = true;
            base.OnEnter(e);

            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            fKeyPressed = true;
            base.OnKeyDown(e);

            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            fKeyPressed = false;

            return;
        }
        /// <summary>
        /// Обработка сообщений Windows
        /// </summary>
        /// <param name="pMessage">{Message}</param>
        protected override void WndProc(ref Message pMessage)
        {
            if (pMessage.Msg == 0x14) // WM_ERASEBKGND
            {
                using (var vGarphics = Graphics.FromHdc(pMessage.WParam))
                {
                    using (var vBrush = new SolidBrush(BackColor))
                    {
                        vGarphics.FillRectangle(vBrush, ClientRectangle);
                    }
                }
                return;
            }

            base.WndProc(ref pMessage);
        }

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Разрешение скрывать нулевое значение MS SQL сервера
        /// </summary>
        public bool __fHideSqlEmptyValue = true;

        #endregion Атрибуты

        #region = Внутренние

        /// <summary>
        /// Вид отображения даты-времени
        /// </summary>
        private DATETIMETYPES fDateTimeType = DATETIMETYPES.DateTime;
        /// <summary>
        /// Текущий формат отображения данных
        /// </summary>
        private string fCustomFormat = "";
        /// <summary>
        /// Формат для отображения пустой даты
        /// </summary>
        private string fCustomFormatNull = " ";
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        private FILLTYPES fFillType = FILLTYPES.None;
        /// <summary>
        /// Хранение в источнике ввиде BigInt
        /// </summary>
        private bool fStorageInTicks = false;
        /// <summary>
        /// Наличие нажатой клавиши
        /// </summary>
        private bool fKeyPressed = false;

        #endregion Внутренние

        #region = Служебные

        /// <summary>
        /// Полное название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Вид отображения даты - времени
        /// </summary>
        public DATETIMETYPES __fDateTimeType_
        {
            get { return fDateTimeType; }
            set
            {
                fDateTimeType = value;
                switch (fDateTimeType)
                {
                    case DATETIMETYPES.Date:
                        CustomFormat = "dd.MM.yyyy";
                        Width = 95;
                        break;
                    case DATETIMETYPES.DateTime:
                        CustomFormat = "dd.MM.yyyy hh:mm";
                        Width = 140;
                        break;
                }
                fCustomFormat = CustomFormat;
                if (__eDateTimeTypeChanged != null)
                    __eDateTimeTypeChanged(this, new EventArgs());

            }
        }
        /// <summary>
        /// Обязательность заполнения
        /// </summary>
        public FILLTYPES __fFillType_
        {
            get { return fFillType; }
            set
            {
                fFillType = value;
                //if (fEnabled == true)
                //{
                if (fFillType == FILLTYPES.None)
                    BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
                else
                    BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBackNecessarily);
                //}
                //else
                //BackColor = crlApplication._oInterface._mColor(Enums.COLORS.DataBackDisabled);
            }
        }
        /// <summary>
        /// Значение контрола
        /// </summary>
        public DateTime __fValue_
        {
            get
            {
                DateTime vValue = new DateTime();
                if (Value > MinDate)
                    vValue = Value;
                else
                {
                    if (__fHideSqlEmptyValue)
                        vValue = MinDate;
                    else
                        vValue = Value;
                }
                return vValue;
            }
            set
            {
                DateTime vValue = Convert.ToDateTime(value);
                if (vValue < MinDate)
                    vValue = MinDate;
                Value = vValue;
                if (Value <= MinDate)
                {
                    if (__fHideSqlEmptyValue == true)
                        CustomFormat = fCustomFormatNull;
                }
                else
                    CustomFormat = fCustomFormat;
            }
        }
        /// <summary>
        /// Значение компонента в тиках
        /// </summary>
        public long __fTicks_
        {
            get { return __fValue_.Ticks; }
            set { __fValue_ = new DateTime(value); }
        }
        /// <summary>
        /// Хранение в базе данных в типе BigInt
        /// </summary>
        public bool __fValueInTicks_
        {
            get { return fStorageInTicks; }
            set { fStorageInTicks = value; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при изменении типа отображаемых данных
        /// </summary>
        public event EventHandler __eDateTimeTypeChanged;
        /// <summary>
        /// Возникает при изменении данных в ручную
        /// </summary>
        public event EventHandler __eValueInteractiveChanged;
        /// <summary>
        /// Возникает при программном изменении данных
        /// </summary>
        public event EventHandler __eValueProgrammativChanged;

        #endregion СОБЫТИЯ
    }
}
