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
            string relativePath = @"..\Diagrams\sample.vsd";

            // Resolve the relative path to an absolute path
            string absolutePath = Path.GetFullPath(relativePath);

            // Load the diagram using the absolute path (lifecycle rule: use Diagram constructor)
            using (Diagram diagram = new Diagram(absolutePath))
            {
                // Example operation: save the diagram in another format
                string outputPath = Path.Combine(Path.GetDirectoryName(absolutePath) ?? string.Empty, "sample_converted.vdx");
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
