using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	//============================================================================
	// Enter押下時のフォーカス移動
	//============================================================================
	public static class EnterThenNextFocus
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "EnterThenNextFocus";
		const string DescriptionValue = "Enterキー押下でフォーカス移動します。";

		public static readonly DependencyProperty EnterThenNextFocusProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(bool), typeof(EnterThenNextFocus), new PropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static bool GetEnterThenNextFocus(DependencyObject obj)
		{
			return (bool)obj.GetValue(EnterThenNextFocusProperty);
		}

		public static void SetEnterThenNextFocus(DependencyObject obj, bool value)
		{
			obj.SetValue(EnterThenNextFocusProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Control textBox = (Control)sender;
			if (textBox == null) return;

			if ((bool)e.NewValue)
			{
				textBox.PreviewKeyDown += TextBox_OnPreviewKeyDown;
			}
			else
			{
				textBox.PreviewKeyDown -= TextBox_OnPreviewKeyDown;
			}
		}

		private static void TextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			ModifierKeys modifierKeys = Keyboard.Modifiers;
			FrameworkElement element = Keyboard.FocusedElement as FrameworkElement;

			if (e.Key == Key.Enter){
				if ((modifierKeys & ModifierKeys.Alt) != ModifierKeys.None)
					return;
				if ((modifierKeys & ModifierKeys.Control) != ModifierKeys.None)
					return;
				if ((modifierKeys & ModifierKeys.Windows) != ModifierKeys.None)
					return;
				if ((modifierKeys & ModifierKeys.Shift) != ModifierKeys.None)
				{
					if (element != null)
						element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
				}
				else
				{
					if (element != null)
						element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
				}
				e.Handled = true;
			}
		}
	}
}
