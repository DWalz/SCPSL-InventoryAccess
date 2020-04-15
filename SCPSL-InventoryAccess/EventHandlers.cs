using System;
using EXILED;
using EXILED.Extensions;

namespace SCPSL_InventoryAccess
{
    public class EventHandlers
    {
        public void OnPlayerDoorInteract(ref DoorInteractionEvent ev)
        {
            ev.Allow = hasPermission(ev.Player, ev.Door.permissionLevel);
        }

        public void OnPlayerLockerInteract(LockerInteractionEvent ev)
        {
            ev.Allow = hasPermission(ev.Player, ev.Locker.chambers[0].accessToken);
        }

        private bool hasPermission(ReferenceHub player, String requested)
        {
            if (requested == "")
            {
                return true;
            }
            foreach (Inventory.SyncItemInfo itemInfo in player.GetAllItems())
            {
                if (player.inventory.GetItemByID(itemInfo.id).permissions.Contains(requested))
                {
                    return true;
                }
            }

            return false;
        }
    }
}