using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Abstract movement class. Handles interactions with the main movement component.
    public abstract class MovementBehaviour : MonoBehaviour
    {
        //NOTE: UNITY
        protected virtual void Awake(){}
        protected virtual void Start(){}
        protected virtual void Update(){}
        protected virtual void FixedUpdate(){}
        protected virtual void LateUpdate(){}

        //NOTE: GETTERS

        // Returns the last Time.time value at which the character jumped.
        public abstract float GetLastJumpTime();
        
        public abstract float GetMultiplierForward();
        public abstract float GetMultiplierSideways();
        public abstract float GetMultiplierBackwards();

        // Returns the character's current velocity.
        public abstract Vector3 GetVelocity();
        // Returns true if the character is grounded.
        public abstract bool IsGrounded();
        // Returns last frame's IsGrounded value.
        public abstract bool WasGrounded();
        
        // Returns true if the character is jumping.
        public abstract bool IsJumping();

        // Returns true if the character can set its crouching value to the passed parameter.
        public abstract bool CanCrouch(bool newCrouching);
        // Returns true if the character is crouching.
        public abstract bool IsCrouching();


        //NOTE: METHODS

        // Calling this will make the character jump!
        public abstract void Jump();
        // Forces crouch/un-crouch!
        public abstract void Crouch(bool crouching);
        // Tries to crouch/un-crouch.
        public abstract void TryCrouch(bool value);
        
        // Tries to toggle the crouching state. This method should also make sure to handle any automatic
        // un-crouching that may happen when the character stops being under an object.
        public abstract void TryToggleCrouch();
    }
}
