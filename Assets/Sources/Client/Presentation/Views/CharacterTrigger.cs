using UnityEngine;

namespace Sources.Client.Presentation.Views
{
    public class CharacterTrigger : MonoBehaviour
    {
        public bool IsSeeCharacter { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterController _))
                IsSeeCharacter = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterController _))
                IsSeeCharacter = false;
        }
    }
}