using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine($"Creating a {nameof(PlaybackActor)}.");
            Receive<PlayMovieMessage>(HandlePlayMovieMessage, message => message.UserId == 42);
        }

        private static void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            var (userId, userName) = message;
            Console.WriteLine($"User ID: #{userId} and User Name: {userName}");
        }
    }
}