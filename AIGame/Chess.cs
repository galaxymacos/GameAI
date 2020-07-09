namespace AIGame
{
    public class Chess
    {
        public OwnerType owner;

        public enum OwnerType
        {
            Player,
            AI,
            NoBody
        }

        public Chess(OwnerType owner)
        {
            this.owner = owner;
        }

        public override string ToString()
        {
            switch (owner)
            {
                case OwnerType.Player:
                    return "X";
                case OwnerType.AI:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}