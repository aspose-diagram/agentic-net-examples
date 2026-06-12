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
                const string targetPropName = "MyProp";
                const string targetPropValue = "TargetValue";

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Props collection
                        if (shape.Props != null)
                        {
                            // Search for the custom property with the specified name
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                                {
                                    // Output the matching shape's ID
                                    Console.WriteLine($"Matching Shape ID: {shape.ID}");
                                    // No need to check other properties for this shape
                                    break;
                                }
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