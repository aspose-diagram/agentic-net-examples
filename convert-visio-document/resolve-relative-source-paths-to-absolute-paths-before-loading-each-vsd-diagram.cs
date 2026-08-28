using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Relative path to the source VSD file
            string relativePath = @"Diagrams\sample.vsd";

            // Resolve the relative path to an absolute path
            string absolutePath = Path.GetFullPath(relativePath);

            // Load the diagram using the absolute path
            Diagram diagram = new Diagram(absolutePath);

            // Example operation: modify the diagram (placeholder for actual logic)
            // ...

            // Define the output path (saving in the same directory with a new name)
            string outputPath = Path.Combine(Path.GetDirectoryName(absolutePath) ?? string.Empty, "sample_output.vsd");

            // Save the diagram using the absolute output path
            diagram.Save(outputPath, SaveFileFormat.Vsd);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Diagram loaded from: " + absolutePath);
            Console.WriteLine("Diagram saved to: " + outputPath);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
