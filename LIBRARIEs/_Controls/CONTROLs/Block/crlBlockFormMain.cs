using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary> Класс панели 'crlBlockFormMain'
    /// </summary>
    /// <remarks> Блок главного окна</remarks>
    public class crlBlockFormMain : UserControl
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlBlockFormMain()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка компонентов
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            SuspendLayout();

            #region Размещение компонентов

            Controls.Add(__cMenu);
            __cMenu.Items.Add(_cMenuApplication);
            _cMenuApplication.DropDownItems.Add(_cMenuApplicationUserChange);
            _cMenuApplication.DropDownItems.Add(__cMenuApplicationUserRights);
            _cMenuApplication.DropDownItems.Add(_cMenuApplicationTunes);
            _cMenuApplication.DropDownItems.Add(_cMenuApplicationHelp);
            _cMenuApplication.DropDownItems.Add(_cMenuApplicationAbout);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Dock = DockStyle.Fill;

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }
        /// <summary>Настройка компонентов
        /// </summary>
        protected virtual void _mInit()
        {
            SuspendLayout();

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            TabStop = false;
            
            // _cMenuApplication
            {
                _cMenuApplication.Alignment = ToolStripItemAlignment.Right;
                _cMenuApplication.Image = nlResourcesImages.Properties.Resources._ApplicationForm_16C;
                _cMenuApplication.__fCaption_ = "Приложение";
            }
            // _cMenuApplicationUserChange
            {
                _cMenuApplicationUserChange.Click += __cMenuApplicationUserChange_Click;
                _cMenuApplicationUserChange.Image = nlResourcesImages.Properties.Resources._User_b16C;
                _cMenuApplicationUserChange.Name = "__cMenuApplicationUserChange";
                _cMenuApplicationUserChange.__fCaption_ = "Смена пользователя";
            }
            // _cMenuApplicationUserRights
            {
                __cMenuApplicationUserRights.Click += _cMenuApplicationUserRights_Click;
                __cMenuApplicationUserRights.Image = nlResourcesImages.Properties.Resources._UserEdit_b16;
                __cMenuApplicationUserRights.Name = "__cMenuApplicationUserChange";
                __cMenuApplicationUserRights.__fCaption_ = "Определение прав пользователей";
            }
            // _cMenuApplicationTunes
            {
                _cMenuApplicationTunes.Click += mMenuApplicationTunes_Click;
                _cMenuApplicationTunes.Image = nlResourcesImages.Properties.Resources._Gear_b16C;
                _cMenuApplicationTunes.Name = "__cMenuApplicationTunes";
                _cMenuApplicationTunes.__fCaption_ = "Настройки приложения";
            }
            // _cMenuApplicationHelp
            {
                _cMenuApplicationHelp.Click += mMenuApplicationHelp_Click;
                _cMenuApplicationHelp.Image = nlResourcesImages.Properties.Resources._SignHelp_b16C;
                _cMenuApplicationHelp.Name = "__cMenuApplicationHelp";
                _cMenuApplicationHelp.__fCaption_ = "Помощь";
            }
            // __cMenuApplicationAbout
            {
                _cMenuApplicationAbout.Click += mMenuApplicationAbout_Click;
                _cMenuApplicationAbout.Image = nlResourcesImages.Properties.Resources._SignInfo_b16C;
                _cMenuApplicationAbout.Name = "__cMenuApplicationAbout";
                _cMenuApplicationAbout.__fCaption_ = "О приложении";
            }

            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>Выполняется после создания объекта
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _mInit();
        }

        #endregion Объект

        #region Пункты меню

        /// <summary>Выполняется при выборе пункта меню 'Приложение/Смена пользователя' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void __cMenuApplicationUserChange_Click(object sender, System.EventArgs e)
        {
            if (__eMenuApplicationUserChangeClick != null)
                __eMenuApplicationUserChangeClick(this, new EventArgs());
        }
        private void _cMenuApplicationUserRights_Click(object sender, EventArgs e)
        {
            if (__eMenuApplicationUserRightsClick != null)
                __eMenuApplicationUserRightsClick(this, new EventArgs());
        }
        /// <summary>Выполняется при выборе пункта меню 'Приложение/Настройки приложения' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mMenuApplicationTunes_Click(object sender, System.EventArgs e)
        {
            crlFormTunes vFormTunes = new crlFormTunes();
            vFormTunes.ShowDialog();
        }
        /// <summary>Выполняется при выборе пункта меню 'Приложение/Помощь' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mMenuApplicationHelp_Click(object sender, System.EventArgs e)
        {
            (FindForm() as crlForm).__mHelp();
        }
        /// <summary>Выполняется при выборе пункта меню 'Приложение/О приложении' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mMenuApplicationAbout_Click(object sender, System.EventArgs e)
        {
            crlFormAbout vFormAbout = new crlFormAbout();
            vFormAbout.ShowDialog();
        }

        #endregion Пункты меню

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Вставляет пункт меню в меню по указанному индексу
        /// </summary>
        /// <param name="pMenuItem"></param>
        /// <param name="pIndex"></param>
        public void __mMenuAdd(crlComponentMenuItem pMenuItem, int pIndex = 0)
        {
            __cMenu.Items.Insert(pIndex, pMenuItem);
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние 

        /// <summary>Полное название класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Меню главного окна
        /// </summary>
        public crlComponentMenu __cMenu = new crlComponentMenu();
        /// <summary>Пункт меню 'Приложение'
        /// </summary>
        protected crlComponentMenuItem _cMenuApplication = new crlComponentMenuItem();
        /// <summary>Пункт меню 'Приложение / Смена пользователя'
        /// </summary>
        protected crlComponentMenuItem _cMenuApplicationUserChange = new crlComponentMenuItem();
        /// <summary>Пункт меню 'Приложение / Права пользователя'
        /// </summary>
        public crlComponentMenuItem __cMenuApplicationUserRights = new crlComponentMenuItem();
        /// <summary>Пункт меню 'Приложение / Настойки приложения'
        /// </summary>
        protected crlComponentMenuItem _cMenuApplicationTunes = new crlComponentMenuItem();
        /// <summary>Пункт меню 'Приложение / Помощь'
        /// </summary>
        protected crlComponentMenuItem _cMenuApplicationHelp = new crlComponentMenuItem();
        /// <summary>Пункт меню 'Приложение / О приложении'
        /// </summary>
        protected crlComponentMenuItem _cMenuApplicationAbout = new crlComponentMenuItem();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СОБЫТИЯ

        /// <summary>Возникает при выборе пункта меню 'Приложение/Смена пользователя' левой кнопкой мыши
        /// </summary>
        public event EventHandler __eMenuApplicationUserChangeClick;
        /// <summary>Возникает при выборе пункта меню 'Приложение/Права пользователя' левой кнопкой мыши
        /// </summary>
        public event EventHandler __eMenuApplicationUserRightsClick;

        #endregion = СОБЫТИЯ
    }
}
