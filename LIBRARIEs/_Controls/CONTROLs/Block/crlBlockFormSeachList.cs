using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nlControls
{
    public class crlBlockFormSeachList : crlComponentPanel
    {
        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка компонента
        /// </summary>
        protected override void _mObjectAssembly()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            SuspendLayout();

            base._mObjectAssembly();

            #region Размещение компонентов

            Controls.Add(_cInput);
            Controls.Add(_cList);

            #endregion Размещение компонентов

            #region Настройка компонентов 

            // _cInput
            {
                _cInput.Location = new Point(0, 0);
                _cInput.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                _cInput.__fCheckVisible_ = false;
                _cInput.__fCaption_ = "Получатель";
                _cInput.__fFieldName = "lnkPctGro";
                //_clnkPctGro.__oEssence = new trdEssencePctGro("tTrade", nlData.DELETETYPES.Mark);
                //_clnkPctGro.__oFormSelect = typeof(trdFormGridProductsGroups);
                //_clnkPctGro.__fFieldsCharList.Add("desPctGro");
                //_clnkPctGro.__fFieldCode = "codPctGro";
                //_clnkPctGro.__fFormSearchCaption = crlApplication.__oTunes.__mTranslate("Поиск группы товаров");

                #region Сетка / Определение колонок

                //_clnkPctGro._mColumnAdd(crlApplication.__oTunes.__mTranslate("Ключ")
                //                            , "cluPctGro"
                //                            , true
                //                            , false
                //                            , "DataGridViewTextBoxColumn");
                //_clnkPctGro._mColumnAdd(crlApplication.__oTunes.__mTranslate("Код")
                //                           , "codPctGro"
                //                           , true
                //                           , true
                //                           , "DataGridViewTextBoxColumn");
                //_clnkPctGro._mColumnAdd(crlApplication.__oTunes.__mTranslate("Название")
                //                           , "desPctGro"
                //                           , true
                //                           , true
                //                           , "DataGridViewTextBoxColumn");

                #endregion Сетка / Определение колонок


            }
            // _cList
            {
                _cList.Location = new Point(0, _cInput.Height);
                _cInput.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            }

            #endregion Настройка компонентов

            ResumeLayout();
        }
        #endregion Объект

        #endregion - Поведение

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Компоненты

        /// <summary>Поле ввода
        /// </summary>
        protected crlInputFormSearch _cInput = new crlInputFormSearch();
        /// <summary>Список
        /// </summary>
        protected crlComponentListBox _cList = new crlComponentListBox();

        #endregion - Компоненты

        #endregion ПОЛЯ
    }
}
