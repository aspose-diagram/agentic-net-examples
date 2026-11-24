using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the first non-deleted shape on the first page
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found in the diagram.");
                    return;
                }

                // Add new connection points to the shape
                // First connection point at the middle of the top edge
                Connection conn1 = new Connection();
                conn1.X.Ufe.F = "Width*0.5";
                conn1.Y.Ufe.F = "Height*0";
                targetShape.Connections.Add(conn1);

                // Second connection point at the middle of the bottom edge
                Connection conn2 = new Connection();
                conn2.X.Ufe.F = "Width*0.5";
                conn2.Y.Ufe.F = "Height*1";
                targetShape.Connections.Add(conn2);

                // Verify that all connection points exist and are correctly typed
                int index = 0;
                foreach (Connection conn in targetShape.Connections)
                {
                    index++;

                    // Ensure the connection object itself is not null
                    if (conn == null)
                    {
                        throw new Exception($"Connection #{index} is null.");
                    }

                    // Verify X and Y cells are present
                    if (conn.X == null)
                    {
                        throw new Exception($"Connection #{index} has a null X cell.");
                    }
                    if (conn.Y == null)
                    {
                        throw new Exception($"Connection #{index} has a null Y cell.");
                    }

                    // Verify that the formula strings are not empty
                    string xFormula = conn.X.Ufe.F;
                    string yFormula = conn.Y.Ufe.F;

                    if (string.IsNullOrEmpty(xFormula))
                    {
                        throw new Exception($"Connection #{index} X formula is empty.");
                    }
                    if (string.IsNullOrEmpty(yFormula))
                    {
                        throw new Exception($"Connection #{index} Y formula is empty.");
                    }

                    // Output verification result for this connection point
                    Console.WriteLine($"Connection #{index}: X = \"{xFormula}\", Y = \"{yFormula}\" verified.");
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}