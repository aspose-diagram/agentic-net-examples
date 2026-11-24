using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "OutputDiagram.vsdx";

        // Guard: ensure the directory for the output exists (if any)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Determine the next available page ID (Page.ID is an int in this API version)
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create a new page and assign a unique ID and name
                Page newPage = new Page();
                newPage.ID = maxPageId + 1; // explicit int assignment
                newPage.Name = "OvalPage";

                // Add the new page to the diagram
                diagram.Pages.Add(newPage);

                // Define oval (ellipse) position and size (in inches)
                double pinX = 1.0;   // X coordinate of the lower-left corner
                double pinY = 1.0;   // Y coordinate of the lower-left corner
                double width = 2.0;  // Width of the oval
                double height = 1.0; // Height of the oval

                // Draw the oval on the newly added page
                // DrawEllipse returns the shape ID (long) but we don't need it further here
                newPage.DrawEllipse(pinX, pinY, width, height);

                // Save the diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}