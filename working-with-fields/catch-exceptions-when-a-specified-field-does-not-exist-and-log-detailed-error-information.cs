using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Name of the custom property (field) to check
                string targetFieldName = "MyField";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        try
                        {
                            // Attempt to find the custom property by name
                            Prop targetProp = null;
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == targetFieldName)
                                {
                                    targetProp = prop;
                                    break;
                                }
                            }

                            // If the property was not found, throw an exception
                            if (targetProp == null)
                            {
                                throw new Exception($"Custom property '{targetFieldName}' not found on shape ID {shape.ID} (NameU: {shape.NameU}).");
                            }

                            // Property exists – you can work with it here
                            Console.WriteLine($"Found property '{targetFieldName}' on shape ID {shape.ID} with value: {targetProp.Value.Val}");
                        }
                        catch (Exception ex)
                        {
                            // Log detailed error information
                            Console.WriteLine("Error accessing custom property:");
                            Console.WriteLine($"Message: {ex.Message}");
                            Console.WriteLine($"StackTrace: {ex.StackTrace}");
                        }
                    }
                }

                // Save the diagram (no changes made in this example)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }