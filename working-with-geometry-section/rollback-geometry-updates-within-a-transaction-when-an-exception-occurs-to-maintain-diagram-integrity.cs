using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a backup of the original diagram in memory
            MemoryStream backupStream = new MemoryStream();
            diagram.Save(backupStream, SaveFileFormat.Vsdx);
            backupStream.Position = 0; // Reset stream for reading

            try
            {
                // Perform geometry updates inside the transaction
                // Example: draw a rectangle on the first page
                Page page = diagram.Pages[0];
                // DrawRectangle(pinX, pinY, width, height)
                // This creates a rectangle shape; the method returns the shape ID (long)
                long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);
                // Retrieve the shape to modify further if needed
                Shape rectShape = page.Shapes.GetShape(rectId);
                // Set a fill color as an example of additional geometry-related change
                rectShape.Fill.FillForegnd.Value = "#FFCC00"; // orange fill
            }
            catch (Exception ex)
            {
                // An error occurred – roll back to the original diagram state
                Console.WriteLine($"Error during update: {ex.Message}");
                // Reload the diagram from the backup stream
                backupStream.Position = 0;
                diagram = new Diagram(backupStream);
            }

            // Save the (possibly rolled‑back) diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
