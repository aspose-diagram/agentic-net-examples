using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define spacing options (in inches)
            LayoutOptions layoutOptions = new LayoutOptions
            {
                SpaceShapes = 0.5f // adjust as needed
            };

            // Apply layout (spacing) to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.Layout(layoutOptions);
            }

            // Invoke validation to confirm diagram integrity
            Validation validation = diagram.Validation;

            // Optionally, inspect validation issues
            foreach (var issue in validation.Issues)
            {
                Console.WriteLine($"Issue: {issue}");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
