using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a folder path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Process each Visio file in the folder (currently handling .vsdx files).
        string[] files = Directory.GetFiles(folderPath, "*.vsdx");
        foreach (string filePath in files)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block to ensure proper disposal.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page explicitly typed as Page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Define pentagon geometry (centered at (5,5) inches, radius 1 inch).
                        double centerX = 5.0;
                        double centerY = 5.0;
                        double radius = 1.0;
                        double[] points = new double[12]; // 5 vertices + repeat first = 6 points * 2 coordinates

                        for (int i = 0; i < 5; i++)
                        {
                            // Angle starts at -90 degrees (top) and steps by 72 degrees.
                            double angle = -Math.PI / 2 + i * 2 * Math.PI / 5;
                            double x = centerX + radius * Math.Cos(angle);
                            double y = centerY + radius * Math.Sin(angle);
                            points[i * 2] = x;
                            points[i * 2 + 1] = y;
                        }

                        // Close the polygon by repeating the first point.
                        points[10] = points[0];
                        points[11] = points[1];

                        // Draw the pentagon on the current page.
                        page.DrawPolyline(points);
                    }

                    // Prepare output file path (original name with suffix).
                    string directory = Path.GetDirectoryName(filePath);
                    string filenameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_pentagon.vsdx");

                    // Save the modified diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Write any Aspose or IO errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}