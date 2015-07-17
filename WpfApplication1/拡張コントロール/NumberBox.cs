using System;
using System.Windows.Input;
using System.Windows;

namespace WpfApplication1
{
	class NumberBox : BaseBox
	{
		public NumberBox()
		{
			// 右詰
			this.TextAlignment = TextAlignment.Right;
			// 数値のみ入力可能
			this.SetValue(InputCharcter.InputCharcterProperty, RegexCheck.InputChar.Number);
			// IMEを強制的にOFF
			this.SetValue(InputMethod.IsInputMethodEnabledProperty, false);
			// 表示フォーマット
			this.SetValue(NumberFormat.NumberFormatProperty, true);
			// クリップボード経由の貼り付けをチェック
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, textBox_OnPaste));
		}

		private void textBox_OnPaste(Object sender, ExecutedRoutedEventArgs e)
		{
			// クリップボード内にテキストがなければ処理終了
			if (!Clipboard.ContainsText()) return;

			// クリップボードの内容を取得
			string value = Clipboard.GetText();

			// 取得した文字列がからの場合は処理終了
			if (String.IsNullOrEmpty(value)) return;

			// 数値以外は貼付不可
			if (!RegexCheck.IsMatched(value, RegexCheck.InputChar.Number)) return;

			// 貼り付け
			this.Paste();
		}
	}
}
