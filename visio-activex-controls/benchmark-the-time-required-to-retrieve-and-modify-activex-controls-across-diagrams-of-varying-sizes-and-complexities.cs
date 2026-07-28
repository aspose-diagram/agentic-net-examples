using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files to benchmark
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Get all Visio files (VSDX) in the folder
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        foreach (string filePath in diagramFiles)
        {
            try
            {
                long elapsedMs = ProcessDiagram(filePath);
                Console.WriteLine($"File: {Path.GetFileName(filePath)} - Retrieval & Modification Time: {elapsedMs} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }

    // Loads a diagram, retrieves all ActiveX controls, modifies a property, and returns elapsed time in milliseconds
    static long ProcessDiagram(string filePath)
    {
        // Load the diagram
        Diagram diagram = new Diagram(filePath);

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
                    // Determine the control type and modify a property accordingly
                    switch (shape.ActiveXControl.Type)
                    {
                        case ControlType.CommandButton:
                            var cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                            cmdBtn.Caption = "Modified";
                            break;

                        case ControlType.CheckBox:
                            var chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                            // Set to checked; to represent unchecked use (CheckValueType)0
                            chkBox.Value = CheckValueType.Checked;
                            break;

                        case ControlType.TextBox:
                            var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                            txtBox.Text = "Modified";
                            break;

                        // Add other control types as needed
                        default:
                            // No modification for other control types
                            break;
                    }
                }
            }
        }

        sw.Stop();

        // Save the modified diagram to a new file (optional)
        string outputPath = Path.Combine(Path.GetDirectoryName(filePath),
                                         Path.GetFileNameWithoutExtension(filePath) + "_modified.vsdx");
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        // Return elapsed milliseconds
        return sw.ElapsedMilliseconds;
    }
}
