using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Define the output path for the template diagram.
        string outputPath = "Template.vsdx";

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required.
            using (Diagram diagram = new Diagram())
            {
                // Add a window to the diagram (required for proper display).
                Window window = new Window();
                window.WindowState = WindowStateValue.Maximized;
                window.WindowWidth = 1100;
                window.WindowHeight = 700;
                window.WindowType = WindowTypeValue.Drawing;
                diagram.Windows.Add(window);

                // Insert a new blank page.
                Page newPage = new Page();
                // Set a unique ID for the page (max existing ID + 1).
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }
                newPage.ID = maxPageId + 1;
                newPage.Name = "TemplatePage";
                diagram.Pages.Add(newPage);

                // Add a simple rectangle shape to the page using a built‑in master.
                // The AddShape method returns the shape ID; retrieve the Shape object.
                long shapeId = newPage.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle");
                Shape shape = newPage.Shapes.GetShape((int)shapeId);

                // Create user‑defined cells (custom fields) for the shape.
                // Example: a cell for "DocumentTitle" and another for "Author".
                User titleCell = new User();
                titleCell.Name = "DocumentTitle";
                titleCell.Value.Val = "Untitled Document";
                shape.Users.Add(titleCell);

                User authorCell = new User();
                authorCell.Name = "Author";
                authorCell.Value.Val = "Unknown Author";
                shape.Users.Add(authorCell);

                // Save the diagram as a VSDX template.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Template diagram created successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during diagram creation or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}