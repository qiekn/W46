using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Animator Hashes.
    public static class AHashes
    {
        // Leaning Bool Hash.
        public static readonly int Leaning = Animator.StringToHash("Leaning");
        // Aiming Bool Hash.
        public static readonly int Aim = Animator.StringToHash("Aim");
        
        // Crouching Bool Hash.
        public static readonly int Crouching = Animator.StringToHash("Crouching");
        
        // Leaning Float Hash.
        public static readonly int LeaningInput = Animator.StringToHash("Leaning Input");
        // Stop Trigger Hash.
        public static readonly int Stop = Animator.StringToHash("Stop");
        
        // Reloading Bool Hash.
        public static readonly int Reloading = Animator.StringToHash("Reloading");
        // Inspecting Bool Hash.
        public static readonly int Inspecting = Animator.StringToHash("Inspecting");
        
        // Meleeing Bool Hash.
        public static readonly int Meleeing = Animator.StringToHash("Meleeing");
        // Grenading Bool Hash.
        public static readonly int Grenading = Animator.StringToHash("Grenading");
        
        // Bolt Action Bool Hash.
        public static readonly int Bolt = Animator.StringToHash("Bolt Action");
        
        // Holstering Bool Hash.
        public static readonly int Holstering = Animator.StringToHash("Holstering");
        // Holstered Bool Hash.
        public static readonly int Holstered = Animator.StringToHash("Holstered");

        // Running Bool Hash.
        public static readonly int Running = Animator.StringToHash("Running");
        // Lowered Bool Hash.
        public static readonly int Lowered = Animator.StringToHash("Lowered");
        
        // Alpha Action Offset Float Hash.
        public static readonly int AlphaActionOffset = Animator.StringToHash("Alpha Action Offset");

        // AlphaIKHandLeft.
        public static readonly int AlphaIKHandLeft = Animator.StringToHash("Alpha IK Hand Left");
        // AlphaIKHandRight.
        public static readonly int AlphaIKHandRight = Animator.StringToHash("Alpha IK Hand Right");
        
        // Aiming Alpha Value.
        public static readonly int AimingAlpha = Animator.StringToHash("Aiming");

        // Hashed "Movement".
        public static readonly int Movement = Animator.StringToHash("Movement");
        // Hashed "Leaning".
        public static readonly int LeaningForward = Animator.StringToHash("Leaning Forward");
        
        // Hashed "Aiming Speed Multiplier".
        public static readonly int AimingSpeedMultiplier = Animator.StringToHash("Aiming Speed Multiplier");
        // Hashed "Turning".
        public static readonly int Turning = Animator.StringToHash("Turning");
        
        // Hashed "Horizontal".
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        // Hashed "Vertical".
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        
        // Hashed "Play Rate Locomotion Forward".
        public static readonly int PlayRateLocomotionForward = Animator.StringToHash("Play Rate Locomotion Forward");
        // Hashed "Play Rate Locomotion Sideways".
        public static readonly int PlayRateLocomotionSideways = Animator.StringToHash("Play Rate Locomotion Sideways");
        // Hashed "Play Rate Locomotion Backwards".
        public static readonly int PlayRateLocomotionBackwards = Animator.StringToHash("Play Rate Locomotion Backwards");
    }
}
