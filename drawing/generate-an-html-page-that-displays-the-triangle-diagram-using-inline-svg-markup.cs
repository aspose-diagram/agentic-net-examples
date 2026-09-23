using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // SVG markup that draws a simple triangle
        string svg = @"<svg width=""200"" height=""200"" xmlns=""http://www.w3.org/2000/svg"">" +
                     @"<polygon points=""100,10 190,190 10,190"" style=""fill:lime;stroke:purple;stroke-width:1"" />" +
                     @"</svg>";

        // Complete HTML page embedding the SVG inline
        string html = @"<!DOCTYPE html>" +
                      "<html>" +
                      "<head><meta charset=\"UTF-8\"><title>Triangle Diagram</title></head>" +
                      "<body>" + svg + "</body>" +
                      "</html>";

        // Write the HTML content to a file named triangle.html
        File.WriteAllText("triangle.html", html, Encoding.UTF8);

        Console.WriteLine("HTML file 'triangle.html' has been created.");
    }
}
