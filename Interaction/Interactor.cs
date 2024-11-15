using UnityEngine;
using UnityEngine.InputSystem;

namespace InfimaGames.LowPolyShooterPack
{
    // Interactor.
    public class Interactor : InteractorBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("References")]
        
        [Tooltip("Used to determine where to trace the interaction from, and what direction it should go in.")]
        [SerializeField]
        private Transform interactor;

        [Header("Settings")]

        [Tooltip("Mask used to trace for interactions.")]
        [SerializeField]
        private LayerMask mask;

        [Tooltip("Radius of the trace.")]
        [SerializeField]
        private float radius = 1.0f;
        
        [Tooltip("Maximum interaction distance.")]
        [SerializeField]
        private float maxDistance = 5.0f;

        #endregion

        #region FIELDS

        // Main Hit Result.
        private RaycastHit hitResult;
		// Interactable.
        private Interactable interactable;

        #endregion

        #region UNITY

        // Update.
        protected override void Update()
        {
	        //Interaction Trace.
	        if (Physics.SphereCast(interactor.position, radius,
		            interactor.forward, out hitResult, maxDistance, mask))
	        {
		        //If we hit a collider.
		        if (hitResult.collider != null)
		        {
			        //Try to get the interactable.
			        interactable = hitResult.collider.GetComponent<Interactable>();
		        }
		        else
			        interactable = null;
	        }
	        else
		        interactable = null;
        }

        #endregion

        #region INPUT

		// Interact.
	    // ReSharper disable once UnusedMember.Global
	    public void TryInteract(InputAction.CallbackContext context)
		{
			//Switch.
			switch (context)
			{
				//Performed.
				case {phase: InputActionPhase.Performed}:
					//Make sure we can interact before continuing.
					if (CanInteract() == false)
						return;
					
					//Try Interact.
					if (interactable != null)
						interactable.Interact(gameObject);
					break;
			}
		}

        #endregion

        #region GETTERS
        
        // Can Interact
        public override bool CanInteract()
        {
	        //TODO: Add this.
	        //Block while the cursor is unlocked.
	        // if (!cursorLocked)
	        //  return;
	        
	        //Return.
	        return true;
        }

        // Get Hit Result.
        // <returns></returns>
        public override RaycastHit GetHitResult() => hitResult;
		// Get Interactable.
        public override Interactable GetInteractable() => interactable;

        #endregion
    }
}
