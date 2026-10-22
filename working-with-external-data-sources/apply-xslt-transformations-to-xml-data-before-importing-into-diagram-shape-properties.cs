using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: diagram file, input XML, XSLT file, output diagram
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
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the source XML document
            XmlDocument sourceXml = new XmlDocument();
            sourceXml.Load(xmlPath);

            // Prepare the XSLT transformation
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            // Perform transformation into a string
            string transformedXml;
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
                {
                    xslt.Transform(sourceXml, xmlWriter);
                }
                transformedXml = stringWriter.ToString();
            }

            // Apply the transformed XML to each shape's Data1 property
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page has shapes
                if (page.Shapes == null) continue;

                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];
                    if (shape != null)
                    {
                        // Store the transformed XML in the shape's Data1 field
                        shape.Data1 = transformedXml;
                    }
                }
            }

            // Optionally store the transformed XML at the document level as SolutionXML
            // (If the property exists; otherwise this line can be omitted safely)
            // diagram.SolutionXML = transformedXml;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}