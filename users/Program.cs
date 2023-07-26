using System.CommandLine;
using users;

var loginOption = new Option<string>(
        name: "--login",
        description: "User login.")
    {IsRequired = true,};
loginOption.AddAlias("-l");

var passwordOption = new Option<string>(
        name: "--password",
        description: "Password.")
    {IsRequired = true,};
passwordOption.AddAlias("-p");

var adminOption = new Option<bool>(
    name: "--admin",
    description: "Create administrator user.");
adminOption.AddAlias("-a");

var rootCommand = new RootCommand("Users management utility for Mars reservation application.");

var addCommand = new Command("add", "Add new user.")
{
    loginOption,
    passwordOption,
    adminOption,
};
rootCommand.AddCommand(addCommand);

addCommand.SetHandler(async (login, password, isAdmin) =>
{
    await new Users(new string[]{}).Add(login, password, isAdmin);
}, loginOption, passwordOption, adminOption);

return await rootCommand.InvokeAsync(args);