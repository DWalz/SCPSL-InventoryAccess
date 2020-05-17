using System;
using System.Collections.Generic;
using EXILED;
using EXILED.Extensions;

namespace SCPSL_InventoryAccess
{
    public class EventHandlers
    {
        
        private static List<RoleType> SCP_ROLES = new List<RoleType>
        {
            RoleType.Scp049, RoleType.Scp079, RoleType.Scp096, RoleType.Scp106, 
            RoleType.Scp173, RoleType.Scp0492, RoleType.Scp93953, RoleType.Scp93989
        };

        public void OnPlayerDoorInteract(ref DoorInteractionEvent ev)
        {
            if (SCP_ROLES.Contains(ev.Player.GetRole())) return;
            if (ev.Allow) return;
            ev.Allow = hasPermission(ev.Player, ev.Door.permissionLevel);
        }

        public void OnPlayerLockerInteract(LockerInteractionEvent ev)
        {
            if (SCP_ROLES.Contains(ev.Player.GetRole())) return;
            if (ev.Allow) return;
            ev.Allow = hasPermission(ev.Player, ev.Locker.chambers[0].accessToken);
        }

        public void OnGeneratorAccess(ref GeneratorUnlockEvent ev)
        {
            if (SCP_ROLES.Contains(ev.Player.GetRole())) return;
            if (ev.Allow) return;
            ev.Allow = hasPermission(ev.Player, "ARMORY_LVL_2");
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