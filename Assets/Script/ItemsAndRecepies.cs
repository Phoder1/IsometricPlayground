namespace Assets.Recepies {
    public class Item {
        public readonly string name;  
        public readonly int id;

        private Item(string _name, int _id) {
            name = _name;
            id = _id;
        }

        public static Item Flower { get { return new Item("Flower", 1); } }
    }


    
    public class ItemSlot
    {
        public Item item;
        public int amount;

        public ItemSlot(Item _item, int _amount) {
            item = _item;
            amount = _amount;
        }
    }

    public class Recipe {
        public readonly ItemSlot[] recipeIngridients;
        public readonly Item outcome;

        private Recipe(ItemSlot[] _recipe, Item _outcome) {
            recipeIngridients = _recipe;
            outcome = _outcome;

        }

        public static Recipe Flower { 
            get { 
                return new Recipe( 
                    new ItemSlot[1] { new ItemSlot(Item.Flower, 5) }
                    ,  
                    Item.Flower 
                    ); 
            } 
        }
    }
}
