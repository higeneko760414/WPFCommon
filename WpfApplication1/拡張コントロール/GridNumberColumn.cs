using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class GridNumberColumn : GridBaseColumn
	{
		const string PropertyName = "MaxLength";

		// コンストラクタ
		public GridNumberColumn()
		{
		}

		// プロパティ定義
		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly DependencyProperty MaxLengthProperty =
			DependencyProperty.Register(PropertyName, typeof(int), typeof(GridNumberColumn), new UIPropertyMetadata(0));

		// データ表示用コントロールの設定
		protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		{
			TextBlock txtBlock = (TextBlock)base.GenerateElement(cell, dataItem);

			// 垂直方向配置
			txtBlock.VerticalAlignment = VerticalAlignment.Center;
			// 水平方向配置
			txtBlock.HorizontalAlignment = HorizontalAlignment.Right;
			// 表示フォーマット
			string stringFormat = (string)this.GetValue(DisplayFormat.DisplayFormatProperty);
			base.SetStringFormat(txtBlock, stringFormat);

			return txtBlock;
		}

		// データ編集用コントロールの設定
		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
		{
			TextBox textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);

			// 最大桁数
			textBox.MaxLength = this.MaxLength;
			// 垂直方向配置
			textBox.VerticalContentAlignment = VerticalAlignment.Center;
			// 水平方向配置
			textBox.HorizontalContentAlignment = HorizontalAlignment.Right;
			// 折り返し表示の禁止
			textBox.TextWrapping = TextWrapping.NoWrap;
			// Enterによるフォーカス移動
			textBox.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// フォーカス取得時にテキスト全選択
			textBox.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
			// 数値のみ入力可能
			textBox.SetValue(InputNumber.InputNumberProperty, true);
			// IMEを強制的にOFF
			textBox.SetValue(InputMethod.IsInputMethodEnabledProperty, false);
			// 表示フォーマット
			string stringFormat = (string)this.GetValue(DisplayFormat.DisplayFormatProperty);
			base.SetStringFormat(textBox, stringFormat);

			return textBox;
		}
	}
}
