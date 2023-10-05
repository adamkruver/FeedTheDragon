using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishCollider : MonoBehaviour
    {
        [field: SerializeField] public FishView FishView { get; private set; }
    }
}