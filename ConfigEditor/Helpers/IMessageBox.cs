using System.Windows;

namespace ConfigEditor.Helpers
{
    public interface IMessageBox
    {
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}
