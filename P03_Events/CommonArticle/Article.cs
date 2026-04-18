namespace CommonArticleLibrary
{
    public record Article : DomainEntity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public Guid AuthorId { get; init; }
        public bool IsPublished { get; init; } = false;

        public Article(string title, string description, Guid authorId)
        {
            Title = title;
            Description = description;
            AuthorId = authorId;
        }

        public Article Create()
        {
            return this with { IsPublished = true };
        }

        public Article WithTitle(string title)
        {
            return this with { Title = title };
        }

        public Article WithDescription(string description)
        {
            return this with { Description = description };
        }
    }
}
