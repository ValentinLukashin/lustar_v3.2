using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormEmailSend'
    /// </summary>
    /// <remarks>Форма для формирования отправляемого почтового сообщения</remarks>
    public class crlFormEmailSend : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка формы
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(_cAreaEmailSend);
            Controls.SetChildIndex(_cAreaEmailSend, 0);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Text = "Базовая форма для формирования отправляемого почтового сообщения";

            // _cAreaEmailSend 
            {
                _cAreaEmailSend.Dock = DockStyle.Fill;
                _cAreaEmailSend.__cMessage.__fButtonOpenVisible_ = false;
                _cAreaEmailSend.__cMessage.__fButtonSaveVisible_ = false;
                //_cAreaEmailSend.__cMessage.__cButtonHelp.Visible = false;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для формирования отправляемого почтового сообщения
        /// </summary>
        public crlAreaEmailSend _cAreaEmailSend = new crlAreaEmailSend();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
