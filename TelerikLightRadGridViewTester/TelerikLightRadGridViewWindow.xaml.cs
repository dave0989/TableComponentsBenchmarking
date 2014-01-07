using System;
using System.Linq;

namespace TelerikLightRadGridViewTester
{
    /// <summary>
    /// Interaction logic for TelerikLightRadGridViewWindow.xaml
    /// </summary>
    public partial class TelerikLightRadGridViewWindow
    {
        public TelerikLightRadGridViewWindow()
        {
            this.InitializeComponent();
        }

        private void RadGridView_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            e.IsValid = false;
        }
    }
}