using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    // Player Interface.
    public class CanvasSpawner : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Canvas prefab spawned at start. Displays the player's user interface.")]
        [SerializeField]
        private GameObject canvasPrefab;
        
        [Tooltip("Quality settings menu prefab spawned at start. Used for switching between different quality settings in-game.")]
        [SerializeField]
        private GameObject qualitySettingsPrefab;

        #endregion

        #region UNITY

        // Awake.
        private void Awake()
        {
            //Spawn Interface.
            Instantiate(canvasPrefab);
            //Spawn Quality Settings Menu.
            Instantiate(qualitySettingsPrefab);
        }

        #endregion
    }
}
