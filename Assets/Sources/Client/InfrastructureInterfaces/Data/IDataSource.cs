namespace Sources.Client.InfrastructureInterfaces.Data
{
    public interface IDataSource<in TType, TObject> : IDataProvider<TType, TObject>
    {
        void Save<T>(TObject @object) where T : TType;
    }
}