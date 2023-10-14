using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.Components
{
    public class FirstContactComponent : IComponent
    {
        private readonly MutableLiveData<Type> _lastKnownType = new MutableLiveData<Type>();
        private readonly List<Type> _knownTypes = new List<Type>();

        public LiveData<Type> LastKnownType => _lastKnownType;

        private bool HasKnownType(Type type) =>
            _knownTypes.Contains(type);

        public void AddKnownType(Type type)
        {
            if(HasKnownType(type))
                return;
            
            _knownTypes.Add(type);
            _lastKnownType.Value = type;
            Debug.Log(type);
        }
    }
}