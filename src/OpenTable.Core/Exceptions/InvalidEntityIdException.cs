namespace OpenTable.Core.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    private readonly object _id;

    public InvalidEntityIdException(object id) 
        : base($"Cannot set: {id} as entity identifier.")
    {
        _id = id;
    }
}