using nlApplication;
using nlTest;
using System;
using System.Collections;

namespace tstApplication
{
    /// <summary>
    /// Класс 'Program'
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>Элемент для тестирования библиотеки 'appApplication'</remarks>
    internal class Program
    {
        static tstTest fTest = new tstTest(); // Объект для проверки результатов тестирования

        static void Main(string[] args)
        {
            if (tstApplication.__oEventsHandler.__mBegin() == true)
            {
                //mTestApplication();
                mTestTypes();
            }
            tstApplication.__oEventsHandler.__mEnd();
        }
        /// <summary>
        /// Тестирование классов составляющих основу приложения
        /// </summary>
        static void mTestApplication()
        {
            bool vResult = true; // Результат выполнения теста класса
            bool vReturn = true; // Результат выполнения метода

            #region Проверка обработчика ошибок

            Console.WriteLine("Должно появиться окно с сообщением \n'Тестирование ошибки приложения.'\nПроцедура: 'mTestApplication()'");
            tstApplication.__mBuild();
            appUnitError vError = new appUnitError();
            vError.__fErrorsType = ERRORSTYPES.Application;
            vError.__fMessage_ = "Тестирование ошибки приложения";
            vError.__fProcedure = "mTestApplication()";
            tstApplication.__oErrorsHandler.__mShow(vError);

            #endregion Проверка обработчика ошибок

            vReturn = fTest.__mCompare(tstApplication.__fPrefix, "tst");
            vResult &= vReturn;
            Console.WriteLine("tstApplication.__fPrefix".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));

            Console.WriteLine("\nТест класса 'tstApplication'".PadRight(91, '.') + (vResult == true ? "OK" : "Error"));

