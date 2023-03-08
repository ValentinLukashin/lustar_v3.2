using System;
using System.Drawing;

namespace nlControls
{
    /// <summary>Класс 'crlInputPhone'
    /// </summary>
    /// <remarks>Поле ввода телефонного номера</remarks>
    public class crlInputPhone : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            Panel2.Controls.Add(_cInput);

            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.TextChanged += _cInput_TextChanged;
            }

            ResumeLayout(false);
        }
        /// <summary>Выполняется при изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput_TextChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true; /// Включение использования фильтра
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ    

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Поле ввода символьных данных по маске
        /// </summary>
        protected crlComponentCharMask _cInput = new crlComponentCharMask();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
