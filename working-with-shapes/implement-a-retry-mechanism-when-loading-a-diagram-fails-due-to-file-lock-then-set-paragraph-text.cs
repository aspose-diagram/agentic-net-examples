using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Retry configuration
            int maxRetries = 3;
            int delayMilliseconds = 1000;

            Diagram diagram = null;

            // Attempt to load the diagram with retry on file lock
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    diagram = new Diagram(inputPath);
                    break; // Success, exit the retry loop
                }
                catch (IOException ex)
                {
                    // If this was the last attempt, rethrow the exception
                    if (attempt == maxRetries)
                    {
                        Console.WriteLine($"Failed to load diagram after {maxRetries} attempts: {ex.Message}");
                        throw;
                    }

                    // Log and wait before retrying
                    Console.WriteLine($"Attempt {attempt} failed (possible file lock). Retrying in {delayMilliseconds} ms...");
                    Thread.Sleep(delayMilliseconds);
                }
            }

            // Ensure the diagram was loaded
            if (diagram == null)
            {
                Console.WriteLine("Diagram could not be loaded.");
                return;
            }

            // Locate the first shape on the first page
            Page firstPage = diagram.Pages[0];
            Shape targetShape = null;
            foreach (Shape shape in firstPage.Shapes)
            {
                targetShape = shape;
                break; // Take the first shape found
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found on the first page.");
                return;
            }

            // Clear existing text and set new paragraph text
            targetShape.Text.Value.Clear();
            targetShape.Text.Value.Add(new Txt("This is the new paragraph text."));

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
