using MsExcel = Microsoft.Office.Interop.Excel;
using MsWord = Microsoft.Office.Interop.Word;
using nlApplication;
using System;
using System.IO;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlAreaReportPreview'
    /// </summary>
    /// <remarks>Область для предварительного просомтра отчетов</remarks>
    public class crlAreaReportPreview : crlArea
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Построение объекта
        /// </summary>
        protected override void _mObjectAssembly()
        {
            base._mObjectAssembly();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            SuspendLayout();

            #region Размещение компонентов

            Panel2.Controls.Add(_cBrowser);
            Panel2.Controls.SetChildIndex(_cBrowser, 0);
            __cToolBar.Items.Add(__cButtonSave);
            __cToolBar.Items.Add(_cButtonHelp);
            __cToolBar.Items.Add(__cButtonEdit);
            __cToolBar.Items.Add(__cButtonOperations);

            __cButtonEdit.DropDownItems.Add(__cButtonEditBrowser);
            __cButtonEdit.DropDownItems.Add(__cButtonEditWord);
            __cButtonEdit.DropDownItems.Add(__cButtonEditExcel);

            __cButtonOperations.DropDownItems.Add(__cButtonOperationsUserRights);

            #endregion Размещение компонентов

            #region Настройка компонентов


            // __cButtonSave
            {
                __cButtonSave.Click += cButtonSave_Click;
                __cButtonSave.Image = nlResourcesImages.Properties.Resources._Diskette_b32C;
            }
            //// __cButtonHelp
            //{
            //    __cButtonHelp.Image = nlResourcesImages.Properties.Resources._SignQuestion_b32C;
            //}
            // __cButtonEdit
            {
                __cButtonEdit.Alignment = ToolStripItemAlignment.Right;
                __cButtonEdit.Image = nlResourcesImages.Properties.Resources._PageEdit_y32;
                {
                    __cButtonEditBrowser.Click += cButtonBrowser_Click;
                    __cButtonEditBrowser.Image = nlResourcesImages.Properties.Resources._ApplicationGoogle_16C;
                    __cButtonEditBrowser.__mCaptionBuilding("Открыть в Интернет браузере");
                    __cButtonEditExcel.Click += cButtonExcel_Click;
                    __cButtonEditExcel.Image = nlResourcesImages.Properties.Resources._ApplicationMSExcel_g16C;
                    __cButtonEditExcel.__mCaptionBuilding("Открыть в MS Excel");
                    __cButtonEditWord.Image = nlResourcesImages.Properties.Resources._ApplicationMSWord_b16C;
                    __cButtonEditWord.__mCaptionBuilding("Открыть в MS Word");
                    __cButtonEditWord.Click += cButtonWord_Click;
                }
            }
            // __cButtonOperations
            {
                __cButtonOperations.Alignment = ToolStripItemAlignment.Right;
                __cButtonOperations.Image = nlResourcesImages.Properties.Resources._PageGear_y32;
                {
                    __cButtonOperationsUserRights.Image = nlResourcesImages.Properties.Resources._UserEdit_b16;
                    __cButtonOperationsUserRights.__mCaptionBuilding("Права пользователей");
                }
            }
            // __cBrowser
            {
                _cBrowser.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);
        }

        #region Кнопки управления

        private void cButtonSave_Click(object sender, EventArgs e)
        {
            appFileHtml vFileHtml = new appFileHtml(); // Объект для работы с Html файлами
            string vFilePath = vFileHtml.__mAdressToString(_cBrowser.__pUrl);
            // ; /// Чтение файла
            string vTagValue = vFileHtml.__mReadTag(vFilePath, "TITLE"); /// Чтение тэга 'Title';
            string vFileReportPath = ""; // Путь и имя файла сохраняемого отчета
            if (vTagValue.Length > 0)
                vFileReportPath = Path.Combine(crlApplication.__oPathes.__fFolderReports_, vTagValue + "_" + appTypeDateTime.__mDateTimeToFileNameTillSecond(DateTime.Now) + ".htm");
            else
                vFileReportPath = Path.Combine(crlApplication.__oPathes.__fFolderReports_, Path.GetFileNameWithoutExtension(vFilePath) + "_" + appTypeDateTime.__mDateTimeToFileNameTillSecond(DateTime.Now) + Path.GetExtension(vFilePath));

            File.Copy(vFilePath, vFileReportPath, true);

            if (File.Exists(vFileReportPath) == true)
                crlApplication.__oMessages.__mShow(MESSAGESTYPES.Info, "Отчет сохранен", crlApplication.__oTunes.__mTranslate("Файл") + ":" + vFileReportPath, _fClassNameFull + "cButtonSave_Click(object, EventArgs)");
        }
        private void cButtonBrowser_Click(object sender, EventArgs e)
        {
            string vReportFilePath = appTypeString.__mUrlToFile(_cBrowser.__pUrl); // Путь и название файла отчета
            if (vReportFilePath.Length == 0)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Имя файла отчета не указано");
                vError.__fProcedure = _fClassNameFull + "cButtonBrowser_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
                return;
            }
            if (File.Exists(vReportFilePath) == false)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Имя файла указано не верно");
                vError.__fProcedure = _fClassNameFull + "cButtonBrowser_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
                return;
            }
            System.Diagnostics.Process.Start(vReportFilePath);
        }
        /// <summary>Выполняется при выборе кнопки 'Открыть в MS Word'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cButtonWord_Click(object sender, EventArgs e)
        {
            object vReportFilePath = _cBrowser.__pUrl; // Путь и название файла отчета

            if (appTypeString.__mUrlToFile(vReportFilePath.ToString()).Length == 0)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Имя файла отчета не указано");
                vError.__fProcedure = _fClassNameFull + "cButtonWord_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
                return;
            }
            if (File.Exists(appTypeString.__mUrlToFile(vReportFilePath.ToString())) == false)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Имя файла указано не верно");
                vError.__fProcedure = _fClassNameFull + "cButtonWord_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
                return;
            }

            object vReadOnly = false;
            object vIsVisible = true;
            object vFormat = MsWord.WdOpenFormat.wdOpenFormatWebPages; // https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.word.wdopenformat?view=word-pia
            MsWord.Application wordObject = new MsWord.Application();
            object vNull = System.Reflection.Missing.Value;

            MsWord._Document docs = wordObject.Documents.Open(ref vReportFilePath, ref vIsVisible, ref vReadOnly, ref vNull, ref vNull, ref vNull, ref vNull, ref vNull, ref vNull, ref vFormat, ref vNull, ref vIsVisible, ref vNull, ref vNull, ref vNull, ref vNull);

            wordObject.Visible = true;
        }
        /// <summary>Выполняется при выборе кнопки 'Открыть в MS Excel'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cButtonExcel_Click(object sender, EventArgs e)
        {
            //ntwTypeString vTypeString = new ntwTypeString();
            string vReportFilePath = appTypeString.__mUrlToFile(_cBrowser.__pUrl); // Путь и название файла отчета
            if (vReportFilePath.Length == 0)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Имя файла отчета не указано");
                vError.__fProcedure = _fClassNameFull + "cButtonExcel_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
                return;
            }
            if (File.Exists(vReportFilePath) == false)
            {
                appUnitError vError = new appUnitError();
                vError.__fErrorsType = ERRORSTYPES.Programming;
                vError.__mMessageBuild("Имя файла указано не верно");
                vError.__fProcedure = _fClassNameFull + "cButtonExcel_Click(object, EventArgs)";
                crlApplication.__oErrorsHandler.__mShow(vError);
                return;
            }
            MsExcel.Application vExcel;
            vExcel = new MsExcel.Application();
            MsExcel.Workbook voWorkBook = vExcel.Workbooks.Open(vReportFilePath);
            vExcel.Visible = true;
        }

        #endregion Кнопки управления

        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        ///// <summary>Панель управления
        ///// </summary>
        //protected crlComponentToolBar __cToolBar = new crlComponentToolBar();
        /// <summary>* Кнопка 'Применить'
        /// </summary>
        protected crlComponentToolBarButton __cButtonSave = new crlComponentToolBarButton();
        ///// <summary>Кнопка 'Помощь'
        ///// </summary>
        //protected crlComponentToolBarButton __cButtonHelp = new crlComponentToolBarButton();
        /// <summary>* Кнопка 'Правка'
        /// </summary>
        protected crlComponentToolBarButtonMenu __cButtonEdit = new crlComponentToolBarButtonMenu();
        /// <summary>* Кнопка 'Операции'
        /// </summary>
        protected crlComponentToolBarButtonMenu __cButtonOperations = new crlComponentToolBarButtonMenu();

        /// <summary>* Пункт меню 'Правка/Открыть в Браузере'
        /// </summary>
        protected crlComponentMenuItem __cButtonEditBrowser = new crlComponentMenuItem();
        /// <summary>* Пункт меню 'Правка/Открыть в MS Word'
        /// </summary>
        protected crlComponentMenuItem __cButtonEditWord = new crlComponentMenuItem();
        /// <summary>* Пункт меню 'Правка/Открыть в MS Excel'
        /// </summary>
        protected crlComponentMenuItem __cButtonEditExcel = new crlComponentMenuItem();

        /// <summary>* Пункт меню 'Операции/Определение прав пользователей'
        /// </summary>
        protected crlComponentMenuItem __cButtonOperationsUserRights = new crlComponentMenuItem();

        /// <summary>* WebBrowser
        /// </summary>
        protected crlComponentBrowser _cBrowser = new crlComponentBrowser();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Загружаемый адрес
        /// </summary>
        public string _fUrl
        {
            get { return _cBrowser.__pUrl; }
            set { _cBrowser.__pUrl = value; }
        }

        #endregion = СВОЙСТВА
    }
}
