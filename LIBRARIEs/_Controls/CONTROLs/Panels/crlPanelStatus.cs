

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace nlControls
{
    /// <summary>Класс 'crlPanelStatus'
    /// </summary>
    /// <remarks>Класс для отображения статуса формы</remarks>
    public class crlPanelStatus : crlComponentPanel
    {
        #region = ДИЗАЙНЕРЫ

        public crlPanelStatus()
        {
            _mLoad();
        }

        #endregion = ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка компонента
        /// </summary>
        protected virtual void _mLoad()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            SuspendLayout();

            #region Размещение компонентов

            Controls.Add(_cSplitter);
            _cSplitter.Panel1.Controls.Add(_cLabel);
            _cSplitter.Panel2.Controls.Add(_cProgress);

            #endregion Размещение компонентов

            #region Настройка компонентов

            BackColor = Color.Transparent;
            Dock = DockStyle.Bottom;

            Height = 25;

            // _cSplitter
            {
                _cSplitter.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                _cSplitter.BorderStyle = BorderStyle.Fixed3D;
                _cSplitter.IsSplitterFixed = true;
                _cSplitter.FixedPanel = FixedPanel.Panel2;
                _cSplitter.Size = new Size(Width - Height, Height);
                _cSplitter.SplitterDistance = _cSplitter.Width - 102;
                _cSplitter.TabStop = false;
            }
            // _cLabel
            {
                _cLabel.Location = new Point(5, 5);
            }
            // _cProgress
            {
                _cProgress.BackColor = Color.Aqua;
                _cProgress.Location = new Point(Width - 110);
                _cProgress.Size = new Size(100, 20);
            }
            // _cTimer
            {
                _cTimer.Interval = 5000;
                _cTimer.Tick += _cTimer_Tick;
            }
            #endregion Настройка компонентов

            ResumeLayout(false);
        }

        #endregion Объект

        private void _cTimer_Tick(object sender, EventArgs e)
        {
            _cLabel.Text = "";
            _cTimer.Stop();
        }

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Сборка выражения с параметрами и перевод выражения на язык интерфейса 
        /// </summary>
        /// <param name="pString">Текст</param>
        /// <param name="pParameters">Список дополнительных парамметров</param>
        public void __mCaptionBuilding(string pString, params object[] pParameters)
        {
            fTextWithOutTranslate = String.Format(pString, pParameters);
            _cLabel.Text = crlApplication.__oTunes.__mTranslate(pString, pParameters);
            _cTimer.Start();
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Не переведенное сообщение
        /// </summary>
        private string fTextWithOutTranslate = "";
        /// <summary>Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Надпись
        /// </summary>
        protected crlComponentLabel _cLabel = new crlComponentLabel();
        /// <summary>Прогресс
        /// </summary>
        protected crlComponentProgress _cProgress = new crlComponentProgress();
        /// <summary>Разделитель
        /// </summary>
        protected crlComponentSplitter _cSplitter = new crlComponentSplitter();
        /// <summary>Таймер
        /// </summary>
        protected Timer _cTimer = new Timer();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Текст заголовка
        /// </summary>
        /// <remarks>Отображаемый текст переводиться на язык интерфейса. Возвращается не переведенный текст</remarks>
        public string __fCaption_
        {
            get { return fTextWithOutTranslate; }
            set
            {
                fTextWithOutTranslate = value;
                _cLabel.Text = crlApplication.__oTunes.__mTranslate(fTextWithOutTranslate);
                _cTimer.Start();
            }
        }

        #endregion = СВОЙСТВА
    }
}
