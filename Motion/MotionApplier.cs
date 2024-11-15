using UnityEngine;
using System.Collections.Generic;

namespace InfimaGames.LowPolyShooterPack
{
    // ApplyMode. Determines how a MotionApplier should apply the values from the Motions that are subscribed to it.
    public enum ApplyMode { Override, Add }
    
    // MotionApplier. Applies all location, rotation values from Motion components subscribed to it in accordance with the
    // settings of this component.
    public class MotionApplier : MonoBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Settings")]

        [Tooltip("Determines the way this component applies the values for all subscribed Motion components.")]
        [SerializeField]
        private ApplyMode applyMode;
        
        #endregion
        
        #region FIELDS
        
        // Subscribed Motions.
        private readonly List<Motion> motions = new List<Motion>();

        // This Transform.
        private Transform thisTransform;

        #endregion
        
        #region METHODS

        // Awake.
        private void Awake()
        {
            //Cache.
            thisTransform = transform;
        }
        // LateUpdate.
        private void LateUpdate()
        {
            //Final Location.
            Vector3 finalLocation = default;
            //Final Euler Angles.
            Vector3 finaEulerAngles = default;
            
            //ForEach Motion.
            motions.ForEach((motion =>
            {
                //Tick.
                motion.Tick();
                
                //Add Location.
                finalLocation += motion.GetLocation() * motion.Alpha;
                //Add Rotation.
                finaEulerAngles += motion.GetEulerAngles() * motion.Alpha;
            }));

            //Override Mode.
            if(applyMode == ApplyMode.Override)
            {
                //Set Location.
                thisTransform.localPosition = finalLocation;
                //Set Euler Angles.
                thisTransform.localEulerAngles = finaEulerAngles;
            }
            //Add Mode.
            else if (applyMode == ApplyMode.Add)
            {
                //Add Location.
                thisTransform.localPosition += finalLocation;
                //Add Euler Angles.
                thisTransform.localEulerAngles += finaEulerAngles;
            }
        }
        
        // Subscribe a Motion to this MotionApplier. This means that the Motion's results every frame will be computed,
        // and applied, by this MotionApplier.
        public void Subscribe(Motion motion) => motions.Add(motion);
        
        #endregion
    }
}
