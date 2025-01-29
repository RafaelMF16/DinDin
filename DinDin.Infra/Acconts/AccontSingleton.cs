using DinDin.Domain.Acconts;

namespace DinDin.Infra.Acconts
{
    public sealed class AccontSingleton : List<Accont>
    {
        private AccontSingleton() { }

        private static readonly Lazy<AccontSingleton> lazy = new(() => new AccontSingleton());

        public static AccontSingleton Instance
        {
            get 
            { 
                return lazy.Value; 
            }
        }
    }
}