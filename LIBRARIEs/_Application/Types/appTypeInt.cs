using System.Collections;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appTypeInt'
    /// </summary>
    /// <remarks>Элемент для работы с целочисленными типами. Не наследуется</remarks>
    public sealed class appTypeInt
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>
        /// Преобразование числа в двоичный код
        /// </summary>
        /// <param name="pReturn">Параметр для возвращения данных = Двоичный код полученного числа</param>
        /// <param name="pNumber">Целое число</param>
        /// <returns>[true] - методы выполнен без ошибок, иначе - [false]</returns>
        public static bool __mInt2Binary(out string pReturn, int pNumber)
        {
            pReturn = ""; // Возвращаемое значение
            int[] vIntArray = new int[1];
            vIntArray[0] = pNumber; // Полученное число
            BitArray vBitArray = new BitArray(vIntArray); // ba будет содержать в себе массив флагов

            // Вывод числа в двоичном представлении
            for (int vAmount = 0; vAmount < vBitArray.Length; vAmount++)
            {
                if (vBitArray[vAmount])
                    pReturn = "1" + pReturn;
                else
                    pReturn = "0" + pReturn;
            }

            pReturn = pReturn.Substring(24);

            return true;
        }
        /// <summary>
        /// Преобразование двоичного кода в число
        /// </summary>
        /// <param name="pResult">Параметр для размещения полученных данных = Целое число</param>
        /// <param name="pBinaryNumber">Строка - двоичный код</param>
        /// <returns>[true] - методы выполнен без ошибок, иначе - [false]</returns>
        public static bool __mBinary2Int(out int pResult, string pBinaryNumber)
        {
            pResult = new int();
            foreach (var vSymbol in pBinaryNumber)
            {
                pResult <<= 1;
                pResult += vSymbol - '0';
            }

            return true;
        }

        #endregion Процедуры

        #endregion МЕТОДЫ
    }
}
