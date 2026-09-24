using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be processed
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the custom property name and the value to filter by
                string targetPropName = "MyCustomProp";
                string targetPropValue = "DesiredValue";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has custom properties collection
                        if (shape.Props == null)
                            continue;

                        // Check each custom property (Prop) of the shape
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                            {
                                // Output the shape ID that matches the criteria
                                Console.WriteLine($"Matching Shape ID: {shape.ID}");
                                // No need to check other properties of this shape
                                break;
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }