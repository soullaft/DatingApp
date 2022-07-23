namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculate age between <see cref="DateTime.Now"/> and date of birth
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            
            return age;
        }
    }
}
