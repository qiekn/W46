using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Weapon Attachment Manager Behaviour.
    public abstract class WeaponAttachmentManagerBehaviour : MonoBehaviour
    {
        #region UNITY FUNCTIONS

        // Awake.
        protected virtual void Awake(){}

        // Start.
        protected virtual void Start(){}

        // Update.
        protected virtual void Update(){}

        // Late Update.
        protected virtual void LateUpdate(){}

        #endregion
        
        #region GETTERS

        // Returns the equipped scope.
        public abstract ScopeBehaviour GetEquippedScope();
        // Returns the equipped scope default.
        public abstract ScopeBehaviour GetEquippedScopeDefault();
        
        // Returns the equipped magazine.
        public abstract MagazineBehaviour GetEquippedMagazine();
        // Returns the equipped muzzle.
        public abstract MuzzleBehaviour GetEquippedMuzzle();
        
        // Returns the equipped laser.
        public abstract LaserBehaviour GetEquippedLaser();
        // Returns the equipped grip.
        public abstract GripBehaviour GetEquippedGrip();
        
        #endregion
    }
}
