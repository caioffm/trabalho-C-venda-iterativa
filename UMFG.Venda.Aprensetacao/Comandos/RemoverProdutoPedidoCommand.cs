using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMFG.Venda.Aprensetacao.Classes;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.Comandos
{
    internal sealed class RemoverProdutoPedidoCommand : AbstractCommand
    {
        public override bool CanExecute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;
            
            return vm?.ProdutoSelecionado != null && vm.Pedido.Produtos.Contains(vm.ProdutoSelecionado);
        }

        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;
            if (vm != null && vm.ProdutoSelecionado != null)
            {
                
                vm.Pedido.Produtos.Remove(vm.ProdutoSelecionado);
                vm.Pedido.Total = vm.Pedido.Produtos.Sum(x => x.Preco);
                vm.OnPedidoChanged(); 
            }
        }
    }
}

