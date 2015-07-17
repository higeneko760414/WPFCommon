using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class GridTimeColumn : GridBaseColumn
	{
		// コンストラクタ
		public GridTimeColumn()
		{
		}

		// データ表示用コントロールの設定
		protected override FrameworkElement GenerateElement(DataGridCell cell, Object dataItem)
		{
			TextBlock txtBlock = (TextBlock)base.GenerateElement(cell, dataItem);

			// 垂直方向配置
			txtBlock.VerticalAlignment = VerticalAlignment.Center;
			// 水平方向配置
			txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
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
			textBox.MaxLength = 5;
			// 垂直方向配置
			textBox.VerticalContentAlignment = VerticalAlignment.Center;
			// 水平方向配置
			textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			// IMEを強制的にOFF
			textBox.SetValue(InputMethod.IsInputMethodEnabledProperty, false);
			// 折り返し表示の禁止
			textBox.TextWrapping = TextWrapping.NoWrap;
			// Enterによるフォーカス移動
			textBox.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// フォーカス取得時にテキスト全選択
			textBox.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
			// 時刻フォーマット変換
			textBox.SetValue(TimeFormat.TimeFormatProperty, true);
			// 入力可能文字列の制限
			textBox.SetValue(InputCharcter.InputCharcterProperty, RegexCheck.InputChar.Time_Only);
			// 表示フォーマット
			string stringFormat = (String)this.GetValue(DisplayFormat.DisplayFormatProperty);
			base.SetStringFormat(textBox, stringFormat);

			return textBox;
		}	}
}
