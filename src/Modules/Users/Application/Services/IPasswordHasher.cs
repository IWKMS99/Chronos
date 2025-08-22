﻿namespace Chronos.Modules.Users.Application.Services;

public interface IPasswordHasher {
    string Hash(string password);
    bool Verify(string password, string hash);
}