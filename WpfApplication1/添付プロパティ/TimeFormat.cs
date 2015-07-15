using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class TimeFormat
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "TimeFormat";
		const string DescriptionValue = "時刻項目の入力値をフォーマットします。";
		const string DefaultFormat = "HH:mm";
		const string SeparateMark = ":";

		public static readonly DependencyProperty TimeFormatProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(bool), typeof(TimeFormat), new UIPropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static bool GetTimeFormat(DependencyObject obj)
		{
			return (bool)obj.GetValue(TimeFormatProperty);
		}

		public static void SetTimeFormat(DependencyObject obj, bool value)
		{
			obj.SetValue(TimeFormatProperty, value);
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
