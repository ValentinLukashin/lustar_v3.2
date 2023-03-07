using nlApplication;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>
    /// Класс 'crlErrorsHandler'
    /// </summary>
    /// <remarks>Элемент для обработки ошибок визуального приложения</remarks>
    public class crlErrorsHandler : appErrorsHandler
    {
        /// <summary>
        /// Вывод на экран сообщения об ошибке. Для возможности замены стандартного окна VStudio
        /// </summary>
        /// <param name="pMessageType">Вид ошибки</param>
        /// <param name="pMessage">Сообщение</param>
        /// <param name="pMessageDetails">Детали сообщения</param>
        /// <param name="pProcedure">Процедура</param>
        /// <returns>Решение пользователя</returns>
        public override DialogResult __mShowMessage(MESSAGESTYPES pMessageType, string pMessage, string pMessageDetails, string pProcedure)
        {
            return crlApplication.__oMessages.__mShow(MESSAGESTYPES.Error, pMessage, pMessageDetails, pProcedure); // Возвращаемое значение
        }
    }
}
