using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace WindowsClientApplication.Views {
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window {
        public const int GWL_STYLE = ( -16 ); // ウィンドウスタイル
        public const int GWL_EXSTYLE = ( -20 ); // 拡張ウィンドウスタイル

        public const int WS_SYSMENU = 0x00080000; // システムメニュを表示する
        public const int WS_EX_TRANSPARENT = 0x00000020; // 透過ウィンドウスタイル

        [DllImport( "user32" )]
        protected static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport( "user32" )]
        protected static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwLong);

        public Shell() {

            // 透過背景
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.Background = new SolidColorBrush( Colors.Transparent );

            // 全画面表示
            this.WindowState = WindowState.Maximized;

            // 最前面表示
            this.Topmost = true;

            // xamlの読み込み
            InitializeComponent();

        }

        protected override void OnSourceInitialized(EventArgs e) {

            //WindowHandle(Win32) を取得
            var handle = new WindowInteropHelper( this ).Handle;

            //システムメニュを非表示
            int windowStyle = GetWindowLong( handle, GWL_STYLE );
            windowStyle &= ~WS_SYSMENU; //フラグを消す
            SetWindowLong( handle, GWL_STYLE, windowStyle );

            //クリックをスルー
            int extendStyle = GetWindowLong( handle, GWL_EXSTYLE );
            extendStyle |= WS_EX_TRANSPARENT; //フラグの追加
            SetWindowLong( handle, GWL_EXSTYLE, extendStyle );

            base.OnSourceInitialized( e );
        }
    }
}
