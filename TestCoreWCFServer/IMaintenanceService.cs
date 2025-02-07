namespace TestCoreWCFServer
{
    public interface IMaintenanceServiceCallback
    {
        [System.ServiceModel.OperationContract]
        string SendByServer(string message);
    }

    [System.ServiceModel.ServiceContract(CallbackContract = typeof(IMaintenanceServiceCallback))]
    public interface IMaintenanceService
    {
        [System.ServiceModel.OperationContract]
        string ReceivedByServer(string message);
    }
}
