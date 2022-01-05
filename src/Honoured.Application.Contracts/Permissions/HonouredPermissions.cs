namespace Honoured.Permissions
{
    public static class HonouredPermissions
    {
        public const string GroupName = "Honoured";

        public static class Disciplines
        {
            public const string Default = GroupName + ".Discipline";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Artists
        {
            public const string Default = GroupName + ".Artist";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Profile = Default + ".Profile";
            public const string Portfolio = Default + ".Portfolio";
        }
        public static class ArtWorks
        {
            public const string Default = GroupName + ".ArtWork";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        public static class ArtLovers
        {
            public const string Default = GroupName + ".ArtLover";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        public static class Placements
        {
            public const string Default = GroupName + ".Placement";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        public static class Countries
        {
            public const string Default = GroupName + ".Country";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        public static class Markets
        {
            public const string Default = GroupName + ".Market";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Deliveries
        {
            public const string Default = GroupName + ".Delivery";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Dimensions
        {
            public const string Default = GroupName + ".Dimension";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        public static class ArtistSubscriptions
        {
            public const string Default = GroupName + ".ArtistSubscriptions";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}