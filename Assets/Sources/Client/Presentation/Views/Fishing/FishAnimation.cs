using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishAnimation : MonoBehaviour
    {
        private int _catchHash = Animator.StringToHash("Catch");
        private int _swimHash = Animator.StringToHash("Swim");

        [SerializeField] private Animator _animator;

        public void Catch() =>
            _animator.SetTrigger(_catchHash);

        public void Swim() =>
            _animator.SetTrigger(_swimHash);
    }
}