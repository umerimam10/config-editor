using System.Windows;

namespace ConfigEditor.Helpers
{
    public class ConfigEditorMessageBox : IMessageBox
    {
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
