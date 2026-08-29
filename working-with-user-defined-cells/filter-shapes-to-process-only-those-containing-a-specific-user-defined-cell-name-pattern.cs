using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output Visio file (optional, after processing)
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the user‑defined cell name pattern to look for
            string pattern = "CustomCell";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check each user‑defined cell (User) for the pattern
                    foreach (User userCell in shape.Users)
                    {
                        // Ensure the cell name is not null before checking
                        if (!string.IsNullOrEmpty(userCell.Name) && userCell.Name.Contains(pattern))
                        {
                            // Process the shape – here we simply output its ID and Name
                            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, UserCell: {userCell.Name}");
                            // Additional processing logic can be placed here
                            break; // Stop checking other user cells for this shape
                        }
                    }
                }
            }

            // Save the diagram (optional – only if modifications were made)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
