using Rocket.API;
using Rocket.Core.Plugins;
using System;
using Logger = Rocket.Core.Logging.Logger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using BTVehicleDamagePrevention.Helpers;

namespace BTVehicleDamagePrevention
{
    public partial class VDPPlugin : RocketPlugin<VDPConfiguration>
    {
        public static VDPPlugin Instance;
        protected override void Load()
        {
            Instance = this;
            Logger.Log("#############################################", ConsoleColor.Yellow);
            Logger.Log("###              BT-VDP Loaded            ###", ConsoleColor.Yellow);
            Logger.Log("###   Plugin Created By blazethrower320   ###", ConsoleColor.Yellow);
            Logger.Log("###            Join my Discord:           ###", ConsoleColor.Yellow);
            Logger.Log("###     https://discord.gg/YsaXwBSTSm     ###", ConsoleColor.Yellow);
            Logger.Log("#############################################", ConsoleColor.Yellow);
            VehicleManager.onDamageVehicleRequested += onDamageVehicleRequested;
        }

        private void onDamageVehicleRequested(CSteamID instigatorSteamID, InteractableVehicle vehicle, ref ushort pendingTotalDamage, ref bool canRepair, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            var player = UnturnedPlayer.FromCSteamID(instigatorSteamID);
            if(player == null)
            {
                SendDebugMessage("Player Null ( Zombie Damaged Vehicle ) ");
                return;
            }
            if(VDPPlugin.Instance.Configuration.Instance.AllowVehicleDamage)
            {
                SendDebugMessage(player.CharacterName + " tried Damaging a Vehicle, However, AllowVehicleDamage Configuration Option set to True - Allowing Vehicle Damaging");
                shouldAllow = true;
                return;
            }
            if(!player.HasPermission(VDPPlugin.Instance.Configuration.Instance.BypassPermission) && VDPPlugin.Instance.Configuration.Instance.AllowVehicleDamage == false)
            {
                shouldAllow = false;
                TranslationHelper.SendMessageTranslation(player.CSteamID, "VehicleDamagePrevention", VDPPlugin.Instance.Configuration.Instance.BypassPermission);
                SendDebugMessage(player.CharacterName + " has damaged a Vehicle, However does not have [ " + VDPPlugin.Instance.Configuration.Instance.BypassPermission + " ] Bypass Permission. Canceling!");
                return;
            }
            shouldAllow = true;
            SendDebugMessage(player.CharacterName + " has damaged a Vehicle! Player has Permission: " + VDPPlugin.Instance.Configuration.Instance.BypassPermission);
        }

        protected override void Unload()
        {
            VehicleManager.onDamageVehicleRequested -= onDamageVehicleRequested;
            Logger.Log("BT-VehicleDamagePrevention Unloaded");
        }
        public void SendDebugMessage(string message)
        {
            if (VDPPlugin.Instance.Configuration.Instance.DebugMode)
            {
                Logger.Log("DEBUG >> " + message);
            }
        }
    }
}
