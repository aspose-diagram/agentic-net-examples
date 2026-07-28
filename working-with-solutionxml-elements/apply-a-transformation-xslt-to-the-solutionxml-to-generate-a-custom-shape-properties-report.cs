using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Locate the SolutionXML that holds the custom shape properties.
            // Adjust the name filter according to the actual SolutionXML name in your diagram.
            SolutionXML shapePropsXml = null;
            foreach (SolutionXML sx in diagram.SolutionXMLs)
            {
                if (sx.Name == "ShapeProperties")
                {
                    shapePropsXml = sx;
                    break;
                }
            }

            if (shapePropsXml == null)
            {
                Console.WriteLine("SolutionXML named 'ShapeProperties' not found in the diagram.");
                return;
            }

            // Load the XSLT that defines the report layout (replace with your XSLT file path)
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("ShapePropertiesReport.xslt");

            // Prepare the XML source from the SolutionXML's XmlValue
            XmlDocument sourceXml = new XmlDocument();
            sourceXml.LoadXml(shapePropsXml.XmlValue);

            // Perform the transformation and write the result to an output file
            using (FileStream output = new FileStream("ShapePropertiesReport.html", FileMode.Create, FileAccess.Write))
            {
                xslt.Transform(sourceXml, null, output);
            }

            Console.WriteLine("Custom shape properties report generated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
