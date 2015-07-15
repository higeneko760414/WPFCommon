using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Input;

namespace WpfApplication1
{
	public class CommonData : INotifyPropertyChanged, INotifyDataErrorInfo
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		private Dictionary<string, List<ValidationErrorInfo>> _errors =
				  new Dictionary<string, List<ValidationErrorInfo>>();

		public IEnumerable GetErrors(string propertyName)
		{
			if (!_errors.ContainsKey(propertyName))
				return null;

			return _errors[propertyName];
		}

		public bool HasErrors
		{
			get { return this._errors.Count > 0; }
		}

		protected void AddError(string propertyName, string value)
		{
			if (!_errors.ContainsKey(propertyName))
				_errors.Add(propertyName, new List<ValidationErrorInfo>());

			_errors[propertyName].Add(new ValidationErrorInfo() { ErrorCode = 1, ErrorMessage = value });

			NotifyErrorsChanged(propertyName);
		}

		protected void RemoveError(string propertyName)
		{
			if (_errors.ContainsKey(propertyName))
				_errors.Remove(propertyName);

			NotifyErrorsChanged(propertyName);
		}

		protected void NotifyErrorsChanged(string propertyName)
		{
			if (this.ErrorsChanged != null)
				this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
		}

		protected void NotifyPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void NotifyPropertyChangedAll()
		{
			Type t = typeof(WpfApplication1.MainWindowsData);
			MemberInfo[] members = t.GetProperties();
			foreach (MemberInfo m in members)
			{
				NotifyPropertyChanged(m.Name);
			}
		}

		protected void Validation(string propertyName, object value)
		{
			try
			{
				Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propertyName });
				RemoveError(propertyName);
			}
			catch(ValidationException e)
			{
				AddError(propertyName, e.Message);
				// マウスカーソルを戻す
				Mouse.OverrideCursor = null;
				throw e;
			}
		}

		public void SetValue<T>(ref T oldValue, T newValue, string propatyName)
		{
			// 変更前後の値が同じなら処理しない
			if (GetErrors(propatyName) == null)
			{
				if (oldValue == null && newValue == null) return;
				if (oldValue.Equals(newValue)) return;
			}
			// マウスカーソルを変更
			Mouse.OverrideCursor = Cursors.Wait;
			// 入力値の検証
			Validation(propatyName, newValue);
			// 検証OKの場合は値を更新
			oldValue = newValue;
			// プロパティ値の変更を通知
			NotifyPropertyChanged(propatyName);
			// マウスカーソルを戻す
			Mouse.OverrideCursor = null;
		}
	}
}
