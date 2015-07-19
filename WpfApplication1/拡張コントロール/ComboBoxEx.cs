using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class ComboBoxEx : ComboBox
	{
		const string PropertyName = "MaxLength";
		const string EditTextBoxName = "PART_EditableTextBox";

		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly DependencyProperty MaxLengthProperty =
			DependencyProperty.Register(PropertyName, typeof(int), typeof(ComboBoxEx), new UIPropertyMetadata(0));

		public ComboBoxEx()
		{
			// テキストボックスの高さを固定
			this.Height = 23;
			// 垂直方向配置
			this.VerticalContentAlignment = VerticalAlignment.Center;
			// Enterによるフォーカス移動
			this.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// 入力文字種の制限
			this.SetValue(InputCharcter.InputCharcterProperty, InputCharcter.GetInputCharcter(this));
			// MaxLengthの設定
			this.Loaded += ComboBoxEx_Loaded;
			// 前ゼロ埋め
			this.SetValue(ZeroPadding.ZeroPaddingProperty, false);
		}

		private void ComboBoxEx_Loaded(Object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;

			if (comboBox == null) return;
			if (!comboBox.IsEditable) return;

			TextBox textBox = (TextBox)comboBox.Template.FindName(EditTextBoxName, comboBox);

			if (textBox != null)
			{
				textBox.MaxLength = this.MaxLength;
				textBox.SetValue(InputCharcter.InputCharcterProperty, InputCharcter.GetInputCharcter(this));
			}
		}
	}
}
