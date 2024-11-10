using System;

namespace ssm.Systems
{
    public readonly struct ToastyQoLRequirement
    {
        // Typically the name of a boss, but theoretically can be anything.
        public readonly string RequirementName;

        public readonly Func<bool> Requirement;

        public ToastyQoLRequirement(string requirementName, Func<bool> requirement)
        {
            RequirementName = requirementName;
            Requirement = requirement;
        }
    }
}
