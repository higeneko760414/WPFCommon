using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
	class DisplayFormat
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "DisplayFormat";
		const string DescriptionValue = "画面に表示するときのフォーマットを取得または設定します。";

		public static readonly DependencyProperty DisplayFormatProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(string), typeof(DisplayFormat));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		[AttachedPropertyBrowsableForType(typeof(TextBox))]
		[AttachedPropertyBrowsableForType(typeof(GridBaseColumn))]
		public static string GetDisplayFormat(DependencyObject obj)
		{
			return (string)obj.GetValue(DisplayFormatProperty);
		}

		public static void SetDisplayFormat(DependencyObject obj, string value)
		{
			obj.SetValue(DisplayFormatProperty, value);
		}
	}
}
