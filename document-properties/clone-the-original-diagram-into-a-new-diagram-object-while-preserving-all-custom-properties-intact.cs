using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the original Visio file
            string sourcePath = "source.vsdx";

            // Path where the cloned diagram will be saved
            string outputPath = "cloned.vsdx";

            // Load the original diagram
            Diagram original = new Diagram(sourcePath);

            // Clone the diagram using an in‑memory stream to preserve all data,
            // including custom document properties
            using (MemoryStream stream = new MemoryStream())
            {
                // Save the original diagram into the stream in VSDX format
                original.Save(stream, SaveFileFormat.Vsdx);

                // Reset the stream position before loading
                stream.Position = 0;

                // Load a new Diagram instance from the stream (the clone)
                Diagram cloned = new Diagram(stream);

                // Save the cloned diagram to the desired output file
                cloned.Save(outputPath, SaveFileFormat.Vsdx);

                // Optional: display the custom properties of the cloned diagram
                Console.WriteLine("Custom properties in the cloned diagram:");
                foreach (var prop in cloned.DocumentProps.CustomProps)
                {
                    // CustomValue.ValueString holds the property value as a string
                    Console.WriteLine($"{prop.Name}: {prop.CustomValue.ValueString}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
