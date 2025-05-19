namespace Convoquei.Core.Genericos.Extensoes
{
    public static class HashSetExtensoes
    {
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                hashSet.Add(item);
            }
        }
    }
}
