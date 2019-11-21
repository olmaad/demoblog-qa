namespace DemoBlogBackend.Rating
{
    public static class RatingWeights
    {
        public static double PostMarkToPost { get; } = 0.9;
        public static double PostMarkToUser { get; } = 0.95;
        public static double PostMarkToPersonal { get; } = 0.7;

        public static double CommentMarkToComment { get; } = 0.9;
        public static double CommentMarkToUser { get; } = 0.975;
        public static double CommentMarkToPersonal { get; } = 0.8;
    }
}
