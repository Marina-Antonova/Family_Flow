namespace FamilyFlow.Common
{
    public static class ValidationConstants
    {
        public static class FamilyMember
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int MinAge = 0;
            public const int MaxAge = 120;
        }
        public static class HouseTask
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;

            public const int DescriptionMinLenght = 5;
            public const int DescriptionMaxLength = 300;
        }
        public static class ScheduleEvent
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;
        }
    }
}
