using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input legacy file path and output file path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramCommentMigration <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the legacy diagram file.
                Diagram diagram = new Diagram(inputPath);

                // Log existing comments (annotations) for verification.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Annotation comment in page.PageSheet.Annotations)
                    {
                        // MarkerIndex uniquely identifies the comment.
                        long commentId = comment.MarkerIndex.Value;
                        string commentText = comment.Comment.Value;
                        Console.WriteLine($"Page \"{page.Name}\" - Comment ID {commentId}: {commentText}");
                    }
                }

                // Save the diagram in the latest VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\" with comments preserved.");
            }
            catch (Exception ex)
            {
                // Report any errors encountered during processing.
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }