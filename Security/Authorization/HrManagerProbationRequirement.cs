using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Security.Authorization
{
    public class HrManagerProbationRequirement : IAuthorizationRequirement
    {
        public HrManagerProbationRequirement(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }

        public int ProbationMonths { get; }
    }

    public class HrManagerProbationRequirementHandler 
        : AuthorizationHandler<HrManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
            , HrManagerProbationRequirement requirement)
        {
            if(!context.User.HasClaim(x => x.Type == "EmploymentDate"))
                return Task.CompletedTask;

            var empDate = DateTime
                .Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;

            if(period.Days > 30 * requirement.ProbationMonths)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}