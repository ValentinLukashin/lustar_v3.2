using nlApplication;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    public class crlFormTunes : crlForm
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

            Controls.Add(_cBlockInputs);
            Controls.Add(_cToolBar);
            _cToolBar.Items.Add(_cButtonSave);
            _cToolBar.Items.Add(_cButtonHelp);

            #endregion Размещение компонентов

            #region Настройка компонентов

            __mCaptionBuilding("Настройки приложения");
            _fHelpTopic = "";

            // _cButtonSave
            {
                _cButtonSave.Image = global::nlResourcesImages.Properties.Resources._Diskette_b32C;
                _cButtonSave.ToolTipText = "[ Ctrl + A ] " + crlApplication.__oTunes.__mTranslate("Сохранить");
                _cButtonSave.Click += mButtonSave_Click;
            }           
            // _cButtonHelp
            {
                _cButtonHelp.Image = global::nlResourcesImages.Properties.Resources._SignQuestion_b32C;
                _cButtonHelp.ToolTipText = "[ F1 ] " + crlApplication.__oTunes.__mTranslate("Помощь");
                _cButtonHelp.Click += mButtonHelp_Click;
                _cButtonHelp.__eMouseClickRight += mButtonHelp___eMouseClickRight;
            }
            // _cBlockInputs
            {
                _cBlockInputs.Dock = DockStyle.Fill;
                _cBlockInputs.__fCheckShow = false;
            }

            #endregion Настройка компонентов

            ResumeLayout();

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.FullName + ".";
        }

        protected override void OnCreateControl()
        {
            __mDataLoad();
            base.OnCreateControl();
        }

        #endregion Объект

        #region Кнопки управления

        /// <summary>Выпоняется при выборе кнопки 'Помощь' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonHelp_Click(object sender, EventArgs e)
        {
            __mHelp();
        }
        /// <summary>Выпоняется при выборе кнопки 'Помощь' правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonHelp___eMouseClickRight(object sender, EventArgs e)
        {
            Form vForm = FindForm() as Form;
            if (vForm.MinimumSize.Width > 0)
            {
                vForm.MinimumSize = new Size(0, 0);
                (FindForm() as crlForm).__cStatus.__fCaption_ = "Минимальные размеры формы сброшены";
            }
            else
            {
                vForm.MinimumSize = new Size(vForm.Width, vForm.Height);
                (FindForm() as crlForm).__cStatus.__fCaption_ = "Текущие размеры установлены как минимальные";
            }
        }
        /// <summary>Выпоняется при выборе кнопки 'Сохранить' левой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mButtonSave_Click(object sender, EventArgs e)
        {
            int vTuneIndex = 0;

            foreach (crlInput vInput in _cBlockInputs.Controls)
            {
                if (vInput is crlInputComboBool)
                {
                    vTuneIndex = (vInput as crlInputComboBool).__fTuneIndex;
                    crlApplication.__oTunes.__mTuneWrite(vTuneIndex, vInput.__fValue_);
                }
                if (vInput is crlInputChar)
                {
                    vTuneIndex = (vInput as crlInputChar).__fTuneIndex;
                    crlApplication.__oTunes.__mTuneWrite(vTuneIndex, vInput.__fValue_);
                }
                if (vInput is crlInputPath)
                {
                    vTuneIndex = (vInput as crlInputPath).__fTuneIndex;
                    crlApplication.__oTunes.__mTuneWrite(vTuneIndex, vInput.__fValue_);
                }
            }

            Close();
        }

        #endregion Кнопки управления

        #endregion - Поведение

        #region - Процедуры

        /// <summary>Загрузка данных
        /// </summary>
        public void __mDataLoad()
        {
            for (int vAmount = 0; vAmount < crlApplication.__oTunes.__fTunesCount; vAmount++)
            { /// Выполняется перебор настроек
                appUnitTune vUnitTune = crlApplication.__oTunes.__mTuneByIndex(vAmount); /// Получение настройки
                switch (vUnitTune.__fObjectForEdit)
                {
                    case "crlInputComboBool": /// Редактирование логического выражения
                        {
                            crlInputComboBool vComboBool = new crlInputComboBool();
                            vComboBool.__fCaption_ = vUnitTune.__fDescription;
                            vComboBool.__fTuneIndex = vAmount;
                            vComboBool.__fEnabled_ = vUnitTune.__fEdited;
                            _cBlockInputs.__mInputAdd(vComboBool);
                            vComboBool.__fValue_ = Convert.ToBoolean(vUnitTune.__fValue);
                            break;
                        }
                    case "crlInputChar": /// Редактирование строки
                        {
                            crlInputChar vChar = new crlInputChar();
                            vChar.__fCaption_ = vUnitTune.__fDescription;
                            vChar.__fTuneIndex = vAmount;
                            vChar.__fSymbolsCount_ = -1;
                            vChar.__fEnabled_ = vUnitTune.__fEdited;
                            _cBlockInputs.__mInputAdd(vChar); /// Добавление компонента
                            vChar.__fValue_ = vUnitTune.__fValue;
                            break;
                        }
                    case "crlInputPath": /// Редактирование пути к папке
                        {
                            crlInputPath vPath = new crlInputPath();
                            vPath.__fCaption_ = vUnitTune.__fDescription;
                            vPath.__fTuneIndex = vAmount;
                            vPath.__fSymbolsCount_ = -1;
                            vPath.__fEnabled_ = vUnitTune.__fEdited;
                            _cBlockInputs.__mInputAdd(vPath); /// Добавление компонента
                            vPath.__fValue_ = vUnitTune.__fValue;
                            vPath.__fPathType = PATHTYPES.Folder;
                            break;
                        }
                }
            }
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Полоса инструментов
        /// </summary>
        protected crlComponentToolBar _cToolBar = new crlComponentToolBar();
        /// <summary>Кнопка 'Сохранить'
        /// </summary>
        protected crlComponentToolBarButton _cButtonSave = new crlComponentToolBarButton();
        /// <summary>Кнопка 'Помощь'
        /// </summary>
        protected crlComponentToolBarButton _cButtonHelp = new crlComponentToolBarButton();
        /// <summary>Блок для размещения полей ввода настроек
        /// </summary>
        protected crlBlockInputs _cBlockInputs = new crlBlockInputs();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
