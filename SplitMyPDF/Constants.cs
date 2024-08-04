namespace SplitMyPDF;

public class Constants
{
    
    
    ////////////////
    // PDFTK binary
    ////////////////
    public static readonly string PATH_TO_PDFTK = @"";
    
    ////////////////
    // Input/Output
    ////////////////
    
    // PDF to read
    public static string INPUT_PDF = @"";

    // PDFs to make
    public static string OUTPUT_INSTRUCTIONS = @"";
    
    // Where to write new PDFs
    public static string OUTPUT_FOLDER = @"";
    
    /////////////////
    // Special chars
    /////////////////
    
    // Instructs the program to make this folder in the output folder and place subsequent output PDFs in it
    public static readonly char DIRECTORY_MARKER = '#';
    
    // Columns (Output file name and pages) are delimitted 
    public static readonly char COLUMN_DELIMITER = ':';
    
    // Pages are in sets and can be delimitted 
    public static readonly char PAGE_DELIMITER = ',';

    public static readonly char PAGE_RANGE_SPECIFIER = '-';
}