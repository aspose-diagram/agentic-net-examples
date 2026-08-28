using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramHeaderFooterHelper
{
    // Copies header and footer settings from source diagram to target diagram
    private static void CopyHeaderFooter(Diagram source, Diagram target)
    {
        // HeaderFooter property is read‑only, so we copy its individual writable properties
        var sourceHF = source.HeaderFooter;
        var targetHF = target.HeaderFooter;

        Type hfType = sourceHF.GetType();
        foreach (PropertyInfo prop in hfType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            // Only copy properties that can be read and written
            if (prop.CanRead && prop.CanWrite)
            {
                object value = prop.GetValue(sourceHF);
                prop.SetValue(targetHF, value);
            }
        }
    }

    // Public method that loads diagrams, copies header/footer, and saves the result
    public static void CopyHeaderFooterBetweenFiles(string sourceFilePath, string targetFilePath, string outputFilePath)
    {
        // Load source and target diagrams using the provided constructors
        using (Diagram sourceDiagram = new Diagram(sourceFilePath))
        using (Diagram targetDiagram = new Diagram(targetFilePath))
        {
            // Perform the copy operation
            CopyHeaderFooter(sourceDiagram, targetDiagram);

            // Save the modified target diagram (preserving page content)
            targetDiagram.Save(outputFilePath, SaveFileFormat.Vdx);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramHeaderFooterHelper.CopyHeaderFooterBetweenFiles("", "", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
