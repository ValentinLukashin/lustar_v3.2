﻿using nlControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlFormTray
{
    /// <summary>
    /// Класс 'traComponentNotifyIcon'
    /// </summary>
    /// <remarks>Создание и работа с иконкой в трее</remarks>
    public class traComponentNotifyIcon
    {
        #region = ДИЗАЙНЕРЫ

        /// <summary>
        /// Конструктор
        /// </summary>
        public traComponentNotifyIcon()
        {
            __mObjectAssembly();
        }

        #endregion ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void __mObjectAssembly()
        {
            #region /// Размещение компонентов
            #endregion Размещение компонентов

            #region /// Настройка компонентов

            cNotifyIcon.DoubleClick += mNotifyIcon_DoubleClick;
            cNotifyIcon.MouseClick += __mNotifyIcon_MouseClick;
            cNotifyIcon.Visible = true;

            #endregion Настройка компонентов

            return;
        }
        /// <summary>
        /// Выполняется при двойном клике мыши по иконке в трее
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (__eNotifyIconDoubleClick != null)
                __eNotifyIconDoubleClick(cNotifyIcon, new EventArgs());
        }
        /// <summary>
        /// Выполняется при клике мыши по иконке в трее
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void __mNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (__eOnMouseClickLeft != null)
                    __eOnMouseClickLeft(this, e);
                else
                {
                    if (cForm is crlForm)
                    {
                        if (cForm.Created == false)
                            cForm.ShowDialog();
                        else
                            cForm.WindowState = FormWindowState.Normal;
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (__eOnMouseClickRight != null)
                    __eOnMouseClickRight(this, e);
            }
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Создание контекстного меню
        /// </summary>
        public void __mContextMenuCrteate(traComponentPopupMenu pPopupMenu)
        {
            cNotifyIcon.ContextMenuStrip = pPopupMenu;
        }

        /// <summary>
        /// Скрытие иконки в трее
        /// </summary>
        public void __mIconHide()
        {
            cNotifyIcon.Visible = false;
        }
        /// <summary>
        /// Вывод на экран всплывающего сообщения
        /// </summary>
        /// <param name="pInterval">Время индикации в миллисекундах</param>
        /// <param name="pMessage">Отображаемое сообщение</param>
        public void __mShowBaloon(int pInterval, string pMessage, string pTitle = "", Icon pIcon = null)
        {
            if (String.IsNullOrEmpty(pTitle) == false)
                cNotifyIcon.BalloonTipTitle = pTitle;
            cNotifyIcon.BalloonTipText = pMessage;
            if (pIcon != null)
                cNotifyIcon.Icon = pIcon;
            cNotifyIcon.ShowBalloonTip(pInterval);
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>
        /// Отображаемая форма
        /// </summary>
        crlForm cForm;
        /// <summary>
        /// Иконка в трее
        /// </summary>
        NotifyIcon cNotifyIcon = new NotifyIcon();

        #endregion Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст всплывающей подсказки
        /// </summary>
        public string __fBalloonTipText
        {
            get { return cNotifyIcon.BalloonTipText; }
            set { cNotifyIcon.BalloonTipText = value.Trim(); }
        }
        /// <summary>
        /// Заголовок всплывающей подсказки
        /// </summary>
        public string __fBalloonTipTitle
        {
            get { return cNotifyIcon.BalloonTipTitle; }
            set { cNotifyIcon.BalloonTipTitle = value.Trim(); }
        }
        /// <summary>
        /// Изображение иконки
        /// </summary>
        public Icon __oIcon
        {
            get { return cNotifyIcon.Icon; }
            set { cNotifyIcon.Icon = value; }
        }
        /// <summary>
        /// Текст всплывающий при наведении курсора
        /// </summary>
        public string __fText
        {
            get { return cNotifyIcon.Text; }
            set { cNotifyIcon.Text = value.Trim(); }
        }
        /// <summary>
        /// Форма для отображения настроек
        /// </summary>
        public crlForm __cForm
        {
            get { return cForm; }
            set { cForm = value; }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при двойном клике по иконке в трее
        /// </summary>
        public event EventHandler __eNotifyIconDoubleClick;
        /// <summary>
        /// Возникает при клике левой кнопки мыши по иконке в трее
        /// </summary>
        public event EventHandler __eOnMouseClickLeft;
        /// <summary>
        /// Возникает при клике правой кнопки мыши по иконке в трее
        /// </summary>
        public event EventHandler __eOnMouseClickRight;

        #endregion СОБЫТИЯ
    }
}
