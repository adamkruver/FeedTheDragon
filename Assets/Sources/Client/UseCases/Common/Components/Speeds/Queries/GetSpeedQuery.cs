﻿using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.Common.Components.Speeds.Queries
{
    public class GetSpeedQuery : ComponentUseCaseBase<SpeedComponent>
    {
        public GetSpeedQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public LiveData<float> Handle(int id) =>
            GetComponent(id).Speed;
    }
}