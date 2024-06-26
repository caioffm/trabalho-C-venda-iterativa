﻿using System;
using System.Windows.Input;

namespace UMFG.Venda.Aprensetacao.Classes
{
    internal abstract class AbstractCommand : AbstractNotifyPropertyChange, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
