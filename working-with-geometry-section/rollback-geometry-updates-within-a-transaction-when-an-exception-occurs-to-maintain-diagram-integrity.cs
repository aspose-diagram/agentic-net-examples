using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file after successful updates
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Preserve original state in a memory stream for rollback
            MemoryStream backupStream = new MemoryStream();
            diagram.Save(backupStream, SaveFileFormat.Vsdx);
            // Reset stream position for later reading
            backupStream.Position = 0;

            try
            {
                // Example geometry update: move the first shape on the first page
                Page page = diagram.Pages[0];
                if (page.Shapes.Count > 0)
                {
                    // Retrieve the shape by its ID (first shape)
                    Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);
                    // Move shape by 1 inch right and 0.5 inch up
                    shape.Move(1.0, -0.5);
                }

                // Additional geometry modifications can be placed here
                // ...

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Rollback: reload the original diagram from the backup stream
                backupStream.Position = 0;
                Diagram restoredDiagram = new Diagram(backupStream);
                // Save the restored diagram to the output path to maintain integrity
                restoredDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Log the error
                Console.WriteLine("An error occurred during updates. Changes have been rolled back.");
                Console.WriteLine($"Error details: {ex.Message}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
