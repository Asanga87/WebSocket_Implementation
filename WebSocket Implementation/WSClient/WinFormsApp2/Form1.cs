using System.Net.WebSockets;
using System.Text;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ClientWebSocket ws = new ClientWebSocket();
        HttpClient client = new HttpClient();
        byte[] buffer = new byte[256];
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SetupConnection();//Establish connection with websocket.
                panel2.BackColor = ColorTranslator.FromHtml("#4c4c4d");
            }

            catch(Exception e1)
            {

                MessageBox.Show("System Error due to" + e1.ToString());
            }
           
        }

        //Establish connection with Web Socket. Websocket get method has one integer parameter to identify exact required function from client. 
        public async void SetupConnection()
        {
            await ws.ConnectAsync(new Uri("ws://localhost:6767/message?id=1"), CancellationToken.None);

        }

        //Welcome handler method excute from button1 click method.
        public async void WelcomeCallHandler()
        {
            String wellcometext = "";
            if (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                }
                else
                {

                   label1.Text = (Encoding.ASCII.GetString(buffer, 0, result.Count));

                }

               ws.Abort();

            }
            
        }

        //This is for Ping handler call. Excute from button 2 click method.Generate Http Call.
        public async void PingCallHandler()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:6767/api/Work/server/ping", null);
            if (message.IsSuccessStatusCode)
            {
                var Jstring = await message.Content.ReadAsStringAsync();
                String Jtext = Jstring;
                label1.Text = Jtext;
              
            }


        }
        //This is for Work handler call. Execute from button3 click method.Generate Http Call.
        public async void WorkCallHandler()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:6767/api/Work/work/start", null);
            if (message.IsSuccessStatusCode)
            {
                var Jstring = await message.Content.ReadAsStringAsync();
                String Jtext = Jstring;
                label1.Text = Jtext;

            }


        }
        //This is for Work handler call. Execute from button5 click method.Generate ws Call for same endpoint as Workerhandler.
        public async void WorkCallHandler_Ws()
        {
            using (var ws = new ClientWebSocket())
            {
                String text = "";
                await ws.ConnectAsync(new Uri("ws://localhost:6767/message?id=3"), CancellationToken.None);
                var buffer = new byte[256];
                while (ws.State == WebSocketState.Open || ws.State == WebSocketState.CloseSent)
                {
                    var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

                    Console.WriteLine(result.ToString());
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                        
                    }
                    else
                    {
                        text = Encoding.ASCII.GetString(buffer, 0, result.Count);
                        label1.Text=text;
                        
                    }
                }
              
            }




        }

        //Welcome method execution. Click method call to the Welcomehandler method.
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WelcomeCallHandler();

                label4.Text = "WS call from Windows client to Endpont: Message --> Reply From: From Web Socket open ws Client";
                panel2.BackColor = ColorTranslator.FromHtml("#4646ab");
                welcomePB.Visible = true;
            }

            catch(Exception e1)
            {

                MessageBox.Show("System Error due to" + e1.ToString());
            }
        }

        //Ping method execution. Click method call to pingCallHandler.
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PingCallHandler();
                label4.Text = "HTTP call from Windows client to server/ping POST Method -->  WS call from server/ping Post method to Web Socket --->Reply From: From Web Socket open POST Method---> Reply from POST method to Windows Client as Json";
                panel2.BackColor = ColorTranslator.FromHtml("#d94d07");
                PingPB.Visible = true;
            }
            catch (Exception e1)
            {

                MessageBox.Show("System Error due to" + e1.ToString());
            }
        }

        //Application exit button clcik. 
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Work method execution. Click method call to WorkCallHandler.
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                WorkCallHandler();
                label4.Text = "HTTP call from Windows client to work/start POST Method -->  WS call from work/start Post method to Web Socket --->Reply From: From Web Socket open POST Method---> Reply from POST method to Windows Client as Json";
                panel2.BackColor = ColorTranslator.FromHtml("#5ff53d");
                WAPB.Visible = true;
            }

            catch (Exception e1)
            {

                MessageBox.Show("System Error due to" + e1.ToString());
            }
        }

        //Work method execution via ws protocol. Click method call to WorkCallHandler.
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                WorkCallHandler_Ws();
                label4.Text = "WS call from Windows client to Endpont: Message --> Reply From: From Web Socket open ws Client";
                panel2.BackColor = ColorTranslator.FromHtml("#f0100c");
                WaWsPB.Visible = true;
            }
            catch (Exception e1)
            {

                MessageBox.Show("System Error due to" + e1.ToString());
            }
        }
    }
}