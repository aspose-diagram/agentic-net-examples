using System;
using Aspose.Diagram;
using System.IO;
using System.IO.Compression;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioPath> <outputZipPath>");
            return;
        }

        string inputPath = args[0];
        string outputZipPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Create a temporary folder to store DXF files
        string tempDir = Path.Combine(Path.GetTempPath(), "VisioDxfExport_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Iterate through each page and each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Generate a minimal DXF representation (placeholder)
                    string dxfContent = GenerateDxfContent(shape, page);

                    // Build a safe file name: PageName_ShapeID.dxf
                    string safePageName = MakeFileSystemSafe(page.NameU);
                    string fileName = $"{safePageName}_Shape{shape.ID}.dxf";
                    string filePath = Path.Combine(tempDir, fileName);

                    File.WriteAllText(filePath, dxfContent);
                }
            }

            // Create a ZIP archive containing all DXF files
            if (File.Exists(outputZipPath))
                File.Delete(outputZipPath);

            ZipFile.CreateFromDirectory(tempDir, outputZipPath);
            Console.WriteLine($"DXF files have been archived to: {outputZipPath}");
        }
        finally
        {
            // Clean up the temporary directory
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    // Generates a simple DXF file content for a shape.
    // Real geometry extraction would require a dedicated DXF exporter,
    // which Aspose.Diagram does not provide. This placeholder writes a POINT entity at the shape's location.
    static string GenerateDxfContent(Shape shape, Page page)
    {
        var sb = new StringBuilder();

        sb.AppendLine("0");
        sb.AppendLine("SECTION");
        sb.AppendLine("2");
        sb.AppendLine("HEADER");
        sb.AppendLine("0");
        sb.AppendLine("ENDSEC");
        sb.AppendLine("0");
        sb.AppendLine("SECTION");
        sb.AppendLine("2");
        sb.AppendLine("ENTITIES");

        // POINT entity representing the shape's PinX, PinY coordinates
        sb.AppendLine("0");
        sb.AppendLine("POINT");
        sb.AppendLine("8");
        sb.AppendLine("0"); // layer
        sb.AppendLine("10");
        sb.AppendLine(shape.XForm.PinX.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        sb.AppendLine("20");
        sb.AppendLine(shape.XForm.PinY.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        sb.AppendLine("30");
        sb.AppendLine("0.0");

        sb.AppendLine("0");
        sb.AppendLine("ENDSEC");
        sb.AppendLine("0");
        sb.AppendLine("EOF");

        return sb.ToString();
    }

    // Replaces characters that are invalid in file names with underscores
    static string MakeFileSystemSafe(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}
