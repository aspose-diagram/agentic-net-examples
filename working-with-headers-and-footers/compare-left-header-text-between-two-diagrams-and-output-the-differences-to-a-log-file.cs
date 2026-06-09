using System;
using System.IO;
using Aspose.Diagram;

class HeaderComparison
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the two Visio diagram files
            string diagramPath1 = @"C:\Diagrams\Diagram1.vsdx";
            string diagramPath2 = @"C:\Diagrams\Diagram2.vsdx";

            // Path to the log file where differences will be recorded
            string logFilePath = @"C:\Diagrams\HeaderDifferences.log";

            // Load the first diagram
            using (Diagram diagram1 = new Diagram(diagramPath1))
            // Load the second diagram
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Retrieve the left header text from each diagram
                string headerLeft1 = diagram1.HeaderFooter.HeaderLeft ?? string.Empty;
                string headerLeft2 = diagram2.HeaderFooter.HeaderLeft ?? string.Empty;

                // Compare the header texts
                if (!headerLeft1.Equals(headerLeft2, StringComparison.Ordinal))
                {
                    // Prepare the difference message
                    string diffMessage = $"HeaderLeft differs between diagrams:{Environment.NewLine}" +
                                         $"Diagram 1 ({Path.GetFileName(diagramPath1)}): \"{headerLeft1}\"{Environment.NewLine}" +
                                         $"Diagram 2 ({Path.GetFileName(diagramPath2)}): \"{headerLeft2}\"{Environment.NewLine}" +
                                         $"Comparison performed at: {DateTime.Now}{Environment.NewLine}";

                    // Write the difference to the log file (append if the file already exists)
                    File.AppendAllText(logFilePath, diffMessage);
                }
                else
                {
                    // Optionally log that the headers are identical
                    string sameMessage = $"HeaderLeft is identical for both diagrams ({Path.GetFileName(diagramPath1)} and {Path.GetFileName(diagramPath2)}) at {DateTime.Now}{Environment.NewLine}";
                    File.AppendAllText(logFilePath, sameMessage);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
