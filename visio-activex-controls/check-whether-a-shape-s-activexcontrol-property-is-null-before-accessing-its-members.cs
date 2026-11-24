using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (optional). If not provided, append "_out" to the input name.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_out.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control before accessing it
                    if (shape.ActiveXControl != null)
                    {
                        // Access a member safely – for example, the control type
                        ControlType ctrlType = shape.ActiveXControl.Type;
                        Console.WriteLine($"Shape ID {shape.ID} contains ActiveX control of type {ctrlType}.");

                        // Example of modifying a property (if applicable). Here we just demonstrate
                        // reading the Caption property for a CommandButton control.
                        if (ctrlType == ControlType.CommandButton)
                        {
                            // Cast to the specific control class
                            CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                            // Output current caption
                            Console.WriteLine($"  Current Caption: {cmdBtn.Caption}");
                            // Optionally modify the caption
                            cmdBtn.Caption = "Updated Caption";
                        }
                    }
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}