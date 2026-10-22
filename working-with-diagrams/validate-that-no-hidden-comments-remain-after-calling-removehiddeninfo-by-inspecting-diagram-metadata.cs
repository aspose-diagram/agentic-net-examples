using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden information (shapes, masters, styles).
                // Combine the desired flags using bitwise OR and cast to int.
                int flags = (int)(RemoveHiddenInfoItem.Shapes |
                                  RemoveHiddenInfoItem.Masters |
                                  RemoveHiddenInfoItem.Styles);
                diagram.RemoveHiddenInformation(flags);

                // Validate that no comments (annotations) remain in any page.
                bool hiddenCommentsExist = false;

                foreach (Page page in diagram.Pages)
                {
                    // Ensure the PageSheet and its Annotations collection are available.
                    if (page.PageSheet != null && page.PageSheet.Annotations != null && page.PageSheet.Annotations.Count > 0)
                    {
                        hiddenCommentsExist = true;
                        break;
                    }
                }

                if (hiddenCommentsExist)
                {
                    Console.Error.WriteLine("Hidden comments are still present after removing hidden information.");
                }
                else
                {
                    Console.WriteLine("Validation successful: No hidden comments found after removal.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}