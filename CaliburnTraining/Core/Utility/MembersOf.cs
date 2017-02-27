using System;
using System.Linq.Expressions;

namespace Core.Utility
{
    public static class MembersOf<T>
    {
        public static string GetName<TR>(Expression<Func<T, TR>> expr)
        {
            var node = expr.Body as MemberExpression;
            if (ReferenceEquals(null, node))
                throw new InvalidOperationException("Expression must be of member access");
            return node.Member.Name;
        }
    }
}
