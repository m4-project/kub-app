using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace KubApp
{
    public class Kub
    {
        public delegate void OnResponse(uint value);

        public enum DataType
        {
            Temperature = 0
        };

        public enum Mode
        {
            Manual = 0,
            Temperature = 1
        };

        public string id { get; private set; }

        private Dictionary<int, OnResponse> requests = new Dictionary<int, OnResponse>();
        private short nextRequestId = 0;
        private MqttClient mqttClient;

        public Kub(string id, MqttClient mqttClient)
        {
            this.id = id;
            this.mqttClient = mqttClient;

            mqttClient.Subscribe(new string[] { "kub/" + this.id + "/kubres" }, new byte[] { 0 });
        }

        public void PacketReceived(byte[] payload, string packetType)
        {
            switch (packetType)
            {
                case "kubres":
                    //Correct endianness for payload bytes
                    ushort resId = BitConverter.ToUInt16(payload, 0);
                    uint value = BitConverter.ToUInt32(new byte[4] {payload[5], payload[4], payload[3], payload[2] }, 0);
                    if (!this.requests.ContainsKey(resId))
                    {
                        return;
                    }
                    OnResponse callback = this.requests[resId];
                    if (callback == null)
                    {
                        return;
                    }
                    callback(value);
                    break;
                default:
                    break;
            }
        }

        public void RequestData(DataType type, OnResponse callback)
        {
            short reqId = nextRequestId++;
            byte[] idPayload = BitConverter.GetBytes(reqId);
            byte[] reqPayload = new byte[8] { 1, 0, 0, 0, 3, (byte)type , idPayload[0], idPayload[1]};

            this.requests.Add(reqId, callback);
            mqttClient.Publish("kub/" + this.id + "/kubreq", reqPayload);
        }

        public void SetLed(byte led, byte r, byte g, byte b)
        {
            byte[] payload = new byte[9] { 1, 0, 0, 0, 4, led, r, g, b };
            mqttClient.Publish("kub/" + this.id + "/setled", payload);
        }

        public void SetMode(Mode mode)
        {
            byte[] payload = new byte[6] { 1, 0, 0, 0, 1, (byte)mode };
            mqttClient.Publish("kub/" + this.id + "/setmode", payload);
        }

        public override string ToString()
        {
            return this.id;
        }
    }
}
