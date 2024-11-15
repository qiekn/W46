using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    // Interface Element.
    public abstract class Element : MonoBehaviour
    {
        #region FIELDS
        
        // Game Mode Service.
        protected IGameModeService gameModeService;
        
        // Player Character.
        protected CharacterBehaviour characterBehaviour;
        // Player Character Inventory.
        protected InventoryBehaviour inventoryBehaviour;

        // Equipped Weapon.
        protected WeaponBehaviour equippedWeaponBehaviour;
        
        #endregion

        #region UNITY

        // Awake.
        protected virtual void Awake()
        {
            //Get Game Mode Service. Very useful to get Game Mode references.
            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            
            //Get Player Character.
            characterBehaviour = gameModeService.GetPlayerCharacter();
            //Get Player Character Inventory.
            inventoryBehaviour = characterBehaviour.GetInventory();
        }
        
        // Update.
        private void Update()
        {
            //Ignore if we don't have an Inventory.
            if (Equals(inventoryBehaviour, null))
                return;

            //Get Equipped Weapon.
            equippedWeaponBehaviour = inventoryBehaviour.GetEquipped();
            
            //Tick.
            Tick();
        }

        #endregion

        #region METHODS

        // Tick.
        protected virtual void Tick() {}

        #endregion
    }
}
