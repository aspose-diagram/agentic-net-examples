using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        // Retry parameters
        const int maxRetries = 3;
        const int delayMilliseconds = 1000;
        int attempt = 0;
        Diagram diagram = null;

        // Attempt to load the diagram with retry on file lock (IOException)
        while (attempt < maxRetries)
        {
            try
            {
                diagram = new Diagram(inputPath);
                break; // Success, exit loop
            }
            catch (IOException ex)
            {
                attempt++;
                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine($"Failed to load diagram after {maxRetries} attempts: {ex.Message}");
                    return;
                }
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error while loading diagram: {ex.Message}");
                return;
            }
        }

        if (diagram == null)
        {
            Console.Error.WriteLine("Diagram could not be loaded.");
            return;
        }

        // Access the first page
        Page page = diagram.Pages[0];

        // Find the first shape on the page (skip deleted shapes)
        Shape targetShape = null;
        foreach (Shape shape in page.Shapes)
        {
            if (shape.Del == BOOL.False) // ignore logically deleted shapes
            {
                targetShape = shape;
                break;
            }
        }

        if (targetShape == null)
        {
            Console.Error.WriteLine("No suitable shape found on the first page.");
            return;
        }

        // Clear existing text and set new paragraph text
        targetShape.Text.Value.Clear();
        targetShape.Text.Value.Add(new Txt("This is the new paragraph text."));

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}