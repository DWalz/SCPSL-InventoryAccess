using System;
using Exiled.API.Features;
using HarmonyLib;
using Player = Exiled.Events.Handlers.Player;

namespace SCPSL_InventoryAccess
{
    public class InventoryAccessPlugin : Plugin<Config>
    {
        private EventHandlers _handlers;
        private static int _harmonyCounter;

        private static Harmony HarmonyInstance { get; set; }

        public override void OnEnabled()
        {
            Log.Info("Enabling InventoryAccess");
            Log.Info("Registering event handlers");
            try
            {
                _handlers = new EventHandlers();
                Player.InteractingDoor += _handlers.OnPlayerDoorInteract;
                Player.InteractingLocker += _handlers.OnPlayerLockerInteract;
                Player.UnlockingGenerator += _handlers.OnGeneratorAccess;
                Player.ActivatingWarheadPanel += _handlers.OnActivatingWarheadPanel;

                HarmonyInstance = new Harmony($"{Name}{_harmonyCounter++}");
                HarmonyInstance.PatchAll();
            }
            catch (Exception)
            {
                Log.Error("Registering event handlers failed!");
            }
        }

        public override void OnDisabled()
        {
            Log.Info("De-registering event handlers");
            Player.InteractingDoor -= _handlers.OnPlayerDoorInteract;
            Player.InteractingLocker -= _handlers.OnPlayerLockerInteract;
            Player.UnlockingGenerator -= _handlers.OnGeneratorAccess;
            Player.ActivatingWarheadPanel -= _handlers.OnActivatingWarheadPanel;

            if (HarmonyInstance != null || HarmonyInstance != default)
                HarmonyInstance.UnpatchAll();

            _handlers = null;
        }

        public override string Name { get; } = "InventoryAccess";
    }
}