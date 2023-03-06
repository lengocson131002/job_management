using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository.Enums;

namespace JobApplicationManagement.Utils
{
    public static class EnumExtension
    {
        public static string? ToJobTypeString(this JobType jobType)
        {
            switch (jobType)
            {
                case JobType.PART_TIME:
                    return "Part time";
                case JobType.FULL_TIME:
                    return "Full time";
            }
            return null;
        }

        public static string? ToJobLevelString(this JobLevel level)
        {
            switch (level)
            {
                case JobLevel.INTERNSHIP:
                    return "Internship";
                case JobLevel.FRESHER:
                    return "Fresher";
                case JobLevel.JUNIOR:
                    return "Junior";
                case JobLevel.SENIOR:
                    return "Senior";
            }

            return null;
        }
    }
}
