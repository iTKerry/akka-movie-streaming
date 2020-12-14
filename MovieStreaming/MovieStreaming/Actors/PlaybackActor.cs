using Akka.Actor;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "PlaybackStatistics");
        }
    }
}