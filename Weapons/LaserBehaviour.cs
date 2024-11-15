using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Laser Abstract Class.
    public abstract class LaserBehaviour : MonoBehaviour
    {
        #region GETTERS

        // Returns the Sprite used on the Character's Interface.
        public abstract Sprite GetSprite();
        
        // Returns true if this laser should be off while the character is running.
        public abstract bool GetTurnOffWhileRunning();
        // Returns true if this laser should be off while the character is aiming.
        public abstract bool GetTurnOffWhileAiming();

        // Toggles the laser.
        public abstract void Toggle();
        // Reapplies the laser.
        public abstract void Reapply();
        // Hides the laser.
        public abstract void Hide();

        #endregion
    }
}
