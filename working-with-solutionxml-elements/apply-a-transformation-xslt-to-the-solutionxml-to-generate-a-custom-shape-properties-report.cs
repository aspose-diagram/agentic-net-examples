using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class ShapePropertiesReport
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the first SolutionXML (or locate by name if needed)
            SolutionXML solutionXml = null;
            foreach (SolutionXML sx in diagram.SolutionXMLs)
            {
                // Example: look for a specific name, otherwise take the first one
                if (sx.Name == "ShapeProperties" || solutionXml == null)
                    solutionXml = sx;
            }

            if (solutionXml == null)
            {
                Console.WriteLine("No SolutionXML found in the diagram.");
                return;
            }

            // Get the XML content stored in the SolutionXML
            string xmlContent = solutionXml.XmlValue;

            // Load the XSLT stylesheet that defines the custom report
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("ShapePropertiesReport.xslt");

            // Prepare input and output for the transformation
            using (StringReader sr = new StringReader(xmlContent))
            using (XmlReader xmlReader = XmlReader.Create(sr))
            using (StringWriter sw = new StringWriter())
            using (XmlWriter resultWriter = XmlWriter.Create(sw, xslt.OutputSettings))
            {
                // Apply the transformation
                xslt.Transform(xmlReader, resultWriter);

                // Get the transformed result as a string
                string report = sw.ToString();

                // Save the report to a file (HTML in this example)
                File.WriteAllText("ShapePropertiesReport.html", report);
                Console.WriteLine("Report generated successfully: ShapePropertiesReport.html");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
