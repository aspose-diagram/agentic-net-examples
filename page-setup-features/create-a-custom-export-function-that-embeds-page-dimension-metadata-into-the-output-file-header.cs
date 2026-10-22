using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Retrieve dimensions of the first page (values are in inches).
            Page firstPage = diagram.Pages[0];
            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

            // Create a custom property for page width.
            CustomProp widthProp = new CustomProp();
            widthProp.Name = "PageWidthInches";
            widthProp.PropType = PropType.String; // Store as string.
            widthProp.CustomValue.ValueString = pageWidth.ToString();

            // Create a custom property for page height.
            CustomProp heightProp = new CustomProp();
            heightProp.Name = "PageHeightInches";
            heightProp.PropType = PropType.String; // Store as string.
            heightProp.CustomValue.ValueString = pageHeight.ToString();

            // Add custom properties to the document's metadata collection.
            diagram.DocumentProps.CustomProps.Add(widthProp);
            diagram.DocumentProps.CustomProps.Add(heightProp);

            // Save the diagram to VSDX format. Custom properties become part of the file header/metadata.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram exported successfully with page dimension metadata to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}