using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            bool controlModified = false;

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        try
                        {
                            // Determine the type of ActiveX control
                            if (shape.ActiveXControl.Type == ControlType.CheckBox)
                            {
                                // Cast to the specific control class
                                CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                                // Attempt to set the checkbox value.
                                // This operation may require specific permissions and can throw COMException.
                                checkBox.Value = CheckValueType.Checked;

                                Console.WriteLine($"Checked CheckBox control on shape ID {shape.ID}.");
                                controlModified = true;
                            }
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID} contains ActiveX control of type {shape.ActiveXControl.Type}, no action taken.");
                            }
                        }
                        catch (COMException comEx)
                        {
                            // Handle permission-related COM exceptions
                            Console.Error.WriteLine($"COMException accessing ActiveX control on shape ID {shape.ID}: {comEx.Message}");
                        }
                        catch (Exception ex)
                        {
                            // Handle any other unexpected errors for this shape
                            Console.Error.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }
            }

            // Save the modified diagram if any control was changed
            if (controlModified)
            {
                string outputDir = Path.GetDirectoryName(diagramPath);
                string outputFileName = Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx";
                string outputPath = Path.Combine(outputDir, outputFileName);

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
            else
            {
                Console.WriteLine("No ActiveX controls were modified.");
            }
        }
        catch (Exception ex)
        {
            // General error handling for diagram loading/saving
            Console.Error.WriteLine($"Failed to process diagram: {ex.Message}");
        }
    }
}