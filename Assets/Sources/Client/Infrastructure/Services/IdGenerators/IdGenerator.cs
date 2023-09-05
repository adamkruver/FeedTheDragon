using System;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;

namespace Sources.Client.Infrastructure.Services.IdGenerators
{
    public class IdGenerator : IIdGenerator
    {
        private int _currentId;
        
        public IdGenerator(int startId)
        {
            _currentId = startId;
        }

        public int GetId()
        {
            return _currentId++;
        }
    }
}