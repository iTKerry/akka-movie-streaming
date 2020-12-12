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
            Console.WriteLine("Creating...");
            Console.WriteLine("Set initial behaviour to stopped.");
            
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(_ => Console.WriteLine("Cannot start playing movie before stopping existing one"));
            Receive<StopMovieMessage>(_ => StopPlayingMovie());

            Console.WriteLine("Has become to playing state.");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(msg => StartPlayingMovie(msg.Title));
            Receive<StopMovieMessage>(_ => Console.WriteLine("Error: Cannot stop if nothing is playing."));

            Console.WriteLine("Has become to stopped state.");
        }
        
        private void StartPlayingMovie(string message)
        {
            _currentlyWatching = message;
            Become(Playing);
        }

        private void StopPlayingMovie()
        {
            _currentlyWatching = null;
            Become(Stopped);
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