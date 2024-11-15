using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // LowerData. Contains all the information needed by a character to lower its weapon with some nice
    // motion, and nice offsets.
    [CreateAssetMenu(fileName = "SO_Lower_Name", menuName = "Infima Games/Low Poly Shooter Pack/Lower Data", order = 0)]
    public class LowerData : ScriptableObject
    {
        #region PROPERTIES
        
        // Interpolation.
        public SpringSettings Interpolation => interpolation;

        // LocationOffset.
        public Vector3 LocationOffset => locationOffset;
        // RotationOffset.
        public Vector3 RotationOffset => rotationOffset;
        
        #endregion
        
        #region FIELDS SERIALIZED

        [Title(label: "Interpolation")]

        [Tooltip("Interpolation settings.")]
        [SerializeField]
        private SpringSettings interpolation = SpringSettings.Default();

        [Title(label: "Offsets")]

        [Tooltip("Location offset applied in the lowered state.")]
        [SerializeField]
        private Vector3 locationOffset;

        [Tooltip("Rotation offset applied in the lowered state.")]
        [SerializeField]
        private Vector3 rotationOffset;

        #endregion
    }
}
