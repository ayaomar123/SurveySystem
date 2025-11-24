namespace SurveySystem.Domain.Entites
{
    public record struct Error
    {
        public string Code { get; }

        public string Description { get; }

        public ErrorKind Type { get; }
        public Error(string code, string description, ErrorKind type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        

        public enum ErrorKind
        {
            Failure,
            Unexpected,
            Validation,
            Conflict,
            NotFound,
            Unauthorized,
            Forbidden,
        }
    }
}
