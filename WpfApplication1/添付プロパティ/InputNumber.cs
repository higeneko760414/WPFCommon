using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class InputNumber
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "InputNumber";
		const string DescriptionValue = "数値のみを入力可能にします。";

		public static readonly DependencyProperty InputNumberProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(bool), typeof(InputNumber), new UIPropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static bool GetInputNumber(DependencyObject obj)
		{
			return (bool)obj.GetValue(InputNumberProperty);
		}

		public static void SetInputNumber(DependencyObject obj, bool value)
		{
			obj.SetValue(InputNumberProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if ((bool)e.NewValue)
			{
				textBox.PreviewTextInput += textBox_OnPreviewTextInput;
			}
			else
			{
				textBox.PreviewTextInput -= textBox_OnPreviewTextInput;
			}
		}

		static void textBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if (RegexCheck.IsMatched(e.Text, RegexCheck.InputChar.Number))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}
	}
}
