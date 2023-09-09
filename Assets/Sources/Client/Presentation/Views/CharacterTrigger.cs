using UnityEngine;

namespace Sources.Client.Presentation.Views
{
    public class CharacterTrigger : MonoBehaviour
    {
        public bool IsSeeCharacter { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enter");

            if (other.TryGetComponent(out CharacterController _))
            {
                Debug.Log("IsSee");
                IsSeeCharacter = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit");

            if (other.TryGetComponent(out CharacterController _))
            {
                Debug.Log("NoSee");
                IsSeeCharacter = false;
            }
        }
    }
}