using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramHeaderFooterHelper
{
    // Copies header and footer settings from source diagram to target diagram.
    // Page content of the target diagram remains unchanged.
    public static void CopyHeaderFooter(string sourceFilePath, string targetFilePath, string outputFilePath)
    {
        // Load the source diagram
        Diagram sourceDiagram = new Diagram(sourceFilePath);

        // Load the target diagram (the diagram whose header/footer will be replaced)
        Diagram targetDiagram = new Diagram(targetFilePath);

        // Copy the HeaderFooter settings.
        // HeaderFooter property is read‑only, but it returns a mutable object.
        // We copy each sub‑property manually. Since the exact members of HeaderFooter
        // are not listed in the documentation, we use reflection to copy all public
        // writable properties from the source HeaderFooter to the target HeaderFooter.
        var sourceHeaderFooter = sourceDiagram.HeaderFooter;
        var targetHeaderFooter = targetDiagram.HeaderFooter;

        var headerFooterType = typeof(HeaderFooter);
        var properties = headerFooterType.GetProperties();

        foreach (var prop in properties)
        {
            // Only copy if the property can be written to.
            if (prop.CanWrite)
            {
                var value = prop.GetValue(sourceHeaderFooter);
                prop.SetValue(targetHeaderFooter, value);
            }
        }

        // Save the modified target diagram.
        // Using VDX format as an example; adjust SaveFileFormat if needed.
        targetDiagram.Save(outputFilePath, SaveFileFormat.Vdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramHeaderFooterHelper.CopyHeaderFooter("", "", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
