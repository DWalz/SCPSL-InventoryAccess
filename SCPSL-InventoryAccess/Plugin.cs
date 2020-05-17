using System;
using EXILED;

namespace SCPSL_InventoryAccess
{
    public class InventoryAccessPlugin : Plugin
    {

        private string _name = "InventoryAccess";

        private EventHandlers _handlers;
        
        public override void OnEnable()
        {
            Log.Info("Enabling InventoryAccess");
            Log.Info("Registering event handlers");
            try
            {
                _handlers = new EventHandlers();
                Events.DoorInteractEvent += _handlers.OnPlayerDoorInteract;
                Events.LockerInteractEvent += _handlers.OnPlayerLockerInteract;
                Events.GeneratorUnlockEvent += _handlers.OnGeneratorAccess;
            }
            catch (Exception)
            {
                Log.Error("Registering event handlers failed!");
            }
        }

        public override void OnDisable()
        {
            Log.Info("De-registering event handlers");
            Events.DoorInteractEvent -= _handlers.OnPlayerDoorInteract;
            Events.LockerInteractEvent -= _handlers.OnPlayerLockerInteract;
            Events.GeneratorUnlockEvent -= _handlers.OnGeneratorAccess;
            _handlers = null;
        }

        public override void OnReload()
        {
            OnDisable();
            OnEnable();
        }

        public override string getName { get => _name; }
    }
    
    
}