using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Name of the custom property to filter by
                string customPropertyName = "MyCustomProp";

                // Desired value of the custom property
                string targetValue = "TargetValue";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check each custom property (Prop) of the shape
                        foreach (Prop prop in shape.Props)
                        {
                            // Compare property name and value
                            if (prop.Name == customPropertyName && prop.Value.Val == targetValue)
                            {
                                // Output the shape ID
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