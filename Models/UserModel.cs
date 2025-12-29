namespace async_vs_sync_benchmark_api.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string NameKh { get; set; }
    }
    public class UserAddModel : UserModel { }
    public class UserEditModel : UserModel
    {
        public long Id { get; set; }
    }
    public class UserViewModel : UserEditModel { }
    public class UserListModel : UserEditModel { }
}
