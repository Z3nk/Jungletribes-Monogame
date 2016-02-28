using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.Net;

namespace Jungletribes_Common
{
    public class WorldState
    {
        private IEnumerable<Element> _networkObjects;
        private IEnumerable<NetOutgoingMessage> _messagesToSend;
        private NetClient _client;
        public static WorldState Instance { get; set; }
        private IPEndPoint _ipEndPoint;

        public WorldState(NetClient client)
        {
            _networkObjects = new List<Element>();
            _client = client;
            if (Instance == null)
                Instance = this;
        }

        /// <summary>
        /// Avoir des enum permet de connaitre le template d'envoie de message et d eviter de faire de la reflection ou d'utiliser le mot clef dynamic qui sont gourmand
        /// </summary>
        /// <param name="typeMessage"></param>
        /// <param name="values"></param>
        public void CreateMessage(EnumMessageToServer typeMessage,List<object> values)
        {
            NetOutgoingMessage message = _client.CreateMessage();
            switch (typeMessage)
            {
                case EnumMessageToServer.Init:
                    message.Write((byte)values[0]); // EnumTypeElement type perso choisi
                    message.Write((string)values[1]); // String pseudo
                    break;
                case EnumMessageToServer.Update:
                    message.Write((byte)values[0]); // EnumMoveCommand command de deplacement
                    message.Write((byte)values[1]); // EnumActionCommand command d'actions
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            NetIncomingMessage message;
            while ((message = _client.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        EnumMessageFromServer typeMessage = (EnumMessageFromServer)message.ReadByte();
                        switch (typeMessage)
                        {
                            case EnumMessageFromServer.Init:
                                _ipEndPoint = message.SenderEndPoint;
                                uint nbElements = message.ReadUInt16();
                                for (int i = 0; i < nbElements; i++)
                                {
                                    EnumTypeElement elementType = (EnumTypeElement)message.ReadByte();
                                    switch (elementType)
                                    {
                                        case EnumTypeElement.Loup:
                                            break;
                                        case EnumTypeElement.Guerrier:
                                            break;
                                    }
                                }
                                    break;
                            case EnumMessageFromServer.Update:
                                int ID = message.ReadUInt16();
                                var NetworkObject = _networkObjects.FirstOrDefault(p => p != null && p.id == ID);
                                if (NetworkObject != null)
                                    NetworkObject.Synchronise(message);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        switch (message.SenderConnection.Status)
                        {
                            case NetConnectionStatus.None:
                                break;
                            case NetConnectionStatus.InitiatedConnect:
                                break;
                            case NetConnectionStatus.ReceivedInitiation:
                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                break;
                            case NetConnectionStatus.RespondedConnect:
                                break;
                            case NetConnectionStatus.Connected:
                                break;
                            case NetConnectionStatus.Disconnecting:
                                break;
                            case NetConnectionStatus.Disconnected:
                                break;
                            default:
                                break;
                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        Console.WriteLine(message.ReadString());
                        break;

                    default:
                        Console.WriteLine("unhandled message with type: "
                            + message.MessageType);
                        break;
                }

                _client.Recycle(message);
            }

            foreach(var NetworkObject in _networkObjects)
            {
                NetworkObject.Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime)
        {
            foreach (var NetworkObject in _networkObjects)
            {
                NetworkObject.Draw(gameTime);
            }
        }
    }
}
