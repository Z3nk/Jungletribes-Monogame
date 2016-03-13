using Jungletribes_Common;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Jungletribes_Server
{
    public class WorldStateServer
    {
        private IEnumerable<Element> _networkObjects;
        private IEnumerable<NetOutgoingMessage> _messagesToSend;
        private NetServer _serveur;
        public static WorldStateServer Instance { get; set; }
        private IPEndPoint _ipEndPoint;

        public WorldStateServer(NetServer serveur)
        {
            _networkObjects = new List<Element>();
            _serveur = serveur;
            if (Instance == null)
                Instance = this;
        }

        /// <summary>
        /// Avoir des enum permet de connaitre le template d'envoie de message et d eviter de faire de la reflection ou d'utiliser le mot clef dynamic qui sont gourmand
        /// </summary>
        /// <param name="typeMessage"></param>
        /// <param name="values"></param>
        public void CreateMessage(EnumMessageToServer typeMessage, List<object> values)
        {
            NetOutgoingMessage message = _serveur.CreateMessage();
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

        public void Update()
        {
            NetIncomingMessage message;
            while ((message = _serveur.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        EnumMessageToServer typeMessage = (EnumMessageToServer)message.ReadByte();
                        switch (typeMessage)
                        {
                            case EnumMessageToServer.Init:
                                break;
                            case EnumMessageToServer.Update:
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

                _serveur.Recycle(message);
            }
        }
    }
}
