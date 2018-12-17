using System;
using <%= solutionName %>.Core.Services.Unique;

namespace <%= solutionName %>.Infrastructure.Unique
{
    public class GuidService : IGuidService
    {
        public Guid New()
        {
            return Guid.NewGuid();
        }
    }
}
