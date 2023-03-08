using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormMessage'
    /// </summary>
    public class crlFormMessage : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Построение объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            ControlBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(500, 300);
            Controls.Add(_cAreaMessage);

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";
        }
        /// <summary>* Выполняется при отпускании горячих клавиш
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.A) // Ctrl+A
                _cAreaMessage.__mPressButtonApply();

            base.OnKeyUp(e);
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для построения фильтра
        /// </summary>
        public crlAreaMessage _cAreaMessage = new crlAreaMessage();

        #endregion - Компоненты

        #endregion ПОЛЯ    
    }
}
