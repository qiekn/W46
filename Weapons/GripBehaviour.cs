using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Grip Abstract Class.
    public abstract class GripBehaviour : MonoBehaviour
    {
        #region GETTERS

        // Returns the Sprite used on the Character's Interface.
        public abstract Sprite GetSprite();

        #endregion
    }
}
