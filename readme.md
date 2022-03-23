# Thorium Defensive Extensions

A library that makes defensive programming easier. 

## What is defensive programming?

_"Defensive programming is about protecting yourself from being hurt by something dangerous (If bad data is sent to a routine, it will not hurt the routine). By writing code that will protect yourself from bad data, unexpected events, and other programmers mistakes, will in most case reduce bugs and create a high quality software.
Good programmers will not let bad data through. It’s important to validate input parameters to not let the garbage in. It’s also important to make sure that if garbage does come in, noting will goes out or an exception will be thrown. "_

https://weblogs.asp.net/fredriknormen/defensive-programming-and-design-by-contract-on-a-routine-level

## Getting Started

1. Install the FluentDefense package https://www.nuget.org/packages/Thorium.FluentDefense
2. Include the using statement for the defensive extensions: `using Thorium.FluentDefense`

## Example Code

```csharp
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
