using nlApplication;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlMessages'
    /// </summary>
    /// <remarks>Класс для отображения сообщений пользователю визуального приложения</remarks>
    public class crlMessages : appMessages
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Перевод выражений на язык пользователя и отображение сообщения пользователю
        /// </summary>
        /// <param name="pMessageType">Вид сообщения</param>
        /// <param name="pMessageText">Текст сообщения</param>
        /// <param name="pMessageDetails">Детали сообщения</param>
        /// <param name="pProcedure">Процедура выдавшая сообщение</param>
        /// <returns>{DialogResult}</returns>
        public override DialogResult __mShow(MESSAGESTYPES pMessageType, string pMessageText, string pMessageDetails = "", bool pCheckStatus = false, string pCheckText = "", string pProcedure = "")
        {
            DialogResult vReturn = DialogResult.None; // Возвращаемое значение

            if (__fShowForUser == false) /// Запрещено отображение сообщения пользователю
                return vReturn;

            crlFormMessage vFormMessage = new crlFormMessage();
            vFormMessage._cAreaMessage.__fMessageType_ = pMessageType;
            switch (pMessageType)
            {
                case MESSAGESTYPES.Question:
                    pMessageText = pMessageText + "?";
                    break;
                default:
                    pMessageText = pMessageText + ".";
                    break;
            }
            vFormMessage._cAreaMessage.__fMessage_ = pMessageText;
            vFormMessage._cAreaMessage._fMessageDetail = pMessageDetails;
            vFormMessage._cAreaMessage.__fCheckChecked_ = pCheckStatus;
            vFormMessage._cAreaMessage.__fCheckCaption_ = pCheckText;
            if (pCheckText.Length > 0)
                vFormMessage._cAreaMessage.__fCheckVisible_ = true;
            else
                vFormMessage._cAreaMessage.__fCheckVisible_ = false;

            vFormMessage.ShowDialog();
            vReturn = vFormMessage._cAreaMessage._fResult;

            return vReturn;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ
    }
}
