using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        // Desired rotation angle in degrees
        const double rotationAngle = 45.0;
        // Tag name and value that identify masters to rotate
        const string tagName = "RotateTag";
        const string tagValue = "True";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                bool shouldRotate = false;

                // Check each shape within the master for the custom property tag
                foreach (Shape shape in master.Shapes)
                {
                    // Guard: ensure the shape has custom properties collection
                    if (shape.Props != null)
                    {
                        // Retrieve the property by name
                        Prop prop = shape.Props.GetProp(tagName);
                        // If the property exists and matches the expected value, mark for rotation
                        if (prop != null && string.Equals(prop.Value.Val, tagValue, StringComparison.OrdinalIgnoreCase))
                        {
                            shouldRotate = true;
                            break; // No need to check further shapes in this master
                        }
                    }
                }

                // If the master matches the tag criteria, rotate all its shapes
                if (shouldRotate)
                {
                    foreach (Shape shape in master.Shapes)
                    {
                        // Apply rotation by setting the Angle cell value
                        shape.XForm.Angle.Value = rotationAngle;
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}