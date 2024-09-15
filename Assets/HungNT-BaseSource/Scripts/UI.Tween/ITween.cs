using Cysharp.Threading.Tasks;

namespace BaseSource
{
    public interface ITween
    {
        void Init();
        UniTask Show();
        UniTask Hide();
    }
}