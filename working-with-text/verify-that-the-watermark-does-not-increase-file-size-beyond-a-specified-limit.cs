using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and maximum allowed size increase in bytes
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <maxSizeIncreaseBytes>");
            return;
        }

        string inputPath = args[0];
        string limitArg = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse size limit
        if (!long.TryParse(limitArg, out long maxIncreaseBytes) || maxIncreaseBytes < 0)
        {
            Console.Error.WriteLine("Invalid size increase limit. It must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page watermark using the correct AddText overload (positional arguments)
                // Font size is specified in inches (e.g., 0.25 inches ≈ 18 points)
                page.AddText(
                    0,               // pinX
                    0,               // pinY
                    pageWidth,       // width
                    pageHeight,      // height
                    "Watermark",     // text
                    "Calibri",       // fontName
                    "#a5a5a5",       // fontColor (hex)
                    0.25);           // fontSize (in inches)

                // Save the modified diagram to a temporary file
                string tempPath = Path.Combine(Path.GetTempPath(), $"watermarked_{Guid.NewGuid()}.vsdx");
                diagram.Save(tempPath, SaveFileFormat.Vsdx);

                // Compare file sizes
                long originalSize = new FileInfo(inputPath).Length;
                long newSize = new FileInfo(tempPath).Length;
                long sizeIncrease = newSize - originalSize;

                Console.WriteLine($"Original size: {originalSize} bytes");
                Console.WriteLine($"Watermarked size: {newSize} bytes");
                Console.WriteLine($"Size increase: {sizeIncrease} bytes");

                // Verify the increase does not exceed the specified limit
                if (sizeIncrease > maxIncreaseBytes)
                {
                    Console.Error.WriteLine($"Error: Size increase exceeds the allowed limit of {maxIncreaseBytes} bytes.");
                    // Clean up the temporary file
                    File.Delete(tempPath);
                    return;
                }

                Console.WriteLine("Watermark added successfully within the size limit.");
                // The watermarked file remains in the temporary folder for further use if needed
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}