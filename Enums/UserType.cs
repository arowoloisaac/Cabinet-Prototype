﻿using System.Text.Json.Serialization;

namespace Cabinet_Prototype.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        Student,
        Teeacher
    }
}