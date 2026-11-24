using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the Visio file to process.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // If the shape contains any fields, remove them.
                        // Iterate backwards because removing shifts indices.
                        for (int i = shape.Fields.Count - 1; i >= 0; i--)
                        {
                            // Remove the field at index i.
                            shape.Fields.RemoveAt(i);
                        }

                        // Example of inserting a new field could be added here
                        // if the appropriate API were known. This placeholder
                        // demonstrates where such logic would reside.
                        // // shape.Fields.Add(new Field(...));
                    }
                }

                // Determine the appropriate SaveFileFormat based on the original file extension.
                SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(inputPath));

                // Save the modified diagram back to its original location, overwriting the file.
                diagram.Save(inputPath, format);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding SaveFileFormat enum value.
    /// Defaults to Vsdx if the extension is unrecognized.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        // Normalize the extension to lower case without the leading dot.
        string ext = extension?.TrimStart('.').ToLowerInvariant();

        return ext switch
        {
            "vsdx" => SaveFileFormat.Vsdx,
            "vsd"  => SaveFileFormat.Vsd,
            "vdx"  => SaveFileFormat.Vdx,
            "vsx"  => SaveFileFormat.Vsx,
            "vtx"  => SaveFileFormat.Vtx,
            "vssx" => SaveFileFormat.Vssx,
            "vstx" => SaveFileFormat.Vstx,
            "vsdm" => SaveFileFormat.Vsdm,
            "vssm" => SaveFileFormat.Vssm,
            "vstm" => SaveFileFormat.Vstm,
            "pdf"  => SaveFileFormat.Pdf,
            "png"  => SaveFileFormat.Png,
            "jpeg" => SaveFileFormat.Jpeg,
            "jpg"  => SaveFileFormat.Jpeg,
            "svg"  => SaveFileFormat.Svg,
            "html" => SaveFileFormat.Html,
            "swf"  => SaveFileFormat.Swf,
            "xps"  => SaveFileFormat.Xps,
            _ => SaveFileFormat.Vsdx // Default fallback format.
        };
    }
}