using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // MotionType.
    public enum MotionType { Camera, Item }
    
    // Motion. This abstract class serves as a base class for all components that apply any sort of cool procedural
    // motions to either the weapons, or the camera, in the asset.
    // It has a bunch of helper things that make it easier to handle, and runs through the MotionApplier, forming
    // a nice cycle! 
    [RequireComponent(typeof(MotionApplier))]
    public abstract class Motion : MonoBehaviour
    {
        #region PROPERTIES
        
        // Alpha.
        public float Alpha => alpha;
        
        #endregion
        
        #region FIELDS SERIALIZED
        
        [Title(label: "Motion")]
        
        [Tooltip("The Motion's alpha. Used to more easily control how much of the motion is applied.")]
        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float alpha = 1.0f;

        [Title(label: "References")]
        
        [Tooltip("The MotionApplier that will apply this Motion's values.")]
        [SerializeField, NotNull]
        protected MotionApplier motionApplier;
        
        #endregion
        
        #region METHODS
        
        // Awake.
        protected virtual void Awake()
        {
            //Try to get the applier if we haven't assigned it.
            if (motionApplier == null)
                motionApplier = GetComponent<MotionApplier>();
            
            //Subscribe.
            if(motionApplier != null)
                motionApplier.Subscribe(this);
        }

        // Tick.
        public abstract void Tick();
        
        #endregion
        
        #region FUNCTIONS
        
        // GetLocation.
        public abstract Vector3 GetLocation();
        // GetEulerAngles.
        public abstract Vector3 GetEulerAngles();
        
        #endregion
    }
}
