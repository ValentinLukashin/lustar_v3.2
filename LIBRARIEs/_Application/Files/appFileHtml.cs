using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace nlApplication
{
    /// <summary>
    /// Класс 'appFileHtml'
    /// </summary>
    /// <remarks>Класс для работы с HTML файлами</remarks>
    public class appFileHtml
    {
        #region = МЕТОДЫ

        #region - Процедуры

        /// <summary>Преобразование Url адреса в локальный адрес
        /// </summary>
        /// <param name="pAddress"></param>
        /// <returns></returns>
        public string __mAdressToString(string pAddress)
        {
            if (pAddress.StartsWith("file:///"))
                pAddress = pAddress.Substring(8);
            return pAddress.Replace('/', '\\');
        }
        /// <summary>Чтение указанного тэга
        /// </summary>
        /// <param name="vTagName"></param>
        /// <returns></returns>
        public string __mReadTag(string pFilePath, string vTagName)
        {
            //string vReturn = ""; // Возвращаемое значение
            //{
            //    // Загружем страницу
            //    //string data = _Open("http://ya.ru/");

            //    // Тег для поиска, ищем теги <a></a>
            //    //string tag = "a";
            //    string pattern = string.Format(@"\<{0}.*?\>(?<tegData>.+?)\<\/{0}\>", vTagName.Trim());
            //    // \<{0}.*?\> - открывающий тег
            //    // \<\/{0}\> - закрывающий тег
            //    // (?<tegData>.+?) - содержимое тега, записываем в группу tegData

            //    Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
            //    MatchCollection matches = regex.Matches(_FileBody_);

            //    foreach (Match matche in matches)
            //    {
            //        string _v_ = matche.Value;
            //        vReturn = matche.Groups["tegData"].Value;
            //    }

            //    ////*
            //    //Encoding utf8 = Encoding.GetEncoding("UTF-8");
            //    //Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            //    //byte[] utf8Bytes = win1251.GetBytes(vReturn);
            //    //byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

            //    //return win1251.GetString(win1251Bytes);
            //    ////*


            //    return vReturn;

            //    //Encoding win1251 = Encoding.GetEncoding(1251);
            //    //byte[] utf8Bytes = win1251.GetBytes(vReturn);
            //    //return win1251.GetString(utf8Bytes);
            //}

            WebClient wc = new WebClient();
            //string html = wc.DownloadString("<your url goes here>");
            string html = __mOpen(pFilePath);
            //Regex regex = new Regex("<span id=\"yfs_l10_\\^gsptse\">([0-9\\.,]*)");
            string pattern = string.Format(@"\<{0}.*?\>(?<tegData>.+?)\<\/{0}\>", vTagName.Trim());
            //    // \<{0}.*?\> - открывающий тег
            //    // \<\/{0}\> - закрывающий тег
            //    // (?<tegData>.+?) - содержимое тега, записываем в группу tegData

            Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);

            MatchCollection matches = regex.Matches(html);
            if (matches.Count > 0 && matches[0].Groups.Count > 0)
            {
                // group 0 is entire string, group 1 is value matched in parenthesis
                return matches[0].Groups[1].Value;
            }

            return "";
        }
        /// <summary>Открытие файла
        /// </summary>
        /// <param name="url">Адрес файла</param>
        public string __mOpen(string url)
        {
            WebClient client = new WebClient();
            using (Stream data = client.OpenRead(url))
            {
                //using (StreamReader reader = new StreamReader(data, Encoding.GetEncoding(1251)))
                using (StreamReader reader = new StreamReader(data, Encoding.Default))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public string __mGetTag(string text)
        {
            for (int i = 1; i < 100; i++)
            {
                //string expr = @"<div[^>]*?" + @"[^>]*?>((.|\s)*?(<\/div>)){" + i.ToString() + @"}";
                string expr = @"<src[^>]*?" + @"[^>]*?>((.|\s)*?(<\/src>)){" + i.ToString() + @"}";
                Regex rgx1 = new Regex(expr, RegexOptions.Compiled);
                Match mc1 = rgx1.Match(text);
                //Regex rgx2 = new Regex(@"<div[^>]*?>", RegexOptions.Compiled);
                Regex rgx2 = new Regex(@"<src[^>]*?>", RegexOptions.Compiled);
                MatchCollection mc2 = rgx2.Matches(mc1.Value);
                if ((i - mc2.Count) == 0)
                {
                    return mc1.Value;
                }
            }
            return "no result";
        }

        #endregion - Процедуры

        #endregion = МЕТОДЫ - Процедуры   

        #region = ПОЛЯ

        #region - Внутренние

        /// <summary>
        /// Содержание файла
        /// </summary>
        //private string _FileBody_ = "";

        #endregion - Внутренние

        #endregion ПОЛЯ - Внутренние
    }
}
