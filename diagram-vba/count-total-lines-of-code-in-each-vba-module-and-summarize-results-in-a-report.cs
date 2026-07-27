using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram file (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Prepare a report string
            StringWriter report = new StringWriter();
            report.WriteLine("VBA Module Line Count Report");
            report.WriteLine("============================");
            report.WriteLine();

            // Iterate through each VBA module
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                VbaModule module = vbaProject.Modules[i];
                string code = module.Codes ?? string.Empty;

                // Count lines by splitting on newline characters
                int lineCount = 0;
                using (StringReader reader = new StringReader(code))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }

                // Append module information to the report
                report.WriteLine($"Module Name : {module.Name}");
                report.WriteLine($"Module Type : {module.Type}");
                report.WriteLine($"Line Count  : {lineCount}");
                report.WriteLine();
            }

            // Write the report to a text file (replace with desired output path)
            File.WriteAllText("VbaModuleLineCountReport.txt", report.ToString());

            // Optionally, also output to console
            Console.WriteLine(report.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
