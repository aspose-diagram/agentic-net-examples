using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramMetadataExample <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Collect all comment texts from all pages
                List<string> commentTexts = new List<string>();
                foreach (Page page in diagram.Pages)
                {
                    // Annotations are stored in the PageSheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // The comment text is accessed via the .Comment.Value property
                        string text = annotation.Comment.Value;
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            commentTexts.Add(text);
                        }
                    }
                }

                // Combine comments into a single string (e.g., separated by line breaks)
                string combinedComments = string.Join(Environment.NewLine, commentTexts);

                // Embed the combined comments as a hidden custom property
                // First, check if a property with the same name already exists and remove it
                CustomProp existingProp = null;
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (prop.Name == "EmbeddedComments")
                    {
                        existingProp = prop;
                        break;
                    }
                }
                if (existingProp != null)
                {
                    diagram.DocumentProps.CustomProps.Remove(existingProp);
                }

                // Create and add the new custom property
                CustomProp customProp = new CustomProp();
                customProp.Name = "EmbeddedComments";
                customProp.PropType = PropType.String;
                customProp.CustomValue.ValueString = combinedComments;
                diagram.DocumentProps.CustomProps.Add(customProp);

                // Save the diagram with the embedded metadata
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Comments extracted and embedded successfully.");
            }
            catch (Exception ex)
            {
                // Report any errors
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }