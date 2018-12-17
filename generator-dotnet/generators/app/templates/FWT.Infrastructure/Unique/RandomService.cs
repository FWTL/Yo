using System;
using <%= solutionName %>.Core.Services.Unique;

namespace <%= solutionName %>.Infrastructure.Services.Unique
{
    public class RandomService : IRandomService
    {
        public Random Random { get; } = new Random();
    }
}