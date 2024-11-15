using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // ItemAnimationData. Stores all information related to the weapon-specific procedural data.
    public class ItemAnimationData : ItemAnimationDataBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Item Offsets")]

        [Tooltip("The object that contains all offset data for this item.")]
        [SerializeField, InLineEditor]
        private ItemOffsets itemOffsets;
        
        [Title(label: "Lowered Data")]

        [Tooltip("This object contains all the data needed for us to set the lowered pose of this weapon.")]
        [SerializeField, InLineEditor]
        private LowerData lowerData;

        [Title(label: "Leaning Data")]

        [Tooltip("LeaningData. Contains all the information on what this weapon should do while the character is leaning.")]
        [SerializeField, InLineEditor]
        private LeaningData leaningData;
        
        [Title(label: "Camera Recoil Data")]

        [Tooltip("Weapon Recoil Data Asset. Used to get some camera recoil values, usually for weapons.")]
        [SerializeField, InLineEditor]
        private RecoilData cameraRecoilData;
        
        [Title(label: "Weapon Recoil Data")]

        [Tooltip("Weapon Recoil Data Asset. Used to get some recoil values, usually for weapons.")]
        [SerializeField, InLineEditor]
        private RecoilData weaponRecoilData;

        #endregion
        
        #region GETTERS

        // GetCameraRecoilData.
        public override RecoilData GetCameraRecoilData() => cameraRecoilData;
        // GetWeaponRecoilData.
        public override RecoilData GetWeaponRecoilData() => weaponRecoilData;

        // GetRecoilData.
        public override RecoilData GetRecoilData(MotionType motionType) =>
            motionType == MotionType.Item ? GetWeaponRecoilData() : GetCameraRecoilData();

        // GetLowerData.
        public override LowerData GetLowerData() => lowerData;
        // GetLeaningData.
        public override LeaningData GetLeaningData() => leaningData;
        
        // GetItemOffsets.
        public override ItemOffsets GetItemOffsets() => itemOffsets;

        #endregion
    }   
}
