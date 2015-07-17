using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

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
		[AttachedPropertyBrowsableForType(typeof(ComboBoxEx))]
		[AttachedPropertyBrowsableForType(typeof(GridComboBoxColumn))]
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
			// コントロールがPreviewTextInputイベントを持っているかチェック
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			EventInfo eventInfo = sender.GetType().GetEvent("PreviewTextInput", bindingFlags);
			if (eventInfo == null) return;

			// PreviewTextInputイベントに割り当てるハンドラの情報を生成
			MethodInfo methodInfo = typeof(InputCharcter).GetMethod("textBox_OnPreviewTextInput", bindingFlags);

			// ハンドラをデリゲート化
			Delegate eventMethod = Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo);

			// ハンドラをアタッチ
			if ((RegexCheck.InputChar)e.NewValue == RegexCheck.InputChar.All)
			{
				eventInfo.RemoveEventHandler(sender, eventMethod);
			}
			else
			{
				eventInfo.AddEventHandler(sender, eventMethod);
			}
		}

		static void textBox_OnPreviewTextInput(Object sender, TextCompositionEventArgs e)
		{
			// 入力値が空白の場合は処理しない
			if (String.IsNullOrEmpty(e.TextComposition.Text)) return;

			if (RegexCheck.IsMatched(e.TextComposition.Text, GetInputCharcter((DependencyObject)sender)))
			{
				// 入力条件則に一致する場合は処理続行
				e.Handled = false;
			}
			else
			{
				// Textプロパティを取得
				PropertyInfo property = sender.GetType().GetProperty("Text");
				// Textプロパティの値を取得
				string value = (String)property.GetValue(sender);
				// Textプロパティの値を置換
				value = value.Replace(e.TextComposition.Text, String.Empty);
				// Textプロパティの値を更新
				property.SetValue(sender, value);

				// ComboBoxのText対応
				TextBox comboText = (TextBox)e.OriginalSource;
				if (comboText != sender)
				{
					comboText.Text = value;
				}
				e.Handled = true;
			}
		}
	}
}
