using SharpCompress;

namespace Bb.OpenApiServices
{
    public static class StringArrayExtension
    {

        public static string[] Concat(this string[] self, params string[] items)
        {
            List<string> result = new List<string>(self.Length + items.Length);
            self.ForEach(c => result.Add(c));
            items.ForEach(c => result.Add(c));
            return items.ToArray();
        }

    }


}