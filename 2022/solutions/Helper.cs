using System.Reflection;

public class Helper
{
    public static List<string> get_input(string file_name)
    {
        string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        string input_file = Path.Combine(currentDirectory, "..\\..\\..\\..\\input\\" + file_name);

        return File.ReadAllLines(input_file).ToList();
    }

}

