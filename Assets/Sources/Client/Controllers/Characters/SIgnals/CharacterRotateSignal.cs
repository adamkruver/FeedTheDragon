﻿using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.SIgnals
{
    public class CharacterRotateSignal : ISignal
    {
        public CharacterRotateSignal(Vector3 lookDirection)
        {
            LookDirection = lookDirection;
        }

        public Vector3 LookDirection { get; }
    }
}