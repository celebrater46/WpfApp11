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

namespace WpfApp11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFromConfig()
        {
            var cfg = Config.ReadConfig();
            if (cfg == null)
            {
                // No Setting
                return;
            }

            nameText.Text = cfg.Person.Name;
            ageText.Text = cfg.Person.Age.ToString();
            cal.SelectedDate = cfg.Person.Birthday.ToLocalTime();
            memoText.Text = cfg.Memo;
        }

        private void SaveToConfig()
        {
            var cfg = new Config();

            cfg.Person.Name = nameText.Text;
            int age;
            if (int.TryParse(ageText.Text, out age) == false)
            {
                // when convert failed
                age = 0;
            }

            cfg.Person.Age = age;
            cfg.Person.Birthday = (DateTime)cal.SelectedDate;
            cfg.Memo = memoText.Text;
            
            // Save Config
            Config.WriteConfig(cfg);
        }

        private void WindowContentRendered(object sender, EventArgs e)
        {
            LoadFromConfig();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            SaveToConfig();
        }
    }
}