namespace Gameplay.Environment.Triggers.ConcreteTriggers
{
    public class UndoReset : BaseTrigger
    {
        public override void Trigger()
        {
            GameManager.ResetUndo();
        }
    }
}