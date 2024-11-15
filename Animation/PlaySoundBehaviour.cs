﻿using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Play Sound Behaviour. Plays an AudioClip using our custom AudioManager!
    public class PlaySoundBehaviour : StateMachineBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Setup")]
        
        [Tooltip("AudioClip to play!")]
        [SerializeField]
        private AudioClip clip;
        
        [Title(label: "Settings")]

        [Tooltip("Audio Settings.")]
        [SerializeField]
        private AudioSettings settings = new AudioSettings(1.0f, 0.0f, true);

        // Audio Manager Service. Handles all game audio.
        private IAudioManagerService audioManagerService;

        #endregion

        #region UNITY

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Try grab a reference to the sound managing service.
            audioManagerService ??= ServiceLocator.Current.Get<IAudioManagerService>();

            //Play!
            audioManagerService?.PlayOneShot(clip, settings);
        }

        #endregion
    }
}
