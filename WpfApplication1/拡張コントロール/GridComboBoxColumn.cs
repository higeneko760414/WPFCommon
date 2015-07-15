using System.Windows;
using System.Windows.Controls;
using System;
using System.Reflection;

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
		public bool IsEditable
		{
			get { return (bool)GetValue(IsEditableProperty); }
			set { SetValue(IsEditableProperty, value); }
		}

		public static readonly DependencyProperty IsEditableProperty =
			DependencyProperty.Register(PropertyName, typeof(bool), typeof(GridComboBoxColumn), new UIPropertyMetadata(false));

		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly DependencyProperty MaxLengthProperty =
			DependencyProperty.Register("MaxLength", typeof(int), typeof(GridComboBoxColumn), new UIPropertyMetadata(0));

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

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			Type senderType = textBox.GetType();

			// IsEditableの設定
			PropertyInfo propertyInfo = senderType.GetProperty(PropertyName, bindingFlags);
			propertyInfo.SetValue(textBox, this.IsEditable);

			// IsEditableの設定
			//var textBox2 = textBox.FindChild(typeof(TextBox), "PART_EditableTextBox");
			//textBox.Pre
			//propertyInfo = senderType.GetProperty("MaxLength", bindingFlags);
			//PropertyInfo[] sss = senderType.GetProperties(bindingFlags);
			//propertyInfo.SetValue(textBox, this.MaxLength);

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
