using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define diagram files to process. Adjust paths as needed.
                string[] diagramFiles = new string[]
                {
                    "DiagramSmall.vsdx",
                    "DiagramMedium.vsdx",
                    "DiagramLarge.vsdx"
                };

                foreach (string filePath in diagramFiles)
                {
                    // Measure total time for loading, processing, and saving the diagram.
                    Stopwatch sw = Stopwatch.StartNew();

                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape contains an ActiveX control.
                            if (shape.ActiveXControl != null)
                            {
                                // Determine the control type and modify a property accordingly.
                                if (shape.ActiveXControl.Type == ControlType.CommandButton)
                                {
                                    // Cast to the specific control class.
                                    CommandButtonActiveXControl btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                    // Update the caption text.
                                    btn.Caption = "Updated";
                                }
                                else if (shape.ActiveXControl.Type == ControlType.CheckBox)
                                {
                                    // Example for a CheckBox control.
                                    CheckBoxActiveXControl chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                                    // Toggle the check state.
                                    chk.Value = CheckValueType.Checked;
                                }
                                // Add additional control types as needed.
                            }
                        }
                    }

                    // Optionally save the modified diagram to a new file.
                    string outputPath = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(filePath),
                        System.IO.Path.GetFileNameWithoutExtension(filePath) + "_Modified.vsdx");

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    sw.Stop();

                    Console.WriteLine($"Processed '{filePath}' in {sw.Elapsed.TotalMilliseconds} ms. Output saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }