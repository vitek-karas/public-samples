// See https://aka.ms/new-console-template for more information
using System.Text;
using XmlLiteWrapper;

Console.WriteLine("Hello, World!");

string inputXml = "<root/>";
using MemoryStream inputStream = new(Encoding.UTF8.GetBytes(inputXml));
IXmlReader xmlReader = XmlLite.CreateXmlReader();
xmlReader.SetInput(inputStream);
while (xmlReader.Read() is XmlNodeType nodeType && nodeType != XmlNodeType.XmlNodeType_None)
{
    if (nodeType == XmlNodeType.XmlNodeType_Element)
        Console.WriteLine(xmlReader.LocalName);
}