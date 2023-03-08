using nlApplication;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentMenuItem'
    /// </summary>
    /// <remarks>Компонент - пункт меню</remarks>
    public class crlComponentMenuItem : ToolStripMenuItem
    {
        #region = ДИЗАЙНЕРЫ

        public crlComponentMenuItem()
        {
            _mObjectAssembly();
        }

        #endregion = ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region - Объект

        /// <summary>
        /// Загрузка компонента
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            // Отсутствует SuspemdLayout

            #region /// Настройка компонента

            BackColor = Color.Transparent;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            ImageScaling = ToolStripItemImageScaling.None;
            Size = new Size(16, 16);
            
            #endregion Настройка компонента

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }

        #endregion Объект

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Перевод выражения на язык интерфейса 
        /// </summary>
        /// <param name="pString">Текст</param>
        /// <param name="pParameters">Список дополнительных парамметров</param>
        public void __mCaptionBuilding(string pString, params object[] pParameters)
        {
            fCaptionNotTranslate = String.Format(pString, pParameters);
            Text = crlApplication.__oTunes.__mTranslate(pString, pParameters);
        }
        /// <summary>
        /// Добавление пункта вложенного меню
        /// </summary>
        /// <param name="pMenuItem"></param>
        public void __mDropDownItemsAdd(crlComponentMenuItem pMenuItem)
        {
            DropDownItems.Add(pMenuItem);
        }
        /// <summary>
        /// Очистка от вложенных пунктов меню
        /// </summary>
        public void __mDropDownItemsClear()
        {
            DropDownItems.Clear();
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Не переведенное выражение надписи
        /// </summary>
        private string fCaptionNotTranslate = "";
        /// <summary>Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion - Внутренние

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Надпись компонента
        /// </summary>
        /// <remarks>При установке переводит на язык интерфейса, возвращает не переведеное выражение</remarks>
        public string __fCaption_
        {
            get { return fCaptionNotTranslate; }
            set
            {
                fCaptionNotTranslate = value.Trim();
                Text = appApplication.__oTunes.__mTranslate(fCaptionNotTranslate);
            }
        }

        #endregion СВОЙСТВА
    }
}
