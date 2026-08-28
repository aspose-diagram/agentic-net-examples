using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public class Program
{
    public static void Main(string[] args)
    {
        // Get the input file path from command‑line arguments or ask the user.
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.WriteLine("No file path was provided. Exiting.");
            return;
        }

        // Load the diagram. Wrap in a using block to ensure resources are released.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                Console.WriteLine($"Scanning '{inputPath}' for shapes that contain ActiveX controls...");

                bool foundAny = false;

                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // The ActiveXControl property is null when the shape does not embed a control.
                        if (shape.ActiveXControl != null)
                        {
                            foundAny = true;
                            ControlType ctrlType = shape.ActiveXControl.Type;

                            Console.WriteLine(
                                $"Page: {page.NameU} (ID: {page.ID}) | " +
                                $"Shape ID: {shape.ID} | NameU: {shape.NameU} | " +
                                $"ActiveX Control Type: {ctrlType}");
                        }
                    }
                }

                if (!foundAny)
                {
                    Console.WriteLine("No shapes with ActiveX controls were found in the document.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}
