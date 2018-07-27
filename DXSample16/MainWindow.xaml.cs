using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;

namespace DXSample16
{
    public partial class MainWindow : ThemedWindow
    {
        private NotificationService NotificationService { get; set; }

        private ImageSource Icon { get; set; }

        private Random Rng { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitNotificationService();
            Rng = new Random();
            Icon = new BitmapImage(new Uri("pack://application:,,,/DXSample16;component/dummy.png"));
        }

        private void InitNotificationService()
        {
            var appId = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false).Cast<AssemblyTitleAttribute>().First().Title;
            var notificationTemplate = Application.Current.Resources["CustomNotificationTemplate"] as DataTemplate;
            var notificationStyle = Application.Current.Resources["CustomNotificationStyle"] as Style;

            NotificationService = new NotificationService
            {
                ApplicationId = appId,
                UseWin8NotificationsIfAvailable = false,
                PredefinedNotificationTemplate = NotificationTemplate.ShortHeaderAndTwoTextFields,
                CustomNotificationPosition = NotificationPosition.BottomRight,
                CustomNotificationTemplate = notificationTemplate,
                CustomNotificationStyle = notificationStyle
            };
        }

        private void NoButtons_OnClick(Object sender, RoutedEventArgs e)
        {
            var viewModel = new NotificationViewModel("Notification!", "Additional Text", "Another line", Icon, null);
            ShowNotification(viewModel);
        }

        private void WithButtons_OnClick(Object sender, RoutedEventArgs e)
        {
            var buttons = new List<NotificationViewModel.ButtonInfo>();
            var count = Rng.Next(3, 15);
            for (var i = 0; i < count; i++)
            {
                var iCopy = i;
                buttons.Add(new NotificationViewModel.ButtonInfo($"Item {i}", () => Output.Text = $"Item {iCopy}"));
            }
            var viewModel = new NotificationViewModel("Notification!", "Additional Text", null, Icon, buttons);
            ShowNotification(viewModel);
        }

        private void ShowNotification(NotificationViewModel viewModel)
        {
            var notification = NotificationService.CreateCustomNotification(viewModel);
            viewModel.CloseSelf = notification.Hide;
            notification.ShowAsync();
        }
    }
}
