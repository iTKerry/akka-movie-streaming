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

            var userProps = Props.Create<UserActor>();
            var userActorRef = _movieStreamingActorSystem.ActorOf(userProps, nameof(UserActor));

            Console.WriteLine($"Sending {nameof(PlayMovieMessage)}: Codenan the destroyer");
            userActorRef.Tell(new PlayMovieMessage(42, "Codenan the destroyer"));
            
            Console.WriteLine($"Sending {nameof(PlayMovieMessage)}: Boolean Lies");
            userActorRef.Tell(new PlayMovieMessage(42, "Boolean Lies"));
            
            Console.WriteLine($"Sending {nameof(StopMovieMessage)} #1");
            userActorRef.Tell(new StopMovieMessage());
            
            Console.WriteLine($"Sending {nameof(StopMovieMessage)} #2");
            userActorRef.Tell(new StopMovieMessage());
            
            await _movieStreamingActorSystem.Terminate();
            Console.WriteLine($"{nameof(ActorSystem)} terminated.");            
            
            Console.Read();
        }
    }
}