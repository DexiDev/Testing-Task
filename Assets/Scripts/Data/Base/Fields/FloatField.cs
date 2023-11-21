namespace Game.Data.Fields
{
    public abstract class FloatField : IDataField<float>
    {
        public void IncreaseValue(float value)
        {
            SetValue(Value + value);
        }
        
        public void DecreaseValue(float value)
        {
            SetValue(Value - value);
        }
    }
}