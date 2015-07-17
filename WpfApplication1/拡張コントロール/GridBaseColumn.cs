using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApplication1
{
	class GridBaseColumn : DataGridTextColumn
	{
		// コンストラクタ
		public GridBaseColumn()
		{
		}

		// StringFormatのバインディング設定
		public void SetStringFormat(DependencyObject sender, String format)
		{
			if (sender == null) return;

			if (sender is TextBox)
			{
				Binding oldBinding = BindingOperations.GetBinding(sender, TextBox.TextProperty);
				BindingOperations.ClearBinding(sender, TextBox.TextProperty);
				Binding newBinding = new Binding(oldBinding.Path.Path)
				{
					StringFormat = format,
					Mode = BindingMode.TwoWay,
					TargetNullValue = ""
				};
				BindingOperations.SetBinding(sender, TextBox.TextProperty, newBinding);
			}
			else
			{
				Binding oldBinding = BindingOperations.GetBinding(sender, TextBlock.TextProperty);
				BindingOperations.ClearBinding(sender, TextBlock.TextProperty);
				Binding newBinding = new Binding(oldBinding.Path.Path)
				{
					StringFormat = format,
					Mode = BindingMode.TwoWay,
					TargetNullValue = ""
				};
				BindingOperations.SetBinding(sender, TextBlock.TextProperty, newBinding);
			}
		}
	}
}
