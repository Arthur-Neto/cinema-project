namespace Theater.Infra.Crosscutting.Exceptions
{
    public enum ErrorType
    {
        NotFound,
        Duplicating,
        IncorrectUserPassword,
        SessionLessThanTenDaysToStart,
        RoomWithSession,
    }
}
