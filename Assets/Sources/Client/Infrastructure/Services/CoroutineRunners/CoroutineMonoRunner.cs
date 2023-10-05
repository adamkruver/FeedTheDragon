﻿using System;
using System.Collections;
using Sources.Client.PresentationInterfaces.Coroutines;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.CoroutineRunners
{
    public class CoroutineMonoRunner
    {
        private readonly ICoroutineMonoBehaviour _coroutineMonoBehaviour;

        private Coroutine _routine;

        public CoroutineMonoRunner(ICoroutineMonoBehaviour coroutineMonoBehaviour)
        {
            _coroutineMonoBehaviour = coroutineMonoBehaviour;
        }

        public void Run(IEnumerator coroutine)
        {
            Stop();

            if (_coroutineMonoBehaviour == null)
                throw new NullReferenceException(nameof(_coroutineMonoBehaviour));
            
            _routine = _coroutineMonoBehaviour.StartCoroutine(coroutine);
        }

        public void Stop()
        {
            if(_coroutineMonoBehaviour == null)
                return;
            
            if (_routine != null)
                _coroutineMonoBehaviour.StopCoroutine(_routine);
        }
    }
}