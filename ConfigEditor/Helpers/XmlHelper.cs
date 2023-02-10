using System;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ConfigEditor.Helpers
{
    public class XmlHelper
    {
        public static StringBuilder XmlSerialize<T>(object classObject)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
            nameSpace.Add("", "");

            StringBuilder serializedString = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Auto, Indent = true };
            using (XmlWriter writer = XmlWriter.Create(serializedString, settings))
                serializer.Serialize(writer, classObject, nameSpace);

            return serializedString;
        }

        public static T XmlDeserialize<T>(string serializedString) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (System.IO.StringReader stringReader = new System.IO.StringReader(serializedString))
            {
                using (XmlReader reader = XmlReader.Create(stringReader, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Auto }))
                {
                    return serializer.Deserialize(reader) as T;
                }
            }
        }

        public static bool IsXmlValid(string xml, IMessageBox msgBox)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream schemaStream = asm.GetManifestResourceStream("ConfigEditor.Resources.Schema.xsd");

            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                XmlSchema schema = XmlSchema.Read(schemaStream, null);
                settings.Schemas.Add(schema);
                settings.ValidationType = ValidationType.Schema;

                byte[] byteArray = Encoding.UTF8.GetBytes(xml);

                using (System.IO.Stream stream = new System.IO.MemoryStream(byteArray))
                {
                    using (XmlReader reader = XmlReader.Create(stream, settings))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(reader);

                        stream.Close();
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                msgBox.Show(ex.Message, "Config file couldn't be saved", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                schemaStream.Close();
            }
            return true;
        }
    }
}
