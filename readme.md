# Defensive Extensions

A library that makes defensive programming easier. 

### Goals briefly: 
- Zero dependency
- Extensible
- Developer Friendly

## What is defensive programming?

_"Defensive programming is about protecting yourself from being hurt by something dangerous (If bad data is sent to a routine, it will not hurt the routine). By writing code that will protect yourself from bad data, unexpected events, and other programmers mistakes, will in most case reduce bugs and create a high quality software.
Good programmers will not let bad data through. It’s important to validate input parameters to not let the garbage in. It’s also important to make sure that if garbage does come in, noting will goes out or an exception will be thrown. "_

https://weblogs.asp.net/fredriknormen/defensive-programming-and-design-by-contract-on-a-routine-level

## Getting Started

1. Install the FluentDefense package https://www.nuget.org/packages/Thorium.FluentDefense
2. Include the using statement for the defensive extensions: `using Thorium.FluentDefense`

## A note about compatibility
Note that this library is only supported on dotnet 6+ and will likely not be backward compatible in newer versions. If you would like multitargeting, you may contribute or request backward compat from me. Since this is a hobby project, I cannot realistically support production usage besides my own. You may easily stick with older versions of the library, or upgrade your own projects for compatibility with newer dotnet versions.

## Example Code

```c#
public void EnableAuthentication(string tokenBaseUrl, string clientId, string clientSecret, string appName)
{
    _tokenBaseUrl = tokenBaseUrl;
    
    tokenBaseUrl
        .Defend()
        .ValidUri()
        .Throw(); // throw an exception if any of the validations fail

    clientId
        .Defend("client id") // you can also specify the argument name
        .NotNullOrEmpty()
        .ErrorMessage; // Get a single string newline seperated list of errors.

    var errors = clientSecret
        .Defend()
        .NotNullOrEmpty()
        .Errors; // get a list of errors

    appName
        .Defend()
        .NotNullOrEmpty()
        .Throw();

    _tokenAuthenticationCredentials = new TokenAuthenticationCredentials
    {
        AppName = appName,
        ClientId = clientId,
        ClientSecret = clientSecret
    };
}
```

## Extending Fluent Defense

You can easily add to an existing defender using the `Custom()` method.

```c#
public static class TestIntDefenderExtensions
{
    public static IntDefender IsEven(this IntDefender defender)
    {
        defender.Custom(value => value % 2 == 0, (value, name) => $"{value} for {name} is not even");
        return defender;
    }
}
```

You can then use it as you would use the defender normally, and mix it in with existing defensive actions.

```c#
itemId.Defend()
    .NotZero()
    .NotNegative()
    .IsEven() // chain your custom extension easily
    .Custom(i => i.ToString().Length == 5, (_, _) => "Order ids must be five digits")
    .Throw();
```
