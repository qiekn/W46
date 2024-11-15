using UnityEngine;
using UnityEngine.InputSystem;

namespace InfimaGames.LowPolyShooterPack
{
    // LaserToggleInput. This class is called from the PlayerInput component, and tries to toggle the equipped weapon's flashlight/laser
    // on/off.
    public class LaserToggleInput : MonoBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "References")]

        [Tooltip("The character's Animator component.")]
        [SerializeField, NotNull]
        private Animator animator;
        
        [Tooltip("The character's InventoryBehaviour component.")]
        [SerializeField, NotNull]
        private InventoryBehaviour inventoryBehaviour;

        #endregion
        
        #region FIELDS
        
        // Equipped Laser.
        private LaserBehaviour laserBehaviour;

        // Last Frame's Aiming Value.
        private bool wasAiming;
        // Last Frame's Running Value.
        private bool wasRunning;
        
        #endregion
        
        #region METHODS

        // Update.
        private void Update()
        {
            //Check References.
            if (animator == null || inventoryBehaviour == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, gameObject);
                
                //Return.
                return;
            }
            
            //Grab a reference to the currently equipped weapon.
            WeaponBehaviour weaponBehaviour = inventoryBehaviour.GetEquipped();
            if (weaponBehaviour == null)
                return;

            //Get a reference to the equipped weapon's laser component, if it has one.
            laserBehaviour = weaponBehaviour.GetAttachmentManager().GetEquippedLaser();
            if (laserBehaviour == null)
                return;
            
            //Get current aiming value.
            bool aiming = animator.GetBool(AHashes.Aim);
            //Get the current running value.
            bool running = animator.GetBool(AHashes.Running);
            
            //If the character just started aiming.
            if (aiming && !wasAiming)
            {
                //Hide the laser if needed.
                if(laserBehaviour.GetTurnOffWhileAiming())
                    laserBehaviour.Hide();
            }
            //If the character just stopped aiming.
            else if (!aiming && wasAiming)
            {
                //Reapply the laser if needed.
                if(laserBehaviour.GetTurnOffWhileAiming())
                    laserBehaviour.Reapply();
            }

            //If the character just started running.
            if (running && !wasRunning)
            {
                //Hide the laser if needed.
                if (laserBehaviour.GetTurnOffWhileRunning())
                    laserBehaviour.Hide();
            }
            //If the character just stopped running.
            else if (!running && wasRunning)
            {
                //Reapply the laser if needed.
                if (laserBehaviour.GetTurnOffWhileRunning())
                    laserBehaviour.Reapply();
            }

            //Save Aiming Value.
            wasAiming = aiming;
            //Save Running Value.
            wasRunning = running;
        }

        // Input.
        public void Input(InputAction.CallbackContext context)
        {
            //Switch.
            switch (context)
            {
                //Performed.
                case {phase: InputActionPhase.Performed}:
                    //Toggle.
                    Toggle();
                    break;
            }
        }

        // Toggle.
        private void Toggle()
        {
			//Check Reference.
			if(laserBehaviour == null)
				return;
			
            //Call Toggle on the LaserBehaviour component.
            laserBehaviour.Toggle();
        }
        
        #endregion
    }
}
