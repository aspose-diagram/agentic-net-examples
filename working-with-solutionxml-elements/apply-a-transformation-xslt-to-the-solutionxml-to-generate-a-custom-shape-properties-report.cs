using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input arguments: diagram file, XSLT file, output report file
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string xsltPath = args.Length > 1 ? args[1] : "transform.xslt";
        string outputPath = args.Length > 2 ? args[2] : "report.xml";

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Load the XSLT transformation
        XslCompiledTransform xslt = new XslCompiledTransform();
        try
        {
            xslt.Load(xsltPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XSLT file: {ex.Message}");
            return;
        }

        // Load the Visio diagram using Aspose.Diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Prepare a writer for the final report
        using (StreamWriter reportWriter = new StreamWriter(outputPath, false))
        {
            // Iterate over all SolutionXML elements in the document
            // (Assuming diagram.SolutionXMLs provides the collection)
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Retrieve the raw XML stored in the SolutionXML element
                string xmlContent = solXml.XmlValue;

                // Transform the XML using the loaded XSLT
                try
                {
                    using (StringReader stringReader = new StringReader(xmlContent))
                    using (XmlReader xmlReader = XmlReader.Create(stringReader))
                    using (StringWriter stringWriter = new StringWriter())
                    {
                        xslt.Transform(xmlReader, null, stringWriter);
                        string transformedResult = stringWriter.ToString();

                        // Write the transformed result to the report file
                        reportWriter.WriteLine($"<!-- SolutionXML Name: {solXml.Name} -->");
                        reportWriter.WriteLine(transformedResult);
                        reportWriter.WriteLine(); // Add a blank line between entries
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"XSLT transformation failed for SolutionXML '{solXml.Name}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Report generated successfully at: {outputPath}");
    }
}