namespace Vsslabs.Bll.Services.AspnetIdentity.Tasks
{
    /// <summary>
    /// A class that holds properties/values required to execute some user tasks on AD
    /// </summary>
    internal class UserTaskModel
    {
        public string OldPassword { get; set; }
        public Data.User User { get; set; }
    }
}
