namespace Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory class for building <see cref="ISet{T}">decks</see> of <see cref="Card"/>s.
    /// </summary>
    public class DeckFactory
    {
        private string[] seeds;
        public IList<string> Seeds
        {
            get
            {
                return seeds != null ? seeds.ToList() : null;
            }
            set
            {
                seeds = value != null ? value.ToArray() : null;
            }
        }

        private string[] names;
        public IList<string> Names
        {
            get
            {
                return names != null ? names.ToList() : null;
            }
            set
            {
                names = value != null ? value.ToArray() : null;
            }
        }

        public int DeckSize
        {
            get
            {
                return (Names != null ? Names.Count : 0) * (Seeds != null ? Seeds.Count : 0);
            }
        }

        public ISet<Card> Deck
        {
            get
            {
                if (Names == null || Seeds == null)
                {
                    throw new InvalidOperationException();
                }

                return new HashSet<Card>(Enumerable
                    .Range(0, Names.Count)
                    .SelectMany(i => Enumerable
                        .Repeat(i, Seeds.Count)
                        .Zip(
                            Enumerable.Range(0, Seeds.Count),
                            (n, s) => Tuple.Create(Names[n], Seeds[s], n)))
                    .Select(tuple => new Card(tuple))
                .ToList());
            }
        }
    }
}
