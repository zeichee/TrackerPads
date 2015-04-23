using Oosa.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.AspnetIdentity.Tasks
{
    internal class UpdateUserTask 
    {
        private readonly UserTaskModel _model;
        private readonly ApplicationUserManager _userManager;
        public UpdateUserTask(UserTaskModel model, ApplicationUserManager userManager)
        {
            _model = model;
            _userManager = userManager;
        }
        public async Task<Oosa.Core.TaskResult> ExecuteTask()
        {
            var task = await _userManager.UpdateAsync(_model.User);

            return task.Succeeded ? TaskResult.Success : TaskResult.Failed(task.Errors.ToArray());
        }
    }
}
