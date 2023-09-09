using Utils.LiveData;

namespace Sources.Client.Domain.Components
{
    public class AnimationSpeedComponent : IComponent
    {
        private readonly float _multiplier;

        private readonly MutableLiveData<float> _animationSpeed = new MutableLiveData<float>();

        public AnimationSpeedComponent(float multiplier) =>
            _multiplier = multiplier;

        public LiveData<float> AnimationSpeed => _animationSpeed;

        public void Set(float speed) =>
            _animationSpeed.Value = speed * _multiplier;
    }
}