using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlFormSearch'
    /// </summary>
    /// <remarks>Форма для поиска по текстовым полям</remarks>
    public class crlFormSearch : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(_cAreaSearch);
            Controls.SetChildIndex(_cAreaSearch, 0);

            #endregion Размещение компонентов

            #region Описание компонентов

            // _cAreaSearch
            {
                _cAreaSearch.Dock = DockStyle.Fill;
            }

            #endregion Описание компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

        }
        /// <summary>Выполняется при отпускании горячих клавиш
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1) // F1
            //    _cAreaSearch._cButtonHelp.PerformClick();
            if (e.Control == true & e.KeyCode == Keys.A) // Ctrl+A
                _cAreaSearch.__mButtonSelectClick();

            base.OnKeyUp(e);
        }

        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Загрузка настроек формы из файла
        /// </summary>
        /// <param name="pFormName">Название формы</param>
        protected override void __mTunesLoad(string pFormName)
        {
            base.__mTunesLoad(pFormName);

            #region Загрузка видимости полей

            //foreach (DataGridViewColumn vGridColumn in _cAreaSearch._fColumns)
            //{
            //    string vString = _oFileIni._mValueRead(pFormName.ToUpper(), "Field_" + vGridColumn.Name);
            //    try
            //    {
            //        vGridColumn.Visible = Convert.ToBoolean(vString);
            //    }
            //    catch
            //    {
            //        vGridColumn.Visible = true;
            //    }
            //}

            #endregion Загрузка видимости полей
        }
        /// <summary>Сохранение настроек формы в файл
        /// </summary>
        /// <param name="pFormName">Название формы</param>
        protected override void __mTunesSave(string pFormName)
        {
            base.__mTunesSave(pFormName);

            #region Сохранение видимости полей

            //foreach (DataGridViewColumn vGridColumn in _cAreaSearch._fColumns)
            //{
            //    _oFileIni._mValueWrite(vGridColumn.Visible.ToString(), pFormName.ToUpper(), "Field_" + vGridColumn.Name);
            //}

            #endregion Сохранение видимости полей
            /// Сохранение сортировки
            //_cAreaSearch.__mSortingSave();
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Область для правки табличных данных
        /// </summary>
        public crlAreaSearch _cAreaSearch = new crlAreaSearch();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Список отображаемых колонок
        /// </summary>
        public List<crlDataGridColumn> _fColumnsList
        {
            get { return _cAreaSearch.__fColumnsList_; }
            set { _cAreaSearch.__fColumnsList_ = value; }
        }

        #endregion = СВОЙСТВА
    }
}
