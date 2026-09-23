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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom properties
            var customProps = diagram.DocumentProps.CustomProps;

            // Locate the custom property named "ReviewDate"
            CustomProp? reviewProp = null;
            foreach (CustomProp prop in customProps)
            {
                if (prop.Name == "ReviewDate")
                {
                    reviewProp = prop;
                    break;
                }
            }

            // Remove the property if it exists
            if (reviewProp != null)
            {
                customProps.Remove(reviewProp);
                Console.WriteLine("Custom property 'ReviewDate' has been removed.");
            }
            else
            {
                Console.WriteLine("Custom property 'ReviewDate' was not found.");
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
