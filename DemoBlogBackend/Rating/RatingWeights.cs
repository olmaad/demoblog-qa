namespace DemoBlogBackend.Rating
{
    public static class RatingWeights
    {
        public static double PostVoteToPost { get; } = 0.9;
        public static double PostVoteToUser { get; } = 0.95;
        public static double PostVoteToPersonal { get; } = 0.7;

        public static double CommentVoteToComment { get; } = 0.9;
        public static double CommentVoteToUser { get; } = 0.975;
        public static double CommentVoteToPersonal { get; } = 0.8;
    }
}
