using System.Windows.Controls;

namespace WpfApplication1
{
	class ComboBoxEx : ComboBox
	{
		public ComboBoxEx()
		{
			// テキストボックスの高さを固定
			this.Height = 23;
			// Enterによるフォーカス移動
			this.SetValue(EnterThenNextFocus.EnterThenNextFocusProperty, true);
			// フォーカス取得時にテキスト全選択
//			this.SetValue(SelectOnFocus.SelectOnFocusProperty, true);
			// 入力文字種の制限
			this.SetValue(InputCharcter.InputCharcterProperty, RegexCheck.InputChar.Zenkaku_All);
			this.IsEditable = true;
		}
	}
}
