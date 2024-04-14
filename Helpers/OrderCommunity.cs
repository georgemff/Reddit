using Reddit.Enums;
using Reddit.Models;

namespace Reddit.Helpers;

public class OrderCommunity
{
    public static IQueryable<Community> OrderCommunityByKey(IQueryable<Community> queryableCommunity,
        Enums.SortKey? sortKey,
        bool? isAscending)
    {
        bool asc = isAscending ?? true;

        if (asc)
        {
            switch (sortKey)
            {
                case SortKey.CreatedAt:
                    return queryableCommunity
                        .OrderBy(c => c.CreateAt)
                        .AsQueryable();

                case SortKey.PostsCount:
                    return queryableCommunity
                        .OrderBy(c => c.Posts.Count()).AsQueryable();

                case SortKey.SubscribersCount:
                    return queryableCommunity
                        .OrderBy(c => c.Subscribers.Count())
                        .AsQueryable();

                default:
                    return queryableCommunity
                        .OrderBy(c => c.Id)
                        .AsQueryable();
            }
        }
        else
        {
            switch (sortKey)
            {
                case SortKey.CreatedAt:
                    return queryableCommunity
                        .OrderByDescending(c => c.CreateAt)
                        .AsQueryable();

                case SortKey.PostsCount:
                    return queryableCommunity
                        .OrderByDescending(c => c.Posts.Count()).AsQueryable();

                case SortKey.SubscribersCount:
                    return queryableCommunity
                        .OrderByDescending(c => c.Subscribers.Count())
                        .AsQueryable();

                default:
                    return queryableCommunity
                        .OrderByDescending(c => c.Id)
                        .AsQueryable();
            }
        }
    }
}