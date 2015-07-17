using System;
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
			DependencyProperty.RegisterAttached(PropertyName, typeof(Boolean), typeof(EnterThenNextFocus), new PropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static Boolean GetEnterThenNextFocus(DependencyObject obj)
		{
			return (Boolean)obj.GetValue(EnterThenNextFocusProperty);
		}

		public static void SetEnterThenNextFocus(DependencyObject obj, Boolean value)
		{
			obj.SetValue(EnterThenNextFocusProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Control textBox = (Control)sender;
			if (textBox == null) return;

			if ((Boolean)e.NewValue)
			{
				textBox.PreviewKeyDown += TextBox_OnPreviewKeyDown;
			}
			else
			{
				textBox.PreviewKeyDown -= TextBox_OnPreviewKeyDown;
			}
		}

		private static void TextBox_OnPreviewKeyDown(Object sender, KeyEventArgs e)
		{
			ModifierKeys modifierKeys = Keyboard.Modifiers;
			FrameworkElement element = (FrameworkElement)Keyboard.FocusedElement;

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
