using System;
using System.Collections.Generic;

namespace SharpRoll.Controllers
{
    public interface IInputHandler
    {
        List<string> HandlesKeywords { get; }
        string HandleInput(string[] inputTokens);
    }
}