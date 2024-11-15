using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // ItemAnimationDataBehaviour. Used as an abstract class to contain all definitions for the Recoil class.
    public abstract class ItemAnimationDataBehaviour : MonoBehaviour
    {
        #region GETTERS
        
        // This function should return the RecoilData used for the camera.
        public abstract RecoilData GetCameraRecoilData();
        // This function should return the RecoilData used for the weapon.
        public abstract RecoilData GetWeaponRecoilData();
        // Returns a RecoilData value according to the passed MotionType.
        // <returns></returns>
        public abstract RecoilData GetRecoilData(MotionType motionType);

        // Return all the data needed to set the lowered pose of a weapon.
        public abstract LowerData GetLowerData();
        // Returns the LeaningData needed to apply to the equipped weapon while the character is leaning.
        public abstract LeaningData GetLeaningData();
        
        // Returns the ItemOffsets object needed to apply proper offsets to all items.
        public abstract ItemOffsets GetItemOffsets();


        #endregion
    }
}
