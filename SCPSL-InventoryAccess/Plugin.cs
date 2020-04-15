using System;
using EXILED;

namespace SCPSL_InventoryAccess
{
    public class InventoryAccessPlugin : Plugin
    {

        private string _name = "InventoryAccess";
        private string _prefix = "[InvAcc] ";

        private EventHandlers _handlers;
        
        public override void OnEnable()
        {
            Log.Info(_prefix + "Enabling InventoryAccess");
            Log.Info(_prefix + "Registering event handlers");
            try
            {
                _handlers = new EventHandlers();
                Events.DoorInteractEvent += _handlers.OnPlayerDoorInteract;
                Events.LockerInteractEvent += _handlers.OnPlayerLockerInteract;
            }
            catch (Exception e)
            {
                Log.Error(_prefix + "Registering event handlers failed!");
            }
        }

        public override void OnDisable()
        {
            Log.Info(_prefix + "De-registering event handlers");
            Events.DoorInteractEvent -= _handlers.OnPlayerDoorInteract;
            Events.LockerInteractEvent -= _handlers.OnPlayerLockerInteract;
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