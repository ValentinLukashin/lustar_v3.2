using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nlControls
{
    /// <summary>Класс 'crlInputTitle'
    /// </summary>
    /// <remarks>Заголовок полей ввода</remarks>
    public class crlInputTitle : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов
            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cLabelCaption
            {
                _cLabelCaption.__fLabelType_ = LABELTYPES.Title; /// Назвначение вида надписи-заголовка - 'Заголовок' 
            }
            // _cLabelValue
            {
                _cLabelValue.Text = "";
                _cLabelValue.Visible = false;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ    
    }
}
