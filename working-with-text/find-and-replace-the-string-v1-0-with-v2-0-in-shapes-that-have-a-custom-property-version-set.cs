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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through custom properties (Props) of the shape
                    foreach (Prop prop in shape.Props)
                    {
                        // Check for the custom property named "Version"
                        if (prop.Name == "Version")
                        {
                            string currentValue = prop.Value.Val;
                            // Replace 'v1.0' with 'v2.0' if present
                            if (!string.IsNullOrEmpty(currentValue) && currentValue.Contains("v1.0"))
                            {
                                string newValue = currentValue.Replace("v1.0", "v2.0");
                                prop.Value.Val = newValue;
                                Console.WriteLine($"Shape ID {shape.ID}: Version updated to '{newValue}'.");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
