using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (VSDX) to be loaded
                string visioPath = "input.vsdx";

                // Path to the XML data source that contains mapping information
                string xmlPath = "mapping.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Load and parse the XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                // Example XML structure:
                // <Mappings>
                //   <Shape id="1" text="Updated Text" fill="#FF0000" />
                //   <Shape id="2" text="Another Text" fill="#00FF00" />
                // </Mappings>

                XmlNodeList shapeNodes = xmlDoc.SelectNodes("//Shape");
                if (shapeNodes != null)
                {
                    foreach (XmlNode node in shapeNodes)
                    {
                        // Retrieve the shape ID from the XML attribute
                        if (int.TryParse(node.Attributes["id"]?.Value, out int shapeId))
                        {
                            // Find the shape on the first page (adjust if needed)
                            Page page = diagram.Pages[0];
                            Shape shape = page.Shapes.GetShape(shapeId);
                            if (shape != null)
                            {
                                // Update shape text if the 'text' attribute exists
                                string newText = node.Attributes["text"]?.Value;
                                if (!string.IsNullOrEmpty(newText))
                                {
                                    // Clear existing text runs and add the new text
                                    shape.Text.Value.Clear();
                                    shape.Text.Value.Add(new Txt(newText));
                                }

                                // Update shape fill color if the 'fill' attribute exists
                                string fillColor = node.Attributes["fill"]?.Value;
                                if (!string.IsNullOrEmpty(fillColor))
                                {
                                    // FillForegnd expects a HEX color string
                                    shape.Fill.FillForegnd.Value = fillColor;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }