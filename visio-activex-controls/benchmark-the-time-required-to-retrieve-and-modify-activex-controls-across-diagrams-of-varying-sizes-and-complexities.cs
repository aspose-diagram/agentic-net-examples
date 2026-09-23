using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files to benchmark
        string inputFolder = @"C:\VisioFiles";
        // Folder to save modified files
        string outputFolder = @"C:\VisioFiles\Modified";

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        string[] files = Directory.GetFiles(inputFolder, "*.vsdx");
        foreach (string filePath in files)
        {
            Console.WriteLine($"Processing: {Path.GetFileName(filePath)}");
            ProcessDiagram(filePath, outputFolder);
        }

        Console.WriteLine("Benchmark completed.");
    }

    static void ProcessDiagram(string filePath, string outputFolder)
    {
        // Load diagram
        Diagram diagram = new Diagram(filePath);

        // Stopwatch for retrieval
        Stopwatch retrievalSw = Stopwatch.StartNew();

        // Collect all shapes that contain ActiveX controls
        var activeXShapes = new System.Collections.Generic.List<Shape>();
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.ActiveXControl != null)
                {
                    activeXShapes.Add(shape);
                }
            }
        }

        retrievalSw.Stop();
        Console.WriteLine($"  Retrieval time: {retrievalSw.ElapsedMilliseconds} ms (found {activeXShapes.Count} controls)");

        // Stopwatch for modification
        Stopwatch modifySw = Stopwatch.StartNew();

        foreach (Shape shape in activeXShapes)
        {
            // Determine control type and modify a representative property
            switch (shape.ActiveXControl.Type)
            {
                case ControlType.CommandButton:
                    var cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                    cmdBtn.Caption = "Modified";
                    break;

                case ControlType.CheckBox:
                    var chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                    chkBox.Value = CheckValueType.Checked;
                    break;

                case ControlType.TextBox:
                    var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                    txtBox.Text = "Modified";
                    break;

                case ControlType.SpinButton:
                    var spinBtn = (SpinButtonActiveXControl)shape.ActiveXControl;
                    spinBtn.Position = 10;
                    break;

                case ControlType.Image:
                    var imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                    // Example: clear image data (set empty byte array)
                    imgCtrl.Picture = new byte[0];
                    break;

                // Add other control types as needed
                default:
                    // No modification for unhandled types
                    break;
            }
        }

        modifySw.Stop();
        Console.WriteLine($"  Modification time: {modifySw.ElapsedMilliseconds} ms");

        // Save modified diagram
        string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + "_mod.vsdx");
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"  Saved modified diagram to: {outputPath}");
    }
}
