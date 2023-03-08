using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormTree'
    /// </summary>
    /// <remarks>Абстактный класс формы для построения форм для правки древовидных данных</remarks>
    public abstract class crlFormTree : crlForm
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

            Text = "Базовая форма для правки древовидных данных";

            #region Размещение компонентов

            Controls.Add(__cAreaTree);
            Controls.SetChildIndex(__cAreaTree, 0);
            Controls.SetChildIndex(__cStatus, 1);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // _cAreaTree
            {
                __cAreaTree.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            
        }

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для правки табличных данных
        /// </summary>
        public crlAreaTree __cAreaTree = new crlAreaTree();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
