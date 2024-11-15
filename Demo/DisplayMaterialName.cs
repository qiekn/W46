using TMPro;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Displays a material's name in the world.
    public class DisplayMaterialName : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Mesh.")]
        [SerializeField]
        private Renderer mesh;

        [Tooltip("Text.")]
        [SerializeField]
        private TextMeshProUGUI materialText;

        #endregion

        #region FIELDS

        // Material.
        private Material meshMaterial;

        #endregion

        #region UNITY

        private void Start()
        {
            //Get current material name from the mesh.
            string sharedMaterialName = mesh.sharedMaterial.name;
            //Output current material name to the UI text.
            materialText.text = sharedMaterialName;
        }

        #endregion
    }
}
