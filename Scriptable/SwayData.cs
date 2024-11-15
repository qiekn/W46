using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // ScriptableObject containing a location and rotation curve, along with settings to interpolate
    // them using the Spring class.
    // Very helpful for lots of procedural motions that use curves.
    [CreateAssetMenu(fileName = "SO_SD_Default", menuName = "Infima Games/Low Poly Shooter Pack/Sway Data")]
    public class SwayData : ScriptableObject
    {
        #region PROPERTIES

        // Look.
        public SwayType Look => look;

        // Movement.
        public SwayType Movement => movement;

        // SpringSettings.
        public SpringSettings SpringSettings => springSettings;
        
        #endregion
        
        #region FIELDS SERIALIZED

        [Title(label: "Look")]
        
        [Tooltip("Look Sway.")]
        [SerializeField]
        private SwayType look;

        [Title(label: "Movement")]
        
        [Tooltip("Movement Sway.")]
        [SerializeField]
        private SwayType movement;
        
        [Title(label: "Spring Settings")]
        
        [Tooltip("Spring Settings For Sway.")]
        [SerializeField]
        private SpringSettings springSettings = SpringSettings.Default();
        
        #endregion
    }
}
