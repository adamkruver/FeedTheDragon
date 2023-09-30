using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingSight : MonoBehaviour
    {
        [SerializeField] private FishingLineCursor _cursor;
        // private FishingSightSphereCaster _fishingSightSphereCaster;
        [SerializeField] private float _radius = 1f;

        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            
            _cursor.SetPosition(mousePosition);

           // _cursor.CanCatch = _fishingSightSphereCaster.TryGetFish(mousePosition, _radius, out FishView fish);
        }
    }
}