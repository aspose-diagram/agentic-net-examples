using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        const string inputPath = "input.vsdx";
        const string outputPath = "output.vsdx";
        const int maxRetry = 3;
        const int delayMs = 1000;

        Diagram diagram = null;
        int attempt = 0;
        while (attempt < maxRetry)
        {
            try
            {
                // Attempt to load the diagram
                diagram = new Diagram(inputPath);
                break; // Success, exit loop
            }
            catch (IOException ex)
            {
                // Likely a file lock; wait and retry
                attempt++;
                if (attempt >= maxRetry)
                {
                    Console.WriteLine($"Failed to load diagram after {maxRetry} attempts: {ex.Message}");
                    return;
                }
                Console.WriteLine($"Attempt {attempt} failed due to file lock. Retrying in {delayMs} ms...");
                Thread.Sleep(delayMs);
            }
        }

        if (diagram == null)
        {
            Console.WriteLine("Diagram could not be loaded.");
            return;
        }

        // Find the first non-deleted shape on the first page
        Page page = diagram.Pages[0];
        Shape targetShape = null;
        foreach (Shape shape in page.Shapes)
        {
            if (shape.Del == BOOL.False)
            {
                targetShape = shape;
                break;
            }
        }

        if (targetShape == null)
        {
            Console.WriteLine("No suitable shape found to update text.");
            return;
        }

        // Clear existing text and set new paragraph text
        targetShape.Text.Value.Clear();
        targetShape.Text.Value.Add(new Txt("Updated paragraph text"));

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
    }
}
