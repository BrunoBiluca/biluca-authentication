using System;

namespace Authentication.Domain.Entities.Interfaces
{
    public interface IEntityDate
    {
        DateTime Created { get; }
        DateTime? Updated { get; set; }
    }
}