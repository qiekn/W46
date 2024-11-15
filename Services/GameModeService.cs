namespace InfimaGames.LowPolyShooterPack
{
    // Game Mode Service.
    public class GameModeService : IGameModeService
    {
        #region FIELDS
        
        // The Player Character.
        private CharacterBehaviour playerCharacter;
        
        #endregion
        
        #region FUNCTIONS
        
        public CharacterBehaviour GetPlayerCharacter()
        {
            //Make sure we have a player character that is good to go!
            if (playerCharacter == null)
                playerCharacter = UnityEngine.Object.FindAnyObjectByType<CharacterBehaviour>();
            
            //Return.
            return playerCharacter;
        }
        
        #endregion
    }
}
