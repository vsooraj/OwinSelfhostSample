﻿namespace OwinSelfhostSample.Models
{
    public interface ILogger
    {
        void Write(string message, params object[] args);
    }
}
