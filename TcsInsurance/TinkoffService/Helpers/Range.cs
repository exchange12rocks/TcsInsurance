using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace TinkoffService.Helpers
{
    public struct Range : IEnumerable<int>
    {
        public static Range CreateByEndExclusive(int begin, int endExclusive)
        {
            return new Range(begin, endExclusive - begin);
        }
        public static Range CreateByEndInclusive(int begin, int endInclusive)
        {
            return new Range(begin, endInclusive - begin + 1);
        }
        public readonly int Begin;
        public readonly int Count;
        public int EndExclusive
        {
            get
            {
                return this.Begin + this.Count;
            }
        }
        public int EndInclusive
        {
            get
            {
                return this.Begin + this.Count - 1;
            }
        }
        public Range(int begin, int count)
        {
            this.Begin = begin;
            this.Count = Math.Max(count, 0);
        }
        public IEnumerable<int> GetIndexes()
        {
            return Enumerable.Range(this.Begin, this.Count);
        }
        private IEnumerable<Range> splitBeginIncludedEndExcluded(SortedSet<int> indexes)
        {
            for (int index = 1; index < indexes.Count; index++)
            {
                Range result = CreateByEndExclusive(indexes.ElementAt(index - 1), indexes.ElementAt(index));
                if (result.Any()) yield return result;
            }
        }
        private IEnumerable<Range> SplitAfter(IEnumerable<int> indexes)
        {
            SortedSet<int> result = new SortedSet<int>(indexes.Select(A => A + 1));
            result.Add(this.Begin);
            result.Add(this.EndExclusive);
            return this.splitBeginIncludedEndExcluded(result);
        }
        public IEnumerable<Range> SplitAfter<T>(Func<int, T> select, Func<T, bool> separatorFunc)
        {
            return this.SplitAfter(this
                .Select(index => new { index = index, item = select(index) })
                .Where(A => separatorFunc(A.item))
                .Select(A => A.index));
        }
        public IEnumerable<Range> SplitAfter<T>(Func<int, bool> separatorFunc)
        {
            return this.SplitAfter(this.Where(A => separatorFunc(A)));
        }
        private IEnumerable<Range> SplitBefore(IEnumerable<int> indexes)
        {
            SortedSet<int> result = new SortedSet<int>(indexes);
            result.Add(this.Begin);
            result.Add(this.EndExclusive);
            return this.splitBeginIncludedEndExcluded(result);
        }
        public IEnumerable<Range> SplitBefore<T>(Func<int, T> select, Func<T, bool> separatorFunc)
        {
            return this.SplitBefore(this
                .Select(index => new { index = index, item = select(index) })
                .Where(A => separatorFunc(A.item))
                .Select(A => A.index));
        }
        public IEnumerable<Range> SplitBefore<T>(Func<int, bool> separatorFunc)
        {
            return this.SplitBefore(this.Where(A => separatorFunc(A)));
        }
        private IEnumerable<Range> GetBlocks(Func<int, bool> beginFunc, Func<int, bool> endFunc, Func<int, int, Range> createRangeFunc)
        {
            int? begin = null;
            for (int index = this.Begin; index < this.EndExclusive; index++)
            {
                if (beginFunc(index)) begin = index;
                if (begin != null)
                {
                    if (endFunc(index))
                    {
                        Range result = createRangeFunc(begin.Value, index);
                        if (result.Any()) yield return result;
                        begin = null;
                    }
                }
            }
        }
        public IEnumerable<Range> GetBlocks<T>(Func<int, T> select, Func<T, bool> beginFunc, Func<T, bool> endFunc, Func<int, int, Range> createRangeFunc)
        {
            return this.GetBlocks(index => beginFunc(select(index)), index => endFunc(select(index)), createRangeFunc);
        }
        public IEnumerator<int> GetEnumerator()
        {
            return this.GetIndexes().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetIndexes().GetEnumerator();
        }
    }
}