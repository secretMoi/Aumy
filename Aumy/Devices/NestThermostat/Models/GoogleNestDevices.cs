using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat.Models;

public class GoogleNestDevices
{
    public List<Device> Devices { get; set; }
    
    public class SdmDevicesTraitsInfo
    {
        [JsonPropertyName("customName")]
        public string CustomName { get; set; }
    }
    
    public class SdmDevicesTraitsHumidity
    {
        [JsonPropertyName("ambientHumidityPercent")]
        public int AmbientHumidityPercent { get; set; }
    }

    public class SdmDevicesTraitsConnectivity
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class SdmDevicesTraitsFan
    {
    }

    public class SdmDevicesTraitsThermostatMode
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("availableModes")]
        public List<string> AvailableModes { get; set; }
    }

    public class SdmDevicesTraitsThermostatEco
    {
        [JsonPropertyName("availableModes")]
        public List<string> AvailableModes { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("heatCelsius")]
        public double HeatCelsius { get; set; }

        [JsonPropertyName("coolCelsius")]
        public double CoolCelsius { get; set; }
    }

    public class SdmDevicesTraitsThermostatHvac
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class SdmDevicesTraitsSettings
    {
        [JsonPropertyName("temperatureScale")]
        public string TemperatureScale { get; set; }
    }

    public class SdmDevicesTraitsThermostatTemperatureSetpoint
    {
        [JsonPropertyName("heatCelsius")]
        public double HeatCelsius { get; set; }
    }

    public class SdmDevicesTraitsTemperature
    {
        [JsonPropertyName("ambientTemperatureCelsius")]
        public double AmbientTemperatureCelsius { get; set; }
    }

    public class Traits
    {
        [JsonProperty("sdm.devices.traits.Info")]
        public SdmDevicesTraitsInfo SdmDevicesTraitsInfo { get; set; }

        [JsonProperty("sdm.devices.traits.Humidity")]
        public SdmDevicesTraitsHumidity SdmDevicesTraitsHumidity { get; set; }

        [JsonProperty("sdm.devices.traits.Connectivity")]
        public SdmDevicesTraitsConnectivity SdmDevicesTraitsConnectivity { get; set; }

        [JsonProperty("sdm.devices.traits.Fan")]
        public SdmDevicesTraitsFan SdmDevicesTraitsFan { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatMode")]
        public SdmDevicesTraitsThermostatMode SdmDevicesTraitsThermostatMode { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatEco")]
        public SdmDevicesTraitsThermostatEco SdmDevicesTraitsThermostatEco { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatHvac")]
        public SdmDevicesTraitsThermostatHvac SdmDevicesTraitsThermostatHvac { get; set; }

        [JsonProperty("sdm.devices.traits.Settings")]
        public SdmDevicesTraitsSettings SdmDevicesTraitsSettings { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatTemperatureSetpoint")]
        public SdmDevicesTraitsThermostatTemperatureSetpoint SdmDevicesTraitsThermostatTemperatureSetpoint { get; set; }

        [JsonProperty("sdm.devices.traits.Temperature")]
        public SdmDevicesTraitsTemperature SdmDevicesTraitsTemperature { get; set; }
    }

    public class ParentRelation
    {
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
    
    public class Device
    {
        public string Name { get; set; }
        
        public string Type { get; set; }
        
        public string Assignee { get; set; }
        
        [JsonPropertyName("traits")]
        public Traits Traits { get; set; }
        
        [JsonPropertyName("parentRelations")]
        public List<ParentRelation> ParentRelations { get; set; }
    }
}