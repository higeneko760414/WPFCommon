using System;

namespace WpfApplication1
{
	public class NumericCheck
	{
		//　値が数値かチェック
		public static Boolean IsNumeric(String value)
		{
			double d;
			if (Double.TryParse(value, out d))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		// 値の範囲チェック
		public static Boolean ChkRange(Double minValue, Double maxValue, Double targetValue)
		{
			if (minValue > maxValue) return false;

			if (targetValue < minValue) return false;

			if (targetValue > maxValue) return false;

			return true;
		}
	}
}
