using nlApplication;
using nlControls;
using nlCsDocumentation;
using System;
using System.IO;
using System.Windows.Forms;

namespace naCsDocumentation
{
    /// <summary>
    /// Класс 'csdFormMain'
    /// </summary>
    /// <remarks>Главная форма приложения 'CsDocumentation'</remarks>
    public class csdFormMain : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Выполняется при выборе кнопки 'Помощь'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonHelp_Click(object sender, EventArgs e)
        {
            __mHelp();

            return;
        }
        /// <summary>
        /// Выполняется при выборе кнопки 'Выполнить'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cButtonRun_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_cFolderScan.__fValue_.ToString()) == true)
            {
                /// Сохранение папок проектов C#
                _cFolderScan.__mItemsSaveToFile("scan");

                csdDocumenting vDocumating = new csdDocumenting();
                //vDocumating.__fHeader2 = Convert.ToBoolean(_cHeader2Handle.__fValue_);
                //vDocumating.__fHeader4 = Convert.ToBoolean(_cHeader4Handle.__fValue_);
                vDocumating.__mDocumentingProject(_cFolderScan.__fValue_.ToString().Trim());
                //vDocumating.__mDocumentationToFile(Path.Combine(_cFolderScan.__fValue_.ToString().Trim(), "DOCUMENTATION"));
            }
            else
            {
                appUnitError vError = new appUnitError();
                vError.__mMessageBuild("Путь к проекту указан не верно");
                vError.__fErrorsType = ERRORSTYPES.User;
                vError.__fProcedure = _fClassNameFull + "_cButtonRun_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
            }

            return;
        }

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region /// Размещение компонентов

            Controls.Add(_cBlockInput);
            Controls.Add(_cToolbar);
            _cToolbar.Items.Add(_cButtonRun);
            _cToolbar.Items.Add(_cButtonHelp);

            _cBlockInput.__mInputAdd(_cFolderScan);

            #endregion Размещение компонентов

            #region /// Настройка компонентов

            __mCaptionBuilding("Документирование CS проектов");
            ShowInTaskbar = true;

            // _cButtonRun
            {
                _cButtonRun.Image = global::nlResourcesImages.Properties.Resources._SignApply_g32C;
                _cButtonRun.Click += _cButtonRun_Click;
            }
            // _cButtonHelp
            {
                _cButtonHelp.Image = global::nlResourcesImages.Properties.Resources._SignQuestion_b32C;
                _cButtonHelp.Click += _cButtonHelp_Click;
            }
            // _cBlockInput
            {
                _cBlockInput.Dock = DockStyle.Fill;
            }
            // _cFolderScan
            {
                _cFolderScan.__fCaption_ = "Путь к проекту";
                _cFolderScan.__fPathType = PATHTYPES.Folder;
                _cFolderScan.__fCheckVisible_ = false;
            }

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка настроек текущей формы
        /// </summary>
        protected override void __mTunesLoad()
        {
            _cFolderScan.__mItemsLoadFromFile("scan");

            base.__mTunesLoad();

            return;
        }
        /// <summary>
        /// Сохранение настроек текущей формы
        /// </summary>
        protected override void __mTunesSave()
        {
            _cFolderScan.__mItemsSaveToFile("scan");

            base.__mTunesSave();

            return;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Панель управления
        /// </summary>
        protected crlComponentToolBar _cToolbar = new crlComponentToolBar();
        /// <summary>
        /// Кнопка 'Выполнить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonRun = new crlComponentToolBarButton();
        /// <summary>
        /// Кнопка 'Помощь'
        /// </summary>
        protected crlComponentToolBarButton _cButtonHelp = new crlComponentToolBarButton();
        /// <summary>
        /// Панель для размещения компонентов
        /// </summary>
        protected crlBlockInputs _cBlockInput = new crlBlockInputs();
        /// <summary>
        /// Путь и имя папки для сканирования документов CS
        /// </summary>
        protected crlInputComboPath _cFolderScan = new crlInputComboPath();

        #endregion Компоненты

        #endregion ПОЛЯ
    }
}
