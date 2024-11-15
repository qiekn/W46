using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Makes an object face the main camera.
    public class FaceCamera : MonoBehaviour
    {
        #region FIELDS

        // Main Camera Transform.
        private Transform cameraTransform;

        #endregion
        
        #region UNITY

        private void Start()
        {
            //Cache Camera Transform.
            if (Camera.main != null) 
                cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            //Look At.
            transform.LookAt(cameraTransform, Vector3.up);
        }

        #endregion
    }
}
