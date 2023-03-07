using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentToolBar'
    /// </summary>
    /// <remarks>Компонент - кнопка панели управления</remarks>
    public class crlComponentToolBarButton : ToolStripButton
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentToolBarButton()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            // Отсутствует SuspendLayout

            #region /// Настройка компонента

            DisplayStyle = ToolStripItemDisplayStyle.Image;

            #endregion Настройка компонента

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при отпускании правой кнопки мыши
        /// </summary>
        /// <param name="pEvent"></param>
        protected override void OnMouseUp(MouseEventArgs pEvent)
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

            base.OnMouseUp(pEvent);
        }

        #endregion Объект

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
        /// Текст без перевода
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
                fTextWithOutTranslate = value;
                Text = crlApplication.__oTunes.__mTranslate(fTextWithOutTranslate);
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при выборе правой кнопкой мыши
        /// </summary>
        public event EventHandler __eMouseClickRight;
        /// <summary>
        /// Возникает при выборе левой кнопкой мыши
        /// </summary>
        public event EventHandler __eMouseClickLeft;

        #endregion СОБЫТИЯ
    }
}
