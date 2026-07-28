using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only group shapes
                        if (shape.Type == TypeValue.Group)
                        {
                            // Absolute position of the group shape
                            double groupPinX = shape.XForm.PinX.Value;
                            double groupPinY = shape.XForm.PinY.Value;

                            Console.WriteLine($"Group Shape ID={shape.ID}, Name={shape.NameU}, PinX={groupPinX}, PinY={groupPinY}");

                            // Iterate through sub‑shapes within the group
                            foreach (Aspose.Diagram.Shape subShape in shape.Shapes)
                            {
                                // Skip deleted sub‑shapes
                                if (subShape.Del == BOOL.True)
                                    continue;

                                // Sub‑shape coordinates are relative to the group's origin.
                                // Approximate absolute coordinates by adding the group's PinX/PinY.
                                double absPinX = groupPinX + subShape.XForm.PinX.Value;
                                double absPinY = groupPinY + subShape.XForm.PinY.Value;

                                Console.WriteLine($"  Sub‑Shape ID={subShape.ID}, Name={subShape.NameU}, Absolute PinX={absPinX}, Absolute PinY={absPinY}");
                            }
                        }
                    }
                }

                // Optionally save the diagram (no changes made in this example)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }