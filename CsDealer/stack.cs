using System;
using System.Collections.Generic;
using System.Linq;

public class Stack
{
    //===============================================================================
    // Stack Class
    //===============================================================================
 
    public List<Card> _cards;
    public Dictionary<string, Dictionary<string, int>> ranks;
    public int _i;
    
    public Stack(List<Card> cards = null, bool sort = false,
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
        this.ranks = ranks;
        this._i = 0; // What is this for?

        if (sort)
        {
            Sort(this.ranks);
        }
    }


    public static Stack operator+ (Stack stack, object other)
    {
        Stack newStack;

        if (other is Stack)
        {
            List<Card> cardList = new List<Card>();
            
            cardList.AddRange(stack.Cards);
            
            cardList.AddRange(((Stack)other).Cards);
            
            newStack = new Stack(cards: cardList);
        }
        else if (other is Deck)
        {
            List<Card> cardList = new List<Card>();
            
            cardList.AddRange(stack.Cards);
            
            cardList.AddRange(((Deck)other).Cards);
            
            newStack = new Stack(cards: cardList);
        }
        else if (other is List<Card>)
        {
            List<Card> cardList = new List<Card>();
            
            cardList.AddRange(stack.Cards);
            
            List<Card> otherCards = other as List<Card>;
            cardList.AddRange(otherCards);
            
            newStack = new Stack(cards: cardList);
        }
        else
        {
            throw new System.ArgumentException("Object on the right side of '+' must be of"
                + " type Stack or Deck or be a list of Card instances");
        }
        
        return newStack;
    }
    




/*
    def __contains__(self, card):
        """
        Allows for Card instance (not value & suit) inclusion checks.
 
        :arg Card card:
            The Card instance to check for.
 
        :returns:
            Whether or not the Card instance is in the Deck.
 
        """
        return id(card) in [id(x) for x in self.cards]
 
    def __delitem__(self, indice):
        """
        Allows for deletion of a Card instance, using del.
 
        :arg int indice:
            The indice to delete.
 
        """
        del self.cards[indice]
 
    def __eq__(self, other):
        """
        Allows for Stack comparisons. Checks to see if the given ``other``
        contains the same cards, in the same order (based on value & suit,
        not instance).
 
        :arg other:
            The other ``Stack``/``Deck`` instance or ``list`` to compare to.
 
        :returns:
            ``True`` or ``False``.
 
        """
        if len(self.cards) == len(other):
            for i, card in enumerate(self.cards):
                if card != other[i]:
                    return False
            return True
        else:
            return False
 
    def __getitem__(self, key):
        """
        Allows for accessing, and slicing of cards, using ``Deck[indice]``,
        ``Deck[start:stop]``, etc.
 
        :arg int indice:
            The indice to get.
 
        :returns:
            The ``Card`` at the given indice.
 
        """
        self_len = len(self)
        if isinstance(key, slice):
            return [self[i] for i in xrange(*key.indices(self_len))]
        elif isinstance(key, int):
            if key < 0 :
                key += self_len
            if key >= self_len:
                raise IndexError("The index ({}) is out of range.".format(key))
            return self.cards[key]
        else:
            raise TypeError("Invalid argument type.")
 
    def __len__(self):
        """
        Allows check the Stack length, with len.
 
        :returns:
            The length of the stack (self.cards).
 
        """
        return len(self.cards)
 
    def __ne__(self, other):
        """
        Allows for Stack comparisons. Checks to see if the given ``other``
        does not contain the same cards, in the same order (based on value &
        suit, not instance).
 
        :arg other:
            The other ``Stack``/``Deck`` instance or ``list`` to compare to.
 
        :returns:
            ``True`` or ``False``.
 
        """
        if len(self.cards) == len(other):
            for i, card in enumerate(self.cards):
                if card != other[i]:
                    return True
            return False
        else:
            return True
 
    def __repr__(self):
        """
        The repr magic method.
 
        :returns:
            A representation of the ``Deck`` instance.
 
        """
        return "Stack(cards=%r)" % (self.cards)
 
    def __setitem__(self, indice, value):
        """
        Assign cards to specific stack indices, like a list.
 
        Example:
            stack[16] = card_object
 
        :arg int indice:
            The indice to set.
        :arg Card value:
            The Card to set the indice to.
 
        """
        self.cards[indice] = value
 
    def __str__(self):
        """
        Allows users to print a human readable representation of the ``Stack``
        instance, using ``print``.
 
        :returns:
            A str of the names of the cards in the stack.
 
        """
        card_names = "".join([x.name + "\n" for x in self.cards]).rstrip("\n")
        return "%s" % (card_names)
 
    def add(self, cards, end=TOP):
        """
        Adds the given list of ``Card`` instances to the top of the stack.
 
        :arg cards:
            The cards to add to the ``Stack``. Can be a single ``Card``
            instance, or a ``list`` of cards.
        :arg str end:
            The end of the ``Stack`` to add the cards to. Can be ``TOP`` ("top")
            or ``BOTTOM`` ("bottom").
 
        """
        if end is TOP:
            try:
                self.cards += cards
            except:
                self.cards += [cards]
        elif end is BOTTOM:
            try:
                self.cards.extendleft(cards)
            except:
                self.cards.extendleft([cards])
 
    */

