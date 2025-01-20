namespace DinDin.Domain.Users
{
    public sealed class UserSingleton : List<User>
    {
        private UserSingleton() { }

        private static readonly Lazy<UserSingleton> lazy = new Lazy<UserSingleton>(() => new UserSingleton());

        public static UserSingleton Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}