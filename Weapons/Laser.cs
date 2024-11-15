﻿using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // LaserType. Used to determine what functionality a Laser attachment has!
    public enum LaserType { Lasersight, Flashlight }
    
    // Laser. Represents a Weapon's laser.
    public class Laser : LaserBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]

        [Tooltip("Sprite. Displayed on the player's interface.")]
        [SerializeField]
        private Sprite sprite;

        [Tooltip("Type of laser.")]
        [SerializeField]
        private LaserType laserType;
        
        [Tooltip("True if the lasersight should start active.")]
        [SerializeField]
        private bool active = true;

        [Tooltip("If true, the laser will be turned off automatically while the character is running.")]
        [SerializeField]
        private bool turnOffWhileRunning = true;

        [Tooltip("If true, the laser will be turned off automatically while the character is aiming.")]
        [SerializeField]
        private bool turnOffWhileAiming = true;
        
        [Title(label: "Audio")]
        
        [Tooltip("The AudioClip played when toggling the laser.")]
        [SerializeField]
        private AudioClip toggleClip;

        [Tooltip("The AudioSettings used for the toggleClip.")]
        [SerializeField]
        private AudioSettings toggleAudioSettings;
        
        [Title(label: "Expanded Settings")]
        
        [Tooltip("Transform of the laser.")]
        [SerializeField]
        private Transform laserTransform;

        [ShowIf("laserType", LaserType.Lasersight)]
        [Tooltip("Determines how thick the laser beam is.")]
        [SerializeField]
        private float beamThickness = 1.2f;

        [ShowIf("laserType", LaserType.Lasersight)]
        [Tooltip("Maximum distance for tracing the laser beam.")]
        [SerializeField]
        private float beamMaxDistance = 500.0f;
        
        #endregion

        #region FIELDS

        // Beam Parent.
        private Transform beamParent;

        #endregion
        
        #region GETTERS

        // GetSprite.
        public override Sprite GetSprite() => sprite;
        // GetTurnOffWhileRunning.
        public override bool GetTurnOffWhileRunning() => turnOffWhileRunning;
        // GetTurnOffWhileAiming.
        public override bool GetTurnOffWhileAiming() => turnOffWhileAiming;

        #endregion
        
        #region METHODS
        
        // Toggle.
        public override void Toggle()
        {
            //Toggle active.
            active = !active;
            
            //Activate/Deactivate the laser.
            Reapply();
            
            //Plays a little sound now that we're toggling this laser!
            if(toggleClip != null)
                ServiceLocator.Current.Get<IAudioManagerService>().PlayOneShot(toggleClip, toggleAudioSettings);
        }
        // Reapply.
        public override void Reapply()
        {
            //Activate/Deactivate the laser.
            if(laserTransform != null)
                laserTransform.gameObject.SetActive(active);
        }
        // Hide.
        public override void Hide()
        {
            //Deactivate Laser.
            if(laserTransform != null)
                laserTransform.gameObject.SetActive(false);
        }
        
        #endregion

        #region UNITY

        // Awake.
        private void Awake()
        {
            //Ignore.
            if (laserTransform == null)
                return;
            
            //Cache beam parent.
            beamParent = laserTransform.parent;
        }
        // Update.
        private void Update()
        {
            //Ignore.
            if (laserTransform == null)
                return;
        
            //Target Scale. We'll use the default value if we don't hit anything with our raycast.
            float targetScale = beamMaxDistance;
            
            //Raycast forward from the beam starting point.
            if (Physics.Raycast(new Ray(laserTransform.position, beamParent.forward), out RaycastHit hit, beamMaxDistance))
                targetScale = hit.distance * 5.0f;
            
            //Scale to reach the hit location.
            beamParent.localScale = new Vector3(beamThickness, beamThickness, targetScale);
        }

        #endregion
    }
}
