using UnityEngine;
using UnityEngine.InputSystem;

namespace InfimaGames.LowPolyShooterPack
{
    // Crouching Input.
    public class CrouchingInput : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "References")]

        [Tooltip("The character's CharacterBehaviour component.")]
        [SerializeField, NotNull]
        private CharacterBehaviour characterBehaviour;
        
        [Tooltip("The character's MovementBehaviour component.")]
        [SerializeField, NotNull]
        private MovementBehaviour movementBehaviour;

        [Title(label: "Settings")]

        [Tooltip("If true, the crouch button has to be held to keep crouching.")]
        [SerializeField]
        private bool holdToCrouch;
        
        #endregion

        #region FIELDS

        // holding. If true, the player is holding the crouching button.
        private bool holding;

        #endregion

        #region UNITY

        // Update.
        private void Update()
        {
            //Change the crouching state based on whether we're holding if we need to.
            //We only do this for hold-crouch, otherwise we don't even bother with this.
            if(holdToCrouch)
                movementBehaviour.TryCrouch(holding);
        }

        #endregion
        
        #region INPUT

        // Crouch. Calling this from the new Unity Input component will directly make the character
        // crouch/un-crouch depending on its state.
        // Keep in mind that this method is called from an input event, so it doesn't have any direct references.
        public void Crouch(InputAction.CallbackContext context)
        {
            //Check that all our references are correctly assigned.
            if (characterBehaviour == null || movementBehaviour == null)
            {
                //ReferenceError.
                Log.ReferenceError(this, this.gameObject);

                //Return.
                return;
            }
            
            //Block while the cursor is unlocked.
            if (!characterBehaviour.IsCursorLocked())
                return;

            //Switch.
            switch (context.phase)
            {
                //Started.
                case InputActionPhase.Started:
                    holding = true;
                    break;
                //Performed.
                case InputActionPhase.Performed:
                    //TryToggleCrouch.
                    if(!holdToCrouch)
                        movementBehaviour.TryToggleCrouch();
                    break;
                //Canceled.
                case InputActionPhase.Canceled:
                    holding = false;
                    break;
            }
        }

        #endregion
    }
}
