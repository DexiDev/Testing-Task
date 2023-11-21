namespace Game.Data.Fields
{
    public abstract class IntField : IDataField<int>
    {
        public void IncreaseValue(int value)
        {
            SetValue(Value + value);
        }
        
        public void DecreaseValue(int value)
        {
            SetValue(Value - value);
        }
    }
}