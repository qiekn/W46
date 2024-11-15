using TMPro;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    // Text Interface Element.
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class ElementText : Element
    {
        #region FIELDS

        // Text Mesh.
        protected TextMeshProUGUI textMesh;

        #endregion

        #region UNITY

        protected override void Awake()
        {
            //Base.
            base.Awake();

            //Get Text Mesh.
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        #endregion
    }
}
