using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file (read‑only version)
                string outputPath = "output_readonly.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            // Disable user interaction by hiding control handles
                            // (NoCtlHandles is a BoolValue; set its Value to TRUE)
                            shape.Misc.NoCtlHandles.Value = BOOL.True;

                            // Prevent the control from being printed (optional)
                            shape.Misc.NonPrinting.Value = BOOL.True;

                            // Clear double‑click event to avoid any scripted actions
                            shape.Event.EventDblClick.Ufe.F = "";
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }