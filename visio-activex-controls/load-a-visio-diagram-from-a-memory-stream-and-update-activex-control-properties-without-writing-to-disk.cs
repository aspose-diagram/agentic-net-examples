using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio file into a memory stream.
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream inputStream = new MemoryStream(fileBytes))
            {
                // Load the diagram from the memory stream.
                using (Diagram diagram = new Diagram(inputStream))
                {
                    // Iterate through all pages in the diagram.
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        // Iterate through all shapes on the current page.
                        for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                        {
                            Shape shape = page.Shapes[shapeIndex];

                            // Check whether the shape contains an ActiveX control.
                            if (shape.ActiveXControl != null)
                            {
                                // Determine the type of the ActiveX control.
                                ControlType ctrlType = shape.ActiveXControl.Type;

                                // Update properties based on the specific control type.
                                if (ctrlType == ControlType.CommandButton)
                                {
                                    // Cast to the concrete CommandButtonActiveXControl class.
                                    CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                                    // Set a new caption for the command button.
                                    button.Caption = "Updated Caption";

                                    Console.WriteLine($"CommandButton caption updated on shape ID {shape.ID}.");
                                }
                                else if (ctrlType == ControlType.TextBox)
                                {
                                    // Cast to the concrete TextBoxActiveXControl class.
                                    TextBoxActiveXControl textBox = (TextBoxActiveXControl)shape.ActiveXControl;

                                    // Set new text content for the text box.
                                    textBox.Text = "New Text Content";

                                    Console.WriteLine($"TextBox text updated on shape ID {shape.ID}.");
                                }
                                // Additional control types can be handled here as needed.
                            }
                        }
                    }

                    // Optionally save the modified diagram back to a memory stream (no disk I/O).
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        diagram.Save(outputStream, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Diagram processed successfully. Output size: {outputStream.Length} bytes.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}