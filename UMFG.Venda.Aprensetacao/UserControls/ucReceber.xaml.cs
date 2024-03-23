
using System.Windows.Controls;
using UMFG.Venda.Aprensetacao.Interfaces;
using UMFG.Venda.Aprensetacao.Models;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.UserControls
{
    /// <summary>
    /// Interação lógica para ucReceber.xam
    /// </summary>
    public partial class ucReceber : UserControl
    {
        private ucReceber(IObserver observer, PedidoModel pedido)
        {
            InitializeComponent();

            DataContext = new ReceberViewModel(this,observer,pedido);
        }

        internal static PedidoModel Exibir (IObserver observer,
            PedidoModel pedido)
        {
            var tela = new ucReceber(observer, pedido);
            var vm = tela.DataContext as ReceberViewModel;

            vm.Notify();
            return vm.Pedido;
        }

        

    }
}
