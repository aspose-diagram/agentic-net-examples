using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl == null)
                        continue;

                    // Verify the control is a CommandButton
                    if (shape.ActiveXControl.Type != ControlType.CommandButton)
                        continue;

                    // Retrieve the HelpTopic value (the help file/path)
                    // Note: HelpTopic is a Str2Value; we access its .Value property to get the string.
                    string helpTopic = shape.Help.HelpTopic.Value;

                    // Output the shape ID and associated help topic
                    Console.WriteLine($"Page {page.ID}, Shape ID {shape.ID}: HelpTopic = \"{helpTopic}\"");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}