using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom document properties
            var customProps = diagram.DocumentProps.CustomProps;

            // Collect properties with null values
            var propsToRemove = new List<CustomProp>();
            foreach (CustomProp prop in customProps)
            {
                // CustomValue.ValueString holds the string value of the property
                if (prop.CustomValue.ValueString == null)
                {
                    propsToRemove.Add(prop);
                }
            }

            // Remove the identified properties from the collection
            foreach (var prop in propsToRemove)
            {
                customProps.Remove(prop);
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
