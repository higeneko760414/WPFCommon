using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
{
	class TimeBox : BaseBox
	{
		public TimeBox()
		{
			// テキストボックスの幅を固定
			this.Width = 45;
			// 入力可能文字数を固定
			this.MaxLength = 5;
			// 水平方向配置
			this.HorizontalContentAlignment = HorizontalAlignment.Center;
			// IMEを強制的にOFF
			this.SetValue(InputMethod.IsInputMethodEnabledProperty, false);
			// 時刻フォーマット変換
			this.SetValue(TimeFormat.TimeFormatProperty, true);
			// 入力可能文字列の制限
			this.SetValue(InputCharcter.InputCharcterProperty, RegexCheck.InputChar.Time_Only);
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
			if (!RegexCheck.IsMatched(value, RegexCheck.InputChar.Time_Only)) return;

			// マッチした
			this.Paste();
		}
	}
}
