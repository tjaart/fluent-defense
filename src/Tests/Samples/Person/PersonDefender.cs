using FluentDefense;

namespace Tests.Samples.Person;

public class PersonDefender : DefenderBase<PersonDefender, Person>
{
    public PersonDefender(string parameterName, Person value) : base(parameterName, value)
    {
    }

    // create a method for the defender, returning itself back to the caller
    public PersonDefender HasValidName()
    {
        if (Value.Name is null)
        {
            AddError("Name was not supplied for person.");
        }
        else if (Value.Name?.Length < 2)
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