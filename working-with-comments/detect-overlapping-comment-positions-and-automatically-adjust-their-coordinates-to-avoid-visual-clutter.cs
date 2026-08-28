using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect existing annotations
                    List<Annotation> originalAnnotations = new List<Annotation>();
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        originalAnnotations.Add(ann);
                    }

                    // Offset step (in inches) to separate overlapping comments
                    double offsetStep = 0.5;
                    int index = 0;

                    // Clear original comment text and add a new comment with adjusted coordinates
                    foreach (Annotation ann in originalAnnotations)
                    {
                        string commentText = ann.Comment.Value;

                        // Clear the original comment to avoid duplication
                        ann.Comment.Value = string.Empty;

                        // Calculate new position
                        double newPinX = index * offsetStep;
                        double newPinY = index * offsetStep;

                        // Add the comment at the new position
                        page.AddComment(newPinX, newPinY, commentText);

                        index++;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }