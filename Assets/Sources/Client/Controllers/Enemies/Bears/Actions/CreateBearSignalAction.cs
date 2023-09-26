using Sources.Client.Controllers.Enemies.Bears.Signals;
using Sources.Client.Controllers.Enemies.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Enemies.Bears;

namespace Sources.Client.Controllers.Enemies.Bears.Actions
{
    public class CreateBearSignalAction : ISignalAction<CreateBearSignal>
    {
        private readonly CreateBearQuery _createBearQuery;
        private readonly BindableViewBuilder<EnemyViewModel> _bindableViewBuilder;

        public CreateBearSignalAction(
            CreateBearQuery createBearQuery,
            BindableViewBuilder<EnemyViewModel> bindableViewBuilder
        )
        {
            _createBearQuery = createBearQuery;
            _bindableViewBuilder = bindableViewBuilder;
        }

        public void Handle(CreateBearSignal signal)
        {
            int id = _createBearQuery.Handle(signal.Position);
            _bindableViewBuilder.Build(id, "Bear");
        }
    }
}