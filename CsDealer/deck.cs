using System;
using System.Collections.Generic;
using System.Linq;

public class Deck : Stack
{
    //===============================================================================
    // Deck Class
    //===============================================================================
 
    private List<Card> _cards;
    public bool jokers;
    public int numJokers;
    public bool build;
    public bool rebuild;
    public bool reshuffle;
    public new Dictionary<string, Dictionary<string, int>> ranks;
    public int decksUsed;
    
    public Deck(List<Card> cards = null, bool jokers = false, int numJokers = 0, 
        bool build = true, bool rebuild = false, bool reshuffle = false,
        Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (cards == null)
        {
            cards = new List<Card>();
        }
        
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;
        }
        
        this._cards = cards;
        this.jokers = jokers;
        this.numJokers = numJokers;
        this.rebuild = rebuild;
        this.reshuffle = reshuffle;
        this.ranks = ranks;
        this.decksUsed = 0;

        if (build)
        {
            Build();
        } 
    }


    public static Deck operator+ (Deck deck, object other)
    {
        Deck newDeck;

        if (other.GetType() == typeof(Stack))
        {
            List<Card> cardList = new List<Card>();
            
            cardList.AddRange(deck.Cards);
            
            cardList.AddRange(((Stack)other).Cards);
            
            newDeck = new Deck(cards: cardList);
        }
        else if (other.GetType() == typeof(Deck))
        {
            List<Card> cardList = new List<Card>();
            
            cardList.AddRange(deck.Cards);
            
            cardList.AddRange(((Deck)other).Cards);
            
            newDeck = new Deck(cards: cardList);
        }
        else if (other is List<Card>)
        {
            List<Card> cardList = new List<Card>();
            
            cardList.AddRange(deck.Cards);
            
            List<Card> otherCards = other as List<Card>;
            cardList.AddRange(otherCards);
            
            newDeck = new Deck(cards: cardList);
        }
        else
        {
            throw new System.ArgumentException("Object on the right side of '+' must be of"
                + " type Stack or Deck or be a list of Card instances");
        }
        
        return newDeck;
    }


    public string Repr()
    {
        return $"Deck(cards={Cards})";
    }


    public void Build(bool jokers = false, int numJokers = 0)
    {
        if (jokers == false)
        {
            jokers = this.jokers;
        }

        if (numJokers == 0)
        {
            numJokers = this.numJokers;
        }

        this.decksUsed += 1;

        Cards.AddRange(Tools.BuildCards(jokers, numJokers));
    }


    public Stack Deal(int num = 1, bool rebuild = false, bool shuffle = false, 
        string end = Const.TOP)
    {
        if (num <= 0)
        {
            throw new System.ArgumentException("The 'num' parameter must be >= 1.");
        }
        
        List<Card> dealtCards = new List<Card>();
        int _num = num;
        
        if (!rebuild)
        {
            rebuild = this.rebuild;
        }

        if (!shuffle)
        {
            shuffle = this.reshuffle;
        }

        Card card;
        int n;

        while (num > 0)
        {
            n = _num - num;

            try
            {
                if (end == Const.TOP)
                {
                    card = Cards[0];
                    Cards.RemoveAt(0);
                }
                else
                {
                    card = Cards[-1];
                    Cards.RemoveAt(-1);
                }

                dealtCards[n] = card;
                num -= 1;
            }
            catch (System.IndexOutOfRangeException)
            {
                if (Size == 0)
                {
                    if (rebuild)
                    {
                        Build();
                        
                        if (shuffle)
                        {
                            Shuffle();
                        }
                    }
                }
                else
                {
                    break;
                }
            }     
        }
        
        return new Stack(cards: dealtCards);
    }

    //===============================================================================
    // Helper Functions
    //===============================================================================
    
    public static Deck ConvertToDeck(Stack stack)
    {
        List<Card> cardStack = stack.Cards;

        return new Deck(cardStack);
    }
}
