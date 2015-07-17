using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class GridComboBoxColumn : DataGridComboBoxColumn
	{
		const string PropertyName = "IsEditable";

		// コンストラクタ
		public GridComboBoxColumn()
		{
		}

		// プロパティ定義
		public Boolean IsEditable
		{
			get { return (Boolean)GetValue(IsEditableProperty); }
			set { SetValue(IsEditableProperty, value); }
		}

		public static readonly DependencyProperty IsEditableProperty =
			DependencyProperty.Register(PropertyName, typeof(Boolean), typeof(GridComboBoxColumn), new UIPropertyMetadata(false));

		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly DependencyProperty MaxLengthProperty =
			DependencyProperty.Register("MaxLength", typeof(int), typeof(GridComboBoxColumn), new UIPropertyMetadata(0));

		// データ表示用コントロールの設定
		protected override FrameworkElement GenerateElement(DataGridCell cell, Object dataItem)
		{

			ComboBox txtBlock = (ComboBox)base.GenerateElement(cell, dataItem);

			//// 垂直方向配置
			txtBlock.VerticalAlignment = VerticalAlignment.Center;
			//// 表示フォーマット
			//string stringFormat = (string)this.GetValue(DisplayFormat.DisplayFormatProperty);
			//base.SetStringFormat(txtBlock, stringFormat);

			return txtBlock;
		}

		// データ編集用コントロールの設定
		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, Object dataItem)
		{
			ComboBox comboBox = (ComboBox)base.GenerateEditingElement(cell, dataItem);

			// IsEditableの設定
			comboBox.IsEditable = this.IsEditable;
			// MaxLength等の設定
			comboBox.Loaded += textBox_Loaded;
			// 高さを固定
			comboBox.Height = 20;
			// 垂直方向配置
			comboBox.VerticalAlignment = VerticalAlignment.Center;
			//// 表示フォーマット
			//string stringFormat = (string)this.GetValue(DisplayFormat.DisplayFormatProperty);
			//base.SetStringFormat(textBox, stringFormat);

			return comboBox;
		}

		void textBox_Loaded(Object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;

			if (comboBox == null) return;
			if (!comboBox.IsEditable) return;

			TextBox textBox = (TextBox)comboBox.Template.FindName("PART_EditableTextBox", comboBox);
			if (textBox != null)
			{
				// 入力可能文字数
				textBox.MaxLength = this.MaxLength;
				// 入力可能文字種
				textBox.SetValue(InputCharcter.InputCharcterProperty, InputCharcter.GetInputCharcter(this));
				//// 折り返し表示の禁止
				//textBox.TextWrapping = System.Windows.TextWrapping.NoWrap;
				//// Enterによるフォーカス移動
				//textBox.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
				//// フォーカス取得時にテキスト全選択
				//textBox.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
				//// ０埋め
				//textBox.SetValue(ZeroPadding.ZeroPaddingProperty, this.GetValue(ZeroPadding.ZeroPaddingProperty));
			}
		}
	}
}
