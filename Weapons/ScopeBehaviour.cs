using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public abstract class ScopeBehaviour : MonoBehaviour {
        #region GETTERS

        //NOTE: Field & MouseSensitivity & Spread
        public abstract float GetFieldOfViewMultiplierAim();
        public abstract float GetFieldOfViewMultiplierAimWeapon();
        public abstract float GetMultiplierMouseSensitivity();
        public abstract float GetMultiplierSpread();

        //NOTE: Location & Rotation
        public abstract Vector3 GetOffsetAimingLocation();
        public abstract Vector3 GetOffsetAimingRotation();

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
