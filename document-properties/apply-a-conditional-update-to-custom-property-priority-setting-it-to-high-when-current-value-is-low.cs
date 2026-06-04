using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the input Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Flag to indicate whether the property was found
                bool priorityFound = false;

                // Iterate through custom properties
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (string.Equals(prop.Name, "Priority", StringComparison.OrdinalIgnoreCase))
                    {
                        priorityFound = true;
                        // Check current value
                        string currentValue = prop.CustomValue.ValueString;
                        if (string.Equals(currentValue, "Low", StringComparison.OrdinalIgnoreCase))
                        {
                            // Update to "High"
                            prop.CustomValue.ValueString = "High";
                            Console.WriteLine("Custom property 'Priority' updated from 'Low' to 'High'.");
                        }
                        else
                        {
                            Console.WriteLine($"Custom property 'Priority' has value '{currentValue}' and was not changed.");
                        }
                        break;
                    }
                }

                // If the property does not exist, add it with value "High"
                if (!priorityFound)
                {
                    CustomProp newProp = new CustomProp
                    {
                        Name = "Priority",
                        PropType = PropType.String,
                        CustomValue = { ValueString = "High" }
                    };
                    diagram.DocumentProps.CustomProps.Add(newProp);
                    Console.WriteLine("Custom property 'Priority' was not found and has been added with value 'High'.");
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }