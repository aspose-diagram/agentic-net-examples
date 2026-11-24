using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: diagramPath xmlPath xsltPath outputPath
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <xmlPath> <xsltPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string xmlPath = args[1];
        string xsltPath = args[2];
        string outputPath = args[3];

        // Guard clauses for input files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load the source XML document
                XDocument sourceXml = XDocument.Load(xmlPath);

                // Prepare XSLT transformation
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                // Perform transformation and capture the result as a string
                string transformedXml;
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
                    {
                        xslt.Transform(sourceXml.CreateReader(), xmlWriter);
                    }
                    transformedXml = stringWriter.ToString();
                }

                // Iterate over all pages and shapes to store the transformed XML
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page contains shapes
                    if (page.Shapes == null) continue;

                    // Iterate through shapes by index (ShapeCollection is not generic)
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];
                        if (shape == null) continue;

                        // Ensure the Misc collection exists
                        if (shape.Misc == null) continue;

                        // Store the transformed XML in the shape's comment (as SolutionXML is not available per API)
                        shape.Misc.Comment.Value = transformedXml;
                    }
                }

                // Save the modified diagram with a valid SaveFileFormat enum value
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}