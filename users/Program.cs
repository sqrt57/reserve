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

// Add user

var addCommand = new Command("add", "Add new user.")
{
    loginOption,
    passwordOption,
    adminOption,
};
rootCommand.AddCommand(addCommand);
addCommand.SetHandler(async (login, password, isAdmin) =>
{
    await new Users().Add(login, password, isAdmin);
}, loginOption, passwordOption, adminOption);

// Change password

var changeCommand = new Command("change", "Change user password. Unlock user.")
{
    loginOption,
    passwordOption,
};
rootCommand.AddCommand(changeCommand);
changeCommand.SetHandler(async (login, password) =>
{
    await new Users().Change(login, password);
}, loginOption, passwordOption);

// Change password

var unlockCommand = new Command("unlock", "Unlock user.")
{
    loginOption,
};
rootCommand.AddCommand(unlockCommand);
unlockCommand.SetHandler(async (login) =>
{
    await new Users().Unlock(login);
}, loginOption);

return await rootCommand.InvokeAsync(args);
