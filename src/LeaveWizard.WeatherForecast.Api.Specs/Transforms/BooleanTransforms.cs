using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Transforms
{
    [Binding]
    public class BooleanTransforms
    {
        [StepArgumentTransformation("(in|not in)")]
        public bool TransformInOrNot(string inOrNot)
        {
            return inOrNot.StartsWith("in");
        }

        [StepArgumentTransformation("(should|should not)")]
        public bool TransformShouldOrShouldOrNot(string shouldOrShouldNot)
        {
            return !shouldOrShouldNot.EndsWith("not");
        }

        [StepArgumentTransformation("(is|is not)")]
        public bool TransformIsOrNot(string isOrIsNot)
        {
            return !isOrIsNot.EndsWith("not");
        }

        [StepArgumentTransformation("(to|to not)")]
        public bool TransformToOrNot(string toOrToNot)
        {
            return !toOrToNot.EndsWith("not");
        }

        [StepArgumentTransformation("(has been|has not been)")]
        public bool TransformHasBeenOrNot(string hasBeenOrNot)
        {
            return !hasBeenOrNot.Contains("not");
        }

        [StepArgumentTransformation("(has|has not)")]
        public bool TransformHasOrNot(string hasOrNot)
        {
            return !hasOrNot.Contains("not");
        }
    }
}