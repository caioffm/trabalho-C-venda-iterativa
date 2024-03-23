
namespace UMFG.Venda.Aprensetacao.Interfaces
{
    public interface IObserver
    {
        void Update(ISubject subject);
        void UpdateView(string viewName);
    }
}
