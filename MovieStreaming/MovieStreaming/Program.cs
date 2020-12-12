using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Actors;

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
            
            Console.Read();

            await _movieStreamingActorSystem.Terminate();
        }
    }
}