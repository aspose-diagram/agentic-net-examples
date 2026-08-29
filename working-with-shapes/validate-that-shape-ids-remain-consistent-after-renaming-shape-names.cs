using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (using the provided load rule)
            Diagram diagram = new Diagram("{inputPath}");

            // Work with the first page
            Page page = diagram.Pages[0];

            // Define original and new shape names
            string oldName = "Shape1";
            string newName = "RenamedShape1";

            // Retrieve the shape by its original name
            Shape shape = page.Shapes.GetShape(oldName);
            if (shape == null)
            {
                Console.WriteLine($"Shape with name '{oldName}' not found.");
                return;
            }

            // Store the original ID for later comparison
            long originalId = shape.ID;

            // Rename the shape
            shape.Name = newName;
            shape.RefreshData(); // Ensure internal references are updated

            // Retrieve the shape using the new name
            Shape renamedShape = page.Shapes.GetShape(newName);
            if (renamedShape == null)
            {
                Console.WriteLine($"Renamed shape '{newName}' not found.");
                return;
            }

            // Validate that the ID has not changed after renaming
            if (renamedShape.ID == originalId)
            {
                Console.WriteLine("Shape ID remains consistent after renaming.");
            }
            else
            {
                Console.WriteLine($"Shape ID changed! Original: {originalId}, After rename: {renamedShape.ID}");
            }

            // Save the diagram (using the provided save rule)
            diagram.Save("{outputPath}", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
