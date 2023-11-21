using Game.Data;
using UnityEngine;

namespace Game.Loadings
{
    [CreateAssetMenu(menuName = "Data/Loading/Loading Data", fileName = "Loading Data")]
    public class LoadingData : DataScriptable
    {
        [field: SerializeField] public LoadingSettings[] LoadingSettings { get; private set; }
        [field: SerializeField] public float MinDurationLoading { get; private set; }
    }
}