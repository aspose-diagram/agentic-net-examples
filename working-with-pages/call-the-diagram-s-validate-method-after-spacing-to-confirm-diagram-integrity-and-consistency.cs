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

            // Load the diagram (using the provided load rule)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Configure layout options with desired spacing (in inches)
                LayoutOptions layoutOptions = new LayoutOptions
                {
                    SpaceShapes = 0.5f // example spacing
                };

                // Apply layout (spacing) to each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.Layout(layoutOptions);
                }

                // Validate the diagram after spacing
                // Aspose.Diagram provides a Validation property to access validation results
                Validation validation = diagram.Validation;
                Console.WriteLine($"Validation issues count: {validation.Issues.Count}");

                // Save the diagram (using the provided save rule)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
