namespace InfimaGames.LowPolyShooterPack
{
    // Game Mode Service.
    public interface IGameModeService : IGameService
    {
        // Returns the Player Character.
        CharacterBehaviour GetPlayerCharacter();
    }
}
