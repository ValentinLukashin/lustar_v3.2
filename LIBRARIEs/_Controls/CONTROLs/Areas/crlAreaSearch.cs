using nlDataMaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    public class crlAreaSearch : crlArea 
    {
        #region = МЕТОДЫ

        /// <summary>* Построение объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            base._mObjectAssembly();

            PerformLayout();

            #region Размещение компонентов

            Panel2.Controls.Add(_cSplitterInputData);
            Panel2.Controls.SetChildIndex(_cSplitterInputData, 0);
            _cSplitterInputData.Panel1.Controls.Add(_cLabelSearch);
            _cSplitterInputData.Panel1.Controls.Add(_cStringSearch);
            _cSplitterInputData.Panel2.Controls.Add(_cGrid);
            __cToolBar.Items.Insert(0, _cButtonSelect);

            #endregion Размещение компонентов

            #region Описание компонентов

            // __cToolBar
            {
                // _cButtonSelect
                {
                    _cButtonSelect.Click += _cButtonSelect_Click;
                    _cButtonSelect.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                }
            }
            // _cSplitterInputData
            {
                _cSplitterInputData.BorderStyle = BorderStyle.None;
                _cSplitterInputData.Dock = DockStyle.Fill;
                _cSplitterInputData.FixedPanel = FixedPanel.Panel1;
                _cSplitterInputData.IsSplitterFixed = true;
                _cSplitterInputData.Orientation = Orientation.Horizontal;
                _cSplitterInputData.SplitterDistance = _cStringSearch.Height;
            }
            // _cLabelSearch
            {
                _cLabelSearch.AutoSize = true;
                _cLabelSearch.Location = new Point(0, 5);
                _cLabelSearch.__mCaptionBuilding("Введите искомый текст");
                _cLabelSearch.__eMouseClickRight += _cLabelSearch___eMouseClickRight;
            }
            // _cStringSearch
            {
                _cStringSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                _cStringSearch.KeyDown += _KeyDown;
                _cStringSearch.Location = new Point(_cLabelSearch.Width + crlInterface.__fIntervalHorizontal + 50, 0);
                _cStringSearch.Size = new Size(Width - _cStringSearch.Left - crlInterface.__fIntervalHorizontal, 25);
                _cStringSearch.TextChanged += _mButtonSelect_TextChanged;
            }
            // _cGrid
            {
                _cGrid.Dock = DockStyle.Fill;
                _cGrid.CellDoubleClick += _cGrid_CellDoubleClick;
                _cGrid.KeyDown += _KeyDown;
                _cGrid.KeyPress += _cGrid_KeyPress;
            }

            #endregion Описание компонентов

            ResumeLayout(false);
            _cGrid.Focus();

            return;
        }

        private void _cLabelSearch___eMouseClickRight(object sender, EventArgs e)
        {

            _cMenuItemS[0] = new MenuItem(crlApplication.__oTunes.__mTranslate("Искать вхождение в строку"), mMenuLabelSearch);
            _cMenuItemS[1] = new MenuItem(crlApplication.__oTunes.__mTranslate("Искать с начала строки"), mMenuLabelSearch);

            ContextMenu vContextMenu = new ContextMenu(_cMenuItemS);
            _cLabelSearch.ContextMenu = vContextMenu;
        }

        private void mMenuLabelSearch(object sender, EventArgs e)
        {
            MenuItem vMenuItem = sender as MenuItem;
            crlForm vForm = FindForm() as crlForm;
            switch (vMenuItem.Index)
            {
                case 0:
                    vForm.__cStatus.__fCaption_ = crlApplication.__oTunes.__mTranslate("Ищется вхождение в строку");
                    _cMenuItemS[0].Checked = true;
                    _cMenuItemS[1].Checked = false;
                    fSearchStatus = true;
                    break;
                case 1:
                    vForm.__cStatus.__fCaption_ = crlApplication.__oTunes.__mTranslate("Ищется сначала строки");
                    _cMenuItemS[0].Checked = false;
                    _cMenuItemS[1].Checked = true;
                    fSearchStatus = false;
                    break;
            }
            _mButtonSelect_TextChanged(null, null);
        }

        /// <summary>Выполняется при двойном клике по ячейке сетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _cButtonSelect_Click(null, null);
        }
        private void _cGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            { /// Если нажатая клавиша не 'Enter'
                if (e.KeyChar.ToString().Length == 1)
                {
                    _cStringSearch.Text = _cStringSearch.Text.Trim() + e.KeyChar.ToString();
                    _cStringSearch.SelectionStart = _cStringSearch.Text.Trim().Length;
                    _cStringSearch.SelectionLength = 0;
                    _cStringSearch.Focus();
                }
            }
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Выбрать'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonSelect_Click(object sender, EventArgs e)
        {
            int vSelectedIndex = _cGrid.CurrentRow.Index; // Индекс выбранной в сетке записи
            __fValueClue = Convert.ToInt32(_cGrid.Rows[vSelectedIndex].Cells["clu" + __fEssence_.__fTableName].Value);
            __fValueString = Convert.ToString(_cGrid.Rows[vSelectedIndex].Cells["des" + __fEssence_.__fTableName].Value);

            FindForm().Close();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    _cButtonSelect_Click(null, null);
                    break;
                case Keys.Up:
                    if (_cGrid.CurrentRow.Index > 0)
                    {
                        _cGrid.Rows[_cGrid.CurrentRow.Index - 1].Selected = true;
                        _cGrid.Focus();
                    }
                    break;
                case Keys.Down:
                    if (_cGrid.CurrentRow.Index < _cGrid.Rows.Count - 1)
                    {
                        _cGrid.Rows[_cGrid.CurrentRow.Index + 1].Selected = true;
                        _cGrid.Focus();
                    }
                    break;
            }
        }

        private void _mButtonSelect_TextChanged(object sender, EventArgs e)
        {
            char[] vCharArray = _cStringSearch.Text.ToCharArray(); // Массив введенных символов
            string vFilter = ""; // Построеное выражение фильтра

            if (vCharArray.Length == 0)
            {
                return;
            } /// Если нет введенных символов - поиск не выполняется

            if (fSearchStatus == true)
            {
                if (vCharArray[vCharArray.Length - 1] == ' ')
                { /// Если последний символ пробел

                    #region Построение фильтра

                    foreach (string vField in __fFieldsCharList)
                    {
                        if (vFilter.Length > 0)
                            vFilter = vFilter + " or ";
                        vFilter = vFilter + crlApplication.__oData.__mExpressionLikeEntryTranslit(vField, _cStringSearch.Text);
                    }

                    #endregion Построение фильтра

                    _cGrid.DataSource = _cGrid.__oEssence.__mGrid(vFilter, "des" + _cGrid.__oEssence.__fTableName); /// Выполнение запроса данных
                }
            }
            else
            {
                if (vCharArray[vCharArray.Length - 1] == ' ')
                { /// Если последний символ пробел

                    #region Построение фильтра

                    foreach (string vField in __fFieldsCharList)
                    {
                        if (vFilter.Length > 0)
                            vFilter = vFilter + " or ";
                        vFilter = crlApplication.__oData.__mExpressionLikeStartTranslit(vField, _cStringSearch.Text);
                    }

                    #endregion Построение фильтра

                    _cGrid.DataSource = _cGrid.__oEssence.__mGrid(vFilter, "des" + _cGrid.__oEssence.__fTableName); /// Выполнение запроса данных
                }
            }
        }

        /// <summary>* Выполняется при выборе кнопки 'Выбрать'
        /// </summary>
        public void __mButtonSelectClick()
        {
            _cButtonSelect.PerformClick();
            return;
        }

        #region - Процедуры

        /// <summary>* Создание колонок в сетке
        /// </summary>
        public void __mColumnsBuild()
        {
            _cGrid.__mColumnsBuild();
        }
        /// <summary>* Установка фокуса в поле поиска данных
        /// </summary>
        public void __mStringSearchFocus()
        {
            _cStringSearch.Focus();
            return;
        }
        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>Список символьных полей в которых будет вестись поиск 
        /// </summary>
        public ArrayList __fFieldsCharList = new ArrayList();
        /// <summary>Название поля учетного кода
        /// </summary>
        public string __fFieldCode  = "";
        /// <summary>Идентификатор выбранной записи
        /// </summary>
        public int __fValueClue = 0;
        /// <summary>Название выбранной записи
        /// </summary>
        public string __fValueString = "";

        #endregion - Атрибуты

        #region - Внутренние

        /// <summary>(f) Статус поиска [true] поиск по вхождению, [false] поиск сначала строки
        /// </summary>
        private bool fSearchStatus = true;

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Таблица
        /// </summary>
        protected crlComponentGrid _cGrid = new crlComponentGrid();
        /// <summary>Разделитель панели фильтра и панели данных
        /// </summary>
        protected crlComponentSplitter _cSplitterInputData = new crlComponentSplitter();
        /// <summary>Кнопка 'Применить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSelect = new crlComponentToolBarButton();
        /// <summary>Надпись
        /// </summary>
        protected crlComponentLabel _cLabelSearch = new crlComponentLabel();
        /// <summary>Поле ввода текста для поиска
        /// </summary>
        protected crlComponentChar _cStringSearch = new crlComponentChar();
        /// <summary>Контекстное меню надписи-заголовка
        /// </summary>
        protected MenuItem[] _cMenuItemS = new MenuItem[2];

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>* Список отображаемых колонок
        /// </summary>
        public List<crlDataGridColumn> __fColumnsList_
        {
            get { return _cGrid.__fColumnsList; }
            set { _cGrid.__fColumnsList = value; }
        }
        /// <summary>* Сущность данных
        /// </summary>
        public datUnitEssence __fEssence_
        {
            get { return _cGrid.__oEssence; }
            set { _cGrid.__oEssence = value; }
        }
        /// <summary>* Текст в поле ввода данных для поиска
        /// </summary>
        public string __fStringSearchText_
        {
            get { return _cStringSearch.Text; }
            set { _cStringSearch.Text = value; }
        }
        /// <summary>* Начало выделения текста в поле ввода данных для поиска
        /// </summary>
        public int __fStringSearchSelectionStart_
        {
            get { return _cStringSearch.SelectionStart; }
            set { _cStringSearch.SelectionStart = value; }
        }
        /// <summary>* Количество выделяемых символов в поле ввожа данных для поиска
        /// </summary>
        public int __fStringSearchSelectionLength_
        {
            get { return _cStringSearch.SelectionLength; }
            set { _cStringSearch.SelectionLength = value; }
        }

        #endregion = СВОЙСТВА
    }
}
