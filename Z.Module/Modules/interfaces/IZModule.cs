namespace Z.Module.Modules.interfaces
{
    public interface IZModule :
        IPreConfigureServices,
        IOnInitApplication,
        IPostInitApplication
    {
        void ConfigureServices(ServiceConfigerContext context);
    }
}
