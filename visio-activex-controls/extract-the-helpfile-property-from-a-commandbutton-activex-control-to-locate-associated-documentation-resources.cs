using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
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
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control of type CommandButton
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                    {
                        // Cast to the specific CommandButtonActiveXControl (optional, not used further here)
                        CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)shape.ActiveXControl;

                        // Retrieve the HelpTopic value from the shape's Help object
                        // Note: HelpTopic is a Str2Value; we access its underlying string via the Value property
                        string helpTopic = shape.Help.HelpTopic.Value;

                        // Output the extracted HelpTopic information
                        Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}, HelpTopic: {helpTopic}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram related errors to the error stream
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}