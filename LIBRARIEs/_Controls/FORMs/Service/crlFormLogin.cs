using nlDataMaster;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс формы 'crlFormLogin'
    /// </summary>
    /// <remarks>Форма для регистрации пользователя</remarks>
    public class crlFormLogin : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(_cSplitter);
            Controls.Add(_cToolBar);
            Controls.SetChildIndex(__cStatus, 0);
            _cSplitter.Panel1.Controls.Add(_cLogoType);
            _cSplitter.Panel2.Controls.Add(_cInputUserCode);
            _cSplitter.Panel2.Controls.Add(_cInputUserPassword);

            _cToolBar.Items.Add(_cButtonApply);
            _cToolBar.Items.Add(_cButtonHelp);

            #endregion Размещение компонентов

            #region Настройка компонентов

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = true;
            Height = 270;
            Width = 350; /// Назначение ширины формы
            __mCaptionBuilding("Регистрация пользователя");


            _cToolBar.TabStop = false;

            // _cButtonApply
            {
                _cButtonApply.Click += _cButtonApply_Click;
                _cButtonApply.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                _cButtonApply.ToolTipText = "[ Ctrl + A ] " + crlApplication.__oTunes.__mTranslate("Применить");
            }
            // _cButtonHelp
            {
                _cButtonHelp.Click += _cButtonHelp_Click;
                _cButtonHelp.Image = global::nlResourcesImages.Properties.Resources._SignQuestion_b32C;
                _cButtonHelp.ToolTipText = "[ F1 ] " + crlApplication.__oTunes.__mTranslate("Помощь");
            }
            // _cSplitter
            {
                _cSplitter.Dock = DockStyle.Fill;
                _cSplitter.Orientation = Orientation.Horizontal;
                _cSplitter.SplitterDistance = 80;
                _cSplitter.IsSplitterFixed = true;
                _cSplitter.FixedPanel = FixedPanel.Panel1;
            }
            // _cLogoType
            {
                _cLogoType.SizeMode = PictureBoxSizeMode.CenterImage;
                _cLogoType.Top = crlInterface.__fIntervalVertical; 
                _cLogoType.Left = crlInterface.__fIntervalHorizontal;
                _cLogoType.Dock = DockStyle.Fill;

                if (File.Exists(crlApplication.__oPathes.__fFolderStart + "\\Logo.png") == true)
                {
                    _cLogoType.BackColor = Color.Transparent;
                    _cLogoType.Image = Image.FromFile(crlApplication.__oPathes.__fFolderStart + "\\Logo.png");
                }
            }
            // _cInputUserCode
            {
                _cInputUserCode.Location = new Point(crlInterface.__fIntervalHorizontal, crlInterface.__fIntervalVertical);
                _cInputUserCode.Width = Width - crlInterface.__fFormBorderWidth * 2 - crlInterface.__fIntervalHorizontal * 2;
                _cInputUserCode.__fCaption_ = "Код пользователя";
                _cInputUserCode.__fCheckVisible_ = false;
                _cInputUserCode.__fFillType_ = FILLTYPES.Necessarily;
                _cInputUserCode.__fPrecision_ = 3;
            }
            // _cInputUserPassword
            {
                _cInputUserPassword.Location = new Point(crlInterface.__fIntervalHorizontal
                    , _cInputUserCode.Top + _cInputUserCode.Height + crlInterface.__fIntervalVertical);
                _cInputUserPassword.Width = Width - crlInterface.__fFormBorderWidth * 2 - crlInterface.__fIntervalHorizontal * 2;
                _cInputUserPassword.__fCaption_ = "Пароль";
                _cInputUserPassword.__fPasswordChar_ = '*';
                _cInputUserPassword.__fCheckVisible_ = false;
                _cInputUserPassword.__fFillType_ = FILLTYPES.Necessarily;
            }
            
            __mTunesLoad();
            _cInputUserCode.Focus();

            #endregion Настойка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        /// <summary>Выполняется после построения объекта
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _cSplitter.SplitterDistance = 80;
        }
        /// <summary>Выполняется при отпускании горячих клавиш
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) // F1
                _cButtonHelp.PerformClick();
            //    mButtonHelp_ClickLeft(_cButtonHelp, new EventArgs());
            //if (e.KeyCode == Keys.F9) // F9
            //    _mButtonApply_ClickLeft(_cButtonApply, new EventArgs());
            if (e.Control == true & e.KeyCode == Keys.A) // Ctrl+A
                _cButtonApply.PerformClick();

            base.OnKeyUp(e);
        }

        #endregion Объект

        #region Кнопки управления

        /// <summary>
        /// Выполняется при выборе кнопки 'Применить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonApply_Click(object sender, EventArgs e)
        {
            int vFound = crlApplication.__oData.__mTableRowsCountWhere("Usr", "codUsr=" + _cInputUserCode.__fValue_.ToString().Trim(), __fDataSourceAlias); // Количество найденных значений
            /// Если количество пользователей с указанными кодом и паролем равно 0, выполняется регистрация пользователя и форма закрывается
            if (vFound > 0)
            {
                DataTable vDataTable = crlApplication.__oData.__mSqlQuery("Select * From Usr as U"
                    + " Left Join UsrRol as UR On UR.CLU = U.lnkUsrRol"
                    + " Where codUsr = " + _cInputUserCode.__fValue_.ToString().Trim()
                    , __fDataSourceAlias);
                if (vDataTable.Rows[0]["Psw"].ToString().Trim() == _cInputUserPassword.__fValue_.ToString().Trim())
                {
                    datUnitDataSource vDataSource = crlApplication.__oData.__mDataSourceGet(__fDataSourceAlias);
                    vDataSource.__fUserAdministrator = Convert.ToBoolean(vDataTable.Rows[0]["mrkAdm"]);
                    vDataSource.__fUserAlias = Convert.ToString(vDataTable.Rows[0]["desUsr"]).Trim();
                    vDataSource.__fUserClue = Convert.ToInt32(vDataTable.Rows[0]["CLU"]);
                    vDataSource.__fUserCode = Convert.ToInt32(vDataTable.Rows[0]["codUsr"]);
                    vDataSource.__fUserRoleClue = Convert.ToInt32(vDataTable.Rows[0]["lnkUsrRol"]);
                    vDataSource.__fUserRoleName = Convert.ToString(vDataTable.Rows[0]["desUsrRol"]).Trim();
                    __fRegistered = true;
                }
            }
            /// Выполняется приращение попыток проверки кода и пароля
            fAttemptsAmount++;
            /// Так как форма не закрылась - введены не верные данные. Выводиться количество попыток ввода в строку статуса формы.
            if (fAttemptsAmount == 1)
                __cStatus.__mCaptionBuilding("Выполнена 1 попытка");
            else
                __cStatus.__mCaptionBuilding("Выполнено {0} попытки", fAttemptsAmount);
            /// Если количество попыток ввода завершившихся неудачей превышает 3, закрываем форму.
            if (fAttemptsAmount >= 3)
            {
                Close();
            }
            if (__fRegistered == true)
                Close();
        }
        /// <summary>Выполняется при выборе кнопки 'Помощь'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonHelp_Click(object sender, EventArgs e)
        {
            __mHelp();
        }

        #endregion Кнопки управления

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Статус регистрирующегося пользователя
        /// </summary>
        public bool __fRegistered = false;
        /// <summary>
        /// Псевдоним источника данных
        /// </summary>
        public string __fDataSourceAlias = "";

        #endregion Атрибуты

        #region = Служебные

        /// <summary>
        /// Количество выполненных попыток
        /// </summary>
        private int fAttemptsAmount = 0;

        #endregion Служебные

        #region - Компоненты

        /// <summary>
        /// Кнопка 'Применить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonApply = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Помощь'
        /// </summary>
        protected crlComponentToolBarButton _cButtonHelp = new crlComponentToolBarButton();
        /// <summary>
        /// Разделитель
        /// </summary>
        protected crlComponentSplitter _cSplitter = new crlComponentSplitter();
        /// <summary>
        /// Логотип пользователя программы
        /// </summary>
        protected crlComponentPicture _cLogoType = new crlComponentPicture();
        /// <summary>
        /// Поле ввода кода пользователя
        /// </summary>
        protected crlInputNumberInt _cInputUserCode = new crlInputNumberInt();
        /// <summary>
        /// Поле ввода пароля пользователя
        /// </summary>
        protected crlInputChar _cInputUserPassword = new crlInputChar();
        /// <summary>
        /// Панель инструментов
        /// </summary>
        protected crlComponentToolBar _cToolBar = new crlComponentToolBar();

        #endregion Компоненты

        #endregion ПОЛЯ
    }
}
