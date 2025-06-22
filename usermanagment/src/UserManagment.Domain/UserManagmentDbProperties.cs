namespace UserManagment;

public static class UserManagmentDbProperties
{
    public static string DbTablePrefix { get; set; } = "UserManagment";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "UserManagment";
}
