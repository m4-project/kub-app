﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace KubApp
{
    class Kub
    {
        public delegate void OnResponse(int value);

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

            this.RequestData(DataType.Temperature, delegate (int value)
            {
                System.Diagnostics.Debug.WriteLine("Temperature: " + value);
            });
        }

        public void PacketReceived(byte[] payload, string packetType)
        {
            switch(packetType)
            {
                case "kubres":
                    //Handle
                    short resId = BitConverter.ToInt16(payload, 0);
                    int value = BitConverter.ToInt32(payload, 2);
                    OnResponse callback = this.requests[resId];
                    if(callback == null)
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
            byte[] reqPayload = new byte[3];
            reqPayload[0] = (byte) type;
            byte[] idPayload = BitConverter.GetBytes(reqId);
            reqPayload[1] = idPayload[0];
            reqPayload[2] = idPayload[1];

            this.requests.Add(reqId, callback);
            mqttClient.Publish("kub/" + this.id + "/kubreq", reqPayload);
        }

        public void SetLed(byte led, byte r, byte g, byte b)
        {
            byte[] payload = new byte[4] { led, r, g, b };
            mqttClient.Publish("kub/" + this.id + "/setled", payload);
        }

        public void SetMode(Mode mode)
        {
            byte[] payload = new byte[1] { (byte) mode };
            mqttClient.Publish("kub/" + this.id + "/setmode", payload);
        }
    }
}
