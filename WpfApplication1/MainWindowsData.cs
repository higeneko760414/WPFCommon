using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WpfApplication1
{
	// 一覧用データクラス
	public class Parson : CommonData
	{
		private string _shainCd = String.Empty;
		private string _shainLastName = String.Empty;
		private string _shainFirstName = String.Empty;
		private int? _shainAge = null;
		private DateTime? _shainBirthDay = null;
		private string _shainBirthTime = String.Empty;

		// 社員コード
		public string ShainCd
		{
			get { return _shainCd; }
			set { SetValue(ref _shainCd, value, Util.GetName(() => ShainCd)); }
		}

		// 社員姓
		public string ShainLastName
		{
			get { return _shainLastName; }
			set { SetValue(ref _shainLastName, value, Util.GetName(() => ShainLastName)); }
		}

		// 社員名
		public string ShainFirstName
		{
			get { return _shainFirstName; }
			set { SetValue(ref _shainFirstName, value, Util.GetName(() => ShainFirstName)); }
		}

		// 社員年齢
//		[CustomValidation(typeof(ValidationRules), "ChkAge")]
		public int? ShainAge
		{
			get { return _shainAge; }
			set { SetValue(ref _shainAge, value, Util.GetName(() => ShainAge)); }
		}

		// 社員誕生日
		public DateTime? ShainBirthDay
		{
			get { return _shainBirthDay; }
			set { SetValue(ref _shainBirthDay, value, Util.GetName(() => ShainBirthDay)); }
		}

		// 社員誕生時間
		public string ShainBirthTime
		{
			get { return _shainBirthTime; }
			set { SetValue(ref _shainBirthTime, value, Util.GetName(() => ShainBirthTime)); }
		}
	}

	// 明細用データクラス
	public class MainWindowsData : CommonData
	{
		private string _shainCd = String.Empty;
		private string _shainLastName = String.Empty;
		private string _shainFirstName = String.Empty;
		private int? _shainAge = null;
		private DateTime? _shainBirthDay = null;
		private string _shainBirthTime = String.Empty;
		private ObservableCollection<Parson> _parsons = null;
		private ObservableCollection<ComboBoxSource> _sex = null;

		public MainWindowsData()
		{
			_parsons = new ObservableCollection<Parson>();
			_sex = new ObservableCollection<ComboBoxSource>();
		}

		// 社員リスト
		public ObservableCollection<Parson> Parsons
		{
			get { return _parsons; }
			set { _parsons = value; }
		}

		// 性別
		public ObservableCollection<ComboBoxSource> Sex
		{
			get { return _sex; }
			set { _sex = value; }
		}

		// 社員コード
		public string DetailShainCd
		{
			get { return _shainCd; }
			set { SetValue(ref _shainCd, value, Util.GetName(() => DetailShainCd)); }
		}

		// 社員姓
		public string DetailShainLastName
		{
			get { return _shainLastName; }
			set { SetValue(ref _shainLastName, value, Util.GetName(() => DetailShainLastName)); }
		}

		// 社員名
		public string DetailShainFirstName
		{
			get { return _shainFirstName; }
			set { SetValue(ref _shainFirstName, value, Util.GetName(() => DetailShainFirstName)); }
		}

		// 社員年齢
		[CustomValidation(typeof(ValidationRules), "ChkAge")]
		public int? DetailShainAge
		{
			get { return _shainAge; }
			set { SetValue(ref _shainAge, value, Util.GetName(() => DetailShainAge)); }
		}

		// 社員誕生日
		public DateTime? DetailShainBirthDay
		{
			get { return _shainBirthDay; }
			set { SetValue(ref _shainBirthDay, value, Util.GetName(() => DetailShainBirthDay)); }
		}

		// 社員誕生時間
		public string DetailShainBirthTime
		{
			get { return _shainBirthTime; }
			set { SetValue(ref _shainBirthTime, value, Util.GetName(() => DetailShainBirthTime)); }
		}
	}
}
