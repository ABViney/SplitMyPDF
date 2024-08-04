namespace SplitMyPDF.Models;

public class Instruction
{
    public string OutputDirectory { get; set; }
    public string Name { get; set; }
    public int[] Pages { get; set; }

    public string Path => System.IO.Path.Combine(OutputDirectory, Name.EndsWith(".pdf") ? Name : String.Concat(Name, ".pdf"));

    public Instruction(string outputDirectory, string name, int[] pages)
    {
        OutputDirectory = outputDirectory;
        Name = name;
        Pages = pages;
    }
}