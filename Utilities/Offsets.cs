using System;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Weapon Offsets.
    [Serializable]
    public struct Offsets
    {
        // Standing Location.
        public Vector3 StandingLocation => standingLocation;
        // Standing Rotation.
        public Vector3 StandingRotation => standingRotation;
        
        // Aiming Location.
        public Vector3 AimingLocation => aimingLocation;
        // Aiming Rotation.
        public Vector3 AimingRotation => aimingRotation;
        
        // Running Location.
        public Vector3 RunningLocation => runningLocation;
        // Running Rotation.
        public Vector3 RunningRotation => runningRotation;
        
        // Crouching Location.
        public Vector3 CrouchingLocation => crouchingLocation;
        // Crouching Rotation.
        public Vector3 CrouchingRotation => crouchingRotation;
        
        // Action Location.
        public Vector3 ActionLocation => actionLocation;
        // Action Rotation.
        public Vector3 ActionRotation => actionRotation;
        
        [Header("Standing Offset")]
        
        [Tooltip("Weapon bone location offset while standing.")]
        [SerializeField]
        private Vector3 standingLocation;
        
        [Tooltip("Weapon bone rotation offset while standing.")]
        [SerializeField]
        private Vector3 standingRotation;

        [Header("Aiming Offset")]
        
        [Tooltip("Weapon bone location offset while aiming.")]
        [SerializeField]
        private Vector3 aimingLocation;
        
        [Tooltip("Weapon bone rotation offset while aiming.")]
        [SerializeField]
        private Vector3 aimingRotation;
        
        [Header("Running Offset")]
        
        [Tooltip("Weapon bone location offset while running.")]
        [SerializeField]
        private Vector3 runningLocation;
        
        [Tooltip("Weapon bone rotation offset while running.")]
        [SerializeField]
        private Vector3 runningRotation;
        
        [Header("Crouching Offset")]
        
        [Tooltip("Weapon bone location offset while crouching.")]
        [SerializeField]
        private Vector3 crouchingLocation;
        
        [Tooltip("Weapon bone rotation offset while crouching.")]
        [SerializeField]
        private Vector3 crouchingRotation;
        
        [Header("Action Offset")]
        
        [Tooltip("Weapon bone location offset while performing an action (grenade, melee).")]
        [SerializeField]
        private Vector3 actionLocation;
        
        [Tooltip("Weapon bone rotation offset while performing an action (grenade, melee).")]
        [SerializeField]
        private Vector3 actionRotation;
    }
}
