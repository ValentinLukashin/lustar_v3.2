using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormEmailRecipientsTypes'
    /// </summary>
    /// <remarks>Базовая форма для выбора вида заполнения списка получателей</remarks>
    public class crlFormEmailRecipientsTypes : crlForm
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

            Controls.Add(__cToolBar);
            __cToolBar.Items.Add(__cButtonApply);
            __cToolBar.Items.Add(__cButtonHelp);

            #endregion Размещение компонентов

            #region Настройка компонентов

            // __cButtonApply
            {
                __cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                __cButtonApply.ToolTipText = "[ Ctrl + A ] " + crlApplication.__oTunes.__mTranslate("Применить");
                __cButtonApply.Click += __cButtonApply_Click;
                //__cButtonApply._eMouseClickRight += _mButtonHelp_eMouseClickRight;
            }
            // _cButtonHelp
            {
                __cButtonHelp.Image = global::nlResourcesImages.Properties.Resources._SignQuestion_b32C;
                __cButtonHelp.ToolTipText = "[ F1 ] " + crlApplication.__oTunes.__mTranslate("Помощь");
                __cButtonHelp.Click += __cButtonHelp_Click;
                __cButtonHelp.__eMouseClickRight += _mButtonHelp_eMouseClickRight;
            }

            #endregion Настройка компонентов

            ResumeLayout();
        }
        /// <summary>Выполняется после создания формы
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            int vOptionClue = 0;
            fTop = __cToolBar.Height  + crlInterface.__fIntervalVertical;  // + crlInterface.__fFormHeaderHeight
            foreach (string vOptionText in __fOptionsList)
            {
                crlComponentOption vOption = new crlComponentOption();
                vOption.__mCaptionBuilding(vOptionText);
                vOption.Location = new System.Drawing.Point(5, fTop);
                vOption.Tag = vOptionClue;
                fTop = fTop + vOption.Height + crlInterface.__fIntervalVertical;
                Controls.Add(vOption);
                vOptionClue++;
            }
        }
        /// <summary>Выполняется при выборе кнопки 'Помощь' правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _mButtonHelp_eMouseClickRight(object sender, EventArgs e)
        {
            Form vForm = FindForm() as Form;
            if (vForm.MinimumSize.Width > 0)
            {
                vForm.MinimumSize = new Size(0, 0);
                (FindForm() as crlFormEmailRecipientsTypes).__cStatus.__fCaption_ = "Минимальные размеры формы сброшены";
            }
            else
            {
                vForm.MinimumSize = new Size(vForm.Width, vForm.Height);
                (FindForm() as crlFormEmailRecipientsTypes).__cStatus.__fCaption_ = "Текущие размеры установлены как минимальные";
            }
        }

        #endregion Объект

        #region Кнопки управления

        private void __cButtonApply_Click(object sender, EventArgs e)
        {
            foreach (Control vControl in Controls)
            {
                if (vControl is crlComponentOption)
                {
                    if ((vControl as crlComponentOption).Checked == true)
                    {
                        __fReturnValue = Convert.ToInt32((vControl as crlComponentOption).Tag);
                        break;
                    }
                }
            }
            Close();
        }

        /// <summary>Выполняется при выборе кнопки 'Помощь' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cButtonHelp_Click(object sender, EventArgs e)
        {
            (FindForm() as crlFormEmailRecipientsTypes).__mHelp();
        }

        #endregion Кнопки управления

        #endregion - Поведение


        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Список допустимых значений
        /// </summary>
        public ArrayList __fOptionsList = new ArrayList();
        /// <summary>Возвращаемое значение
        /// </summary>
        public int __fReturnValue = -1;

        #endregion - Атрибуты

        #region - Внутренние

        /// <summary>Отступ от верхнего края формы
        /// </summary>
        private int fTop = 0; 

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Полоса инструментов
        /// </summary>
        public crlComponentToolBar __cToolBar = new crlComponentToolBar();
        /// <summary>Кнопка 'Применить'
        /// </summary>
        public crlComponentToolBarButton __cButtonApply = new crlComponentToolBarButton();
        /// <summary>Кнопка 'Помощь'
        /// </summary>
        public crlComponentToolBarButton __cButtonHelp = new crlComponentToolBarButton();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
