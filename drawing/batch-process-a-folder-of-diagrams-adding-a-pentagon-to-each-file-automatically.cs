using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the folder containing Visio diagrams: ");
            folderPath = Console.ReadLine();
        }

        // Guard: folder must exist.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Supported Visio file extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

        // Get all files with supported extensions.
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(supportedExtensions, ext) < 0)
            {
                // Skip unsupported files.
                continue;
            }

            // Guard: ensure the file exists (should always be true here).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page and add a pentagon.
                foreach (Page page in diagram.Pages)
                {
                    // Define pentagon vertices (centered at (5,5) inches, radius 1 inch).
                    // Points are defined in the order: top, top-right, bottom-right, bottom-left, top-left, and back to top to close the shape.
                    double centerX = 5.0;
                    double centerY = 5.0;
                    double radius = 1.0;
                    double angle = -Math.PI / 2; // start at top point
                    double[] points = new double[12]; // 5 points + repeat first = 6 points * 2 coordinates
                    for (int i = 0; i < 5; i++)
                    {
                        double x = centerX + radius * Math.Cos(angle + i * 2 * Math.PI / 5);
                        double y = centerY + radius * Math.Sin(angle + i * 2 * Math.PI / 5);
                        points[i * 2] = x;
                        points[i * 2 + 1] = y;
                    }
                    // Repeat the first point to close the polygon.
                    points[10] = points[0];
                    points[11] = points[1];

                    // Draw the pentagon as a closed polyline.
                    page.DrawPolyline(points);
                }

                // Determine appropriate SaveFileFormat based on original extension.
                SaveFileFormat format = GetSaveFileFormat(ext);
                if (format == 0)
                {
                    Console.Error.WriteLine($"Unsupported file format for saving: {filePath}");
                    diagram.Dispose();
                    continue;
                }

                // Overwrite the original file with the modified diagram.
                diagram.Save(filePath, format);
                diagram.Dispose();

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum.
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vstm": return SaveFileFormat.Vstm;
            default: return 0; // Unknown/unsupported
        }
    }
}