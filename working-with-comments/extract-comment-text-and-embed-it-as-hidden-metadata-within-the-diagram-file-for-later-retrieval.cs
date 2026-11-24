using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
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
                // Collect all comment texts from all pages
                var allComments = new System.Text.StringBuilder();

                foreach (Page page in diagram.Pages)
                {
                    // Iterate over annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        string commentText = annotation.Comment?.Value ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(commentText))
                        {
                            allComments.AppendLine(commentText);
                        }
                    }
                }

                // Prepare the combined comment string (trim trailing newline)
                string combinedComments = allComments.ToString().TrimEnd();

                // Embed the combined comments as a custom document property
                const string propertyName = "EmbeddedComments";

                // Check if the custom property already exists
                bool propertyExists = false;
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (string.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Update existing property's value
                        prop.CustomValue.ValueString = combinedComments;
                        propertyExists = true;
                        break;
                    }
                }

                if (!propertyExists)
                {
                    // Create a new custom property
                    CustomProp newProp = new CustomProp();
                    newProp.Name = propertyName;
                    newProp.PropType = PropType.String;
                    newProp.CustomValue.ValueString = combinedComments;

                    // Add the new property to the diagram
                    diagram.DocumentProps.CustomProps.Add(newProp);
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with embedded comments to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}