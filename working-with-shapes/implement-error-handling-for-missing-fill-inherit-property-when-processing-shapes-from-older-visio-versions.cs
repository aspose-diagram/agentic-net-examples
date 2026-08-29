using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    ProcessShape(shape);
                }
            }

            // Save the diagram after processing
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    private static void ProcessShape(Shape shape)
    {
        // Use reflection to safely check for the existence of Fill.Inherit
        PropertyInfo fillProperty = typeof(Shape).GetProperty("Fill");
        if (fillProperty == null)
        {
            Console.WriteLine($"Shape ID {shape.ID} does not have a Fill property.");
            return;
        }

        object fillInstance = fillProperty.GetValue(shape);
        if (fillInstance == null)
        {
            Console.WriteLine($"Shape ID {shape.ID} has a null Fill instance.");
            return;
        }

        // Attempt to locate the Inherit property on the Fill object
        PropertyInfo inheritProperty = fillInstance.GetType().GetProperty("Inherit");
        if (inheritProperty == null)
        {
            // The Inherit property is missing (e.g., older Visio version)
            Console.WriteLine($"Shape ID {shape.ID} is missing Fill.Inherit property. Applying fallback fill.");

            // Example fallback: set a solid fill pattern
            // Ensure the Fill object is not null before accessing its members
            PropertyInfo fillPatternProp = fillInstance.GetType().GetProperty("FillPattern");
            if (fillPatternProp != null)
            {
                // FillPattern is a DoubleValue; set its Value to 1 (solid)
                object fillPatternInstance = fillPatternProp.GetValue(fillInstance);
                PropertyInfo valueProp = fillPatternInstance.GetType().GetProperty("Value");
                if (valueProp != null)
                {
                    valueProp.SetValue(fillPatternInstance, 1);
                }
            }

            return;
        }

        // If the Inherit property exists, you can read or manipulate it as needed
        object inheritValue = inheritProperty.GetValue(fillInstance);
        Console.WriteLine($"Shape ID {shape.ID} has Fill.Inherit property. Value: {inheritValue}");
    }
}
