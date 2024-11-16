using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Weapon Scope.
    public class Scope : ScopeBehaviour {
        #region FIELDS SERIALIZED
    
        [Title(label: "Multipliers")]
        
        [Tooltip("Amount to multiply the mouse sensitivity by while aiming through this scope.")]
        [SerializeField]
        private float multiplierMouseSensitivity = 0.8f;
        
        [Tooltip("Value multiplied by the weapon's spread while aiming through this scope.")]
        [SerializeField]
        private float multiplierSpread = 0.1f;

        [Title(label: "Interface")]

        [Tooltip("Interface Sprite.")]
        [SerializeField]
        private Sprite sprite;

        [Title(label: "Sway")]

        [Tooltip("The value to multiply the weapon sway by while aiming through this scope.")]
        [SerializeField]
        private float swayMultiplier = 1.0f;
        
        [Title(label: "Aiming Offset")]
        
        [Tooltip("Weapon bone location offset while aiming.")]
        [SerializeField]
        private Vector3 offsetAimingLocation;
        
        [Tooltip("Weapon bone rotation offset while aiming.")]
        [SerializeField]
        private Vector3 offsetAimingRotation;
        
        [Title(label: "Field Of View")]

        [Tooltip("Field Of View Multiplier Aim.")]
        [SerializeField]
        private float fieldOfViewMultiplierAim = 0.9f;
        
        [Tooltip("Field Of View Multiplier Aim Weapon.")]
        [SerializeField]
        private float fieldOfViewMultiplierAimWeapon = 0.7f;

        [Title(label: "Materials")]

        [Tooltip("The index of the scope material that gets hidden when we don't aim.")]
        [SerializeField]
        private int materialIndex = 3;

        [Tooltip("Material to block the scope while not aiming through it.")]
        [SerializeField]
        private Material materialHidden;
        
        #endregion

        #region FIELDS

        // Mesh Renderer.
        private MeshRenderer meshRenderer;
        // Default scope material. We store it so we can re-apply it at any time, since it is
        // usually changed at runtime.
        private Material materialDefault;

        #endregion

        #region UNITY

        private void Awake() {
            //Cache Scope Renderer.
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            
            if (!HasMaterialIndex())
                return;
            
            //Cache default material.
            materialDefault = meshRenderer.materials[materialIndex];
        }

        private void Start() {
            OnAimStop();
        }

        #endregion

        #region GETTERS

        public override float GetFieldOfViewMultiplierAim() => fieldOfViewMultiplierAim;
        public override float GetFieldOfViewMultiplierAimWeapon() => fieldOfViewMultiplierAimWeapon;

        public override float GetMultiplierMouseSensitivity() => multiplierMouseSensitivity;
        public override float GetMultiplierSpread() => multiplierSpread;

        public override Vector3 GetOffsetAimingLocation() => offsetAimingLocation;
        public override Vector3 GetOffsetAimingRotation() => offsetAimingRotation;


        public override Sprite GetSprite() => sprite;
        public override float GetSwayMultiplier() => swayMultiplier;

        #endregion

        #region METHODS

        // OnAim() 和 OnAimStop() 负责控制瞄准镜的材质切换，改变瞄准镜外观
        public override void OnAim() {
            //Make sure that the index can exist.
            if (!HasMaterialIndex())
                return;

            //Get Materials.
            Material[] materials = meshRenderer.materials;
            //Restore to default material.
            materials[materialIndex] = materialDefault;
            //Update Materials.
            meshRenderer.materials = materials;
        }

        public override void OnAimStop() {
            if (!HasMaterialIndex())
                return;
            
            //Get Materials.
            Material[] materials = meshRenderer.materials;
            //Hide.
            materials[materialIndex] = materialHidden;
            //Update Materials.
            meshRenderer.materials = materials;
        }

        // Returns true if the Scope's Mesh Renderer could have this Material index.
        private bool HasMaterialIndex() {
            if (meshRenderer == null)
                return false;
            //Make sure that the index can exist.
            return materialIndex >= 0 && materialIndex < meshRenderer.materials.Length;
        }

        #endregion
    }
}
