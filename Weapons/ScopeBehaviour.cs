using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Scope Behaviour.
    public abstract class ScopeBehaviour : MonoBehaviour
    {
        #region GETTERS

        // Returns the value of multiplierMouseSensitivity.
        // <returns></returns>
        public abstract float GetMultiplierMouseSensitivity();
        
        // Returns the value of multiplierSpread.
        // <returns></returns>
        public abstract float GetMultiplierSpread();

        // Returns the aiming location offset.
        // <returns></returns>
        public abstract Vector3 GetOffsetAimingLocation();
        // Returns the aiming rotation offset.
        // <returns></returns>
        public abstract Vector3 GetOffsetAimingRotation();

        public abstract float GetFieldOfViewMultiplierAim();
        public abstract float GetFieldOfViewMultiplierAimWeapon();

        // Returns the Sprite used on the Character's Interface.
        public abstract Sprite GetSprite();
        // Returns the value to multiply the weapon sway by while aiming through this scope.
        public abstract float GetSwayMultiplier();

        #endregion

        #region METHODS

        // Called when the character using this scope aims through it.
        public abstract void OnAim();

        // Called when the character using this scope stops aiming through it.
        public abstract void OnAimStop();

        #endregion
    }
}
