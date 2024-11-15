using System;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // FeelState. Contains information on different things that happen in a single state.
    [Serializable]
    public struct FeelState
    {
        #region PROPERTIES

        // Offset.
        public FeelStateOffset Offset => offset;
        // SwayData.
        public SwayData SwayData => swayData;

        // JumpingCurves.
        public ACurves JumpingCurves => jumpingCurves;
        // FallingCurves.
        public ACurves FallingCurves => fallingCurves;
        // LandingCurves.
        public ACurves LandingCurves => landingCurves;
        
        #endregion
        
        #region FIELDS SERIALIZED
        
        [Title(label: "Offset")]
        
        [Tooltip("Offset.")]
        [SerializeField, InLineEditor]
        public FeelStateOffset offset;
        
        [Title(label: "Sway Data")]
        
        [Tooltip("Settings relating to sway.")]
        [SerializeField, InLineEditor]
        public SwayData swayData;
        
        [Title(label: "Jumping Curves")]

        [Tooltip("Animation curves played when the character jumps.")]
        [SerializeField, InLineEditor]
        public ACurves jumpingCurves;
        
        [Title(label: "Falling Curves")]
        
        [Tooltip("Animation curves played when the character falls.")]
        [SerializeField, InLineEditor]
        public ACurves fallingCurves;
        
        [Title(label: "Landing Curves")]

        [Tooltip("Animation curves played when the character lands.")]
        [SerializeField, InLineEditor]
        public ACurves landingCurves;
        
        #endregion
    }
}
