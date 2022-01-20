using System;

namespace simpleBlog.Ui.Interface
{
    public interface IConfigUi
    {
        string connectionString { get; }
        string url { get; }
        string secrteKey { get; }
        long dateTimeNow { get; }
    }
}
