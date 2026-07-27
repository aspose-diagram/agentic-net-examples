using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the modified file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the collection of custom properties
                var customProps = diagram.DocumentProps.CustomProps;

                // Find and remove the custom property named "ReviewDate"
                for (int i = customProps.Count - 1; i >= 0; i--)
                {
                    var prop = customProps[i];
                    if (prop.Name == "ReviewDate")
                    {
                        customProps.Remove(prop);
                        Console.WriteLine("Removed custom property 'ReviewDate'.");
                        break;
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
