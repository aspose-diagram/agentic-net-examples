using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input file paths – adjust as needed
                string diagramPath = "input.vsdx";
                string xmlPath = "data.xml";
                string xsltPath = "transform.xslt";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Load the source XML document
                XmlDocument xmlDoc = new XmlDocument();
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    xmlDoc.Load(xmlStream);
                }

                // Prepare the XSLT transformation
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                // Perform the transformation and capture the result as a string
                string transformedXml;
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
                    {
                        xslt.Transform(xmlDoc, xmlWriter);
                    }
                    transformedXml = stringWriter.ToString();
                }

                // Example: import the transformed XML into the first shape's Data1 property
                // Retrieve the first page
                Page page = diagram.Pages.GetPage(0);
                // Retrieve the first shape on the page (if any)
                if (page.Shapes.Count > 0)
                {
                    Shape shape = page.Shapes.GetShape(0);
                    // Assign the transformed XML string directly (Data1 is a plain string property)
                    shape.Data1 = transformedXml;
                }
                else
                {
                    throw new Exception("No shapes found on the first page to import data.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }