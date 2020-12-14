using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                var user = GetOrCreateChildUser(message.UserId);
                user.Tell(message);
            });
            
            Receive<StopMovieMessage>(message =>
            {
                var user = GetOrCreateChildUser(message.UserId);
                user.Tell(message);
            });
        }

        private IActorRef GetOrCreateChildUser(int userId)
        {
            if (_users.ContainsKey(userId))
                return _users[userId];
            
            var user = Context.ActorOf(Props.Create(() => new UserActor(userId)), $"User{userId}");
            _users.Add(userId, user);
            return user;
        }
    }
}