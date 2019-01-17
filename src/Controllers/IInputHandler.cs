using System;
using System.Collections.Generic;

namespace SharpRoll.Controllers
{
    public interface IInputHandler
    {
        string HandlesKeyword { get; }
        string HandleInput(string[] inputTokens);
    }
}