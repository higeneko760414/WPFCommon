using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class SelectOnFocus
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "SelectOnFocus";
		const string DescriptionValue = "フォーカス取得時にテキストを全選択します。";

		public static readonly DependencyProperty SelectOnFocusProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(bool), typeof(SelectOnFocus), new PropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static bool GetSelectOnFocus(DependencyObject obj)
		{
			return (bool)obj.GetValue(SelectOnFocusProperty);
		}

		public static void SetSelectOnFocus(DependencyObject obj, bool value)
		{
			obj.SetValue(SelectOnFocusProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if ((bool)e.NewValue)
			{
				textBox.GotFocus += textBox_GotFocus;
				textBox.PreviewMouseLeftButtonDown += textBox_PreviewMouseLeftButtonDown;
			}
			else
			{
				textBox.GotFocus -= textBox_GotFocus;
				textBox.PreviewMouseLeftButtonDown -= textBox_PreviewMouseLeftButtonDown;
			}
		}

		static void textBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if (!textBox.IsFocused)
			{
				textBox.Focus();
				e.Handled = true;
			}
		}

		static void textBox_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			textBox.Dispatcher.BeginInvoke((Action)(() => textBox.SelectAll()));
		}
	}
}
