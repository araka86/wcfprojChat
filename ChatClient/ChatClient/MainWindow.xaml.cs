using ChatClient.ServiceReferenceChat;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

namespace ChatClient
{


    public partial class MainWindow : Window, IServiceChatCallback
    {


        bool isConnected = false;
        //Обьект хоста типа нашего хоста в нашем клиенте для взаимодействия с его методами
        ServiceChatClient client;
        int ID;

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                btnConDiscon.Content = "Disconnect";
                isConnected = true;
            }

        }
        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                btnConDiscon.Content = "Connect";
                isConnected = false;
            }

        }





        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }

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
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
            }
        }
        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (lbChat.SelectedItem != null)
                {
                    Clipboard.SetText(lbChat.SelectedItem.ToString());
                    e.Handled = true;
                }
            }
        }
    }
}
