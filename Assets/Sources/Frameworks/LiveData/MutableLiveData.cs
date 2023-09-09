﻿using System;
using System.Collections.Generic;

namespace Utils.LiveData
{
    public class MutableLiveData<T> : IDisposable
    {
        private T _value;

        public MutableLiveData(T value = default)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                Changed?.Invoke(_value);
            }
        }

        public event Action<T> Changed;
        public event Action Disposing;

        public void Dispose()
        {
            Disposing?.Invoke();
        }

        public static implicit operator LiveData<T>(MutableLiveData<T> data) =>
            new LiveData<T>(data);
    }
}