using UnityEngine;

namespace Utils.LiveDatas.Sources.Frameworks.LiveDatas
{
    public class Vector3LiveData : LiveData<Vector3>
    {
        public Vector3LiveData(MutableLiveData<Vector3> liveData) : base(liveData)
        {
        }
    }
}