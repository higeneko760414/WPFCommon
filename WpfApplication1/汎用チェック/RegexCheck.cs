using System;
using System.Text.RegularExpressions;

namespace WpfApplication1
{
	public class RegexCheck
	{
		public enum InputChar
		{
			// 全文字種
			All,
			// 全角カタカナのみ
			Zenkaku_Kana,
			// 半角カタカナのみ
			Hankaku_Kana,
			// 全角ひらがなのみ
			Zenkaku_Hira,
			// アルファベットのみ
			Alphabet_Only,
			// 数字のみ
			Number,
			// 全角文字のみ
			Zenkaku_All,
			// 半角文字のみ
			Hankaku_All,
			// 日付のみ
			Date_Only,
			// 時刻のみ
			Time_Only,
		}

		public static bool IsMatched(string value, InputChar regexValue)
		{
			string RegexString = String.Empty;

			switch (regexValue)
			{
				case InputChar.Hankaku_Kana:
					RegexString = @"^[ｧ-ﾝﾞﾟ]+$";
					break;
				case InputChar.Zenkaku_Hira:
					RegexString = @"^[ぁ-ん]+$";
					break;
				case InputChar.Zenkaku_Kana:
					RegexString = @"^[ァ-ン]+$";
					break;
				case InputChar.Alphabet_Only:
					RegexString = @"^[a-zA-Z]+$";
					break;
				case InputChar.Number:
					RegexString = @"^[-.0-9]+$";
					break;
				case InputChar.Zenkaku_All:
					RegexString = @"^[^ -~｡-ﾟ]*$";
					break;
				case InputChar.Hankaku_All:
					RegexString = @"^[ -~｡-ﾟ]*$";
					break;
				case InputChar.Date_Only:
					RegexString = @"^[/0-9]+$";
					break;
				case InputChar.Time_Only:
					RegexString = @"^[:0-9]+$";
					break;
			}

			if (Regex.IsMatch(value, RegexString))
			{
				// マッチした
				return true;
			}
			else
			{
				// マッチしなかった
				return false;
			}
		}
	}
}
