using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        // Folder containing Visio files to benchmark
        string inputFolder = @"C:\VisioDiagrams";
        // Output folder for modified diagrams
        string outputFolder = @"C:\VisioDiagrams\Modified";

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Process each .vsdx file in the folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            try
            {
                TimeSpan duration = ProcessDiagram(filePath, outputFolder);
                Console.WriteLine($"File: {Path.GetFileName(filePath)} - Time elapsed: {duration.TotalMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Console.WriteLine("Benchmark completed.");
    }

    /// <summary>
    /// Loads a diagram, retrieves all ActiveX controls, modifies a property, saves the diagram,
    /// and returns the time taken for the retrieve‑modify operation.
    /// </summary>
    static TimeSpan ProcessDiagram(string inputPath, string outputFolder)
    {
        // Load the diagram from file
        Diagram diagram = new Diagram(inputPath);

        Stopwatch sw = Stopwatch.StartNew();

        // Iterate through all pages
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Check if the shape contains an ActiveX control
                if (shape.ActiveXControl != null)
                {
                    // Determine the control type
                    ControlType ctrlType = shape.ActiveXControl.Type;

                    // Example modification for CommandButton controls
                    if (ctrlType == ControlType.CommandButton)
                    {
                        CommandButtonActiveXControl btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                        // Update the caption to indicate modification
                        btn.Caption = $"Modified {DateTime.Now:HHmmss}";
                    }
                    // Example modification for CheckBox controls
                    else if (ctrlType == ControlType.CheckBox)
                    {
                        CheckBoxActiveXControl chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                        // Toggle the checked state
                        chk.Value = chk.Value == CheckValueType.Checked ? (CheckValueType)0 : CheckValueType.Checked;
                    }
                    // Example modification for TextBox controls
                    else if (ctrlType == ControlType.TextBox)
                    {
                        TextBoxActiveXControl txt = (TextBoxActiveXControl)shape.ActiveXControl;
                        txt.Text = $"Updated at {DateTime.Now}";
                    }
                    // Add other control types as needed
                }
            }
        }

        sw.Stop();

        // Save the modified diagram with a new name
        string fileName = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(outputFolder, $"{fileName}_modified.vsdx");
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        // No explicit disposal needed; Diagram implements IDisposable but disposal is optional in this context
        return sw.Elapsed;
    }
}
