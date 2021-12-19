﻿using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat.Models;

public class RefreshToken
{
	[JsonProperty("access_token")]
	public string AccessToken { get; set; }

	[JsonProperty("expires_in")]
	public int ExpiresIn { get; set; }

	[JsonProperty("scope")]
	public string Scope { get; set; }

	[JsonProperty("token_type")]
	public string TokenType { get; set; }

	public long Timestamp { get; set; }
}