using nlApplication;
using System;

namespace nlTest
{
    /// <summary>
    /// Класс 'tstTest'
    /// </summary>
    /// <remarks>Элемент для написания текстов</remarks>
    public class tstTest
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Сревнение результата выполнения метода с ожидаемым результатом
        /// </summary>
        /// <param name="pValueExpected">Результат выполнения метода</param>
        /// <param name="pValueActual">Ожидаемый результат</param>
        /// <returns>[true] - методы выполнен без ошибок, иначе - [false]</returns>
        public bool __mCompare(object pValueExpected, object pValueActual)
        {
            Type vTypeExpected = pValueExpected.GetType();
            Type vTypeActual = pValueActual.GetType();
            fCyclesCount++;

            if (vTypeExpected == vTypeActual)
            {
                fResult &= true;
                if (vTypeActual == typeof(System.DateTime))
                {
                    if (Convert.ToDateTime(pValueExpected) == Convert.ToDateTime(pValueActual))
                        fResult &= true;
                    else
                        fResult &= false;
                }
                if (vTypeActual == typeof(System.Int32))
                {
                    if (Convert.ToInt32(pValueExpected) == Convert.ToInt32(pValueActual))
                        fResult &= true;
                    else
                        fResult &= false;
                }
                if (vTypeActual == typeof(System.String))
                {
                    if (Convert.ToString(pValueExpected) == Convert.ToString(pValueActual))
                        fResult &= true;
                    else
                        fResult &= false;
                }
            }
            else
                fResult &= false;

            return fResult;
        }
        /// <summary>
        /// Проверка - является ли значение пустым
        /// </summary>
        /// <param name="pValueExpected"></param>
        /// <returns></returns>
        public bool __mValueIsEmpty(object pValueExpected)
        {
            Type vTypeExpected = pValueExpected.GetType();
            fCyclesCount++;
            DateTime vDateEmpty = appTypeDateTime.__mMsSqlDateEmpty();
            if (vTypeExpected == typeof(System.DateTime))
            {
                if (Convert.ToDateTime(pValueExpected) == vDateEmpty)
                    fResult &= true;
                else
                    fResult &= false;
            }
            if (vTypeExpected == typeof(System.Int32))
            {
                if (Convert.ToInt32(pValueExpected) == 0)
                    fResult &= true;
                else
                    fResult &= false;
            }
            if (vTypeExpected == typeof(System.String))
            {
                if (String.IsNullOrEmpty(pValueExpected.ToString()) == true)
                    fResult &= true;
                else
                    fResult &= false;
            }

            return true;
        }
        /// <summary>
        /// Проверка - является ли значение не пустым
        /// </summary>
        /// <param name="pValueExpected"></param>
        /// <returns></returns>
        public bool __mValueIsNotEmpty(object pValueExpected)
        {
            Type vTypeExpected = pValueExpected.GetType();
            fCyclesCount++;
            DateTime vDateEmpty = appTypeDateTime.__mMsSqlDateEmpty();
            if (vTypeExpected == typeof(System.DateTime))
            {
                if (Convert.ToDateTime(pValueExpected) == vDateEmpty)
                    fResult &= false;
                else
                    fResult &= true;
            }
            if (vTypeExpected == typeof(System.Int32))
            {
                if (Convert.ToInt32(pValueExpected) == 0)
                    fResult &= false;
                else
                    fResult &= true;
            }
            if (vTypeExpected == typeof(System.String))
            {
                if (String.IsNullOrEmpty(pValueExpected.ToString()) == true)
                    fResult &= false;
                else
                    fResult &= true;
            }

            return true;
        }
        /// <summary>
        /// Возвращает итоговый результат тестирования результатов выполнения метода и готовит класс к обработке следующего метода 
        /// </summary>
        /// <returns>Результат тестирования</returns>
        public bool __mResult()
        {
            bool vResult = fResult;
            fResult = true;
            fCyclesCount = 0;
            return vResult;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ

        #region = ПОЛЯ

        #region - Служебные

        /// <summary>
        /// Количество циклов проверки
        /// </summary>
        int fCyclesCount = 0;
        /// <summary>
        /// Результат проверки
        /// </summary>
        bool fResult = true;

        #endregion Служебные

        #endregion ПОЛЯ
    }
}
