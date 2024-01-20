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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ServiceChat.IServiceChatCallback
    {
        bool isConnectet = false;
        ServiceChat.ServiceChatClient client;
        int Id;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        void ConnectUser() {
            if (!isConnectet)
            {
                client = new ServiceChat.ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                Id = client.Connect(tbusername.Text);
                tbusername.IsEnabled = false;
                bConnDisconn.Content = "Disconnect";
                isConnectet = true;
            }
        }
        void DisconnectUser() {
            if (isConnectet)
            {
                client.Disconnect(Id);
                client = null;
                tbusername.IsEnabled = true;
                bConnDisconn.Content = "Connect";
                isConnectet = true;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnectet){
                DisconnectUser();
            }
            if (!isConnectet) {
                ConnectUser();
            }
        }

        public void SendCallback(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.Send(tbMessage.Text, Id);
                    tbMessage.Text = string.Empty;
                    
                }
            }
        }
    }
}
