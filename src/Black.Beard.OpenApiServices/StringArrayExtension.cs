
namespace Bb.OpenApiServices
{
    public static class StringArrayExtension
    {

        /// <summary>
        /// concat two strings arrays
        /// </summary>
        /// <param name="self"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string[] Concat(this string[] self, params string[] items)
        {

            List<string> result = new List<string>(self.Length + items.Length);

            foreach (var item in self)
                result.Add(item);
            foreach (var item in items)
                result.Add(item);

            return result.ToArray();

        }

    }


}