        using System.Windows.Controls;
        using UMFG.Venda.Aprensetacao.Classes;
        using UMFG.Venda.Aprensetacao.Comandos;
        using UMFG.Venda.Aprensetacao.Interfaces;
        using UMFG.Venda.Aprensetacao.UserControls;

        namespace UMFG.Venda.Aprensetacao.ViewModels
        {
            internal sealed class MainWindowViewModel : AbstractViewModel, IObserver
            {
                private UserControl _userControl;

                public ListarProdutosCommand ListarProdutos { get; private set; }
                    = new ListarProdutosCommand();

                public UserControl UserControl
                {
                    get => _userControl;
                    set => SetField(ref _userControl, value);
                }
                public MainWindowViewModel() : base("UMFG | Tela Principal")
                {

                }

                public void UpdateView(string viewName)
                {
                    switch (viewName)
                    {
                        case "ucListarProdutos":
                            UserControl = new ucListarProdutos(); 
                            break;
                            
                    }

                    switch (viewName)
                    {
                        case "ucListarProdutos":
                            UserControl = new ucListarProdutos(this); 
                            break;
                           
                    }
                }

                public void Update(ISubject subject)
                {
                    if (subject is ListarProdutosViewModel)
                        UserControl = (subject as ListarProdutosViewModel).UserControl;

                    if (subject is ReceberViewModel)
                        UserControl = (subject as ReceberViewModel).UserControl;
                }


            }
        }
