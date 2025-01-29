using DinDin.Domain.Users;

namespace DinDin.Infra.Users
{
    public sealed class UserSingleton : List<User>
    {
        private UserSingleton() { }

        private static readonly Lazy<UserSingleton> lazy = new(() => new UserSingleton());

        public static UserSingleton Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}