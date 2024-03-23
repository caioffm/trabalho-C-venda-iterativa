using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using UMFG.Venda.Aprensetacao.Classes;
using UMFG.Venda.Aprensetacao.Interfaces;
using UMFG.Venda.Aprensetacao.Models;
using System.Windows.Input;
using System.Windows;
using UMFG.Venda.Aprensetacao.UserControls;

namespace UMFG.Venda.Aprensetacao.ViewModels
{
    internal sealed class ReceberViewModel : AbstractViewModel, INotifyPropertyChanged
    {
        private string _cardName;
        private string _cardNumber;
        private string _cardExpiration;
        private string _cardCVV;
        private PedidoModel _pedido = new PedidoModel();

        public string CardName
        {
            get => _cardName;
            set
            {
                if (_cardName != value)
                {
                    _cardName = value;
                    OnPropertyChanged(); // Este é o método herdado
                    OnPropertyChanged(nameof(IsPaymentValid)); // Notifica que IsPaymentValid pode ter mudado
                }
            }
        }

        public string CardNumber
        {
            get => _cardNumber ?? string.Empty; // Retorna uma string vazia se _cardNumber for null
            set
            {
                if (_cardNumber != value)
                {
                    _cardNumber = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPaymentValid));
                }
            }
        }

        public string CardExpiration
        {
            get => _cardExpiration;
            set
            {
                _cardExpiration = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPaymentValid));
            }
        }

        public string CardCVV
        {
            get => _cardCVV ?? string.Empty; // Retorna uma string vazia se _cardCVV for null
            set
            {
                if (_cardCVV != value)
                {
                    _cardCVV = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPaymentValid));
                }
            }
        }

        public class RelayCommand : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
                this.canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                this.execute(parameter);
            }
        }
        public ICommand FinalizarVendaCommand { get; private set; }

        public ReceberViewModel(UserControl userControl,
            IObserver observer,
            PedidoModel pedido) : base("Receber")
        {
            UserControl = userControl ?? throw new ArgumentNullException(nameof(userControl));
            MainUserControl = observer ?? throw new ArgumentNullException(nameof(observer));
            Pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));
            Add(observer);
            FinalizarVendaCommand = new RelayCommand(param => this.FinalizarVenda(), param => this.CanFinalizarVenda());
        }

        private bool CanFinalizarVenda()
        {
            
            return IsPaymentValid;
        }

        private void FinalizarVenda()
        {
            if (!IsPaymentValid)
            {
                MessageBox.Show("Por favor, verifique os dados do pagamento.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Venda finalizada com sucesso.", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            // Notificar o MainWindowViewModel para voltar para a tela de listagem de produtos
            MainUserControl.UpdateView("ucListarProdutos");
        }


        public bool IsPaymentValid
        {
            get
            {
                // Validação do número do cartão usando o algoritmo de Luhn.
                bool validCardNumber = !string.IsNullOrEmpty(CardNumber) &&
                                       CardNumber.Length == 16 &&
                                       IsCardNumberValid(CardNumber);

                // Verifica se o CVV contém apenas números e tem o comprimento correto.
                bool validCVV = !string.IsNullOrEmpty(CardCVV) &&
                                CardCVV.Length == 3 &&
                                CardCVV.All(char.IsDigit);

                // Validação da data de expiração mais restritiva.
                bool validExpiry = DateTime.TryParseExact(CardExpiration, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiryDate) &&
                                   expiryDate > DateTime.Now &&
                                   expiryDate < DateTime.Now.AddYears(10);

                // Verifica se o nome no cartão contém apenas letras e espaços.
                bool validName = !string.IsNullOrWhiteSpace(CardName) &&
                                 CardName.All(c => char.IsLetter(c) || c == ' ');

                return validCardNumber && validCVV && validExpiry && validName;
            }
        }
        private static bool IsCardNumberValid(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                var digit = cardNumber[i] - '0';
                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }
                sum += digit;
                alternate = !alternate;
            }
            return (sum % 10) == 0;
        }

        public PedidoModel Pedido
        {
            get => _pedido;
            set => SetField(ref _pedido, value);
        }

        // Implementação do INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
