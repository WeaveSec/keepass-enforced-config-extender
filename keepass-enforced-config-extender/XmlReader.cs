using System;
using System.Xml;

internal class XmlReader
{
    public XmlNodeList ReadXmlFile(string filename)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.PreserveWhitespace = true;
        xmlDoc.Load("config.xml");

        XmlNodeList nodes = xmlDoc.DocumentElement.ChildNodes;

        return nodes;
/*        foreach (XmlNode node in nodes)
        {
            Console.WriteLine(node.Name + ": " + node.InnerText);
        }*/
    }
}