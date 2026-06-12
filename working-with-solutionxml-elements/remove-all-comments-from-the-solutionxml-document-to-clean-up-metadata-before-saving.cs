using System;
using Aspose.Diagram;
using System.Xml;
using System.IO;

public class DiagramCleaner
{
    // Removes XML comments from all SolutionXML entries in the diagram.
    public static void RemoveSolutionXmlComments(Diagram diagram)
    {
        // Iterate through each SolutionXML in the diagram.
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            if (string.IsNullOrEmpty(solXml.XmlValue))
                continue;

            // Load the XML content into an XmlDocument.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true; // Preserve original formatting.
            xmlDoc.LoadXml(solXml.XmlValue);

            // Select all comment nodes in the document.
            XmlNodeList commentNodes = xmlDoc.SelectNodes("//comment()");

            // Remove each comment node from its parent.
            foreach (XmlNode comment in commentNodes)
            {
                if (comment.ParentNode != null)
                {
                    comment.ParentNode.RemoveChild(comment);
                }
            }

            // Save the cleaned XML back to a string.
            using (StringWriter writer = new StringWriter())
            {
                xmlDoc.Save(writer);
                solXml.XmlValue = writer.ToString();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramCleaner.RemoveSolutionXmlComments(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
