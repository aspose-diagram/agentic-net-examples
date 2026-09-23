using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // List to store comment (annotation) IDs
                List<long> commentIds = new List<long>();

                // First pass: collect all comment IDs from every page
                foreach (Page page in diagram.Pages)
                {
                    // Annotations are stored in the page's PageSheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        long id = annotation.MarkerIndex.Value;
                        commentIds.Add(id);
                    }
                }

                // Output the collected IDs
                Console.WriteLine("Collected Comment IDs:");
                foreach (long id in commentIds)
                {
                    Console.WriteLine($"- {id}");
                }

                // Second pass: retrieve detailed comment objects using the IDs
                Console.WriteLine("\nDetailed Comment Information:");
                foreach (long targetId in commentIds)
                {
                    bool found = false;

                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            if (annotation.MarkerIndex.Value == targetId)
                            {
                                // Detailed information about the comment
                                string commentText = annotation.Comment.Value;
                                int reviewerId = annotation.ReviewerID.Value;
                                int shapeId = annotation.ShapeID; // ShapeID is a primitive int

                                Console.WriteLine($"Comment ID: {targetId}");
                                Console.WriteLine($"  Text      : {commentText}");
                                Console.WriteLine($"  ReviewerID: {reviewerId}");
                                Console.WriteLine($"  ShapeID   : {shapeId}");
                                Console.WriteLine();

                                found = true;
                                break;
                            }
                        }

                        if (found) break;
                    }

                    if (!found)
                    {
                        Console.WriteLine($"Comment ID {targetId} not found in any page.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }