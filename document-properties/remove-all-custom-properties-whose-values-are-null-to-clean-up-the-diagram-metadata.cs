using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the collection of custom document properties
                var customProps = diagram.DocumentProps.CustomProps;

                // Iterate backwards to safely remove items while iterating
                for (int i = customProps.Count - 1; i >= 0; i--)
                {
                    var prop = customProps[i];

                    // Check if the custom property's value is null
                    // CustomValue may be null or its ValueString may be null
                    if (prop.CustomValue == null || prop.CustomValue.ValueString == null)
                    {
                        // Remove the custom property from the collection
                        customProps.Remove(prop);
                    }
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