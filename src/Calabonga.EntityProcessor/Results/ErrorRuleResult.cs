﻿using Calabonga.EntityProcessor.Base;

namespace Calabonga.EntityProcessor.Results;

public class ErrorRuleResult : RuleResultBase
{
    public ErrorRuleResult(string message) : base(false)
        => ErrorMessage = message ?? throw new ArgumentNullException(nameof(message));

    public override string? ErrorMessage { get; }
}