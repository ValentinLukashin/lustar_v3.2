using mshtml;
using nlApplication;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentBrowser'
    /// </summary>
    /// <remarks>Компонент для просмотра HTML файлов</remarks>
    public class crlComponentBrowser : WebBrowser
    {
        #region = БИБЛИОТЕКИ

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

        #endregion БИБЛИОТЕКИ

        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentBrowser()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            SuspendLayout();

            ScriptErrorsSuppressed = true; // Отключение отображения сообщения об ошибках скриптов
            IsWebBrowserContextMenuEnabled = false; // Отключение стандартного контекстного меню

            // "Googlebot/2.1 (+http://www.google.com/bot.html)"
            // "Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 7.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 820)"
            //ChangeUserAgent("Googlebot/2.1 (+http://www.google.com/bot.html)"); // Google
            __mChangeUserAgent("Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv 11.0) like Gecko"); // IE11

            this.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.mDocumentCompleted);
            this.NewWindow += new CancelEventHandler(this.mNewWindow);
            this.ProgressChanged += new WebBrowserProgressChangedEventHandler(this.mProgressChanged);
            this.Navigated += new WebBrowserNavigatedEventHandler(this.__mWeb_Navigated);

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется когда страница загружена полностью
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != (sender as WebBrowser).Url.AbsolutePath)
            {
                if (e.Url.AbsolutePath.ToUpper().Contains("ABOUT:BLANK") == false)
                    return;
            }
            //if (e.Url.AbsoluteUri != __Url)
            //    return;
            else
            {
                fUrl = e.Url.ToString().Trim();
                //_PageLoaded(__Url);
            }
            // 1-й вариант
            //if (e.Url.AbsolutePath != (sender as System.Windows.Forms.WebBrowser).Url.AbsolutePath)
            //    return;

            // 2-й вариант
            //while (ReadyState != WebBrowserReadyState.Complete)
            //{
            //    Application.DoEvents();
            //}
            Document.MouseDown += __mDocument_MouseDown;
            __fDocumentLoaded = true;

            return;
        }
        /// <summary>
        /// Выполняется при создании нового окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mNewWindow(object sender, CancelEventArgs e)
        {
            Navigate(((System.Windows.Forms.WebBrowser)sender).StatusText); // Запрет открытия страницы в новом окне
            e.Cancel = true;

            return;
        }
        /// <summary>
        /// Выполняется когда изменяется состояние процесса загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            //this._tsProgressBar.Value = (int)
            //    (e.CurrentProgress * 100 / e.MaximumProgress);
            // _ProgressChange(e.MaximumProgress, e.CurrentProgress);
            return;
        }
        /// <summary>
        /// Выполняется при смене адреса 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigating(WebBrowserNavigatingEventArgs e)
        {
            string vUrlAddress = e.Url.ToString(); // Url Address
            //appFileHtml vFileHtml = new appFileHtml(); // Объект для работы с HTML файлами
            //vUrlAddress = appTypeString._mSymbolsLastDelete(vUrlAddress, "/");
            if (__mIsAddressUrl(vUrlAddress) == true)
                e.Cancel = false;
            else
                e.Cancel = true;
            base.OnNavigating(e);

            return;
        }
        private void __mNavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            if (StatusCode.ToString() == "404")
            {
                MessageBox.Show("Page no found");
            }

            return;
        }
        private void __mWeb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // Get htmlDocument
            //       HTMLDocument doc = (HTMLDocument)((WebBrowser)sender).Document;

            //html reset css - 0 is the insert/index position
            //     IHTMLStyleSheet resetCss = doc.createStyleSheet("http://www.fablecode.com/css/reset.css", 0);
            return;
        }
        public void __mChangeUserAgent(string Agent)
        {
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, Agent, Agent.Length, 0);

            return;
        }
        /// <summary>
        /// Выполняется при клике левой кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void __mDocument_MouseDown(object sender, HtmlElementEventArgs e)
        {
            if (e.MouseButtonsPressed == MouseButtons.Right) /// Клик по правой кнопки мыши
            {
                if (Document != null)
                {
                    HtmlElement vHtmlElement = Document.GetElementFromPoint(e.ClientMousePosition);
                    __fElementUnderMouseTagName = vHtmlElement.TagName;
                    if (__fElementUnderMouseTagName == "A") /// Курсор мыши находиться над ссылкой
                        __fLinkUnderMouse = appTypeString.__mWordNumber(vHtmlElement.OuterHtml, 1, "\"");
                    if (__fElementUnderMouseTagName == "IMG") /// Курсор мыши находиться над изображением
                    {
                        Regex vRegex = new Regex(@"(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+");
                        var mts = vRegex.Matches(vHtmlElement.OuterHtml);
                        __fLinkUnderMouse = mts[0].Value;
                    }

                    if (__eMouseClickRight != null)
                        __eMouseClickRight(sender, e);
                }
            }

            return;
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Проверка, является ли адрес web страницей
        /// </summary>
        /// <param name="pAddress">Предполагаемая web страница</param>
        /// <returns></returns>
        public bool __mIsAddressUrl(string pAddress)
        {
            bool vReturn = false; // Возвращаемое значение
            Uri myUri;
            if (Uri.TryCreate(pAddress, UriKind.Absolute, out myUri))
            {
                vReturn = true;
            }

            return vReturn;
        }
        /// <summary>
        /// Изменение масштаба
        /// </summary>
        /// <param name="pZoom">Устанавливаемый масштаб</param>
        /// <returns>[true] - масштаб был изменен, [false] - масштаб достиг предельного значения</returns>
        public void __mZoom(int pZoom)
        {
            Document.Body.Style = "zoom:" + pZoom + "%;";

            return;
        }
        /// <summary>
        /// Сохранение всех изображений открытой страницы
        /// </summary>
        /// <param name="pFolderPath">Путь и имя папки для размещения файлов</param>
        public void __mSaveAllImages(string pFolderPath)
        {
            IHTMLDocument2 doc = (IHTMLDocument2)this.Document.DomDocument;
            IHTMLControlRange imgRange = (IHTMLControlRange)((HTMLBody)doc.body).createControlRange();

            foreach (IHTMLImgElement img in doc.images)
            {
                imgRange.add((IHTMLControlElement)img);

                imgRange.execCommand("Copy", false, null);

                using (Bitmap bmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap))
                {
                    bmp.Save(@"C:\" + img.nameProp);
                }
            }

            return;
        }
        /// <summary>
        /// Чтение описания документа из Meta данных
        /// </summary>
        public void __mDisplayMetaDescription()
        {
            if (Document != null)
            {
                HtmlElementCollection elems = Document.GetElementsByTagName("META");
                foreach (HtmlElement elem in elems)
                {
                    String nameStr = elem.GetAttribute("name");
                    if (nameStr != null && nameStr.Length != 0)
                    {
                        String contentStr = elem.GetAttribute("content");
                        MessageBox.Show("Document: " + Url.ToString() + "\nDescription: " + contentStr);
                    }
                }
            }

            return;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Атрибуты

        /// <summary>
        /// Документ загружен, загрузка документа завершена
        /// </summary>
        public bool __fDocumentLoaded = false;
        /// <summary>
        /// Название тэга элемента находящего под курсором во время нажатия правой кнопки мыши
        /// </summary>
        public string __fElementUnderMouseTagName = "";
        /// <summary>
        /// Значение ссылки под курсором
        /// </summary>
        public string __fLinkUnderMouse = "";

        #endregion Атрибуты

        #region - Внутренние 

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region - Константы

        const int URLMON_OPTION_USERAGENT = 0x10000001;

        #endregion Константы

        #region = Служебные

        /// <summary>
        /// Загружаемый адрес
        /// </summary>
        private string fUrl = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Загружаемый адрес
        /// </summary>
        public string __pUrl
        {
            get { return fUrl.ToString(); }
            set
            {
                fUrl = value.Trim();
                __fDocumentLoaded = false;
                Navigate(fUrl);
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при клике правой кнопки мыши по компоненту
        /// </summary>
        public event EventHandler __eMouseClickRight;

        #endregion СОБЫТИЯ
    }
}
