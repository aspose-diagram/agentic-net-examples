using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string diagramPath = "input.vsdx";

                // The custom property name and value to filter masters by
                string targetPropName = "Category";
                string targetPropValue = "Important";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                Console.WriteLine($"Scanning masters in diagram: {diagramPath}");
                Console.WriteLine($"Looking for masters that contain a shape with custom property \"{targetPropName}\" = \"{targetPropValue}\"");
                Console.WriteLine();

                bool anyMasterFound = false;

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    bool masterMatches = false;

                    // Iterate through all shapes defined in the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // Ensure the shape has custom properties collection
                        if (shape.Props == null) continue;

                        // Check each custom property on the shape
                        foreach (Prop prop in shape.Props)
                        {
                            // Compare property name and value
                            if (prop.Name == targetPropName && prop.Value != null && prop.Value.Val == targetPropValue)
                            {
                                masterMatches = true;
                                break;
                            }
                        }

                        if (masterMatches) break;
                    }

                    if (masterMatches)
                    {
                        anyMasterFound = true;
                        Console.WriteLine($"Master ID: {master.ID}, Name: {master.Name}");
                    }
                }

                if (!anyMasterFound)
                {
                    Console.WriteLine("No masters matched the specified custom property criteria.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }