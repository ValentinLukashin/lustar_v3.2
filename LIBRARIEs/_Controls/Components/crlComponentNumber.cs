using nlApplication;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace nlControls
{
	/// <summary>Класс 'crlComponentNumber'
	/// </summary>
	/// <remarks>Компонент для ввода числовых значений</remarks>
	public class crlComponentNumber : TextBox
	{
		#region = ДИЗАЙНЕРЫ

		public crlComponentNumber()
		{
			_mLoad();
		}

		#endregion = ДИЗАЙНЕРЫ

		#region = МЕТОДЫ

		#region = Действия

		#region - Объект

		/// <summary>
		/// Сборка объекта
		/// </summary>
		protected virtual void _mLoad()
		{
			SuspendLayout();

			#region Настройка компонента

			Font = crlApplication.__oInterface.__mFont(FONTS.Data);
			ForeColor = crlApplication.__oInterface.__mColor(COLORS.Data);
			TextAlign = HorizontalAlignment.Right;
			Text = "0";
			LostFocus += new EventHandler(mLostFocus);
			GotFocus += new EventHandler(mGotFocus);
			TextChanged += new EventHandler(mTextChanged);
			KeyDown += new KeyEventHandler(mKeyDown);
			KeyPress += new KeyPressEventHandler(mKeyPress);
			Validating += new CancelEventHandler(mValidating);

			#endregion Настройка компонента

			ResumeLayout(false);

			Type vType = this.GetType();
			Name = vType.Name;
			_fClassNameFull = vType.Namespace + "." + Name + ".";

			return;
		}
		/// <summary>Выполняется при потере фокуса
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mLostFocus(object sender, EventArgs e)
		{
			fNoChangeEvent = true;
			fInternalValue = Convert.ToDecimal(this.Text);

			if (!(fScaleOnLostFocus == 0))
			{
				this.Text = this._mFormatNumber();
			}
			else
			{
				if (this.Text.IndexOf('-') < 0)
				{
					this.Text = this._mFormatNumber();
				}
				else
				{
					if (this.Text == "-")
					{
						this.Text = "";
					}
					else
					{
						this.Text = this._mFormatNumber();
					}
				}
			}

			fNoChangeEvent = false;
		}
		/// <summary>Выполняется при получении фокуса
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mGotFocus(object sender, EventArgs e)
		{
			fNoChangeEvent = true;
			this.Text = Convert.ToString(fInternalValue);

			if (!(fScaleOnFocus == 0))
			{
				this.Text = this._mFormatNumber();
			}
			else
			{
				if (this.Text.IndexOf('-') < 0)
				{
					this.Text = this._mFormatNumber();
				}
				else
				{
					if (this.Text == "-")
					{
						this.Text = "";
					}
					else
					{
						this.Text = this._mFormatNumber();
					}
				}
			}

			fNoChangeEvent = false;
		}
		/// <summary>Выполняется при изменении текста компонента
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mTextChanged(object sender, EventArgs e)
		{
			int vSelectionStart = 0;
			bool vPositionCursorBeforeComma = false;

			if (fNoChangeEvent || (this.SelectionStart == -1))
			{
				return;
			}

			fNoChangeEvent = true;

			if (string.Empty.Equals(this.Text.Trim()))
			{
				Text = "0";
			}

			//if (Text.Substring(0, 1) == __fGroupSeperator_)
			//{
			//	Text = Text.Substring(1);
			//}

			if (!(fScaleOnFocus == 0))
			{
				if (this.SelectionStart == (this.Text.IndexOf(__fDecimalSeperator_)))
				{
					vPositionCursorBeforeComma = true;
				}
				else
				{
					vSelectionStart = this.SelectionStart;
				}
			}
			else
			{
				vSelectionStart = this.SelectionStart;
			}

			fInternalValue = Convert.ToDecimal(this.Text);
			this.__fValue_ = Convert.ToDecimal(this.Text);

			if (Focused)
			{
				if (!(fScaleOnFocus == 0))
				{
					this.Text = this._mFormatNumber();
				}
				else
				{
					if (this.Text.IndexOf('-') < 0)
					{
						this.Text = this._mFormatNumber();
					}
					else
					{
						if (this.Text.Equals('-'))
						{
							this.Text = "";
						}
						else
						{
							this.Text = this._mFormatNumber();
						}
					}
				}
			}
			else
			{
				if (!(fScaleOnLostFocus == 0))
				{
					this.Text = this._mFormatNumber();
				}
				else
				{
					if (this.Text.IndexOf('-') < 0)
					{
						this.Text = this._mFormatNumber();
					}
					else
					{
						if (this.Text.Equals('-'))
						{
							this.Text = "";
						}
						else
						{
							this.Text = this._mFormatNumber();
						}
					}
				}

			}

			if (!(fScaleOnFocus == 0))
			{
				if (vPositionCursorBeforeComma)
				{
					this.SelectionStart = (this.Text.IndexOf(__fDecimalSeperator_));
				}
				else
				{
					this.SelectionStart = vSelectionStart;
				}
			}
			else
			{
				this.SelectionStart = vSelectionStart;
			}

			if (__eInteractiveChanged != null)
				__eInteractiveChanged(this, e);

			fNoChangeEvent = false;
		}
		/// <summary>Выполняется при нажатии клавиши
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mKeyDown(object sender, KeyEventArgs e)
		{
			bool vPositionCursorJustBeforeComma = false;

			if (!(fScaleOnFocus == 0))
			{
				vPositionCursorJustBeforeComma = (this.SelectionStart == (this.Text.IndexOf(__fDecimalSeperator_)));
			}

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Delete:
					if (vPositionCursorJustBeforeComma)
					{
						this.SelectionStart = this.Text.IndexOf(__fDecimalSeperator_) + 1;
						e.Handled = true;
						break;
					}

					if (this.Text.IndexOf('-') < 0)
					{
						if (this.SelectionLength == this.Text.Length)
						{
							this.Text = "0";
							this.SelectionStart = 1;
							e.Handled = true;
							break;
						}
					}
					else
					{

						if (this.SelectionLength == this.Text.Length)
						{
							this.Text = "0";
							this.SelectionStart = 1;
							e.Handled = true;
							break;
						}

						if (this.SelectionLength > 0)
						{
							if (this.SelectedText != "-")
							{
								if (Convert.ToDouble(this.SelectedText) == System.Math.Abs(Convert.ToDouble(this.Text)))
								{
									this.Text = "0";
									this.SelectionStart = 1;
									e.Handled = true;
									break;
								}
							}
						}
					}
					break;
				default:
					break;
			}
			return;
		}
		/// <summary>Выполняется при выборе клавиши
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mKeyPress(object sender, KeyPressEventArgs e)
		{
			bool vPositionCursorBeforeComma = false; // Каретка находиться перед запятой
			bool vInputBeforeCommaValid = false;
			bool vPositionCursorJustAfterComma = false; // Каретка находиться после запятой
			int vSelectStart = 0;

			vInputBeforeCommaValid = true;

			if (e.KeyChar.Equals('-'))
			{
				if (this.__fAllowNegative_)
				{
					if (this.Text.IndexOf('-') < 0)
					{

						vSelectStart = this.SelectionStart;

						if (!(Convert.ToDecimal(this.Text) == 0))
						{
							this.Text = "-" + this.Text;

							this.SelectionStart = vSelectStart + 1;
						}
						e.Handled = true;
						return;
					}
					else
					{

						switch (this.SelectionLength)
						{

							case 0:
								vSelectStart = this.SelectionStart;

								this.Text = Convert.ToString(Convert.ToDouble(this.Text) * -1);

								this.SelectionStart = vSelectStart - 1;

								e.Handled = true;
								break;
							default:
								if (this.SelectionLength == this.TextLength)
									this.Text = "-0";
								e.Handled = true;
								break;
						}
					}
					e.Handled = true;
					return;
				}
			}

			if (e.KeyChar.Equals('+'))
			{
				if (!(this.Text.IndexOf('-') < 0))
				{
					switch (this.SelectionLength)
					{
						case 0:
							vSelectStart = this.SelectionStart;

							this.Text = Convert.ToString(Convert.ToDouble(this.Text) * -1);

							this.SelectionStart = vSelectStart - 1;

							e.Handled = true;
							break;
						default:
							if (this.TextLength == this.SelectionLength)
							{
								this.Text = "0";
								e.Handled = true;
							}
							break;
					}
				}
				e.Handled = true;
				return;
			}

			if (!(fScaleOnFocus == 0))
			{
				vPositionCursorJustAfterComma = (this.SelectionStart == this.Text.IndexOf(__fDecimalSeperator_) + 1);
			}

			if (e.KeyChar == '\b')
			{
				if (vPositionCursorJustAfterComma)
				{
					this.SelectionStart = this.Text.IndexOf(__fDecimalSeperator_);
					e.Handled = true;
				}

				if (this.SelectionLength == this.Text.Length)
				{
					this.Text = "0";
					this.SelectionStart = 1;
					e.Handled = true;

				}

				if (e.KeyChar.Equals(null))
				{
					e.Handled = true;
				}
				return;
			}

			if (!(fScaleOnFocus == 0))
			{
				vPositionCursorBeforeComma = !(this.SelectionStart >= this.Text.IndexOf(__fDecimalSeperator_) + 1);
			}

			if (e.KeyChar.ToString() == __fDecimalSeperator_ | e.KeyChar.ToString() == ".")
			{
				if (vPositionCursorBeforeComma)
				{
					this.SelectionStart = this.Text.IndexOf(__fDecimalSeperator_) + 1;
					this.SelectionLength = 0;
				}

				e.Handled = true;
				return;
			}

			if (!(fScaleOnFocus == 0))
			{
				if (this.SelectionStart == this.Text.Length)
				{
					e.Handled = true;
					return;
				}
			}

			if (!(fScaleOnFocus == 0))
			{
				if (this.Text.IndexOf('-') < 0)
				{
					vInputBeforeCommaValid = !(this.Text.Substring(0, this.Text.IndexOf(__fDecimalSeperator_)).Length >= (fPrecision - fScaleOnFocus));
				}
				else
				{
					vInputBeforeCommaValid = !(this.Text.Substring(0, this.Text.IndexOf(__fDecimalSeperator_)).Length >= (fPrecision - fScaleOnFocus + 1));
				}
			}
			else
			{
				if (this.Text.IndexOf('-') < 0)
				{
					vInputBeforeCommaValid = !((this.Text.Length) >= fPrecision);
				}
				else
				{
					vInputBeforeCommaValid = !((this.Text.Length) >= fPrecision + 1);
				}
			}

			if (!(fScaleOnFocus == 0))
			{
				if ((this.Text.Substring(0, 1) == "0") && !(this.SelectionStart == 0))
				{
					vInputBeforeCommaValid = true;
				}
				if (this.SelectionLength > 0)
				{
					vInputBeforeCommaValid = true;
				}
			}
			else
			{
				if ((this.Text.Substring(0, 1) == "0") && ((this.SelectionStart == this.Text.Length) || (this.SelectionLength == 1)))
				{
					vInputBeforeCommaValid = true;
				}
				if (this.SelectionLength > 0)
				{
					vInputBeforeCommaValid = true;
				}
			}

			if (!(fScaleOnFocus == 0))
			{
				if (vPositionCursorBeforeComma && !(vInputBeforeCommaValid))
				{
					e.Handled = true;
					return;
				}
			}
			else
			{
				if (!(vInputBeforeCommaValid))
				{
					e.Handled = true;
					return;
				}
			}
		}
		/// <summary>Выполняется при проверке введенных данных
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mValidating(object sender, CancelEventArgs e)
		{
			if ((this.Text.Equals(string.Empty) || Convert.ToDecimal(this.__fValue_).Equals(Convert.ToDecimal(0))) && !this.__fAllowZero_)
			{
				(FindForm() as crlForm).__mBaloonMessage(this, crlApplication.__oTunes.__mTranslate("Введите данные отличные от нуля"));
				e.Cancel = true;
			}
		}

		#endregion Объект

		#endregion - Поведение

		#region - Процедуры

		/// <summary>
		/// Formats a the text inf the textbox (which represents a number) according to
		/// the scale,precision and the enviroment settings.
		/// </summary>
		protected string _mFormatNumber()
		{
			StringBuilder lsb_Format = new StringBuilder();
			int li_Counter = 1;
#pragma warning disable CS0219 // Переменной "ll_Remainder" присвоено значение, но оно ни разу не использовано.
			long ll_Remainder = 0;
#pragma warning restore CS0219 // Переменной "ll_Remainder" присвоено значение, но оно ни разу не использовано.

			if (this.Focused)
			{
				while (li_Counter <= fPrecision - fScaleOnFocus)
				{
					if (li_Counter == 1)
					{
						lsb_Format.Insert(0, '0');
					}
					else
					{
						lsb_Format.Insert(0, '#');
					}

					//System.Math.DivRem(li_Counter, 3, out ll_Remainder);
					//if ((ll_Remainder == 0) && (li_Counter + 1 <= fPrecision - fScaleOnFocus))
					//{
					//	//lsb_Format.Insert(0, ',');
					//}

					li_Counter++;
				}

				li_Counter = 1;

				if (fScaleOnFocus > 0)
				{
					lsb_Format.Append(".");

					while (li_Counter <= fScaleOnFocus)
					{
						lsb_Format.Append('0');
						li_Counter++;
					}
				}
			}
			else
			{
				while (li_Counter <= fPrecision - fScaleOnLostFocus)
				{
					if (li_Counter == 1)
					{
						lsb_Format.Insert(0, '0');
					}
					else
					{
						lsb_Format.Insert(0, '#');
					}
					//System.Math.DivRem(li_Counter, 3, out ll_Remainder);
					//if ((ll_Remainder == 0) && (li_Counter + 1 <= fPrecision - fScaleOnLostFocus))
					//{
					//	// 06.04
					//	// lsb_Format.Insert(0, ',');
					//}
					li_Counter++;
				}

				li_Counter = 1;

				if (fScaleOnLostFocus > 0)
				{
					lsb_Format.Append(".");

					while (li_Counter <= fScaleOnLostFocus)
					{
						lsb_Format.Append('0');
						li_Counter++;
					}
				}
			}
			return Convert.ToDecimal(this.Text).ToString(lsb_Format.ToString());
		}

		#endregion - Процедуры

		#endregion = МЕТОДЫ

		#region = ПОЛЯ

		#region - Внутренние

		/// <summary>Максимальное количество отображаемых символов после запятой при потере фокуса
		/// </summary>
		private int fScaleOnLostFocus = 0;
		/// <summary>Вид ввода данных
		/// </summary>
		private FILLTYPES fFillType = FILLTYPES.None;
		/// <summary>Промежуточное значение
		/// </summary>
		private Decimal fInternalValue = 0;
		/// <summary>Числовое значение
		/// </summary>
		private Decimal fNumericValue = 0;
		/// <summary>Максимальное количество отображаемых символов после запятой при полученном фокусе
		/// </summary>
		private int fScaleOnFocus = 0;
		/// <summary>Максимальное количество отображаемых числовых символов
		/// </summary>
		private int fPrecision = 10;
		/// <summary>Разрешение использования отрицательных значений
		/// </summary>
		private bool fAllowNegative = true;
		/// <summary>Блокировка событий
		/// </summary>
		private bool fNoChangeEvent = false;
		/// <summary>Разрешение использования нулевого значения
		/// </summary>
		private bool fAllowZero = false;

		/// <summary>Полное имя класса
		/// </summary>
		protected string _fClassNameFull = "";

		#endregion - Внутренние

		#endregion = ПОЛЯ

		#region = СВОЙСТВА

		/// <summary>Разрешение использования отрицательных значений
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue(false)]
		[Description("Разрешение использования отрицательных значений")]
		[RefreshProperties(RefreshProperties.None)]
		public bool __fAllowNegative_
		{
			get { return fAllowNegative; }
			set { fAllowNegative = value; }
		}
		/// <summary>Разрешение использования нулевого значения
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue(false)]
		[Description("Разрешение использования нулевого значения")]
		[RefreshProperties(RefreshProperties.None)]
		public bool __fAllowZero_
		{
			get { return fAllowZero; }
			set { fAllowZero = value; }
		}
		/// <summary>Обязательность заполнения
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue(false)]
		[Description("Разрешение использования нулевого значения")]
		[RefreshProperties(RefreshProperties.None)]
		public FILLTYPES __fFillType_
		{
			get { return fFillType; }
			set
			{
				fFillType = value;
				if (fFillType == FILLTYPES.None)
				{
					fAllowZero = true;
					BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBack);
				}
				else
				{
					fAllowZero = false;
					BackColor = crlApplication.__oInterface.__mColor(COLORS.DataBackNecessarily);
				}
			}
		}
		/// <summary>Максимальное количество отображаемых числовых символов
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue(10)]
		[Description("Максимальное количество отображаемых числовых символов")]
		[RefreshProperties(RefreshProperties.None)]
		public int __fPrecision_
		{
			get { return fPrecision; }
			set
			{
				if (value < 0)
				{
					appUnitError vError = new appUnitError();
					vError.__fErrorsType = ERRORSTYPES.Programming;
					vError.__fProcedure = _fClassNameFull + "__fNumericPrecision_";
					vError.__fMessage_ = "Точность не может быть отрицательной";
					vError.__mPropertyAdd("Полученное значение: {0}", value);
					crlApplication.__oErrorsHandler.__mShow(vError);
					return;
				} /// Проверка значения - не должно быть отрицательным

				if (value < this.__fScaleOnFocus_)
				{
					this.__fScaleOnFocus_ = value;
				}

				fPrecision = value;
				if (fPrecision < 3)
					fPrecision = fPrecision + 1;

				Width = Convert.ToInt32(crlTypeFont.__mMeasureText(fPrecision, crlApplication.__oInterface.__mFont(FONTS.Data)).Width);
			}
		}
		/// <summary>Максимальное количество отображаемых символов после запятой при полученном фокусе
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue(0)]
		[Description("Максимальное количество отображаемых символов после запятой при полученном фокусе")]
		[RefreshProperties(RefreshProperties.All)]
		public int __fScaleOnFocus_
		{
			get { return fScaleOnFocus; }
			set
			{
				appUnitError vError = new appUnitError();
				vError.__fErrorsType = ERRORSTYPES.Programming;
				vError.__fProcedure = _fClassNameFull + "__fNumericScaleOnFocus_";
				vError.__mPropertyAdd("Полученное значение: {0}", value);

				if (value < 0)
				{ /// Проверка: Масштаб - отрицательное число
					vError.__fMessage_ = "Масштаб не может быть отрицательным";
					crlApplication.__oErrorsHandler.__mShow(vError);
					return;
				}

				if (value >= this.__fPrecision_)
				{ /// Проверка: Масштаб - больше точности
					vError.__fMessage_ = "Масштаб не может быть больше точности";
					crlApplication.__oErrorsHandler.__mShow(vError);
					return;
				}

				fScaleOnFocus = value;

				if (fScaleOnFocus > 0)
				{
					this.Text = "0" + __fDecimalSeperator_ + new string(Convert.ToChar("0"), fScaleOnFocus);
				}
				else
				{
					this.Text = "0";
				}

				if (fPrecision < 3)
					fPrecision = fPrecision + 1;
				Width = Convert.ToInt32(crlTypeFont.__mMeasureText(fPrecision + 1, crlApplication.__oInterface.__mFont(FONTS.Data)).Width); /// Определение ширины компонента
			}
		}
		/// <summary>Максимальное количество отображаемых символов после запятой при потере фокуса
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue(0)]
		[Description("Максимальное количество отображаемых символов после запятой при потере фокуса")]
		[RefreshProperties(RefreshProperties.All)]
		public int __fScaleOnLostFocus_
		{
			get { return fScaleOnLostFocus; }
			set
			{
				appUnitError vError = new appUnitError();
				vError.__fErrorsType = ERRORSTYPES.Programming;
				vError.__fProcedure = _fClassNameFull + "__fNumericScaleOnLostFocus_";
				vError.__mPropertyAdd("Полученное значение: {0}", value);

				//Scale cannot be negative
				if (value < 0)
				{
					vError.__fMessage_ = "Масштаб не может быть отрицательным";
					crlApplication.__oErrorsHandler.__mShow(vError);
					return;
				}

				//Scale cannot be larger than precision
				if (value >= this.__fPrecision_)
				{
					vError.__fMessage_ = "Масштаб не может быть больше точности";
					crlApplication.__oErrorsHandler.__mShow(vError);
					return;
				}

				fScaleOnLostFocus = value;
			}
		}
		/// <summary>Символ десятичного разделителя
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue("")]
		[Description("Символ десятичного разделителя")]
		[RefreshProperties(RefreshProperties.None)]
		private string __fDecimalSeperator_
		{
			get
			{
				return System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
			}
		}
		/// <summary>Символ тысячного разделителя
		/// </summary>
		[Category("Расширенные настройки")]
		[DefaultValue("")]
		[Description("Символ тысячного разделителя")]
		[RefreshProperties(RefreshProperties.None)]
		private string __fGroupSeperator_
		{
			get
			{
				return System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator;
			}
		}
		/// <summary>Текущее числовое значение отображаемое в компоненте
		/// </summary>
		[Bindable(true)]
		[Category("Расширенные настройки")]
		[DefaultValue(0)]
		[Description("Числовое значение")]
		[RefreshProperties(RefreshProperties.None)]
		public object __fValue_
		{
			get { return fNumericValue; }
			set
			{
				if (value.Equals(DBNull.Value))
				{
					if (value.Equals(0))
					{
						this.Text = Convert.ToString(0);
						fNumericValue = Convert.ToDecimal(0);
						if (__eProgrammaticChanged != null)
						{
							__eProgrammaticChanged(this, new EventArgs());
						}
						return;
					}
				}

				if (!value.Equals(fNumericValue))
				{
					this.Text = Convert.ToString(value);
					fNumericValue = Convert.ToDecimal(value);
					if (__eProgrammaticChanged != null)
					{
						__eProgrammaticChanged(this, new EventArgs());
					}
				}
			}
		}

		#endregion = СВОЙСТВА

		#region = СОБЫТИЯ

		/// <summary>Возникает при изменении данных пользователем
		/// </summary>
		public event EventHandler __eInteractiveChanged;
		/// <summary>Возникает при программном изменении значения
		/// </summary>
		public event EventHandler __eProgrammaticChanged;

        #endregion = СОБЫТИЯ
    }
}
