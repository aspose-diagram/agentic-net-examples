using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all SolutionXML elements
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                string xmlContent = solutionXml.XmlValue;

                // Skip if there is no XML content
                if (string.IsNullOrEmpty(xmlContent))
                    continue;

                // Load XML into XmlDocument preserving whitespace
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.LoadXml(xmlContent);

                // Remove all comment nodes recursively
                RemoveComments(doc);

                // Serialize the cleaned XML back to string
                using (StringWriter stringWriter = new StringWriter())
                {
                    doc.Save(stringWriter);
                    solutionXml.XmlValue = stringWriter.ToString();
                }
            }

            // Save the cleaned diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively removes all XML comment nodes from the given XmlNode.
    /// </summary>
    /// <param name="node">The XmlNode to process.</param>
    private static void RemoveComments(XmlNode node)
    {
        if (node == null)
            return;

        // Collect comment nodes to remove to avoid modifying the collection during iteration
        var commentsToRemove = new System.Collections.Generic.List<XmlNode>();
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Comment)
            {
                commentsToRemove.Add(child);
            }
            else
            {
                // Recurse into element nodes
                RemoveComments(child);
            }
        }

        // Remove collected comment nodes
        foreach (XmlNode comment in commentsToRemove)
        {
            node.RemoveChild(comment);
        }
    }
}