    public List<Card> Cards
    {
        get
        {
            return this._cards;
        }
        set
        {
            this._cards = value;
        }
    }

    /*
    def deal(self, num=1, end=TOP):
        """
        Returns a list of cards, which are removed from the Stack.
 
        :arg int num:
            The number of cards to deal.
        :arg str end:
            Which end to deal from. Can be ``0`` (top) or ``1`` (bottom).
 
        :returns:
            The given number of cards from the stack.
 
        """
        ends = {TOP: self.cards.pop, BOTTOM: self.cards.popleft}
 
        self_size = self.size
 
        if num <= self_size:
            dealt_cards = [None] * num
        else:
            num = self_size
            dealt_cards = [None] * self_size
 
        if self_size:
            for n in xrange(num):
                try:
                    card = ends[end]()
                    dealt_cards[n] = card
                except:
                    break
 
            return Stack(cards=dealt_cards)
        else:
            return Stack()
 
    def empty(self, return_cards=False):
        """
        Empties the stack, removing all cards from it, and returns them.
 
        :arg bool return_cards:
            Whether or not to return the cards.
 
        :returns:
            If ``return_cards=True``, a list containing the cards removed
            from the Stack.
 
        """
        cards = list(self.cards)
        self.cards = []
 
        if return_cards:
            return cards
 
    def find(self, term, limit=0, sort=False, ranks=None):
        """
        Searches the stack for cards with a value, suit, name, or
        abbreviation matching the given argument, 'term'.
 
        :arg str term:
            The search term. Can be a card full name, value, suit,
            or abbreviation.
        :arg int limit:
            The number of items to retrieve for each term. ``0`` equals
            no limit.
        :arg bool sort:
            Whether or not to sort the results.
        :arg dict ranks:
            The rank dict to reference for sorting. If ``None``, it will
            default to ``DEFAULT_RANKS``.
 
        :returns:
            A list of stack indices for the cards matching the given terms,
            if found.
 
        """
        ranks = ranks or self.ranks
        found_indices = []
        count = 0
 
        if not limit:
            for i, card in enumerate(self.cards):
                if check_term(card, term):
                    found_indices.append(i)
        else:
            for i, card in enumerate(self.cards):
                if count < limit:
                    if check_term(card, term):
                        found_indices.append(i)
                        count += 1
                else:
                    break
 
        if sort:
            found_indices = sort_card_indices(self, found_indices, ranks)
 
        return found_indices
 
    def find_list(self, terms, limit=0, sort=False, ranks=None):
        """
        Searches the stack for cards with a value, suit, name, or
        abbreviation matching the given argument, 'terms'.
 
        :arg list terms:
            The search terms. Can be card full names, suits, values,
            or abbreviations.
        :arg int limit:
            The number of items to retrieve for each term.
        :arg bool sort:
            Whether or not to sort the results, by poker ranks.
        :arg dict ranks:
            The rank dict to reference for sorting. If ``None``, it will
            default to ``DEFAULT_RANKS``.
 
        :returns:
            A list of stack indices for the cards matching the given terms,
            if found.
 
        """
        ranks = ranks or self.ranks
        found_indices = []
        count = 0
 
        if not limit:
            for term in terms:
                for i, card in enumerate(self.cards):
                    if check_term(card, term) and i not in found_indices:
                        found_indices.append(i)
        else:
            for term in terms:
                for i, card in enumerate(self.cards):
                    if count < limit:
                        if check_term(card, term) and i not in found_indices:
                            found_indices.append(i)
                            count += 1
                    else:
                        break
                count = 0
 
        if sort:
            found_indices = sort_card_indices(self, found_indices, ranks)
 
        return found_indices
 
    def get(self, term, limit=0, sort=False, ranks=None):
        """
        Get the specified card from the stack.
 
        :arg term:
            The search term. Can be a card full name, value, suit,
            abbreviation, or stack indice.
        :arg int limit:
            The number of items to retrieve for each term.
        :arg bool sort:
            Whether or not to sort the results, by poker ranks.
        :arg dict ranks:
            The rank dict to reference for sorting. If ``None``, it will
            default to ``DEFAULT_RANKS``.
 
        :returns:
            A list of the specified cards, if found.
 
        """
        ranks = ranks or self.ranks
        got_cards = []
 
        try:
            indices = self.find(term, limit=limit)
            got_cards = [self.cards[i] for i in indices]
            self.cards = [v for i, v in enumerate(self.cards) if
                i not in indices]
        except:
            got_cards = [self.cards[term]]
            self.cards = [v for i, v in enumerate(self.cards) if i is not term]
 
        if sort:
            got_cards = sortCards(got_cards, ranks)
 
        return got_cards
 
    def get_list(self, terms, limit=0, sort=False, ranks=None):
        """
        Get the specified cards from the stack.
 
        :arg term:
            The search term. Can be a card full name, value, suit,
            abbreviation, or stack indice.
        :arg int limit:
            The number of items to retrieve for each term.
        :arg bool sort:
            Whether or not to sort the results, by poker ranks.
        :arg dict ranks:
            The rank dict to reference for sorting. If ``None``, it will
            default to ``DEFAULT_RANKS``.
 
        :returns:
            A list of the specified cards, if found.
 
        """
        ranks = ranks or self.ranks
        got_cards = []
 
        try:
            indices = self.find_list(terms, limit=limit)
            got_cards = [self.cards[i] for i in indices if self.cards[i]
                not in got_cards]
            self.cards = [v for i, v in enumerate(self.cards) if
                i not in indices]
        except:
            indices = []
            for item in terms:
                try:
                    card = self.cards[item]
                    if card not in got_cards:
                        got_cards.append(card)
                        indices.append(item)
                except:
                    indices += self.find(item, limit=limit)
                    got_cards += [self.cards[i] for i in indices if
                        self.cards[i] not in got_cards]
            self.cards = [v for i, v in enumerate(self.cards) if
                i not in indices]
 
        if sort:
            got_cards = sortCards(got_cards, ranks)
 
        return got_cards
 
    def insert(self, card, indice=-1):
        """
        Insert a given card into the stack at a given indice.
 
        :arg Card card:
            The card to insert into the stack.
        :arg int indice:
            Where to insert the given card.
 
        """
        self_size = len(self.cards)
 
        if indice in [0, -1]:
            if indice == -1:
                self.cards.append(card)
            else:
                self.cards.appendleft(card)
        elif indice != self_size:
            half_x, half_y = self.split(indice)
            self.cards = list(half_x.cards) + [card] + list(half_y.cards)
 
 
    def insert_list(self, cards, indice=-1):
        """
        Insert a list of given cards into the stack at a given indice.
 
        :arg list cards:
            The list of cards to insert into the stack.
        :arg int indice:
            Where to insert the given cards.
 
        """
        self_size = len(self.cards)
 
        if indice in [0, -1]:
            if indice == -1:
                self.cards += cards
            else:
                self.cards.extendleft(cards)
        elif indice != self_size:
            half_x, half_y = self.split(indice)
            self.cards = list(half_x.cards) + list(cards) + list(half_y.cards)
 
    */


