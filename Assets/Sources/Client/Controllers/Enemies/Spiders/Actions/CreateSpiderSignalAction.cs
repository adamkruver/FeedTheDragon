using Sources.Client.Controllers.Enemies.Spiders.Signals;
using Sources.Client.Controllers.Enemies.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Enemies.Spiders.Queries;

namespace Sources.Client.Controllers.Enemies.Spiders.Actions
{
    public class CreateSpiderSignalAction : ISignalAction<CreateSpiderSignal>
    {
        private readonly CreateSpiderQuery _createSpiderQuery;
        private readonly BindableViewBuilder<EnemyViewModel> _bindableViewBuilder;

        public CreateSpiderSignalAction(
            CreateSpiderQuery createSpiderQuery,
            BindableViewBuilder<EnemyViewModel> bindableViewBuilder
            )
        {
            _createSpiderQuery = createSpiderQuery;
            _bindableViewBuilder = bindableViewBuilder;
        }

        public void Handle(CreateSpiderSignal signal)
        {
            int id = _createSpiderQuery.Handle(signal.Position);
            _bindableViewBuilder.Build(id, "Spider");
        }
    }
}