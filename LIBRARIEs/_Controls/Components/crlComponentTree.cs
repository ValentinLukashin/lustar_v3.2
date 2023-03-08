using nlApplication;
using nlDataMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentTree'
    /// </summary>
    /// <remarks>Компонент для правки древовидных данных</remarks>
    public class crlComponentTree : TreeView
    {
        #region = ДИЗАЙНЕРЫ

        public crlComponentTree()
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

            __mImageListLoad();

            ResumeLayout(false);

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            return;
        }
        /// <summary>
        /// Выполняется при отпускании клавиши
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter
                base.OnDoubleClick(e);

            base.OnKeyUp(e);
        }
        /// <summary>
        /// Выполняется при выборе узла мышью
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                if (__eMouseClickRight != null)
                    __eMouseClickRight(this, new EventArgs());
            if (e.Button == MouseButtons.Left)
                if (__eMouseClickLeft != null)
                    __eMouseClickLeft(this, new EventArgs());
            SelectedNode = e.Node;

            base.OnNodeMouseClick(e);
        }

        #endregion Поведение

        #region - Процедуры

        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <returns>[true] - данные загружены без ошибок, иначе - [false]</returns>
        public bool __mDataLoad()
        {
            //bool vReturn = true; // Возвращаемое значение
            DataTable vDataTable; // Таблица с данными
            crlTreeNode vTreeNode; // Новый узел
            //crlUnitTreeNode vTreeNodeParent; // Родительский узел  
            appFileIni vFileIni = (FindForm() as crlForm).__oFileIni;// Объект для работы с конфигурационными файлами
            string vFormName = FindForm().Name;// Имя формы на которой расположен компонент

            if (__oEssence != null)
            {
                vDataTable = __oEssence._mTree("LCK = 0");
                Nodes.Clear(); /// Очистка дерева от узлов, вниз не перемещать
                _fNodeListOnLoad.Clear(); /// Очистка списка добавленных узлов
                {
                    SuspendLayout();

                    //appUnitTreeNode vTreeNode = new appUnitTreeNode(); // Новый узел
                    /// Сохранение состояния перед обновлением загружаемых данных
                    string vNodeSelect = vFileIni.__mValueReadWrite("0,0", vFormName, "NodeSelected"); // Уровень вложености и сортировка выбранного узла прочитанные из файла настроек форм
                    int vSelectLevel = Convert.ToInt32(appTypeString.__mWordNumberComma(vNodeSelect, 0)); // Уровень вложенности узла
                    int vSelectIndex = Convert.ToInt32(appTypeString.__mWordNumberComma(vNodeSelect, 1)); // Сортировка узла
                    List<crlTreeNode> vNodeExpanddList = new List<crlTreeNode>(); // Список развернутых узлов

                    foreach (DataRow vData_Row in vDataTable.Rows)
                    {
                        vTreeNode = new crlTreeNode();
                        vTreeNode.__fNodeFolder = true;
                        vTreeNode.__fClue = Convert.ToInt32(vData_Row["CLU"]);
                        vTreeNode.__fClueParent = Convert.ToInt32(vData_Row["lnk" + __oEssence.__fTableName]);
                        //vTreeNode._Level = Convert.ToInt32(vData_Row["TreLvl"]);
                        vTreeNode.__fSort = Convert.ToInt32(vData_Row["TreSor"]);
                        vTreeNode.Tag = vData_Row["CLU"].ToString();
                        vTreeNode.Text = vData_Row["des" + __oEssence.__fTableName].ToString().Trim();
                        if (vDataTable.Columns.Contains("dpn" + __oEssence.__fTableName) == true) // Если в таблице существует поле описания
                            vTreeNode.__fDescription = vData_Row["dpn" + __oEssence.__fTableName].ToString().Trim();
                        if (vDataTable.Columns.Contains("StrImgNam") == true) // Если в таблице существует поле статусного изображения
                            vTreeNode.ImageKey = vData_Row["StrImgNam"].ToString().Trim();
                        if (vTreeNode.__fClueParent == 0) // Узел верхнего уровня
                            __mNodeNew(vTreeNode);
                        else
                            __mNodeSupply(__mNodeGetByClueOnLoad(vTreeNode.__fClueParent), vTreeNode);

                        #region Определение выбранного узла

                        if (vTreeNode.Level == vSelectLevel & vTreeNode.Index == vSelectIndex)
                            SelectedNode = vTreeNode;

                        #endregion Определение выбранного узла

                        #region Загрузка развернутых узлов

                        string vNodeExpanded = vFileIni.__mValueReadWrite(false.ToString(), vFormName, "NodeExpanded_" + vTreeNode.Level.ToString() + "_" + vTreeNode.Index.ToString()); // Чтение состояния развернутости узла
                        try
                        {
                            if (Convert.ToBoolean(vNodeExpanded) == true)
                                vNodeExpanddList.Add(vTreeNode);
                        }
                        catch { }

                        #endregion Загрузка развернутых узлов

                        //    _NodeListOnLoad.Add(vTreeNode); /// !!! Вверх не перемещать. Добавление узла в список загруженных узлов
                        //}

                        //#endregion Загрузка узлов

                        #region Разворачивание узлов

                        foreach (crlTreeNode vNodeExpd in vNodeExpanddList)
                        {
                            vNodeExpd.Expand();
                        }

                        #endregion Разворачивание узлов

                        //_fNodeListOnLoad.Clear();
                    }

                    ResumeLayout(false);

                    return true;
                }
            }
            else
            {
                appUnitError vError = new appUnitError();
                vError.__mMessageBuild("Источник данных не определен");
                vError.__fProcedure = _fClassNameFull + "_DataLoad(string, string, int)";
                vError.__fErrorsType = ERRORSTYPES.Programming;
                appApplication.__oErrorsHandler.__mShow(vError);

                return false;
            }
        }
        /// <summary>
        /// Сохранение состояния текущего дерева
        /// </summary>
        /// <returns>[true] - данные сохранены без ошибок, иначе - [false]</returns>
        public bool __mDataSave()
        {
            bool vReturn = true;
            appFileIni vFileIni = (FindForm() as crlForm).__oFileIni; // Объект для работы с конфигурационными файлами
            string vFormName = (FindForm() as crlForm).Name; // Имя формы на которой расположен контрол
            List<crlTreeNode> vNodeList = __mNodeListFull();

            #region Сохранение состояния узлов

            foreach (crlTreeNode vNode in vNodeList) // Сохранение состояния развернутости узлов 
            {
                vFileIni.__mValueWrite(vNode.IsExpanded.ToString(), vFormName, "NodeExpanded_" + vNode.Level.ToString() + "_" + vNode.Index.ToString());
            }
            if (SelectedNode != null) // Сохранение выбранного узла
                vFileIni.__mValueWrite(SelectedNode.Level.ToString() + "," + SelectedNode.Index.ToString(), vFormName, "NodeSelected"); // Cохранение состояния фокуса узла

            #endregion Сохранение состояния узлов

            return vReturn;
        }
        /// <summary>
        /// Создание списка изображений
        /// </summary>
        public void __mImageListLoad()
        {
            ImageList vImageList = new ImageList(); // Список изображений

            /// Загрузка изображений
            {
            }

            if (vImageList.Images.Count > 0)
                ImageList = vImageList;
        }
        /// <summary>
        /// Создание нового узла
        /// </summary>
        /// <param name="pTreeNode">Объект узла</param>
        /// <returns>[true] - узел добавлен, иначе - [false]</returns>
        public crlTreeNode __mNodeNew(crlTreeNode pTreeNode)
        {
            Nodes.Add(pTreeNode);
            _fNodeListOnLoad.Add(pTreeNode);
            return pTreeNode;
        }
        /// <summary>
        /// Создание нового узла с переводом заголовка на язык пользователя
        /// </summary>
        /// <param name="pCaptionText">Заголовок</param>
        /// <param name="pTag">Содержание тэга</param>
        /// <returns>[true] - Узел добавлен, иначе - [false]</returns>
        public crlTreeNode __mNodeNew(string pCaptionText, string pTag)
        {
            return __mNodeNew(pCaptionText, pTag, -1, -1, crlApplication.__oInterface.__mFont(FONTS.NodeNotEdit), crlApplication.__oInterface.__mColor(COLORS.Text));
        }
        /// <summary>
        /// Создание нового узла с переводом заголовка на язык пользователя
        /// </summary>
        /// <param name="pCaptionText">Заголовок</param>
        /// <param name="pTag">Содержание тэга</param>
        /// <param name="pImageIndexNormal">Индекс нормального изображения</param>
        /// <param name="pImageIndexSelected">Индекс изображения выбранного узла</param>
        /// <param name="pFont">Шрифт</param>
        /// <param name="pColor">Цвет</param>
        /// <returns>[true] - Узел добавлен, иначе - [false]</returns>
        public crlTreeNode __mNodeNew(string pCaptionText, string pTag, int pImageIndexNormal, int pImageIndexSelected, Font pFont, Color pColor)
        {
            crlTreeNode vTreeNode = new crlTreeNode(); // Создаваемый узел

            vTreeNode.Name = "Nod" + Nodes.Count + 1;
            vTreeNode.Text = crlApplication.__oTunes.__mTranslate(pCaptionText) + "  ";
            vTreeNode.Tag = pTag;
            if (pImageIndexNormal >= 0)
                vTreeNode.ImageIndex = pImageIndexNormal;
            if (pImageIndexSelected >= 0)
                vTreeNode.SelectedImageIndex = pImageIndexNormal;
            vTreeNode.NodeFont = pFont;
            vTreeNode.ForeColor = pColor;

            __mNodeNew(vTreeNode);

            return vTreeNode;
        }
        /// <summary>
        /// Добавление вложенного узла в указанный узел
        /// </summary>
        /// <param name="pTreeNodeParent">Родительский узел</param>
        /// <param name="pTreeNode">Добавляемый узел</param>
        /// <returns>[true] - узел добавлен, иначе - [false]</returns>
        public crlTreeNode __mNodeSupply(crlTreeNode pTreeNodeParent, crlTreeNode pTreeNode)
        {
            pTreeNodeParent.Nodes.Add(pTreeNode);
            _fNodeListOnLoad.Add(pTreeNode);
            return pTreeNode;
        }
        /// <summary>
        /// Создание вложенного узла с переводом заголовка на язык пользователя
        /// </summary>
        /// <param name="pCaptionText">Заголовок</param>
        /// <param name="pTag">Содержание тэга</param>
        /// <returns>[true] - Узел добавлен, иначе - [false]</returns>
        public crlTreeNode __mNodeSupply(crlTreeNode pTreeNodeParent, string pCaptionText, string pTag)
        {
            return __mNodeSupply(pTreeNodeParent, pCaptionText, pTag, -1, -1, crlApplication.__oInterface.__mFont(FONTS.NodeNotEdit), crlApplication.__oInterface.__mColor(COLORS.Text));
        }
        /// <summary>
        /// Создание вложенного узла с переводом заголовка на язык пользователя
        /// </summary>
        /// <param name="pTreeNodeParent">Родительский узел</param>
        /// <param name="pCaptionText">Заголовок</param>
        /// <param name="pTag">Содержание тэга</param>
        /// <param name="pImageIndexNormal">Индекс нормального изображения</param>
        /// <param name="pImageIndexSelected">Индекс изображения выбранного узла</param>
        /// <param name="pFont">Шрифт</param>
        /// <param name="pColor">Цвет</param>
        /// <returns>[true] - Узел добавлен, иначе - [false]</returns>
        public crlTreeNode __mNodeSupply(crlTreeNode pTreeNodeParent, string pCaptionText, string pTag, int pImageIndexNormal, int pImageIndexSelected, Font pFont, Color pColor)
        {
            crlTreeNode vTreeNode = new crlTreeNode(); // Создаваемый узел

            vTreeNode.Name = "Nod" + Nodes.Count + 1;
            vTreeNode.Text = crlApplication.__oTunes.__mTranslate(pCaptionText) + "  ";
            vTreeNode.Tag = pTag;
            if (pImageIndexNormal >= 0)
                vTreeNode.ImageIndex = pImageIndexNormal;
            if (pImageIndexSelected >= 0)
                vTreeNode.SelectedImageIndex = pImageIndexNormal;
            vTreeNode.NodeFont = pFont;
            vTreeNode.ForeColor = pColor;

            __mNodeSupply(pTreeNodeParent, vTreeNode);

            return vTreeNode;
        }
        /// <summary>
        /// Получение узла дерева по идентификатору записи
        /// </summary>
        /// <param name="pClue">Идентификатор записи</param>
        /// <returns></returns>
        public crlTreeNode __mNodeGetByClueOnLoad(int pClue)
        {
            crlTreeNode vReturn = null; // Возвращаемое значение

            foreach (crlTreeNode vTreeNode in this._fNodeListOnLoad)
            {
                if (vTreeNode.__fNodeService != true)
                {
                    if (vTreeNode.__fClue == pClue)
                    {
                        vReturn = vTreeNode as crlTreeNode;
                        break;
                    }
                }
            }

            return vReturn;
        }
        /// <summary>
        /// Получение списка узлов дерева, включая вложенные
        /// </summary>
        /// <param name="prNode">Узел дерева</param>
        /// <returns></returns>
        public List<crlTreeNode> __mNodeListFull()
        {
            fNodeChildList.Clear();
            TreeNodeCollection vNodeClct = this.Nodes;
            foreach (crlTreeNode vNode in vNodeClct)
            {
                fNodeChildList.Add(vNode);
                //_NodeList(vNode, false); // Чтение вложенных узлов
            }
            return fNodeChildList;
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region = Внутренние 

        /// <summary>
        /// Список узлов прочитанный с дерева при выполнении методов [ _NodeFullList ]
        /// </summary>
        /// <remarks>Переменная класса, т.к. нужен одновременный доступ из методов [ _NodeFullList ] и [ _NodeList ], которвые работают рекурсивно</remarks>
        private List<crlTreeNode> fNodeChildList = new List<crlTreeNode>();
        /// <summary>
        /// Список загружаемых узлов в компоненте во время загрузки данных из источника данных, для поиска родительских узлов
        /// Очищается после загрузки данных из источника данных
        /// </summary>
        /// <remarks></remarks>
        protected List<crlTreeNode> _fNodeListOnLoad = new List<crlTreeNode>();
        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion - Внутренние

        #region = Объекты

        /// <summary>
        /// Сушность данных
        /// </summary>
        public datUnitEssence __oEssence;

        #endregion Объекты

        #endregion = ПОЛЯ

        #region = СВОЙСТВА 

        public int __fRecordClue_
        {
            get
            {
                if (SelectedNode != null)
                    return (SelectedNode as crlTreeNode).__fClue;
                else
                    return -1;
            }
        }

        #endregion СВОЙСТВА

        #region = СОБЫТИЯ

        /// <summary>
        /// Возникает при клике левой кнопки мыши по компоненту
        /// </summary>
        public event EventHandler __eMouseClickLeft;
        /// <summary>
        /// Возникает при клике правой кнопки мыши по компоненту
        /// </summary>
        public event EventHandler __eMouseClickRight;

        #endregion СОБЫТИЯ    
    }
    /// <summary>
    /// Класс 'crlTreeNode'
    /// </summary>
    /// <remarks>Компонент - Узел дерева</remarks>
    public class crlTreeNode : TreeNode
    {
        #region # КОНСТРУКТОРЫ

        public crlTreeNode()
        {
            _mObjectAssembly();
        }

        #endregion КОНСТРУКТОРЫ

        #region = МЕТОДЫ

        #region - Поведение

        /// <summary>
        /// Сборка объекта
        /// </summary>
        protected virtual void _mObjectAssembly()
        {
            // Отсутствует SuspendLayout

            #region /// Настройка компонента

            NodeFont = crlApplication.__oInterface.__mFont(FONTS.NodeNotEdit);
            ForeColor = crlApplication.__oInterface.__mColor(COLORS.Text);
            
            #endregion Настройка компонента

            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + vType.Name + ".";

            return;
        }

        #endregion Поведение

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region = Атрибуты

        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int __fClue = 0;
        /// <summary>
        /// Идентификатор родительской записи
        /// </summary>
        public int __fClueParent = 0;
        /// <summary>
        /// Описание
        /// </summary>
        public string __fDescription = "";
        /// <summary>
        /// Узел является папкой, иначе - значением
        /// </summary>
        public bool __fNodeFolder = false;
        /// <summary>
        /// Название формы для открытия узла при исползовании его в качестве меню
        /// </summary>
        public string __fFormCall = "";
        /// <summary>
        /// Служебная папка
        /// </summary>
        public bool __fNodeService = false;
        /// <summary>
        /// Сортировка
        /// </summary>
        public int __fSort = 0;

        #endregion Атрибуты

        #region - Внутренние

        /// <summary>
        /// Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";

        #endregion Внутренние

        #region - Служебные

        /// <summary>
        /// Текст заголовка узла
        /// </summary>
        private string fCaption = "";

        #endregion Служебные

        #endregion ПОЛЯ

        #region = СВОЙСТВА

        /// <summary>
        /// Текст заголовка узла
        /// </summary>
        public string __fCaption_
        {
            get { return fCaption; }
            set
            {
                fCaption = value;
                Text = crlApplication.__oTunes.__mTranslate(fCaption);
            }
        }

        #endregion СВОЙСТВА
    }
}
