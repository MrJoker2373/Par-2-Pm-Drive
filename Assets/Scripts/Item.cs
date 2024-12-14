namespace CarGame
{
    using UnityEngine;

    [System.Serializable]
    public struct Item
    {
        public string Title;
        public string Description;
        public int Price;
        public Sprite Sprite;
        public ItemType Type;
        public CarType CarType;

        public enum ItemType
        {
            Car,
            Boost,
            Punch,
            Shield
        }
    }
}