using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file after processing
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the formula strings for LocPinX and LocPinY
                        string locPinXFormula = shape.XForm.LocPinX.Ufe.F;
                        string locPinYFormula = shape.XForm.LocPinY.Ufe.F;

                        // Try to parse the formulas as numeric values
                        bool isLocPinXNumeric = double.TryParse(locPinXFormula, out double locPinXValue);
                        bool isLocPinYNumeric = double.TryParse(locPinYFormula, out double locPinYValue);

                        // If either cell does not contain a numeric value, report and skip calculation
                        if (!isLocPinXNumeric || !isLocPinYNumeric)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has non‑numeric LocPin values.");
                            Console.WriteLine($"  LocPinX: '{locPinXFormula}'  LocPinY: '{locPinYFormula}'");
                            continue; // Skip absolute pin calculation for this shape
                        }

                        // Perform absolute pin calculation (example: set PinX/Y to the numeric LocPin values)
                        shape.XForm.PinX.Value = locPinXValue;
                        shape.XForm.PinY.Value = locPinYValue;
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