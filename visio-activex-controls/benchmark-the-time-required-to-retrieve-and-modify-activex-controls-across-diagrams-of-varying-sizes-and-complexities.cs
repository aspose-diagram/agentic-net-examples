using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate that at least one diagram file path is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath1> [<diagramPath2> ...]");
            return;
        }

        // Process each diagram file supplied on the command line.
        foreach (string diagramPath in args)
        {
            // Guard against missing files.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    int totalActiveXCount = 0;
                    Stopwatch sw = Stopwatch.StartNew();

                    // Iterate through all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve the ActiveX control attached to the shape, if any.
                            if (shape.ActiveXControl != null)
                            {
                                totalActiveXCount++;

                                // Enable the control (property expects a native bool, not BOOL).
                                shape.ActiveXControl.IsEnabled = true;

                                // Determine the specific control type to apply type‑specific modifications.
                                ControlType ctrlType = shape.ActiveXControl.Type;
                                switch (ctrlType)
                                {
                                    case ControlType.CommandButton:
                                        // Cast to CommandButtonActiveXControl and modify its Caption.
                                        CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                        cmdBtn.Caption = "Updated Caption";
                                        break;

                                    case ControlType.TextBox:
                                        // Cast to TextBoxActiveXControl and modify its Text.
                                        TextBoxActiveXControl txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                        txtBox.Text = "Updated Text";
                                        break;

                                    case ControlType.Image:
                                        // Cast to ImageActiveXControl and clear its picture (example operation).
                                        ImageActiveXControl imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                        imgCtrl.Picture = new byte[0];
                                        break;

                                    // Additional control types can be handled here as needed.
                                    default:
                                        // No specific modification for unsupported control types.
                                        break;
                                }
                            }
                        }
                    }

                    sw.Stop();

                    // Output benchmark results for the current file.
                    Console.WriteLine($"File: {diagramPath}");
                    Console.WriteLine($"ActiveX controls processed: {totalActiveXCount}");
                    Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
                    Console.WriteLine(new string('-', 40));
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }
    }
}