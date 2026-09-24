using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the LocPinX and LocPinY formulas
                    string locPinXFormula = shape.XForm.LocPinX.Ufe.F;
                    string locPinYFormula = shape.XForm.LocPinY.Ufe.F;

                    // Validate that both formulas are numeric
                    if (!IsNumeric(locPinXFormula))
                    {
                        throw new Exception($"Shape ID {shape.ID} on page '{page.Name}' has a non-numeric LocPinX value: '{locPinXFormula}'.");
                    }

                    if (!IsNumeric(locPinYFormula))
                    {
                        throw new Exception($"Shape ID {shape.ID} on page '{page.Name}' has a non-numeric LocPinY value: '{locPinYFormula}'.");
                    }

                    // Perform absolute pin calculations (example logic)
                    double locPinX = double.Parse(locPinXFormula);
                    double locPinY = double.Parse(locPinYFormula);

                    // Adjust the absolute PinX and PinY based on LocPin values
                    shape.XForm.PinX.Value = shape.XForm.PinX.Value + locPinX;
                    shape.XForm.PinY.Value = shape.XForm.PinY.Value + locPinY;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to determine if a string represents a numeric value
    static bool IsNumeric(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        value = value.Trim();
        double result;
        return double.TryParse(value, out result);
    }
}
