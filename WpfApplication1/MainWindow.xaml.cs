using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApplication1
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			MainWindowsData bindData = new MainWindowsData();
			this.DataContext = bindData;

			btnClose.Click += new RoutedEventHandler(Onb2Click);
			btnSet.Click += new RoutedEventHandler(Onb3Click);

			bindData.Parsons.Add(new Parson { ShainCd = "000001", ShainFirstName = "佐藤", ShainLastName = "太郎", ShainAge = 22, ShainBirthDay = new DateTime(1976,04,14), ShainBirthTime="23:54" });
			bindData.Parsons.Add(new Parson { ShainCd = "000002", ShainFirstName = "山田", ShainLastName = "次郎", ShainAge = 24, ShainBirthDay = new DateTime(2000,07,07), ShainBirthTime = "03:07" });

			bindData.Sex.Add(new ComboBoxSource { Value = "1", DisplayValue = "男" });
			bindData.Sex.Add(new ComboBoxSource { Value = "2", DisplayValue = "女" });

		}

		void Onb2Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		void Onb3Click(object sender, RoutedEventArgs e)
		{
			//((MainWindowsData)this.DataContext).text1 = "サンプルです";
			//((MainWindowsData)this.DataContext).text2 = "サンプルです２";
			//((MainWindowsData)this.DataContext).Age = "123456";
		}
	}
}
