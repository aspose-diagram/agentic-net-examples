using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Validate input arguments
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
                return;
            }

            string inputPath = args[0];
            string outputPath = args.Length > 1 ? args[1] : "cleaned_output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect custom properties with null values
            var propsToRemove = new List<CustomProp>();
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                // Some custom properties may have a null CustomValue or a null string inside it
                if (prop.CustomValue == null || prop.CustomValue.ValueString == null)
                {
                    propsToRemove.Add(prop);
                }
            }

            // Remove the identified properties from the collection
            foreach (CustomProp prop in propsToRemove)
            {
                diagram.DocumentProps.CustomProps.Remove(prop);
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}' with null-valued custom properties removed.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
