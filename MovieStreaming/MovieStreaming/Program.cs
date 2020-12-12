using System;
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
            Console.WriteLine($"[{nameof(ActorSystem)}] created.");
            
            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, nameof(PlaybackActor));
            
            playbackActorRef.Tell(new PlayMovieMessage(1, "Akka.NET: THE MOVIE"));
            
            await _movieStreamingActorSystem.Terminate();
            Console.WriteLine($"{nameof(ActorSystem)} terminated.");            
            
            Console.Read();
        }
    }
}