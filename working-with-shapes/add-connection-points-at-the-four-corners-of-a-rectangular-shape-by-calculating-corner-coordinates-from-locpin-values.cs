using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify rectangular shapes by master name "Rectangle".
                        // Adjust this condition if your rectangle uses a different master.
                        if (shape.Master != null && shape.Master.NameU == "Rectangle")
                        {
                            // Calculate corner coordinates using LocPin, Width, Height, and Pin positions.
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;
                            double locPinX = shape.XForm.LocPinX.Value;
                            double locPinY = shape.XForm.LocPinY.Value;

                            // Left, Right, Bottom, Top positions.
                            double left = pinX - (width - locPinX);
                            double right = pinX + locPinX;
                            double bottom = pinY - locPinY;
                            double top = pinY + (height - locPinY);

                            // Add connection points at the four corners.
                            // Bottom‑Left
                            Connection cpBL = new Connection();
                            cpBL.X.Ufe.F = "0";
                            cpBL.Y.Ufe.F = "0";
                            shape.Connections.Add(cpBL);

                            // Bottom‑Right
                            Connection cpBR = new Connection();
                            cpBR.X.Ufe.F = "Width";
                            cpBR.Y.Ufe.F = "0";
                            shape.Connections.Add(cpBR);

                            // Top‑Left
                            Connection cpTL = new Connection();
                            cpTL.X.Ufe.F = "0";
                            cpTL.Y.Ufe.F = "Height";
                            shape.Connections.Add(cpTL);

                            // Top‑Right
                            Connection cpTR = new Connection();
                            cpTR.X.Ufe.F = "Width";
                            cpTR.Y.Ufe.F = "Height";
                            shape.Connections.Add(cpTR);
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}