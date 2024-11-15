using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public class CopyTransform : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Tooltip("Transform to copy from.")]
        [SerializeField]
        private Transform copy;

        #endregion

        #region FIELDS

        // Local Transform.
        private Transform local;

        #endregion

        #region UNITY FUNCTIONS

        // Awake.
        private void Awake()
        {
            //Cache local.
            local = transform;
        }

        // Update.
        private void Update()
        {
            //Copy position.
            local.position = copy.position;
            //Copy rotation.
            local.rotation = copy.rotation;
            //Copy scale.
            local.localScale = copy.localScale;
        }

        #endregion
    }
}
