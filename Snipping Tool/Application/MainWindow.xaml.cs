using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Snipping_Tool.API;
using Snipping_Tool.API.methods;
using Snipping_Tool.Application.Helpers;

namespace Snipping_Tool.Application
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ImgurApi _imgurApi;
        private ContextMenu trayContextMenu;
        private readonly KeyboardHook _keyboardHook;
        private readonly NotifyIcon _notifyIcon;
        public MainWindow()
        {
            _keyboardHook = new KeyboardHook();
            _imgurApi = new ImgurApi("ad832da6ad2e936", "d8b53a51fd43dc3de0d1960f254276dbc7dd7cda");
            _notifyIcon = new NotifyIcon {Icon = new Icon(GetType(), "icon.ico")};
            Initialize();
            
        }

        private void Initialize()
        {
            InitializeComponent();
            SetUpWindow();
        }

        private void SetUpWindow()
        {
            trayContextMenu = new ContextMenu();
            trayContextMenu.MenuItems.Add("&New Screenshot", ContextMenuNewScreenshot);
            
            _notifyIcon.MouseClick += NotifyIconOnMouseClick;
            _notifyIcon.ContextMenu = trayContextMenu;
            UsernameLabel.Content = _imgurApi.GetUsername();
            _keyboardHook.KeyPressed += TakeFullScreenSnapsShot;
            _keyboardHook.RegisterHotKey(ModifierKeys.Control, Keys.E); 
        }

        private void ContextMenuNewScreenshot(object sender, EventArgs eventArgs)
        {
            TakeFullScreenSnapsShot();
        }

        private void TakeFullScreenSnapsShot(object sender, KeyPressedEventArgs e)
        {
            TakeFullScreenSnapsShot();
        }

        private void NotifyIconOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.Button == MouseButtons.Left)
            {
                WindowState = WindowState.Normal;
                ShowInTaskbar = true;
                _notifyIcon.Visible = false;
            }

        }

        /// <summary>
        ///     Check if the user is logged in before proceeding, and then allows the user to take a screenshot.
        /// </summary>
        private void ButtonNewFullscreenSnip_Click(object sender, RoutedEventArgs e)
        {
            TakeFullScreenSnapsShot();
        }

        private void TakeFullScreenSnapsShot()
        {
            if (!_imgurApi.IsLoggedIn()) return;
            _imgurApi.RefreshToken();
            var windowStateBefore = WindowState;
            var showInTaskbarBefore = ShowInTaskbar;
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
                ShowInTaskbar = false;
                while (ShowInTaskbar) { }
            }
            var screenShot = new ScreenShot();
            var screenBuffer = screenShot.CaptureScreen();
            var imgurUpload = new ImgurImage(screenBuffer);
            WindowState = windowStateBefore;
            ShowInTaskbar = showInTaskbarBefore;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _notifyIcon.Visible = false;
            _keyboardHook.Dispose();
            
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            
            _notifyIcon.Visible = true;
            _notifyIcon.ShowBalloonTip(3000, "Imgur for Windows is still running!", Properties.Resources.BalloonShortCut, ToolTipIcon.Info);
            if (WindowState != WindowState.Minimized) return;
            ShowInTaskbar = false;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = Properties.Resources.ImgurSnippingTool;
            _notifyIcon.BalloonTipText = Properties.Resources.BalloonShortCut;
            _notifyIcon.ShowBalloonTip(3000, "Imgur for Windows is still running!", Properties.Resources.BalloonShortCut, ToolTipIcon.Info);
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}