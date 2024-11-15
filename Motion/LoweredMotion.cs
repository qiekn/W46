using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // LoweredMotion. This class drives the procedural offsets that lower a weapon.
    public class LoweredMotion : Motion
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "References")]
        
        [Tooltip("The LowerWeapon component that determines whether the character is lowering their " +
                 "weapon, or not at any given time.")]
        [SerializeField, NotNull]
        private LowerWeapon lowerWeapon;

        [Title(label: "References Character")]
        
        [Tooltip("The character's CharacterBehaviour component.")]
        [SerializeField, NotNull]
        private CharacterBehaviour characterBehaviour;
        
        [Tooltip("The character's InventoryBehaviour component.")]
        [SerializeField, NotNull]
        private InventoryBehaviour inventoryBehaviour;

        #endregion
        
        #region FIELDS
        
        // Lowered Spring Location. Used to get the GameObject into a changed lowered
        // pose.
        private readonly Spring loweredSpringLocation = new Spring();
        // Recoil Spring Rotation. Used to get the GameObject into a changed lowered
        // pose.
        private readonly Spring loweredSpringRotation = new Spring();

        // LowerData for the current equipped weapon. If there's none, then there's no lowering, I guess.
        private LowerData lowerData;
        
        #endregion
        
        #region METHODS

        // Tick.
        public override void Tick()
        {
            //Check References.
            if (lowerWeapon == null || characterBehaviour == null || inventoryBehaviour == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);

                //Return.
                return;
            }

            //Get ItemAnimationDataBehaviour.
            var animationData = inventoryBehaviour.GetEquipped().GetComponent<ItemAnimationDataBehaviour>();
            if (animationData == null)
                return;
            
            //Get LowerData.
            lowerData = animationData.GetLowerData();
            if (lowerData == null)
                return;

            //Update Location Value.
            loweredSpringLocation.UpdateEndValue(lowerWeapon.IsLowered() ? lowerData.LocationOffset : default);
            //Update Rotation Value.
            loweredSpringRotation.UpdateEndValue(lowerWeapon.IsLowered() ? lowerData.RotationOffset : default);
        }
        
        #endregion
        
        #region FUNCTIONS

        // GetLocation.
        public override Vector3 GetLocation()
        {
            //Check References.
            if (lowerData == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);

                //Return;
                return default;
            }
            
            //Return.
            return loweredSpringLocation.Evaluate(lowerData.Interpolation);
        }
        // GetEulerAngles.
        public override Vector3 GetEulerAngles()
        {
            //Check References.
            if (lowerData == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);

                //Return;
                return default;
            }
            
            //Return.
            return loweredSpringRotation.Evaluate(lowerData.Interpolation);
        }
        
        #endregion
    }
}
