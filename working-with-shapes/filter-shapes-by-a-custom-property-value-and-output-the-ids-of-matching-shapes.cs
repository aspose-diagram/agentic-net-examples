using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Name and value of the custom property to filter by
                string targetPropertyName = "Category";
                string targetPropertyValue = "Important";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // List to hold IDs of matching shapes
                List<long> matchingShapeIds = new List<long>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has custom properties
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == targetPropertyName && prop.Value.Val == targetPropertyValue)
                                {
                                    matchingShapeIds.Add(shape.ID);
                                    // No need to check other properties for this shape
                                    break;
                                }
                            }
                        }
                    }
                }

                // Output the IDs of matching shapes
                Console.WriteLine("Shapes with custom property '{0}' = '{1}':", targetPropertyName, targetPropertyValue);
                foreach (long id in matchingShapeIds)
                {
                    Console.WriteLine("Shape ID: " + id);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }