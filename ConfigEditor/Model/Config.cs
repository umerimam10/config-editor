using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ConfigEditor.Model
{
    [Serializable]
    [XmlRoot("Config")]
    public class Config
    {
        ObservableCollection<Server> _servers;
        ObservableCollection<Client> _clients;

        [XmlArray("Servers")]
        public ObservableCollection<Server> Servers
        {
            get { return _servers; }
            set { _servers = value; }
        }

        [XmlArray("Clients")]
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }
    }

    [Serializable]
    public class Server
    {
        string _path;
        string _connectionString;

        [XmlAttribute("path")]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        [XmlElement("ConnectionString")]
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
    }

    [Serializable]
    public class Client
    {
        string _path;
        bool _tsClient;

        [XmlAttribute("path")]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        [XmlElement("TSClient")]
        public bool TSClient
        {
            get { return _tsClient; }
            set { _tsClient = value; }
        }
    }
}
