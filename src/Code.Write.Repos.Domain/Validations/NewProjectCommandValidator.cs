using Code.Writer.Repos.Domain.Commands;
using Flunt.Validations;

namespace Code.Writer.Repos.Domain.Validations
{
    public class NewProjectCommandValidator: Contract<CreateProjectCommand>
    {
        public NewProjectCommandValidator()
        {

        }
    }
}
