using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_with_metadata.vsdx";

                try
                {
                    // Load the diagram from file
                    Diagram diagram = new Diagram(inputPath);

                    // Collect all comment texts from all pages
                    string allComments = string.Empty;
                    foreach (Page page in diagram.Pages)
                    {
                        // Annotations are stored in the PageSheet
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Append comment text; use newline as separator
                            allComments += annotation.Comment.Value + Environment.NewLine;
                        }
                    }

                    // Trim trailing newline
                    allComments = allComments.TrimEnd();

                    // Create a custom document property to hold the comments metadata
                    CustomProp commentProp = new CustomProp
                    {
                        Name = "CommentsMetadata",
                        PropType = PropType.String,
                        // Store the concatenated comments as a string value
                        CustomValue = { ValueString = allComments }
                    };

                    // Add the custom property to the document
                    diagram.DocumentProps.CustomProps.Add(commentProp);

                    // Save the diagram with the new metadata
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine("Diagram processed successfully. Comments embedded as metadata.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }