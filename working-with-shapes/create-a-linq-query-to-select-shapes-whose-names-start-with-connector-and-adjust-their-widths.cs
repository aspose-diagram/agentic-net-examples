using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the new width for the connector shapes (in inches)
                double newWidth = 2.0;

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // LINQ query to select shapes whose Name starts with "Connector"
                    var connectorShapes = page.Shapes
                                              .Cast<Shape>()
                                              .Where(s => !string.IsNullOrEmpty(s.Name) && s.Name.StartsWith("Connector"));

                    // Adjust the width of each selected shape
                    foreach (Shape shape in connectorShapes)
                    {
                        shape.XForm.Width.Value = newWidth;
                    }
                }

                // Save the modified diagram (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }