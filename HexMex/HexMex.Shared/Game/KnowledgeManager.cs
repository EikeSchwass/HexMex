using System;

namespace HexMex.Game
{
    public class KnowledgeManager
    {
        private int knowledge1;
        private int knowledge2;
        private int knowledge3;
        public event Action<KnowledgeManager> KnowledgeCountChanged;
        public int Knowledge1
        {
            get => knowledge1;
            set
            {
                knowledge1 = value;
                KnowledgeCountChanged?.Invoke(this);
            }
        }
        public int Knowledge2
        {
            get => knowledge2;
            set
            {
                knowledge2 = value;
                KnowledgeCountChanged?.Invoke(this);
            }
        }
        public int Knowledge3
        {
            get => knowledge3;
            set
            {
                knowledge3 = value;
                KnowledgeCountChanged?.Invoke(this);
            }
        }
    }
}