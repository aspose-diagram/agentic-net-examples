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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the first group shape on the page
                Shape groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                    throw new Exception("No group shape found on the page.");

                // Iterate through sub‑shapes of the group and set their width to 5.0 inches
                foreach (Shape subShape in groupShape.Shapes)
                {
                    subShape.SetWidth(5.0);
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