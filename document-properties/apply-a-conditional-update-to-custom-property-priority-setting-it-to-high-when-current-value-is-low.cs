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
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access custom properties collection
            var customProps = diagram.DocumentProps.CustomProps;

            // Find the custom property named "Priority"
            for (int i = 0; i < customProps.Count; i++)
            {
                var prop = customProps[i];
                if (prop.Name == "Priority")
                {
                    // Get current value as string
                    string currentValue = prop.CustomValue.ValueString;

                    // If the current value is "Low", set it to "High"
                    if (string.Equals(currentValue, "Low", StringComparison.OrdinalIgnoreCase))
                    {
                        prop.CustomValue.ValueString = "High";
                        Console.WriteLine("Custom property 'Priority' updated to 'High'.");
                    }
                    else
                    {
                        Console.WriteLine($"Custom property 'Priority' has value '{currentValue}' – no change needed.");
                    }

                    // Property found and processed; exit loop
                    break;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
