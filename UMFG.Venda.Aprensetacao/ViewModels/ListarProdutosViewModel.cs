using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UMFG.Venda.Aprensetacao.Classes;
using UMFG.Venda.Aprensetacao.Comandos;
using UMFG.Venda.Aprensetacao.Interfaces;
using UMFG.Venda.Aprensetacao.Models;

namespace UMFG.Venda.Aprensetacao.ViewModels
{
    internal sealed class ListarProdutosViewModel : AbstractViewModel
    {
        // Descrições únicas para cada produto
        private const string DESCRICAO_BATATA = "Batatas frescas, cortadas e fritas à perfeição. Crocantes por fora e macias por dentro, ideais para acompanhar qualquer refeição.";
        private const string DESCRICAO_COMBO = "O combo perfeito para matar sua fome, incluindo um lanche de sua escolha, batatas fritas e um refrigerante. Economia e sabor juntos.";
        private const string DESCRICAO_LANCHE = "Lanche gourmet com hambúrguer de carne 100% bovina, queijo premium, alface crocante, tomate fresco e nosso molho especial. Satisfação garantida.";
        private const string DESCRICAO_REFRIGERANTE = "Refrigerante gelado, disponível em diversos sabores. A escolha refrescante para acompanhar suas refeições ou para um momento de relax.";



        public void OnPedidoChanged()
        {
            Adicionar.RaiseCanExecuteChanged();
            Remover.RaiseCanExecuteChanged();
            Receber.RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(Pedido));
        }


        private ProdutoModel _produtoSelecionado = new ProdutoModel();

        private ObservableCollection<ProdutoModel> _produtos
            = new ObservableCollection<ProdutoModel>();

        private PedidoModel _pedido = new PedidoModel();

        public ProdutoModel ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetField(ref _produtoSelecionado, value);
        }

        public ObservableCollection<ProdutoModel> Produtos
        {
            get => _produtos;
            set => SetField(ref _produtos, value);
        }

        public PedidoModel Pedido
        {
            get => _pedido;
            set => SetField(ref _pedido, value);
        }

        public AdicionarProdutoPedidoComand Adicionar { get; private set; }
            = new AdicionarProdutoPedidoComand();

        public RemoverProdutoPedidoCommand Remover { get; private set; } = new RemoverProdutoPedidoCommand();

        public ReceberPedidoCommand Receber { get; private set; }
            = new ReceberPedidoCommand();

        public ListarProdutosViewModel(UserControl userControl, IObserver observer)
            : base("Lista de Produtos")
        {
            UserControl = userControl;
            MainUserControl = observer;

            Add(observer);

            CarregarProdutos();
        }
       
        private void CarregarProdutos()
        {
            Produtos.Clear();

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net7.0-windows\Imagens\batata.png", UriKind.Relative)),
                Referencia = "Batata",
                Descricao = DESCRICAO_BATATA,
                Preco = 10.90m,
            });

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net7.0-windows\Imagens\combo.png", UriKind.Relative)),
                Referencia = "Combo",
                Descricao = DESCRICAO_COMBO,
                Preco = 45.90m,
            });

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net7.0-windows\Imagens\lanche.png", UriKind.Relative)),
                Referencia = "Lanche",
                Descricao = DESCRICAO_LANCHE,
                Preco = 22.90m,
            });

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net7.0-windows\Imagens\refrigerante.png", UriKind.Relative)),
                Referencia = "Refrigerante",
                Descricao = DESCRICAO_REFRIGERANTE,
                Preco = 7.50m,
            });
        }
    }
}