    public void Insert(List<Card> cards, int index = -1)
    {
        int size = Size;

        if (index < 0 && size + index >= 0)
        {
            index += size;
        }
        else if (index < 0 || index >= size)
        {
            throw new System.ArgumentException("Parameter 'index' must be between"
                + $" {-size} and {size - 1}, inclusive.");
        }

        if (index == size - 1)
        {
            Cards.AddRange(cards);
        }
        else if (index == 0)
        {
            Cards = cards.Concat(Cards) as List<Card>;
        }
        else
        {
            var splitCards = this.Split(index, false);
            List<Card> beforeCards = splitCards.Item1.Cards;
            List<Card> afterCards = splitCards.Item2.Cards;
            Cards = beforeCards.Concat(cards).Concat(afterCards) as List<Card>;
        }
    }


    public bool IsSorted(Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = this.ranks;
        }

        return Tools.CheckSorted(Cards, ranks);
    }
    

    public void OpenCards(string filename = null)
    {
        Cards = Tools.OpenCards(filename);
    }


    public Card RandomCard(bool remove_ = false)
    {
        return Tools.RandomCard(Cards, remove_);
    }


    public void Reverse()
    {
        Cards.Reverse();
    }


    public void SaveCards(string filename = null)
    {
        Tools.SaveCards(Cards, filename);
    }


    private static Random random = new Random();

    public void Shuffle(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            int n = Cards.Count;  
            while (n > 1) 
            {  
                n--;  
                int k = random.Next(n + 1);  
                Card card = Cards[k];  
                Cards[k] = Cards[n];  
                Cards[n] = card;  
            }
        }
    }


    public int Size
    {
        get
        {
            return Cards.Count;
        }
    }

    
    public void Sort(Dictionary<string, Dictionary<string, int>> ranks = null)
    {
        if (ranks == null)
        {
            ranks = this.ranks;
        }

        Cards = Tools.SortCards(Cards, ranks);
    }


    public Tuple<Stack,Stack> Split(int index = 0, bool halve = true) 
    // Extra parameter solves some issues. Also, method incorporates negative indicies.
    {
        int size = Size;
        
        if (size > 1)
        {
            if (index < 0 && size + index >= 0)
            {
                index += size;
            }
            else if (index < 0 || index >= size)
            {
                throw new System.ArgumentException("Parameter 'index' must be between"
                    + $" {-size} and {size - 1}, inclusive.");
            }

            if (index == 0 && halve == true)
            {
                int mid;
                
                if (size % 2 == 0)
                {
                    mid = size / 2;
                }
                else
                {
                    mid = (size - 1) / 2;
                }
                
                return Tuple.Create(new Stack(cards: Cards.GetRange(0, mid - 1)), 
                    new Stack(cards: Cards.GetRange(mid, size - mid - 1)));
            }
            else
            {
                return Tuple.Create(new Stack(cards: Cards.GetRange(0, index - 1)), 
                    new Stack(cards: Cards.GetRange(index, size - index - 1)));
            }
        }
        else
        {
            return Tuple.Create(new Stack(cards: Cards), new Stack());
        }
    }

    //===============================================================================
    // Helper Functions
    //===============================================================================

    public static Stack ConvertToStack(Deck deck)
    {
        return new Stack(deck.Cards);
    }
}
