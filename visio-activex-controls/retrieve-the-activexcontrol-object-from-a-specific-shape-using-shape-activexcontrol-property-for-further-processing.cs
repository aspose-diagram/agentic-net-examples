using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check diagram file existence.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse shape ID (expected to be a long integer).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. Must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Retrieve the first page (you can adjust to target a specific page if needed).
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape found with ID {shapeId} on page 0.");
                return;
            }

            // Access the ActiveXControl associated with the shape.
            ActiveXControl activeX = shape.ActiveXControl;
            if (activeX == null)
            {
                Console.Error.WriteLine($"Info: Shape ID {shapeId} does not contain an ActiveX control.");
                return;
            }

            // Output the generic control type.
            Console.WriteLine($"ActiveX Control Type: {activeX.Type}");

            // Process based on specific control type.
            switch (activeX.Type)
            {
                case ControlType.CommandButton:
                    // Cast to CommandButtonActiveXControl to access button-specific properties.
                    CommandButtonActiveXControl button = (CommandButtonActiveXControl)activeX;
                    Console.WriteLine($"Original Caption: {button.Caption}");

                    // Example modification: change the caption.
                    button.Caption = "Updated Caption";
                    Console.WriteLine($"Updated Caption: {button.Caption}");
                    break;

                case ControlType.CheckBox:
                    // Cast to CheckBoxActiveXControl to access checkbox-specific properties.
                    CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)activeX;
                    Console.WriteLine($"CheckBox IsChecked: {checkBox.IsChecked}");

                    // Example modification: toggle the checked state.
                    checkBox.IsChecked = !checkBox.IsChecked;
                    Console.WriteLine($"Toggled IsChecked: {checkBox.IsChecked}");
                    break;

                case ControlType.Image:
                    // Cast to ImageActiveXControl to access image-specific properties.
                    ImageActiveXControl imageCtrl = (ImageActiveXControl)activeX;
                    Console.WriteLine($"Image control has picture data length: {imageCtrl.Picture?.Length ?? 0} bytes");
                    // Example: replace image with a new file (if desired).
                    // string newImagePath = "newImage.png";
                    // if (File.Exists(newImagePath))
                    // {
                    //     imageCtrl.Picture = File.ReadAllBytes(newImagePath);
                    //     Console.WriteLine("Image picture replaced.");
                    // }
                    break;

                // Add handling for other control types as needed.
                default:
                    Console.WriteLine("No specific handling implemented for this control type.");
                    break;
            }

            // Save the modified diagram to a new file.
            string outputDir = Path.GetDirectoryName(diagramPath) ?? Directory.GetCurrentDirectory();
            string outputFileName = Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx";
            string outputPath = Path.Combine(outputDir, outputFileName);

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or general exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}