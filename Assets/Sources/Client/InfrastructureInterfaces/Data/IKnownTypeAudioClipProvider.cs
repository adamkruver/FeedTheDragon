using System;
using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Data
{
    public interface IKnownTypeAudioClipProvider
    {
        AudioClip Provide(Type type);
    }
}