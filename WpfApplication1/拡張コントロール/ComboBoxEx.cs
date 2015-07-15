using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class ComboBoxEx : ComboBox
	{
		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly DependencyProperty MaxLengthProperty =
			DependencyProperty.Register("MaxLength", typeof(int), typeof(ComboBoxEx), new UIPropertyMetadata(0));

		public ComboBoxEx()
		{
			// テキストボックスの高さを固定
			this.Height = 23;
			// Enterによるフォーカス移動
			this.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// 入力文字種の制限
			this.SetValue(InputCharcter.InputCharcterProperty, RegexCheck.InputChar.Zenkaku_All);
			// MaxLengthの設定
			this.Loaded += ComboBoxEx_Loaded;
		}

		private void ComboBoxEx_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;

			if (comboBox == null) return;
			if (!comboBox.IsEditable) return;

			TextBox textBox = (TextBox)comboBox.Template.FindName("PART_EditableTextBox", comboBox);
			if (textBox != null)
				textBox.MaxLength = this.MaxLength;
		}
	}
}
