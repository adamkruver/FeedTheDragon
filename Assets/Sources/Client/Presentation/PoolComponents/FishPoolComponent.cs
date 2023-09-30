using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;
using UnityEngine.Pool;

namespace Sources.Client.Presentation.PoolComponents
{
    public class FishPoolComponent : MonoBehaviour
    {
        private IObjectPool<FishView> _pool;
        private FishView _fishView;
        
        public void SetPool(IObjectPool<FishView> pool, FishView fishView)
        {
            _fishView = fishView;
            _pool = pool;
        }

        public void Release()
        {
            _pool.Release(_fishView);
        }
    }
}