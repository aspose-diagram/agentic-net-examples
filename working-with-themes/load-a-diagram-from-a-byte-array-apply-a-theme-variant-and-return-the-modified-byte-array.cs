using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramThemeHelper
{
    // Loads a Visio diagram from a byte array, applies the specified theme variant to all shapes,
    // and returns the modified diagram as a byte array in VDX format.
    public static byte[] ApplyThemeVariant(byte[] diagramBytes, PresetThemeVariantValue variant)
    {
        // Load the diagram from the input byte array using the Diagram(Stream) constructor.
        using (var inputStream = new MemoryStream(diagramBytes))
        using (var diagram = new Diagram(inputStream))
        {
            // Iterate through all pages and shapes, setting the PresetThemeVariant property.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.PresetThemeVariant = variant;
                }
            }

            // Save the modified diagram to a memory stream using the Save(Stream, SaveFileFormat) method.
            using (var outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vdx);
                // Return the resulting byte array.
                return outputStream.ToArray();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