            Console.Read();
        }
        static void mTestTypes()
        {
            bool vResult = true; // Результат выполнения теста класса
            bool vResultFull = true; // Результат выполнения теста библиотеки
            bool vReturn = true; // Результат выполнения метода
            DateTime vDateTime = new DateTime(2022, 1, 1);
            DateTime vDateTime2 = new DateTime(2022, 1, 1);
            TimeSpan vTimeSpan;
            int vInt = -1;
            string vString = "";
            string vString2 = "";
            bool vBoolean = false;
            ArrayList vArrayList = new ArrayList();

            #region appTypeDateTime

            Console.WriteLine("\nТест класса 'appTypeDateTime':");

            {
                vDateTime = new DateTime(2022, 1, 1);
                vInt = appTypeDateTime.__mDayOfWeekNumber(vDateTime);
                vReturn = fTest.__mCompare(vInt, 6);
                vResult &= vReturn;
                Console.WriteLine("__mDayOfWeekNumber(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDayOfWeekNumber(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1);
                vString = appTypeDateTime.__mDateToString(vDateTime);
                vReturn = fTest.__mCompare(vString, "01.01.2022");
                vResult &= vReturn;
                Console.WriteLine("__mDateToString(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateToString(DateTime)
            {
                vDateTime = appTypeDateTime.__mDateTimeToDate(vDateTime);
                vReturn = fTest.__mCompare(vDateTime, new DateTime(2022, 1, 1));
                vResult &= vReturn;
                Console.WriteLine("__mDateTimeToDate(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateTimeToDate(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateTimeToString(vDateTime);
                vReturn = fTest.__mCompare(vString, "01.01.2022 10:11:12");
                vResult &= vReturn;
                Console.WriteLine("__mDateTimeToString(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateTimeToString(DateTime)
            {
                vDateTime = appTypeDateTime.__mMsSqlDateEmpty();
                vReturn = fTest.__mCompare(vDateTime, new DateTime(1900, 1, 1));
                vResult &= vReturn;
                Console.WriteLine("__mMsSqlDateEmpty()".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMsSqlDateEmpty()
            {
                vString = appTypeDateTime.__mMsSqlDateEmptyToString();
                vReturn = fTest.__mCompare(vString, "19000101");
                vResult &= vReturn;
                Console.WriteLine("__mMsSqlDateEmptyToString()".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMsSqlDateEmptyToString()
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mMsSqlDateToString(vDateTime);
                vReturn = fTest.__mCompare(vString, "20220101");
                vResult &= vReturn;
                Console.WriteLine("__mMsSqlDateToString(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMsSqlDateToString(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mMsSqlDateTimeToString(vDateTime);
                vReturn = fTest.__mCompare(vString, "20220101 10:11:12");
                vResult &= vReturn;
                Console.WriteLine("__mMsSqlDateTimeToString(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMsSqlDateTimeToString(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateToFileName(vDateTime);
                vReturn = fTest.__mCompare(vString, "2022_01_01");
                vResult &= vReturn;
                Console.WriteLine("__mDateToFileName(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateToFileName(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateTimeToFileNameTillMinute(vDateTime);
                vReturn = fTest.__mCompare(vString, "20220101_1011");
                vResult &= vReturn;
                Console.WriteLine("__mDateTimeToFileNameTillMinute(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateTimeToFileNameTillMinute(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateTimeToFileNameTillSecond(vDateTime);
                vReturn = fTest.__mCompare(vString, "20220101_101112");
                vResult &= vReturn;
                Console.WriteLine("__mDateTimeToFileNameTillSecond(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateTimeToFileNameTillSecond(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateTimeToFileNameTillMillisecond(vDateTime);
                vReturn = fTest.__mCompare(vString, "20220101_101112_000");
                vResult &= vReturn;
                Console.WriteLine("__mDateTimeToFileNameTillMillisecond(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateTimeToFileNameTillMillisecond(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateToStringTillMinute(vDateTime);
                vReturn = fTest.__mCompare(vString, "01.01.2022 10:11");
                vResult &= vReturn;
                Console.WriteLine("__mDateToStringTillMinute(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateToStringTillMinute(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateToStringTillSecond(vDateTime);
                vReturn = fTest.__mCompare(vString, "01.01.2022 10:11:12");
                vResult &= vReturn;
                Console.WriteLine("__mDateToStringTillSecond(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateToStringTillSecond(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vString = appTypeDateTime.__mDateToStringTillMillisecond(vDateTime);
                vReturn = fTest.__mCompare(vString, "01.01.2022 10:11:12.000");
                vResult &= vReturn;
                Console.WriteLine("__mDateToStringTillMillisecond(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDateToStringTillMillisecond(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 12, 12, 12, 121);
                vDateTime2 = new DateTime(2022, 1, 1, 13, 13, 13, 131);
                vTimeSpan = vDateTime2 - vDateTime;
                vString = appTypeDateTime.__mIntervalToString(vTimeSpan);
                vReturn = fTest.__mCompare(vString, "1ч 1м 1с");
                vResult &= vReturn;
                Console.WriteLine("__mIntervalToString(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mIntervalToString(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mDayBegin(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 1));
                vResult &= vReturn;
                Console.WriteLine("__mDayBegin(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDayBegin(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mDayEnd(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 1, 23, 59, 59));
                vResult &= vReturn;
                Console.WriteLine("__mDayEnd(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mDayEnd(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mMonthBegin(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 1));
                vResult &= vReturn;
                Console.WriteLine("__mMonthBegin(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMonthBegin(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mMonthEnd(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 31, 23, 59, 59));
                vResult &= vReturn;
                Console.WriteLine("__mMonthEnd(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMonthEnd(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mQuarterBegin(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 1));
                vResult &= vReturn;
                Console.WriteLine("__mQuarterBegin(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mQuarterBegin(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mQuarterEnd(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 3, 31, 23, 59, 59));
                vResult &= vReturn;
                Console.WriteLine("__mQuarterEnd(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mQuarterEnd(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mWeekBegin(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2021, 12, 27));
                vResult &= vReturn;
                Console.WriteLine("__mWeekBegin(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWeekBegin(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mWeekEnd(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 2, 23, 59, 59));
                vResult &= vReturn;
                Console.WriteLine("__mWeekEnd(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWeekEnd(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mYearBegin(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 1, 1));
                vResult &= vReturn;
                Console.WriteLine("__mYearBegin(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mYearBegin(DateTime)
            {
                vDateTime = new DateTime(2022, 1, 1, 10, 11, 12);
                vDateTime2 = appTypeDateTime.__mYearEnd(vDateTime);
                vReturn = fTest.__mCompare(vDateTime2, new DateTime(2022, 12, 31, 23, 59, 59));
                vResult &= vReturn;
                Console.WriteLine("__mYearEnd(DateTime)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mYearEnd(DateTime)

            /// Результат проверки класса
            Console.WriteLine("\nТест класса 'appTypeDateTime'".PadRight(91, '.') + (vResult == true ? "OK" : "Error"));
            vResultFull &= vResult;

            #endregion appTypeDateTime

            #region appTypeInt

            Console.WriteLine("\nТест класса 'appTypeInt':");

            {
                vString = "";
                vInt = 24;
                vReturn = appTypeInt.__mInt2Binary(out vString, vInt);
                vResult &= vReturn;
                fTest.__mCompare(vString, "00011000");
                Console.WriteLine("__mInt2Binary(out int, DateTime)".PadRight(90, '.') + (vReturn == true & fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mInt2Binary
            {
                vString = "00011000";
                vInt = 0;
                vReturn = appTypeInt.__mBinary2Int(out vInt, vString);
                vResult &= vReturn;
                fTest.__mCompare(vInt, 24);
                Console.WriteLine("__mBinary2Int(out int, DateTime)".PadRight(90, '.') + (vReturn == true & fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mBinary2Int

            /// Результат проверки класса
            Console.WriteLine("\nТест класса 'appTypeInt'".PadRight(91, '.') + (vResult == true ? "OK" : "Error"));
            vResultFull &= vResult;

            #endregion appTypeInt

            #region appTypeString

            Console.WriteLine("\nТест класса 'appTypeString':");

            {
                vString = appTypeString.__mFolderCompare(@"D:\TEMP\TEMP2", @"D:\TEMP");
                vReturn = fTest.__mCompare(vString, "TEMP2");
                vResult &= vReturn;
                Console.WriteLine("__mFolderCompare(string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mFolderCompare(string, string)
            {
                vString = appTypeString.__mUrlToFile(@"file:///test.html");
                vReturn = fTest.__mCompare(vString, "test.html");
                vResult &= vReturn;
                Console.WriteLine("__mUrlToFile(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mUrlToFile(string)
            {
                vInt = appTypeString.__mSymbolAsciiCode("test");
                vReturn = fTest.__mCompare(vInt, 116);
                vResult &= vReturn;
                Console.WriteLine("__mSymbolAsciiCode(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolAsciiCode(string)
            {
                vString = appTypeString.__mSymbolChange("test", "et", "12");
                vReturn = fTest.__mCompare(vString, "21s2");
                vResult &= vReturn;
                Console.WriteLine("__mSymbolChange(string, string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolChange(string, string, string)
            {
                vString = appTypeString.__mSymbolsFromLeft("test", 2);
                vReturn = fTest.__mCompare(vString, "te");
                vResult &= vReturn;
                Console.WriteLine("__mSymbolsFromLeft(string, int)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolsFromLeft(string, int)
            {
                vString = appTypeString.__mSymbolsFromRight("test", 2);
                vReturn = fTest.__mCompare(vString, "st");
                vResult &= vReturn;
                Console.WriteLine("__mSymbolsFromRight(string, int)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolsFromRight(string, int)
            {
                vString = appTypeString.__mSymbolsLastDelete("test,", ",");
                vReturn = fTest.__mCompare(vString, "test");
                vResult &= vReturn;
                Console.WriteLine("__mSymbolsLastDelete(string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolsLastDelete(string, string)
            {
                vString = appTypeString.__mSymbolsOnlyUpperCase("tESt");
                vReturn = fTest.__mCompare(vString, "ES");
                vResult &= vReturn;

                vString = appTypeString.__mSymbolsOnlyUpperCase("test");
                vReturn = fTest.__mCompare(vString, "");
                vResult &= vReturn;

                Console.WriteLine("__mSymbolsOnlyUpperCase(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolsOnlyUpperCase(string)
            {
                vString = appTypeString.__mSymbolsDeleteNumbers("te55st");
                vReturn = fTest.__mCompare(vString, "test");
                vResult &= vReturn;
                Console.WriteLine("__mSymbolsDeleteNumbers(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolsDeleteNumbers(string)
            {
                //vString = appTypeString.__mSymbolSpecial("12");
                //vReturn = fTest.__mCompare(vString, "\u000C");
                //vResult &= vReturn;
                //Console.WriteLine("__mSymbolSpecial(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mSymbolSpecial(string)
            {
                vString = appTypeString.__mChangeStringInExpression("qwerty", "qw", "zx");
                vReturn = fTest.__mCompare(vString, "zxerty");
                vResult &= vReturn;

                vString = appTypeString.__mChangeStringInExpression("qwerty", "we", "zx");
                vReturn = fTest.__mCompare(vString, "qzxrty");
                vResult &= vReturn;

                Console.WriteLine("__mChangeStringInExpression(string, string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mChangeStringInExpression(string, string, string)
            {
                vInt = appTypeString.__mExpressionInExpression("qwerty", "er");
                vReturn = fTest.__mCompare(vInt, 2);
                vResult &= vReturn;
                Console.WriteLine("__mExpressionInExpression(string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mExpressionInExpression(string , string)
            {
                vInt = appTypeString.__mWordCount("Петров Иван Сергеевич", ' ');
                vReturn = fTest.__mCompare(vInt, 3);
                vResult &= vReturn;
                Console.WriteLine("__mWordCount(string, char)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordCount(string, char)
            {
                vInt = appTypeString.__mWordCount("Петров Иван Сергеевич", " ");
                vReturn = fTest.__mCompare(vInt, 3);
                vResult &= vReturn;
                Console.WriteLine("__mWordCount(string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordCount(string, string)
            {
                vInt = appTypeString.__mWordCountComma("Петров, Иван Сергеевич");
                vReturn = fTest.__mCompare(vInt, 2);
                vResult &= vReturn;
                Console.WriteLine("__mWordCountComma(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordCountComma(string)
            {
                vInt = appTypeString.__mWordCountDot("Петров. Иван Сергеевич");
                vReturn = fTest.__mCompare(vInt, 2);
                vResult &= vReturn;
                Console.WriteLine("__mWordCountDot(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordCountDot(string)
            {
                vInt = appTypeString.__mWordCountSpace("Петров Иван Сергеевич");
                vReturn = fTest.__mCompare(vInt, 3);
                vResult &= vReturn;
                Console.WriteLine("__mWordCountSpace(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordCountSpace(string)
            {
                vInt = appTypeString.__mWordInList("Сидоров", "Петров", "Сидоров", "Иванов");
                vReturn = fTest.__mCompare(vInt, 1);
                vResult &= vReturn;
                Console.WriteLine("__mWordInList(string, params string[])".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordInList(string, params string[])
            {
                vInt = appTypeString.__mWordInList("Сидоров", "Петров, Сидоров, Иванов", ',');
                vReturn = fTest.__mCompare(vInt, 1);
                vResult &= vReturn;
                Console.WriteLine("__mWordInList(string, string, char)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordInList(string, string, char)
            {
                vArrayList = new ArrayList();
                vArrayList.Add("Петров");
                vArrayList.Add("Сидоров");
                vArrayList.Add("Иванов");

                vBoolean = appTypeString.__mWordInArrayList("Сидоров", vArrayList);
                vReturn = fTest.__mCompare(vBoolean, true);
                vResult &= vReturn;

                vBoolean = appTypeString.__mWordInArrayList("Федоров", vArrayList);
                vReturn = fTest.__mCompare(vBoolean, false);
                vResult &= vReturn;

                Console.WriteLine("__mWordInArrayList(string, ArrayList)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordInArrayList(string, ArrayList)
            {
                vArrayList = new ArrayList();
                vArrayList.Add("Петров");
                vArrayList.Add("Сидоров");
                vArrayList.Add("Иванов");

                vInt = appTypeString.__mWordIndexInArrayList("Сидоров", vArrayList);
                vReturn = fTest.__mCompare(vInt, 1);
                vResult &= vReturn;

                vInt = appTypeString.__mWordIndexInArrayList("Федоров", vArrayList);
                vReturn = fTest.__mCompare(vInt, -1);
                vResult &= vReturn;

                Console.WriteLine("__mWordIndexInArrayList(string, ArrayList)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordIndexInArrayList(string, ArrayList)
            {
                vString = appTypeString.__mWordLast("Федоров Иван Петрович", ' ');
                vReturn = fTest.__mCompare(vString, "Петрович");
                vResult &= vReturn;

                Console.WriteLine("__mWordLast(string, char)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordLast(string, char)
            {
                vString = appTypeString.__mWordLast("Федоров Иван Петрович", " ");
                vReturn = fTest.__mCompare(vString, "Петрович");
                vResult &= vReturn;

                Console.WriteLine("__mWordLast(string, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordLast(string, string)
            {
                vString = appTypeString.__mWordNumber("Федоров Иван Петрович", 1, ' ');
                vReturn = fTest.__mCompare(vString, "Иван");
                vResult &= vReturn;

                Console.WriteLine("__mWordNumber(string, int, char)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordNumber(string, int, char)
            {
                vString = appTypeString.__mWordNumber("Федоров Иван Петрович", 1, " ");
                vReturn = fTest.__mCompare(vString, "Иван");
                vResult &= vReturn;

                Console.WriteLine("__mWordNumber(string, int, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordNumber(string, int, string)
            {
                vString = appTypeString.__mWordNumberComma("Федоров, Иван, Петрович", 1);
                vReturn = fTest.__mCompare(vString, "Иван");
                vResult &= vReturn;

                Console.WriteLine("__mWordNumberComma(string, int)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordNumberComma(string, int)
            {
                vString = appTypeString.__mWordNumberDot("Федоров. Иван. Петрович", 1);
                vReturn = fTest.__mCompare(vString, "Иван");
                vResult &= vReturn;

                Console.WriteLine("__mWordNumberDot(string, int)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordNumberDot(string, int)
            {
                vString = appTypeString.__mWordNumberSpace("Федоров Иван Петрович", 1);
                vReturn = fTest.__mCompare(vString, "Иван");
                vResult &= vReturn;

                Console.WriteLine("__mWordNumberSpace(string, int)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordNumberSpace(string, int)
            {
                vString = appTypeString.__mWordPersonal("федоров");
                vReturn = fTest.__mCompare(vString, "Федоров");
                vResult &= vReturn;

                Console.WriteLine("__mWordPersonal(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordPersonal(string)
            {
                vArrayList = appTypeString.__mWordsList("федоров, Петров, Иванов", ',');
                vReturn = fTest.__mCompare(vArrayList, new ArrayList() { "федоров, Петров, Иванов" });
                vResult &= vReturn;

                Console.WriteLine("__mWordsList(string, char)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordsList(string, char)
            {
                vString = appTypeString.__mWordReplicate(5, "word");
                vReturn = fTest.__mCompare(vString, "wordwordwordwordword");
                vResult &= vReturn;

                Console.WriteLine("__mWordReplicate(int, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            }/// __mWordReplicate(int, string)
            {
                vBoolean = appTypeString.__mWordIsInt("word");
                vReturn = fTest.__mCompare(vBoolean, false);
                vResult &= vReturn;

                vBoolean = appTypeString.__mWordIsInt("5");
                vReturn = fTest.__mCompare(vBoolean, true);
                vResult &= vReturn;

                vBoolean = appTypeString.__mWordIsInt("525");
                vReturn = fTest.__mCompare(vBoolean, true);
                vResult &= vReturn;

                Console.WriteLine("__mWordIsInt(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mWordIsInt(string)
            {
                vString = "Параметр=значение";
                vString2 = "";
                vBoolean = appTypeString.__mParameterParse(ref vString, ref vString2);

                vReturn = fTest.__mCompare(vString, "ПАРАМЕТР");
                vResult &= vReturn;

                vReturn = fTest.__mCompare(vString2, "значение");
                vResult &= vReturn;

                Console.WriteLine("__mParameterParse(ref string, ref string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mParameterParse(ref string, ref string)
            {
                vBoolean = appTypeString.__mMaskFits("word.txt", "*.*");
                vReturn = fTest.__mCompare(vBoolean, true);
                vResult &= vReturn;

                Console.WriteLine("__mMaskFits(int, string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mMaskFits(string, string)
            {
                vString = appTypeString.__mGetHash("word.txt");
                vReturn = fTest.__mCompare(vString, "C2CvZ3uPSTACUP6M5VUdOHqqJb4=");
                vResult &= vReturn;

                Console.WriteLine("__mGetHash(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mGetHash(string)
            {
                vBoolean = appTypeString.__mIsGuid(Guid.NewGuid().ToString());
                vReturn = fTest.__mCompare(vBoolean, true);
                vResult &= vReturn;

                vBoolean = appTypeString.__mIsGuid("word");
                vReturn = fTest.__mCompare(vBoolean, false);
                vResult &= vReturn;

                Console.WriteLine("__mIsGuid(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mIsGuid(string)
            {
                vString = appTypeString.__mFilterLikeTranslate("слово", "desNam", "Or");
                vReturn = fTest.__mCompare(vString, "(desNam like '%CKJDJ%'  OR desNam like '%СЛОВО%') ");
                vResult &= vReturn;

                Console.WriteLine("__mFilterLikeTranslate(string)".PadRight(90, '.') + (fTest.__mResult() == true ? "OK" : "Error"));
            } /// __mFilterLikeTranslate(string, string, string)

            /// Результат проверки класса
            Console.WriteLine("\nТест класса 'appTypeString'".PadRight(91, '.') + (vResult == true ? "OK" : "Error"));
            vResultFull &= vResult;

            #endregion appTypeString

            /// Результат проверки библиотеки
            Console.WriteLine("\nТест библиотеки 'appApplication'".PadRight(91, '.') + (vResultFull == true ? "OK" : "Error"));

            Console.WriteLine("\nЕсли в процессе проверки у какого-то метода стоит 'Error', а у класса 'OK', значит ошибка возникла при проверке значения параметра 'ref' при выводе в консоль.");

            Console.Read();
        }
    }
}
