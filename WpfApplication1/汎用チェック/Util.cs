using System;
using System.Linq.Expressions;

namespace WpfApplication1
{
	static class Util
	{
		public static string GetName<T>(Expression<Func<T>> e)
		{
			var member = (MemberExpression)e.Body;
			return member.Member.Name;
		}
	}
}
