
using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Processors
{
    public interface IProcessor
    {
        void Process(ProcessableOrder processable);
    }
}
