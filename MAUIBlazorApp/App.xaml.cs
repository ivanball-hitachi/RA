using MAUIBlazorApp.Handlers;
using Microsoft.Maui.Platform;

namespace MAUIBlazorApp
{
    public partial class App : Microsoft.Maui.Controls.Application
    { 
        public App()
        {
            InitializeComponent();

            //Border less entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if __ANDROID__
                    handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                }
            });

            MainPage = new AppShell();
        }
    }
}