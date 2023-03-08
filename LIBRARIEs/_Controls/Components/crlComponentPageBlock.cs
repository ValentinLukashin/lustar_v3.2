using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace nlControls
{
    /// <summary>Класс 'crlComponentPageBlock'
    /// </summary>
    /// <remarks>Компонент - блок вкладок</remarks>
    public class crlComponentPageBlock : TabControl
    {
        #region = ДИЗАЙНЕРЫ

        public crlComponentPageBlock()
        {
            _mObjectLoad();
        }

        #endregion = ДИЗАЙНЕРЫ

        #region = МЕТОДЫ

        #region - Поведение

        #region Объект

        /// <summary>Загрузка компонента
        /// </summary>
        protected virtual void _mObjectLoad()
        {
            Type vType = this.GetType();
            Name = vType.Name;
            _fClassNameFull = vType.Namespace + "." + Name + ".";

            SuspendLayout();

            #region Настройка компонента

            DrawMode = TabDrawMode.OwnerDrawFixed; // 25
            DrawItem += CrlComponentPageBlock_DrawItem; // 25
            //SelectedIndexChanged += CrlComponentPageBlock_SelectedIndexChanged;

            #endregion Настройка компонента

            ResumeLayout();
        }

        //private void CrlComponentPageBlock_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int a = 1;
        //}

        private void CrlComponentPageBlock_DrawItem(object sender, DrawItemEventArgs e) 
        {
            using (Brush vBrush = new SolidBrush(crlApplication.__oInterface.__mColor(COLORS.FormActive)))
            {
                e.Graphics.FillRectangle(vBrush, e.Bounds);
                SizeF vSizeF = e.Graphics.MeasureString(TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - vSizeF.Width) / 2, e.Bounds.Top + (e.Bounds.Height - vSizeF.Height) / 2 + 1);

                Rectangle vRectangle = e.Bounds;
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, vRectangle);
                e.DrawFocusRectangle();
            }
            if (e.Index == SelectedIndex)
            {
                Brush vBrush = new SolidBrush(Color.Bisque);
                e.Graphics.FillRectangle(vBrush, e.Bounds);
                SizeF vSizeF = e.Graphics.MeasureString(TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(TabPages[e.Index].Text, new Font(this.Font, FontStyle.Bold), Brushes.Black, e.Bounds.Left + (e.Bounds.Width - vSizeF.Width) / 2, e.Bounds.Top + (e.Bounds.Height - vSizeF.Height) / 2 + 1);

                Rectangle vRectangle = e.Bounds;
                e.Graphics.DrawRectangle(Pens.Gray, vRectangle);
                e.DrawFocusRectangle();
            }
        }

        #endregion Объект

        #endregion - Поведение

        public void SetTabHeader(TabPage page, Color color) // 25
        {
            TabColors[page] = color;
            Invalidate();
        }

        #endregion = МЕТОДЫ

        #region = ПОЛЯ

        #region - Внутренние 

        /// <summary>Полное имя класса
        /// </summary>
        protected string _fClassNameFull = "";
        /// <summary>
        /// </summary>
        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>(); // 25

        #endregion - Внутренние


        #endregion ПОЛЯ
    }
}
