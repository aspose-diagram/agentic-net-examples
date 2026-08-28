using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom properties
            var customProps = diagram.DocumentProps.CustomProps;

            bool updated = false;

            // Locate the custom property named "Priority"
            foreach (CustomProp prop in customProps)
            {
                if (prop.Name == "Priority")
                {
                    // If its current value is "Low", change it to "High"
                    if (prop.CustomValue.ValueString == "Low")
                    {
                        prop.CustomValue.ValueString = "High";
                        updated = true;
                    }
                    break; // Property found; exit loop
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Output the result
            if (updated)
            {
                Console.WriteLine("Custom property 'Priority' was updated to 'High'.");
            }
            else
            {
                Console.WriteLine("Custom property 'Priority' was not updated (not found or not 'Low').");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
