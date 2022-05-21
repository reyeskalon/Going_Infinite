using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardJsonConverter
{
    public class CardSorter
    {
        public List<ReducedCard> SortBySet(List<ReducedCard> cards, string setCode)
        {
            List<ReducedCard> result = new List<ReducedCard>();
            foreach (ReducedCard card in cards)
            {
                if(card.Set == setCode)
                {
                    result.Add(card);
                }
            }
            return result;
        }

        public List<ReducedCard> SortByColor(List<ReducedCard> cards, string colorCode, bool multiColor)
        {
            List<ReducedCard> result = new List<ReducedCard>();
            foreach (ReducedCard card in cards)
            {
                if (multiColor == false)
                {
                    if (card.Colors.Contains(colorCode) && card.Colors.Count == 1)
                    {
                        result.Add(card);
                    }
                }
                else
                {
                    if (card.Colors.Contains(colorCode))
                    {
                        result.Add(card);
                    }
                }
            }
            return result;
        }
        public List<ReducedCard> SortByColorless(List<ReducedCard> cards)
        {
            List<ReducedCard> result = new List<ReducedCard>();
            foreach (ReducedCard card in cards)
            {
                if(card.Colors.Count == 0 && card.ManaCost.Length > 0)
                {
                    result.Add(card);
                }
            }
            return result;
        }

        public List<ReducedCard> SortByMultiColor(List<ReducedCard> cards)
        {
            List<ReducedCard> result = new List<ReducedCard>();
            foreach (ReducedCard card in cards)
            {
                if (card.Colors.Count > 1)
                {
                    result.Add(card);
                }
            }
            return result;
        }

        public List<ReducedCard> SortByCardType(List<ReducedCard> cards, string cardType)
        {
            List<ReducedCard> result = new List<ReducedCard>();
            foreach (ReducedCard card in cards)
            {
                if (card.TypeLine.Contains(cardType))
                {
                    result.Add(card);
                }
            }
            return result;
        }

        public List<ReducedCard> CardReducer(List<Card> cards)
        {
            List<ReducedCard> result = new List<ReducedCard>();
            foreach (Card card in cards)
            {
                {
                    ReducedCard reducedCard = new ReducedCard();
                    reducedCard.Id = card.Id;
                    reducedCard.Name = card.Name;
                    reducedCard.Rarity = card.Rarity;
                    reducedCard.ManaCost = card.ManaCost;
                    reducedCard.Cmc = card.Cmc;
                    reducedCard.TypeLine = card.TypeLine;
                    reducedCard.OracleText = card.OracleText;
                    reducedCard.Colors = card.Colors;
                    reducedCard.ColorIdentity = card.ColorIdentity;
                    reducedCard.Keywords = card.Keywords;
                    reducedCard.Set = card.Set;
                    reducedCard.SetName = card.SetName;
                    if(card.ImageUris != null)
                    {
                        reducedCard.Image = card.ImageUris.Normal;
                        reducedCard.ArtCrop = card.ImageUris.ArtCrop;
                    }
                    else
                    {
                        reducedCard.Image = null;
                        reducedCard.ArtCrop = null;
                    }

                    result.Add(reducedCard);
                }
            }
            return result;
        }
    }
}
