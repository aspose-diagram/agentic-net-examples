using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, XSLT file, output report file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <xsltPath> <outputReportPath>");
            return;
        }

        string diagramPath = args[0];
        string xsltPath = args[1];
        string outputReportPath = args[2];

        // Guard input files existence
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

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Prepare XSLT transformation
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                // StringBuilder to accumulate transformed results
                StringBuilder reportBuilder = new StringBuilder();

                // Iterate over all SolutionXML elements stored at the document level
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // Skip if the XML content is null or whitespace
                    if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                    {
                        continue;
                    }

                    // Perform XSLT transformation on the XML string
                    using (StringReader sr = new StringReader(solXml.XmlValue))
                    using (XmlReader xmlReader = XmlReader.Create(sr))
                    using (StringWriter sw = new StringWriter())
                    using (XmlWriter xmlWriter = XmlWriter.Create(sw, xslt.OutputSettings))
                    {
                        xslt.Transform(xmlReader, xmlWriter);
                        // Append a header and the transformed result to the report
                        reportBuilder.AppendLine($"--- Transformation for SolutionXML: {solXml.Name} ---");
                        reportBuilder.AppendLine(sw.ToString());
                        reportBuilder.AppendLine(); // extra line for readability
                    }
                }

                // Write the accumulated report to the output file
                File.WriteAllText(outputReportPath, reportBuilder.ToString());
                Console.WriteLine($"Report generated successfully at: {outputReportPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram or XSLT processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}