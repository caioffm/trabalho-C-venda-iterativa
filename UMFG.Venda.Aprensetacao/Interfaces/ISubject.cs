
namespace UMFG.Venda.Aprensetacao.Interfaces
{
    public interface ISubject
    {
        void Add(IObserver observer);
        void Remove(IObserver observer);
        void Notify();
    }
}
