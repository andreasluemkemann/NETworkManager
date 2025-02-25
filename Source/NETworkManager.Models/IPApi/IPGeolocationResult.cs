﻿namespace NETworkManager.Models.IPApi;


/// <summary>
/// Class contains the result of the IP geolocation information retrieval.
/// </summary>
public class IPGeolocationResult
{
    /// <summary>
    /// IP geolocation information retrieved from the API.
    /// </summary>
    public IPGeolocationInfo Info { get; set; } = new();

    /// <summary>
    /// Indicates if the rate limit of the API is reached.
    /// </summary>
    public bool RateLimitIsReached { get; set; }
    
    /// <summary>
    /// Remaining time in seconds until the rate limit window resets.
    /// </summary>
    public int RateLimitRemainingTime { get; set; }
    
    /// <summary>
    /// Indicates if the IP geolocation information retrieval has failed.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Error message if the IP geolocation information retrieval has failed.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Error code if the IP geolocation information retrieval has failed.
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="IPGeolocationResult"/>.
    /// </summary>
    public IPGeolocationResult()
    {
        
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="IPGeolocationResult"/> with the IP geolocation information.
    /// </summary>
    /// <param name="info">IP geolocation information retrieved from the API.</param>
    public IPGeolocationResult(IPGeolocationInfo info)
    {
        Info = info;
    }

    /// <summary>
    /// Create a new instance of <see cref="IPGeolocationResult"/> that indicates that the rate limit 
    /// of the API is reached.
    /// </summary>
    /// <param name="isRateLimitReached">Indicates if the rate limit of the API is reached.</param>
    /// <param name="rateLimitRemainingTime">Remaining time in seconds until the rate limit window resets.</param>
    public IPGeolocationResult(bool isRateLimitReached, int rateLimitRemainingTime)
    {
        RateLimitIsReached = isRateLimitReached;
        RateLimitRemainingTime = rateLimitRemainingTime;
    }

    /// <summary>
    /// Create a new instance of <see cref="IPGeolocationResult"/> with an error message and an error code.
    /// </summary>
    /// <param name="hasError">Indicates if the IP geolocation information retrieval has failed.</param>
    /// <param name="errorMessage">Error message if the IP geolocation information retrieval has failed.</param>
    /// <param name="errorCode">Error code if the IP geolocation information retrieval has failed (or -1 if exception was thrown).</param>
    public IPGeolocationResult(bool hasError, string errorMessage, int errorCode)
    {
        HasError = hasError;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }
}
