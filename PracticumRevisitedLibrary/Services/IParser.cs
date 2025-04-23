using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Services
{
    public interface IParser
    {
        ParsedOrder ParseInput(string input);
    }
}
