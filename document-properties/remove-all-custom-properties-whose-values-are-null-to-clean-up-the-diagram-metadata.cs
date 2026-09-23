using System.IO;
using System;
using System.Collections.Generic;
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

            // Get the collection of custom document properties
            var customProps = diagram.DocumentProps.CustomProps;

            // Collect properties whose values are null
            var propsToRemove = new List<CustomProp>();
            foreach (CustomProp prop in customProps)
            {
                // Some custom properties may have a null CustomValue object
                // or a null ValueString inside the CustomValue
                if (prop.CustomValue == null || prop.CustomValue.ValueString == null)
                {
                    propsToRemove.Add(prop);
                }
            }

            // Remove the identified properties from the collection
            foreach (CustomProp prop in propsToRemove)
            {
                customProps.Remove(prop);
            }

            // Save the cleaned diagram
            string outputPath = "output_cleaned.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
