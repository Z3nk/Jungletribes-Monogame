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
        public List<Player> _Players;
        private List<NetOutgoingMessage> _messagesToSend;
        private NetServer _serveur;
        public static WorldStateServer Instance { get; set; }

        public WorldStateServer(NetServer serveur)
        {
            _Players = new List<Player>();
            _serveur = serveur;
            if (Instance == null)
                Instance = this;
        }

        /// <summary>
        /// Avoir des enum permet de connaitre le template d'envoie de message et d eviter de faire de la reflection ou d'utiliser le mot clef dynamic qui sont gourmand
        /// </summary>
        /// <param name="typeMessage"></param>
        /// <param name="values"></param>
        public void CreateMessage(EnumMessageFromServer typeMessage, List<object> values, IPEndPoint listener)
        {
            NetOutgoingMessage message = _serveur.CreateMessage();
            switch (typeMessage)
            {
                case EnumMessageFromServer.Init:
                    message.Write((byte)values[0]); // EnumTypeElement type perso choisi
                    message.Write((string)values[1]); // String pseudo
                    break;
                case EnumMessageFromServer.Update:
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
                                var p = (EnumTypeElement)message.ReadByte(); // EnumTypeElement type perso choisi
                                ElementPlayable typeElement;
                                switch (p)
                                {
                                    case EnumTypeElement.CircleElement:
                                        typeElement = new CircleElement();
                                        break;

                                    default:
                                        typeElement = null;
                                        break;
                                }

                                var p2 = message.ReadString(); // String pseudo
                                Player NewPlayer = new Player()
                                {
                                    _Element = typeElement,
                                    _NamePlayer = p2,
                                    _PlayerState = EnumPlayerState.SearchingGame,
                                    _EndPoint = message.SenderEndPoint,
                                };
                                typeElement.position = new Vector2(new Random().Next(1920), new Random().Next(1080));
                                typeElement._MyPlayer = NewPlayer;

                                _Players.Add(NewPlayer);
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
