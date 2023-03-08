using System;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlFormDocument'
    /// </summary>
    /// <remarks>Абстактный класс формы для построения форм для правки документов</remarks>
    public abstract class crlFormDocument : crlForm
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка формы
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(__cAreaDocument);
            Controls.SetChildIndex(__cAreaDocument, 0);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Text = "Базовая форма для правки документов";

            // _cAreaDocument
            {
                __cAreaDocument.Dock = DockStyle.Fill;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }

        #endregion Объект

        #endregion - Поведение

        #region - Процедуры

        /// <summary> Загрузка настроек текущей формы из файла
        /// </summary>
        protected override void __mTunesLoad()
        {
            base.__mTunesLoad();

            string vString = __oFileIni.__mValueRead(Name.ToUpper(), "SplitterCaptionContent");
            if (vString.Length == 0)
                vString = "200";
            __cAreaDocument.__fSplitterCaptionContentSplitterDistance_ = Convert.ToInt32(vString);

            vString = __oFileIni.__mValueRead(Name.ToUpper(), "SplitterCaptionLeftRight");
            if (vString.Length == 0)
                vString = "200";
            __cAreaDocument.__fSplitterCaptionLeftRightSplitterDistance_ = Convert.ToInt32(vString);
            
            return;
        }
        /// <summary> Сохранение настроек текущей формы в файл
        /// </summary>
        protected override void __mTunesSave()
        {
            base.__mTunesSave();

            __oFileIni.__mValueWrite(__cAreaDocument.__fSplitterCaptionContentSplitterDistance_.ToString(), Name.ToUpper(), "SplitterCaptionContent");
            __oFileIni.__mValueWrite(__cAreaDocument.__fSplitterCaptionLeftRightSplitterDistance_.ToString(), Name.ToUpper(), "SplitterCaptionLeftRight");

            return;
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Область для правки документов
        /// </summary>
        public crlAreaDocument __cAreaDocument = new crlAreaDocument();

        #endregion Компоненты

        #endregion ПОЛЯ
    }
}
