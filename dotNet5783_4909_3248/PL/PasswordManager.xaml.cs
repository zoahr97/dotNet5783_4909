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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for PasswordManager.xaml
    /// </summary>
    public partial class PasswordManager : Window
    {
        public PasswordManager()
        {
            InitializeComponent();
        }

        private void Managerbutton_Click(object sender, RoutedEventArgs e)
        {  
            if(passwordBox_Copy.Password == "")
            {
                MessageBox.Show("נא להזין שם משתמש!!");
            }
            if(passwordBox.Password == "")
            {
                MessageBox.Show("נא להזין סיסמא!!");
            }
            else
            {
                if (passwordBox.Password == "1234" && passwordBox_Copy.Password=="זוהר")
                {
                    new ManagerView().ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(" סיסמא שגויה,נא להכניס סיסמא תקינה!");
                    passwordBox_Copy.Password = "";
                    passwordBox.Password = "";      
                }
            }
            
        }
    }
}
