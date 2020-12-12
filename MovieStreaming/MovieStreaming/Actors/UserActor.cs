using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine($"[{nameof(UserActor)}] Creating...");
            Receive<PlayMovieMessage>(HandlePlayMovieMessage);
            Receive<StopMovieMessage>(HandleStopMovieMessage);
        }

        private void HandleStopMovieMessage(StopMovieMessage message)
        {
            _currentlyWatching = null;
            Console.WriteLine($"[{nameof(UserActor)}:{nameof(HandleStopMovieMessage)}]");
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (string.IsNullOrWhiteSpace(_currentlyWatching))
            {
                StartPlayingMovie(message.Title);
            }
            else
            {
                Console.WriteLine(
                    $"[{nameof(UserActor)}:{nameof(HandlePlayMovieMessage)}] " +
                    $"Error: cannot start playing another movie before stopping existing one ({_currentlyWatching}).");
            }
        }

        private void StartPlayingMovie(string message)
        {
            _currentlyWatching = message;
            Console.WriteLine(
                $"[{nameof(UserActor)}:{nameof(HandlePlayMovieMessage)}] " +
                $"User is currently watching {_currentlyWatching}.");
        }

        protected override void PreStart()
        {
            Console.WriteLine($"[{nameof(UserActor)}] {nameof(PreStart)}");
        }

        protected override void PostStop()
        {
            Console.WriteLine($"[{nameof(UserActor)}] {nameof(PostStop)}");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"[{nameof(UserActor)}] {nameof(PreRestart)} because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"[{nameof(UserActor)}] {nameof(PostRestart)} because: {reason}");
            base.PostRestart(reason);
        }
    }
}