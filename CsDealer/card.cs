using System;
using System.Globalization;
using System.Collections.Generic;

public class Card
{
    //===============================================================================
    // Card Class
    //===============================================================================
    
    public string value;
    public string suit;
    public string abbrev;
    public string name;
    
    public Card(string value, string suit)
    {
        this.value = value.ToUpper();
        this.suit = suit.ToUpper();
        
        this.abbrev = CardAbbrev(this.value, this.suit);
        this.name = CardName(this.value, this.suit);
    }
 
    
    public static bool operator== (Card leftCard, Card rightCard)
    {
        return leftCard.value == rightCard.value && leftCard.suit == rightCard.suit;
    }


    public static bool operator!= (Card leftCard, Card rightCard)
    {
        return leftCard.value != rightCard.value || leftCard.suit != rightCard.suit;
    }


    public static bool operator>= (Card leftCard, Card rightCard)
    {
        return Const.DEFAULT_RANKS["values"][leftCard.value] > Const.DEFAULT_RANKS["values"][rightCard.value]
            || (
                Const.DEFAULT_RANKS["values"][leftCard.value] >= Const.DEFAULT_RANKS["values"][rightCard.value]
                &&
                Const.DEFAULT_RANKS["suits"][leftCard.suit] >= Const.DEFAULT_RANKS["suits"][rightCard.suit]
                );
    }


    public static bool operator> (Card leftCard, Card rightCard)
    {
        return Const.DEFAULT_RANKS["values"][leftCard.value] > Const.DEFAULT_RANKS["values"][rightCard.value]
            || (
                Const.DEFAULT_RANKS["values"][leftCard.value] >= Const.DEFAULT_RANKS["values"][rightCard.value]
                &&
                Const.DEFAULT_RANKS["suits"][leftCard.suit] > Const.DEFAULT_RANKS["suits"][rightCard.suit]
                );
    }


    public static bool operator<= (Card leftCard, Card rightCard)
    {
        return Const.DEFAULT_RANKS["values"][leftCard.value] < Const.DEFAULT_RANKS["values"][rightCard.value]
            || (
                Const.DEFAULT_RANKS["values"][leftCard.value] <= Const.DEFAULT_RANKS["values"][rightCard.value]
                &&
                Const.DEFAULT_RANKS["suits"][leftCard.suit] <= Const.DEFAULT_RANKS["suits"][rightCard.suit]
                );
    }


    public static bool operator< (Card leftCard, Card rightCard)
    {
        return Const.DEFAULT_RANKS["values"][leftCard.value] < Const.DEFAULT_RANKS["values"][rightCard.value]
            || (
                Const.DEFAULT_RANKS["values"][leftCard.value] <= Const.DEFAULT_RANKS["values"][rightCard.value]
                &&
                Const.DEFAULT_RANKS["suits"][leftCard.suit] < Const.DEFAULT_RANKS["suits"][rightCard.suit]
                );
    }


    public override int GetHashCode()
    {
        Tuple<string,string> cardTuple = new Tuple<string,string>(this.value, this.suit);

        return cardTuple.GetHashCode();
    }


    public string Repr()
    {
        return $"Card(value={this.value}, suit={this.suit})";
    }


    public string Str()
    {
        return $"{this.name}";
    }


    public bool Equals(object other, Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;   
        }

        if (other is Card)
        {
            Card _other = (Card)other;

            if (ranks.ContainsKey("suits"))
            {
                return ranks["values"][this.value] == ranks["values"][_other.value]
                    &&
                    ranks["suits"][this.suit] == ranks["suits"][_other.suit];
            }
            else
            {
                return ranks["values"][this.value] == ranks["values"][_other.value];
            }
        }
        else
        {
            return false;
        }
    }


    public bool GreaterThanOrEqual(object other, Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;   
        }

        if (other is Card)
        {
            Card _other = (Card)other;

            if (ranks.ContainsKey("suits"))
            {
                return ranks["values"][this.value] >= ranks["values"][_other.value]
                    || (
                        ranks["suits"][this.suit] >= ranks["suits"][_other.suit]
                        &&
                        ranks["suits"][this.suit] >= ranks["suits"][_other.suit]
                        );
            }
            else
            {
                return ranks["values"][this.value] >= ranks["values"][_other.value];
            }
        }
        else
        {
            return false;
        }
    }


    public bool GreaterThan(object other, Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;   
        }

        if (other is Card)
        {
            Card _other = (Card)other;

            if (ranks.ContainsKey("suits"))
            {
                return ranks["values"][this.value] > ranks["values"][_other.value]
                    || (
                        ranks["suits"][this.suit] >= ranks["suits"][_other.suit]
                        &&
                        ranks["suits"][this.suit] > ranks["suits"][_other.suit]
                        );
            }
            else
            {
                return ranks["values"][this.value] > ranks["values"][_other.value];
            }
        }
        else
        {
            return false;
        }
    }


    public bool LessThanOrEqual(object other, Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;   
        }

        if (other is Card)
        {
            Card _other = (Card)other;

            if (ranks.ContainsKey("suits"))
            {
                return ranks["values"][this.value] <= ranks["values"][_other.value]
                    || (
                        ranks["suits"][this.suit] <= ranks["suits"][_other.suit]
                        &&
                        ranks["suits"][this.suit] <= ranks["suits"][_other.suit]
                        );
            }
            else
            {
                return ranks["values"][this.value] <= ranks["values"][_other.value];
            }
        }
        else
        {
            return false;
        }
    }


    public bool LessThan(object other, Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;   
        }

        if (other is Card)
        {
            Card _other = (Card)other;

            if (ranks.ContainsKey("suits"))
            {
                return ranks["values"][this.value] < ranks["values"][_other.value]
                    || (
                        ranks["suits"][this.suit] <= ranks["suits"][_other.suit]
                        &&
                        ranks["suits"][this.suit] < ranks["suits"][_other.suit]
                        );
            }
            else
            {
                return ranks["values"][this.value] < ranks["values"][_other.value];
            }
        }
        else
        {
            return false;
        }
    }


    public bool NotEqual(object other, Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = Const.DEFAULT_RANKS;   
        }

        if (other is Card)
        {
            Card _other = (Card)other;

            if (ranks.ContainsKey("suits"))
            {
                return ranks["values"][this.value] != ranks["values"][_other.value]
                    &&
                    ranks["suits"][this.suit] != ranks["suits"][_other.suit];
            }
            else
            {
                return ranks["values"][this.value] != ranks["values"][_other.value];
            }
        }
        else
        {
            return true;
        }
    }


    //===============================================================================
    // Helper Functions
    //===============================================================================

    public string CardAbbrev(string value, string suit)
    {
        if (value == "Joker")
        {
            return "JKR";
        }    
        else if (value == "10")
        {
            return $"10{suit[0]}";
        }
        else
        {
            return $"{value[0]}{suit[0]}";
        }
    }


    public string CardName(string value, string suit)
    {
        if (value == "Joker")
        {
            return "Joker";
        }
        else
        {
            return $"{value} of {suit}";
        }
    }

}
