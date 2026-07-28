using System;
using System.IO;
using Aspose.Diagram;

class HeaderComparison
{
    static void Main()
    {
        try
        {

            // Paths to the two Visio diagrams to compare
            string diagramPath1 = @"C:\Diagrams\Diagram1.vsdx";
            string diagramPath2 = @"C:\Diagrams\Diagram2.vsdx";

            // Path to the log file where differences will be recorded
            string logFilePath = @"C:\Diagrams\HeaderComparisonLog.txt";

            // Load the first diagram using the Aspose.Diagram constructor that accepts a file path
            Diagram diagram1 = new Diagram(diagramPath1);
            // Load the second diagram
            Diagram diagram2 = new Diagram(diagramPath2);

            // Retrieve the left header text from each diagram
            string headerLeft1 = diagram1.HeaderFooter.HeaderLeft;
            string headerLeft2 = diagram2.HeaderFooter.HeaderLeft;

            // Open the log file for appending
            using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
            {
                // Compare the header texts and write the result to the log
                if (!string.Equals(headerLeft1, headerLeft2, StringComparison.Ordinal))
                {
                    logWriter.WriteLine($"[{DateTime.Now}] HeaderLeft differs:");
                    logWriter.WriteLine($"  Diagram 1: \"{headerLeft1}\"");
                    logWriter.WriteLine($"  Diagram 2: \"{headerLeft2}\"");
                }
                else
                {
                    logWriter.WriteLine($"[{DateTime.Now}] HeaderLeft is identical: \"{headerLeft1}\"");
                }
            }

            // Clean up resources
            diagram1.Dispose();
            diagram2.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
