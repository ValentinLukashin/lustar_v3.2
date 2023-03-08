namespace nlControls
{
    /// <summary>
    /// Класс 'crlComponentGridFolder'
    /// </summary>
    /// <remarks>Элемент для просмотра древовидных данных</remarks>
    public class crlComponentGridFolder : crlComponentGrid
    {
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="pRecordClue"></param>
        /// <returns>[true] - данные загружены, иначе - [false]</returns>
        public override bool __mDataLoad(int pRecordClue)
        {
            DataSource = __oEssence.__mGrid("", "");

            return base.__mDataLoad(pRecordClue);
        }
    }
}
