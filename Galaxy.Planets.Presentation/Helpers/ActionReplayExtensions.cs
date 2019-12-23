using Galaxy.Planets.Core.Models;
using Galaxy.Teams;

namespace Galaxy.Planets.Presentation.Helpers
{
    public static class ActionReplayExtensions
    {
        public static ActionResponse ToActionResponse(this ActionReplay actionReplay)
        {
            var response = new ActionResponse
            {
                Success = actionReplay.Success
            };

            if (actionReplay.Success) return response;
            
            foreach (var error in actionReplay.Errors)
            {
                response.Errors.Add(new Core.Models.ActionError
                {
                    Code = error.Code,
                    Description = error.Description
                });
            }

            return response;
        }
    }
}