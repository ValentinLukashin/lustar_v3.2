using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlPanelMenuTree'
    /// </summary>
    /// <remarks>Панель пользовательского древовидного меню</remarks>
    public class crlPanelMenuTree : crlComponentSplitter
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка компонента
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel1.Controls.Add(_cPicture);
            Panel1.Controls.Add(_cLabelRole);
            Panel1.Controls.Add(_cLabelRoleValue);
            Panel1.Controls.Add(_cLabelUser);
            Panel1.Controls.Add(_cLabelUserValue);
            Panel2.Controls.Add(_cTree);

            #endregion Размещение компонентов

            #region Настройка компонентов

            IsSplitterFixed = true;
            FixedPanel = FixedPanel.Panel1;
            Orientation = Orientation.Horizontal;
            TabStop = false;

            // _cPicture
            {
                _cPicture.Image = nlResourcesImages.Properties.Resources._SmileInfo_y32C;
                _cPicture.Location = new Point(5, 5);
                _cPicture.SizeMode = PictureBoxSizeMode.Normal;
                _cPicture.Size = new Size(_cPicture.Image.Width, _cPicture.Image.Height);
            }
            // _cLabelRole
            {
                _cLabelRole.Location = new Point(_cPicture.Width + crlInterface.__fIntervalHorizontal * 2, 10);
                _cLabelRole.__fCaption_ = "Роль пользователя";
            }
            // __cLabelRoleValue
            {
                _cLabelRoleValue.Location = new Point(180, 10);
            }
            // __cLabelUser
            {
                _cLabelUser.Location = new Point(_cPicture.Width + crlInterface.__fIntervalHorizontal * 2, 40);
                _cLabelUser.__fCaption_ = "Пользователь";
            }
            // __cLabelUserValue
            {
                _cLabelUserValue.Location = new Point(180, 40);
            }
            // __cTree
            {
                _cTree.Dock = DockStyle.Fill;
                _cTree.DoubleClick += mTree_DoubleClick;
            }

            SplitterDistance = _cPicture.Width + crlInterface.__fIntervalVertical * 2;

            #endregion Настройка компонентов

            ResumeLayout();
        }
        /// <summary>Выполняется при получении фокуса
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _cTree.Focus();
        }

        #endregion Объект

        /// <summary>Выполняется при двойном клике мыши по узлу 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTree_DoubleClick(object sender, System.EventArgs e)
        {
            if (__eTreeDoubleClick != null)
                __eTreeDoubleClick(_cTree, new EventArgs());
        }

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Передача фокуса дереву
        /// </summary>
        public void _mFocus()
        {
            _cTree.Focus();
        }
        /// <summary>Удаление узлов из контрола
        /// </summary>
        public void _mNodesClear()
        {
            _cTree.Nodes.Clear();
        }
        /// <summary>Создание нового узла с переводом заголовка на язык пользователя
        /// </summary>
        /// <param name="pCaptionText">Заголовок</param>
        /// <param name="pTag">Содержание тэга</param>
        /// <returns>[true] - Узел добавлен, иначе - [false]</returns>
        public crlTreeNode _mNodeNew(string pCaptionText, string pTag)
        {
            return _cTree.__mNodeNew(pCaptionText, pTag);
        }
        /// <summary>Создание вложенного узла с переводом заголовка на язык пользователя
        /// </summary>
        /// <param name="pCaptionText">Заголовок</param>
        /// <param name="pTag">Содержание тэга</param>
        /// <returns>[true] - Узел добавлен, иначе - [false]</returns>
        public crlTreeNode _mNodeSupply(crlTreeNode pTreeNodeParent, string pCaptionText, string pTag)
        {
            return _cTree.__mNodeSupply(pTreeNodeParent, pCaptionText, pTag);
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Изображение пользователя
        /// </summary>
        protected crlComponentPicture _cPicture = new crlComponentPicture();
        /// <summary>Заголовок 'Роль пользователя'
        /// </summary>
        protected crlComponentLabel _cLabelRole = new crlComponentLabel();
        /// <summary>Название роли пользователя
        /// </summary>
        protected crlComponentLabel _cLabelRoleValue = new crlComponentLabel();
        /// <summary>Заголовок 'Псевдоним пользователя'
        /// </summary>
        protected crlComponentLabel _cLabelUser = new crlComponentLabel();
        /// <summary>Название псевдонима пользователя
        /// </summary>
        protected crlComponentLabel _cLabelUserValue = new crlComponentLabel();
        /// <summary>Меню пользователя
        /// </summary>
        protected crlComponentTree _cTree = new crlComponentTree();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Отображение роли пользователя
        /// </summary>
        public string __fUserRole_
        {
            set { _cLabelRoleValue.Text = value; }
        }
        /// <summary>Отображение псевдонима пользователя
        /// </summary>
        public string __fUserAlias_
        {
            set { _cLabelUserValue.Text = value; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        public event EventHandler __eTreeDoubleClick;

        #endregion СОБЫТИЯ
    }
}
