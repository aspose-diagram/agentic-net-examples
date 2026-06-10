using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // The custom property name and the value to search for
                string targetPropName = "MyCustomProp";
                string targetPropValue = "DesiredValue";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    bool masterMatches = false;

                    // Check each shape within the master for the custom property
                    foreach (Shape shape in master.Shapes)
                    {
                        // Ensure the shape has custom properties
                        if (shape.Props == null) continue;

                        foreach (Prop prop in shape.Props)
                        {
                            // Compare property name and value
                            if (prop.Label != null && prop.Label.Value == targetPropName &&
                                prop.Value != null && prop.Value.Val == targetPropValue)
                            {
                                masterMatches = true;
                                break;
                            }
                        }

                        if (masterMatches) break;
                    }

                    // Output matching masters
                    if (masterMatches)
                    {
                        Console.WriteLine($"Master found: Name = {master.Name}, ID = {master.ID}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }