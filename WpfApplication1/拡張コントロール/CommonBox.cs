using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class CommonBox : BaseBox
	{
		public CommonBox()
		{
			// ゼロ埋め
			this.SetValue(ZeroPadding.ZeroPaddingProperty, false);
			// 入力文字種の制限
			this.SetValue(InputCharcter.InputCharcterProperty, RegexCheck.InputChar.All);
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

			// 入力規則に一致しない場合は終了
			if (!RegexCheck.IsMatched(value, (RegexCheck.InputChar)GetValue(InputCharcter.InputCharcterProperty))) return;

			// マッチした
			this.Paste();
		}
	}
}
