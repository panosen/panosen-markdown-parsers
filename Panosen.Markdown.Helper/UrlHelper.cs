using System;

namespace Panosen.Markdown.Helper
{
    public class UrlHelper
    {
        /// <summary>
        /// Checks if the given URL is allowed in a markdown link.
        /// </summary>
        /// <param name="url"> The URL to check. </param>
        /// <returns> <c>true</c> if the URL is valid; <c>false</c> otherwise. </returns>
        public static bool IsUrlValid(string url)
        {
            // URLs can be relative.
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
            {
                return true;
            }

            // Check the scheme is allowed.
            foreach (var scheme in Constant.KnownSchemes)
            {
                if (result.Scheme.Equals(scheme))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
