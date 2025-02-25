﻿namespace NETworkManager.Models.Network;

/// <summary>
/// Class contains the options for the traceroute.
/// </summary>
public class TracerouteOptions
{
    /// <summary>
    /// Timeout in milliseconds after which a ping is considered lost.
    /// </summary>
    public int Timeout { get; }

    /// <summary>
    /// Size of the buffer used in the ping in bytes.    
    /// </summary>
    public byte[] Buffer { get; }

    /// <summary>
    /// Maximum number of hops between the local and remote computer.
    /// </summary>
    public int MaximumHops { get; }
    
    /// <summary>
    /// Do not fragment the ping packet.    
    /// </summary>
    public bool DontFragment { get; }

    /// <summary>
    /// Resolve the hostname for an IP address.
    /// </summary>
    public bool ResolveHostname { get; }
    
    /// <summary>
    /// Check IP geolocation via ip-api.com.
    /// </summary>
    public bool CheckIPApiIPGeolocation { get; }
    
    /// <summary>
    /// Create an instance of <see cref="TracerouteOptions"/> with parameters.
    /// </summary>
    /// <param name="timeout">Timeout in milliseconds after which a ping is considered lost.</param>
    /// <param name="buffer">Size of the buffer used in the ping in bytes.</param>
    /// <param name="maximumHops">Maximum number of hops between the local and remote computer.</param>
    /// <param name="dontFragment">Do not fragment the ping packet.</param>
    /// <param name="resolveHostname">Resolve the hostname for an IP address.</param>
    /// <param name="checkIPApiIPGeolocation">Check IP geolocation via ip-api.com.</param>
    public TracerouteOptions(int timeout, byte[] buffer, int maximumHops, bool dontFragment, bool resolveHostname, bool checkIPApiIPGeolocation)
    {
        Timeout = timeout;
        Buffer = buffer;
        MaximumHops = maximumHops;
        DontFragment = dontFragment;
        ResolveHostname = resolveHostname;
        CheckIPApiIPGeolocation = checkIPApiIPGeolocation;
    }
}
