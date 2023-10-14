using System;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.Common.Components.FirstContacts.Queries
{
    public class GetLastKnownTypeQuery : ComponentUseCaseBase<FirstContactComponent>
    {
        public GetLastKnownTypeQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public LiveData<Type> Handle(int id) =>
            GetComponent(id).LastKnownType;
    }
}