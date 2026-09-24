using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (command‑line argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to collect report lines for shapes with broken data links
        List<string> brokenShapesReport = new List<string>();

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine whether the shape has any Data* fields populated
                    bool hasDataLink = !string.IsNullOrWhiteSpace(shape.Data1) ||
                                       !string.IsNullOrWhiteSpace(shape.Data2) ||
                                       !string.IsNullOrWhiteSpace(shape.Data3);

                    if (!hasDataLink)
                        continue; // No data fields, move to next shape

                    // If the diagram contains no data connections, any populated Data* field is considered broken
                    if (diagram.DataConnections.Count == 0)
                    {
                        brokenShapesReport.Add(
                            $"Page: \"{page.NameU}\", Shape ID: {shape.ID}, Name: \"{shape.NameU}\", Data1: \"{shape.Data1}\", Data2: \"{shape.Data2}\", Data3: \"{shape.Data3}\"");
                    }
                    else
                    {
                        // Verify that at least one Data* value references an existing data connection
                        bool referenceValid = false;
                        foreach (DataConnection conn in diagram.DataConnections)
                        {
                            // Use the connection's Command or ConnectionString as a reference string (Name property does not exist)
                            string reference = !string.IsNullOrWhiteSpace(conn.Command) ? conn.Command : conn.ConnectionString;

                            if (!string.IsNullOrWhiteSpace(reference) &&
                                (shape.Data1?.Contains(reference) == true ||
                                 shape.Data2?.Contains(reference) == true ||
                                 shape.Data3?.Contains(reference) == true))
                            {
                                referenceValid = true;
                                break;
                            }
                        }

                        // If no valid reference was found, record the shape as having a broken link
                        if (!referenceValid)
                        {
                            brokenShapesReport.Add(
                                $"Page: \"{page.NameU}\", Shape ID: {shape.ID}, Name: \"{shape.NameU}\", Data1: \"{shape.Data1}\", Data2: \"{shape.Data2}\", Data3: \"{shape.Data3}\"");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred while loading or processing the diagram
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Output the report to the console
        Console.WriteLine("=== Broken Data Link Report ===");
        if (brokenShapesReport.Count == 0)
        {
            Console.WriteLine("No broken data links were detected.");
        }
        else
        {
            foreach (string line in brokenShapesReport)
                Console.WriteLine(line);
        }

        // Attempt to write the report to a text file
        string reportPath = "BrokenDataLinksReport.txt";
        try
        {
            File.WriteAllLines(reportPath, brokenShapesReport);
            Console.WriteLine($"\nReport saved to: {Path.GetFullPath(reportPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFailed to write report file: {ex.Message}");
        }
    }
}