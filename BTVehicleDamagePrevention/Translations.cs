using Rocket.API.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTVehicleDamagePrevention
{
    public partial class VDPPlugin
    {
        public override TranslationList DefaultTranslations => new TranslationList
        {
            {
                "VehicleDamagePrevention", "[color=#FF0000]{{BTPlugins}} [/color][color=#F3F3F3]Unable to Damage Vehicles! Missing Permission[/color] [color=#3E65FF]{0}[/color]"
            },

        };
    }
}