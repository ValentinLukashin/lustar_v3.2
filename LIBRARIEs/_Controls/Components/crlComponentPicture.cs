using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentPicture'
    /// </summary>
    /// <remarks>Компонент - изображение</remarks>
    public class crlComponentPicture : PictureBox
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentPicture()
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
            SuspendLayout();

            #region /// Настройка компонента

            SizeMode = PictureBoxSizeMode.Normal;
            TabStop = false;

            #endregion Настройка компонента

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }

        #endregion Объект

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #endregion ПОЛЯ
    }
}
