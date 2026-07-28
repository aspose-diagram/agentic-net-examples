using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file, output file and the keyword to search for in comments
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";
                string keyword = "TODO";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and collect annotations that contain the keyword
                foreach (Page page in diagram.Pages)
                {
                    var annotations = page.PageSheet.Annotations;
                    var toRemove = new List<Annotation>();

                    foreach (Annotation ann in annotations)
                    {
                        // Ensure the comment text is not null before checking
                        if (ann.Comment.Value != null && ann.Comment.Value.Contains(keyword))
                        {
                            toRemove.Add(ann);
                        }
                    }

                    // Remove the collected annotations from the page
                    foreach (Annotation ann in toRemove)
                    {
                        // AnnotationCollection supports removal by object
                        annotations.Remove(ann);
                    }
                }

                // Verify that no remaining annotation contains the keyword
                bool stillExists = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        if (ann.Comment.Value != null && ann.Comment.Value.Contains(keyword))
                        {
                            stillExists = true;
                            break;
                        }
                    }
                    if (stillExists) break;
                }

                if (stillExists)
                {
                    throw new Exception($"Comment with keyword '{keyword}' still exists after deletion.");
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