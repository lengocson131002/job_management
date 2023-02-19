namespace JobApplicationManagement.Utils
{
    public class SalaryUtils
    {
        public static string GetSalary(long? min, long? max)
        {
            if (min == null && max == null)
            {
                return "Deal";
            }
            else if (min != null)
            {
                return "Min " + min + " $";
            }
            else
            {
                return "Up to " + max + " $";
            }
        }
    }
}
