namespace VoterApi.Services;

public class DuplicateVoterException : Exception
{
    public DuplicateVoterException(string idNumber)
        : base($"A voter with id number '{idNumber}' already exists.")
    {
    }
}
