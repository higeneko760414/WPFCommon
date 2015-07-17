using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class GridTextColumn : GridBaseColumn
	{
		const string PropertyName = "MaxLength";

		// コンストラクタ
		public GridTextColumn()
		{
		}

		// プロパティ定義
		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly DependencyProperty MaxLengthProperty =
			DependencyProperty.Register(PropertyName, typeof(int), typeof(GridTextColumn), new UIPropertyMetadata(0));

		// データ表示用コントロールの設定
		protected override FrameworkElement GenerateElement(DataGridCell cell, Object dataItem)
		{
			TextBlock txtBlock = (TextBlock)base.GenerateElement(cell, dataItem);

			// 垂直方向配置
			txtBlock.VerticalAlignment = VerticalAlignment.Center;
			// 表示フォーマット
			string stringFormat = (String)this.GetValue(DisplayFormat.DisplayFormatProperty);
			base.SetStringFormat(txtBlock, stringFormat);

			return txtBlock;
		}

		// データ編集用コントロールの設定
		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, Object dataItem)
		{
			TextBox textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);
			
			// 最大桁数
			textBox.MaxLength = this.MaxLength;
			// 垂直方向配置
			textBox.VerticalContentAlignment = VerticalAlignment.Center;
			// 折り返し表示の禁止
			textBox.TextWrapping = TextWrapping.NoWrap;
			// Enterによるフォーカス移動
			textBox.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// フォーカス取得時にテキスト全選択
			textBox.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
			// ０埋め
			textBox.SetValue(ZeroPadding.ZeroPaddingProperty, this.GetValue(ZeroPadding.ZeroPaddingProperty));
			// 入力文字種の制限
			textBox.SetValue(InputCharcter.InputCharcterProperty, this.GetValue(InputCharcter.InputCharcterProperty));
			// 表示フォーマット
			string stringFormat = (String)this.GetValue(DisplayFormat.DisplayFormatProperty);
			base.SetStringFormat(textBox, stringFormat);

			return textBox;
		}
	}
}
