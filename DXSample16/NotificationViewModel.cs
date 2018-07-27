using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace DXSample16
{
    public class NotificationViewModel
    {
        public ImageSource Image { get; }

        public String Caption { get; }

        public String TextZeile1 { get; }

        public String TextZeile2 { get; }

        public Boolean ShowList => Items?.Count > 0;

        public IList<ButtonInfo> Items { get; }

        public Int32 Height => ShowList ? 200 : 89;             // this isn't a good solution

        public ButtonInfo SelectedItem
        {
            get => null;
            set
            {
                value?.Execute();
                CloseSelf?.Invoke();
            }
        }

        public Action CloseSelf { get; set; }

        public NotificationViewModel(String caption, String textZeile1, String textZeile2, ImageSource image, IList<ButtonInfo> buttons = null)
        {
            Image = image;
            Caption = caption;
            TextZeile1 = textZeile1;
            TextZeile2 = textZeile2;

            Items = buttons;
        }

        public class ButtonInfo
        {
            public String Caption { get; }

            public Action Execute { get; }

            public ButtonInfo(String caption, Action execute)
            {
                Caption = caption;
                Execute = execute;
            }
        }
    }
}