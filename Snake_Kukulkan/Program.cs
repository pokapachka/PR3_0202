using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common;
using Newtonsoft.Json;

namespace Snake_Kukulkan
{
    public class Program
    {
        public static List<Leaders> Leaders = new List<Leaders>();
        public static List<ViewModelUserSettings> RemoteIpAddresses = new List<ViewModelUserSettings>();
        public static List<ViewModelGames> ViewModelGames = new List<ViewModelGames>();
        private static Int32 _localPort = 5001;
        public static Int32 MaxSpeed = 15;
        private static void Send() 
        {
            foreach (ViewModelUserSettings User in RemoteIpAddresses) 
            {
                UdpClient sender = new UdpClient();
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(User.IPAddress), Int32.Parse(User.Port));
                try 
                {
                    Byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ViewModelGames.Find(x => x.IdSnake == User.IdSnake)));
                    sender.Send(bytes, bytes.Length, endPoint);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Data sended to client {User.IPAddress}:{User.Port}");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An exception occured in send method: {(e.Message.Length > 100 ? e.Message.Substring(0,100) : e.Message)}");
                }
                finally { sender.Close(); }
            }
        }
        public static int AddSnake() 
        {
            ViewModelGames vmgPlayer = new ViewModelGames();
            vmgPlayer.SnakesPlayers = new Snakes() 
            {
                Points = new List<Snakes.Point> 
                {
                    new Snakes.Point(30, 10),
                    new Snakes.Point(20, 10),
                    new Snakes.Point(10, 10),
                }, 
                Direction = Snakes.DirectionType.Start
            };
            vmgPlayer.Points = new Snakes.Point(new Random().Next(10, 783), new Random().Next(10, 410));
            ViewModelGames.Add(vmgPlayer);
            return ViewModelGames.FindIndex(x => x == vmgPlayer);
        }
        public static void Reciever() 
        {
            UdpClient recievingUdpClient = new UdpClient(_localPort);
            IPEndPoint remoteIpEndPoint = null;
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Server initialized");
                while (true) 
                {
                    Byte[] recievedBytes = recievingUdpClient.Receive(ref remoteIpEndPoint);
                    String recievedData = Encoding.UTF8.GetString(recievedBytes).Trim();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Recieved data: {recievedData}");
                    if (recievedData == String.Empty) 
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Recieved data is empty");
                        return;
                    }
                    String[] parts = recievedData.Split(new char[] { '|' });
                    if (parts.Length < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Recieved data obtained has fewer parts than two");
                        return;
                    }
                    if (parts[0] == "/start")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Processing the start command");
                        try
                        {
                            ViewModelUserSettings vmus = JsonConvert.DeserializeObject<ViewModelUserSettings>(parts[1]);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"User who sent the start command: {vmus.IPAddress}:{vmus.Port}");
                            RemoteIpAddresses.Add(vmus);
                            vmus.IdSnake = AddSnake();
                            ViewModelGames[vmus.IdSnake].IdSnake = vmus.IdSnake;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Start command has been successfully processed");
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("An exception occured in start command");
                            return;
                        }
                    }
                    else if (parts[0] == "Up" || parts[0] == "Right" || parts[0] == "Down" || parts[0] == "Left")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Processing the move command");
                        try 
                        {
                            ViewModelUserSettings vmus = JsonConvert.DeserializeObject<ViewModelUserSettings>(parts[1]);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"User who sent the move command: {vmus.IPAddress}:{vmus.Port}");
                            Int32 PlayerId = RemoteIpAddresses.FindIndex(x => x.IPAddress == vmus.IPAddress);
                            if (PlayerId != -1)
                            {
                                switch (parts[0]) 
                                {
                                    case "Up":
                                        if (ViewModelGames[PlayerId].SnakesPlayers.Direction != Snakes.DirectionType.Down)
                                            ViewModelGames[PlayerId].SnakesPlayers.Direction = Snakes.DirectionType.Up;
                                        break;
                                    case "Right":
                                        if (ViewModelGames[PlayerId].SnakesPlayers.Direction != Snakes.DirectionType.Left)
                                            ViewModelGames[PlayerId].SnakesPlayers.Direction = Snakes.DirectionType.Right;
                                        break;
                                    case "Down":
                                        if (ViewModelGames[PlayerId].SnakesPlayers.Direction != Snakes.DirectionType.Up)
                                            ViewModelGames[PlayerId].SnakesPlayers.Direction = Snakes.DirectionType.Down;
                                        break;
                                    case "Left":
                                        if (ViewModelGames[PlayerId].SnakesPlayers.Direction != Snakes.DirectionType.Right)
                                            ViewModelGames[PlayerId].SnakesPlayers.Direction = Snakes.DirectionType.Left;
                                        break;
                                }
                            }
                            else 
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine($"Player with specified IP-address has not found");
                                return;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Move command has been successfully processed");
                        }
                        catch 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("An exception occured in move command");
                            return;
                        }
                    }
                    else 
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Сommand cannot be processed, incoming command: {parts[0]}");
                    }
                }
            }
            catch (Exception e) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An exception occured in reciever method: {(e.Message.Length > 100 ? e.Message.Substring(0, 100) : e.Message)}");
            }
        }
        public static void Main(string[] args)
        {

        }
    }
}
