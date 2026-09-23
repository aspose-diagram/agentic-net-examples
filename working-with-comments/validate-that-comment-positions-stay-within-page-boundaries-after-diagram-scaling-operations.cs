using System;
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

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation comment in page.PageSheet.Annotations)
                    {
                        // Attempt to read the comment's X and Y coordinates.
                        // These cells are typically stored as DoubleValue objects.
                        double commentX = comment.X.Value;
                        double commentY = comment.Y.Value;

                        // Validate that the comment lies within the page boundaries
                        bool isInside = commentX >= 0 && commentX <= pageWidth &&
                                        commentY >= 0 && commentY <= pageHeight;

                        if (!isInside)
                        {
                            string message = $"Comment (ID: {comment.MarkerIndex.Value}) on page '{page.Name}' is out of bounds. " +
                                             $"Position: ({commentX}, {commentY}) inches, Page size: ({pageWidth}, {pageHeight}) inches.";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }
                        else
                        {
                            Console.WriteLine($"Comment (ID: {comment.MarkerIndex.Value}) is within page bounds.");
                        }
                    }
                }

                Console.WriteLine("All comment positions are within page boundaries.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }