using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the LocPinX and LocPinY formula strings
                        string locPinXFormula = shape.XForm.LocPinX.Ufe.F;
                        string locPinYFormula = shape.XForm.LocPinY.Ufe.F;

                        // Try to parse the formulas as numeric values
                        bool isLocPinXNumeric = double.TryParse(locPinXFormula, out double locPinXValue);
                        bool isLocPinYNumeric = double.TryParse(locPinYFormula, out double locPinYValue);

                        if (!isLocPinXNumeric || !isLocPinYNumeric)
                        {
                            // If either value is not numeric, skip calculation for this shape
                            Console.WriteLine($"Shape ID {shape.ID} has non-numeric LocPin values. Skipping.");
                            continue;
                        }

                        // Perform absolute pin calculation
                        // Absolute PinX = PinX + LocPinX
                        // Absolute PinY = PinY + LocPinY
                        double absolutePinX = shape.XForm.PinX.Value + locPinXValue;
                        double absolutePinY = shape.XForm.PinY.Value + locPinYValue;

                        // Update the shape's PinX and PinY to the absolute values
                        shape.XForm.PinX.Value = absolutePinX;
                        shape.XForm.PinY.Value = absolutePinY;

                        // Reset local pins to zero after conversion
                        shape.XForm.LocPinX.Value = 0.0;
                        shape.XForm.LocPinY.Value = 0.0;

                        Console.WriteLine($"Shape ID {shape.ID} pin updated to ({absolutePinX}, {absolutePinY}).");
                    }
                }

                // Save the modified diagram (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }