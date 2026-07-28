using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input and output file paths
        Console.Write("Enter the path of the Visio file to process: ");
        string inputPath = Console.ReadLine();

        Console.Write("Enter the path where the updated file should be saved: ");
        string outputPath = Console.ReadLine();

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Reset the line jump style to the library default
                        shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                    }
                }
            }

            // Save the modified diagram (preserving original format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Connector line jump styles have been reset and the diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
