﻿using System.Text.Json.Serialization;

namespace DeckIQ.Core.Responses;

public class Response<TData>
{
    
    private readonly int _code ;

    [JsonConstructor]
    public Response()
    {
        _code = Configuration.DefoultStatusCode;
        
    }

    public Response(
        TData data, 
        int code = Configuration.DefoultStatusCode, 
        string? message = null)
    {
        Data = data;
        Message= message;
        _code = code;
    }
    public TData? Data { get; set; }
    public string? Message { get; set; }
    
    [JsonIgnore]
    public bool IsSuccess =>
        _code is >= 200 and <= 299;
    
}