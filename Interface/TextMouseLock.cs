namespace InfimaGames.LowPolyShooterPack.Interface
{
    // This component handles warning developers whether their mouse is locked or not by
    // updating a text in the interface.
    public class TextMouseLock : ElementText
    {
        #region METHODS

        protected override void Tick()
        {
            //Update the text based on whether the cursor is locked or not.
            textMesh.text = "Cursor " + (characterBehaviour.IsCursorLocked() ? "Locked" : "Unlocked");
        }

        #endregion
    }
}
