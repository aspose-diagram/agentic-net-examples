using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command‑line argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            bool buttonFound = false;

            // Iterate through all pages and shapes to locate a CommandButton ActiveX control.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control.
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                    {
                        // Cast to the specific CommandButtonActiveXControl type.
                        CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                        // Output the Caption property of the button.
                        Console.WriteLine($"Shape ID {shape.ID} - Button Caption: \"{button.Caption}\"");
                        buttonFound = true;
                    }
                }
            }

            if (!buttonFound)
            {
                Console.WriteLine("No CommandButton ActiveX control was found in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}