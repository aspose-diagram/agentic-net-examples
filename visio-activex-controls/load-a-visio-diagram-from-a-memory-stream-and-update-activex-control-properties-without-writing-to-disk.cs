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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Load the Visio file into a memory stream.
        byte[] fileBytes;
        try
        {
            fileBytes = File.ReadAllBytes(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file into memory: {ex.Message}");
            return;
        }

        using (MemoryStream inputStream = new MemoryStream(fileBytes))
        {
            // Load the diagram from the memory stream.
            try
            {
                using (Diagram diagram = new Diagram(inputStream))
                {
                    // Iterate through all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        for (int i = 0; i < page.Shapes.Count; i++)
                        {
                            Shape shape = page.Shapes[i];

                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check whether the shape hosts an ActiveX control.
                            if (shape.ActiveXControl != null)
                            {
                                // Determine the specific control type.
                                ControlType ctrlType = shape.ActiveXControl.Type;

                                if (ctrlType == ControlType.TextBox)
                                {
                                    // Cast to TextBoxActiveXControl and update its text.
                                    TextBoxActiveXControl txtCtrl = (TextBoxActiveXControl)shape.ActiveXControl;
                                    txtCtrl.Text = "Updated Text via Aspose.Diagram";
                                    // No IsEnabled property (bool) is set here to avoid BOOL conversion errors.
                                }
                                else if (ctrlType == ControlType.CheckBox)
                                {
                                    // Cast to CheckBoxActiveXControl and set its checked state.
                                    CheckBoxActiveXControl chkCtrl = (CheckBoxActiveXControl)shape.ActiveXControl;
                                    chkCtrl.Value = CheckValueType.Checked; // Mark as checked.
                                }
                                else if (ctrlType == ControlType.Image)
                                {
                                    // Cast to ImageActiveXControl and assign a placeholder picture.
                                    ImageActiveXControl imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                    imgCtrl.Picture = new byte[0]; // Empty byte array as placeholder.
                                }
                                // Additional control types can be handled similarly.
                            }
                        }
                    }

                    // Save the modified diagram to a memory stream (no disk I/O).
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        diagram.Save(outputStream, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Diagram successfully updated and saved to memory. Output size: {outputStream.Length} bytes.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
                return;
            }
        }
    }
}