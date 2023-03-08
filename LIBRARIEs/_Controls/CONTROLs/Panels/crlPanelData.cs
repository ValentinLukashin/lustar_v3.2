using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlPanelData'
    /// </summary>
    /// <remarks></remarks>
    public class crlPanelData : UserControl
    {
        #region = ДИЗАЙНЕРЫ

        public crlPanelData() 
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

            #region /// Размещение компонентов
            #endregion Размещение компонентов

            #region /// Настройка компонентов

            

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Поведение

        #region - Прцедуры

        /// <summary>
        /// Добавление панели с данными
        /// </summary>
        /// <param name="pUnitPanelData"></param>
        /// <returns></returns>
        public bool __mUnitPanelDataAdd(crlUnitPanelData pUnitPanelData)
        {
            bool vReturn = true; // Возвращаемое значение
            Controls.Add(pUnitPanelData);
            pUnitPanelData.Top = fTop;
            pUnitPanelData.Width = Width;
            fTop = pUnitPanelData.Top + pUnitPanelData.Height;
            return vReturn;
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

        private int fTop = 0;

        #endregion Служебные

        #endregion ПОЛЯ
    }

    /// <summary>
    /// Класс 'crlPanelData'
    /// </summary>
    /// <remarks></remarks>
    public class crlUnitPanelData : UserControl
    {
        #region = ДИЗАЙНЕРЫ

        public crlUnitPanelData()
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

            #region /// Размещение компонентов
            #endregion Размещение компонентов

            #region /// Настройка компонентов


            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        public bool __mControlAdd(Control pControl)
        {
            bool vReturn = true;
            Controls.Add(pControl);
            return vReturn;
        }

        public bool __mRecordLoad()
        {
            bool vReturn = true;

            return vReturn;
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
        #endregion Служебные

        #endregion ПОЛЯ
    }
}
