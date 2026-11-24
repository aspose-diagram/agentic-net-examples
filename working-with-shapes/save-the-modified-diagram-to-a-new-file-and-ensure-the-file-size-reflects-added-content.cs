using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Record original file size for later comparison
                long originalSize = new FileInfo(inputPath).Length;

                // --- Begin modification block ---
                // Add a new blank page to the diagram to increase its content size
                Page newPage = new Page();
                newPage.Name = "AddedPage";
                // Assign a unique ID (max existing ID + 1)
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                newPage.ID = maxId + 1;
                diagram.Pages.Add(newPage);
                // --- End modification block ---

                // Save the modified diagram to the specified output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Verify that the new file size reflects the added content
                long newSize = new FileInfo(outputPath).Length;
                Console.WriteLine($"Original file size: {originalSize} bytes");
                Console.WriteLine($"New file size: {newSize} bytes");
                if (newSize > originalSize)
                {
                    Console.WriteLine("The file size increased as expected after modification.");
                }
                else
                {
                    Console.WriteLine("Warning: The file size did not increase after modification.");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}