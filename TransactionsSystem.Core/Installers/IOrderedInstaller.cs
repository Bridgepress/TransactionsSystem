namespace TransactionsSystem.Core.Installers
{
    public interface IOrderedInstaller : IInstaller
    {
        int Order { get; }
    }
}
