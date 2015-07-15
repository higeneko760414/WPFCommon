using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
	class ValidationErrorInfo
	{
		public int ErrorCode { get; set; }

		public string ErrorMessage { get; set; }

		public override string ToString()
		{
			return ErrorMessage;
		}
	}
}
