using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramProcessor
{
    // Saves a diagram to the specified path while logging its Visio version and build number.
    public void SaveDiagramWithLogging(string sourcePath, string destinationPath, SaveFileFormat format)
    {
        // Load the diagram from the source file.
        using (Diagram diagram = new Diagram(sourcePath))
        {
            // Log the embedded Visio version and build number.
            Console.WriteLine($"Visio Version: {diagram.Version}");
            Console.WriteLine($"Visio Build Number: {diagram.Buildnum}");

            // Save the diagram to the destination using the requested format.
            diagram.Save(destinationPath, format);
        }
    }

    // Example overload using SaveOptions (e.g., for VDX/VSX formats).
    public void SaveDiagramWithLogging(string sourcePath, string destinationPath, SaveOptions options)
    {
        using (Diagram diagram = new Diagram(sourcePath))
        {
            Console.WriteLine($"Visio Version: {diagram.Version}");
            Console.WriteLine($"Visio Build Number: {diagram.Buildnum}");

            diagram.Save(destinationPath, options);
        }
    }
}

// Example usage:
// var processor = new DiagramProcessor();
// processor.SaveDiagramWithLogging("input.vsdx", "output.vsdx", SaveFileFormat.Vsdx);
// Or using options:
// var options = new DiagramSaveOptions(SaveFileFormat.Vdx);
// processor.SaveDiagramWithLogging("input.vsdx", "output.vdx", options);

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramProcessor();
            obj.SaveDiagramWithLogging("", "", null);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
