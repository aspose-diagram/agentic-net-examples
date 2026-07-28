using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt for the Visio file path
            Console.Write("Enter the path to the Visio file: ");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            // Prompt for the custom property name to search
            Console.Write("Enter the custom property name: ");
            string propName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(propName))
            {
                Console.WriteLine("Property name cannot be empty.");
                return;
            }

            // Prompt for the custom property value to match
            Console.Write("Enter the custom property value to match: ");
            string propValue = Console.ReadLine();

            if (propValue == null)
            {
                Console.WriteLine("Property value cannot be null.");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            bool anyMatch = false;

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Check each shape within the master for the custom property
                foreach (Shape shape in master.Shapes)
                {
                    // Ensure the shape has custom properties
                    if (shape.Props != null && shape.Props.Count > 0)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            // Compare property name and value (case-insensitive)
                            if (string.Equals(prop.Name, propName, StringComparison.OrdinalIgnoreCase) &&
                                string.Equals(prop.Value.Val, propValue, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Master '{master.Name}' contains the property '{propName}' with value '{propValue}'.");
                                anyMatch = true;
                                // Break out of inner loops once a match is found for this master
                                goto NextMaster;
                            }
                        }
                    }
                }

            NextMaster:
                continue;
            }

            if (!anyMatch)
            {
                Console.WriteLine("No masters found with the specified custom property value.");
            }
        }
    }