using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApplication1
{
	class BaseBox : TextBox
	{
		public BaseBox()
		{
			// テキストボックスの高さを固定
			this.Height = 23;
			// 折り返し表示の禁止
			this.TextWrapping = System.Windows.TextWrapping.NoWrap;
			// Enterによるフォーカス移動
			this.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// フォーカス取得時にテキスト全選択
			this.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
			// バインディングの設定
			this.Initialized += delegate
			{
				Binding oldBinding = BindingOperations.GetBinding(this, TextBox.TextProperty);
				if (oldBinding == null) return;
				BindingOperations.ClearBinding(this, TextBox.TextProperty);
				Binding newBinding = new Binding(oldBinding.Path.Path)
				{
					StringFormat = DisplayFormat.GetDisplayFormat(this),
					Mode = BindingMode.TwoWay,
					TargetNullValue = String.Empty
				};
				BindingOperations.SetBinding(this, TextBox.TextProperty, newBinding);
			};

		}
	}
}
