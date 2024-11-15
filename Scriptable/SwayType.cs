using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // SwayType. Holds information on horizontal, vertical SwayDirection values for SwayMotion components to use.
    [CreateAssetMenu(fileName = "SO_ST_Default", menuName = "Infima Games/Low Poly Shooter Pack/Sway Type")]
    public class SwayType : ScriptableObject
    {
        #region PROPERTIES

        // Horizontal.
        public SwayDirection Horizontal => horizontal;
        // Vertical.
        public SwayDirection Vertical => vertical;
        
        #endregion
        
        #region FIELDS SERIALIZED
        
        [Title(label: "Horizontal")]
        
        [Tooltip("Horizontal Sway.")]
        [SerializeField]
        private SwayDirection horizontal;

        [Title(label: "Vertical")]
        
        [Tooltip("Vertical Sway.")]
        [SerializeField]
        private SwayDirection vertical;
        
        #endregion
    }
}
