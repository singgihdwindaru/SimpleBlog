using System;

namespace simpleBlog.Api.Interfaces
{
    public interface IAppSettings
    {
        string connectionString { get; }
    }
}
