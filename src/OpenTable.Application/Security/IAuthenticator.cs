﻿namespace OpenTable.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}