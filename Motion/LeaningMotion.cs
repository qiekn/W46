using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // LeaningMotion. This component handles all leaning-related procedural animation through the MotionApplier
    // system!
    public class LeaningMotion : Motion
    {
        #region FIELDS SERIALIZED
        
        [Tooltip("The character's InventoryBehaviour component.")]
        [SerializeField, NotNull]
        private InventoryBehaviour inventoryBehaviour;
        
        [Tooltip("The character's CharacterBehaviour component.")]
        [SerializeField, NotNull]
        private CharacterBehaviour characterBehaviour;
        
        [Tooltip("The character's Animator component.")]
        [SerializeField, NotNull]
        private Animator characterAnimator;
        
        [Title(label: "Settings")]

        [Tooltip("The type of motion we want this component to apply.")]
        [SerializeField]
        private MotionType motionType;
        
        #endregion
        
        #region FIELDS
        
        // Spring Location. Used to get the GameObject leaning.
        private readonly Spring springLocation = new Spring();
        // Spring Rotation. Used to get the GameObject leaning.
        private readonly Spring springRotation = new Spring();

        // Leaning curves to play.
        private ACurves leaningCurves;
        
        #endregion
        
        #region METHODS

        // Tick.
        public override void Tick()
        {
            //Check for reference errors.
            if (inventoryBehaviour == null || characterBehaviour == null || characterAnimator == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);
                
                //Return.
                return;
            }

            //Try to get an ItemAnimationDataBehaviour from the equipped weapon.
            var animationDataBehaviour = inventoryBehaviour.GetEquipped().GetComponent<ItemAnimationDataBehaviour>();
            //If there's none, then we don't even need to run this script at all, basically.
            if (animationDataBehaviour == null)
                return;

            //Try to get the LeaningData.
            LeaningData leaningData = animationDataBehaviour.GetLeaningData();
            if (leaningData == null)
                return;
            
            //This returns the correct leaning curves to use based on whether the character is aiming.
            leaningCurves = leaningData.GetCurves(motionType, characterBehaviour.IsAiming());
            //Check Reference.
            if (leaningCurves == null)
            {
                //Reset.
                springLocation.UpdateEndValue(default);
                springRotation.UpdateEndValue(default);

                //Return.
                return;
            }

            //Grab the leaning value from the character's Animator.
            float leaning = characterAnimator.GetFloat(AHashes.LeaningInput);
            
            //Update Location.
            springLocation.UpdateEndValue(leaningCurves.LocationCurves.EvaluateCurves(leaning) * leaningCurves.LocationMultiplier);
            //Update Rotation.
            springRotation.UpdateEndValue(leaningCurves.RotationCurves.EvaluateCurves(leaning) * leaningCurves.RotationMultiplier);
        }
        
        #endregion
        
        #region FUNCTIONS

        // GetLocation.
        public override Vector3 GetLocation()
        {
            //Check Reference.
            if (leaningCurves == null)
                return default;
            
            //Return.
            return springLocation.Evaluate(leaningCurves.LocationSpring);
        }
        // GetEulerAngles.
        public override Vector3 GetEulerAngles()
        {           
            //Check Reference.
            if (leaningCurves == null)
                return default;
            
            //Return.
            return springRotation.Evaluate(leaningCurves.RotationSpring);
        }
        
        #endregion
    }
}
