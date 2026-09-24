using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the new width (in inches) for the connector shapes
                double newWidth = 2.0;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // LINQ query to select shapes whose universal name starts with "Connector"
                    var connectorShapes = page.Shapes
                                              .Where(shape => !string.IsNullOrEmpty(shape.NameU) &&
                                                              shape.NameU.StartsWith("Connector", StringComparison.Ordinal));

                    // Adjust the width of each selected shape
                    foreach (Shape shape in connectorShapes)
                    {
                        shape.XForm.Width.Value = newWidth;
                    }
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