namespace Galaxy.Planets.Core.Models
{
    public class ActionError
    {
        public string Code { get; set; }
        public string Description { get; set; }
        
        public static ActionError NotFound(string name)
        {
            return new ActionError
            {
                Code = "NotFound",
                Description = $"{name} was not found"
            };
        }
        
        public static ActionError NotAvailableForTeam(string name)
        {
            return new ActionError
            {
                Code = "NotAvailable",
                Description = $"{name} is not available to be assigned to a team"
            };
        }
        
        public static ActionError NoName(string entity)
        {
            return new ActionError
            {
                Code = "NoName",
                Description = $"The {entity} should have a name"
            };
        }
        
        public static ActionError NameToLong(string entity)
        {
            return new ActionError
            {
                Code = "NameToLong",
                Description = $"The {entity} name should be less than 256 characters"
            };
        }
    }
}