using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the first non‑deleted shape to work with.
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
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found in the diagram.");
                    return;
                }

                // Add two new connection points to the shape.
                Connection conn1 = new Connection();
                conn1.X.Ufe.F = "Width*0.33";
                conn1.Y.Ufe.F = "Height*0";
                targetShape.Connections.Add(conn1);

                Connection conn2 = new Connection();
                conn2.X.Ufe.F = "Width*0.66";
                conn2.Y.Ufe.F = "Height*0";
                targetShape.Connections.Add(conn2);

                // Verify that each connection point exists and is correctly typed.
                int connectionIndex = 0;
                foreach (Connection conn in targetShape.Connections)
                {
                    connectionIndex++;

                    // Ensure the connection object itself is not null.
                    if (conn == null)
                    {
                        throw new Exception($"Connection #{connectionIndex} is null.");
                    }

                    // Verify X and Y cells are present.
                    if (conn.X == null || conn.Y == null)
                    {
                        throw new Exception($"Connection #{connectionIndex} missing X or Y cell.");
                    }

                    // Verify the Ufe (formula) objects are present.
                    if (conn.X.Ufe == null || conn.Y.Ufe == null)
                    {
                        throw new Exception($"Connection #{connectionIndex} missing Ufe (formula) objects.");
                    }

                    // Verify that the formula strings are not empty.
                    if (string.IsNullOrEmpty(conn.X.Ufe.F) || string.IsNullOrEmpty(conn.Y.Ufe.F))
                    {
                        throw new Exception($"Connection #{connectionIndex} has an empty formula.");
                    }

                    // Output verification details.
                    Console.WriteLine($"Connection #{connectionIndex}: X = {conn.X.Ufe.F}, Y = {conn.Y.Ufe.F}");
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}