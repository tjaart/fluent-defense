# Defensive Extensions

A library that makes defensive programming easier.

### Goals briefly:

- Zero dependency
- Extensible
- Developer Friendly

### **NOT** Goals

- High performance (low cpu/memory)

## What is defensive programming?

_"Defensive programming is about protecting yourself from being hurt by something dangerous (If bad data is sent to a
routine, it will not hurt the routine). By writing code that will protect yourself from bad data, unexpected events, and
other programmers mistakes, will in most case reduce bugs and create a high quality software.
Good programmers will not let bad data through. It’s important to validate input parameters to not let the garbage in.
It’s also important to make sure that if garbage does come in, noting will goes out or an exception will be thrown. "_

https://weblogs.asp.net/fredriknormen/defensive-programming-and-design-by-contract-on-a-routine-level

## Getting Started

1. Install the FluentDefense package https://www.nuget.org/packages/FluentDefense
2. Include the using statement for the defensive extensions: `using FluentDefense`

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

### Building Defenders for your own types

You can easily create a new defender for your own custom types in two simple steps.

1. Create a defender subclass

```csharp
public class PersonDefender : DefenderBase<PersonDefender, Person>
{
    public PersonDefender(string parameterName, Person value) : base(parameterName, value)
    {
    }
    
    // create a method for the defender, returning itself back to the caller
    public PersonDefender HasValidName()
    {
        if (Value.Name?.Length < 2)
        {
            // add any errors you need
            AddError($"'{Value.Name}' is not a valid name for person '{ParameterName}'. Name must have at least two characters and not be null");
        }

        return this;
    }

    public PersonDefender HasValidAge()
    {
        if (!Value.Age.Defend().InRange(1, 120).IsValid)
        {
            AddError($"'{Value.Age}' is not a valid age for person '{ParameterName}'. Age must be between 1 and 120 years");
        }

        return this;
    }

    // you can create composite calls for common use cases
    public PersonDefender HasEnoughInfoProvided() => HasValidName().HasValidAge();
}
```

2. Create an extension method that extends the object with a Defend() method.

```csharp
public static class PersonDefenderExtensions
{
    public static PersonDefender Defend(this Person person, [CallerArgumentExpression("person")] string parameterName = "")
        => new(parameterName, person);
}
```

3. Use the new extension for validation!

```csharp
var applicant = new Person();

applicant.Defend()
    .HasEnoughInfoProvided()
    .Throw();
```



