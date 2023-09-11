namespace Utils.LiveDatas.Sources.Frameworks.LiveDatas
{
    public class BoolLiveData : LiveData<bool>
    {
        public BoolLiveData(MutableLiveData<bool> liveData) : base(liveData)
        {
        }
    }
}