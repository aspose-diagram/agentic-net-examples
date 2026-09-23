using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source files
                string xmlInputPath = "input.xml";
                string xsltPath = "transform.xslt";
                string diagramPath = "template.vsdx";
                string outputDiagramPath = "output.vsdx";

                // Validate input files exist
                if (!File.Exists(xmlInputPath))
                    throw new FileNotFoundException($"XML input file not found: {xmlInputPath}");
                if (!File.Exists(xsltPath))
                    throw new FileNotFoundException($"XSLT file not found: {xsltPath}");
                if (!File.Exists(diagramPath))
                    throw new FileNotFoundException($"Diagram template file not found: {diagramPath}");

                // Apply XSLT transformation to the XML data
                string transformedXml = TransformXml(xmlInputPath, xsltPath);

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page (pages are 0‑based)
                Page page = diagram.Pages[0];

                // Find the first shape on the page to update
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                    throw new InvalidOperationException("No shapes found on the first page of the diagram.");

                // Replace the shape's text with the transformed XML content
                targetShape.Text.Value.Clear();
                targetShape.Text.Value.Add(new Txt(transformedXml));

                // Save the modified diagram
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Performs XSLT transformation and returns the resulting string
        private static string TransformXml(string xmlPath, string xsltPath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                {
                    xslt.Transform(xmlReader, null, stringWriter);
                }
                return stringWriter.ToString();
            }
        }
    }