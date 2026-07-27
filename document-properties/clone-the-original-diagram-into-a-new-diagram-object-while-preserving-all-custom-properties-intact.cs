using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "source.vsdx";
                // Path where the cloned diagram will be saved
                string clonedPath = "cloned.vsdx";

                // Load the original diagram from file
                Diagram originalDiagram = new Diagram(sourcePath);

                // Clone the diagram by saving it to a memory stream and loading it back
                Diagram clonedDiagram;
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the original diagram into the memory stream in VSDX format
                    originalDiagram.Save(ms, SaveFileFormat.Vsdx);
                    // Reset stream position to the beginning before loading
                    ms.Position = 0;
                    // Load a new Diagram instance from the memory stream
                    clonedDiagram = new Diagram(ms);
                }

                // Verify that custom document properties are preserved
                Console.WriteLine("Custom properties in the cloned diagram:");
                foreach (var prop in clonedDiagram.DocumentProps.CustomProps)
                {
                    // CustomValue holds the actual value; use ValueString for string representation
                    string value = prop.CustomValue?.ValueString ?? "(null)";
                    Console.WriteLine($"- {prop.Name}: {value}");
                }

                // Save the cloned diagram to a new file
                clonedDiagram.Save(clonedPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Cloned diagram saved to '{clonedPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }