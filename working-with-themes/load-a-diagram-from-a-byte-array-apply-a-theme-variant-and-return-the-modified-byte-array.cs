using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramHelper
{
    public static byte[] ApplyThemeVariant(byte[] diagramBytes, PresetThemeVariantValue variant)
    {
        // Load diagram from the input byte array
        using (var inputStream = new MemoryStream(diagramBytes))
        using (var diagram = new Diagram(inputStream))
        {
            // Apply the preset theme variant to every shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.PresetThemeVariant = variant;
                }
            }

            // Save the modified diagram to a new memory stream
            using (var outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vdx);
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
