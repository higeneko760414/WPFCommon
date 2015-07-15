using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApplication1
{
	class GridComboBoxColumn : DataGridComboBoxColumn
	{
		// コンストラクタ
		public GridComboBoxColumn()
		{
		}

		// データ表示用コントロールの設定
		protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		{

			var txtBlock = base.GenerateElement(cell, dataItem);

			//// 垂直方向配置
			txtBlock.VerticalAlignment = VerticalAlignment.Center;
			//// 表示フォーマット
			//string stringFormat = (string)this.GetValue(DisplayFormat.DisplayFormatProperty);
			//base.SetStringFormat(txtBlock, stringFormat);

			return txtBlock;
		}

		// データ編集用コントロールの設定
		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
		{
			var textBox = base.GenerateEditingElement(cell, dataItem);

			textBox.Height = 20;
			//// 最大桁数
			//textBox.MaxLength = this.MaxLength;
			// 垂直方向配置
			textBox.VerticalAlignment = VerticalAlignment.Center;
			//// 折り返し表示の禁止
			//textBox.TextWrapping = System.Windows.TextWrapping.NoWrap;
			//// Enterによるフォーカス移動
			//textBox.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			//// フォーカス取得時にテキスト全選択
			//textBox.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
			//// ０埋め
			//textBox.SetValue(ZeroPadding.ZeroPaddingProperty, this.GetValue(ZeroPadding.ZeroPaddingProperty));
			//// 入力文字種の制限
			//textBox.SetValue(InputCharcter.InputCharcterProperty, this.GetValue(InputCharcter.InputCharcterProperty));
			//// 表示フォーマット
			//string stringFormat = (string)this.GetValue(DisplayFormat.DisplayFormatProperty);
			//base.SetStringFormat(textBox, stringFormat);

			return textBox;
		}
	}
}
