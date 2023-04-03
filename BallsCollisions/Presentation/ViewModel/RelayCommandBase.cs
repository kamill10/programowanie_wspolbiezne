using System;

namespace Presentation.ViewModel
{
    public class RelayCommandBase
    {
        public event EventHandler CanExecuteChanged;
    }
}