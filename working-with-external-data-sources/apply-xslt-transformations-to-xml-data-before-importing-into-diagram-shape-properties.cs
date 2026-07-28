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

            // File paths
            string diagramPath = "input.vsdx";
            string xmlPath = "data.xml";
            string xsltPath = "transform.xslt";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the source XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Load the XSLT stylesheet
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            // Perform the transformation and capture the result as a string
            string transformedXml;
            using (StringWriter stringWriter = new StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
            {
                xslt.Transform(xmlDoc, xmlWriter);
                transformedXml = stringWriter.ToString();
            }

            // Locate the shape that will receive the transformed data
            Page page = diagram.Pages.GetPage(0);
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null && shape.NameU.Equals("TargetShape", StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("Target shape not found in the diagram.");
            }

            // Replace the shape's text with the transformed XML content
            targetShape.Text.Value.Clear();
            targetShape.Text.Value.Add(new Txt(transformedXml));

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
