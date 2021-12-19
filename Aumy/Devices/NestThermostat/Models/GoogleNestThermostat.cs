using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat.Models;

public class GoogleNestThermostat
{
    public List<Device> Devices { get; set; }
    
    public class Info
    {
        [JsonPropertyName("customName")]
        public string CustomName { get; set; }
    }
    
    public class Humidity
    {
        [JsonPropertyName("ambientHumidityPercent")]
        public int AmbientHumidityPercent { get; set; }
    }

    public class Connectivity
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Fan
    {
    }

    public class ThermostatMode
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("availableModes")]
        public List<string> AvailableModes { get; set; }
    }

    public class ThermostatEco
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

    public class ThermostatHvac
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Settings
    {
        [JsonPropertyName("temperatureScale")]
        public string TemperatureScale { get; set; }
    }

    public class ThermostatTemperatureSetpoint
    {
        [JsonPropertyName("heatCelsius")]
        public double HeatCelsius { get; set; }
    }

    public class Temperature
    {
        [JsonPropertyName("ambientTemperatureCelsius")]
        public double AmbientTemperatureCelsius { get; set; }
    }

    public class Traits
    {
        [JsonProperty("sdm.devices.traits.Info")]
        public Info Info { get; set; }

        [JsonProperty("sdm.devices.traits.Humidity")]
        public Humidity Humidity { get; set; }

        [JsonProperty("sdm.devices.traits.Connectivity")]
        public Connectivity Connectivity { get; set; }

        [JsonProperty("sdm.devices.traits.Fan")]
        public Fan Fan { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatMode")]
        public ThermostatMode ThermostatMode { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatEco")]
        public ThermostatEco ThermostatEco { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatHvac")]
        public ThermostatHvac ThermostatHvac { get; set; }

        [JsonProperty("sdm.devices.traits.Settings")]
        public Settings Settings { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatTemperatureSetpoint")]
        public ThermostatTemperatureSetpoint ThermostatTemperatureSetpoint { get; set; }

        [JsonProperty("sdm.devices.traits.Temperature")]
        public Temperature Temperature { get; set; }
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