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

                // Paths for the diagram, source XML, XSLT stylesheet, and output diagram
                string diagramPath = "input.vsdx";
                string xmlPath = "data.xml";
                string xsltPath = "transform.xslt";
                string outputPath = "output.vsdx";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(diagramPath);

                    // Load and apply the XSLT transformation to the XML data
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(xsltPath);

                    string transformedXml;
                    using (StringWriter stringWriter = new StringWriter())
                    {
                        // Transform the XML file; the result is written to the StringWriter
                        xslt.Transform(xmlPath, null, stringWriter);
                        transformedXml = stringWriter.ToString();
                    }

                    // Access the first page and the first shape on that page
                    Page page = diagram.Pages[0];
                    Shape shape = page.Shapes[0];

                    // Replace the shape's text with the transformed XML content
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(transformedXml));

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Simple error handling – output the exception message
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }