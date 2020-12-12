using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    internal static class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        private static async Task Main()
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("ActorSystem created.");
            
            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, nameof(PlaybackActor));
            
            playbackActorRef.Tell(new PlayMovieMessage(1, "Akka.NET: THE MOVIE"));

            var packet = new Packet(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
            
            playbackActorRef.Tell(packet);
            
            Console.Read();

            await _movieStreamingActorSystem.Terminate();
        }
    }
    
    public class Packet
    {
        public int Pos { get; private set; } = 0;
        public byte[] Buf { get; set; } = new byte[1460];
        public Socket Socket { get; private set; }
        
        public Packet(Socket socket)
        {
            Socket = socket;
        }
    }
}