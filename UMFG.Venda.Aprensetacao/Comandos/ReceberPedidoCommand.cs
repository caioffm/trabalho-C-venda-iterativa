using System;
using System.Windows;
using UMFG.Venda.Aprensetacao.Classes;
using UMFG.Venda.Aprensetacao.UserControls;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.Comandos
{
    internal sealed class ReceberPedidoCommand : AbstractCommand
    {
        public override bool CanExecute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;
            return vm?.Pedido.Produtos.Count > 0;
        }

        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;
            if (vm != null && vm.Pedido.Produtos.Count > 0)
            {
                ucReceber.Exibir(vm.MainUserControl, vm.Pedido);
            }
        }
    }
    }

