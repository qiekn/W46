using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Interactor Behaviour.
    public abstract class InteractorBehaviour : MonoBehaviour
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

        // Returns true if an interaction is viable.
        public abstract bool CanInteract();

        // Returns the result of the interaction trace.
        public abstract RaycastHit GetHitResult();
        // Returns the current interactable found by the trace.
        public abstract Interactable GetInteractable();

        #endregion
    }
}
