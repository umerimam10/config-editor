using System;
using System.Windows;

namespace ConfigEditor.Helpers
{
    public class MockMessageBox : IMessageBox
    {
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            Console.WriteLine(messageBoxText);
            return MessageBoxResult.OK;
        }
    }
}
