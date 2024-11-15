using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Magazine Behaviour.
    public abstract class MagazineBehaviour : MonoBehaviour
    {
        #region GETTERS
        
        // Returns The Total Ammunition.
        public abstract int GetAmmunitionTotal();
        // Returns the Sprite used on the Character's Interface.
        public abstract Sprite GetSprite();

        #endregion
    }
}
