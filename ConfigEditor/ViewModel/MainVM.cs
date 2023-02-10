using ConfigEditor.Helpers;
using ConfigEditor.Model;
using ConfigEditor.View;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

namespace ConfigEditor.ViewModel
{
    class MainVM
    {
        #region Private fields
        private ICommand _addConfigCommand;
        private ICommand _editConfigCommand;
        #endregion

        #region Public Events
        public ICommand AddConfigCommand
        {
            get 
            {
                _addConfigCommand = new RelayCommand(
                    param => true,
                    param => OpenAddConfigWindow()
                );
                return _addConfigCommand;
            }
        }

        public ICommand EditConfigCommand
        {
            get
            {
                _editConfigCommand = new RelayCommand(
                    param => true,
                    param => OpenEditConfigWindow()
                );
                return _editConfigCommand;
            }
        }
        #endregion

        #region Private Methods
        private void OpenAddConfigWindow()
        {
            ConfigEditorWindow dialog = new ConfigEditorWindow() { DataContext = new ConfigEditorVM(new Config()) };
            dialog.ShowDialog();
        }        

        private void OpenEditConfigWindow()
        {
            // open existing config file
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text Files (.txt)|*.txt";
            fileDialog.Multiselect = false;

            if ((bool)!fileDialog.ShowDialog())
            {
                return;
            }

            // read config file
            string configText;
            using (StreamReader readtext = new StreamReader(fileDialog.FileName))
            {
                configText = readtext.ReadToEnd();
            }

            Config config = XmlHelper.XmlDeserialize<Config>(configText);

            ConfigEditorWindow configEditorWindow = new ConfigEditorWindow() { DataContext = new ConfigEditorVM(config) };
            configEditorWindow.ShowDialog();
        }
        #endregion
    }
}
