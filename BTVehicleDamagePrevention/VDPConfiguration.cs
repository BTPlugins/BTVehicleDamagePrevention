using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BTVehicleDamagePrevention
{
    public class VDPConfiguration : IRocketPluginConfiguration
    {
        public bool AllowVehicleDamage { get; set; }
        public string BypassPermission { get; set; }
        public bool DebugMode { get; set; }
        public void LoadDefaults()
        {
            AllowVehicleDamage = false; // False - Cannot Damage | True - Can Damage, No need for Bypass Perm
            BypassPermission = "Allow.VehicleDamage";
            DebugMode = false;
        }
    }
}
