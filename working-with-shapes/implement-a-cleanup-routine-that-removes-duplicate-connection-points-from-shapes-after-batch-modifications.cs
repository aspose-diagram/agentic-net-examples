using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output_cleaned.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Connections != null && shape.Connections.Count > 1)
                    {
                        var connections = shape.Connections;
                        var seen = new List<(string X, string Y)>();
                        var duplicates = new List<Connection>();

                        for (int i = 0; i < connections.Count; i++)
                        {
                            var conn = connections[i];
                            string xFormula = conn.X.Ufe.F;
                            string yFormula = conn.Y.Ufe.F;

                            if (seen.Exists(s => s.X == xFormula && s.Y == yFormula))
                            {
                                duplicates.Add(conn);
                            }
                            else
                            {
                                seen.Add((xFormula, yFormula));
                            }
                        }

                        foreach (var dup in duplicates)
                        {
                            connections.Remove(dup);
                        }
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}