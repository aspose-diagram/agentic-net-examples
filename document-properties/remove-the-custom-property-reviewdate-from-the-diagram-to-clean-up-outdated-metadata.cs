using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path after removal of the custom property
                string outputPath = "output_cleaned.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Access the collection of custom properties
                var customProps = diagram.DocumentProps.CustomProps;

                // Find the custom property named "ReviewDate"
                CustomProp? reviewDateProp = null;
                foreach (CustomProp prop in customProps)
                {
                    if (prop.Name == "ReviewDate")
                    {
                        reviewDateProp = prop;
                        break;
                    }
                }

                // If the property exists, remove it from the collection
                if (reviewDateProp != null)
                {
                    customProps.Remove(reviewDateProp);
                    Console.WriteLine("Custom property 'ReviewDate' removed.");
                }
                else
                {
                    Console.WriteLine("Custom property 'ReviewDate' not found; no changes made.");
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