using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(HandleIncrementMessage);
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.Title))
            {
                _moviePlayCounts[message.Title]++;
            }
            else
            {
                _moviePlayCounts.Add(message.Title, 1);
            }

            Console.WriteLine($"Movie {message.Title} has been played {_moviePlayCounts[message.Title]}");
        }
    }
}