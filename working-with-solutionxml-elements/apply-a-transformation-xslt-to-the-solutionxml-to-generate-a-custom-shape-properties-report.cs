using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class ShapePropertiesReportGenerator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Retrieve the first SolutionXML (adjust the index or name as needed)
            if (diagram.SolutionXMLs.Count == 0)
            {
                Console.WriteLine("No SolutionXML data found in the diagram.");
                return;
            }

            var solutionXml = diagram.SolutionXMLs[0]; // or locate by Name if required
            string xmlContent = solutionXml.XmlValue;

            // Load the XSLT transformation
            var xslt = new XslCompiledTransform();
            xslt.Load("transform.xslt"); // path to your XSLT file

            // Perform the transformation
            string report;
            using (var xmlReader = XmlReader.Create(new StringReader(xmlContent)))
            using (var resultWriter = new StringWriter())
            {
                xslt.Transform(xmlReader, null, resultWriter);
                report = resultWriter.ToString();
            }

            // Save the generated report to an HTML file
            File.WriteAllText("ShapePropertiesReport.html", report);
            Console.WriteLine("Report generated: ShapePropertiesReport.html");

            // Optional: store the report back into the diagram as a new SolutionXML entry
            // var reportSolutionXml = new SolutionXML("ShapePropertiesReport", report);
            // diagram.SolutionXMLs.Add(reportSolutionXml);
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
