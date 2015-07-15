using System;
using System.ComponentModel.DataAnnotations;

namespace WpfApplication1
{
	public class ValidationRules
	{
		// 年齢の妥当性チェック
		public static ValidationResult ChkAge(string value, ValidationContext context)
		{
			if (string.IsNullOrEmpty(value)) return ValidationResult.Success;

			if (!NumericCheck.IsNumeric(value)) return new ValidationResult("数値を入力してください。");

			double d = Double.Parse(value);

			if (!NumericCheck.ChkRange(0, 150, d)) return new ValidationResult("0から150の範囲内で入力してください。");

			var a = (CommonData)context.ObjectInstance;

			return ValidationResult.Success;
		}
	}
}
