using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Search for a user‑defined cell named "EventCalc"
                    User eventCalcUser = null;
                    foreach (User user in shape.Users)
                    {
                        if (user.Name == "EventCalc")
                        {
                            eventCalcUser = user;
                            break;
                        }
                    }

                    // If the cell exists, evaluate its value and apply formatting
                    if (eventCalcUser != null)
                    {
                        // Attempt to parse the cell value as a double
                        if (double.TryParse(eventCalcUser.Value.Val, out double result))
                        {
                            // Conditional formatting based on the numeric result
                            if (result > 100)
                            {
                                // High values → red fill
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                            else
                            {
                                // Low values → green fill
                                shape.Fill.FillForegnd.Value = "#00FF00";
                            }

                            // Set a common line color (black) for visibility
                            shape.Line.LineColor.Value = "#000000";
                        }
                    }
                }
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
