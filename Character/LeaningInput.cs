using UnityEngine;
using UnityEngine.InputSystem;

namespace InfimaGames.LowPolyShooterPack
{
    // LeaningInput. This script handles all leaning input, and makes sure to let the character's animator know about it.
    public class LeaningInput : MonoBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "References")]
        
        [Tooltip("The character's CharacterBehaviour component.")]
        [SerializeField, NotNull]
        private CharacterBehaviour characterBehaviour;
        
        [Tooltip("The character's Animator component.")]
        [SerializeField, NotNull]
        private Animator characterAnimator;

        #endregion
        
        #region FIELDS
        
        // Current Leaning Value.
        private float leaningInput;
        // True If Leaning.
        private bool isLeaning;

        #endregion
        
        #region METHODS
        
        // Update.
        private void Update()
        {
            //Update isLeaning.
            isLeaning = (leaningInput != 0.0f);
            
            //Update AHashes.LeaningInput Float.
            characterAnimator.SetFloat(AHashes.LeaningInput, leaningInput);
            //Update AHashes.Leaning Bool.
            characterAnimator.SetBool(AHashes.Leaning, isLeaning);
        }

        // Lean.
        public void Lean(InputAction.CallbackContext context)
        {
            //Block while the cursor is unlocked.
            if (!characterBehaviour.IsCursorLocked())
            {
                //Zero out the leaning.
                leaningInput = 0.0f;
                
                //Return.
                return;
            }

            //ReadValue.
            leaningInput = context.ReadValue<float>();
        }
        
        #endregion
    }
}
