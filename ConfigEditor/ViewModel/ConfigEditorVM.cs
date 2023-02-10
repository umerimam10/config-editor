using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using ConfigEditor.Helpers;
using ConfigEditor.Model;

namespace ConfigEditor.ViewModel
{
    public class ConfigEditorVM
    {
        private ICommand _saveConfigCommand;
        public Config Config { get; set; }

        public ICommand SaveConfigCommand
        {
            get
            {
                _saveConfigCommand = new RelayCommand(
                    param => true,
                    param => SaveConfig(new ConfigEditorMessageBox())
                );
                return _saveConfigCommand;
            }
        }

        public ConfigEditorVM(Config config)
        {
            Config = config;
            Config.Clients ??= new ObservableCollection<Client>();
            Config.Servers ??= new ObservableCollection<Server>();
        }

        public bool SaveConfig(IMessageBox messageBox)
        {
            var serializedConfig = XmlHelper.XmlSerialize<Config>(Config);
            if (!XmlHelper.IsXmlValid(serializedConfig.ToString(), messageBox))
                return false;

            try
            {
                File.WriteAllText("test.txt", serializedConfig.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}
