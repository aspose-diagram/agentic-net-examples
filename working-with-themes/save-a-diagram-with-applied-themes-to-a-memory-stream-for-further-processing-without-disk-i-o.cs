using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Create a new empty diagram (contains a default page)
        Diagram diagram = new Diagram();

        // Apply a preset theme to the first page
        Page page = diagram.Pages[0];
        page.PresetTheme = PresetThemeValue.Bubble;
        page.PresetThemeVariant = PresetThemeVariantValue.Variant2;

        // Save the diagram with the applied theme to a memory stream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            diagram.Save(memoryStream, SaveFileFormat.Vsdx);
            // The memory stream now holds the VSDX file bytes
            Console.WriteLine($"Diagram saved to memory stream. Size = {memoryStream.Length} bytes.");
            // Further processing of the stream can be performed here
        }
    }
}
