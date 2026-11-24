using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path; if not supplied, create a default one.
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Processed_" + Path.GetFileName(inputPath));

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only shapes that contain an ActiveX control.
                        if (shape.ActiveXControl != null)
                        {
                            // Output basic information about the shape and its control.
                            Console.WriteLine($"Shape ID {shape.ID} contains an ActiveX control of type {shape.ActiveXControl.Type}.");

                            // Handle CommandButton controls.
                            if (shape.ActiveXControl.Type == ControlType.CommandButton)
                            {
                                // Cast to the concrete CommandButtonActiveXControl.
                                CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)shape.ActiveXControl;
                                // Modify properties of the command button.
                                commandButton.Caption = "Processed";
                                // Example: set width and height (these are double values).
                                commandButton.Width = 2.0;
                                commandButton.Height = 0.5;
                                Console.WriteLine($"  -> Updated CommandButton caption to \"{commandButton.Caption}\".");
                            }
                            // Handle CheckBox controls.
                            else if (shape.ActiveXControl.Type == ControlType.CheckBox)
                            {
                                // Cast to the concrete CheckBoxActiveXControl.
                                CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                // Set the checkbox to checked.
                                checkBox.Value = CheckValueType.Checked;
                                Console.WriteLine("  -> Set CheckBox value to Checked.");
                            }
                            // Handle Image controls.
                            else if (shape.ActiveXControl.Type == ControlType.Image)
                            {
                                // Cast to the concrete ImageActiveXControl.
                                ImageActiveXControl imageCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                // Assign a placeholder image (empty byte array) – replace with real image bytes as needed.
                                imageCtrl.Picture = new byte[0];
                                Console.WriteLine("  -> Assigned empty image to ImageActiveXControl.");
                            }
                            // Additional control‑type handling can be added here.
                        }
                    }
                }

                // Save the modified diagram to the output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}