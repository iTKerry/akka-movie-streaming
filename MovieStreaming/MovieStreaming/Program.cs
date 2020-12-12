using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace MovieStreaming
{
    internal static class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        private static async Task Main()
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            Console.Read();

            await _movieStreamingActorSystem.Terminate();
        }
    }
}