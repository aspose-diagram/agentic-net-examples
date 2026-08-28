using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded and saved.
            string filePath = "diagram.vsdx";

            // Load the diagram from the specified file.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Example geometry modification:
                // Move the first shape on the first page by 1 inch in both X and Y directions.
                if (diagram.Pages.Count > 0)
                {
                    Page page = diagram.Pages[0];

                    // Ensure the page contains at least one shape.
                    if (page.Shapes.Count > 0)
                    {
                        // Retrieve the first shape in the collection.
                        Shape firstShape = null;
                        foreach (Shape s in page.Shapes)
                        {
                            firstShape = s;
                            break;
                        }

                        // Apply the geometry change if a shape was found.
                        if (firstShape != null)
                        {
                            firstShape.Move(1.0, 1.0); // Move by 1 inch on X and Y axes.
                        }
                    }
                }

                // Save the modified diagram back to the original file path,
                // preserving the Visio format (VSDX in this example).
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
