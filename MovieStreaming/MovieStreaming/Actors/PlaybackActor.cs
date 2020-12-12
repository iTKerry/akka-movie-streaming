using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine($"Creating a {nameof(PlaybackActor)}.");
        }
        
        protected override void OnReceive(object message)
        {
            if (message is PlayMovieMessage mgs)
            {
                Console.WriteLine($"User ID: #{mgs.UserId} and User Name: {mgs.UserName}");
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}