using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Interactable.
    public abstract class Interactable : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        //TODO
        [SerializeField]
        protected string interactionText;
        
        #endregion
        
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
        
        #region METHODS
        
        // Called to interact with this object.
        // <param name="actor">The actor starting the interaction.</param>
        public abstract void Interact(GameObject actor = null);
        
        #endregion

        #region GETTERS

        //TODO
        public virtual string GetInteractionText() => interactionText;

        #endregion
    }
}
