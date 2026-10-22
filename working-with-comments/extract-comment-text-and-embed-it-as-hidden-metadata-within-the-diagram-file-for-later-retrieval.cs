using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect all comment texts from all pages
                StringBuilder commentBuilder = new StringBuilder();

                foreach (Page page in diagram.Pages)
                {
                    // Annotations collection holds comments on the page
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment text safely
                        string commentText = ann.Comment?.Value ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(commentText))
                        {
                            commentBuilder.AppendLine(commentText);
                        }
                    }
                }

                // Combine comments into a single string
                string allComments = commentBuilder.ToString().TrimEnd();

                // Embed the combined comments as a custom document property (hidden metadata)
                // Property name "EmbeddedComments" can be used later for retrieval
                if (!string.IsNullOrEmpty(allComments))
                {
                    bool updated = false;

                    // Try to find an existing custom property with the same name
                    foreach (CustomProp existingProp in diagram.DocumentProps.CustomProps)
                    {
                        if (existingProp.Name == "EmbeddedComments")
                        {
                            // Update the existing property's value
                            existingProp.CustomValue.ValueString = allComments;
                            updated = true;
                            break;
                        }
                    }

                    // If not found, create a new custom property
                    if (!updated)
                    {
                        CustomProp newProp = new CustomProp();
                        newProp.Name = "EmbeddedComments";
                        newProp.PropType = PropType.String;
                        newProp.CustomValue = new CustomValue();
                        newProp.CustomValue.ValueString = allComments;
                        diagram.DocumentProps.CustomProps.Add(newProp);
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}