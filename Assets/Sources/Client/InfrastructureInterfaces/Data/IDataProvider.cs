namespace Sources.Client.InfrastructureInterfaces.Data
{
    public interface IDataProvider<in TType, TObject>
    {
        TObject Load<T>() where T: TType;
    }
}