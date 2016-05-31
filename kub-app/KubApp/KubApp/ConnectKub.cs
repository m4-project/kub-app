using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace KubApp
{
    public class ConnectKub
    {
        //Maakt een nieuwe MqttClient aan
        private MqttClient client = new MqttClient("home.jk-5.nl", 1883, false, MqttSslProtocols.None);

        //De dictionary voor de Kubs
        public Dictionary<string, Kub> kubs = new Dictionary<string, Kub>();

        //Aangemaakte Kub instantie
        private Kub kub;

        //Kub ID
        private string id;

        public ConnectKub()
        {
            client.Connect("kub-app");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //TODO: maak instellingenpagina om kubs te koppelen
            Kub kub = new Kub(id, client);
            this.kubs.Add(id, kub);
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string[] parts = e.Topic.Split('/');

            if (parts.Length != 3 || parts[0] != "kub")
            {
                return;
            }

            string kid = parts[1];

            if (e.Message.Length < 5)
            {
                return;
            }

            byte protocolVersion = e.Message[0];

            int payloadLength = BitConverter.ToInt32(e.Message, 1);

            if (e.Message.Length < payloadLength + 5)
            {
                return;
            }

            byte[] payload = e.Message.Skip(5).ToArray();
            Kub kub = kubs[kid];
            kub.PacketReceived(payload, parts[2]);
        }
    }
}
