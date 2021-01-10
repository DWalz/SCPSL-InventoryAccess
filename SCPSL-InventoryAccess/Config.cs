using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCPSL_InventoryAccess
{
    public class Config : IConfig
    {
        [Description("Indicates whether the plugin is enabled or not")]
        public bool IsEnabled { get; set; } = true;
    }
}
