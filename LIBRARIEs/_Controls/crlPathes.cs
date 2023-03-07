using nlDataMaster;
using System.IO;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlPathes'
    /// </summary>
    /// <remarks>Класс для работы с путями приложения работающего с интерфейсом пользователя</remarks>
    public class crlPathes : datPathes
    {
        #region = МЕТОДЫ

        /// <summary>
        /// Путь и имя файла для размещения настроек форм приложения 
        /// </summary>
        public override string __mFileFormTunes()
        {
            string vReturn = ""; // Возвращаемое значение
            if (crlApplication.__oData.__fDataSourceCurrentAlias == "")
                vReturn = Path.Combine(__fFolderTunes_, "forms.tun");
            else
                vReturn = Path.Combine(__fFolderTunes_, "forms_" + crlApplication.__oData.__mUserClue() + ".tun");

            return vReturn;
        }

        #endregion МЕТОДЫ
    }
}
