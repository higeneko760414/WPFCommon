﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
{
	public class InputCharcter
	{
		const string CategoryName = "拡張プロパティ";
		const string PropertyName = "InputCharcter";
		const string DescriptionValue = "テキストボックスに入力可能な文字の種類を指定します。";

		public static readonly DependencyProperty InputCharcterProperty =
			DependencyProperty.RegisterAttached(PropertyName, typeof(RegexCheck.InputChar), typeof(InputCharcter), new UIPropertyMetadata(RegexCheck.InputChar.All, PropertyChanged));

		[Category(CategoryName)]
		[DisplayName(PropertyName)]
		[Description(DescriptionValue)]
		[AttachedPropertyBrowsableForType(typeof(CommonBox))]
		[AttachedPropertyBrowsableForType(typeof(GridTextColumn))]
		public static RegexCheck.InputChar GetInputCharcter(DependencyObject obj)
		{
			return (RegexCheck.InputChar)obj.GetValue(InputCharcterProperty);
		}

		public static void SetInputCharcter(DependencyObject obj, RegexCheck.InputChar value)
		{
			obj.SetValue(InputCharcterProperty, value);
		}

		private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			Type senderType = sender.GetType();
			EventInfo eventInfo = senderType.GetEvent("PreviewTextInput", bindingFlags);
			if (eventInfo == null) return;
			Type classType = typeof(InputCharcter);
			MethodInfo methodInfo = classType.GetMethod("textBox_OnPreviewTextInput", bindingFlags);

			Delegate eventMethod = Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo);

			if ((RegexCheck.InputChar)e.NewValue == RegexCheck.InputChar.All)
			{
				eventInfo.RemoveEventHandler(sender, eventMethod);
			}
			else
			{
				eventInfo.AddEventHandler(sender, eventMethod);
			}
		}

		static void textBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (String.IsNullOrEmpty(e.TextComposition.Text)) return;

			if (RegexCheck.IsMatched(e.TextComposition.Text, GetInputCharcter((DependencyObject)sender)))
			{
				e.Handled = false;
			}
			else
			{
				Type type = sender.GetType();
				PropertyInfo property = type.GetProperty("Text");
				string value = (string)property.GetValue(sender, null);
				property.SetValue(sender, value.Replace(e.TextComposition.Text, String.Empty));
				e.Handled = true;
			}
		}
	}
}
