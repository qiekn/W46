using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // LandMotion. This component plays the landing curves when a character lands.
    public class LandMotion : Motion
    {
        #region FIELDS SERIALIZED
        
        [Tooltip("Reference to the character's FeelManager component.")]
        [SerializeField, NotNull]
        private FeelManager feelManager;

        [Tooltip("Reference to the character's MovementBehaviour component.")]
        [SerializeField, NotNull]
        private MovementBehaviour movementBehaviour;
        
        [Tooltip("The character's Animator component.")]
        [SerializeField, NotNull]
        private Animator characterAnimator;

        [Title(label: "Settings")]

        [Tooltip("The type of this motion.")]
        [SerializeField]
        private MotionType motionType;
        
        #endregion
        
        #region FIELDS
        
        // The location spring.
        private readonly Spring springLocation = new Spring();
        // The rotation spring.
        private readonly Spring springRotation = new Spring();

        // Represents the curves currently being played by this component.
        private ACurves playedCurves;

        // Time.time at which the character last landed on the ground.
        private float landingTime;
        
        #endregion
        
        #region METHODS
        
        // Tick.
        public override void Tick()
        {
            //Check References.
            if (feelManager == null || movementBehaviour == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);

                //Return.
                return;
            }
            
            //Get Feel.
            Feel feel = feelManager.Preset.GetFeel(motionType);
            if (feel == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);
                
                //Return.
                return;
            }
            
            //Location.
            Vector3 location = default;
            //Rotation.
            Vector3 rotation = default;

            //We store the landing time.
            if (movementBehaviour.IsGrounded() && !movementBehaviour.WasGrounded())
                landingTime = Time.time;
            
            //We start playing the landing curves.
            playedCurves = feel.GetState(characterAnimator).LandingCurves;

            //Time where we evaluate the landing curves.
            float evaluateTime = Time.time - landingTime;
                
            //Evaluate Location Curves.
            location += playedCurves.LocationCurves.EvaluateCurves(evaluateTime);
            //Evaluate Rotation Curves.
            rotation += playedCurves.RotationCurves.EvaluateCurves(evaluateTime);

            //Evaluate Location Curves.
            springLocation.UpdateEndValue(location);
            //Evaluate Rotation Curves.
            springRotation.UpdateEndValue(rotation);
        }
        
        #endregion
        
        #region FUNCTIONS

        // GetLocation.
        public override Vector3 GetLocation()
        {
            //Check References.
            if (playedCurves == null)
                return default;

            //Return.
            return springLocation.Evaluate(playedCurves.LocationSpring);
        }
        // GetEulerAngles.
        public override Vector3 GetEulerAngles()
        {
            //Check References.
            if (playedCurves == null)
                return default;
            
            //Return.
            return springRotation.Evaluate(playedCurves.RotationSpring);
        }
        
        #endregion
    }
}
