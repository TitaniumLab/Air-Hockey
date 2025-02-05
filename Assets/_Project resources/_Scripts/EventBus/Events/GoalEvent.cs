namespace AirHockey
{
    public class GoalEvent : ISubscriber
    {
        public int TeamIndex;

        public GoalEvent(int teamIndex)
        {
            TeamIndex = teamIndex;
        }
    }
}
