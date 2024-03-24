namespace BankAccount.Exceptions;

public class BusinessException : Exception
{
    public ErrorInfo ErrorInfo { get; private set; }

    private BusinessException(int code, string message, string techMessage) : base($"{message}\n{techMessage}")
    {
        ErrorInfo = new ErrorInfo
        {
            Code = code,
            UserMessage = message,
            TechnicalMessage = techMessage
        };
    }

    public static BusinessException GenerateBusinessExceptionWithThrow(int code, string message, string techMessage)
        => throw new BusinessException(code, message, techMessage);

    public static BusinessException GenerateBusinessException(int code, string message, string techMessage)
        => new(code, message, techMessage);
}

public class ErrorInfo
{
    public int Code { get; set; }
    public string UserMessage { get; set; }
    public string TechnicalMessage { get; set; }
}