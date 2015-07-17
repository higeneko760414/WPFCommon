using System;
using System.Linq.Expressions;

namespace WpfApplication1
{
	static class Util
	{
		public static String GetName<T>(Expression<Func<T>> e)
		{
			MemberExpression member = (MemberExpression)e.Body;
			return member.Member.Name;
		}
	}
}
