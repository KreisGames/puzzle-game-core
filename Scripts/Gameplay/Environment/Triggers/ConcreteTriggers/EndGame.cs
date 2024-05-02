namespace Gameplay.Environment.Triggers.ConcreteTriggers
{
    public class EndGame : BaseTrigger
    {
        public override void Trigger()
        {
            GameManager.EndGame();
        }
    }
}