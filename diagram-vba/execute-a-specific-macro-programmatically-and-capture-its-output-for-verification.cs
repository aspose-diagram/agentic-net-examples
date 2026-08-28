using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project embedded in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Locate the module that contains the macro named "MyMacro"
            VbaModule macroModule = null;
            foreach (VbaModule module in vbaProject.Modules)
            {
                if (!string.IsNullOrEmpty(module.Codes) && module.Codes.Contains("Sub MyMacro"))
                {
                    macroModule = module;
                    break;
                }
            }

            if (macroModule == null)
            {
                Console.WriteLine("Macro 'MyMacro' not found in the diagram.");
                return;
            }

            // Retrieve the VBA code of the macro
            string vbaCode = macroModule.Codes;

            // Convert the VBA code to VBScript (basic conversion for demonstration)
            string vbScriptCode = ConvertVbaToVbs(vbaCode);

            // Write the VBScript to a temporary file
            string tempVbsPath = Path.Combine(Path.GetTempPath(), "tempMacro.vbs");
            File.WriteAllText(tempVbsPath, vbScriptCode, Encoding.UTF8);

            // Execute the VBScript using Windows Script Host and capture its output
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cscript",
                Arguments = $"//NoLogo \"{tempVbsPath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = Process.Start(psi))
            {
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                // Display the captured output for verification
                Console.WriteLine("Macro Output:");
                Console.WriteLine(output);
            }

            // Clean up the temporary script file
            File.Delete(tempVbsPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Very simple conversion: removes Sub/Function declarations and End statements
    static string ConvertVbaToVbs(string vbaCode)
    {
        var sb = new StringBuilder();
        string[] lines = vbaCode.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.StartsWith("Sub ", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("Function ", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("End Sub", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("End Function", StringComparison.OrdinalIgnoreCase))
            {
                continue; // Skip declaration and termination lines
            }
            sb.AppendLine(line);
        }
        return sb.ToString();
    }
}
