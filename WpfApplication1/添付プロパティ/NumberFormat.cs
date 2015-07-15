﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace WpfApplication1
{
	class NumberFormat
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "NumberFormat";
		const string DescriptionValue = "数値項目の入力値をフォーマットします。";

		//=====================================================================
		// 数値フォーマット
		//=====================================================================
		public static readonly DependencyProperty NumberFormatProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(bool), typeof(NumberFormat), new UIPropertyMetadata(PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		public static bool GetNumberFormat(DependencyObject obj)
		{
			return (bool)obj.GetValue(NumberFormatProperty);
		}

		public static void SetNumberFormat(DependencyObject obj, bool value)
		{
			obj.SetValue(NumberFormatProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if ((bool)e.NewValue)
			{
				textBox.LostFocus += textBox_LostFocus;
			}
			else
			{
				textBox.LostFocus -= textBox_LostFocus;
			}
		}

		static void textBox_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == null) return;

			if (String.IsNullOrEmpty(textBox.Text)) return;

			string format = DisplayFormat.GetDisplayFormat(textBox);
			if (String.IsNullOrEmpty(format)) return;

			double dblValue = 0;
			if (!Double.TryParse(textBox.Text, out dblValue)) return;

			textBox.Text = dblValue.ToString(format);
		}
	}
}
