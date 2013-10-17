using Strilanc.Value;

namespace Lollipop
{
    public interface IIdentityMap
    {
        May<T> Load<T>(string key);

        May<object> Load(string key);

        void Insert<T>(string key, T obj);
    }
}