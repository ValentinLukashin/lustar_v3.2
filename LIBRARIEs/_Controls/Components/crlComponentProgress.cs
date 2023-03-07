using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace nlControls
{
    /// <summary>Класс 'crlComponentProgress'
    /// </summary>
    /// <remarks>Компонент для отображения процента выполненной работы</remarks>
    public class crlComponentProgress : crlComponentPanel
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public crlComponentProgress()
        {
            _mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

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

            #region Загрузка компонентов

            Controls.Add(cLabelLeft);
            Controls.Add(cPercent);
            Controls.Add(cLabelRight);

            #endregion Загрузка компонентов

            #region Настройка компонентов

            BorderStyle = BorderStyle.Fixed3D;
            Size = new Size(100, 25);
            TabStop = false;

            // cPercent
            {
                //cPercent.BackColor = Color.Blue;
                cPercent.Size = new Size(100, 25);
                cPercent.Top = 0;
            }
            // cLabelLeft
            {
                cLabelLeft.AutoSize = false;
                cLabelLeft.BackColor = Color.Blue;
                cLabelLeft.ForeColor = Color.Yellow;
                cLabelLeft.Location = new Point(0, 0);
                cLabelLeft.Text = "0%";
                cLabelLeft.TextAlign = ContentAlignment.MiddleCenter;
                cLabelLeft.Width = 100;
            }
            // cLabelRight
            {
                cLabelRight.AutoSize = false;
                cLabelRight.BackColor = Color.Yellow;
                cLabelRight.ForeColor = Color.Blue;
                cLabelRight.Location = new Point(0, 0);
                cLabelRight.Text = "0%";
                cLabelRight.TextAlign = ContentAlignment.MiddleCenter;
                cLabelRight.Width = 100;
            }

            #endregion Настройка копонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";

            return;
        }

        #endregion Объект

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Процент выполненной работы
        /// </summary>
        private int _fPercent = 0;

        #endregion Внутренние

        #region - Компненты

        /// <summary>
        /// Панель
        /// </summary>
        public Panel cPercent = new Panel();
        /// <summary>
        /// Левая надпись
        /// </summary>
        private crlComponentLabel cLabelLeft = new crlComponentLabel();
        /// <summary>
        /// Правая надпись
        /// </summary>
        private crlComponentLabel cLabelRight = new crlComponentLabel();

        #endregion Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Процент выполненной работы
        /// </summary>
        public int __pPercent_
        {
            get { return _fPercent; }
            set
            {
                int vValue = value;
                if (vValue <= 0)
                    vValue = 0;
                if (vValue > 100)
                    vValue = 100;
                _fPercent = value;
                cPercent.Left = _fPercent - 100;
                cLabelLeft.Text = value.ToString().Trim() + "%";
                cLabelRight.Text = value.ToString().Trim() + "%";
            }
        }
        /// <summary>
        /// Отображение цифр в прогресс баре
        /// </summary>
        public bool __pNumbersVisible_
        {
            get { return cLabelLeft.Visible; }
            set
            {
                cLabelLeft.Visible = value;
                cLabelRight.Visible = cLabelLeft.Visible;
            }
        }

        #endregion СВОЙСТВА
    }
}
