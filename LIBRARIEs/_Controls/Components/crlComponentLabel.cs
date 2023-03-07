using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentLabel'
    /// </summary>
    /// <remarks>Компонент - Label</remarks>
    public class crlComponentLabel : Label
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentLabel()
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

            #region /// Настройки компонента

            AutoSize = true;
            BackColor = Color.Transparent;
            ForeColor = crlApplication.__oInterface.__fColorText;
            fLabelType = LABELTYPES.Normal;

            #endregion Настройка компонента

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>
        /// Выполняется при отпускании правой кнопки мыши
        /// </summary>
        /// <param name="pEvent"></param>
        protected override void OnMouseUp(MouseEventArgs pEvent)
        {
            if (fCaptionClickable == true & Enabled == true)
            {
                if (pEvent.Button == MouseButtons.Left)
                {
                    if (__eMouseClickLeft != null)
                        __eMouseClickLeft(this, new EventArgs());
                }
                if (pEvent.Button == MouseButtons.Right)
                {
                    if (__eMouseClickRight != null)
                        __eMouseClickRight(this, new EventArgs());
                }
            }

            base.OnMouseUp(pEvent);

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Сборка выражения с параметрами и перевод выражения на язык интерфейса 
        /// </summary>
        /// <param name="pString">Текст</param>
        /// <param name="pParameters">Список дополнительных парамметров</param>
        public void __mCaptionBuilding(string pString, params object[] pParameters)
        {
            fTextWithOutTranslate = String.Format(pString, pParameters);
            Text = crlApplication.__oTunes.__mTranslate(pString, pParameters);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region - Служебные

        /// <summary>
        /// Разрешение обрабатывать клики мыши
        /// </summary>
        private bool fCaptionClickable = true;
        /// <summary>
        /// Вид надписи
        /// </summary>
        private LABELTYPES fLabelType = LABELTYPES.Normal;
        /// <summary>
        /// Вид надписи определенный программой
        /// </summary>
        private readonly LABELTYPES fLabelTypeOnLoad = LABELTYPES.Normal;
        /// <summary>
        /// Строка заголовка без перевода
        /// </summary>
        private string fTextWithOutTranslate = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст заголовка
        /// </summary>
        /// <remarks>Отображаемый текст переводиться на язык интерфейса. Возвращается не переведенный текст</remarks>
        public string __fCaption_
        {
            get { return fTextWithOutTranslate; }
            set
            {
                fTextWithOutTranslate = value.Trim();
                Text = crlApplication.__oTunes.__mTranslate(fTextWithOutTranslate);
            }
        }
        /// <summary>
        /// Доступность компонента
        /// </summary>
        /// <remarks>Определяет доступность компонента и работу Lustar функционала</remarks>
        public bool __fEnabled_
        {
            get { return Enabled; }
            set
            {
                Enabled = value;
                fCaptionClickable = Enabled;
                if (value == false)
                    __fLabelType_ = LABELTYPES.Normal;
                else
                    __fLabelType_ = fLabelTypeOnLoad;
            }
        }
        /// <summary>
        /// Вид надписи
        /// </summary>
        public LABELTYPES __fLabelType_
        {
            get { return fLabelType; }
            set
            {
                Cursor = Cursors.Default;
                fLabelType = value;
                switch (fLabelType)
                {
                    case LABELTYPES.Normal:
                        Font = crlApplication.__oInterface.__mFont(FONTS.Text);
                        ForeColor = crlApplication.__oInterface.__mColor(COLORS.Text);
                        break;
                    case LABELTYPES.Button:
                        Font = crlApplication.__oInterface.__mFont(FONTS.TextButton);
                        ForeColor = crlApplication.__oInterface.__mColor(COLORS.TextButton);
                        Cursor = Cursors.Hand;
                        break;
                    case LABELTYPES.Title:
                        Font = crlApplication.__oInterface.__mFont(FONTS.TextTitle);
                        ForeColor = crlApplication.__oInterface.__mColor(COLORS.TextTitle);
                        break;
                }
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при клике левой кнопки мыши по компоненту
        /// </summary>
        public event EventHandler __eMouseClickLeft;
        /// <summary>
        /// Возникает при клике правой кнопки мыши по компоненту
        /// </summary>
        public event EventHandler __eMouseClickRight;

        #endregion СОБЫТИЯ    
    }
}
