using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpRoll.Controllers
{
    public interface IInputHandler
    {
        string HandlesKeyword { get; }
        string HandleInput(string[] inputTokens);
    }
}