
using System.Windows.Controls;
using UMFG.Venda.Aprensetacao.Interfaces;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.UserControls
{
    /// <summary>
    /// Interação lógica para ucListarProdutos.xam
    /// </summary>
    public partial class ucListarProdutos : UserControl
    {

        public ucListarProdutos()
        {
            InitializeComponent();
        }
        public ucListarProdutos(IObserver observer)
        {
            InitializeComponent();
            DataContext = new ListarProdutosViewModel(this,observer);
        }

        internal static void Exibir(IObserver observer)
        {
            (new ucListarProdutos(observer).DataContext as ListarProdutosViewModel).Notify();
        }
    }
}
