using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (you can change the index if needed)
                Page page = diagram.Pages[0];

                // Find a connector shape on the page.
                // Connectors are 1‑D shapes (OneD == true) or have the master name "Dynamic connector".
                Shape connector = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD || (shape.Master != null && shape.Master.Name == "Dynamic connector"))
                    {
                        connector = shape;
                        break;
                    }
                }

                if (connector == null)
                {
                    Console.WriteLine("No connector shape was found on the page.");
                    return;
                }

                // Attach a comment directly to the connector shape.
                // The comment will be positioned at the shape's default comment location.
                page.AddComment(connector, "Review this connector segment.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Comment added to connector (ID: {connector.ID}) and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }