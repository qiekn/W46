using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Abstract movement class. Handles interactions with the main movement component.
    public abstract class MovementBehaviour : MonoBehaviour
    {
        #region UNITY

        // Awake.
        protected virtual void Awake(){}

        // Start.
        protected virtual void Start(){}

        // Update.
        protected virtual void Update(){}

        // Fixed Update.
        protected virtual void FixedUpdate(){}

        // Late Update.
        protected virtual void LateUpdate(){}

        #endregion

        #region GETTERS

        // Returns the last Time.time value at which the character jumped.
        public abstract float GetLastJumpTime();
        
        // Returns the value of MultiplierForward.
        // <returns></returns>
        public abstract float GetMultiplierForward();
        // Returns the value of MultiplierSideways.
        // <returns></returns>
        public abstract float GetMultiplierSideways();
        // Returns the value of MultiplierBackwards.
        // <returns></returns>
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

        #endregion

        #region METHODS

        // Calling this will make the character jump!
        public abstract void Jump();
        // Forces crouch/un-crouch!
        public abstract void Crouch(bool crouching);

        // Tries to crouch/un-crouch.
        public abstract void TryCrouch(bool value);
        
        // Tries to toggle the crouching state. This method should also make sure to handle any automatic
        // un-crouching that may happen when the character stops being under an object.
        public abstract void TryToggleCrouch();

        #endregion
    }
}
