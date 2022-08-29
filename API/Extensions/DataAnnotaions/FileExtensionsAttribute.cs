using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace API.Extensions.DataAnnotaions
{
    /// <summary>
    /// Validation attribute to assert an <see cref="IFormFile">IFormFile</see> property, field or parameter has a specific extention.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private string? _extensions = String.Empty;
        private const string DEFAULT_EXTENSTIONS = "png,jpg,jpeg";

        /// <summary>
        /// Gets or sets the acceptable extensions of the file.
        /// </summary>
        public string Extensions
        {
            get
            {
                // Default file extensions match those from jquery validate.
                return string.IsNullOrEmpty(_extensions) ? DEFAULT_EXTENSTIONS : _extensions;
            }
            set
            {
                _extensions = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string ExtensionsNormalized
        {
            get
            {
                return Extensions.Replace(" ", "", StringComparison.Ordinal).ToUpperInvariant();
            }
        }

        /// <summary>
        /// Override of <see cref="ValidationAttribute.IsValid(object)"/>
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name="value"/> is null.  
        /// It is assumed the <see cref="RequiredAttribute"/> is used if the value may not be null.
        /// </remarks>
        /// <param name="value">The value to test.</param>
        /// <returns><c>true</c> if the value is null or it's extension is not invluded in the set extensionss</returns>
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                throw new ArgumentException($"Incorrect type of argument. Must be IFormFile");
            }

            var extension = Path.GetExtension(file.FileName).Replace(".","").ToUpperInvariant();
            if (!_extensions.Split(',').Contains(extension.ToLower()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Override of <see cref="ValidationAttribute.FormatErrorMessage"/>
        /// </summary>
        /// <param name="name">The name to include in the formatted string</param>
        /// <returns>A localized string to describe the acceptable extensions</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Extensions);
        }
    }
}
