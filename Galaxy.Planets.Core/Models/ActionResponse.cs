using System.Collections.Generic;

namespace Galaxy.Planets.Core.Models
{
    public class ActionResponse
    {
        public ActionResponse()
        {
            Success = true;
            Errors = new List<ActionError>();
        }
        public bool Success { get; set; }
        public List<ActionError> Errors { get; set; }

        public static ActionResponse FailedToAdd()
        {
            return new ActionResponse
            {
                Success = false,
                Errors = new List<ActionError>
                {
                    new ActionError
                    {
                        Code = "FailedToAdd",
                        Description = "The add operation has failed please try again"
                    }
                }
            };
        }
        
        public static ActionResponse FailedToUpdate()
        {
            return new ActionResponse
            {
                Success = false,
                Errors = new List<ActionError>
                {
                    new ActionError
                    {
                        Code = "FailedToUpdate",
                        Description = "The update operation has failed please try again"
                    }
                }
            };
        }
        public static ActionResponse NotFound(string name)
        {
            return new ActionResponse
            {
                Success = false,
                Errors = new List<ActionError>
                {
                    ActionError.NotFound(name)
                }
            };
        }
    }
}