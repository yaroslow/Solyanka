namespace Solyanka.Validator.Extensions
{
    /// <summary>
    /// Class-extensions over validation rules
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Checks that object is not null
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Check result</returns>
        public static bool NotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Checks that string is not null or empty
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Check result</returns>
        public static bool NotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Checks that string is not null or whitespace
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Check result</returns>
        public static bool NotNullOrWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}