using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchPentagonAdder
{
    // Folder containing the Visio diagram files
    private const string InputFolder = @"C:\Diagrams\Input";
    // Folder where modified diagrams will be saved
    private const string OutputFolder = @"C:\Diagrams\Output";

    static void Main()
    {
        try
        {

            // Ensure output directory exists
            Directory.CreateDirectory(OutputFolder);

            // Process each Visio file in the input folder (supports .vsdx, .vsd, .vdx)
            foreach (string filePath in Directory.GetFiles(InputFolder, "*.*", SearchOption.TopDirectoryOnly))
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    continue; // Skip non‑Visio files

                try
                {
                    // Load the diagram using the appropriate constructor (Diagram(string))
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Use the first page (or ActivePage) to add the shape
                        Page page = diagram.Pages[0];

                        // Define position and size for the pentagon (in inches)
                        double pinX = 5.0;   // X‑coordinate of the shape's center
                        double pinY = 5.0;   // Y‑coordinate of the shape's center
                        double width = 2.0; // Width of the pentagon
                        double height = 2.0; // Height of the pentagon

                        // Add a pentagon shape using the master name "Pentagon"
                        // AddShape(double pinX, double pinY, double width, double height, string masterName)
                        page.AddShape(pinX, pinY, width, height, "Pentagon");

                        // Prepare output file path
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string outputPath = Path.Combine(OutputFolder, fileName + ".vdx");

                        // Save the modified diagram using Save(string, SaveFileFormat)
                        diagram.Save(outputPath, SaveFileFormat.Vdx);
                    }

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
