using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Open the diagram in read‑only mode using a FileStream with FileAccess.Read
                using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    // Load the diagram from the read‑only stream
                    Diagram diagram = new Diagram(fs);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape contains an ActiveX control
                            if (shape.ActiveXControl != null)
                            {
                                // Disable interaction by clearing common event formulas
                                // EventDblClick – double‑click action
                                shape.Event.EventDblClick.Ufe.F = "FALSE";

                                // EventDrop – drag‑and‑drop action
                                shape.Event.EventDrop.Ufe.F = "FALSE";

                                // Additional events can be cleared similarly if needed
                            }
                        }
                    }

                    // Save the modified diagram (still in VSDX format)
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("ActiveX controls have been disabled and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }