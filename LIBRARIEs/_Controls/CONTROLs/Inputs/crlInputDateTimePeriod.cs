using nlApplication;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlInputDateTimePeriod'
    /// </summary>
    /// <remarks>Поле ввода периода данных даты и времени</remarks>
    public class crlInputDateTimePeriod : crlInput
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка контрола
        /// </summary>
        protected override void _mObjectAssembly()
        {
            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Panel2.Controls.Add(_cInput);
            Panel2.Controls.Add(_cInputTo);

            #endregion Размещение компонентов

            #region Настройка компонентов

            Height = 50;

            // _cLabelCaption
            {
                _cLabelCaption.__eMouseClickRight += __cLabelCaption_MouseClickRight;
            }
            fLabelCaptionStatus = _cLabelCaption.__fLabelType_; /// Сохранение установленного статуса надписи-заголовка
            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.__eValueInteractiveChanged += _cInput___eValueInteractiveChanged;
            }
            // _cInputTo
            {
                _cInputTo.Location = new Point(0, 25);
                _cInputTo.__eValueInteractiveChanged += _cInput___eValueInteractiveChanged;
            }

            #endregion Настройка компонентов

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }
        /// <summary>Выполняется при изменении введенных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cInput___eValueInteractiveChanged(object sender, EventArgs e)
        {
            __fCheckStatus_ = true;  /// Включение использования фильтра
        }

        #endregion Объект

        #region Контекстное меню

        private void __cLabelCaption_MouseClickRight(object sender, System.EventArgs e)
        {
            MenuItem[] vMenuItemCurrentS = new MenuItem[4];
            vMenuItemCurrentS[0] = new MenuItem(crlApplication.__oTunes.__mTranslate("Неделя"), MenuItemCurrent_Click);
            vMenuItemCurrentS[1] = new MenuItem(crlApplication.__oTunes.__mTranslate("Месяц"), MenuItemCurrent_Click);
            vMenuItemCurrentS[2] = new MenuItem(crlApplication.__oTunes.__mTranslate("Квартал"), MenuItemCurrent_Click);
            vMenuItemCurrentS[3] = new MenuItem(crlApplication.__oTunes.__mTranslate("Год"), MenuItemCurrent_Click);

            MenuItem[] vMenuItemBeforeS = new MenuItem[4];
            vMenuItemBeforeS[0] = new MenuItem(crlApplication.__oTunes.__mTranslate("Неделя"), MenuItemBefore_Click);
            vMenuItemBeforeS[1] = new MenuItem(crlApplication.__oTunes.__mTranslate("Месяц"), MenuItemBefore_Click);
            vMenuItemBeforeS[2] = new MenuItem(crlApplication.__oTunes.__mTranslate("Квартал"), MenuItemBefore_Click);
            vMenuItemBeforeS[3] = new MenuItem(crlApplication.__oTunes.__mTranslate("Год"), MenuItemBefore_Click);

            MenuItem[] vMenuItemS = new MenuItem[4];

            vMenuItemS[0] = new MenuItem(crlApplication.__oTunes.__mTranslate("Текущая"), vMenuItemCurrentS);
            vMenuItemS[1] = new MenuItem(crlApplication.__oTunes.__mTranslate("Предыдущая"), vMenuItemBeforeS);
            vMenuItemS[2] = new MenuItem(crlApplication.__oTunes.__mTranslate("С начала"), MenuItemEmptyStart_Click);
            vMenuItemS[3] = new MenuItem(crlApplication.__oTunes.__mTranslate("Пустой период"), MenuItemEmpty_Click);

            ContextMenu vContextMenu = new ContextMenu(vMenuItemS);
            _cLabelCaption.ContextMenu = vContextMenu;
        }
        private void MenuItemCurrent_Click(object sender, EventArgs e)
        {
            MenuItem vMenuItem = sender as MenuItem;
            
            switch (vMenuItem.Index)
            {
                case 0:
                    _cInput.__fValue_ = appTypeDateTime.__mWeekBegin(DateTime.Now);
                    _cInputTo.__fValue_ = appTypeDateTime.__mWeekEnd(DateTime.Now);
                    break;
                case 1:
                    _cInput.__fValue_ = appTypeDateTime.__mMonthBegin(DateTime.Now);
                    _cInputTo.__fValue_ = appTypeDateTime.__mMonthEnd(DateTime.Now);
                    break;
                case 2:
                    _cInput.__fValue_ = appTypeDateTime.__mQuarterBegin(DateTime.Now);
                    _cInputTo.__fValue_ = appTypeDateTime.__mQuarterEnd(DateTime.Now);
                    break;
                case 3:
                    _cInput.__fValue_ = appTypeDateTime.__mYearBegin(DateTime.Now);
                    _cInputTo.__fValue_ = appTypeDateTime.__mYearEnd(DateTime.Now);
                    break;
            }
            __fCheckStatus_ = true;
        }
        private void MenuItemBefore_Click(object sender, EventArgs e)
        {
            MenuItem vMenuItem = sender as MenuItem;
            switch (vMenuItem.Index)
            {
                case 0:
                    _cInput.__fValue_ = appTypeDateTime.__mWeekBegin(DateTime.Now.AddDays(-7));
                    _cInputTo.__fValue_ = appTypeDateTime.__mWeekEnd(DateTime.Now.AddDays(-7));
                    break;
                case 1:
                    _cInput.__fValue_ = appTypeDateTime.__mMonthBegin(DateTime.Now.AddDays(-31));
                    _cInputTo.__fValue_ = appTypeDateTime.__mMonthEnd(DateTime.Now.AddDays(-31));
                    break;
                case 2:
                    _cInput.__fValue_ = appTypeDateTime.__mQuarterBegin(DateTime.Now.AddDays(-91));
                    _cInputTo.__fValue_ = appTypeDateTime.__mQuarterEnd(DateTime.Now.AddDays(-91));
                    break;
                case 3:
                    _cInput.__fValue_ = appTypeDateTime.__mYearBegin(DateTime.Now.AddDays(-365));
                    _cInputTo.__fValue_ = appTypeDateTime.__mYearEnd(DateTime.Now.AddDays(-365));
                    break;
            }
            __fCheckStatus_ = true;
        }
        /// <summary>
        /// Период с начала отсчета 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemEmpty_Click(object sender, EventArgs e)
        {
            _cInput.__fValue_ = appTypeDateTime.__mMsSqlDateEmpty();
            _cInputTo.__fValue_ = appTypeDateTime.__mMsSqlDateEmpty();
            __fCheckStatus_ = true;
        }

        /// <summary>
        /// Период с начала отсчета 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemEmptyStart_Click(object sender, EventArgs e)
        {
            _cInput.__fValue_ = appTypeDateTime.__mMsSqlDateEmpty();
            __fCheckStatus_ = true;
        }

        #endregion Контекстное меню

        #endregion - Поведение

        #endregion = МЕТОДЫ  

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>Вид надписи перед переходом в недоступный режим
        /// </summary>
        private LABELTYPES fLabelCaptionStatus = LABELTYPES.Normal;

        #endregion - Внутренние

        #region - Компоненты

        /// <summary>Поле ввода даты времени с...
        /// </summary>
        protected crlComponentDateTime _cInput = new crlComponentDateTime();
        /// <summary>Поле ввода даты времени по...
        /// </summary>
        protected crlComponentDateTime _cInputTo = new crlComponentDateTime();

        #endregion - Компоненты

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>Доступность контрола
        /// </summary>
        public override bool __fEnabled_
        {
            get { return base.__fEnabled_; }
            set
            {
                base.__fEnabled_ = value;
                _cInput.Visible = value;
                if (value == true)
                {
                    _cLabelCaption.__fLabelType_ = fLabelCaptionStatus;
                }
                else
                {
                    _cLabelCaption.__fLabelType_ = LABELTYPES.Normal;
                    _cLabelValue.Text = crlApplication.__oTunes.__mTranslate("нет данных");
                }
            }
        }
        /// <summary>Условие фильтра
        /// </summary>
        public override string __fFilterExpression_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                string vFieldNameWithPrefix = ""; // Название поля с префиксом таблицы
                if (__fTableAlias.Trim().Length > 0)
                {
                    vFieldNameWithPrefix = __fTableAlias + "." + __fFieldName;
                } // Добавление префикса таблицы если он указан
                else
                {
                    vFieldNameWithPrefix = __fFieldName;
                } // Префикс таблицы не указан

                if (_cInput.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty() & _cInputTo.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty())
                {
                    vReturn = vFieldNameWithPrefix
                        + " Between "
                        + "Convert(DateTime, '" + appTypeDateTime.__mMsSqlDateTimeToString(Convert.ToDateTime(__fValue_)) + "')"
                        + " and "
                        + "Convert(DateTime, '" + appTypeDateTime.__mMsSqlDateTimeToString(Convert.ToDateTime(__fValueTo_)) + "')";
                } /// Даты 'c' и 'по' больше нулевой даты
                if (_cInput.__fValue_ == appTypeDateTime.__mMsSqlDateEmpty() & _cInputTo.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty())
                {
                    vReturn = vFieldNameWithPrefix
                        + " < "
                        + "Convert(DateTime, '" + appTypeDateTime.__mMsSqlDateTimeToString(Convert.ToDateTime(__fValueTo_)) + "')";
                } /// Дата 'c' нулевая, а 'по' больше нулевой даты
                if (_cInput.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty() & _cInputTo.__fValue_ == appTypeDateTime.__mMsSqlDateEmpty())
                {
                    vReturn = vFieldNameWithPrefix
                        + " > "
                        + "Convert(DateTime, '" + appTypeDateTime.__mMsSqlDateTimeToString(Convert.ToDateTime(__fValue_)) + "')";
                } /// Дата 'c' больше нулевой даты, а 'по' нулевая

                return vReturn;
            }
        }
        /// <summary>Получение условия фильтра для отображения пользователю
        /// </summary>
        public override string __fFilterMessage_
        {
            get
            {
                string vReturn = ""; // Возвращаемое значение

                if (__fCheckStatus_ == true)
                {
                    if (_cInput.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty() & _cInputTo.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty())
                    {
                        vReturn = __fCaption_ + " "
                            + crlApplication.__oTunes.__mTranslate("c") + " "
                            + _cInput.__fValue_.ToString() + " "
                            + crlApplication.__oTunes.__mTranslate("по") + " "
                            + _cInputTo.__fValue_.ToString();
                    } /// Даты 'c' и 'по' больше нулевой даты
                    if (_cInput.__fValue_ == appTypeDateTime.__mMsSqlDateEmpty() & _cInputTo.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty())
                    {
                        vReturn = __fCaption_ + " "
                            + crlApplication.__oTunes.__mTranslate("до") + " "
                            + _cInputTo.__fValue_.ToString();
                    } /// Дата 'c' нулевая, а 'по' больше нулевой даты
                    if (_cInput.__fValue_ > appTypeDateTime.__mMsSqlDateEmpty() & _cInputTo.__fValue_ == appTypeDateTime.__mMsSqlDateEmpty())
                    {
                        vReturn = __fCaption_ + " "
                            + crlApplication.__oTunes.__mTranslate("c") + " "
                            + _cInput.__fValue_.ToString() + " ";
                    } /// Дата 'c' больше нулевой даты, а 'по' нулевая
                }

                return vReturn;
            }
        }
        /// <summary>Обязательность заполнения
        /// </summary>
        public virtual FILLTYPES __pInputFillType
        {
            get { return _cInput.__fFillType_; }
            set
            {
                _cInput.__fFillType_ = value;
                _cInputTo.__fFillType_ = value;
            }
        }
        public bool __pValueInTicks
        {
            get { return _cInput.__fValueInTicks_; }
            set { _cInput.__fValueInTicks_ = value; }
        }
        public override object __fValue_
        {
            get { return _cInput.Value; }
            set { _cInput.Value = Convert.ToDateTime(value); }
        }
        public object __fValueTo_
        {
            get { return _cInputTo.Value; }
            set { _cInputTo.Value = Convert.ToDateTime(value); }
        }

        #endregion = СВОЙСТВА
    }
}
