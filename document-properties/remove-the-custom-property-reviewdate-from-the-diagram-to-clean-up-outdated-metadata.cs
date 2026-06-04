using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the collection of custom properties
                var customProps = diagram.DocumentProps.CustomProps;

                // Locate the custom property named "ReviewDate"
                CustomProp reviewDateProp = null;
                foreach (CustomProp prop in customProps)
                {
                    if (prop.Name == "ReviewDate")
                    {
                        reviewDateProp = prop;
                        break;
                    }
                }

                // Remove the property if it exists
                if (reviewDateProp != null)
                {
                    customProps.Remove(reviewDateProp);
                    Console.WriteLine("Custom property 'ReviewDate' has been removed.");
                }
                else
                {
                    Console.WriteLine("Custom property 'ReviewDate' was not found.");
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
