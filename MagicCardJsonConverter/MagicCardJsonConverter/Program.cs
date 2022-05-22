using Newtonsoft.Json;
using MagicCardJsonConverter;

CardSorter sorter = new CardSorter();

List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(File.ReadAllText("C:/Users/reyes/Projects/going_infinite/Going_Infinite/MagicCardJsonConverter/default-cards-20220521210604.json"));
List<ReducedCard> magicCards = sorter.CardReducer(cards);


List<ReducedCard> newCappenaCards = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsWhite = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsBlue = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsBlack = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsRed = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsGreen = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsMultiColor = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsColorless = new List<ReducedCard>();
List<ReducedCard> newCappenaCardsLands = new List<ReducedCard>();

newCappenaCards = sorter.SortBySet(magicCards, "snc");

newCappenaCardsWhite = sorter.SortByColor(newCappenaCards, "W", false);
newCappenaCardsBlue = sorter.SortByColor(newCappenaCards, "U", false);
newCappenaCardsBlack = sorter.SortByColor(newCappenaCards, "B", false);
newCappenaCardsRed = sorter.SortByColor(newCappenaCards, "R", false);
newCappenaCardsGreen = sorter.SortByColor(newCappenaCards, "G", false);
newCappenaCardsMultiColor = sorter.SortByMultiColor(newCappenaCards);
newCappenaCardsColorless = sorter.SortByColorless(newCappenaCards);
newCappenaCardsLands = sorter.SortByCardType(newCappenaCards, "Land");

newCappenaCardsWhite.Sort((a, b) => a.Name.CompareTo(b.Name));
newCappenaCardsBlue.Sort((a, b) => a.Name.CompareTo(b.Name));
newCappenaCardsBlack.Sort((a, b) => a.Name.CompareTo(b.Name));
newCappenaCardsRed.Sort((a, b) => a.Name.CompareTo(b.Name));
newCappenaCardsGreen.Sort((a, b) => a.Name.CompareTo(b.Name));
newCappenaCardsColorless.Sort((a, b) => a.Name.CompareTo(b.Name));
newCappenaCardsMultiColor.Sort((a, b) => a.Name.CompareTo(b.Name));

newCappenaCards = (newCappenaCardsWhite);
newCappenaCards.AddRange(newCappenaCardsBlue);
newCappenaCards.AddRange(newCappenaCardsBlack);
newCappenaCards.AddRange(newCappenaCardsRed);
newCappenaCards.AddRange(newCappenaCardsGreen);
newCappenaCards.AddRange(newCappenaCardsMultiColor);
newCappenaCards.AddRange(newCappenaCardsColorless);
newCappenaCards.AddRange(newCappenaCardsLands);

StreamWriter sw = new StreamWriter("C:/Users/reyes/Projects/going_infinite/Going_Infinite/Vue/src/NewCappenaCards.json");
sw.WriteLine('[');
for (int i = 0; i < newCappenaCards.Count; i++)
{
    if (i != newCappenaCards.Count - 1)
    {
        string cardToAdd = JsonConvert.SerializeObject(newCappenaCards[i]);
        sw.WriteLine("\n" + cardToAdd + ',');
    }
    else
    {
        string cardToAdd = JsonConvert.SerializeObject(newCappenaCards[i]);
        sw.WriteLine("\n" + cardToAdd);
    }

}
sw.WriteLine(']');
sw.Close();

sw = new StreamWriter("C:/Users/reyes/Projects/going_infinite/Going_Infinite/Vue/src/InsertStreetsOfNewCappenaCardData.sql");
foreach (ReducedCard card in newCappenaCards)
{
    string colors = "";
    string colorIdentity = "";
    string keywords = "";
    string oracleText = "";
    string setName = "";
    string name = "";
    if (card.Colors != null)
    {
        for (int i = 0; i < card.Colors.Count; i++)
        {
            if (i != card.Colors.Count - 1)
            {
                colors += card.Colors[i] + ",";
            }
            else
            {
                colors += card.Colors[i];
            }
        }
    }
    if (card.ColorIdentity != null)
    {
        for (int j = 0; j < card.ColorIdentity.Count; j++)
        {
            if (j != card.ColorIdentity.Count - 1)
            {
                colorIdentity += card.ColorIdentity[j] + ",";
            }
            else
            {
                colorIdentity += card.ColorIdentity[j];
            }
        }
    }
    if (card.Keywords != null)
    {
        for (int k = 0; k < card.Keywords.Count; k++)
        {
            string keywordToAdd = "";
            for (int i = 0; i < card.Keywords[k].Length; i++)
            {
                if (card.Keywords[k][i] != '\'')
                {
                    keywordToAdd += card.Keywords[k][i];
                }
                else
                {
                    keywordToAdd += "''";
                }
            }
            if (k != card.Keywords.Count - 1)
            {
                keywords += keywordToAdd + ",";
            }
            else
            {
                keywords += keywordToAdd;
            }
        }
    }
    if (card.OracleText != null)
    {
        for (int i = 0; i < card.OracleText.Length; i++)
        {
            if (card.OracleText[i] != '\'')
            {
                oracleText += card.OracleText[i];
            }
            else
            {
                oracleText += "''";
            }
        }
    }
    if (card.SetName != null)
    {
        for (int i = 0; i < card.SetName.Length; i++)
        {
            if (card.SetName[i] != '\'')
            {
                setName += card.SetName[i];
            }
            else
            {
                setName += "''";
            }
        }
    }
    if (card.Name != null)
    {
        for (int i = 0; i < card.Name.Length; i++)
        {
            if (card.Name[i] != '\'')
            {
                name += card.Name[i];
            }
            else
            {
                name += "''";
            }
        }
    }
    sw.WriteLine("INSERT INTO cards (card_id, name, rarity, mana_cost, cmc, type_line, oracle_text, colors, color_identity, keywords, set_code, set_name, image, art_crop)");
    sw.WriteLine($"VALUES ('{card.Id}', '{name}', '{card.Rarity}', '{card.ManaCost}', {card.Cmc}, '{card.TypeLine}', '{oracleText}', '{colors}', '{colorIdentity}', '{keywords}', '{card.Set}', '{setName}', '{card.Image}', '{card.ArtCrop}');");
}
sw.Close();