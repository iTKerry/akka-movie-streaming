using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine($"[{nameof(PlaybackActor)}] Creating...");
            Receive<PlayMovieMessage>(HandlePlayMovieMessage);
        }

        private static void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            var (userId, userName) = message;
            Console.WriteLine($"User ID: #{userId} and User Name: {userName}");
        }

        protected override void PreStart()
        {
            Console.WriteLine($"[{nameof(PlaybackActor)}] {nameof(PreStart)}");
        }

        protected override void PostStop()
        {
            Console.WriteLine($"[{nameof(PlaybackActor)}] {nameof(PostStop)}");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"[{nameof(PlaybackActor)}] {nameof(PreRestart)} because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"[{nameof(PlaybackActor)}] {nameof(PostRestart)} because: {reason}");
            base.PostRestart(reason);
        }
    }
}