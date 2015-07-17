using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class ZeroPadding
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "ZeroPadding";
		const string DescriptionValue = "MaxLengthの桁数まで0埋めします。";
		const char paddingChar = '0';

		public static readonly DependencyProperty ZeroPaddingProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(Boolean), typeof(ZeroPadding), new UIPropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		[AttachedPropertyBrowsableForType(typeof(CommonBox))]
		[AttachedPropertyBrowsableForType(typeof(GridTextColumn))]
		[AttachedPropertyBrowsableForType(typeof(GridComboBoxColumn))]
		public static Boolean GetZeroPadding(DependencyObject obj)
		{
			return (Boolean)obj.GetValue(ZeroPaddingProperty);
		}

		public static void SetZeroPadding(DependencyObject obj, Boolean value)
		{
			obj.SetValue(ZeroPaddingProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(sender is TextBox)) return;

			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if ((Boolean)e.NewValue)
			{
				textBox.LostFocus += textBox_LostFocus;
			}
			else
			{
				textBox.LostFocus -= textBox_LostFocus;
			}
		}

		static void textBox_LostFocus(Object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if (String.IsNullOrEmpty(textBox.Text)) return;

			if (!GetZeroPadding(textBox)) return;

			textBox.Text = textBox.Text.PadLeft(textBox.MaxLength, paddingChar);
		}
	}
}
