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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Flag to indicate if the property was found
                bool statusFound = false;

                // Iterate through custom properties to find "Status"
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (string.Equals(prop.Name, "Status", StringComparison.OrdinalIgnoreCase))
                    {
                        // Update the value to "Completed"
                        prop.CustomValue.ValueString = "Completed";
                        statusFound = true;
                        break;
                    }
                }

                // If the property was not found, throw an exception
                if (!statusFound)
                {
                    throw new Exception("Custom property 'Status' was not found in the diagram.");
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }