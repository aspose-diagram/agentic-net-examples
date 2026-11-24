using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least one diagram file path is supplied.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide at least one diagram file path as an argument.");
            return;
        }

        foreach (string inputPath in args)
        {
            // Guard: verify the file exists before processing.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Start timing the retrieval and modification of ActiveX controls.
                    Stopwatch sw = Stopwatch.StartNew();

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the ActiveX control attached to the shape, if any.
                            ActiveXControl activeX = shape.ActiveXControl;
                            if (activeX == null)
                                continue;

                            // Modify properties based on the control type.
                            switch (activeX.Type)
                            {
                                case ControlType.CommandButton:
                                    // Cast to the concrete CommandButton control.
                                    CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)activeX;
                                    // Update the caption.
                                    cmdBtn.Caption = "Updated Caption";
                                    break;

                                case ControlType.TextBox:
                                    // Cast to the concrete TextBox control.
                                    TextBoxActiveXControl txtBox = (TextBoxActiveXControl)activeX;
                                    // Update the displayed text.
                                    txtBox.Text = "Updated Text";
                                    break;

                                case ControlType.CheckBox:
                                    // Cast to the concrete CheckBox control.
                                    CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)activeX;
                                    // Set the checked state using the CheckValueType enum.
                                    chkBox.Value = CheckValueType.Checked;
                                    break;

                                case ControlType.SpinButton:
                                    // Cast to the concrete SpinButton control.
                                    SpinButtonActiveXControl spinBtn = (SpinButtonActiveXControl)activeX;
                                    // Set a new position/value.
                                    spinBtn.Position = 5;
                                    break;

                                // Additional control types can be handled here as needed.
                                default:
                                    // No modification for unsupported control types.
                                    break;
                            }
                        }
                    }

                    sw.Stop();

                    // Output the benchmark result for the current diagram.
                    Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' in {sw.ElapsedMilliseconds} ms.");

                    // Save the modified diagram to a new file to avoid overwriting the original.
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(inputPath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Modified diagram saved to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading or processing.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}