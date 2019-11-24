using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChangeMachine.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChangeMachine.WpfApp.Models.ChangeMachineModel ChangeMachineModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ChangeMachineModel = new ChangeMachine.WpfApp.Models.ChangeMachineModel();
            DataContext = ChangeMachineModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if(btn.Content.ToString().Equals("Einwurf", StringComparison.CurrentCultureIgnoreCase))
                {
                    int value = Convert.ToInt32(cmbMoney.SelectedValue);
                    ChangeMachineModel.InsertValue(value);
                }
                if (btn.Content.ToString().Equals("Abbruch", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChangeMachineModel.Cancel();
                }
                if (btn.Content.ToString().Equals("Wechseln", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChangeMachineModel.Change();
                }
                if (btn.Content.ToString().Equals("Entleeren", StringComparison.CurrentCultureIgnoreCase))
                {
                    ChangeMachineModel.Eject();
                }
                if(btn.Content.ToString().Substring(0,1) == "+")
                {
                    int value = Convert.ToInt32(btn.Content.ToString());
                    ChangeMachineModel.IncreaseSelect(value);
                }
                if (btn.Content.ToString().Substring(0, 1) == "-")
                {
                    int value = Convert.ToInt32(btn.Content.ToString()) * -1;
                    ChangeMachineModel.DecreaseSelect(value);
                }

            }
        }
    }
}
