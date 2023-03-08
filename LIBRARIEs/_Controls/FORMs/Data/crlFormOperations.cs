using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormOperations'
    /// </summary>
    /// <remarks>Абстактный класс формы для выполнения операций</remarks>
    public abstract class crlFormOperations : crlForm
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

            Text = "Базовая форма для выполнения операций";

            #region Размещение компонентов

            Controls.Add(_cAreaOperations);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cAreaOperations
            {
                _cAreaOperations.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для правки табличных данных
        /// </summary>
        protected crlAreaOperations _cAreaOperations = new crlAreaOperations();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
