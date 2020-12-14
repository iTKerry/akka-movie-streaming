using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            _userId = userId;
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(_ => Console.WriteLine("Cannot start playing movie before stopping existing one"));
            Receive<StopMovieMessage>(_ => StopPlayingMovie());
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(msg => StartPlayingMovie(msg.Title));
            Receive<StopMovieMessage>(_ => Console.WriteLine("Error: Cannot stop if nothing is playing."));
        }
        
        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            Context
                .ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));
            Become(Playing);
        }

        private void StopPlayingMovie()
        {
            _currentlyWatching = null;
            Become(Stopped);
        }
    }
}