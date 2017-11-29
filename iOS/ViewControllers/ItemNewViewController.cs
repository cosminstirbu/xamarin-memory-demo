using System;

using UIKit;

namespace MemoryDemo.iOS
{
    public partial class ItemNewViewController : UIViewController
    {
        public ItemsViewModel ViewModel { get; set; }

        public ItemNewViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // The lambda above causes a memory leak

            btnSaveItem.TouchUpInside += (sender, e) => {
                var item = new Item
                {
                    Text = txtTitle.Text,
                    Description = txtDesc.Text
                };
                ViewModel.AddItemCommand.Execute(item);
                NavigationController.PopToRootViewController(true);
            };
        }

        /*
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            btnSaveItem.TouchUpInside += SaveButtonPressed;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            btnSaveItem.TouchUpInside -= SaveButtonPressed;
        }
        */

        void SaveButtonPressed(object sender, EventArgs e)
        {
            var item = new Item
            {
                Text = txtTitle.Text,
                Description = txtDesc.Text
            };
            ViewModel.AddItemCommand.Execute(item);
            NavigationController.PopToRootViewController(true);
        }
    }
}
