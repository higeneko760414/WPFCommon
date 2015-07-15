using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class DateFormat
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "DateFormat";
		const string DescriptionValue = "日付項目の入力値をフォーマットします。";
		const string DefaultFormat = "yyyy/MM/dd";
		const string SeparateMark = "/";

		public static readonly DependencyProperty DateFormatProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(bool), typeof(DateFormat), new UIPropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static bool GetDateFormat(DependencyObject obj)
		{
			return (bool)obj.GetValue(DateFormatProperty);
		}

		public static void SetDateFormat(DependencyObject obj, bool value)
		{
			obj.SetValue(DateFormatProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if ((bool)e.NewValue)
			{
				textBox.LostFocus += textBox_LostFocus;
			}
			else
			{
				textBox.LostFocus -= textBox_LostFocus;
			}
		}

		static void textBox_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if (String.IsNullOrEmpty(textBox.Text)) return;

			string chkValue = textBox.Text;
			string convertValue = String.Empty;

			switch (chkValue.Length)
			{
				case 8:
					convertValue = String.Concat(chkValue.Substring(0, 4), SeparateMark, chkValue.Substring(4, 2), SeparateMark, chkValue.Substring(6, 2));
					break;
				case 4:
					convertValue = String.Concat(chkValue.Substring(0, 2), SeparateMark, chkValue.Substring(2, 2));
					break;
				case 2:
					convertValue = String.Concat(chkValue.Substring(0, 1), SeparateMark, chkValue.Substring(1, 1));
					break;
			}

			// 入力値が日付として有効かチェック
			DateTime dt;
			if (DateTime.TryParse(convertValue, out dt))
			{
				string format = (string)DisplayFormat.GetDisplayFormat(textBox);
				if (!String.IsNullOrEmpty(format))
				{
					textBox.Text = dt.ToString(format);
				}
				else
				{
					textBox.Text = dt.ToString(DefaultFormat);
				}
			}
		}
	}
}
