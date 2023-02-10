using ConfigEditor.Model;
using ConfigEditor.ViewModel;
using Xunit;
using System.Collections.ObjectModel;
using ConfigEditor.Helpers;

namespace ConfigEditorTestProject
{
    public class UnitTestConfigEditor
    {
        [Fact]
        public void Config_ShouldSave()
        {
            Config config = new();
            config.Servers = new ObservableCollection<Server>();
            config.Clients = new ObservableCollection<Client>();

            config.Servers.Add(new Server() { Path = "D:\\XZY", ConnectionString = "TestConnectionString" });
            config.Clients.Add(new Client() { Path = "D:\\XZY", TSClient = true });
            ConfigEditorVM configEditorVM = new(config);

            Assert.True(configEditorVM.SaveConfig(new MockMessageBox()));
        }

        [Fact]
        public void Config_ShouldNotSave()
        {
            Config config = new();
            config.Servers = new ObservableCollection<Server>();
            config.Clients = new ObservableCollection<Client>();

            config.Servers.Add(new Server() { Path = "D:\\XZY" });
            config.Clients.Add(new Client() { Path = "D:\\XZY", TSClient = true });
            ConfigEditorVM configEditorVM = new(config);

            Assert.False(configEditorVM.SaveConfig(new MockMessageBox()));
        }
    }
}
