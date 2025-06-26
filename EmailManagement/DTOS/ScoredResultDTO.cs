namespace EmailManagement.DTOS
{
    public class NodeScoredResult<TKey, TValue, TWeigth>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public TWeigth Weight { get; set; }
        public NodeScoredResult<TKey, TValue, TWeigth> Next { get; set; }

        public NodeScoredResult(TKey key, TValue value, TWeigth weigth)
        {
            Key = key;
            Value = value;
            Weight = weigth;
            Next = null;
        }
    }

    public class ScoredResultDTO<TKey, TValue, TWeigth>
    {
        private NodeScoredResult<TKey, TValue, TWeigth> head;

        public ScoredResultDTO()
        {
            head = null;
        }

        public void AddFirst(TKey key, TValue value, TWeigth weigth) 
        {
            NodeScoredResult<TKey,TValue,TWeigth> newNodeScored = new NodeScoredResult<TKey, TValue, TWeigth>(key, value, weigth);
            newNodeScored.Next = head;
            head = newNodeScored;
        }

    }
}
