using System;

namespace FluentDefense.Defenders;

public class GuidDefender : DefenderBase<GuidDefender, Guid?>
{
    public GuidDefender(Guid? value, string parameterName) : base(parameterName, value)
    {
    }

    public GuidDefender NotNullOrEmpty()
    {
        IsNotNull();
        if (Value.HasValue && Value == Guid.Empty)
        {
            AddError("Guid Value is empty");
        }

        return this;
    }

    private GuidDefender IsNotNull()
    {
        if (!Value.HasValue)
        {
            AddError($"Guid Value is null");
        }

        return this;
    }
}