using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Path to the XSLT file that defines the report format
                string xsltPath = "ShapePropertiesReport.xslt";

                // Output directory for the generated reports
                string outputDir = "Reports";

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare the XSLT transformer
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                // Iterate over all SolutionXML elements in the diagram
                int index = 1;
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // The XML content stored in the SolutionXML element
                    string xmlContent = solutionXml.XmlValue;

                    // Load the XML content into an XmlReader
                    using (StringReader stringReader = new StringReader(xmlContent))
                    using (XmlReader xmlReader = XmlReader.Create(stringReader))
                    // Prepare the output file for this SolutionXML element
                    using (FileStream outputStream = new FileStream(
                        Path.Combine(outputDir, $"SolutionXmlReport_{index}.html"),
                        FileMode.Create, FileAccess.Write))
                    using (XmlWriter xmlWriter = XmlWriter.Create(outputStream, xslt.OutputSettings))
                    {
                        // Apply the XSLT transformation
                        xslt.Transform(xmlReader, xmlWriter);
                    }

                    Console.WriteLine($"Report generated for SolutionXML #{index}");
                    index++;
                }

                // Optionally, save the diagram if any modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }