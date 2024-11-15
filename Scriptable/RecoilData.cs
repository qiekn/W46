using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Recoil Data. Used when playing recoil motions to have all the needed information
    // for that.
    [CreateAssetMenu(fileName = "SO_Recoil", menuName = "Infima Games/Low Poly Shooter Pack/Recoil Data", order = 0)]
    public class RecoilData : ScriptableObject
    {
        #region PROPERTIES

        // StandingStateMultiplier.
        public float StandingStateMultiplier => standingStateMultiplier;
        // Standing Curves.
        public ACurves StandingState => standingState;
        
        // AimingStateMultiplier.
        public float AimingStateMultiplier => aimingStateMultiplier;
        // Aiming Curves.
        public ACurves AimingState => aimingState;
        
        #endregion
        
        #region FIELDS SERIALIZED

        [Title(label: "Standing State")]
        
        [Tooltip("Value to multiply the standingState location/rotation values by.")]
        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float standingStateMultiplier = 1.0f;

        [Tooltip("Standing State.")]
        [SerializeField, InLineEditor]
        private ACurves standingState;

        [Title(label: "Aiming State")]

        [Tooltip("Value to multiply the aimingState location/rotation values by.")]
        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float aimingStateMultiplier = 1.0f;
        
        [Tooltip("Aiming State.")]
        [SerializeField, InLineEditor]
        private ACurves aimingState;
        
        #endregion
    }
}
